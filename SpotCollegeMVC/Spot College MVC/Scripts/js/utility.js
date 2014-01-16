//<remark>
//This function is used to display details of university
//<\reamrk>
function Openpopup(universityName) {
    document.getElementById("lblusername").innerHTML = universityName;
    var University = {
        UniversityName: universityName,
    };
    // $("#MainContent_lblusername").val(username);
    //  alert(username);
    $.ajax({
        type: "POST",
        beforeSend: function () { $("#LoadingImage").show(); },
        complete: function () { $("#LoadingImage").hide(); },
        url: 'Home/GetUnivercityData',
        data: JSON.stringify(University),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            
            var data = msg;
            
            if (data == null) {
                alert("Please input atleast one test scores in order to contact universities");
            }
            else {
                
                document.getElementById("lbladdress").innerHTML = data[0] + " " + data[1] + " " + data[2];
                //if (data[3] != null)
                //    document.getElementById("lblgradustudent").innerHTML = data[3];
                //else
                //    document.getElementById("lblgradustudent").innerHTML = "Not Available";
                //if (data[4] != null)
                //    document.getElementById("lblundergradute").innerHTML = data[4];
                //else
                //    document.getElementById("lblundergradute").innerHTML = "Not Available";
                //if (data[5] != null) {
                //    document.getElementById("lblinternational").innerHTML = data[5]; MainContent_imgprofile;
                //}
                //else
                //    document.getElementById("lblinternational").innerHTML = "Not Available";

                //if (data[6] == null)
                //    document.getElementById("imgprofile").src = "Images/no_image.jpg";
                //else
                //    document.getElementById("imgprofile").src = "Images/Profile/" + data[6];

                //document.getElementById("lblDescription").innerHTML = data[7];

                //if (data[8] != null)
                //    document.getElementById("lblundergradutefee").innerHTML = data[8];
                //else
                //    document.getElementById("lblundergradutefee").innerHTML = "Not Available";

                //if (data[9] != null)
                //    document.getElementById("lblgraduatefee").innerHTML = data[9];
                //else
                //    document.getElementById("lblgraduatefee").innerHTML = "Not Available";

                //if (data[10] != null)
                //    document.getElementById("lblbookfee").innerHTML = data[10];
                //else
                //    document.getElementById("lblbookfee").innerHTML = "Not Available";

                //if (data[11] != null)
                //    document.getElementById("lblroom").innerHTML = data[11];
                //else
                //    document.getElementById("lblroom").innerHTML = "Not Available";

                //if (data[12] != "")
                //    document.getElementById("lbldegreelvloffred").innerHTML = data[12];
                //else
                //    document.getElementById("lbldegreelvloffred").innerHTML = "Not Available";

                //if (data[13] != "")
                //    document.getElementById("lblcourceoffered").innerHTML = data[13];
                //else
                //    document.getElementById("lblcourceoffered").innerHTML = "Not Available";

                $("#myModal").dialog({ modal: true, minWidth: 700, resizable: false, minHeight: 300, closeOnEscape: true, closeText: "" });
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            alert('request failed' + JSON.stringify(xhr) + " " + JSON.stringify(textStatus) + " " + JSON.stringify(errorThrown));
        }
    });
}

