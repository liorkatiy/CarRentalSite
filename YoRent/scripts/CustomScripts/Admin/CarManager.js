/// <reference path="../Utility.js" />
angular.module("admin")
    .controller("car", function ($scope, admin, utility) {
        //create car handler 
        $scope.carHandler = admin.set("Car");
        //create car type handler to b able to choose the car type
        $scope.carTypeHandler = admin.set("CarType");
        $scope.branches = [];
        utility.getBranches($scope.branches);//get all the branches to b able chose the branch

        //add new car
        $scope.add = function (newCar) {
            newCar.branch = parseInt(newCar.branch);
            newCar.cartype = parseInt(newCar.cartype[0]);//the select box is multiple so need to get the first
            $scope.carHandler.add(newCar);
        }

        //update car
        $scope.update = function (car) {
            if (!car.valid)
                car.valid = true;
            $scope.carHandler.update(car);
        }

        //delete car
        $scope.delete = function (regPlate, index) {
            $scope.carHandler.delete(regPlate, index);
        }

        //will b called after search-item element get the results
        $scope.after = function (cars) {
            cars.forEach(function (car) {
                car.valid = car.cartype != null && car.branch != null;
            });
        }

        //show only invalid cars
        $scope.showOnlyValid = function (car,valid) {
            if ($scope.onlyInvalid)
                return !car.valid;
            return true;
        }
    });