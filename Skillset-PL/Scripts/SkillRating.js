
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
