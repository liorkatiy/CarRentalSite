/// <reference path="../angular.js" />
angular.module("utility", [])
    .service("utility", function ($http,$window,$document) {
        var utility = this; //need to save the instance or it wont find the methods

        //change date item to int of days since 1970
        this.dateToDays = function (date) {
            date.setHours(0, 0, 0, 0);
            return Math.ceil(date.getTime() / (1000 * 3600 * 24));
        }

        //calculate the cost from start date to end date
        this.CalcCost = function (start, end, price) {
            if (start == null || end == null || price < 1)
                return 0;
            var days = utility.dateToDays(end) - utility.dateToDays(start);
            if (days < 0) {
                throw 'End Date Is Greater Than Start Date';
            }
            return (days + 1) * price;
        }

        //set the date from C# given string to real date
        this.DateFromCs = function (dateString) {
            if (dateString != null) {
                var ms = dateString.substring(6, dateString.length - 2);
                return new Date(parseInt(ms));
            }
        }

        //check the start date is greater or equals today 
        //and end date greater or equals start
        this.IsDateValid = function (start, end) {
            if (start == null || end == null)
                return false;
            var start = utility.dateToDays(start);
            var end = utility.dateToDays(end);
            var today = utility.dateToDays(new Date());
            return end >= start && start >= today;
        }

        //get all the branches save the first request in session storage to save requests
        this.getBranches = function (branches, after) {
            if (sessionStorage.branches) {
                var _branches = sessionStorage.branches.split('||');
                for (var i = 0; i < _branches.length; i++) {
                    if (_branches[i]) {
                        branches.push(JSON.parse(_branches[i]));
                    }
                }
                if (after) {
                    after(branches);
                }
            }
            else {
                $http.post("/request/getBranches/")
                    .then(function (response) {
                    branches.push.apply(branches, response.data);
                    sessionStorage.branches = '';
                    for (var i = 0; i < response.data.length; i++) {
                        sessionStorage.branches += JSON.stringify(response.data[i]) + '||';
                    }
                    if (after) {
                        after(branches);
                    }
                });
            }
        }

        //save search object to session storage
        this.search = function (search) {
            if (search) {
                sessionStorage.searchcar = JSON.stringify(search);
            }
            if (!search && sessionStorage.searchcar) {
                return JSON.parse(sessionStorage.searchcar);
            }
        }

        //post, document, window are here just because 
        //i use the utility on all the other js
        //to take less parameter in controllers

        //send post request
        this.post = function (url ,item,then) {
            $http.post(url, item).then(then);
        }

        //get the window
        this.window = function () {
            return $window;
        }

        this.document = function () {
            return $document[0];
        }
    });