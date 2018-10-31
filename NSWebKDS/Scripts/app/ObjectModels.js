
//ListData
//Models DishItem
function DishItemModels() {
    this.ID = '';
    this.Set = '';
    this.Name = '';
    this.GName = '';
    this.ListItem = '';     //List<string>
    this.DataTime = '';         //Datetime
    this.Time = '';         //Datetime
    this.Qty = 0.0;
    this.DefaultState = 0;
    this.State = 0;
    this.Color = '';
    this.bgColor = '';
    this.PrinterName = '';
    this.IsTA = false;
    this.IsDel = false;
    this.Sequence = 0;
}

//Models OrderManagement
function OrderManagementModels() {
    this.IdOrderNo = '';
    this.OrderNo = '';
    this.DeliveryNo = '';
    this.DeliveryType = 0;
    this.LapseTime = '';
    this.DeliveryTime = '';     //Datetime
    this.OrderTime = '';
    this.LapseTime = '';
    this.TagNumber = '';
    this.TableName = '';
    this.ZoneName = '';
    this.ListItem = [];
    this.IsTA = false;//List<DishItemModels>
}
//End ListData


//ListSummary

//Models SummaryItem
function SummaryItemModels() {
    this.ProductID = '';
    this.ProductName = '';
    this.ColorCode = '';
    this.NumberOfPending = '';
    this.NumberOfReady = '';
    this.Time = '';         ////Datetime
    this.ListData = [];     //List<OrderManagementModels> 
}
//End ListSummary

//Models Zone
function ZoneModels() {
    this.ID = '';
    this.Name = '';
    this.StoreID = '';
    this.StoreName = '';
    this.Description = '';
    this.Width = 0;
    this.Height = 0;
}
//End Zone

//Models Table
function TableModels() {
    this.Index = '';
    this.ID = '';
    this.Name = '';
    this.StoreID = '';
    this.StoreName = '';
    this.StoreIDInPoins = '';
    this.ZoneID = '';
    this.ZoneName = '';
    this.Cover = 0;
    this.NumberOfPerson = 0;
    this.ViewMode = 0;
    this.XPoint = 0;
    this.YPoint = 0;
    this.ZPoint = 0;
    this.IsActive = false;
    this.IsShowInReservation = false;
    this.IsTemp = false;
    this.OrderID = '';
    this.State = 0;
    this.IsCall = false;
    this.IsGuestCheck = false;
    this.IsWalletOrder = false;
}
//End Table

//Models Printer
function PrinterModels() {
    this.ID = '';
    this.PrinterName = '';
    this.StoreID = '';
    this.StoreName = '';
    this.IPAddress = '';
    this.Port = '';
    this.MaxChar = 0;
    this.IsActive = false;
    this.CreatedUser = '';
    this.PrinterType = 0;
}
//End Printer

//Models Type
function TypeModels() {
    this.ID = '';
    this.Name = '';
}
//End Type