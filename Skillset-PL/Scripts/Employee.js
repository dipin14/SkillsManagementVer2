  
function OnChangeEvent()
{  
    var year = Date.now();
    var selectdate = $('#DateOfBirth').val();
    if (selectdate != null)
    {
        var dateformat = Date.parse(selectdate);
        var timeDiff = Math.abs(year - dateformat);
        var diffDays = Math.ceil(timeDiff / (1000 * 3600 * 24 * 365));
        if (diffDays < 18) {         
            $('#DateOfBirth').val('');
            document.getElementById("birthvalidation").innerText = "Enter a valid birth date";
        }
    }
 
}
$(document).ready(function ()  {
    $("ul li:eq(1) a").parent().addClass('active').parent().parent().addClass('active');
});
function ShowValidation()
{
    $(".has-error").show();
    $(".text-danger").show();
}
function OnSubmitClick()
{
    ShowValidation();
    NumberValidation();
    OnChangeEvent();
}
function NumberValidation()
{
    var temp = $("#MobileNumber").val().trim();
    $("#MobileNumber").val(temp);
}
function OnChangeMobile() {
    NumberValidation();
}
$('#MobileNumber').keyup(function () {
    var temp = $("#MobileNumber").val().trim();
    $("#MobileNumber").val(temp);
});
function resetValidation()
{
    
    $(".has-error").empty();
    $(".text-danger").empty();
}

