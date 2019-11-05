//Load Data in Table when documents is ready  
var MachineMtnContentListID = 0; //=0 Add new, != 0 Edit.
$(document).ready(function () {
    LoadDataMachineType('#SearchMachineType');
    LoadDataMachineType('#drpMachineType');  //import excel
    LoadDataMachineType('#MachineTypeID');
    Common_LoadIsActiveToCombobox('#IsActive');
    $('#FrequencyID').html(Common_GetList_MachineMtnFrequency_HTML());
    $('#SearchFrequencyID').html(Common_GetList_MachineMtnFrequency_HTML());
    $('#ReceiveDate').datepicker();

    //paging.
    //clearSearchTextBox();
    //MachineMtnContent_Search(1);
});

$(document).ready(function () {
    $('#MachineTypeID').change(function () {
        var MachineTypeID = $('#MachineTypeID').val();
        $('#MachinePartID').empty();
        load_MachinePartByMachineType('#MachinePartID', MachineTypeID, '');
    });
});

//function loadData() {
//    $.ajax({
//        url: "/MachineMtnContentList/List",
//        type: "GET",
//        contentType: "application/json;charset=utf-8",
//        dataType: "json",
//        success: function (result) {
//            var html = '';
//            $.each(result, function (key, item) {
//                html += '<tr>';
//                html += '<td>' + item.MachineTypeName + '</td>';
//                html += '<td>' + item.MachinePartName + '</td>';
//                html += '<td>' + item.ContentMtn + '</td>';
//                html += '<td>' + item.ToolMtn + '</td>';
//                html += '<td>' + item.MethodMtn + '</td>';
//                html += '<td>' + item.Standard + '</td>';
//                html += '<td>' + Common_Get_MachineMtnFrequencyName_ById(item.FrequencyID) + '</td>';
//                html += '<td><a href="#" onclick="return GetbyID(\'' + item.ID + '\')">Edit</a> | <a href="#" onclick="return DeleteByID(\'' + item.ID + '\')">Delete</a></td>';
//                html += '</tr>';
//            });
//            $('.tbody').html(html);
//        },
//        error: function (errormessage) {
//            alert('Lỗi/Error loadData' + errormessage.responseText);
//        }
//    });
//}
function load_MachinePartByMachineType(selectObject, machineTypeID, currentMachinePartID) {
    $.ajax({
        url: "/MachinePart/GetbyMachineType",
        data: { 'MachineTypeID': machineTypeID },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result.lstMachinePart, function (key, item) {
                html += '<option value="' + item.ID + '">' + item.Name + '</option>';
            })
            $(selectObject).html(html);
            if (currentMachinePartID != '')
                $(selectObject).val(currentMachinePartID);
        },
        error: function (errormessage) {
            alert('Lỗi/Error function load_MachinePartByMachineType(selectObject, machineTypeID). :' + errormessage.responseText);
        }
    });
}

//function loadDataBySelectedPage(pageNumber) {
//    $.ajax({     
//        url: "/MachineMtnContentList/Selectbypage",
//        data: { 'pageNumber': pageNumber },
//        type: "GET",
//        contentType: "application/json;charset=utf-8",
//        dataType: "json",
//        success: function (result) {
//            var html = '';
//            loginUserID = result.UserID;
//            loginUserName = result.UserName;
//            $('#UserID').val(loginUserName);
//            $.each(result.lstMachineMtnContentList, function (key, item) {
//                html += '<tr>';
//                html += '<td>' + item.MachineTypeName + '</td>';
//                html += '<td>' + item.MachinePartName + '</td>';
//                html += '<td>' + item.ContentMtn + '</td>';
//                html += '<td>' + item.ToolMtn + '</td>';
//                html += '<td>' + item.MethodMtn + '</td>';
//                html += '<td>' + item.Standard + '</td>';
//                html += '<td>' + Common_Get_MachineMtnFrequencyName_ById(item.FrequencyID) + '</td>';
//                html += '<td><a href="#" onclick="return GetbyID(\'' + item.ID + '\')">Edit</a> | <a href="#" onclick="return DeleteByID(\'' + item.ID + '\')">Delete</a></td>';
//                html += '</tr>';
//            });
//            $('.tbody').html(html);
//            //Paging 
//            if (pageNumber == 1) {
//                reloadPageSearch(result.TotalPage);
//                
//            }
//        },
//        error: function (errormessage) {
//            alert('Error ' + errormessage.responseText);
//        }
//    });
//}
id = "btnClearSearchItem"
function clearSearchTextBox() {
    $('#btnClearSearchItem').val("");
    $('#SearchMachinePart').val("");
    $('#SearchContentMaintenance').val("");
    $('#SearchToolMaintenance').val("");
    $('#SearchMethodMaintenance').val("");
    $('#SearchFrequencyID').val("");
    $('#SearchMachineType').val("");
}
function clearTextBox() {
    $('#MachinePartID').empty();
    //LoadDataMachineType('#MachineTypeID');
    $('#MachinePartID').val("");
    $('#ContentMtn').val("");
    $('#ToolMtn').val("");
    $('#MethodMtn').val("");
    $('#Standard').val("");
    $('#FrequencyID').val("");
    $('#Min').val("");
    $('#Max').val("");
    $('#btnUpdate').hide();
    $('#btnAdd').show();
    $('#myModalLabel').text("Thêm nội dung bảo trì/Add new machine maintenance content list");

    //clear for insert
    $('#MachineTypeID').css('border-color', 'lightgrey');
    $('#MachineTypeID').prop('disabled', false);
    $('#MachinePartID').css('border-color', 'lightgrey');
    $('#MachinePartID').prop('disabled', false);
    $('#ContentMtn').css('border-color', 'lightgrey');
    $('#ContentMtn').prop('disabled', false);
    $('#ToolMtn').css('border-color', 'lightgrey');
    $('#MethodMtn').css('border-color', 'lightgrey');
    $('#Standard').css('border-color', 'lightgrey');
    $('#FrequencyID').css('border-color', 'lightgrey');
    $('#IsActive').css('border-color', 'lightgrey');
    //$('#IsActive').val(1); //isActive
     $('#IsActive').val('true'); //isActive
    document.querySelector('#IsActive').value = true; //cannot use $('#IsActive').val(IsActive) with true, false;
    //$('#IsActive').prop('disabled', true);

    MachineMtnContentListID = 0; // add new
}
function Add() {
    clearTextBox();
}
function GetbyID(ID) {
    $('#MachineTypeID').css('border-color', 'lightgrey');
    $('#MachineTypeID').prop('disabled', true);
    $('#MachinePartID').css('border-color', 'lightgrey');
    $('#MachinePartID').prop('disabled', true);
    $('#ContentMtn').css('border-color', 'lightgrey');
    $('#ContentMtn').prop('disabled', true);
    $('#ToolMtn').css('border-color', 'lightgrey');
    $('#MethodMtn').css('border-color', 'lightgrey');
    $('#Standard').css('border-color', 'lightgrey');
    $('#FrequencyID').css('border-color', 'lightgrey');
    //$('#IsActive').prop('disabled', false); //allow to choose.
    $.ajax({
        url: "/MachineMtnContentList/GetbyID/",
        data: { 'ID': ID },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            
            if (result.Code == "00") {
                MachineMtnContentListID = result.lstMachineMtnContentList[0].ID;
                $('#MachineTypeID').val(result.lstMachineMtnContentList[0].MachineTypeID);
                $('#MachinePartID').val(result.lstMachineMtnContentList[0].MachinePartID);
                load_MachinePartByMachineType('#MachinePartID', result.lstMachineMtnContentList[0].MachineTypeID, result.lstMachineMtnContentList[0].MachinePartID);
                $('#ContentMtn').val(result.lstMachineMtnContentList[0].ContentMtn);
                $('#ToolMtn').val(result.lstMachineMtnContentList[0].ToolMtn);
                $('#MethodMtn').val(result.lstMachineMtnContentList[0].MethodMtn);
                $('#Standard').val(result.lstMachineMtnContentList[0].Standard);
                $('#Min').val(result.lstMachineMtnContentList[0].Min);
                $('#Max').val(result.lstMachineMtnContentList[0].Max);
                $('#FrequencyID').val(result.lstMachineMtnContentList[0].FrequencyID);
                var _IsActive = result.lstMachineMtnContentList[0].IsActive;
                $('#IsActive').attr("value", true);   //cannot use $('#IsActive').val(IsActive) with true, false;
                document.querySelector('#IsActive').value = _IsActive;   //cannot use $('#IsActive').val(IsActive) with true, false;
                $('#myModal').modal('show');
                $('#btnUpdate').show();
                $('#btnAdd').hide();
                $('#myModalLabel').text('Cập nhật danh mục nội dung bảo trì/Update Machine Maintenance Content List ');
            }
            else if (result.Code == "99")
            {
                alert('Lỗi lấy thông tin/Error while getting value   ' + result.Message);
            }
        },
        error: function (errormessage) {
            alert('Lỗi/Error    ' + errormessage.responseText);
        }
    });
    return false;
}
function validate() {
    var isValid = true;
    if ($('#MachineTypeID').val().trim() == "") {
        $('#MachineTypeID').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#MachineTypeID').css('border-color', 'lightgrey');
    }
    if ($('#MachinePartID').val() == null || ($('#MachinePartID').val().trim() == "")) {
        $('#MachinePartID').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#MachinePartID').css('border-color', 'lightgrey');
    }
    if ($('#ContentMtn').val().trim() == "") {
        $('#ContentMtn').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#ContentMtn').css('border-color', 'lightgrey');
    }
    if ($('#FrequencyID').val() == null || ($('#FrequencyID').val().trim() == "")) {
        $('#FrequencyID').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#FrequencyID').css('border-color', 'lightgrey');
    }
    return isValid;
}

// **** Region CRUD ***************************************************** 
function InsertMachineMtnContentList() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var MachineMtnContentListObj = {
        ID: 0,
        MachineTypeID: $('#MachineTypeID').val(),
        MachinePartID: $('#MachinePartID').val(),
        ContentMtn: $('#ContentMtn').val(),
        ToolMtn: $('#ToolMtn').val(),
        MethodMtn: $('#MethodMtn').val(),
        Standard: $('#Standard').val(),
        FrequencyID: $('#FrequencyID').val(),

        Min: $('#Min').val(),
        Max: $('#Max').val(),
        IsActive: $('#IsActive').val() // add new

    };
    
    $.ajax({
        url: "/MachineMtnContentList/CountbyCondition/",
        data: JSON.stringify(MachineMtnContentListObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (resultCheck) {
            if ((resultCheck.Code == "00" || resultCheck.Code == "01") && resultCheck.Total == 0) { //not exists.
                $.ajax({
                    url: "/MachineMtnContentList/Insert/",
                    data: JSON.stringify(MachineMtnContentListObj),
                    type: "POST",
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    success: function (result) {
                        if (result.Code == "00") {
                            MachineMtnContent_Search(1);
                            $('#myModal').modal('hide');
                            alert("Thêm mới thành công/Insert successful!");
                        }
                        else if (result.Code == "99")
                            alert("Lỗi thêm mới/Insert error: " + result.Message);
                    },
                    error: function (errormessage) {
                        alert("Lỗi/Error function InsertMachineMtnContentList() : " + errormessage.responseText);
                    }
                });
            }
            else if (resultCheck.Code == "99")
                alert("Lỗi phần kiểm tra dữ liệu MachineMtnContentList-CountbyCondition / Error : " + resultCheck.Message);
            else
                alert("Nội dung bảo trì đã tồn tại, không thể thêm mới! /Content Maintenance exists. Cannot insert.");
        }
    });
}

function UpdateMachineMtnContentList() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var MachineMtnContentListObj = {
        ID: MachineMtnContentListID,
        MachineTypeID: $('#MachineTypeID').val(),
        MachinePart: $('#MachinePartID').val(),
        ContentMtn: $('#ContentMtn').val(),
        ToolMtn: $('#ToolMtn').val(),
        MethodMtn: $('#MethodMtn').val(),
        Standard: $('#Standard').val(),
        FrequencyID: $('#FrequencyID').val(),
        Min: $('#Min').val(),
        Max: $('#Max').val(),
        IsActive: $('#IsActive').val() //allow to choose Active or InActive
    };
    $.ajax({
        url: "/MachineMtnContentList/Insert/",
        data: JSON.stringify(MachineMtnContentListObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            MachineMtnContent_Search(1);
            $('#myModal').modal('hide');
        },
        error: function (errormessage) {
            alert('Lỗi/Error ' + errormessage.responseText);
        }
    });
}

function DeleteByID(ID) {
    var ans = confirm("Bạn có chắc chắn muốn xóa/Are you sure you want to delete ? ");
    if (ans) {
        $.ajax({
            url: "/MachineMtnContentList/CountbyMachineMtnContentID/",
            data: { 'machineMtnContentID': ID },
            type: "GET",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (resultCheck) {
                if ((resultCheck.Code == "00" || resultCheck.Code == "01") && resultCheck.Total == 0) { //not exists.
                    $.ajax({
                        url: "/MachineMtnContentList/Delete/" + ID,
                        type: "POST",
                        contentType: "application/json;charset=UTF-8",
                        dataType: "json",
                        success: function (result) {
                            MachineMtnContent_Search(1);
                            alert("Xóa thành công./Deleted successful!");
                        },
                        error: function (errormessage) {
                            alert(errormessage.responseText);
                        }
                    });
                }
                else if (resultCheck.Code == "99")
                    alert("Lỗi phần kiểm tra dữ liệu MachineMtnContentList/CountbyMachineMtnContentID / Error : " + resultCheck.Message);
                else
                    alert("Nội dung bảo trì đã tồn tại trong bảng kiểm tra bảo trì máy, không thể xóa! /Content Maintenance exists in Machine maintenance checksheet. Cannot delete.");
            },
            error: function (errormessage) {
                alert("Lỗi/Error: " + errormessage.responseText);
            }
        });
    }
}

// **** Region Import Excel ***************************************************** 
//function LoadDrpMachineType() {
//    $.ajax({
//        url: "/MachineType/List/",
//        type: "GET",
//        contentType: "application/json;charset=utf-8",
//        dataType: "json",
//        success: function (result) {
//            var options;
//            $.each(result, function (key, item) {
//                options += '<option value="' + item.ID + '">' + item.TypeName + '</option>';
//            });
//            $('#drpMachineType').html(options);
//        },
//    });
//}

// typeMaintanence and "id choose file" must be the same. 
function ImportMachineMtnContentListByType(typeMaintanence) {
    var ErrorMessage = "";
    if (typeMaintanence == "Daily") {
        if (document.getElementById("ChooseFile" + typeMaintanence).files.length != 0) {
            var MachineMtnContentListObj = {
                MachineTypeID: $('#drpMachineType').val(),
                FrequencyID: 1
            };
        }
        else
            ErrorMessage = 'Hãy chọn file / Choose file to upload!';
    }
    if (typeMaintanence == "Weekly") {
        if (document.getElementById("ChooseFile" + typeMaintanence).files.length != 0) {
            var MachineMtnContentListObj = {
                MachineTypeID: $('#drpMachineType').val(),
                FrequencyID: 2
            };
        }
        else
            ErrorMessage = 'Hãy chọn file / Choose file to upload!';
    }
    if (typeMaintanence == "Monthly") {
        if (document.getElementById("ChooseFile" + typeMaintanence).files.length != 0) {
            var MachineMtnContentListObj = {
                MachineTypeID: $('#drpMachineType').val(),
                FrequencyID: 3
            };
        }
        else
            ErrorMessage = 'Hãy chọn file / Choose file to upload!';
    }
    if (typeMaintanence == "3Months") {
        if (document.getElementById("ChooseFile" + typeMaintanence).files.length != 0) {
            var MachineMtnContentListObj = {
                MachineTypeID: $('#drpMachineType').val(),
                FrequencyID: 4
            };
        }
        else
            ErrorMessage = 'Hãy chọn file / Choose file to upload!';
    }

    if (typeMaintanence == "6Months") {
        if (document.getElementById("ChooseFile" + typeMaintanence).files.length != 0) {
            var MachineMtnContentListObj = {
                MachineTypeID: $('#drpMachineType').val(),
                FrequencyID: 5
            };
        }
        else
            ErrorMessage = 'Hãy chọn file / Choose file to upload!';
    }
    if (typeMaintanence == "Yearly") {
        if (document.getElementById("ChooseFile" + typeMaintanence).files.length != 0) {
            var MachineMtnContentListObj = {
                MachineTypeID: $('#drpMachineType').val(),
                FrequencyID: 6
            };
        }
        else
            ErrorMessage = 'Hãy chọn file / Choose file to upload!';
    }
    if (ErrorMessage == "") { //not error
        ExecuteImportExcel_MachineMtnContentListByType(MachineMtnContentListObj, typeMaintanence);
        MachineMtnContent_Search(1);
        alert('Đã cập nhật file/Imported file!');
    }
    else
        alert('Lỗi/Error: ' + ErrorMessage);
}

//typeMaintanence is the "id choose file".
function ExecuteImportExcel_MachineMtnContentListByType(MachineMtnContentListObj, typeMaintanence) {
    var formData = new FormData();
    var fileImport = document.getElementById("ChooseFile" + typeMaintanence).files[0];

    formData.append("fileImport", fileImport);
    formData.append("MachineTypeMtnContentListObj", JSON.stringify(MachineMtnContentListObj));
    $.ajax({
        url: "/MachineMtnContentList/ImportFileMachineTypeMtnContent/",
        data: formData,
        type: "POST",
        contentType: false,
        processData: false,
        dataType: "json",
        success: function (result) {
            if (result.Code == "00") {
                alert("Cập nhật thành công!/Import successful!");
                $('#myModal').modal('hide');
            }
        },
        error: function (errormessage) {
            alert('Lỗi/Error: ' + errormessage.responseText);
        }
    });
}

function LoadDataMachineType(selectObject) {
    $.ajax({
        url: "/MachineType/List/",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var options;
            $.each(result, function (key, item) {
                options += '<option value="' + item.ID + '">' + item.TypeName + '</option>';
            });
            console.log(options);
            $(selectObject).html(options);
        },
        error: function (errormessage) {
            alert('Lỗi/Error: ' + errormessage.responseText);
        }
    });
}

//event click Searh or Page number button.
//paging
function MachineMtnContent_Search(_pageNumber) {
    var MachineMtnContentListObj = {
        MachineTypeID: $('#SearchMachineType').val(),
        MachinePartName: $('#SearchMachinePart').val(),
        ContentMtn: $('#SearchContentMaintenance').val(),
        ToolMtn: $('#SearchToolMaintenance').val(),
        MethodMtn: $('#SearchMethodMaintenance').val(),
        FrequencyID: $('#SearchFrequencyID').val()
    };
    var searchMachineMtnContent = {
        aMachineTypeMtnContentList: MachineMtnContentListObj,
        pageNumber: _pageNumber
    }
    $.ajax({
        url: "/MachineMtnContentList/Search",
        data: JSON.stringify(searchMachineMtnContent),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            console.log(result.lstMachineMtnContentList);
            $.each(result.lstMachineMtnContentList, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.MachineTypeName + '</td>';
                html += '<td>' + item.MachinePartName + '</td>';
                html += '<td>' + item.ContentMtn + '</td>';
                html += '<td>' + item.ToolMtn + '</td>';
                html += '<td>' + item.MethodMtn + '</td>';
                html += '<td>' + item.Standard + '</td>';
                html += '<td>' + Common_Get_MachineMtnFrequencyName_ById(item.FrequencyID) + '</td>';
                html += '<td>' + item.Min + '</td>';
                html += '<td>' + item.Max + '</td>';
                html += '<td>' + Common_GetActiveNameByID(item.IsActive) + '</td>';
                html += '<td><a href="#" onclick="return GetbyID(\'' + item.ID + '\')">Edit</a> | <a href="#" onclick="return DeleteByID(\'' + item.ID + '\')">Delete</a></td>';
                html += '</tr>';
            });
            $('.tbody').html(html);
            //Paging 
            if (result.PageNumber == 1) {
                reloadPageSearch(result.TotalPage);
            }
        },
        error: function (errormessage) {
            alert('Lỗi/Error function MachineMtnContent_Search(_pageNumber)' + errormessage.responseText);
        }
    });
}
//paging
function reloadPageSearch(p_totalPage) {
    $('#page-selection').bootpag({
        total: p_totalPage,          // total pages
        page: 1,            // default page
        maxVisible: 10,     // visible pagination
        leaps: true         // next/prev leaps through maxVisible
    }).on("page", function (event, num) {
        MachineMtnContent_Search(num);
    });
}
