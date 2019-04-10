
$(document).ready(function () {

    if(Cookies.get('firstDate') != null && Cookies.get('secondDate') != null) 
    {
        $('#startDate').val(Cookies.get('firstDate'));
        $('#endDate').val(Cookies.get('secondDate'));
    }
    else
    {
        var today = new Date();
        var dd = today.getDate();
        var mm = today.getMonth()+1; //January is 0!
        var yyyy = today.getFullYear();

        if(dd<10) {
            dd = '0'+dd
        } 

        if(mm<10) {
            mm = '0'+mm
        } 

        today = yyyy + '-' + mm + '-' + dd;

        $('#startDate').val('2019-04-05');
        $('#endDate').val(today);
    }
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