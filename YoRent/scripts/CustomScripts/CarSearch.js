/// <reference path="Utility.js" />
/// <reference path="Favorites.js" />
angular.module("carSearch", ["scrollCall", "utility","favorites"])
        .controller("carSearch", function ($scope, utility, scrollCall, favorites) {
            $scope.branches = [];
            utility.getBranches($scope.branches);
            $scope.cars = [];
            $scope.noResult; //bool to know when to stop asking for more cars when scroll
            $scope.result = { skip: 0 };
            $scope.getFavorites = favorites.get; //function to get list of all the favorite cars

            //start search
            $scope.search = function () {
                if ($scope.isDateValid()) {
                    $scope.resetSearch();
                    $scope.getCars({ finish: false });
                }
                else {
                    alert("Invalid Date");
                }
            }

            //reset the search 
            $scope.resetSearch = function () {
                $scope.result.skip = 0;
                $scope.cars = [];
                $scope.noResult = false;
            }

            //get cars from search and scrolling
            $scope.getCars = function (finish) {
                if (!$scope.noResult) {
                    utility.post("/request/getcars", $scope.result, function (response) {
                        $scope.noResult = response.data.length == 0;
                        response.data.forEach(function (car) {
                            $scope.cars.push(car);
                            car.totalcost = $scope.CalcCost(car.dailycost);
                        });
                        $scope.result.skip++;
                        finish.finish = true;
                    });
                }
                else finish.finish = true;
            }

            //calculate the total cost
            $scope.CalcCost = function (price) {
                return utility.CalcCost($scope.result.start, $scope.result.end, price);
            }

            //create new date from string
            $scope.newDate = function (dateString) {
                var date = new Date(dateString);
                date.setHours(0, 0, 0, 0);
                return date;
            }

            //check the date is valid
            $scope.isDateValid = function () {
                return utility.IsDateValid($scope.result.start, $scope.result.end);
            }

            //move the user to the car buying page
            $scope.rentCar = function (car) {
                //store all the car data in the session storage 
                sessionStorage.setItem("carToRent", JSON.stringify(car));
                var url = "/account/buycar?start=" + $scope.result.start.toDateString() +
                    "&end=" + $scope.result.end.toDateString() +
                    "&regPlate=" + car.registrationPlate;
                utility.window().location.href = url;
            }

            //add a cars to favorite list
            $scope.addFavorite = function (car) {
                if (!favorites.add(car, car.registrationPlate)) {
                    alert("Already In Favorites");
                }
            }

            //remove car from favorites list
            $scope.removeFavorite = function (car) {
                !favorites.remove(car.registrationPlate)
            }

            //check if the favorites contains car
            $scope.InFavorites = function (car) {
                return favorites.contains(car.registrationPlate);
            }

            //initialize function
            $scope.initialize = function () {
                //set the initialize to null so it wont get called again
                $scope.initialize = null;
                //get search object 
                if (utility.search()) {
                    $scope.result = utility.search();
                    $scope.result.start = new Date($scope.result.start);
                    $scope.result.end = new Date($scope.result.end);
                }
                $scope.resetSearch();
                //get the first cars
                $scope.getCars({ finish: false });
                //initialize the favorites list
                favorites.set();
                // favorites.clear();
                //start the scroll event every time we get to 70% on scroll
                scrollCall.setScroll(0.7, $scope.getCars);
            }
            $scope.initialize();
        })
    .directive("car", function () { //directive to display the cars
            return {
                link: function (scope, elem, attrs) {
                    scope.showFav = attrs.showFav; //attribute to decide if we display remove from favorites or add            
                },
                templateUrl: '/template/car'
            }
    });
