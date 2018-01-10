    angular.module("fileUpload", [])//module for uploading files via ajax
        .service("send", ['$http', function ($http) {
            //post request that take the url the item with the file and the response
            this.post = function (url, item, response) {
                var fd = new FormData();
                for (var key in item)
                    fd.append(key, item[key]);

                var httpConfig = {
                    transformRequest: angular.identity,
                    headers: { 'Content-Type': undefined }
                };
                $http.post(url, fd, httpConfig)
                    .then(response);
            }
        }])
        .directive("fileUpload", ['$parse', function ($parse) {
            //file-upload the model to put the file in
            //preview the model to put the image dataUrl in can be used as src in img element
            return {
                scope: { preview: "=", fileUpload: '=' },
                restrict: 'A',
                link: function (scope, element, attrs) {
                    element.bind('change', function (e) {
                        var reader = new FileReader();

                        reader.onload = function (event) {
                            scope.preview = event.target.result;
                            scope.$apply();
                        }

                        if (element[0].files[0]) {
                            reader.readAsDataURL(element[0].files[0]);
                            scope.fileUpload = element[0].files[0];
                        }
                        else {
                            scope.preview = '';
                            scope.fileUpload = null;
                            scope.$apply();
                        }
                    });
                }
            }
        }]);