
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
   
        var RatingList = new Array();
        for (var i = 0; i < m; i++) {
            var ratingSCore = document.getElementById("Rating " + data[i].skillId).value
            var SkillID = data[i].skillId
            var Notes = document.getElementById("TxtAra " + data[i].skillId).value

            if (ratingSCore != "") {
                var RatingObject = {};

                RatingObject.SkillId = SkillID;
                RatingObject.RatingScore = ratingSCore;
                RatingObject.Note = Notes;
                RatingList.push(RatingObject);
            }
        }
        var specialSkill = document.getElementById("TxtAra").value
        if (specialSkill != "") {
            var specialScore = document.getElementById("Rating 0").value
            var SkillID = 0;
            if (specialScore != "") {
                var RatingObject = {};
                RatingObject.IsSpecialSkill = true;
                RatingObject.Note = specialSkill;
                RatingObject.SkillId = SkillID;
                RatingObject.RatingScore = specialScore;
                RatingList.push(RatingObject);
            }

        }

        CompleteRating(RatingList);

    
}

/*collapsing div*/

function CompleteRating(RatingList)
{
    if (RatingList == "") {
        alert("Please enter your ratings");
    }
    else {
        $.ajax({
            type: "POST",
            url: '/SkillRating/RateSkills',
            data: { ratingList: RatingList },
            complete: function (result) {
                if (result.responseText) {
                 alert('submitted succesfully')
                    reload();
                }
                else {
                    alert('please check your connection');

                }

            }
        });
    }
}
function reload()
{

    $.ajax({
        type: "POST",
        url: '/SkillRating/EmployeeRating',
        data: { },
        complete: function (result) {
            if (result.responseText) {
             
                $('body').html(result.responseText);
            }
            else {
                alert('please check your connection');

            }

        }
    });

}
function Reset()
{
    location.reload();
}