angular.module("admin")
       .controller("user", function ($scope, utility, admin) {
           //create new user admin
           $scope.userHandler = admin.set('user');

           //function to call after the search-item element
           $scope.after = function (users) {
               users.forEach(function (user) {
                   user.birthdate = utility.DateFromCs(user.birthdate);
               });
           }

           //add user
           $scope.add = function (user) {
               $scope.userHandler.add(user);
           }

           //update user if entered password will change  the password
           $scope.update = function (user) {
               if (user.password) {
                   if (!confirm("Are You Sure You Want To Change " + user.username + " Password"))
                       return;
               }
               $scope.userHandler.update(user);
           }

           //delete user
           $scope.delete = function (id, index) {
               $scope.userHandler.delete(id, index);
           }
       });