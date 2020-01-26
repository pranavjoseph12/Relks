var url = "OtherActivityService.asmx";
var pageNumber = 1;
var assignedUser = 0;
var otherTaskTypeId = 0;

$(document).ready(function () {
    //RefreshActivities();
});

function RefreshActivities() {
    $.ajax({
        type: "POST",
        url: url + "/GetAllOtherProjectTasks",
        data: JSON.stringify({ pageNumber: pageNumber, searchTerm: $('#txtSearchOtherActivity').val(), numberOfRecords: $('#ddlNumberOFRecords').val() }),
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
    var totalCount = 0;
    for (var i = 0; i < jsonResponse.length; i++) {
        if (i % 2 == 0) {
            className = 'odd';
        }
        else {
            className = 'even'
        }

        totalCount = jsonResponse[i].TotalCount;

        trObj += '<tr class="' + className + '">';
        trObj += '<td class=" ">' + jsonResponse[i].TaskName + '</td>';
        trObj += '<td class=" ">' + jsonResponse[i].OtherTaskType + '</td>';
        trObj += ' <td class=" ">' + jsonResponse[i].ExpectedStartDate + '</td>';
        trObj += '<td class=" ">' + jsonResponse[i].ExpectedEndDate + '</td>';
        trObj += '<td class=" " style="text-align: center">';
        trObj += '<div title="View Details" class="btn btn-success small_sp" style="margin-right: 5px;"><i class="fa fa-search"></i></div>';
        if (isOtherActivityEditAccess == 'True') {
            trObj += '<button onclick="return ShowEditPopUP(\' ' + jsonResponse[i].OtherTaskId + '\',\' ' + jsonResponse[i].TaskName + '\',\' ' + jsonResponse[i].ExpectedStartDate + '\',\' ' + jsonResponse[i].ExpectedEndDate + '\',\' ' + jsonResponse[i].OtherTaskTypeId + '\',\' ' + jsonResponse[i].AssignedUserId + '\');" class="btn btn-warning small_sp" style="margin-right: 5px;"><i class="fa fa-pencil"></i></button>';
        }
        if (isOtherActivityDeleteAccess == 'True') {
            trObj += '<button title="Delete" onclick="return ShowDeletePopUP(\' ' + jsonResponse[i].OtherTaskId + '\',\' ' + jsonResponse[i].TaskName + '\');" class="btn btn-danger small_sp" style="margin-right: 5px;"><i class="fa fa-trash-o"></i></button>';
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

function ShowDeletePopUP(OtherTaskId, TaskName) {
    var bodyContent = '<div class="form-group"><label>Name:</label><label style="margin-left:20px;">' + TaskName + '</label></div>'
    var footerContent = '<a id="newCatSubmit"   class="btn btn-block btn-success pull-right" style="width: 20%;" data-toggle="tab">Delete</a><a class="btn btn-block btn-warning pull-right classEdit"  style="width: 20%; margin-top: 0px; margin-right: 2%;">Cancel</a>';
    $("#MyModal").find('.modal-content').find('.modal-header').find('h2').text('Confirm Delete Task?');
    $("#MyModal").find('.modal-content').find('.modal-body').html(bodyContent);
    $("#MyModal").find('.modal-content').find('.modal-footer').html(footerContent);
    $("#MyModal").find('.modal-content').find('.modal-footer').find('.btn-warning').unbind().bind('click', function () { $("#MyModal").hide(); })
    $("#MyModal").find('.modal-content').find('.modal-header').find('span').unbind().bind('click', function () { $("#MyModal").hide(); })
    $("#newCatSubmit").unbind().bind('click', function () { DeleteOtherTask(OtherTaskId) })
    $("#MyModal").show();
    return false;
}

function ShowEditPopUP(OtherTaskId,name, startDate,endDate, taskTypeId, userId) {
    var bodyContent = '<div class="col-md-12"><div class="box box-default"><div class="col-md-6 form_cols">';
    bodyContent += '<div class="form-group"> <label>Activity Name:</label> <input id="txtTaskNAme" class="form-control" /></div>';
    bodyContent += '<div class="form-group"> <label>Activity Type:</label> <select id="ddlActivityType"> </select></div>';
    bodyContent += '<div class="form-group"> <label>Assignee:</label> <select id="ddlUsers"> </select></div>';
    bodyContent += '</div> <div class="col-md-6 form_cols">';
    bodyContent += '<div class="form-group"> <label>Expected Start Date:</label> <input onkeypress="return false;" id="txtStartDate" class="form-control" /></div>';
    bodyContent += '<div class="form-group"> <label>Expected End Date:</label> <input onkeypress="return false;" id="txtEndDate" class="form-control" /></div>';
    bodyContent += '<div class="form-group"> <label>Comments:</label> <input id="txtComments" class="form-control" /></div>';
    bodyContent += '</div><div class="clearfix"></div>';
    //bodyContent += '<div class="col-md-6 pull-right bottom_button">';
    //bodyContent += '<a href="#tab2" onclick="return AssignOtherTaskToUser()" class="btn btn-block btn-success pull-right" style="width: 49%;" data-toggle="tab">Save</a>';
    //bodyContent += '<button class="btn btn-block btn-warning pull-right" style="width: 49%; margin-top: 0px; margin-right: 2%;">Clear</button>;'
    bodyContent += ' </div></div>';



    var footerContent = '<a id="newCatSubmit"   class="btn btn-block btn-success pull-right" style="width: 20%;" data-toggle="tab">Update</a><a class="btn btn-block btn-warning pull-right classEdit"  style="width: 20%; margin-top: 0px; margin-right: 2%;">Cancel</a>';
    $("#MyModal").find('.modal-content').find('.modal-header').find('h2').text('Edit Task Details');
    $("#MyModal").find('.modal-content').find('.modal-body').html(bodyContent);
    $("#MyModal").find('.modal-content').find('.modal-footer').html(footerContent);
    $("#MyModal").find('.modal-content').find('.modal-footer').find('.btn-warning').unbind().bind('click', function () { $("#MyModal").hide(); })
    $("#MyModal").find('.modal-content').find('.modal-header').find('span').unbind().bind('click', function () { $("#MyModal").hide(); })
    $("#newCatSubmit").unbind().bind('click', function () { UpdateOtherTask(OtherTaskId) })
    $("#MyModal").show();
    $("#txtStartDate").datetimepicker({ format: 'YYYY-MM-DD HH:mm' });
    $("#txtEndDate").datetimepicker({ format: 'YYYY-MM-DD HH:mm' });
    assignedUser = userId;
    otherTaskTypeId = taskTypeId;
    GetAllOtherTaskType();
    GetAllUsers();
    $("#txtTaskNAme").val(name);
    $("#txtStartDate").val(startDate);
    $("#txtEndDate").val(endDate);
    return false;
}

function DeleteOtherTask(OtherTaskId) {
    $.ajax({
        type: "POST",
        url: url + "/DeleteOtherProjectTask",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ "taskId": OtherTaskId }),
        success: function (result) {
            if (result.d === "true") {
                RefreshActivities();
                alert('Delete Success');
                $("#MyModal").hide();
            }
            else {
                alert("Error");
            }

        }
    });
}

function UpdateOtherTask(otherTaskId) {
    if ($('#ddlActivityType').val() == '-1' || $('#ddlUsers').val() == '-1' || $('#txtTaskNAme').val() == '') {
        alert('Type, User and Name is mandatory.');
    }
    else if ($('#txtEndDate').val() == '-1' || $('#txtStartDate').val() == '-1') {
        alert('Start and End date is mandatory.');
    }
    else {
        $.ajax({
            type: "POST",
            url: url + "/UpdateOtherProjectTask",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                "taskId": otherTaskId, "name": $('#txtTaskNAme').val(), "taskTypeId": $('#ddlActivityType').val(), "assignedUser": $('#ddlUsers').val(),
                "startDate": $('#txtStartDate').val(), "endDate": $('#txtEndDate').val(), "commments": $('#txtComments').val()
            }),
            success: function (result) {
                if (result.d === "true") {
                    RefreshActivities();
                    alert('Updated Successfully');
                    $("#MyModal").hide();
                }
                else {
                    alert("Error");
                }

            }
        });
    }
}

function GetAllOtherTaskType() {
    $.ajax({
        type: "POST",
        url: url + "/GetAllOtherTaskType",
        data: '',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: BindCategory,
        error: OnErrorCall
    });
}

function BindCategory(response) {
    var jsonResponse = JSON.parse(response.d);
    var className = '';
    var typeHtml = '<option value="-1">SELECT</option>';
    $.each(jsonResponse, function (key, type) {
        typeHtml += "<option value='" + type.OtherTaskTypeId + "'>" + type.OtherTaskTypeName + "</option>";
    });

    $('#ddlActivityType').html(typeHtml);
}

function GetAllUsers() {
    $.ajax({
        type: "POST",
        url: "UserService.asmx/GetAllUsers",
        data: '',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: BindUsers,
        error: OnErrorCall
    });
}

function BindUsers(response) {
    var jsonResponse = JSON.parse(response.d);
    var taskHtml = '';
    taskHtml += "<option value='-1'>SELECT</option>";
    $.each(jsonResponse, function (key, user) {
        taskHtml += "<option value='" + user.UserId + "'>" + user.UserName + "</option>";
    });
    $('#ddlUsers').html(taskHtml);
    //$('#ddlUsers').
}




