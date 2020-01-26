var roleServiceUrl = 'RoleService.asmx';

var isEnquiryViewAccess =  true;
var isEnquiryEditAccess =  true;
var isEnquiryDeleteAccess =  true;
var isCustomerViewAccess =  true;
var isCustomerEditAccess =  true;
var isCustomerDeleteAccess =  true;
var isProjectsViewAccess =  true;
var isProjectsEditAccess =  true;
var isProjectsDeleteAccess =  true;
var isOtherActivityViewAccess =  true;
var isOtherActivityEditAccess =  true;
var isOtherActivityDeleteAccess =  true;
var isTaskUpdateViewAccess =  true;
var isTaskUpdateEditAccess =  true;
var isTaskUpdateDeleteAccess =  true;
var isAdminViewAccess =  true;
var isAdminEditAccess =  true;
var isAdminDeleteAccess =  true;
var isExpenseViewAccess =  true;
var isExpenseEditAccess =  true;
var isExpenseDeleteAccess =  true;
var isRolesViewAccess =  true;
var isRolesEditAccess =  true;
var isRolesDeleteAccess =  true;
var isReportsViewAccess =  true;
var isReportsEditAccess =  true;
var isReportsDeleteAccess = true;
var isEnquiryAccess = false;
var isCustomerAcccess = false;
var isProjectsAcccess = false;
var isOtherActivityAcccess = false;
var isTaskUpdateAcccess = false;
var isAdminAcccess = false;
var isExpenseAcccess = false;
var isReportsAcccess = false;
var isAddUserToProjViewAccess = true;
var isAddUserToProjEditAccess = true;
var isAddUserToProjDeleteAccess = true;
GetRolePermissions();


function GetRolePermissions() {
    $.ajax({
        type: "POST",
        url: roleServiceUrl + "/GetRoleDetailsByUserId",
        data: '',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: SetRolePermissions,
        error: OnErrorCall
    });
}

function SetRolePermissions(response) {
    var jsonResponse = JSON.parse(response.d);
    $.each(jsonResponse, function (key, role) {
        if (role.ActionName == 'Enquiry') {
            isEnquiryViewAccess = role.ViewAccess;
            isEnquiryEditAccess = role.EditAccess;
            isEnquiryDeleteAccess = role.DeleteAccess;
        }
        else if (role.ActionName == 'Customer') {
            isCustomerViewAccess = role.ViewAccess;
            isCustomerEditAccess = role.EditAccess;
            isCustomerDeleteAccess = role.DeleteAccess;
        }
        else if (role.ActionName == 'Projects') {
            isProjectsViewAccess = role.ViewAccess;
            isProjectsEditAccess = role.EditAccess;
            isProjectsDeleteAccess = role.DeleteAccess;
        }
        else if (role.ActionName == 'OtherActivity') {
            isOtherActivityViewAccess = false;
            isOtherActivityEditAccess = false;
            isOtherActivityDeleteAccess = false;
        }
        else if (role.ActionName == 'TaskUpdate') {
            isTaskUpdateViewAccess = role.ViewAccess;
            isTaskUpdateEditAccess = role.EditAccess;
            isTaskUpdateDeleteAccess = role.DeleteAccess;
        }
        else if (role.ActionName == 'Admin') {
            isAdminViewAccess = role.ViewAccess;
            isAdminEditAccess = role.EditAccess;
            isAdminDeleteAccess = role.DeleteAccess;
        }
        else if (role.ActionName == 'ExpenseManager') {
            isExpenseViewAccess = role.ViewAccess;
            isExpenseEditAccess = role.EditAccess;
            isExpenseDeleteAccess = role.DeleteAccess;
        }
        else if (role.ActionName == 'Roles') {
            isRolesViewAccess = role.ViewAccess;
            isRolesEditAccess = role.EditAccess;
            isRolesDeleteAccess = role.DeleteAccess;
        }
        else if (role.ActionName == 'Reports') {
            isReportsViewAccess = role.ViewAccess;
            isReportsEditAccess = role.EditAccess;
            isReportsDeleteAccess = role.DeleteAccess;
        }
        else if (role.ActionName == 'User Access To Project') {
            isAddUserToProjViewAccess = role.ViewAccess;
            isAddUserToProjEditAccess = role.EditAccess;
            isAddUserToProjDeleteAccess = role.DeleteAccess;
        }
    });



    if (isEnquiryViewAccess == 'False' && isEnquiryEditAccess == 'False' && isEnquiryDeleteAccess == 'False') {
        $('#liEnquiry').css('display', 'none');
    }
    else {
        $('#liEnquiry').css('display', 'block');
        isEnquiryAccess = true;
    }

    if (isCustomerViewAccess == 'False' && isCustomerEditAccess == 'False' && isCustomerDeleteAccess == 'False') {
        $('#liCustomer').css('display', 'none');
    }
    else {
        $('#liCustomer').css('display', 'block');
        isCustomerAcccess = true;
    }

    if (isProjectsViewAccess == 'False' && isProjectsEditAccess == 'False' && isProjectsDeleteAccess == 'False') {
        $('#liProjects').css('display', 'none');
    }
    else {
        $('#liProjects').css('display', 'block');
        isProjectsAcccess = true;
    }
    if (isOtherActivityViewAccess == 'False' && isOtherActivityEditAccess == 'False' && isOtherActivityDeleteAccess == 'False') {
        $('#liOtherActivity').css('display', 'none');
    }
    else {
        $('#liOtherActivity').css('display', 'block');
        isOtherActivityAcccess = true;
    }
    if (isTaskUpdateEditAccess == 'False') {
        $('#liTaskUpdate').css('display', 'none');
    }
    else {
        $('#liTaskUpdate').css('display', 'block');
        isTaskUpdateAcccess = true;
    }
    if (isAdminViewAccess == 'False' && isAdminEditAccess == 'False' && isAdminDeleteAccess == 'False') {
        $('#liAdmin').css('display', 'none');
    }
    else {
        $('#liAdmin').css('display', 'block');
        isAdminAcccess = true;
    }
    if (isExpenseViewAccess == 'False' && isExpenseEditAccess == 'False' && isExpenseDeleteAccess == 'False') {
        $('#liExpense').css('display', 'none');
    }
    else {
        $('#liExpense').css('display', 'block');
        isExpenseAcccess = true;
    }
    if (isRolesEditAccess == 'False') {
        $('#liRoles').css('display', 'none');
    }
    else {
        $('#liRoles').css('display', 'none');
    }
    if (isReportsViewAccess == 'False' && isReportsEditAccess == 'False' && isReportsDeleteAccess == 'False') {
        $('#liReports').css('display', 'none');
    }
    else {
        $('#liReports').css('display', 'block');
        isReportsAcccess = true;
    }
    $('#liQuotations').css('display', 'block');
    CallPageLoadFunctions();

}

function CallPageLoadFunctions() {
    if (window.location.href.indexOf('Enquiries.aspx') !== -1) {
        if (!isEnquiryAccess) {
            GotoNextPageWithAccess('enquiry');
        }
        if (isEnquiryViewAccess == 'True') {
            RefreshEnquiries($('#ContentPlaceHolder1_hdnEnquiryTpe').val());
        }
        if (isEnquiryEditAccess == 'False') {
            $('#divAddEnquiryButton').css('display', 'none');
        }
    } else if (window.location.href.indexOf('Customers.aspx') !== -1) {
        if (!isCustomerAcccess) {
            GotoNextPageWithAccess('customer');
        }
        if (isCustomerViewAccess == 'True') {
            RefreshCustomer();
        }
        if (isCustomerEditAccess == 'False') {
            $('#divIdAddCustomer').css('display', 'none');
        }
    }
    else if (window.location.href.indexOf('Projects.aspx') !== -1) {
        if (!isProjectsAcccess) {
            GotoNextPageWithAccess('project');
        }
        if (isProjectsViewAccess == 'True') {
            Refresh();
        }
        if (isProjectsEditAccess == 'False') {
            $('#divIdAddProject').css('display', 'none');
        }
    }
    else if (window.location.href.indexOf('OtherActivities.aspx') !== -1) {
        if (!isOtherActivityAcccess) {
            GotoNextPageWithAccess('otheractivity');
        }
        if (isOtherActivityViewAccess == 'True') {
            RefreshActivities();
        }
        if (isOtherActivityEditAccess == 'False') {
            $('#divIdAddOtherActivity').css('display', 'none');
        }
    }
    else if (window.location.href.indexOf('TaskUpdate.aspx') !== -1) {
        if (!isTaskUpdateAcccess) {
            GotoNextPageWithAccess('taskupdate');
        }
    }
    else if (window.location.href.indexOf('Admin.aspx') !== -1) {
        if (!isAdminAcccess) {
            GotoNextPageWithAccess('admin');
        }
        if (isAdminEditAccess == 'False') {
            $('#divAddUserId').css('display', 'none');
        }
    }
    else if (window.location.href.indexOf('Expense.aspx') !== -1) {
        if (!isExpenseAcccess) {
            GotoNextPageWithAccess('expense');
        }
        if (isExpenseEditAccess == 'False') {
            $('#tab1').html('<div class="box-header with-border">You dont have the permission for this action</div>');
            $('#tab3').html('<div class="box-header with-border">You dont have the permission for this action</div>');
        }
        if (isExpenseViewAccess == 'False') {
            $('#tab2').html('<div class="box-header with-border">You dont have the permission for this action</div>');
            $('#tab4').html('<div class="box-header with-border">You dont have the permission for this action</div>');
        }
        else {
            GetAllProjectExpenses();
            GetAllOtherTaskExpenses();
            GetAllProjectIncome();
        }
    }
    else if (window.location.href.indexOf('Reports.aspx') !== -1) {
        if (!isReportsAcccess) {
            GotoNextPageWithAccess('report');
        }
    }
    else if (window.location.href.indexOf('AddProject.aspx') !== -1 || window.location.href.indexOf('EditProject.aspx') !== -1) {
        if (isAddUserToProjEditAccess ==  'False') {
            $('#liUserProjectAccess').css('display', 'none');
        }
    }
}

function ValidateAndRedirectIfNoPageAccess() {
    if (window.location.href.indexOf('Enquiries.aspx') !== -1) {
        if (isEnquiryAccess == false) {

        }
    } else if (window.location.href.indexOf('Customers.aspx') !== -1) {
        if (isCustomerViewAccess == 'True') {
            RefreshCustomer();
        }
        if (isCustomerEditAccess == 'False') {
            $('#divIdAddCustomer').css('display', 'none');
        }
    }
    else if (window.location.href.indexOf('Projects.aspx') !== -1) {
        if (isProjectsViewAccess == 'True') {
            Refresh();
        }
        if (isProjectsEditAccess == 'False') {
            $('#divIdAddProject').css('display', 'none');
        }
    }
    else if (window.location.href.indexOf('OtherActivities.aspx') !== -1) {
        if (isOtherActivityViewAccess == 'True') {
            RefreshActivities();
        }
        if (isOtherActivityEditAccess == 'False') {
            $('#divIdAddOtherActivity').css('display', 'none');
        }
    }
    else if (window.location.href.indexOf('TaskUpdate.aspx') !== -1) {
        if (isOtherActivityViewAccess == 'True') {
            RefreshActivities();
        }
        if (isOtherActivityEditAccess == 'False') {
            $('#divIdAddOtherActivity').css('display', 'none');
        }
    }
    else if (window.location.href.indexOf('Admin.aspx') !== -1) {
        if (isAdminEditAccess == 'False') {
            $('#divAddUserId').css('display', 'none');
        }
    }
    else if (window.location.href.indexOf('Expense.aspx') !== -1) {
        if (isExpenseEditAccess == 'False') {
            $('#tab1').html('<div class="box-header with-border">You dont have the permission for this action</div>');
            $('#tab3').html('<div class="box-header with-border">You dont have the permission for this action</div>');
        }
        if (isExpenseViewAccess == 'False') {
            $('#tab2').html('<div class="box-header with-border">You dont have the permission for this action</div>');
            $('#tab4').html('<div class="box-header with-border">You dont have the permission for this action</div>');
        }
        else {
            GetAllProjectExpenses();
            GetAllOtherTaskExpenses();
            GetAllProjectIncome();
        }

    }
    else if (window.location.href.indexOf('Reports.aspx') !== -1) {
        if (isAdminEditAccess == 'False') {
            $('#divAddUserId').css('display', 'none');
        }
    }
}

function GotoNextPageWithAccess(currentPage) {
    if (!isEnquiryAccess && !isCustomerAcccess && !isProjectsAcccess && !isOtherActivityAcccess && !isTaskUpdateAcccess && !isAdminAcccess && !isExpenseAcccess && !isReportsAcccess) {
        alert('You dont have access');
        window.location.href = 'login.aspx';
    }
    else {
        if (currentPage == 'enquiry') {
            if (isCustomerAcccess) {
                window.location.href = 'Customers.aspx';
            }
            else {
                currentPage = 'customer';
            }
        }

        if (currentPage == 'customer') {
            if (isProjectsAcccess) {
                window.location.href = 'Projects.aspx';
            }
            else {
                currentPage = 'project';
            }
        }

        if (currentPage == 'project') {
            if (isOtherActivityAcccess) {
                window.location.href = 'OtherActivities.aspx';
            }
            else {
                currentPage = 'otheractivity';
            }
        }

        if (currentPage == 'otheractivity') {
            if (isTaskUpdateAcccess) {
                window.location.href = 'TaskUpdate.aspx';
            }
            else {
                currentPage = 'taskupdate';
            }
        }

        if (currentPage == 'taskupdate') {
            if (isAdminAcccess) {
                window.location.href = 'Admin.aspx';
            }
            else {
                currentPage = 'admin';
            }
        }

        if (currentPage == 'admin') {
            if (isExpenseAcccess) {
                window.location.href = 'Expense.aspx';
            }
            else {
                currentPage = 'expense';
            }
        }

        if (currentPage == 'expense') {
            if (isReportsAcccess) {
                window.location.href = 'Reports.aspx';
            }
            else {
                currentPage = 'report';
            }
        }

        if (currentPage == 'report') {
            if (isEnquiryAccess) {
                window.location.href = 'Enquiries.aspx';
            }
            else {
                GotoNextPageWithAccess('enquiry');
            }
        }
    }
}