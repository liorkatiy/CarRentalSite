/// <reference path="../../angular.js" />
/// <reference path="../Utility.js" />
angular.module("employee", ["utility"])
.controller("returnCar", function ($scope, utility) {
    $scope.orders = [];
    //get all the orders from given id card
    $scope.getOrders = function () {
        utility.post(
            "/employee/getorders",
            { search: $scope.search },
            function (response) {
                $scope.orders = response.data;
                var toDays = utility.dateToDays;
                var cost = utility.CalcCost;

                //set the dates from ms string to real dates 
                $scope.orders.forEach(function (order) {
                    order.Start = utility.DateFromCs(order.Start);
                    order.End = utility.DateFromCs(order.End);
                    var today = new Date();

                    //if the car was returned late
                    if (toDays(order.End) < toDays(today)) {
                        order.totalcost = cost(order.Start, order.End, order.DailyCost);
                        order.totalcost += cost(order.End, today, order.LateFee) - order.LateFee;
                    }
                    else { // if the date is at or before the end date
                        order.totalcost = cost(order.Start, today, order.DailyCost);
                    }
                }
            )
            });
    }

    //set the order as returned in the database
    //and remove the order from orders list
    $scope.returnCar = function (index, id) {
        utility.post("/employee/returncar",
            { orderId: id },
            function (response) {
                if (response.data) {
                    $scope.orders.splice(index, 1);
                }
            });
    }
});