
var ordersApp = angular.module('ordersApp',
    [
        'ui.router',
        'mgcrea.ngStrap'
    ]);

ordersApp.config(['$stateProvider', '$urlRouterProvider', function ($stateProvider, $urlRouterProvider) {
    $urlRouterProvider.otherwise('/orders');

    $stateProvider
        .state('orders', {
            url: '/orders',
            templateUrl: '/app/views/orders/index.html',
            controller: 'OrdersController'
        })
        .state('order', {
            url: '/order/:id',
            templateUrl: '/app/views/orders/order.html',
            controller: 'OrderController'
        });
}]);

ordersApp.controller('OrdersController', ['$scope', 'OrderService', function ($scope, OrderService) {
    $scope.main = OrderService;
    $scope.main.orders.getAll();
}]);

ordersApp.controller('OrderController', ['$scope', 'OrderService', '$stateParams', function ($scope, OrderService, $stateParams) {
    $scope.main = OrderService;
    $scope.main.order.customer = null;
    $scope.main.order.data = { OrderId: 0, CustomerId: 0, OrderLines: [], DueDate: new Date() };
    $scope.main.order.get($stateParams.id);
    $scope.main.order.getCustomers();
    $scope.main.order.getProducts();
}]);

ordersApp.service('OrderService', function ($http, $location) {
    var self = {
        calcTotal: function (item) {
            var total = 0;
            angular.forEach(item.OrderLines, function (line) {
                total += self.calcTotalPerItem(line);
            });
            return total;
        },
        calcTotalPerItem: function (line) {
            var parc = (line.Price * line.Quantity);
            return line.Discount > 0 ? parc - ((parc * line.Discount) / 100) : parc;
        },
        orders: {
            items: [],
            getAll: function () {
                $http.get('/api/orders').then(function (response) {
                    if (response.data && response.data.status) {
                        self.orders.items = [];
                        angular.forEach(response.data.items, function (item) {
                            self.orders.items.push(item);
                        });
                    } else {
                        console.error(response);
                    }
                }, function (error) {
                    console.error(error);
                });
            },
            removeOrder: function (item, index) {
                if (!confirm('Are you sure about that?'))
                    return;

                $http({
                    method: 'POST',
                    url: '/api/orders/delete',
                    data: { OrderId: item.OrderId },
                    headers: { 'Content-Type': 'application/json' }
                }).then(function (response) {
                    if (response.data && response.data.status) {
                        self.orders.items.splice(index, 1);
                    } else {
                        console.error(response.data.message);
                    }
                }, function (error) {
                    console.error(error);
                });
            }
        },
        order: {
            data: null,
            customer: null,
            customers: [],
            products: [],
            selectedProduct: null,
            get: function (id) {
                if (id > 0) {
                    $http.get('/api/orders/' + id).then(function (response) {
                        if (response.data && response.data.status && response.data.items.length > 0) {
                            self.order.data.OrderId = response.data.items[0].OrderId;
                            self.order.data.CustomerId = response.data.items[0].Customer.CustomerId;
                            self.order.data.Notes = response.data.items[0].Notes;
                            self.order.data.Created = response.data.items[0].Created;
                            self.order.data.DueDate = response.data.items[0].DueDate;
                            self.order.customer = response.data.items[0].Customer;
                            if (response.data.items[0].OrderLines) {
                                angular.forEach(response.data.items[0].OrderLines, function (line) {
                                    self.order.data.OrderLines.push({
                                        ProductId: line.Product.ProductId,
                                        Quantity: line.Quantity,
                                        Discount: line.Discount,
                                        Price: line.Price,
                                        Description: line.Product.Description,
                                        OrderId: response.data.items[0].OrderId,
                                        OrderLineId: line.OrderLineId
                                    });    
                                });
                            }
                        } else {
                            console.error(response);
                        }
                    }, function (error) {
                        console.error(error);
                    });
                }
            },
            getCustomers: function () {
                $http.get('/api/customers').then(function (response) {
                    if (response.data && response.data.status) {
                        self.order.customers = [];
                        angular.forEach(response.data.items, function (item) {
                            self.order.customers.push(item);
                        });
                    } else {
                        console.error(response);
                    }
                }, function (error) {
                    console.error(error);
                });
            },
            getProducts: function () {
                $http.get('/api/products').then(function (response) {
                    if (response.data && response.data.status) {
                        self.order.products = [];
                        angular.forEach(response.data.items, function (item) {
                            self.order.products.push(item);
                        });
                    } else {
                        console.error(response);
                    }
                }, function (error) {
                    console.error(error);
                });
            },
            addCustomer: function (item) {
                self.order.data.CustomerId = item.CustomerId;
                self.order.customer = item;
            },
            addProduct: function (item) {
                self.order.data.OrderLines.push({
                    ProductId: item.ProductId,
                    Quantity: 1,
                    Discount: 0,
                    Price: item.Price,
                    Description: item.Description,
                    OrderId: self.order.data.OrderId
                });
            },
            save: function () {
                $http({
                    method: 'POST',
                    url: '/api/orders/save',
                    data: self.order.data,
                    headers: { 'Content-Type': 'application/json' }
                }).then(function (response) {
                    if (response.data && response.data.status) {
                        $location.path('/orders');
                    } else {
                        console.error(response.data.message);
                    }
                }, function (error) {
                    console.error(error);
                });
            },
            removeLine: function (item, index) {
                if (!confirm('Are you sure about that?'))
                    return;

                if (self.order.data.OrderId > 0) {
                    $http({
                        method: 'POST',
                        url: '/api/order-lines/delete',
                        data: { OrderLineId: item.OrderLineId },
                        headers: { 'Content-Type': 'application/json' }
                    }).then(function (response) {
                        if (response.data && response.data.status) {
                            self.order.data.OrderLines.splice(index, 1);        
                        } else {
                            console.error(response.data.message);
                        }
                    }, function (error) {
                        console.error(error);
                    });
                } else {
                    self.order.data.OrderLines.splice(index, 1);
                }
            }
        }
    };

    return self;
});