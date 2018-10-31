var app = angular.module('app', ['ngRoute', 'ngCookies']);

var SOUND = _Sound;
var PENDINGTIME = _PendingTime;
var STOREID = _StoreId;
var APPKEY = _AppKey;
var APPSECRET = _AppSecret;
var _socket = io(socketUrl);

app.config(['$provide', '$routeProvider', '$httpProvider', function ($provide, $routeProvider, $httpProvider) {

    //================================================
    // Routes
    //================================================
    $routeProvider.when('/home', {
        templateUrl: 'Home/Index',
        controller: 'homeCtrl'
    });

    $routeProvider.when('/ordermanagement', {
        templateUrl: 'OrderManagement/Index',
        controller: 'ordermanagementCtrl'
    });


}]);


