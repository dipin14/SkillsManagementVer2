

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
//To implement hovering effect of rating stars
function CRateOver(id, val) {

    for (var i = 1; i <= val; i++) {

        var element = document.getElementById("Rate" + i + " " + id);
        element.className = 'starGlow';
    }
}
//To reset the colored stars once the pointer is removed from the star
function CRateOut(id, val) {
    for (var i = 1; i <= val; i++) {

        var element = document.getElementById("Rate" + i + " " + id);
        element.className = 'starFade';
    }
}
//To make the selected ratings persit even when the pointer is removed from the stars
function CRateSelected(id) {

    var setRating = document.getElementById("Rating " + id).value

    for (var i = 1; i <= setRating; i++) {

        var element = document.getElementById("Rate" + i + " " + id);
        element.className = 'starGlow';
    }
}
//Function to validate and collect all the rating values.arguments passed are list of rated skills and total count of skills 
function SubmitRating(RatedSkills, TotalSkills) {

    var RatingList = new Array();
    for (var i = 0; i < TotalSkills; i++) {
        var ratingSCore = document.getElementById("Rating " + RatedSkills[i].skillId).value
        var SkillID = RatedSkills[i].skillId
        var Notes = document.getElementById("TxtAra " + RatedSkills[i].skillId).value

        if (ratingSCore != "") {
            var RatingObject = {};

            RatingObject.SkillId = SkillID;
            RatingObject.RatingScore = ratingSCore;
            RatingObject.Note = Notes;
            RatingList.push(RatingObject);
        }
    }
    var specialSkill = document.getElementById("TxtAra").value
     var specialScore = document.getElementById("Rating 0").value
     var SkillID = 1;
     if (specialSkill != "" && specialScore != "") 
     {
       
            var RatingObject = {};
            RatingObject.IsSpecialSkill = true;
            RatingObject.Note = specialSkill;
            RatingObject.SkillId = SkillID;
            RatingObject.RatingScore = specialScore;
            RatingList.push(RatingObject);
     }
    CompleteRating(RatingList);


}


//Function to post an ajax call to the controller action passing the rating value list
function CompleteRating(RatingList) {
    if (RatingList == "") {
        
        ShowValidation();
    }
    else {
        $.ajax({
            type: "POST",
            url: '/SkillRating/RateSkills',
            data: { ratingList: RatingList },
            complete: function (result) {
                if (result.responseText) {
                RateSkill();
                }
                else {
                    alert("Sorry!Connection error")
                }

            }
        });
    }
}

//Function to post an ajax call to the action controller to get all the skills and skillratings from the db
function RateSkill() {

    $.ajax({
        type: "POST",
        url: '/SkillRating/EmployeeRating',
        data: {},
        complete: function (result) {
            if (result.responseText) {

                AddReload();
              

            }
            else {
                alert('Db Error!Please check your connection');

            }
         
        }

    });

}

//Function to reload the current web page
function AddReload() {
    ShowToast();
    setTimeout(function () { location.reload(); }, 1000);
 
}
function DeleteReload() {
    ShowLoader();
    setTimeout(function () { location.reload(); }, 1000);

}
//Function to Delete a skill rating already done,argument passed Id being the skillRatingId
function DeleteRating(SkillRatingId)
{
    var element = document.getElementById("loading");
   
    $.ajax({
        type: "POST",
        url: '/SkillRating/DeleteRating',
        data: { SkillRatingId: SkillRatingId },
        complete: function (result) {
            if (result.responseText) {
              
                DeleteReload();
            }
            else {
                alert('please check your connection');

            }

        }
    });
}


//Function To enable and Disable special skill button based on the condition that special skill already rated or not.
function EnableSpecial(IsSpecial) {
    var specialSKill = document.getElementById("specialSkillBtn")
    
    if (IsSpecial == true) {

        specialSKill.disabled = true;
    }
    else {
        specialSKill.disabled = false;
    }


}

function ShowLoader()
{
   var element = document.getElementById("loading");
    element.className = 'loader';
    setTimeout(function () { element.className = element.className.replace("loader", ""); }, 3000);
}

function ShowToast()
{
    var x = document.getElementById("snackbar")
    x.className = "show";
    setTimeout(function () { x.className = x.className.replace("show", ""); }, 2000);
}

function Reload()
{
    location.reload();
}

function ShowValidation()
{
    var x = document.getElementById("snackbarValidation")
    x.className = "show";
    setTimeout(function () { x.className = x.className.replace("show", ""); }, 2000);
  //  setTimeout(function () { location.reload(); }, 1000);

}
function AlertEmployee(Message)
{
    var x = document.getElementById("snackbar")
    x.innerHTML = Message;
    x.className = "show";
    setTimeout(function () { x.className = x.className.replace("show", ""); }, 2000);
}