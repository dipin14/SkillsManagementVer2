

function CRate(id, val) {

    document.getElementById("Rating_" + id).value = val;

    for (var i = 1; i <= val; i++) {

        var element = document.getElementById("Rate_" + i + " " + id);
        element.className = 'starGlow';
    }
    // unselect remaining
    for (var i = val + 1; i <= 5; i++) {

        var element = document.getElementById("Rate_" + i + " " + id);
        element.className = 'starFade';
    }
}
//To implement hovering effect of rating stars
function CRateOver(id, val) {

    for (var i = 1; i <= val; i++) {

        var element = document.getElementById("Rate_" + i + " " + id);
        element.className = 'starGlow';
    }
}
//To reset the colored stars once the pointer is removed from the star
function CRateOut(id, val) {
    for (var i = 1; i <= val; i++) {

        var element = document.getElementById("Rate_" + i + " " + id);
        element.className = 'starFade';
    }
}
//To make the selected ratings persit even when the pointer is removed from the stars
function CRateSelected(id,value) {

    var newRating = document.getElementById("Rating_" + id).value
    var setRating;
    if (newRating != "")
    {
        setRating = newRating;
    }
    else
    {
        setRating = value;
    }
    for (var i = 1; i <= setRating; i++) {

        var element = document.getElementById("Rate_" + i + " " + id);
        element.className = 'starGlow';
    }
}
//Function to validate and collect all the rating values.arguments passed are list of rated skills and total count of skills 
    function SubmitRating(AllRatings, TotalSkillsRated, TotalRating)
   {

    var RatingList = new Array();
    for (var i = 0; i < TotalSkillsRated; i++) {
        var ratingSCore = document.getElementById("Rating_"+AllRatings.RatedSkills[i].SkillId).value
        var SkillID = AllRatings.RatedSkills[i].SkillId
        var Notes = document.getElementById("TxtAra_"+AllRatings.RatedSkills[i].SkillId).value

       
        {
            var RatingObject = {};

            RatingObject.SkillId = SkillID;
            RatingObject.RatingScore = ratingSCore;
            RatingObject.Note = Notes;
            RatingList.push(RatingObject);
        }}
      for (var i = 0; i < TotalRating; i++)
      {
            var ratingSCore = document.getElementById("Rating_"+AllRatings.SkillRatings[i].skillId).value
            var SkillID = AllRatings.SkillRatings[i].skillId
            var Notes = document.getElementById("TxtAra_"+AllRatings.SkillRatings[i].skillId).value

            if (ratingSCore != "") 
            {
                var RatingObject = {};

                RatingObject.SkillId = SkillID;
                RatingObject.RatingScore = ratingSCore;
                RatingObject.Note = Notes;
                RatingList.push(RatingObject);
            }
        }
    
      
   
        CompleteRating(RatingList);


    }


    //Function to post an ajax call to the controller action passing the rating value list
    function CompleteRating(RatingList) {
        if (RatingList == "") {
            
                  ValidateEmployee("Enter your ratings");
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
                        ValidateEmployee("Sorry!Connection error")
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
                    ValidateEmployee('Db Error!Please check your connection');

                }

            }

        });

    }

    //Function to reload the current web page
    function AddReload() {
        AlertEmployee("Succesfully added rating");
        setTimeout(function () { location.reload(); }, 1000);

    }

    //Function to Delete a skill rating already done,argument passed Id being the skillRatingId
    function DeleteRating(SkillRatingId) {
        var element = document.getElementById("loading");

        $.ajax({
            type: "POST",
            url: '/SkillRating/DeleteRating',
            data: { SkillRatingId: SkillRatingId },
            complete: function (result) {
                if (result.responseText) {

                    DeleteReload();
                    AlertEmployee("Succesfully removed rating")
                }
                else {
                    ValidateEmployee('please check your connection');

                }

            }
        });
    }

    function DeleteReload() {
        ShowLoader();
        setTimeout(function () { location.reload(); }, 1000);

    }
    //Function To enable and Disable special skill button based on the condition that special skill already rated or not.
    function EnableSpecial(IsSpecial) {
        var specialSKill = document.getElementById("specialSkillBtn")

        if (IsSpecial == true) {
            specialSKill.style.display = "none";
        }
        else {
            specialSKill.disabled = false;

        }


    }

    function ShowLoader() {
        var element = document.getElementById("loading");
        element.className = 'loader';
        setTimeout(function () { element.className = element.className.replace("loader", ""); }, 3000);
    }



    function Reload() {
        location.reload();
    }


    function AlertEmployee(Message) {
        var x = document.getElementById("snackbar")
        x.innerHTML = Message;
        x.className = "show";
        setTimeout(function () { x.className = x.className.replace("show", ""); }, 2000);
    }
    function ValidateEmployee(Message) {
        var x = document.getElementById("snackbarValidation")
        x.innerHTML = Message;
        x.className = "show";
        setTimeout(function () { x.className = x.className.replace("show", ""); }, 2000);
    }
