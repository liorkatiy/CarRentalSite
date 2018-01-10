angular.module("admin")
      .controller("order", function ($scope, admin, utility) {
          //new order admin
          $scope.orderHandler = admin.set('order');
          //new user admin to get the user in the order
          $scope.userHandler = admin.set('user');
          //new car admin to get the car in the order
          $scope.carHandler = admin.set('car');

          //function to call after the search element get all the orders
          $scope.after = function (orders) {
              orders.forEach(function (order) {
                  order.startdate = utility.DateFromCs(order.startdate);
                  order.enddate = utility.DateFromCs(order.enddate);
                  order.returndate = utility.DateFromCs(order.returndate);
                  //is valid check if the order dont have problem with the foreign keys
                  //and that dates are valid
                  order.isValid = (
                      order.carid != null &&
                      order.userid != null &&
                      order.startdate <= order.enddate);
              });
          }

          //add new order
          $scope.add = function (order) {
              if (order.startdate > order.enddate) {
                  alert("Invalid Dates");
                  return;
              }
              order.userid = order.userid[0];
              order.carid = order.carid[0];
              $scope.orderHandler.add(order);
          }

          //update order
          $scope.update = function (order) {
              order.isValid=(order.carid != null &&
                  order.userid != null && 
                  order.startdate <= order.enddate);
              if (order.isValid) {
                  $scope.orderHandler.update(order);
              }
          }

          //delete order
          $scope.delete = function (id, index) {
              $scope.orderHandler.delete(id, index);
          }

          //filter to show only Invalid orders
          $scope.showOnlyInvalid = function(order){
              if ($scope.onlyInvalid) {
                  return !order.isValid;
              }
              return true;
          }

      });