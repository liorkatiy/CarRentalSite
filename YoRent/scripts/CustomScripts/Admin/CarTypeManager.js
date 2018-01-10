angular.module("admin")
       .controller("carType", function ($scope, admin) {
           //preview image for the add car form
           $scope.previewimage = "";
           //create new cartype admin
           $scope.carTypeHandler = admin.set("cartype");

           //add car type
           $scope.add = function (carType) {
               $scope.carTypeHandler.add(carType, null, true);
           }

           //update car type
           $scope.update = function (car) {
               $scope.carTypeHandler.update(car, null, true);
           }

           //delete car type
           $scope.delete = function (id, index) {
               $scope.carTypeHandler.delete(id, index);
           }

       });