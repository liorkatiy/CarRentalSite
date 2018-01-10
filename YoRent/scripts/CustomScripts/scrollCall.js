//call function when scrolling is at given precent
//the function will get again object to be set to true when finish
//for web worker or ajax requests
angular.module("scrollCall", [])
       .service("scrollCall", ['$window', '$document', function ($window, $document) {
           this.setScroll = function (percent, func) {
               var again = { finish: true, count: 100 };
               $window.addEventListener("scroll", function () {
                   var c = Math.max($window.pageYOffset, $document[0].body.scrollTop, $document[0].documentElement.scrollTop);
                   var scrollHeight = Math.max($document[0].body.scrollHeight, $document[0].documentElement.scrollHeight);
                   var clientHeight = $window.innerHeight;
                   var max = scrollHeight - clientHeight;
                   if (c / max > percent && (again.finish || again.count >= 100)) {
                       again.finish = false;
                       again.count = 0;
                       func(again);
                   }
               }, false);
           }
       }]);