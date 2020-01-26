var url = "NotificationService.asmx";
var roleServiceUrl = 'RoleService.asmx';
RefreshNotifications();
//var isEnquiryViewAccess= false;
//var isEnquiryEditAccess= false;
//var isEnquiryDeleteAccess = false;
//var isCustomerViewAccess= false;
//var isCustomerEditAccess= false;
//var isCustomerDeleteAccess = false;
//var isProjectsViewAccess= false;
//var isProjectsEditAccess= false;
//var isProjectsDeleteAccess = false;
//var isOtherActivityViewAccess= false;
//var isOtherActivityEditAccess= false;
//var isOtherActivityDeleteAccess = false;
//var isTaskUpdateViewAccess= false;
//var isTaskUpdateEditAccess= false;
//var isTaskUpdateDeleteAccess = false;
//var isAdminViewAccess= false;
//var isAdminEditAccess= false;
//var isAdminDeleteAccess = false;
//var isExpenseViewAccess= false;
//var isExpenseEditAccess= false;
//var isExpenseDeleteAccess = false;
//var isRolesViewAccess= false;
//var isRolesEditAccess= false;
//var isRolesDeleteAccess = false;
//var isReportsViewAccess= false;
//var isReportsEditAccess= false;
//var isReportsDeleteAccess = false;
//GetRolePermissions();

function RefreshNotifications() {
    $.ajax({
        type: "POST",
        url: url + "/GetAllEnquiries",
        data: '',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccessCall,
        error: OnErrorCall
    });
}



function OnSuccessCall(response) {
    var notificationCount = 0;
    var notifications = '';
    var jsonResponse = JSON.parse(response.d);
    if (jsonResponse.OverDueCount > 0) {
        notificationCount = notificationCount + 1;
        notifications += ' <li><a style="color:red;" href="enquiries.aspx?type=overdue"> <i class="fa fa-users text-aqua"></i>' + jsonResponse.OverDueCount + ' Enquiries is Overdue </a></li>';
    }
    if (jsonResponse.DueTodayCount > 0) {
        notificationCount = notificationCount + 1;
        notifications += ' <li><a href="enquiries.aspx?type=today"> <i class="fa fa-users text-aqua"></i>' + jsonResponse.DueTodayCount + ' Enquiries is Due Today </a></li>';
    }

    $('#liNotificationCountText').html('You have ' + notificationCount + ' notifications');
    $('#spanNotificationCount').html(notificationCount);
    $('#notificationMenuItems').html(notifications);




    var className = '';
    var trObj = '';
    for (var i = 0; i < jsonResponse.length; i++) {
        if (i % 2 == 0) {
            className = 'odd';
        }
        else {
            className = 'even'
        }

        trObj += '<tr class="' + className + '">';
        trObj += '<td class=" ">' + jsonResponse[i].Name + '</td>';
        trObj += '<td class=" ">' + jsonResponse[i].Phone + '</td>';
        trObj += ' <td class=" ">' + jsonResponse[i].Email + '</td>';
        trObj += '<td class=" ">' + jsonResponse[i].Response + '</td>';
        trObj += '<td class=" ">' + jsonResponse[i].EnquiryDate + '</td>';
        trObj += '<td class=" " style="text-align: center">';
        trObj += '<div title="View Details" onclick="window.location.href=\'ViewEnquiry.aspx?EnquiryID=' + jsonResponse[i].EnquiryId + '&IsEditMode=false\';" class="btn btn-success small_sp" style="margin-right: 5px;"><i class="fa fa-search"></i></div>';
        trObj += '<button onclick="return false;" class="btn btn-warning small_sp" style="margin-right: 5px;"><i class="fa fa-pencil"></i></button>';
        trObj += '<button title="Delete" onclick="return ShowDeletePopUP(\' ' + jsonResponse[i].EnquiryId + '\',\' ' + jsonResponse[i].Name + '\',\' ' + jsonResponse[i].Phone + '\');" class="btn btn-danger small_sp" style="margin-right: 5px;"><i class="fa fa-trash-o"></i></button>';
        trObj += '<button onclick="return false;" class="btn btn-primary small_sp" style="margin-right: 5px;"><i class="fa fa-exchange"></i></button>';
        trObj += ' </td></tr>';
        trObj += '';

    }
    $('#tdInnerRow').html(trObj);
}

function OnErrorCall(response) {
    alert(response);
    alert(response.status + " " + response.statusText);
}

function Pagination(totalCount) {
    var pagination = '<ul class="pagination pull-right"><li class="prev disabled"><a onclick="return false;" href="#">← Previous</a></li><li class="next"><a href="#">Next → </a></li></ul>';
    var startNumber = 1;
    if (totalCount > 0) {
        if (pageNumber == 1) {
            if (pageNumber * $('#ddlNumberOFRecords').val() < totalCount) {
                pagination = '<ul class="pagination pull-right"><li class="prev disabled"><a onclick="return false;" href="#">← Previous</a></li><li class="next"><a  onclick="NextClick();" href="#">Next → </a></li></ul>';
            }
            else {
                pagination = '<ul class="pagination pull-right"><li class="prev disabled"><a onclick="return false;" href="#">← Previous</a></li><li class="next disabled"><a onclick="return false;" href="#">Next → </a></li></ul>';
            }
        }
        else {
            startNumber = ((pageNumber - 1) * $('#ddlNumberOFRecords').val()) + 1;
            if (pageNumber * $('#ddlNumberOFRecords').val() < totalCount) {
                pagination = '<ul class="pagination pull-right"><li class="prev"><a  onclick="PrevClick();" href="#">← Previous</a></li><li class="next"><a onclick="NextClick();" href="#">Next → </a></li></ul>';
            }
            else {
                pagination = '<ul class="pagination pull-right"><li class="prev"><a  onclick="PrevClick();" href="#">← Previous</a></li><li class="next disabled"><a onclick="return false;" href="#">Next → </a></li></ul>';
            }
        }
        if (pageNumber * $('#ddlNumberOFRecords').val() < totalCount) {
            startNumber = ((pageNumber - 1) * $('#ddlNumberOFRecords').val()) + 1;
            $('#divCount').html('Showing ' + startNumber + ' to ' + pageNumber * $('#ddlNumberOFRecords').val() + ' of ' + totalCount + ' entries');
        }
        else {
            startNumber = ((pageNumber - 1) * $('#ddlNumberOFRecords').val()) + 1;
            $('#divCount').html('Showing ' + startNumber + ' to ' + totalCount + ' of ' + totalCount + ' entries');
        }
    }
    else {
        $('#divCount').html('No records');
    }
    $('#divPagination').html(pagination);
}

function CustomPagination(totalCount, customPageNumber, divCountId, divPaginationId) {
    var pagination = '<ul class="pagination pull-right"><li class="prev disabled"><a onclick="return false;" href="#">← Previous</a></li><li class="next"><a href="#">Next → </a></li></ul>';
    var startNumber = 1;
    if (totalCount > 0) {
        if (customPageNumber == 1) {
            if (customPageNumber * 10 < totalCount) {
                pagination = '<ul class="pagination pull-right"><li class="prev disabled"><a onclick="return false;" href="#">← Previous</a></li><li class="next"><a  onclick="NextClick();" href="#">Next → </a></li></ul>';
            }
            else {
                pagination = '<ul class="pagination pull-right"><li class="prev disabled"><a onclick="return false;" href="#">← Previous</a></li><li class="next disabled"><a onclick="return false;" href="#">Next → </a></li></ul>';
            }
        }
        else {
            startNumber = ((customPageNumber - 1) * 10) + 1;
            if (customPageNumber * 10 < totalCount) {
                pagination = '<ul class="pagination pull-right"><li class="prev"><a  onclick="PrevClick();" href="#">← Previous</a></li><li class="next"><a onclick="NextClick();" href="#">Next → </a></li></ul>';
            }
            else {
                pagination = '<ul class="pagination pull-right"><li class="prev"><a  onclick="PrevClick();" href="#">← Previous</a></li><li class="next disabled"><a onclick="return false;" href="#">Next → </a></li></ul>';
            }
        }
        if (customPageNumber * 10 < totalCount) {
            startNumber = ((customPageNumber - 1) * 10) + 1;
            $('#' + divCountId).html('Showing ' + startNumber + ' to ' + customPageNumber * 10 + ' of ' + totalCount + ' entries');
        }
        else {
            startNumber = ((customPageNumber - 1) * 10) + 1;
            $('#' + divCountId).html('Showing ' + startNumber + ' to ' + totalCount + ' of ' + totalCount + ' entries');
        }
    }
    else {
        $('#' + divCountId).html('No records');
    }
    $('#' + divPaginationId).html(pagination);
}

function CustomPaginationClick(totalCount, customPageNumber, divCountId, divPaginationId, prevClick, nextClick) {
    var pagination = '<ul class="pagination pull-right"><li class="prev disabled"><a onclick="return false;" href="#">← Previous</a></li><li class="next"><a href="#">Next → </a></li></ul>';
    var startNumber = 1;
    if (totalCount > 0) {
        if (customPageNumber == 1) {
            if (customPageNumber * 10 < totalCount) {
                pagination = '<ul class="pagination pull-right"><li class="prev disabled"><a onclick="return false;" href="#">← Previous</a></li><li class="next"><a  onclick="' + nextClick +'();" href="#">Next → </a></li></ul>';
            }
            else {
                pagination = '<ul class="pagination pull-right"><li class="prev disabled"><a onclick="return false;" href="#">← Previous</a></li><li class="next disabled"><a onclick="return false;" href="#">Next → </a></li></ul>';
            }
        }
        else {
            startNumber = ((customPageNumber - 1) * 10) + 1;
            if (customPageNumber * 10 < totalCount) {
                pagination = '<ul class="pagination pull-right"><li class="prev"><a  onclick="' + prevClick +'();" href="#">← Previous</a></li><li class="next"><a onclick="'+ nextClick + '();" href="#">Next → </a></li></ul>';
            }
            else {
                pagination = '<ul class="pagination pull-right"><li class="prev"><a  onclick="' + prevClick + '();" href="#">← Previous</a></li><li class="next disabled"><a onclick="return false;" href="#">Next → </a></li></ul>';
            }
        }
        if (customPageNumber * 10 < totalCount) {
            startNumber = ((customPageNumber - 1) * 10) + 1;
            $('#' + divCountId).html('Showing ' + startNumber + ' to ' + customPageNumber * 10 + ' of ' + totalCount + ' entries');
        }
        else {
            startNumber = ((customPageNumber - 1) * 10) + 1;
            $('#' + divCountId).html('Showing ' + startNumber + ' to ' + totalCount + ' of ' + totalCount + ' entries');
        }
    }
    else {
        $('#' + divCountId).html('No records');
    }
    $('#' + divPaginationId).html(pagination);
}