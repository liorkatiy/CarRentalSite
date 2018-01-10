angular.module("user", ['utility'])
        .controller("orders", function ($scope, utility) {
            $scope.orders = [];
            //send ajax request to get all the user orders
            $scope.getOrders = function () {
                utility.post(
                    "/request/myorders",
                    {},
                    function (response) {
                        $scope.orders = response.data;
                        $scope.orders.forEach(function (order) {
                            //make sure to change the date from ms to real dates
                            order.Start = utility.DateFromCs(order.Start);
                            order.End = utility.DateFromCs(order.End);
                            order.ReturnDate = utility.DateFromCs(order.ReturnDate);
                        });
                    });
            }
            //filter showing only active orders
            $scope.filterOrders = function (order) {
                if ($scope.onlyNew) {
                    return order.ReturnDate == null;
                }
                return true;
            }
            //start get branches requet
            $scope.getOrders();
        });