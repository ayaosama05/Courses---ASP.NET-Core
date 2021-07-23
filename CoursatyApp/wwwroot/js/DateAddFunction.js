//return date = the date we will pass + number of months we need to add
function AddSubtractToMonths(date, numMonths) {
    var month = date.getMonth();
    var milliSeconds = new Date(date).setMonth(month + numMonths);
    return new Date(milliSeconds);
}