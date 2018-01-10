/// <reference path="../angular.js" />
angular.module("admin", ['fileUpload','utility','formValidators']) // base admin module to be used by the other modules
    .service("admin", ["$http", "send", function ($http, send) {

        this.set = function (name) {
            //name parameter is the name of the objects
            //like car,cartype,order,branch,user
            var items = []; // items list
            //var show = false;
            return new function () {

                //function after the ajax request
                //afterResponse parameter is to call function after the request
                //that take the returned object as parameter
                var then = function (sucssess, afterResponse) {
                    return (function (response) {
                        if (response.data) {
                            if (sucssess != null)
                                alert(sucssess);
                            if (afterResponse != null)
                                afterResponse(response.data);
                        }
                        else
                            alert("Failed");
                    });
                }

                //send ajax request 
                //item = the item to send
                //after = function that will be called after request
                //type = the type of the request add,remove,delete,get
                //succsessMsg = what to alert after the request worked
                //include file = optional if u also send object with file in it
                var request = function (item, after, type, succsessMsg, includeFile) {
                    if (includeFile) {
                        send.post("/admin/" + type + name, item, then(succsessMsg, after));
                    }
                    else {
                        $http.post("/admin/" + type + name, item)
                            .then(then(succsessMsg, after));
                    }
                }

                //add item
                //after parameter is a function to call after the response
                this.add = function (item, after, includeFile) {
                    request(item, after, "add", "Added", includeFile);
                }

                //update item
                //after parameter is a function to call after the response
                this.update = function (item, after, includeFile) {
                    request(item, after, "update", "Updated", includeFile);
                }
                //delete item 
                //id is the item primary key
                //index is the index in the items array
                //after parameter is a function to call after the response
                this.delete = function (id, index, after) {
                    var item = { id: id };
                    var del = function () {
                        items.splice.apply(items, [index, 1]);
                        if (after != null)
                            after();
                    }
                    request(item, del, "delete", "Deleted");
                }

                //searchstr is the string to send
                //after parameter is a function to call after the response
                this.getFromSearch = function (searchstr, after) {
                    if (searchstr == null)
                        searchstr = '';
                    var search = { search: searchstr.trim() };

                    var setItems = function (data) {
                        items.splice.apply(items, [0, items.length])
                        items.push.apply(items, data)
                        if (after != null)
                            after(items);
                    }

                    request(search, setItems, "get");
                }

                //get the current instance name
                this.getName = function () { return name; }

                //get the current instance items
                this.getItems = function () { return items; }
            }
        }
    }])
    .directive("searchItem", function () {
        //create textbox and search button that 
        //update the given admin instance items
        //with the getFromSearch method
        return {
            restrict:"E",
            scope: {
                admin:"=", //the admin instance to use in this search
                after: "=", //any function to call after search
                searchInit: "=", //do search on load with empty string 
                btnClass: "@", //class for the search button
                txtClass: "@" //class for textbox
            },
            controller: function ($scope) {
                $scope.searchText = "";
                $scope.getFromSearch = function () {
                    $scope.admin.getFromSearch($scope.searchText, $scope.after);
                }
                if ($scope.searchInit) {
                    $scope.getFromSearch();
                }
            },
            template: '<input type="button" ng-click="getFromSearch()" value="Search {{name}}" class="{{btnClass}}" /><input type="text" ng-model="searchText" class="{{txtClass}}"/>'
        }
    })
    .directive("add", function () {
        //create add button
        //take name atribute to know the item template name
        //take btn-class attribute if the button should have class
        return {
            scope: true,
            controller: function ($scope) {
                $scope.showAddForm = false;
                $scope.toggleForm = function () {
                    $scope.showAddForm = !$scope.showAddForm;
                }
            },
            template: function (elem, attr) {
                return '<input type="button" ng-click="toggleForm()" value="Add '+attr.name+'" class="'+attr.btnClass+'"/> <add-template template="'+attr.name+'" ng-show="showAddForm"><add-template/>'
            }
        }
    })
    .directive("addTemplate", function () {
        //display template shouuld b used by the add directive
        return {
            templateUrl: function (elem, attr) {
                return "/template/add" + attr.template;
            }
        }
    });