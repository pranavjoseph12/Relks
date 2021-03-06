﻿var url = "UserService.asmx";
var roleServiceUrl = "RoleService.asmx";
var roleResponse;
$(document).ready(function () {
    LoadUsers();
    LoadRoles();
});

function AddUserPopUP() {
    var bodyContent = '';
    //var bodyContent = '<div class="form-group"><label>Allow to assign Tasks:</label><select id="txtAllowAssignTask" class="form-control">';
    //bodyContent += '<option value="No">No</option><option value="Yes">Yes</option></select></div>';
    bodyContent += '<div class="form-group"><label>Name:</label><input id="txtUserName" class="form-control"></input></div>';
    bodyContent += '<div class="form-group"><label>Phone:</label><input id="txtPhone" class="form-control"></input></div>';
    bodyContent += '<div class="form-group"><label>Email(login id):</label><input id="txtEmail" class="form-control"></input></div>';
    bodyContent += '<div class="form-group"><label>Password:</label><input id="txtPassword" class="form-control"></input></div>';
    bodyContent += '<div class="form-group"><label>Role:</label><select id="ddlRoles"><option value="User">User</option></div>';
    var footerContent = '<a id="addUser"   class="btn btn-block btn-success pull-right" style="width: 20%;" data-toggle="tab">Add</a><a class="btn btn-block btn-warning pull-right classEdit"  style="width: 20%; margin-top: 0px; margin-right: 2%;">Cancel</a>';
    $("#MyModal").find('.modal-content').find('.modal-header').find('h2').text('Add a new User');
    $("#MyModal").find('.modal-content').find('.modal-body').html(bodyContent);
    $("#MyModal").find('.modal-content').find('.modal-footer').html(footerContent);
    $("#MyModal").find('.modal-content').find('.modal-footer').find('.btn-warning').unbind().bind('click', function () { $("#MyModal").hide(); })
    $("#MyModal").find('.modal-content').find('.modal-header').find('span').unbind().bind('click', function () { $("#MyModal").hide(); })
    $("#addUser").unbind().bind('click', function () { AddUser() })
    $("#ddlRoles").change(function () {
        var role = this.value;
        if (role == 2) {
            if ($("#ddlCenter").length == 0) {
                var centerData = '<div class="form-group"><label>Center:</label><select id="ddlCenter"><option value="User">User</option></div>';
                $("#ddlRoles").parent('div').after(centerData);
                LoadCenter();
            }
            
        }
    });
    LoadRoles();
    $("#MyModal").show();
    return false;
}
function LoadCenter() {
    $.ajax({
        type: "POST",
        url: url + "/GetAllCenters",
        data: '',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: BindCenters,
        error: OnErrorCall
    });
}
function BindCenters(response) {
    BindAllRoles(response);
    var jsonResponse = JSON.parse(response.d);
    var taskHtml = '';
    taskHtml += "<option value='-1'>SELECT</option>";
    $.each(jsonResponse, function (key, user) {
        taskHtml += "<option value='" + user.Id + "'>" + user.Name + "</option>";
    });
    $('#ddlCenter').html(taskHtml);
}
function AddUser() {
    var curRole = $('#ddlRoles').val();
    var center = 0;
    if (curRole == 2)
        center = $('#ddlCenter').val();
    if ($('#txtUserName').val() == '' || $('#txtPhone').val() == '' || $('#txtEmail').val() == '' || $('#txtPassword').val() == '' || $('#ddlRoles').val() == '-1' || center == '-1') {
        alert('Please enter all the fields');
        return false;
    }
    else {
       
        $.ajax({
            type: "POST",
            url: url + "/AddUser",
            data: JSON.stringify({
                name: $('#txtUserName').val(), phone: $('#txtPhone').val(), email: $('#txtEmail').val(), password: $('#txtPassword').val(),
                role: $('#ddlRoles').val(), allowAssignTask: 'YES', centerValue: center
            }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: OnSuccessCall,
            error: OnErrorCall
        });
        return false;
    }
}
function OnSuccessCall(response) {
    alert('User Added Successfully');
    $("#MyModal").hide();
    LoadUsers();
    return false;
}

function LoadUsers() {
    $.ajax({
        type: "POST",
        url: url + "/GetAllUsers",
        data: '',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: BindUserData,
        error: OnErrorCall
    });
}

function LoadRoles() {
    $.ajax({
        type: "POST",
        url: roleServiceUrl + "/GetAllRoleNameAndId",
        data: '',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: BindRoles,
        error: OnErrorCall
    });
}

function BindRoles(response) {
    BindAllRoles(response);
    var jsonResponse = JSON.parse(response.d);
    var taskHtml = '';
    taskHtml += "<option value='-1'>SELECT</option>";
    $.each(jsonResponse, function (key, user) {
        taskHtml += "<option value='" + user.RoleID + "'>" + user.RoleName + "</option>";
    });
    $('#ddlRoles').html(taskHtml);
}

function BindAllRoles(response) {
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
        trObj += '<td class=" ">' + jsonResponse[i].RoleName + '</td>';
        trObj += '<td class=" " style="text-align: center">';
        trObj += '<a title="View Details" href="#" onclick="return false;" class="btn btn-success small_sp" style="margin-right: 5px;"><i class="fa fa-search"></i></a>';
        if (isRolesEditAccess == 'True') {
            trObj += '<a title="Edit Details" href="EditRole.aspx?id=' + jsonResponse[i].RoleID +'" class="btn btn-warning small_sp" style="margin-right: 5px;"><i class="fa fa-pencil"></i></a>';
        }
        if (isRolesDeleteAccess == 'True') {
            trObj += '<div title="Delete" onclick="return ShowDeletePopUP(\''+ jsonResponse[i].RoleID + '\', \'' + jsonResponse[i].RoleName + '\');" class="btn btn-danger small_sp" style="margin-right: 5px;"><i class="fa fa-trash"></i></div>';
        }
        trObj += '</td></tr>';
    }
    $('#tdInnerRowRoles').html(trObj);
    //Pagination(totalCount);
}

function BindUserData(response) {
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
        trObj += '<td class=" ">' + jsonResponse[i].UserName + '</td>';
        trObj += '<td class=" ">' + jsonResponse[i].Phone + '</td>';
        trObj += '<td class=" ">' + jsonResponse[i].Email + '</td>';
        trObj += '<td class=" " style="text-align: center">';
        trObj += '<div title="View Details" onclick="return false;" class="btn btn-success small_sp" style="margin-right: 5px;"><i class="fa fa-search"></i></div>';
        if (isAdminEditAccess == 'True') {
            trObj += '<div title="Edit Details" class="btn btn-warning small_sp" style="margin-right: 5px;"><i class="fa fa-pencil"></i></div>';
        }
        if (isAdminDeleteAccess == 'True') {
            trObj += '<div title="Delete" onclick="return ShowDeleteUserPopUP(\'' + jsonResponse[i].UserId + '\', \'' + jsonResponse[i].UserName + '\');" class="btn btn-danger small_sp" style="margin-right: 5px;"><i class="fa fa-trash"></i></div>';
        }
        trObj += '</td></tr>';
    }
    $('#tdInnerRowUsers').html(trObj);
    //Pagination(totalCount);
}

function ShowDeletePopUP(RoleId, RoleName) {
    var bodyContent = '<div class="form-group"><label>Name:</label><label style="margin-left:20px;">' + RoleName + '</label></div>'
    var footerContent = '<a id="newCatSubmit"   class="btn btn-block btn-success pull-right" style="width: 20%;" data-toggle="tab">Delete</a><a class="btn btn-block btn-warning pull-right classEdit"  style="width: 20%; margin-top: 0px; margin-right: 2%;">Cancel</a>';
    $("#MyModal").find('.modal-content').find('.modal-header').find('h2').text('Confirm Delete Role?');
    $("#MyModal").find('.modal-content').find('.modal-body').html(bodyContent);
    $("#MyModal").find('.modal-content').find('.modal-footer').html(footerContent);
    $("#MyModal").find('.modal-content').find('.modal-footer').find('.btn-warning').unbind().bind('click', function () { $("#MyModal").hide(); })
    $("#MyModal").find('.modal-content').find('.modal-header').find('span').unbind().bind('click', function () { $("#MyModal").hide(); })
    $("#newCatSubmit").unbind().bind('click', function () { DeleteRole(RoleId) })
    $("#MyModal").show();
    return false;
}

function ShowDeleteUserPopUP(userId, userName) {
    var bodyContent = '<div class="form-group"><label>Name:</label><label style="margin-left:20px;">' + userName + '</label></div>'
    var footerContent = '<a id="newCatSubmit"   class="btn btn-block btn-success pull-right" style="width: 20%;" data-toggle="tab">Delete</a><a class="btn btn-block btn-warning pull-right classEdit"  style="width: 20%; margin-top: 0px; margin-right: 2%;">Cancel</a>';
    $("#MyModal").find('.modal-content').find('.modal-header').find('h2').text('Confirm Delete User?');
    $("#MyModal").find('.modal-content').find('.modal-body').html(bodyContent);
    $("#MyModal").find('.modal-content').find('.modal-footer').html(footerContent);
    $("#MyModal").find('.modal-content').find('.modal-footer').find('.btn-warning').unbind().bind('click', function () { $("#MyModal").hide(); })
    $("#MyModal").find('.modal-content').find('.modal-header').find('span').unbind().bind('click', function () { $("#MyModal").hide(); })
    $("#newCatSubmit").unbind().bind('click', function () { DeleteUser(userId) })
    $("#MyModal").show();
    return false;
}

function DeleteRole(roleId) {
    $("#MyModal").hide();
    $.ajax({
        type: "POST",
        url: roleServiceUrl + "/DeleteRole",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ "roleId": roleId }),
        success: function (result) {
            LoadRoles();
            if (result.d == true) {
                //pageNumber = 1;
                alert('Deleted Role Successfully');
            }
            else {
                alert("Error in deletion. Please try again.");
            }
            
        }
    });
}

function DeleteUser(userId) {
    $("#MyModal").hide();
    $.ajax({
        type: "POST",
        url: url + "/DeleteUser",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ "userId": userId }),
        success: function (result) {
            LoadUsers();
            if (result.d === 'true') {
                //pageNumber = 1;
                alert('Deleted User Successfully');
            }
            else {
                alert("Error in deletion. Please try again.");
            }

        }
    });
}