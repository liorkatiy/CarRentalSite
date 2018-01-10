/// <reference path="../angular.js" />
angular.module("favorites", []) //save favorites
    .service("favorites", function () {
        var favorites = [];// favorites objects array

        //fill the favorites array with items from the local storage
        this.set = function () {
            if (localStorage.favoriteItems) {
                var items = localStorage.favoriteItems.split('||');
                for (var i = 0; i < items.length; i++) {
                    if (items[i]) {
                        var currentItem = JSON.parse(items[i]);
                        favorites.push(currentItem);
                    }
                }
            }
        }

        //get the favorites array
        this.get = function () {
            return favorites;
        }

        //add item to the favorites array and to local storage
        //if already in local storage return false and doesnt add
        this.add = function (item, compareString) {
            if (localStorage.favoriteItems) {
                if (!localStorage.favoriteItems.includes(compareString)) {
                    localStorage.favoriteItems += JSON.stringify(item) + '||';
                    favorites.push(item);
                }
                else {
                    return false;
                }
            }
            else {
                localStorage.favoriteItems = JSON.stringify(item) + '||';
                favorites.push(item);
            }
            return true;
        }

        //remove item from favorites array and local storage
        //return false if couldnt find the item
        this.remove = function (compareString) {
            if (localStorage.favoriteItems) {
                var items = localStorage.favoriteItems.split('||');
                for (var i = 0; i < items.length; i++) {
                    if (items[i].includes(compareString)) {
                        items.splice(i, 1);
                        favorites.splice(i, 1);
                        localStorage.favoriteItems = items.join('||');
                        return true;
                    }
                }
            }
            return false;
        }

        //check if item is in the favorites
        this.contains = function (compareString) {
            if (localStorage.favoriteItems) {
                return localStorage.favoriteItems.includes(compareString);
            }
            return false;
        }

        //clear the storage and the favorites array
        this.clear = function () {
            localStorage.favoriteItems = "";
            favorites = [];
        }
    });