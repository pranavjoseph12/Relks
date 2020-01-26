var url = "ProjectService.asmx";
var pageNumber = 1;
$(document).ready(function () {
    //Search();
});

function Refresh() {
    $.ajax({
        type: "POST",
        url: url + "/GetAllProjects",
        data: JSON.stringify({ pageNumber: pageNumber, searchTerm: $('#txtEnqSeacrh').val(), numberOfRecords: $('#ddlNumberOFRecords').val(), showHiddenProjects: true }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccessCall,
        error: OnErrorCall
    });
}

function OnSuccessCall(response) {
    var jsonResponse = JSON.parse(response.d);
    var className = '';
    var trObj = '';
    for (var i = 0; i < jsonResponse.length; i++) {
        var totalCount = jsonResponse[i].TotalCount;
        if (i % 2 == 0) {
            className = 'odd';
        }
        else {
            className = 'even'
        }

        trObj += '<tr class="' + className + '">';
        trObj += '<td class=" ">' + jsonResponse[i].ProjectName + '</td>';
        trObj += '<td class=" ">' + jsonResponse[i].PrimaryContactName + '</td>';
        trObj += ' <td class=" ">' + jsonResponse[i].PrimaryNumber + '</td>';
        trObj += '<td class=" ">' + jsonResponse[i].StartDate + '</td>';
        trObj += '<td class=" ">' + jsonResponse[i].EndDate + '</td>';
        if (jsonResponse[i].IsCompleted === true) {
            trObj += '<td class=" ">Completed</td>';
        }
        else {
            trObj += '<td class=" ">Ongoing</td>';
        }
        trObj += '<td class=" " style="text-align: center">';
        trObj += '<div title="View Details" onclick="window.location.href=\'ViewProject.aspx?ProjectID=' + jsonResponse[i].ProjectId + '\';" class="btn btn-success small_sp" style="margin-right: 5px;"><i class="fa fa-search"></i></div>';
        if (isProjectsEditAccess == 'True' && jsonResponse[i].IsCompleted === false) {
            trObj += '<div onclick="window.location.href=\'EditProject.aspx?ProjectID=' + jsonResponse[i].ProjectId + '\';" class="btn btn-warning small_sp" style="margin-right: 5px;"><i class="fa fa-pencil"></i></div>';
        }
        if (isTaskUpdateEditAccess == 'True') {
            trObj += '<div title="Add Task" onclick="window.location.href=\'TaskUpdate.aspx?ProjectID=' + jsonResponse[i].ProjectId + '\';" class="btn btn-info small_sp" style="margin-right: 5px;"><i style="color: grey;" class="fa fa-plus-square"></i></div>';
        }

        trObj += ' </td></tr>';
        trObj += '';

    }
    $('#tdInnerRow').html(trObj);
    Pagination(totalCount);
}

function OnErrorCall(response) {
    alert(response);
    alert(response.status + " " + response.statusText);
}

function PrevClick() {
    pageNumber = pageNumber - 1;
    Refresh();
    return false;
}

function Search() {
    pageNumber = 1;
    Refresh();
    return false;
}

function NextClick() {
    pageNumber = pageNumber + 1;
    Refresh();
    return false;
}


