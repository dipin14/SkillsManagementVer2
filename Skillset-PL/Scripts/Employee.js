function OnChangeEvent()
{
    $(".has-error").show();
    $(".text-danger").show();
    var year = Date.now();
    var selectdate = $('#DateOfBirth').val();
    var dateformat = Date.parse(selectdate);
    var timeDiff = Math.abs(year - dateformat);
    var diffDays = Math.ceil(timeDiff / (1000 * 3600 * 24 * 365));
    if (diffDays < 18) {
        document.getElementById("birthvalidation").innerText="Enter a valid birth date";       
    }
    
}

function resetValidation()
{
    $(".has-error").hide();
    $(".text-danger").hide();
}