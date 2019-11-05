//Load Data in Table when documents is ready  
$(document).ready(function () {
    loadDataBySelectedPage(1);
    load_LineUsing("");
    $('#ReceiveDate').datepicker();
});

function clearTextBox() {
    $('#ToolCleaningContentID').val("");  //hiden id value.
    $('#ToolID').val("");
    $('#LineUsing').val("");
    $('#Shift').val("");
    $('#Result').val("");
    $('#NGContents').val("");
    $('#RepairDate').val("");
    $('#RepairContents').val("");
    $('#RepairID').val("");
    $('#CheckBy').val("");
    $('#ImageLink').attr("src", "");

    //$('#btnUpdate').hide();
    //$('#btnAdd').show();
    //$('#myModalLabel').text("Add New Tool Cleaning Content");

    $('#ToolID').css('border-color', 'lightgrey');
    $('#LineUsing').css('border-color', 'lightgrey');
    $('#Shift').css('border-color', 'lightgrey');
    $('#Result').css('border-color', 'lightgrey');
    $('#NGContents').css('border-color', 'lightgrey');
    $('#RepairDate').css('border-color', 'lightgrey');
    $('#RepairContents').css('border-color', 'lightgrey');
    $('#RepairID').css('border-color', 'lightgrey');
    $('#CheckBy').css('border-color', 'lightgrey');
    $('#ImageLink').css('border-color', 'lightgrey');
    //$('#CreatedDate').css('border-color', 'lightgrey');
}

function Get_CleaningInfo_ByToolIDRepairDate() {
    $('#ToolID').css('border-color', 'lightgrey');
    $('#LineUsing').css('border-color', 'lightgrey');
    $('#Shift').css('border-color', 'lightgrey');
    $('#Result').css('border-color', 'lightgrey');
    $('#NGContents').css('border-color', 'lightgrey');
    $('#RepairDate').css('border-color', 'lightgrey');
    $('#RepairContents').css('border-color', 'lightgrey');
    $('#RepairID').css('border-color', 'lightgrey');
    $('#CheckBy').css('border-color', 'lightgrey');
    $('#ImageLink').css('border-color', 'lightgrey');

    var ToolCleaningObj = {
        ToolID: $('#ToolID').val(),
        //LineUsing: $('#LineUsing').val(),
        Shift: $('#Shift').val(),
        //Result: $('#Result').val(),
        //NGContents: $('#NGContents').val(),
        RepairDate: $('#RepairDate').val(),
        //RepairContents: $('#RepairContents').val(),
        //RepairID: $('#RepairID').val(),
        //CheckBy: $('#CheckBy').val(),
        //ImageLink: $('#ImageLink').val()
    }
    $.ajax({
        url: "/ToolCleaningContent/GetbyIDRepairDate/",
        data: JSON.stringify(ToolCleaningObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.Code == "00") { // ToolID exists.
                $.each(result.LstToolCleaningContent, function (key, item) {
                    
                    $('#ToolCleaningContentID').val(item.ID);
                    $('#ToolID').val(item.ToolID);
                    //$('#ToolID').prop('disabled', true);
                    $('#LineUsing').val(item.LineUsing);
                    $('#Shift').val(item.Shift);
                    $('#Result').val(item.Result);
                    $('#NGContents').val(item.NGContents);
                    $('#RepairDate').val(SM_ConvertMsJsonDate_ToString(item.RepairDate));
                    $('#RepairContents').val(item.RepairContents);
                    $('#RepairID').val(item.RepairID);
                    $('#CheckBy').val(item.CheckBy);
                    $('#ImageLink').attr('src', item.ImageLink);
                    $('#myModal').modal('show');
                });
            }
            else if (result.Code == "01") {// not exist. 
                Swal.fire({
                    type: 'error',
                    text: 'Không có mã tool: ' + toolID
                });
                if ($('#chkKeepInfoAndAutoSave').is(':checked')) {
                    $('#ToolID').val("");
                }
                else {
                    clearTextBox();
                }
            }
        },
        error: function (errormessage) {
            alert('Error    ' + errormessage.responseText);
        }
    });
}
function ClearSearchItem() {
    $('#SearchToolID').val("");
    $('#SearchLine').val("");
    $('#SearchResult').val("");
    $('#SearchCheckBy').val("");
    $('#FromDate').val("");
    $('#ToDate').val("");
}
function Get_CleaningInfo_ByID(id) {
    $('#ToolID').css('border-color', 'lightgrey');
    $('#LineUsing').css('border-color', 'lightgrey');
    $('#Shift').css('border-color', 'lightgrey');
    $('#Result').css('border-color', 'lightgrey');
    $('#NGContents').css('border-color', 'lightgrey');
    $('#RepairDate').css('border-color', 'lightgrey');
    $('#RepairContents').css('border-color', 'lightgrey');
    $('#RepairID').css('border-color', 'lightgrey');
    $('#CheckBy').css('border-color', 'lightgrey');
    $('#ImageLink').css('border-color', 'lightgrey');
    $.ajax({
        url: "/ToolCleaningContent/GetbyID/",
        data: { 'ID': id },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.Code == "00") { // ToolID exists.
                $.each(result.LstToolCleaningContent, function (key, item) {
                    $('#ToolCleaningContentID').val(item.ID);
                    $('#ToolID').val(item.ToolID);
                    //$('#ToolID').prop('disabled', true);
                    $('#LineUsing').val(item.LineUsing);
                    $('#Shift').val(item.Shift);
                    $('#Result').val(item.Result);
                    $('#NGContents').val(item.NGContents);
                    $('#RepairDate').val(SM_ConvertMsJsonDate_ToString(item.RepairDate));
                    $('#RepairContents').val(item.RepairContents);
                    $('#RepairID').val(item.RepairID);
                    $('#CheckBy').val(item.CheckBy);
                    $('#ImageLink').attr('src', item.ImageLink);
                    $('#myModal').modal('show');
                    //$('#btnUpdate').show();
                    //$('#btnAdd').hide();
                });
            }
            else if (result.Code == "01") {// not exist. 
                Swal.fire({
                    type: 'error',
                    text: 'Chưa nhập thông tin: ' + id
                });
                //if ($('#chkKeepInfoAndAutoSave').is(':checked')) {
                //    $('#ToolID').val("");
                //}
                //else {
                //    clearTextBox();
                //}
            }
        },
        error: function (errormessage) {
            alert('Error    ' + errormessage.responseText);
        }
    });
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
    if ($.trim($('#LineUsing').val()) == "") {    //check null.
        $('#LineUsing').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#LineUsing').css('border-color', 'lightgrey');
    }
    if ($.trim($('#Shift').val()) == "") {
        $('#Shift').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Shift').css('border-color', 'lightgrey');
    }
    if ($('#Result').val().trim() == "") {
        $('#Result').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Result').css('border-color', 'lightgrey');
    }
    if ($('#NGContents').val().trim() == "") {
        $('#NGContents').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#NGContents').css('border-color', 'lightgrey');
    }
    if ($('#RepairDate').val().trim() == "") {
        $('#RepairDate').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#RepairDate').css('border-color', 'lightgrey');
    }
    if ($('#RepairContents').val().trim() == "") {
        $('#RepairContents').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#RepairContents').css('border-color', 'lightgrey');
    }
    if ($('#RepairID').val().trim() == "") {
        $('#RepairID').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#RepairID').css('border-color', 'lightgrey');
    }
    if ($('#CheckBy').val().trim() == "") {
        $('#CheckBy').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#CheckBy').css('border-color', 'lightgrey');
    }
    //if ($('#ImageLink').val().trim() == "") {
    //    $('#ImageLink').css('border-color', 'Red');
    //    isValid = false;
    //}
    //else {
    //    $('#ImageLink').css('border-color', 'lightgrey');
    //}
    return isValid;
}

// **** Region CRUD ***************************************************** 
function SaveToolCleaningInfo()
{
    if($('#ToolCleaningContentID').val() == "")
    {    InsertToolCleaning();
    }
    else
    {
        UpdateToolCleaning();
    }
}
function Edit_CleaningInfo_ByID(id)
{
    Get_CleaningInfo_ByID(id);
    //if ($('#ToolCleaningContentID').val() != "") {
        ToolCleaning_status("edit");
    //}
}

function AddNew_CleaningInfo() {
    clearTextBox();
    ToolCleaning_status("add");
}

function ToolCleaning_status(status)
{
    if (status == "edit") {
        //if ($('#ToolCleaningContentID').val() != "") {
            $('#ToolID').prop('disabled', true);
            $('#RepairDate').prop('disabled', true);
            $('#Shift').prop('disabled', true);
            $('#chkKeepInfoAndAutoSave').prop('checked', false);
            $('#chkKeepInfoAndAutoSave').prop('disabled', true);// hi.
            $('#myModalLabel').text('Update Cleaning Content');
        //}
    }
    else if (status == "add") {
            $('#ToolCleaningContentID').val("");
            $('#ToolID').prop('disabled', false);
            $('#RepairDate').prop('disabled', false);
            $('#Shift').prop('disabled', false);
            $('#chkKeepInfoAndAutoSave').prop('checked', false);
            $('#chkKeepInfoAndAutoSave').prop('disabled', false);//hien thi.
            $('#myModalLabel').text('Add Cleaning Content');
    }
}

function InsertToolCleaning() {
    var ErrorNotify = "";
    var res = validate();
    if (res == false) {
        return false;
    }    
    //res = isExisted_ToolID($('#ToolID').val());
    //if (res == true)
    //{
    //    ErrorNotify = "ToolID " + $('#ToolID').val() + " doesn't exist!";
    //}
    //res = isExisted_InfoCleaning();
    //if (res == true) {
    //    ErrorNotify = "Exist cleaning info. ToolID " + $('#ToolID').val() + " -Date: " + $('#RepairDate').val() + " -" + $('#Shift').val();
    //    ErrorNotify += "\n Cannot add new."
    //}
    //if (ErrorNotify != "")
    //{
    //    Swal.fire({
    //        type: 'error',
    //        text: ErrorNotify
    //    });
    //    return false;
    //}

    var ToolCleaningObj = {
        ToolID: $('#ToolID').val(),
        LineUsing: $('#LineUsing').val(),
        Shift: $('#Shift').val(),
        Result: $('#Result').val(),
        NGContents: $('#NGContents').val(),
        RepairDate: $('#RepairDate').val(),
        RepairContents: $('#RepairContents').val(),
        RepairID: $('#RepairID').val(),
        CheckBy: $('#CheckBy').val(),
        ImageLink: $('#ImageLink').val()
    };
    $.ajax({
        url: "/ToolCleaningContent/Insert/",
        data: JSON.stringify(ToolCleaningObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.Code == "00") {
                Swal.fire({
                    type: 'success',
                    text: 'Saved info: ' + $('#ToolID').val() + ' !'
                });
                if ($('#chkKeepInfoAndAutoSave').is(':checked')) {
                    $('#ToolID').val("");
                    $('#ToolCleaningContentID').val(""); //dieu kien xac dinh insert or update.
                }
                else {
                    $('#myModal').modal('hide');
                    loadDataBySelectedPage(1);
                }
            }
            else {
                Swal.fire({
                    type: 'error',
                    title: 'Cannot save info: ' + $('#ToolID').val() + ' !',
                    text: result.Message
                });
            }
            
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function UpdateToolCleaning() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var ToolCleaningObj = {
        ID: $('#ToolCleaningContentID').val(),
        ToolID: $('#ToolID').val(),
        LineUsing: $('#LineUsing').val(),
        Shift: $('#Shift').val(),
        Result: $('#Result').val(),
        NGContents: $('#NGContents').val(),
        RepairDate: $('#RepairDate').val(),
        RepairContents: $('#RepairContents').val(),
        RepairID: $('#RepairID').val(),
        CheckBy: $('#CheckBy').val(),
        ImageLink: $('#ImageLink').val()
    };
    $.ajax({
        url: "/ToolCleaningContent/Update/",
        data: JSON.stringify(ToolCleaningObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.Code == "00") {
                Swal.fire(
                      'Saved info: ' + $('#ToolID').val() + ' !'
                    );
            }
            else {
                Swal.fire({
                    type: 'error',
                    text: 'Cannot save info: ' + $('#ToolID').val() + ' !'
                });
            }
            clearTextBox();
            $('#myModal').modal('hide');
            loadDataBySelectedPage(1);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function DeleleByID(ID) {
    var ans = confirm("Are you sure you want to delete?" + ID);
    if (ans) {
        $.ajax({
            url: "/ToolCleaningContent/Delete/" + ID,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                loadDataBySelectedPage(1);
                alert("Deleted successful!");
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}

//function changeImgUrl(object) {
//}

// **** End of region CRUD *****************************************************
$('#ChooseImageFile').change(function (event) {
    alert(this.files[0]);

    $('#ImageLink').attr('scr', event.target.files[0])
});

// **** Region of Paging.

function loadDataBySelectedPage(pageNumber) {
    $.ajax({
        url: "/ToolCleaningContent/ListByPage",
        data: { 'pageNumber': pageNumber },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result.LstToolCleaningContent, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.ToolID + '</td>';
                html += '<td>' + item.LineUsing + '</td>';
                html += '<td>' + item.Shift + '</td>';

                html += '<td>' + item.Result + '</td>';
                html += '<td>' + item.NGContents + '</td>';
                html += '<td>' + SM_ConvertMsJsonDate_ToString(item.RepairDate) + '</td>';
                html += '<td>' + item.RepairContents + '</td>';
                html += '<td>' + item.RepairID + '</td>';
                html += '<td>' + item.CheckBy + '</td>';
                html += '<td>' + item.ImageLink + '</td>';
                //html += '<td>' + SM_ConvertMsJsonDate_ToString(item.CreatedDate) + '</td>';
                html += '<td><a href="#" onclick="return Edit_CleaningInfo_ByID(\'' + item.ID + '\')">Edit</a> | <a href="#" onclick="return DeleleByID(\'' + item.ID + '\')">Delete</a></td>';
                html += '</tr>';
            });
            $('.tbody').html(html);
            //Paging 
            if (pageNumber == 1) {
                reloadPageTool(result.TotalPage);
            }
        },
        error: function (errormessage) {
            alert('Error loadData:' + errormessage.responseText);
        }
    });
}
//paging
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
function scanToolID_Cleaning(e)
{
    SaveToolCleaningInfo();
    //if (e.keyCode == 13) {
    //    if ($('#chkKeepInfoAndAutoSave').is(':checked')) {
    //        ;
    //    }
    //    else {
    //        Get_CleaningInfo_ByToolIDRepairDate($('#ToolID').val());
    //    }
    //}
}
function load_LineUsing(selectObject) {
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
            $('#LineUsing').html(html);
        },
        error: function (errormessage) {
            alert('Error function load_LineUsing(selectObject) :' + errormessage.responseText);
        }
    });
}

function isExisted_ToolID(toolListID) {
    $.ajax({
        url: "/ToolList/GetbyID/",
        data: { 'toolListID': toolListID },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.Code == "00") { // ToolID exists.
                return true;
            }
            else { return false; }
        },
        error: function (errormessage) {
            alert('Error    ' + errormessage.responseText);
            return false;
        }
    });
}

function isExisted_InfoCleaning() {
    var ToolCleaningObj = {
        ToolID: $('#ToolID').val(),
        //LineUsing: $('#LineUsing').val(),
        Shift: $('#Shift').val(),
        //Result: $('#Result').val(),
        //NGContents: $('#NGContents').val(),
        RepairDate: $('#RepairDate').val(),
        //RepairContents: $('#RepairContents').val(),
        //RepairID: $('#RepairID').val(),
        //CheckBy: $('#CheckBy').val(),
        //ImageLink: $('#ImageLink').val()
    }
    $.ajax({
        url: "/ToolCleaningContent/GetbyIDRepairDate/",
        data: JSON.stringify(ToolCleaningObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.Code == "00") { //  existing information.
                return true;
            }
            else if (result.Code == "01") {// not exist. 
                return false;
            }
        },
        error: function (errormessage) {
            alert('Error    ' + errormessage.responseText);
            return false;
        }
    });
}

function ToolCleaning_SearchToolCleaning(_pageNumber) {

    var ToolCleaningObj = {
        ToolID: $('#SearchToolID').val(),
        LineUsing: $('#SearchLine').val(),
        Result: $('#SearchResult').val(),
        CheckBy: $('#SearchCheckBy').val(),
        FromDate: $('#FromDate').val(),
        ToDate: $('#ToDate').val(),
    };

    var returnToolCleaningContentObj = {
        aToolCleaningContent: ToolCleaningObj,
        PageNumber: _pageNumber
    }

    $.ajax({
        url: "/ToolCleaningContent/Search",
        data: JSON.stringify(returnToolCleaningContentObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result.LstToolCleaningContent, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.ToolID + '</td>';
                html += '<td>' + item.LineUsing + '</td>';
                html += '<td>' + item.Shift + '</td>';

                html += '<td>' + item.Result + '</td>';
                html += '<td>' + item.NGContents + '</td>';
                html += '<td>' + SM_ConvertMsJsonDate_ToString(item.RepairDate) + '</td>';
                html += '<td>' + item.RepairContents + '</td>';
                html += '<td>' + item.RepairID + '</td>';
                html += '<td>' + item.CheckBy + '</td>';
                html += '<td>' + item.ImageLink + '</td>';
                //html += '<td>' + SM_ConvertMsJsonDate_ToString(item.CreatedDate) + '</td>';
                html += '<td><a href="#" onclick="return Edit_CleaningInfo_ByID(\'' + item.ID + '\')">Edit</a> | <a href="#" onclick="return DeleleByID(\'' + item.ID + '\')">Delete</a></td>';
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
