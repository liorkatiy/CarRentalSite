angular.module("buyCar", [])
       .controller("buyCar", function ($scope, $http, $window) {
           $scope.creditCard = 0;

           //get the car to display from session storage
           //if not found will redirect to home
           $scope.getCar = function () {
               var car = sessionStorage.getItem("carToRent");
               if (car) {
                   return JSON.parse(car);
               }
               else {
                   alert("Error Redirecting To Home");
                   $window.location.href = "~";
               }
           }
           $scope.car = $scope.getCar();

           //send the credit card number for the server to validate 
           //if validated return to the car search page
           $scope.buyCar = function () {
               $http.post("/request/setorder", { creditcard: $scope.creditCard })
                   .then(function (response) {
                       if (response.data) {
                           alert("Rented");
                       }
                       else {
                           alert("?");
                       }
                       $window.location.href = "/home/cars";
                   });
           }
       });