  
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
function ShowValidation()
{
    $(".has-error").show();
    $(".text-danger").show();
}
function OnSubmitClick()
{
    ShowValidation();
    OnChangeEvent();
}
function resetValidation()
{
    
    $(".has-error").empty();
    $(".text-danger").empty();
}