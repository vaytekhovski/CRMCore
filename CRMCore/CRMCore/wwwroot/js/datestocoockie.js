
$(document).ready(function () {
    $('#startDate').val(Cookies.get('firstDate'));
    $('#endDate').val(Cookies.get('secondDate'));
});

$("#OrderBookAsks").click(function () {
    setDataToCoockie()
});

$("#OrderBookBids").click(function () {
    setDataToCoockie()
});

$("#TradeHistory").click(function () {
    setDataToCoockie()
});

$("#TradeDelta").click(function () {
    setDataToCoockie()
});

$("#AsksOnBids").click(function () {
    setDataToCoockie()
});

$("#DeltaOnTradeHistory").click(function () {
    setDataToCoockie()
});


function setDataToCoockie() {
    Cookies.set('firstDate', $('#startDate').val());
    Cookies.set('secondDate', $('#endDate').val());
}