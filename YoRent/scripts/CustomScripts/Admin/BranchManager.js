/// <reference path="../../angular.js" />
/// <reference path="../utility.js" />
/// <reference path="../fileUpload.js" />
/// <reference path="adminManager.js" />
angular.module("admin")
       .controller("branch", function ($scope, $window, $http, admin,utility) {
           //function to initialize the google maps 
           utility.window().initMap = function () {
               $scope.geocoder = new google.maps.Geocoder();
           }

           //init the branch admin
           $scope.branchHandler = admin.set('branch');

           //get called after the search-item element gget all the branches
           $scope.after = function (branches) {
               branches.forEach(function (branch) {
                   branch.address = "";
                   $scope.getAddress(branch);
               });
           }

           //add new branch
           $scope.add = function (branch) {
               if (branch.id && branch.longitude && branch.latitude) {
                   $scope.branchHandler.add(branch);
               }
           }

           //delete branch
           $scope.delete = function (id,index) {
               $scope.branchHandler.delete(id, index);
           }

           //update branch
           $scope.update = function (branch) {
               if (branch.latitude && branch.longitude && branch.name)
                   $scope.branchHandler.update(branch);
               else
                   alert("missing name or address");
           }

           //get latitude and longitude from address
           $scope.getLatLng = function (branch) {
               $scope.geocoder.geocode({ 'address': branch.address }, function (results, status) {
                   if (status === 'OK') {
                       branch.latitude = results[0].geometry.location.lat();
                       branch.longitude = results[0].geometry.location.lng();
                       branch.address = results[0].formatted_address;
                       $scope.$apply();
                   } else {
                       alert('Geocode was not successful for the following reason: ' + status);
                   }
               });
           }

           //get address from longitude and latitude
           $scope.getAddress = function (branch) {
               var location = { lat: branch.latitude, lng: branch.longitude }
               $scope.geocoder.geocode({ 'location': location }, function (results, status) {
                   if (status === 'OK') {
                       branch.address = results[0].formatted_address;
                       $scope.$apply();
                   } else {
                       alert('Geocode was not successful for the following reason: ' + status);
                   }
               });
           }
       });
