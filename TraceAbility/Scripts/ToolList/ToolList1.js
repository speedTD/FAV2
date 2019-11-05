//Load Data in Table when documents is ready                        
$(document).ready(function () {
    loadDataBySelectedPage(1);
    $("#ReceiveDate").datepicker("setDate", new Date());
    $("#StartUsing").datepicker("setDate", new Date());
    $("#ExpireDate").datepicker("setDate", new Date());

    $("#ReceiveDate").datepicker({ orientation: 'bottom' });
    $("#StartUsing").datepicker({ orientation: 'bottom' });
    $("#ExpireDate").datepicker({ orientation: 'bottom' });

    load_Line("");
    load_ToolType("");
    var loginUserID = 0;
    var loginUserName = "";
});

var ToolList = {
    init: function () {
        loadDataBySelectedPage(1);
        $("#ReceiveDate").datepicker("setDate", new Date());
        $("#ReceiveDate").datepicker({ orientation: 'bottom' });

        $("#StartUsing").datepicker("setDate", new Date());
        $("#ExpireDate").datepicker("setDate", new Date());
        load_Line("");
        load_ToolType("");
        //var loginUserID = 0;
        //var loginUserName = "";

    },
    clearTextBox: function () {
    }
};

function clearTextBox() {
    $('#ToolID').val("");
    $('#UserID').val(loginUserName);
    $('#ToolType').val("");
    $('#ItemCode').val("");
    $('#Maker').val("");
    $('#Specification').val("");
    $('#ReceiveDate').datepicker("setDate", new Date());
    $('#StartUsing').datepicker("setDate", new Date());
    $('#LifeTime').val("");
    $('#ExpireDate').val("");
    $('#LineID').val("");
    $('#Status').val("");
    $('#Remark').val("");
    //$('#CreatedDate').val("");
    $('#ImageUrl').attr("src", "");
    $('#imgalign').html("");
}

function ClearSearchItem() {
    $('#SearchToolID').val("");
    $('#SearchItemCode').val("");
    $('#SearchMaker').val("");
    $('#SearchReceiveDate').val("");
    $('#SearchStartUsing').val("");
    $('#SearchExpireDate').val("");
    $('#SearchLineID').val("");
    loadDataBySelectedPage(1);
}

function GetbyID(toolListID) {
    clearTextBox()
    $('#ToolID').css('border-color', 'lightgrey');
    $('#UserID').css('border-color', 'lightgrey');
    $('#ToolType').css('border-color', 'lightgrey');
    $('#ItemCode').css('border-color', 'lightgrey');
    $('#Maker').css('border-color', 'lightgrey');
    $('#Specification').css('border-color', 'lightgrey');
    $('#ReceiveDate').css('border-color', 'lightgrey');
    $('#StartUsing').css('border-color', 'lightgrey');
    $('#LifeTime').css('border-color', 'lightgrey');
    $('#ExpireDate').css('border-color', 'lightgrey');
    $('#LineID').css('border-color', 'lightgrey');
    $('#Status').css('border-color', 'lightgrey');
    $('#Remark').css('border-color', 'lightgrey');
    $('#ImageUrl').css('border-color', 'lightgrey');
    //$('#CreatedDate').css('border-color', 'lightgrey');
    $.ajax({        
        url: "/ToolList/GetbyID/",
        data: { 'toolListID': toolListID },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.Code == "00") { // ToolID exists.
                $.each(result.lstToolList, function (key, item) {
                    $('#ToolID').val(item.ToolID);
                    //$('#ToolID').prop('disabled', true);
                    //$('#UserID').val(item.UserID);
                    $('#ToolType').val(item.ToolType);
                    $('#ItemCode').val(item.ItemCode);
                    $('#Maker').val(item.Maker);
                    $('#Specification').val(item.Specification);
                    $('#ReceiveDate').val(SM_ConvertMsJsonDate_ToString(item.ReceiveDate));
                    $('#StartUsing').val(SM_ConvertMsJsonDate_ToString(item.StartUsing));
                    $('#LifeTime').val(item.LifeTime);
                    $('#ExpireDate').val(SM_ConvertMsJsonDate_ToString(item.ExpireDate));
                    $('#LineID').val(item.LineID);
                    $('#Status').val(item.Status);
                    $('#Remark').val(item.Remark);
                   
                    
                    if (item.ImageUrl != "" && item.ImageUrl != null && item.ImageUrl.length > 0) {
                        var links = JSON.parse(item.ImageUrl);
                        $('#ImageUrl').attr('src', links[0].replace('~', ".."));
                        var extention = links[0].split(".")[1];
                        if (extention == "jpg" || extention == "png" || extention == "jpeg") {
                            var img = links[0].replace('~', "..");
                            $("#imgalign").html('<img height="200" width="300" src="' + img + '" />');
                        } else {
                            if (links.length == 1) {
                                var img = links[0].replace('~', "..");
                                var name = links[0].split('/');
                                console.log(name);
                                $("#imgalign").html('<a href="' + img + '" download>' + name[name.length - 1] + '<a/>');
                            } else {
                                for (i = 0; i < links; i++) {
                                    var link = links[i].replace('~', "..");
                                    var name = links[i].split('/');
                                    $("#imgalign")
                                        .append('<a href="' + link + '" download>' + name[name.length - 1] + '<a/>');
                                }
                            }
                        }
                    } else {
                        $('#ImageUrl').text('');
                        $('#blah').attr('src', '');
                        $("#imgalign").html('');
                    }
                    //$('#CreatedDate').val(item.CreatedDate);
                    $('#isActive').val(item.isActive);

                    $('#myModal').modal('show');
                    //$('#btnUpdate').hide();
                    //$('#btnAdd').hide();
                    $('#myModalLabel').text('Update Tool ');
                });
            }
        },
        error: function (errormessage) {
            alert('Error    ' + errormessage.responseText);
        }
    });
    return false;
}

function validate() {
    var isValid = true;
    if ($('#ToolID').val().trim() == "") {
        $('#ToolID').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#ToolID').css('border-color', 'lightgrey');
    }
    var _tooltype = $('#ToolType').val();
    if (_tooltype == "" || _tooltype == null) {
        $('#ToolType').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#ToolID').css('border-color', 'lightgrey');
    }
    if ($('#UserID').val().trim() == "") {
        $('#UserID').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#UserID').css('border-color', 'lightgrey');
    }
    if ($('#ToolType').val().trim() == "") {
        $('#ToolType').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#ToolType').css('border-color', 'lightgrey');
    }
    if ($('#ItemCode').val().trim() == "") {
        $('#ItemCode').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#ItemCode').css('border-color', 'lightgrey');
    }
    if ($('#Maker').val().trim() == "") {
        $('#Maker').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Maker').css('border-color', 'lightgrey');
    }

    if ($('#Specification').val().trim() == "") {
        $('#Specification').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Specification').css('border-color', 'lightgrey');
    }
    if ($('#ReceiveDate').val().trim() == "") {
        $('#ReceiveDate').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#ReceiveDate').css('border-color', 'lightgrey');
    }
    if ($('#StartUsing').val().trim() == "") {
        $('#StartUsing').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#StartUsing').css('border-color', 'lightgrey');
    }
    if ($('#LifeTime').val().trim() == "") {
        $('#LifeTime').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#LifeTime').css('border-color', 'lightgrey');
    }
    if ($('#ExpireDate').val().trim() == "") {
        $('#ExpireDate').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#ExpireDate').css('border-color', 'lightgrey');
    }
    var _LineID = $('#LineID').val();
    if (_LineID == "" || _LineID==null) {
        $('#LineID').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#LineID').css('border-color', 'lightgrey');
    }
    if ($('#Status').val().trim() == "") {
        $('#Status').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Status').css('border-color', 'lightgrey');
    }
    if ($('#Remark').val().trim() == "") {
        $('#Remark').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Remark').css('border-color', 'lightgrey');
    }
    //if ($('#ImageUrl').val().trim() == "") {
    //    $('#ImageUrl').css('border-color', 'Red');
    //    isValid = false;
    //}
    //else {
    //    $('#ImageUrl').css('border-color', 'lightgrey');
    //}
    return isValid;
}

// **** Region CRUD ***************************************************** 
function SaveTool() {
    ToolList_Insert();
}

function ToolList_Insert() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var ToolObj = {
        ToolID: $('#ToolID').val(),
        UserID: loginUserID,
        ToolType: $('#ToolType').val(),
        ItemCode: $('#ItemCode').val(),
        Maker: $('#Maker').val(),
        Specification: $('#Specification').val(),
        ReceiveDate: $('#ReceiveDate').val(),
        StartUsing: $('#StartUsing').val(),
        LifeTime: $('#LifeTime').val(),
        ExpireDate: $('#ExpireDate').val(),
        LineID: $('#LineID').val(),
        Status: $('#Status').val(),
        Remark: $('#Remark').val(),
        ImageUrl: $('#ImageUrl').val()
    };
    var file = document.getElementById("ImageUrl").files;
    var data = new FormData();
    if (file.length > 0) {
        for (var i = 0; i < file.length; i++) {
            data.append("fileInput", file[i]);
        }
    }
    data.append("ToolObj", JSON.stringify(ToolObj));
    $.ajax({
        url: "/ToolList/Insert/",
        data: data,
        type: "POST",
        contentType: false,
        processData: false,
        dataType: "json",
        success: function (result) {
            if ($('#chkKeepInfoAndAutoSave').is(':checked')) {
                loadDataBySelectedPage(1);
            }
            else {
                loadDataBySelectedPage(1);
                $('#myModal').modal('hide');
            }
            Swal.fire('Saved!');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function ToolList_Update() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var ToolObj = {
        ToolID: $('#ToolID').val(),
        UserID: loginUserID,
        ToolType: $('#ToolType').val(),
        ItemCode: $('#ItemCode').val(),
        Maker: $('#Maker').val(),
        Specification: $('#Specification').val(),
        ReceiveDate: $('#ReceiveDate').val(),
        StartUsing: $('#StartUsing').val(),
        LifeTime: $('#LifeTime').val(),
        ExpireDate: $('#ExpireDate').val(),
        LineID: $('#LineID').val(),
        Status: $('#Status').val(),
        Remark: $('#Remark').val(),
        ImageUrl: $('#ImageUrl').val()
    };
    var file = document.getElementById("ImageUrl").files;
    var data = new FormData();
    if (file.length > 0) {
        for (var i = 0; i < file.length; i++) {
            data.append("fileInput", file[i]);
        }
    }
    data.append("ToolObj", JSON.stringify(ToolObj));
    $.ajax({
        url: "/ToolList/Update/",
        data: data,
        type: "POST",
        contentType: false,
        processData: false,
        dataType: "json",
        success: function (result) {
            if ($('#chkKeepInfoAndAutoSave').is(':checked')) {
            }
            else {
                loadDataBySelectedPage(1);
                $('#myModal').modal('hide');
            }

        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function DeleleByID(ID) {
    var ans = confirm("Are you sure you want to delete ?" + ID);
    if (ans) {
        $.ajax({
            url: "/ToolList/Delete/" + ID,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                alert("Deleted successful!");
                loadDataBySelectedPage(1);   
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}

//function changeImgUrl(object){
//}

// **** End of region CRUD *****************************************************
$('#ChooseImageFile').change(function (event) {
    alert(this.files[0]);
    $('#ImageUrl').attr('scr', event.target.files[0])
});

function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#blah').attr('src', e.target.result);
        }

        reader.readAsDataURL(input.files[0]);
    }
}

$("#ImageUrl").change(function () {
    readURL(this);
});


// **** Region of Paging.


function ToolList_SearchTools(_pageNumber) {

    var ToolObj = {
        ToolID: $('#SearchToolID').val(),
        UserID: "", //$('#SearchOperatorID').val(),
        ToolType: "", //$('#SearchToolType').val(),
        ItemCode: $('#SearchItemCode').val(),
        Maker: $('#SearchMaker').val(),
        Specification: "", //$('#SearchSpecification').val(),
        ReceiveDate: $('#SearchReceiveDate').val(),
        StartUsing: $('#SearchStartUsing').val(),
        LifeTime: "", //$('#SearchLifeTime').val(),
        ExpireDate: $('#SearchExpireDate').val(),
        LineID: $('#SearchLineID').val(),
        Status: "", //$('#SearchStatus').val(),
        Remark: "", //$('#SearchRemark').val(),
        ImageUrl: ""
    };

    var returnObj = {
        aToolList: ToolObj,
        PageNumber: _pageNumber
    }

    $.ajax({
        url: "/ToolList/Search",
        data: JSON.stringify(returnObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            loginUserID = result.UserID;
            loginUserName = result.UserName;
            $('#UserID').val(loginUserName);
            $.each(result.lstToolList, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.ToolID + '</td>';
                html += '<td>' + item.UserID + '</td>';
                html += '<td>' + item.UserName + '</td>';
                html += '<td>' + item.ToolTypeName + '</td>';
                html += '<td>' + item.ItemCode + '</td>';
                html += '<td>' + item.Maker + '</td>';
                html += '<td>' + item.Specification + '</td>';
                html += '<td>' + SM_ConvertMsJsonDate_ToString(item.ReceiveDate) + '</td>';
                html += '<td>' + SM_ConvertMsJsonDate_ToString(item.StartUsing) + '</td>';
                html += '<td>' + item.LifeTime + '</td>';
                html += '<td>' + SM_ConvertMsJsonDate_ToString(item.ExpireDate) + '</td>';
                html += '<td>' + item.LineID + '</td>';
                html += '<td>' + item.Status + '</td>';
                html += '<td>' + item.Remark + '</td>';
                //html += '<td>' + SM_ConvertMsJsonDate_ToString(item.CreatedDate) + '</td>';
                html += '<td><a href="#" onclick="return GetbyID(\'' + item.ToolID + '\')">Edit</a> | <a href="#" onclick="return DeleleByID(\'' + item.ToolID + '\')">Delete</a></td>';
                html += '</tr>';
            });
            $('.tbody').html(html);
            //Paging 
            if (_pageNumber == 1) {
                reloadPageTool(result.TotalPage);
            }
        },
        error: function (errormessage) {
            alert('Error' + errormessage.responseText);
        }
    });
}

function loadDataBySelectedPage(pageNumber) {
    $.ajax({
        url: "/ToolList/ListByPage",
        data: { 'pageNumber': pageNumber },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            loginUserID = result.UserID;
            loginUserName = result.UserName;
            $('#UserID').val(loginUserName);
            $.each(result.lstToolList, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.ToolID + '</td>';              
                html += '<td>' + item.UserID + '</td>';
                html += '<td>' + item.UserName + '</td>';
                html += '<td>' + item.ToolTypeName + '</td>';
                html += '<td>' + item.ItemCode + '</td>';
                html += '<td>' + item.Maker + '</td>';
                html += '<td>' + item.Specification + '</td>';
                html += '<td>' + SM_ConvertMsJsonDate_ToString(item.ReceiveDate) + '</td>';
                html += '<td>' + SM_ConvertMsJsonDate_ToString(item.StartUsing) + '</td>';
                html += '<td>' + item.LifeTime + '</td>';
                html += '<td>' + SM_ConvertMsJsonDate_ToString(item.ExpireDate) + '</td>';
                html += '<td>' + item.LineID + '</td>';
                html += '<td>' + item.Status + '</td>';
                html += '<td>' + item.Remark + '</td>';
                //html += '<td>' + SM_ConvertMsJsonDate_ToString(item.CreatedDate) + '</td>';
                html += '<td><a href="#" onclick="return GetbyID(\'' + item.ToolID + '\')">Edit</a> | <a href="#" onclick="return DeleleByID(\'' + item.ToolID + '\')">Delete</a></td>';
                html += '</tr>';
            });
            $('.tbody').html(html);
            //Paging 
            if (pageNumber == 1) {
                reloadPageTool(result.TotalPage);
            }
        },
        error: function (errormessage) {
            alert('Error' + errormessage.responseText);
        }
    });
}
//paging.
function reloadPageTool(p_totalPage) {
    $('#page-selection').bootpag({
        total: p_totalPage,          // total pages
        page: 1,            // default page
        maxVisible: 10,     // visible pagination
        leaps: true         // next/prev leaps through maxVisible
    }).on("page", function (event, num) {
        loadDataBySelectedPage(num);
    });
}

function scanToolID(e)
{
    if (e.keyCode == 13)
    {
        if ($('#chkKeepInfoAndAutoSave').is(':checked')) {
            ToolList_Insert();
        }
        else {
            GetbyID($('#ToolID').val());
        }
    }
}

function load_Line(selectObject) {
    $.ajax({
        url: "/Line/ListAll",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<option value="' + item.LineID + '">' + item.LineID + '</option>';
            });
            $('#LineID').html(html);
        },
        error: function (errormessage) {
            alert('Error function load_LineUsing(selectObject) :' + errormessage.responseText);
        }
    });
}

function load_ToolType(selectObject) {
    //<form id="test">
    //    $('#test').serialize


    //event.preventDefault();
    $.ajax({
        url: "/ToolTypeList/List",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<option value="' + item.ToolTypeID + '">' + item.ToolTypeName + '</option>';
            });
            $('#ToolType').html(html);
        },
        error: function (errormessage) {
            alert('Error function load_LineUsing(selectObject) :' + errormessage.responseText);
        }
    });
}