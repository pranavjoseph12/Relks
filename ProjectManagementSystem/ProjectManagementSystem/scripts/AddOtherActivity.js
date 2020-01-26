var url = "OtherActivityService.asmx";
$(document).ready(function () {
    $("#txtStartDate").datetimepicker({ format: 'YYYY-MM-DD HH:mm' });
    $("#txtEndDate").datetimepicker({ format: 'YYYY-MM-DD HH:mm' });
    GetAllOtherTaskType();
    GetAllUsers();
});

function AddUserPopUP() {
    var bodyContent = '<div class="form-group"><label>Type Name:</label><input id="txtNAme" class="form-control"></input></div>';
    var footerContent = '<a id="addUser"   class="btn btn-block btn-success pull-right" style="width: 20%;" data-toggle="tab">Add</a><a class="btn btn-block btn-warning pull-right classEdit"  style="width: 20%; margin-top: 0px; margin-right: 2%;">Cancel</a>';
    $("#MyModal").find('.modal-content').find('.modal-header').find('h2').text('Add new Activity Type');
    $("#MyModal").find('.modal-content').find('.modal-body').html(bodyContent);
    $("#MyModal").find('.modal-content').find('.modal-footer').html(footerContent);
    $("#MyModal").find('.modal-content').find('.modal-footer').find('.btn-warning').unbind().bind('click', function () { $("#MyModal").hide(); })
    $("#MyModal").find('.modal-content').find('.modal-header').find('span').unbind().bind('click', function () { $("#MyModal").hide(); })
    $("#addUser").unbind().bind('click', function () { AddType() })
    $("#MyModal").show();
    return false;
}
function AddType() {
    if ($('#txtNAme').val() == '' ) {
        alert('Please enter name');
        return false;
    }
    else {
        $.ajax({
            type: "POST",
            url: url + "/AddOtherCategoryType",
            data: JSON.stringify({ name: $('#txtNAme').val() }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: OnSuccessCall,
            error: OnErrorCall
        });
        return false;
    }
}
function OnSuccessCall(response) {
    alert('Category Type Added Successfully');
    $("#MyModal").hide();
    BindCategory(response);
    return false;
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

function OnErrorCall(response) {
    alert(response);
    alert(response.status + " " + response.statusText);
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

function BindUsers(response) {
    var jsonResponse = JSON.parse(response.d);
    var taskHtml = '';
    taskHtml += "<option value='-1'>SELECT</option>";
    $.each(jsonResponse, function (key, user) {
        taskHtml += "<option value='" + user.UserId + "'>" + user.UserName + "</option>";
    });
    $('#ddlUsers').html(taskHtml);
}

function AssignOtherTaskToUser() {
    if ($('#txtName').val() == '' || $('#ddlActivityType').val() == '-1' || $('#ddlUsers').val() == '-1' ||
        $('#txtStartDate').val() == '' || $('#txtEndDate').val() == '') {
        alert('All fields are mandatory');
        return false;
    }
    else {
        $.ajax({
            type: "POST",
            url: url + "/AssignOtherTaskToUser",
            data: JSON.stringify({
                categoryType: $('#ddlActivityType').val(), name: $('#txtTaskNAme').val(), assignee: $('#ddlUsers').val(),
                startDate: $('#txtStartDate').val(), endDate: $('#txtEndDate').val(), comments: $('#txtComments').val()
            }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: SuccessAssignToUser,
            error: OnErrorCall
        });
        return false;
    }
}
function SuccessAssignToUser(response) {
    alert('Task assigned to user');
    window.location.href = 'OtherActivities.aspx';
}