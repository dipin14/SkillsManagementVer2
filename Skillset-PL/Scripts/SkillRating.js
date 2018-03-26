
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

function SubmitRating(data,m)
{
    var arr = [];
    for (var i = 0; i < m;i++)
    { var ratingSCore = document.getElementById("Rating " + data[i].skillId).value
    var SkillID= data[i].skillId
    
    if (ratingSCore  != "")
        {
            arr.push({ rateScore: ratingSCore, Skillid:  SkillID});
        }
    }
    CompleteRating(arr);
 
 
}

/*collapsing div*/

function collapse()
{ alert('collapse')


}
function CompleteRating(arr)
{
    alert("inside rating");
    $.ajax({
        type: "POST",
        url: '/SkillRating/RateSkills',
        data: { arr },
        complete: function (result) {
            if (result.responseText) {
               
                alert("success")
            }
            else {
                alert('sorry');

            }

        }
    });
}