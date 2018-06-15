
var ordersApp = angular.module('ordersApp',
    [
        'ui.router'
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
    $scope.main.order.get($stateParams.id);
}]);

ordersApp.service('OrderService', function ($http, $location) {
    var self = {
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
            calcTotal: function (item) {
                var total = 0;
                angular.forEach(item.lines, function (line) {
                    var parc = (line.price * line.quantity);
                    total += line.discount > 0 ? parc - ((parc * line.discount) / 100) : parc;
                });
                return total;
            }
        },
        order: {
            item: null,
            get: function (id) {
                $http.get('/api/orders/' + id).then(function (response) {
                    if (response.data && response.data.status) {
                        console.log(response.data.items[0]);
                        self.orders.item = response.data.items[0];
                    } else {
                        console.error(response);
                    }
                }, function (error) {
                    console.error(error);
                });
            },
        }
    };

    return self;
});