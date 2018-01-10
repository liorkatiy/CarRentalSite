/// <reference path="../angular.js" />
angular.module('formValidators', ['ngMessages'])// validation tools 
    .directive("isInvalid", function () {
        // add to all the form inputs isInavlid function 
        //that check the for is dirty and invalid
        return {
            require: '^form',
            link: function (scope, elm, attrs, ctrl) {
                var checker = function (ctrl) {
                    return function () {
                        return ctrl.$invalid && ctrl.$dirty;
                    }
                };           
                for (var i = 0; i < ctrl.$$controls.length; i++) {
                    ctrl.$$controls[i].isInvalid = new checker(ctrl.$$controls[i]);
                }
            }
        }
    })
    .directive("validateAjax", function ($http, $q) {
        //send ajax request that need to return true or false
        return {
            require: 'ngModel',
            link: function (scope, elm, attrs, ctrl) {
                var url = attrs.validateAjax;//get the ajax request from the ajax-request attribute
                var inverse = attrs.inverse
                elm.removeAttr("validate-ajax");
                ctrl.$asyncValidators.validateAjax = function (modelValue, ViewValue) {
                    var result = $q.defer();
                    $http.post(url, { validate: modelValue })
                        .then(function (response) {
                            if (response.data &&!inverse)
                                result.resolve();
                            else
                                result.reject();
                        });
                    return result.promise;
                }
            }
        }
    })
    .directive('validateIdCard', function () {
        //check the id card is valid
        return {
            restrict: 'A',
            require: 'ngModel',
            link: function (scope, elm, attrs, ctrl) {
                ctrl.$validators.validateIdCard = function (modelValue, viewValue) {
                    if (!viewValue || viewValue.length != 9) {
                        return false;
                    }
                    var sum = 0;
                    for (var i = 0; i < 9; i++) {
                        var n = parseInt(viewValue[i]) * (i % 2 + 1);
                        sum += n > 9 ? (n % 10) + parseInt(n / 10) : n;
                    }
                    return sum % 10 == 0;
                };
            }
        };
    })
    .directive('custom', function () {
        //get custom function that return true or false
        return {
            restrict: 'A',
            require: 'ngModel',
            scope: { custom: "&"},
            link: function (scope, elm, attrs, ctrl) {
                ctrl.$validators.custom = function (modelValue, viewValue) {
                    return scope.custom();
                }
            }
        };
    });
