//have module might not work still learning the geocode api
angular.module("home", ['utility'])
       .controller("homeController", function ($scope, $timeout, utility) {

           //callback function for the geocode
           utility.window().initMap = function () {
               $scope.map = new google.maps.Map(utility.document().getElementById('map'), {
                   center: { lat: 0, lng: 0 },
                   zoom: 19
               });
           }

           //add marker for every branch
           $scope.afterBranch = function (branches) {
               if (!utility.window().google) {//wait until google maps script loaded
                   $timeout($scope.afterBranch, 500, null, branches);
               }
               else {
                   branches.forEach(function (branch) {
                       var markerData = {
                           position: {
                               lat: branch.latitude,
                               lng: branch.longitude
                           },
                           map: $scope.map,
                           title: branch.name
                       };
                       new google.maps.Marker(markerData);
                   });
               }
           }

           $scope.branches = [];
           utility.getBranches($scope.branches, $scope.afterBranch);

           //mov the map center to the selected branch location
           $scope.setMap = function (branch) {
               if (branch) {
                   $scope.search.branch = branch.id;
               }
               if (!utility.window().google) { //wait until google maps script loaded
                   $timeout($scope.setMap, 500, null, branch);
               }
               else {
                   for (var i = 0; i < $scope.branches.length; i++) {
                       if ($scope.search.branch === $scope.branches[i].id) {
                           var latlng = {
                               lat: $scope.branches[i].latitude,
                               lng: $scope.branches[i].longitude
                           }
                           $scope.map.setCenter(latlng);
                           break;
                       }
                   }
               }
           }
           $scope.search = { start: new Date(), end: new Date() }
           //check the start and end date are valid
           $scope.dateValid = function () {
               return utility.IsDateValid($scope.search.start, $scope.search.end);
           }
           $scope.submit = function () {
               if ($scope.dateValid()) {
                   utility.search($scope.search);
                   utility.window().location.href = "/home/cars";
               }
               else {
                   alert("Invalid Date");
               }
           }

       });