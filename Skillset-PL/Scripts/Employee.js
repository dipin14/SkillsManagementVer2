function OnChangeEvent()
{
    var year = Date.now();
    var selectdate = $('#DateOfBirth').val();
    var dateformat = Date.parse(selectdate);
    var timeDiff = Math.abs(year - dateformat);
    var diffDays = Math.ceil(timeDiff / (1000 * 3600 * 24 * 365));
    if (diffDays < 18)
        alert("enter a valid birth date");
    
}