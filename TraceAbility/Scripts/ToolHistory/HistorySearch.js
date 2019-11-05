$(document).ready(function () {
    Search();
    MenuID = 0;
});
function Search() {
    var where = "1=1";
    var count = 0;
    var ToolID = $('#ToolID').val();
    var Item = $('#Item').val();
    var Line = $('#Line').val();
    var Maker = $('#Maker').val();
    var ExpireDate = $('#ExpireDate').val();
    if (ToolID.length > 0)
    {
        where = "tToolList.ToolID = '" + ToolID + "' ";
        count++;
    }
    if (Item.length > 0)
    {
        if (count == 0)
        {
            where = " tToolList.ItemCode = '" + Item + "' ";
            count++;
        }
        else
        {
            where += "AND tToolList.ItemCode = '" + Item + "' ";
        }
    }
    if (Line.length > 0)
    {
        if (count == 0)
        {
            where = " tToolList.LineID = '" + Line + "' ";
            count++;
        }
        else
        {
            where += "AND tToolList.LineID = '" + Line + "' ";
        }
    }
    if (Maker.length > 0)
    {
        if (count == 0)
        {
            where = " tToolList.Maker = '" + Maker + "' ";
            count++;
        }
        else
        {
            where += "AND tToolList.Maker = '" + Maker + "' ";
        }
    }
    if (ExpireDate.length > 0)
    {
        if (count == 0)
        {
            where = " tToolList.ExpireDate = '" + ExpireDate + "' ";
            count++;
        }
        else
        {
            where += "AND tToolList.ExpireDate = '" + ExpireDate + "' ";
        }
    }
    $.ajax({
        url: "/ToolList/SelectByCondition/",
        data: { 'where': where },
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.ToolID + '</td>';
                html += '<td>' + item.ItemCode + '</td>';
                html += '<td>' + item.LineID + '</td>';
                html += '<td>' + item.Maker + '</td>';
                html += '<td>' + SM_ConvertMsJsonDate_ToString(item.ExpireDate) + '</td>';
                html += '</tr>';
            });
            $('.tbody').html(html);
        },
        error: function (errormessage) {
            //alert('Error' + errormessage.responseText);
        }
    });
}

function ClearSearchItem() {
    $('#FromDate').val("");
    $('#FromDate').val("");
    $('#ToDate').val("");
    $('#ExpireDate').val("");
    $('#ToolID').val("");
    $('#Item').val("");
    $('#Line').val("");
    $('#Maker').val("");
}
