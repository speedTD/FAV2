var url_String = document.URL;
var url_HTML = new URL(url_String);
var FPBCheckingID = url_HTML.searchParams.get("id");
$(document).ready(function () {
    $('#FPBCheckingDate').datepicker();
    GetFPBCheckingByID(FPBCheckingID);
    LoadFPBCheckingDetail_ByFPBCheckingID(FPBCheckingID);
    //GetByFPBCheckingID(FPBCheckingID);

});
var checkOK = "OK";
var FPBCheckingIDLast = "";
var loginUserID = 0;
var loginUserName = "";
function GetFPBCheckingByID(ID) {
    idFPBChecking = ID;
    $('#IndicationNo').css('border-color', 'lightgrey');
    $('#IndicationNo').prop('disabled', true);
    $('#FPBCheckingDate').css('border-color', 'lightgrey');
    $('#ItemCode').css('border-color', 'lightgrey');
    $('#ItemCode').prop('disabled', true);
    $('#ItemName').css('border-color', 'lightgrey');
    $('#ItemName').prop('disabled', true);
    $('#BatchNo').css('border-color', 'lightgrey');
    $('#BatchNo').prop('disabled', true);
    $('#MachineID').css('border-color', 'lightgrey');
    $('#MachineID').prop('disabled', true);
    $('#MachineName').css('border-color', 'lightgrey');
    $('#MachineName').prop('disabled', true);
    $('#UserID').css('border-color', 'lightgrey');
    $('#SeqNo').css('border-color', 'lightgrey');
    $('#BlockID').css('border-color', 'lightgrey');
    $('#Result').css('border-color', 'lightgrey');
    $.ajax({
        url: "/FPBChecking/GetbyID/",
        data: { 'ID': ID },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.Code === "99") {
                alert('Lỗi /Error while loading data: ' + result.Message);
                return;
            }
            loginUserID = result.UserID;
            loginUserName = result.UserName;
            $('#UserID').val(loginUserName);
            $('#IndicationNo').val(result.LstFPBChecking[0].IndicationNo);
            $('#FPBCheckingDate').val(SM_ConvertMsJsonDate_ToString(result.LstFPBChecking[0].FPBCheckingDate));
            $('#ItemCode').val(result.LstFPBChecking[0].ItemCode);
            $('#ItemName').val(result.LstFPBChecking[0].ItemName);
            $('#BatchNo').val(result.LstFPBChecking[0].BatchNo);
            $('#MachineID').val(result.LstFPBChecking[0].MachineID);
            $('#MachineName').val(result.LstFPBChecking[0].MachineName);
            $('#SeqNo').val(result.LstFPBChecking[0].SeqNo);
            $('#BlockID').val(result.LstFPBChecking[0].BlockID);
            $("input[name=Result][value=" + result.LstFPBChecking[0].Result + "]").prop('checked', true);

            //if (result.LstFPBChecking[0].Images != "" || result.LstFPBChecking[0].Images != null || result.LstFPBChecking[0].Images.length > 0) {
            //    var links = JSON.parse(result.LstFPBChecking[0].Images);
            //    var extention = links[0].split(".")[1];
            //    if (extention == "jpg" || extention == "png" || extention == "jpeg" || extention == "git" || extention == "bmp" || extention == "tif") {
            //        var img = links[0].replace('~', "..");
            //        $("#imgalign").html('<img class="img-responsive" src="' + img + '" />');
            //    } else {
            //        if (links.length == 1) {
            //            var img = links[0].replace('~', "..");
            //            var name = links[0].split('/');
            //            $("#imgalign").html('<a href="' + img + '" download>' + name[name.length - 1] + '<a/>');
            //        } else {
            //            for (i = 0; i < links; i++) {
            //                var link = links[i].replace('~', "..");
            //                var name = links[i].split('/');
            //                $("#imgalign").append('<a href="' + link + '" download>' + name[name.length - 1] + '<a/>');
            //            }
            //        }
            //    }
            //}
            $('#myModal').modal('show');
            $('#myModalLabel').text('Update FPBChecking ');
        },
        error: function (errormessage) {
            alert('Error    ' + errormessage.responseText);
        }
    });
    return false;
}
function LoadFPBCheckingDetail_ByFPBCheckingID(FPBCheckingID) {
    $.ajax({
        url: "/FPBChecking/LoadFPBCheckingDetail_ByFPBCheckingID/",
        data: { 'FPBCheckingID': FPBCheckingID },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            
            if (result.Code == "00")
            {
                //checkpermissionVM
                var isDisableEdit = '';
                if (result.permisionControllerVM.isAllow_View && !result.permisionControllerVM.isAllow_Edit)  //if user permision is View and not Edit.
                    isDisableEdit = 'disabled';

                var html = '';
                $.each(result.LstFPBCheckingDetail, function (key, item) {

                    var imgLink = item.Images.replace('~', "..");
                    var imgName = item.Images.split('/');
                    var extention = item.Images.split(".")[1];
                    if (extention == "jpg" || extention == "png" || extention == "jpeg" || extention == "git" || extention == "bmp" || extention == "tif") {
                        html += '<div class="ImageContainer" style="position:relative;text-align:center;">';
                        html += '<div style="position:absolute;top:8px;right:16px;font-size:18px">';
                        if (isDisableEdit != 'disabled')
                            html += '<a href="#" onclick="FPBCheckingDetail_DeletebyID(' + item.ID + ')" class="btn btn-primary"><img style="width:25px;height:19px;" src="/Images/Common/upload-tool-delete.png"/></a>';
                        html += '</div>';
                        html += '<a href="' + imgLink + '">';
                        html += '<img src="' + imgLink + '" style="width:100%"/>';
                        html += '</a>';
                        //html += '</div>';
                        html += '</div>';
                    }
                    else if (extention == "pdf")
                    {
                        html += '<div class="ImageContainer" style="position:relative;text-align:center;">';
                        html += '<div style="position:absolute;top:8px;right:16px;font-size:18px">';
                        // html += '<a href="" onclick="alert("bbbbbbbbb");" class="btn btn-primary"><img src="/Images/Common/upload-tool-delete.png" style="width:100%"/></a>';
                        html += '<a href="#" onclick="FPBCheckingDetail_DeletebyID(' + item.ID + ')" class="btn btn-primary"><img style="width:25px;height:19px;" src="/Images/Common/upload-tool-delete.png"/></a>';
                        html += '</div>';
                        html += '<a href="' + imgLink + '">' + imgName[imgName.length - 1] + '</a>';
                       
                        html += '</a>';
                        //html += '</div>';
                        html += '</div>';
                    }
                });
                
                $('#viewImages').html(html);
                
            }
            if (result.Code == "99") {
                alert('Lỗi /Error while loading data: ' + result.Message);
                return;
            }
        },
        error: function (errormessage) {
            alert('Lỗi/Error LoadFPBCheckingDetail_ByFPBCheckingID ' + errormessage.responseText);
        }
    });
}
//function GetByFPBCheckingID(ID) {
//    $.ajax({
//        url: "/FPBChecking/GetByFPBCheckingID",
//        type: "GET",
//        data: { 'FPBCheckingID': ID },
//        contentType: "application/json;charset=utf-8",
//        dataType: "json",
//        success: function (result) {
//            var html = '';            
//            $.each(result.LstFPBCheckingDetail, function (key, item) {
//                html += '<tr>';
//                html += '<td>' + item.CheckingItemName + '</td>';                
//                html += '<td style="display:none"><input type="text" disabled = "true" value= "' + item.FPBCheckingItemID + '"></input></td>';
//                if (item.FrequencyID == 1) {
//                    if (item.Result == "OK") {
//                        html += '<td><input type="checkbox"  name="OK" checked id="firstOK' + item.FPBCheckingItemID + '" AutoPostBack = "true" onchange="check_CheckedChangedFirstOK(\'' + item.FPBCheckingItemID + '\')" >OK</input>  |  <input type="checkbox" name="NG" id="firstNG' + item.FPBCheckingItemID + '" AutoPostBack="true" onchange ="check_CheckedChangedFirstNG(\'' + item.FPBCheckingItemID + '\')" >NG</input></td>';
//                    } 
//                    else
//                        html += '<td><input type="checkbox"  name="OK" id="firstOK' + item.FPBCheckingItemID + '" AutoPostBack = "true" onchange="check_CheckedChangedFirstOK(\'' + item.FPBCheckingItemID + '\')" >OK</input>  |  <input type="checkbox" name="NG" checked id="firstNG' + item.FPBCheckingItemID + '" AutoPostBack="true" onchange ="check_CheckedChangedFirstNG(\'' + item.FPBCheckingItemID + '\')" >NG</input></td>';
//                }
//                else html += '<td></td>';
//                if (item.FrequencyID == 2) {
//                    if (item.Result == "OK") {
//                        html += '<td><input type="checkbox" name="OK" checked id="middleOK' + item.FPBCheckingItemID + '" AutoPostBack = "true" onchange="check_CheckedChangedMiddleOK(\'' + item.FPBCheckingItemID + '\')" >OK</input>  |  <input type="checkbox" name="NG" id="middleNG' + item.FPBCheckingItemID + '" AutoPostBack="true" onchange ="check_CheckedChangedMiddleNG(\'' + item.FPBCheckingItemID + '\')" >NG</input></td>';
//                    }
//                    else
//                        html += '<td><input type="checkbox" name="OK" id="middleOK' + item.FPBCheckingItemID + '" AutoPostBack = "true" onchange="check_CheckedChangedMiddleOK(\'' + item.FPBCheckingItemID + '\')" >OK</input>  |  <input type="checkbox" name="NG" checked id="middleNG' + item.FPBCheckingItemID + '" AutoPostBack="true" onchange ="check_CheckedChangedMiddleNG(\'' + item.FPBCheckingItemID + '\')" >NG</input></td>';
//                }
//                else html += '<td></td>';
//                if (item.FrequencyID == 3) {
//                    if (item.Result == "OK") {
//                        html += '<td><input type="checkbox"  name="OK" checked id="endOK' + item.IDFPBCheckingItem + '" AutoPostBack = "true" onchange="check_CheckedChangedEndOK(\'' + item.IDFPBCheckingItem + '\')" >OK</input>  |  <input type="checkbox" name="NG" id="endNG' + item.IDFPBCheckingItem + '" AutoPostBack="true" onchange ="check_CheckedChangedEndNG(\'' + item.IDFPBCheckingItem + '\')" >NG</input></td>';
//                    }
//                    else
//                        html += '<td><input type="checkbox"  name="OK" id="endOK' + item.IDFPBCheckingItem + '" AutoPostBack = "true" onchange="check_CheckedChangedEndOK(\'' + item.IDFPBCheckingItem + '\')" >OK</input>  |  <input type="checkbox" name="NG" checked id="endNG' + item.IDFPBCheckingItem + '" AutoPostBack="true" onchange ="check_CheckedChangedEndNG(\'' + item.IDFPBCheckingItem + '\')" >NG</input></td>';
//                }
//                else html += '<td></td>';
//                html += '<td style="display:none"><input type="text" disabled = "true" value= "' + item.ID + '"></input></td>';
//                html += '</tr>';
//            });
//            $('.tbody').html(html);
//        },
//        error: function (errormessage) {
//            alert('Error' + errormessage.responseText);
//        }
//    });
//}
function check_CheckedChangedFirstOK(ID) {
    document.getElementById("firstNG" + ID).checked = false;
}
function check_CheckedChangedFirstNG(ID) {
    document.getElementById("firstOK" + ID).checked = false;
}
function check_CheckedChangedMiddleOK(ID) {
    document.getElementById("middleNG" + ID).checked = false;
}
function check_CheckedChangedMiddleNG(ID) {
    document.getElementById("middleOK" + ID).checked = false;
}
function check_CheckedChangedEndOK(ID) {
    document.getElementById("endNG" + ID).checked = false;
}
function check_CheckedChangedEndNG(ID) {
    document.getElementById("endOK" + ID).checked = false;
}
function clearTextBox() {
    //$('#ID').val("");
    $('#FPBCheckingDate').val("");
    $('#IndicationNo').css('border-color', 'lightgrey');
    $('#UserID').val(loginUserID);
    $('#SeqNo').css('border-color', 'lightgrey');
    $('#MachineID').css('border-color', 'lightgrey');
    $('#ItemName').css('border-color', 'lightgrey');
    $('#ItemCode').css('border-color', 'lightgrey');
    $('#BatchNo').css('border-color', 'lightgrey');
    $('#MachineName').css('border-color', 'lightgrey');
}
function validate() {
    var isValid = true;
    if ($('#FPBCheckingDate').val().trim() == "") {
        $('#FPBCheckingDate').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#FPBCheckingDate').css('border-color', 'lightgrey');
    }
    if ($('#IndicationNo').val().trim() == "") {
        $('#IndicationNo').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#IndicationNo').css('border-color', 'lightgrey');
    }
    if ($('#UserID').val().trim() == "") {
        $('#UserID').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#UserID').css('border-color', 'lightgrey');
    }
    if ($('#SeqNo').val().trim() == "") {
        $('#SeqNo').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#SeqNo').css('border-color', 'lightgrey');
    }
    if ($('#MachineID').val().trim() == "") {
        $('#MachineID').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#MachineID').css('border-color', 'lightgrey');
    }
    if ($('#MachineName').val().trim() == "") {
        $('#MachineName').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#MachineName').css('border-color', 'lightgrey');
    }
    if ($('#ItemName').val().trim() == "") {
        $('#ItemName').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#ItemName').css('border-color', 'lightgrey');
    }
    if ($('#ItemCode').val().trim() == "") {
        $('#ItemCode').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#ItemCode').css('border-color', 'lightgrey');
    }
    if ($('#BatchNo').val().trim() == "") {
        $('#BatchNo').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#BatchNo').css('border-color', 'lightgrey');
    }
    return isValid;
}

// **** Region CRUD ***************************************************** 
function InsertFPBChecking() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var file = document.getElementById("Images").files;
    var FPBCheckingObj = {
        ID: idFPBChecking,
        FPBCheckingDate: $('#FPBCheckingDate').val(),
        IndicationNo: $('#IndicationNo').val(),
        UserID: loginUserID,
        SeqNo: $('#SeqNo').val(),
        MachineID: $('#MachineID').val(),
        MachineName: $('#MachineName').val(),
        ItemName: $('#ItemName').val(),
        ItemCode: $('#ItemCode').val(),
        BatchNo: $('#BatchNo').val(),
        BlockID: $('#BlockID').val(),
        Result: $("input[name='Result']:checked").val(),
    };
    $.ajax({
        url: "/FPBChecking/Insert/",
        data: JSON.stringify(FPBCheckingObj),
        type: "POST",
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (result) {
            var data = new FormData();
            if (file.length > 0) {
                for (var i = 0; i < file.length; i++) {
                    data.append("fileInput", file[i]);
                }
            }
            var FPBCheckingDetailObj = {
                Result: $("input[name='Result']:checked").val(),
                FPBCheckingID: idFPBChecking,
            }
            data.append('FPBCheckingDetailObj', JSON.stringify(FPBCheckingDetailObj));
            InsertFPBCheckingDetail(data);
            $('#myModal').modal('hide');
            alert("Update success!");
            setTimeout(function () {
                window.location.href = "./Index";

            }, 250)
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function InsertFPBCheckingDetail(data) {
    $.ajax({
        url: "/FPBChecking/InsertFPBCheckingDetail/",
        data: data,
        type: "POST",
        contentType: false,
        processData: false,
        dataType: "json",
        success: function (result) {
            $('#myModal').modal('hide');
            clearTextBox();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });

}

function FPBCheckingDetail_DeletebyID(ID)
{
    var ans = confirm("Bạn có chắc chắn muốn xóa không? /Are you sure you want to delete ?");
    debugger;
    if (ans) {
        $.ajax({
            url: "/FPBChecking/FPBCheckingDetail_DeletebyID/" + ID,

            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                if (result.Code == "00") {
                    alert("Xóa thành công!/Deleted successful!");
                    GetFPBCheckingByID(FPBCheckingID);
                    LoadFPBCheckingDetail_ByFPBCheckingID(FPBCheckingID);
                }
                if (result.Code == "99") {
                    alert("Lỗi không xóa được/Error while deleting!");
                }
            },
            error: function (errormessage) {
                alert('Lỗi/Error' + errormessage.responseText);
            }
        });
    }
}

