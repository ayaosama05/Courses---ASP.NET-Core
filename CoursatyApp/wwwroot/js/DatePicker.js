$(function () {
    function WireUpDatePicker() {
        var currDate = new Date();

        $('.datepicker').datepicker({
            dateFormat: 'yy-mm-dd',
            minDate: currDate, //user will not be able to select value less than today
            maxDate: AddSubtractToMonths(currDate, 3)
        });
    }

    WireUpDatePicker();
})