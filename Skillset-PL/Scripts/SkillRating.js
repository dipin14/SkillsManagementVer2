
function CRate(id, val) {

    document.getElementById("Rating " + id).value = val;

    for (var i = 1; i <= val; i++) {

        var element = document.getElementById("Rate" + i + " " + id);
        element.className = 'starGlow';
    }
    // unselect remaining
    for (var i = val + 1; i <= 5; i++) {

        var element = document.getElementById("Rate" + i + " " + id);
        element.className = 'starFade';
    }
}

function CRateOver(id, val) {

    for (var i = 1; i <= val; i++) {

        var element = document.getElementById("Rate" + i + " " + id);
        element.className = 'starGlow';
    }
}

function CRateOut(id, val) {
    for (var i = 1; i <= val; i++) {

        var element = document.getElementById("Rate" + i + " " + id);
        element.className = 'starFade';
    }
}

function CRateSelected(id) {

    var setRating = document.getElementById("Rating " + id).value

    for (var i = 1; i <= setRating; i++) {

        var element = document.getElementById("Rate" + i + " " + id);
        element.className = 'starGlow';
    }
}

function SubmitRating( data)
{
    //for(var i=0;i<)
    alert(data);
}
/*collapsing div*/
$(document).on('click', '.panel-heading span.clickable', function (e) {
    var $this = $(this);
    if (!$this.hasClass('panel-collapsed')) {
        $this.parents('.panel').find('.panel-body').slideUp();
        $this.addClass('panel-collapsed');
        $this.find('i').removeClass('glyphicon-chevron-up').addClass('glyphicon-chevron-down');
    } else {
        $this.parents('.panel').find('.panel-body').slideDown();
        $this.removeClass('panel-collapsed');
        $this.find('i').removeClass('glyphicon-chevron-down').addClass('glyphicon-chevron-up');
    }
})
function collapse()
{ alert('collapse')

$('#viewdetails').collapse('hide')
}