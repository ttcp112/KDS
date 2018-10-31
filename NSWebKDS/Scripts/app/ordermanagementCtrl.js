var app = angular.module('app');

//Order
app.directive('emitLastRepeaterElementOrder', function () {
    return function (scope) {
        if (scope.$last) {
            setTimeout(function () {
                scope.$emit('LastRepeaterElementOrder');
            }, 1);
        }
    };
});
app.directive('emitLastRepeaterElementOrderGrid', function () {
    return function (scope) {
        if (scope.$last) {
            setTimeout(function () {
                scope.$emit('LastRepeaterElementOrderGrid');
            }, 1500);
        }
    };
});
app.controller('ordermanagementCtrl', ['$scope', '$cookies', '$rootScope', '$http', '$filter', '$location', '$interval', '$sce',
    function ($scope, $cookies, $rootScope, $http, $filter, $location, $interval, $sce) {

        var isLoading = true;

        $rootScope.loadOrderList = function () {
            var isShowLoading = false;
            if (isLoading == true) {
                isShowLoading = true;
                $('#loader').show();
            }
            // var isPlay = false;
            var _ListFloorID = $cookies.getObject("ListFloorID");
            var _ListTableID = $cookies.getObject("ListTableID");
            var _ListPrinter = $cookies.getObject("ListPrinter");
            var _TempType = $cookies.getObject("_TempType");
            var _TempIsGroup = $cookies.getObject("IsGroup");
            if (_TempIsGroup != false)
                _TempIsGroup = true;
            var _type = 0;
            if (_TempType != -1)
                _type = _TempType;

            //OrderManagementClientModels
            var _filterModel = {
                AppKey: APPKEY,
                AppSecret: APPSECRET,
                Mode: 1,
                StoreId: STOREID,

                //============
                IsWeb: true,
                IsRecall: false,
                IsSummary: false,
                ListItemState: [],
                DishStatus: 1,
                ItemFilter: _type,
                IsGroup: _TempIsGroup,

                ListFloorID: _ListFloorID,
                ListTableID: _ListTableID,
                ListPrinterID: _ListPrinter
            };

            var listOrderManagementModels = [];
            var itemChil = []; //dung
            $http({
                method: 'POST',
                url: _HostApiGetFilter,
                data: _filterModel
            }).then(function (response) {
                var _data = JSON.stringify(response.data);
                _data = JSON.parse(_data);
                var listData = JSON.stringify(_data.ListData);
                listData = JSON.parse(listData);
               // console.log(listData)
                $.each(listData, function (index, value) {
                    var IdOrderNo = value.IdOrderNo;
                    var OrderNo = value.OrderNo;
                    var DeliveryNo = value.DeliveryNo;
                    var DeliveryType = value.DeliveryType;
                    //Get Time c# to JS
                    var cSharpDeliveryTime = value.DeliveryTime;
                    var jsDate = new Date(parseInt(cSharpDeliveryTime.replace(/[^0-9 +]/g, '')));
                    //End Get Time c# to JS
                    var DeliveryTime = jsDate.toLocaleTimeString('en-US', { hour: '2-digit', minute: '2-digit', hour12: false });
                    var cSharpOrderTime = value.OrderTime;
                   
                    var jsOrderTime = new Date(parseInt(cSharpOrderTime.replace(/[^0-9 +]/g, '')));
                    var OrderTime = jsOrderTime.toLocaleTimeString('en-US', { hour: '2-digit', minute: '2-digit', hour12: false });
                    var LapseTime = jsOrderTime + '';
                    var TagNumber = value.TagNumber;
                    var TableName = value.TableName;
                    var ZoneName = value.ZoneName;
                    //===========
                   
                    var listDishItemModels = [];
                    var ListItem = value.ListItem;
                  
                    var itemTA = [];
                    var itemEA = [];
                    $.each(ListItem, function (i, val) {
                        
                        var DishItem = new DishItemModels();
                        DishItem.ID = val.ID;
                        DishItem.Set = val.Set;
                        DishItem.Name = val.Name;
                        DishItem.ListItem = val.ListItem;       //List<string>
                        DishItem.Qty = val.Qty;
                        DishItem.DefaultState = val.DefaultState;
                        DishItem.State = val.State;
                        DishItem.bgColor = val.Color;
                        DishItem.Color = (val.IsDel === true) ? '#ff6565' : $scope.getColorByBgColor(val.Color);
                        DishItem.PrinterName = val.PrinterName;
                        DishItem.IsTA = val.IsTA;
                        DishItem.IsDel = val.IsDel;
                        DishItem.Sequence = val.Sequence;
                        DishItem.GName = val.GName;
                        //Get Time c# to JS
                        var cSharpDate = val.Time;
                        var jsDateItem = new Date(parseInt(cSharpDate.replace(/[^0-9 +]/g, '')));
                        var myDate = jsDateItem.toLocaleTimeString('en-US', { hour: '2-digit', minute: '2-digit', hour12: false });
                        //.replace(/([\d]+:[\d]{2})(:[\d]{2})(.*)/, "$1$3");
                        DishItem.Time = myDate;
                        //End Get Time c# to JS
                        var cSharpDateTime = val.DataTime;
                        var jsDateDateTime = new Date(parseInt(cSharpDateTime.replace(/[^0-9 +]/g, '')));
                        var DateTime = jsDateDateTime + '';
                        DishItem.DataTime = DateTime;
                        listDishItemModels.push(DishItem);
                        if (DeliveryNo == '') {
                            if (val.IsTA == true)
                                itemTA.push(DishItem);
                            else
                                itemEA.push(DishItem);
                        } else {
                            itemEA.push(DishItem);
                        }
                           
                       
                    });
                    //dung - set list item for gird
                    if (itemTA != null && itemTA.length > 0) {
                        var d = new OrderManagementModels();
                        d.IdOrderNo = "TA_"+ IdOrderNo;
                        d.OrderNo = OrderNo;
                        d.DeliveryNo = DeliveryNo;
                        d.DeliveryType = DeliveryType;
                        d.OrderTime = OrderTime;
                        d.LapseTime = LapseTime;
                        d.DeliveryTime = DeliveryTime;
                        d.TagNumber = TagNumber;
                        d.TableName = TableName;
                        d.ZoneName = ZoneName;
                        d.ListItem = itemTA;
                        d.IsTA = true;
                        itemChil.push(d);
                    }
                    if (itemEA != null && itemEA.length > 0) {
                        var d = new OrderManagementModels();
                        d.IdOrderNo = IdOrderNo;
                        d.OrderNo = OrderNo;
                        d.DeliveryNo = DeliveryNo;
                        d.DeliveryType = DeliveryType;
                        d.OrderTime = OrderTime;
                        d.LapseTime = LapseTime;
                        d.DeliveryTime = DeliveryTime;
                        d.TagNumber = TagNumber;
                        d.TableName = TableName;
                        d.ZoneName = ZoneName;
                        d.ListItem = itemEA;
                        itemChil.push(d);
                    }
                    //end  

                    //============
                    var item = new OrderManagementModels();
                    item.IdOrderNo = IdOrderNo;
                    item.OrderNo = OrderNo;
                    item.DeliveryNo = DeliveryNo;
                    item.DeliveryType = DeliveryType;
                    item.OrderTime = OrderTime;
                    item.LapseTime = LapseTime;
                    item.DeliveryTime = DeliveryTime;
                    item.TagNumber = TagNumber;
                    item.TableName = TableName;
                    item.ZoneName = ZoneName;
                    item.ListItem = listDishItemModels;
                    listOrderManagementModels.push(item);
                    
                });

                //Get Data for Variable AngularJS // orderResponse
                $scope.orderResponse = listOrderManagementModels;
               // console.log(listOrderManagementModels);
                //set up Gird                  
                var listgird = [];
                var end = 0;
                var countItem = itemChil.length;
                if (countItem > _columnGird)
                    end = _columnGird;
                else
                    end = countItem;
                var countScreenGird = (countItem % _columnGird != 0 ? Math.floor(countItem / _columnGird) + 1 : Math.floor(countItem / _columnGird)); // count screen gird
                for (var i = 0; i < countScreenGird; i++) {
                    var gird = [];
                    for (var j = i * _columnGird; j < end; j++) {
                        gird.push(itemChil[j]);
                    }
                    listgird.push(gird);  // quanty item in 1 screen
                    end = (countItem - end) > _columnGird ? (Number(end) + Number(_columnGird)) : countItem;
                }
                $scope.listGird = listgird; 
               // console.log(listgird);
                var width = $(window).width();
                var height = $(window).height();
                var heightitem = _columnGird / 3;
                $("#parenGrid").css({ 'width': countScreenGird * (width-4)  + 'px', 'height': height - 158 + 'px' }); // setup width parent gird
                $(".gird").css({ 'width': width - 5 + 'px', 'height': height - 163 + 'px' });
                $(".item").css({ 'width': (width - 15) / 3 + 'px', 'height': (height - 172) / heightitem + (heightitem == 1 ? 2 : 0) + 'px' });
                $rootScope.gird($cookies.getObject("layoutType"));
               //end set up gird
            }, function (error) {
                console.log(error, 'can not get data.');
            }).finally(
                function () {
                    $('#divListOrder').show();
                    if (isShowLoading == true) {
                        $('#loader').hide();
                    }
                    //if (isPlay === true) {
                        //$scope.playAudio();
                    //}
                }
            );
        }

        $scope.getColorByBgColor = function (bgColor) {
            if (!bgColor) {
                return '#fff';
            }
            return (parseInt(bgColor.replace('#', ''), 16) > 0xffffff / 2) ? '#000' : '#fff';
        }

        $scope.playAudio = function () {
            var _a = document.createElement('audio');
            _a.src = SOUND;
            _a.autoplay = false;
            setTimeout(function () {
                _a.play();
            }, 150);         
        }

        $scope.$on('LastRepeaterElementOrder', function () {
            $scope.changeColor();
        });

        $scope.$on('LastRepeaterElementOrderGrid', function () {
            var $mvar = $('.gridTime');
            for (i = 0; i < $mvar.length; i++) {
                var _OrderNo = $mvar.eq(i).data("orderno");
                // set height for div item (scroll)
                var parentHeight = $('#item_' + _OrderNo).height();
                var headHeight = $('#_gird_' + _OrderNo).height() + $('.bg-head_' + _OrderNo).height();
                $("#item_grid_" + _OrderNo).css({ 'height': parentHeight - headHeight -3 + 'px' });
            }
        });
        $scope.changeColor = function () {
            var $mvar = $('.rowTimer');
            var _date = new Date();
            for (i = 0; i < $mvar.length; i++) {
                var _dateEnd = $scope.addMinutes(new Date($mvar.eq(i).data("time")), PENDINGTIME);//PENDINGTIME in app.js
                if (_dateEnd <= _date) {
                    //console.log('================= [changeColor] | OrderNo: ' + $mvar.eq(i).data("orderno") + ' | Row: ' + i);
                    //console.log('_date: ' + _date + ' | _dateEnd: ' + _dateEnd);
                    $mvar.eq(i).removeClass('rowTimer');
                    var _OrderNo = $mvar.eq(i).data("orderno");
                    $('#_' + _OrderNo).removeClass('bg-order-table');
                    $('#_' + _OrderNo).addClass('bg-order-table-active');
                    $('#_gird_' + _OrderNo).removeClass('bg-order-gird').addClass('bg-order-gird-active');
                    $('#_gird_TA_' + _OrderNo).removeClass('bg-order-gird').addClass('bg-order-gird-active');
                }
            }
        }
        $rootScope.countTime = function () { // count Time lapse item
            var $mvar = $('.gridTime');
            var _date = new Date();
            for (i = 0; i < $mvar.length; i++) {
                var _Time = new Date($mvar.eq(i).data("lapsetime"));
                var _OrderNo = $mvar.eq(i).data("orderno");
                var s = Math.floor((_date - _Time)/1000);
                var hh = Math.floor(s / 3600);
                var mm = Math.floor((s / 60) % 60);
                var ss = (s % 60);
                hh = Math.floor(hh / 10) >= 1 ? hh : '0' + hh;
                mm = Math.floor(mm / 10) >= 1 ? mm : '0' + mm;
                ss = Math.floor(ss / 10) >= 1 ? ss : '0' + ss;
                $('#_timeSlap_' + _OrderNo).html(hh+':'+mm+':'+ss);
            }
        }
        //$interval(function () {
        //    $scope.changeColor();
        //}, 10000);
        //$interval(function () {
        //    $rootScope.countTime();
        //}, 1000);

        $scope.addMinutes = function (date, minutes) {
            return new Date(date.getTime() + minutes * 60000);
        }

        $scope.renderHtml = function (html_code) {
            return $sce.trustAsHtml(html_code);
        };

        $rootScope.refreshOrder = function () {
            _socket.on('server_action', function (data) {
               
                //console.log(" ====> Action: " + data.PushData.Action + " | Data: " + data.PushData.Data);
                //$rootScope.loadOrderList();
                if (STOREID === data.PushData.StoreID) {
                    var _action = data.PushData.Action;
                    switch (_action) {                     
                        case 'Pos_ItemStateOrder':
                           // check rule here
                            //console.log(" ====> Action: " + data.PushData.Action + " | Data: " + data.PushData.Data + " | " + data.PushData.Data.conten);
                            //console.log(" ====> STOREID: " + STOREID + " | data.PushData.StoreID: " + data.PushData.StoreID);
                            var tmp = data.PushData.Data.split('|');
                            if (tmp.length == 4) {
                                var myObject = JSON.parse(tmp[3]);
                                var TableId = tmp[2];
                                var ZoneId = tmp[1];
                                //console.log(" ====> myObject: " + myObject);
                                //console.log(" ====> TableId: " + tmp[2]);
                                //console.log(" ====> ZoneId: " + ZoneId);

                                var isCheck = checkValue(1, myObject);

                                if (isCheck == 'true' && TableId.length >0)
                                    isCheck = checkTable(TableId);
                                if (isCheck == 'true' && ZoneId.length > 0)
                                    isCheck = checkZone(ZoneId);
                                if (isCheck == 'true') {
                                    isLoading = true;
                                    $rootScope.loadOrderList();
                                    $scope.playAudio();
                                }
                                else {
                                    isLoading = false;
                                    $rootScope.loadOrderList();
                                }
                            }
                           
                            break;
                        case 'Pos_ChangeTableOrder':
                            $rootScope.loadOrderList();
                            $scope.playAudio();
                            break;
                    }
                }
            });
        }
        function checkTable(arr) {
            var status = 'false';
            var _ListTables = $cookies.getObject("ListTableID");  
            if (_ListTables != null && _ListTables.length > 0) {
                for (var i = 0; i < _ListTables.length; i++) {
                    if (arr == _ListTables[i]) {
                        status = 'true';
                        break;
                    }
                }
            } else
                status = 'true';
            return status;
        }
        function checkZone(arr) {
            var status = 'false';
            var _ListZones = $cookies.getObject("ListFloorID");
            if (_ListZones != null && _ListZones.length > 0) {
                for (var i = 0; i < _ListZones.length; i++) {
                    if (arr == _ListZones[i]) {
                        status = 'true';
                        break;
                    }
                }
            }       
            else
                status = 'true';
            return status;
        }
        function checkValue(value, arr) {
            var status = 'false';
            var _ListPrinter = $cookies.getObject("ListPrinter");
            var _Type = $cookies.getObject("Type");
            for (var i = 0; i < arr.length; i++) {
                if (_Type == null || _Type == 0 || (arr[i].IsTakeAway == true && _Type == '2') || (arr[i].IsTakeAway == false && _Type == '1')) {
                    if (_ListPrinter != null && _ListPrinter.length > 0) {
                        var _intersect = intersect(_ListPrinter, arr[i].PrinterIDs);
                        if (_intersect.length > 0 && arr[i].State == value) {
                            status = 'true';
                            break;
                        }
                    } else {
                        var name = arr[i].State;
                        if (name == value) {
                            status = 'true';
                            break;
                        }
                    }
                }              
            }
            return status;
        }
        function intersect(a, b) {
            var t;
            if (b.length > a.length) t = b, b = a, a = t; // indexOf to loop over shorter
            return a.filter(function (e) {
                return b.indexOf(e) > -1;
            });
        }
        //====Init
        $rootScope.loadOrderList();
        $rootScope.refreshOrder();
        $rootScope.loadFilter();


    }]);

//================Filter Right of OM
app.controller('filterCtrl', ["$cookies", "$scope", "$rootScope", "$http", '$filter', '$location', '$interval', '$sce',
    function ($cookies, $scope, $rootScope, $http, $filter, $location, $interval, $sce) {
        $rootScope.loadFilter = function () {
        //$('#loader').show();
        var _ListFloorID = $cookies.getObject("ListFloorID");
        var _ListTableID = $cookies.getObject("ListTableID");
        var _ListPrinter = $cookies.getObject("ListPrinter");
        var _Type = $cookies.getObject("Type");
        var _TempType = $cookies.getObject("_TempType");
        var _TempIsGroup = $cookies.getObject("IsGroup");


        var _filterModel = { StoreId: STOREID }
        $http({
            method: 'POST',
            url: _HostApiGetListFilter,
            data: _filterModel
        }).then(function (response) {
            var _data = JSON.stringify(response.data);
            _data = JSON.parse(_data);

            //Get Data for Variable AngularJS // Get List Zone/Table/Printer

            //===ZONE
            var listZoneModels = [];
            var listZone = JSON.stringify(_data.ListZone);
            listZone = JSON.parse(listZone);
            $.each(listZone, function (index, value) {
                var item = new ZoneModels();
                item.ID = value.ID;
                item.Name = value.Name;
                item.Selected = false;
                if (_ListFloorID != null && _ListFloorID.length > 0) {
                    for (index = 0, len = _ListFloorID.length; index < len; ++index) {
                        if (_ListFloorID[index] == item.ID) {
                            item.Selected = true;
                        }
                    }
                }
                listZoneModels.push(item);
            });
            $rootScope.zoneResponse = listZoneModels;


            //===TABLE
            var listTableModels = [];
            var listTable = JSON.stringify(_data.ListTable);
            listTable = JSON.parse(listTable);
            $.each(listTable, function (index, value) {
                var item = new TableModels();
                item.ID = value.ID;
                item.Name = value.Name;
                item.Selected = false;
                if (_ListTableID != null && _ListTableID.length > 0) {
                    for (index = 0; index < _ListTableID.length; index++) {
                        if (_ListTableID[index] == item.ID) {
                            item.Selected = true;
                        }
                    }
                }
                listTableModels.push(item);
            });
            $rootScope.tableResponse = listTableModels;


            //===PRINTER
            var listPrinterModels = [];
            var listPrinter = JSON.stringify(_data.ListPrinter);
            listPrinter = JSON.parse(listPrinter);
            $.each(listPrinter, function (index, value) {
                var item = new PrinterModels();
                item.ID = value.ID;
                item.Name = value.PrinterName;
                item.Selected = false;
                if (_ListPrinter != null && _ListPrinter.length > 0) {
                    for (index = 0, len = _ListPrinter.length; index < len; ++index) {
                        if (_ListPrinter[index] == item.ID) {
                            item.Selected = true;
                        }
                    }
                }
                listPrinterModels.push(item);
            });
            $rootScope.printerResponse = listPrinterModels;

            //===Type
            var listTypeModels = [];
            var listType = JSON.stringify(_data.ListType);
            listType = JSON.parse(listType);
            $.each(listType, function (index, value) {
                var item = new TypeModels();
                if (_TempType != null) {
                    if (_TempType == -1) {
                        item.ID = value.Value;
                        item.Name = value.Text;
                        item.Selected = false;
                    } else if (_TempType == 0) {
                        item.ID = value.Value;
                        item.Name = value.Text;
                        item.Selected = true;
                    } else {
                        item.ID = value.Value;
                        item.Name = value.Text;
                        if (item.ID == _TempType)
                            item.Selected = true;
                        else
                            item.Selected = false;
                    }
                } else {
                item.ID = value.Value;
                item.Name = value.Text;
                item.Selected = false;
                }
                listTypeModels.push(item);
            });

            $rootScope.typeResponse = listTypeModels;
            hightlight(_ListFloorID, _ListTableID, _ListPrinter, _Type);

            if (_TempIsGroup != false)
                $rootScope.checkGroup = true;
            else
                $rootScope.checkGroup = false;
            //============= 
        }, function (error) {
            console.log(error, 'can not get data.');
        }).finally(
            function () {
                //$('#loader').hide();
            }
        );
    }
    $scope.fnDone = function () {
        $('div[class="dd"]').hide();
        $('i[class="icon-angle-up"]').removeClass('icon-angle-up').addClass('icon-angle-down');
        filter();
        //get value List Printer, Zone, Table
        var printerModel = $rootScope.printerResponse;
        var zoneModel = $rootScope.zoneResponse;
        var tableModel = $rootScope.tableResponse;
        var type = $rootScope.typeResponse;
        var isgroup = $scope.checkGroup;


        var ListPrinter = [];
        var ListFloorID = [];
        var ListTableID = [];
        var Type = [];



        var _TempType = parseInt(-1);

        //choose option selected
        //Printer
        $.each(printerModel, function (index, value) {
            if (value.Selected) {
                ListPrinter.push(value.ID);
            }
        });

        //Zone
        $.each(zoneModel, function (index, value) {
            if (value.Selected) {
                ListFloorID.push(value.ID);
            }
        });

        //Table
        $.each(tableModel, function (index, value) {
            if (value.Selected) {
                ListTableID.push(value.ID);
            }
        });

        //Type
        var _type = parseInt(0);
        $.each(type, function (index, value) {
            if (value.Selected) {
                _type += parseInt(value.ID);
                Type.push(value.ID);
            }
        });

        //_tempType: control UI
        if (_type == 3)
            _TempType = parseInt(0);
        else if (_type != 0)
            _TempType = _type;
        $cookies.putObject("_TempType", _TempType);

        //change color title if it filter
        hightlight(ListFloorID, ListTableID, ListPrinter, Type);
        //put cookie
        $cookies.putObject("ListTableID", ListTableID);
        $cookies.putObject("ListPrinter", ListPrinter);
        $cookies.putObject("ListFloorID", ListFloorID);
        $cookies.putObject("Type", Type);
        $cookies.putObject("IsGroup", isgroup);

        //console.log('ListFloorID: ' + $cookies.getObject("ListFloorID"));
        //console.log('ListTableID: ' + $cookies.getObject("ListTableID"));
        //console.log('ListPrinter: ' + $cookies.getObject("ListPrinter"));
        //console.log('Type: ' + $cookies.getObject("Type"));

        //call loadOrderList
        //$rootScope.loadOrderList();
        location.reload();
    }

    $scope.fnClear = function () {
        angular.forEach($scope.zoneResponse, function (item) {
            item.Selected = false;
        });

        angular.forEach($scope.tableResponse, function (item) {
            item.Selected = false;
        });

        angular.forEach($scope.printerResponse, function (item) {
            item.Selected = false;
        });

        angular.forEach($scope.typeResponse, function (item) {
            item.Selected = false;
        })
        $scope.checkGroup = false;
    }
    function hightlight(ListFloorID, ListTableID, ListPrinter, Type) {
        if (ListTableID != null && ListTableID.length > 0)
            $("#table").addClass("title-red");
        else
            $("#table").removeClass("title-red");

        if (ListPrinter != null && ListPrinter.length > 0)
            $("#printer").addClass("title-red");
        else
            $("#printer").removeClass("title-red");

        if (ListFloorID != null && ListFloorID.length > 0)
            $("#zone").addClass("title-red");
        else
            $("#zone").removeClass("title-red");

        if (Type != null && Type.length > 0)
            $("#type").addClass("title-red");
        else
            $("#type").removeClass("title-red");
    }
}]);

app.controller('navCtrl', ["$cookies", "$scope", "$rootScope", function ($cookies, $scope, $rootScope) {
    $scope.removeCookie = function (name) {
        $cookies.remove(name);
    }
    $rootScope.gird = function (type) {
        $cookies.putObject("layoutType", type);
        if (type == 1) {
            $("#List").hide();
            $(".chooselist").hide();
            $('.choosegrid').show();
            $("#Gird").show();
        } else {
            $("#Gird").hide();
            $('.choosegrid').hide();
            $(".chooselist").show();
            $("#List").show();
        }
    }
}]);


