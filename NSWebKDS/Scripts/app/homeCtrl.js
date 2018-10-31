var app = angular.module("app");
app.controller("homeCtrl", ["$cookies", "$scope", "$location", "$rootScope", "$http", function ($cookies, $scope, $location, $rootScope, $http) {

    //console.log(ControllerName);
    //Init value step1
    $scope.IsStep1 = true;
    $scope.IsShowStep1 = false;
    $scope.IsShowStep2 = false;
    $scope.cookiesActive = $cookies.getObject("cookiesActive");
   // $cookies.remove("cookiesActive");
    if ($scope.cookiesActive === undefined && ControllerName === "Home") {
        $('#activateModalStep1').modal({
            backdrop: 'static',
            keyboard: false
        });
        
    } else if (ControllerName === "Home") {
        $http.get(_HostApiGetSettings).then(function successCallback() {
            window.location.href = _RedirectToActionMana;
        })
    }
    

    //step 1 to step 2
    $scope.fnNextStep = function () {
        $('.loader').show();
        var d = {
            Email: $scope.Email,
            Password: $scope.Password,
            URL: $scope.URL,
            DeviceType: 3,
        };
        $http.post(_HostApiActive, d)
            .then(
            function successCallback(response) {
               // console.log(response);
                if (response != null && response != undefined) {
                    var data = response.data;
                    if (data.Success) {
                        var ListStore = data.ListStore;
                        if (ListStore != null && ListStore != undefined) {
                            $scope.ListStore = ListStore;
                        }
                        $scope.AppKey = data.AppKey;
                        $scope.AppSecret = data.AppSecret;
                        $scope.IsStep2 = true;
                        $scope.IsStep1 = false;
                        //console.log(ListStore);
                    }
                    else {
                        $scope.MessageStep1 = data.Message;
                        $scope.IsShowStep1 = true;
                    }

                }
            },
            function errorCallback(response) {
                alert('error')
            }).finally(
            function () {
                $('.loader').hide();
            }
            );
    }

    $scope.fnActivate = function () {
        $('.loader').show();
        $scope.IsShowStore = false;
        var _storeId = $("[id=myLocation]").val();
        if (_storeId.length == 0)
        {
            $scope.IsShowStore = true;
            $('.loader').hide();
        }
        else {
            var d = {
                AppKey: $scope.AppKey,
                AppSecret: $scope.AppSecret,
                ApproveCode: $scope.ApproveCode,
                StoreId: _storeId,//$scope.Id,
                Email: $scope.Email,
                Password: $scope.Password,
                DeviceType: 3,
                URL: $scope.URL
            };
            //console.log(d);
            $http.post(_HostApiActive, d)
                .then(function successCallback(response) {
                    if (response != null && response !== undefined) {
                        var data = response.data;
                        if (data != null && data !== undefined) {
                            var Success = data.Success;
                            if (Success) {
                                d.Email = $scope.Email;
                                d.Password = $scope.Password;

                                // expire
                              //  var expireDate = new Date();
                               // expireDate.setTime(2144232732000);
                               // $cookies.putObject("cookiesActive", d, { 'expires': expireDate });
                                $cookies.remove("ListTableID");
                                $cookies.remove("ListPrinter");
                                $cookies.remove("ListFloorID");
                                $cookies.remove("Type");
                                $cookies.remove("_TempType");
                                $cookies.remove("IsGruop");
                                //console.log($cookies.getObject("cookiesActive"));
                                $rootScope.StoreId = d.StoreId;
                                $http.get(_HostApiGetSettings).then(function successCallback() {
                                    //window.location.href = "/OrderManagement/Index";
                                    window.location.href = _RedirectToActionMana;
                                })
                            }
                            else {
                                $scope.MessageStep2 = data.Message;
                                $scope.IsShowStep2 = true;
                            }
                        }
                    }
                },
                function errorCallback(response) {
                    alert('fail')
                }).finally(
                function () {
                    $('.loader').hide();
                });
        }

    }
}]);