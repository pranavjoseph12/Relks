var projectServiceUrl = "ProjectService.asmx";
var reportsUrl = 'ReportService.asmx';
var expensePageNumber = 1;
var enquiryPageNumber = 1;
var userTaskPageNumber = 1;


$(document).ready(function () {
    
    $("#btnEnquiryExportToExcel").unbind().bind('click', function () { DownloadEnquiryReport(); })
    $("#btnExpenseExportToExcel").unbind().bind('click', function () { DownloadExpenseReport(); })
    $("#btnTaskExportToExcel").unbind().bind('click', function () { DownloadTaskReport(); })
    $("#txtUserTaskFrom").datepicker({ format: 'yyyy-mm-dd' }).on('change', function () {
        $('.datepicker').hide();
    });
    $("#txtUserTaskTo").datepicker({ format: 'yyyy-mm-dd' }).on('change', function () {
        $('.datepicker').hide();
    });

    $("#dateFromExp").datepicker({ format: 'yyyy-mm-dd' }).on('change', function () {
        $('.datepicker').hide();
    });
    $("#dateToExp").datepicker({ format: 'yyyy-mm-dd' }).on('change', function () {
        $('.datepicker').hide();
    });
   
    GetAllExpenseCategory();
    GetAllUsers();
    GetExpenseReport();
    RefreshEnquiries();
});

function SearchClickUserTasks() {

    if ($('#ddlUsersUserTask').val() == '-1') {
        alert('Please select User');
        return false;
    }

    else {
        $.ajax({
            type: "POST",
            url: reportsUrl + "/GetAllUserTaskReport",
            data: JSON.stringify({
                pageNumber: expensePageNumber, projectName: $('#txtUserTaskProjectName').val(), phaseName: $('#txtUserTaskPhaseName').val(),
                userId: $('#ddlUsersUserTask').val(), fromDate: $('#txtUserTaskFrom').val(), toDate: $('#txtUserTaskTo').val(), numberOfrecords: $('#ddlUserTaskRecordCount').val()
            }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: BindUserTaskReport,
            error: OnErrorCall
        });

    }

    return false;
}

function BindUserTaskReport(response) {
    var jsonResponse = JSON.parse(response.d);
    var className = '';
    var trObj = '';
    var totalCount = 0;
    var currentStatus = '';
    var totalHoursWorked = '0';
    for (var i = 0; i < jsonResponse.length; i++) {
        totalHoursWorked = jsonResponse[i].HoursWorked;
        if (i % 2 == 0) {
            className = 'odd';
        }
        else {
            className = 'even'
        }
        totalCount = jsonResponse[i].TotalCount;

        trObj += '<tr class="' + className + '">';
        trObj += '<td class=" ">' + jsonResponse[i].ProjectName + '</td>';
        trObj += '<td class=" ">' + jsonResponse[i].PhaseName + '</td>';
        trObj += '<td class=" ">' + jsonResponse[i].SubTaskName + '</td>';
        trObj += ' <td class=" ">' + jsonResponse[i].ExpectedStartDate + '</td>';
        trObj += '<td class=" ">' + jsonResponse[i].ExpectedEndDate + '</td>';
        if (jsonResponse[i].IsCompleted == true) {
            trObj += '<td class=" ">Y</td>';
            trObj += '<td class=" ">' + jsonResponse[i].LastUpdatedDate + '</td>';
        }
        else {
            trObj += '<td class=" ">N</td>';
            trObj += '<td class=" "></td>';
        }
        trObj += '<td class=" "><div title="Delete Task" onclick="return DeleteTask(\' ' + jsonResponse[i].ProjectSubTaskId +'\')" class="btn btn-success small_sp" style="margin-right: 5px;"><i class="fa fa-trash-o"></i></div></td>';
        trObj += '</tr>';
    }
    $('#tdInnerRowUserTaskReport').html(trObj);
    //CustomPaginationClick(totalCount, userTaskPageNumber, 'divCountExpense', 'divPaginationExpense', 'PrevClickExpense', 'NextClickExpense');
    CustomPagination(totalCount, userTaskPageNumber, 'divUserTaskReportCount', 'divUserTaskReportPagination', 'PrevClickUserTask', 'NextClickUserTask');
    $('#divTotalHoursWorked').html('Total hours Worked : ' + totalHoursWorked);
    
}
function DeleteTask(subTaskId) {
    if (confirm('Are you Sure you want to delete the selected sale?')) {
        //showProgress();
        $.ajax({
            type: "POST",
            url: reportsUrl + "/DeleteTask",
            data: JSON.stringify({ projectSubTaskId: subTaskId }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: BindUserTaskDelete,
            error: OnErrorCall
        });
    }
    
}
function BindUserTaskDelete(response) {
    SearchClickUserTasks();
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
    taskHtml += "<option value='-1'>SELECT USER</option>";
    $.each(jsonResponse, function (key, user) {
        taskHtml += "<option value='" + user.UserId + "'>" + user.UserName + "</option>";
    });
    $('#ddlUsersUserTask').html(taskHtml);
}

function GetAllExpenseCategory() {
    $.ajax({
        type: "POST",
        url: projectServiceUrl + "/GetAllExpenseCategory",
        data: '',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: BindExpenseCategory,
        error: OnErrorCall
    });
}

function BindExpenseCategory(response) {
    var jsonResponse = JSON.parse(response.d);
    var projectObj = '';
    projectObj += "<option value='-1'>All Category</option>";
    $.each(jsonResponse, function (key, expense) {
        projectObj += "<option value='" + expense.CategoryId + "'>" + expense.CategoryName + "</option>";
    });
    $('#ddlCategorySearch').html(projectObj);

}

function SearchClick() {
    GetExpenseReport();
    return false;
}
function SearchClickEnquiry() {
    RefreshEnquiries();
    return false;
}
function ChangeDueType() {
    GetExpenseReport();
    return false;
}

function GetExpenseReport() {
    $.ajax({
        type: "POST",
        url: reportsUrl + "/GetAllExpensesReport",
        data: JSON.stringify({
            pageNumber: expensePageNumber, searchTerm: $('#txtEnqSeacrh').val(),
            category: $('#ddlCategorySearch').val(), fromDate: $('#dateFromExp').val(), toDate: $('#dateToExp').val(), expenseFor: 'All'
        }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: BindExpenses,
        error: OnErrorCall
    });
}

function BindExpenses(response) {
    var jsonResponse = JSON.parse(response.d);
    var className = '';
    var trObj = '';
    var totalCount = 0;
    for (var i = 0; i < jsonResponse.length; i++) {
        $('#divExpense').html('Total Expense: <b>' + jsonResponse[i].TotalExpense + '</b>');
        if (i % 2 == 0) {
            className = 'odd';
        }
        else {
            className = 'even'
        }
        totalCount = jsonResponse[i].TotalCount;

        trObj += '<tr class="' + className + '">';
        trObj += '<td class=" ">' + jsonResponse[i].ExpenseName + '</td>';
        trObj += '<td class=" ">' + jsonResponse[i].ExpenseDate + '</td>';
        trObj += ' <td class=" ">' + jsonResponse[i].ProjectName + '</td>';
        trObj += '<td class=" ">' + jsonResponse[i].PhaseName + '</td>';
        trObj += '<td class=" ">' + jsonResponse[i].Amount + '</td>';
        trObj += '<td class=" ">' + jsonResponse[i].CategoryName + '</td>';
        trObj += '<td class=" ">' + jsonResponse[i].Comments + '</td>';
        //trObj += '<td class=" " style="text-align: center">';
        //trObj += '<button title="Delete" onclick="return ShowDeletePopUP(\' ' + jsonResponse[i].EnquiryId + '\',\' ' + jsonResponse[i].Name + '\',\' ' + jsonResponse[i].Phone + '\');" class="btn btn-danger small_sp" style="margin-right: 5px;"><i class="fa fa-trash-o"></i></button>';
        //trObj += ' </td>';
        trObj += '</tr>';

    }
    $('#tdInnerRowExpense').html(trObj);
    CustomPaginationClick(totalCount, expensePageNumber, 'divCountExpense', 'divPaginationExpense', 'PrevClickExpense', 'NextClickExpense');
}

function RefreshEnquiries() {
    var numberOfRecords = 1000000;
    $.ajax({
        type: "POST",
        url: reportsUrl + "/GetAllEnquiries",
        data: JSON.stringify({ enquiryType: 'all', pageNumber: enquiryPageNumber, searchTerm: $('#txtEnquirySearch').val(), numberOfRecords: $('#ddlNoOfRecoredEnquiryReport').val(), fromDate: $('#dateFromEnquiry').val(), toDate: $('#dateToEnquiry').val() }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: BindEnquiryReport,
        error: OnErrorCall
    });
}

function GetUserTaskReport() {
    var numberOfRecords = 1000000;
    $.ajax({
        type: "POST",
        url: reportsUrl + "/GetUserTaskReport",
        data: JSON.stringify({ userId: 'all', pageNumber: userTaskPageNumber, dateFrom: $('#txtEnqSeacrh').val(), dateTo: $('#txtEnqSeacrh').val(), numberOfRecords: $('#ddlNoOfRecoredUserTaskReport').val() }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: BindEnquiryReport,
        error: OnErrorCall
    });
}

function BindEnquiryReport(response) {
    var jsonResponse = JSON.parse(response.d);
    var className = '';
    var trObj = '';
    var totalCount = 0;
    var existingCustomer = false;
    for (var i = 0; i < jsonResponse.length; i++) {
        existingCustomer = false;
        if (i % 2 == 0) {
            className = 'odd';
        }
        else {
            className = 'even'
        }
        totalCount = jsonResponse[i].TotalEnquiries;

        trObj += '<tr class="' + className + '">';
        trObj += '<td class=" ">' + jsonResponse[i].Name + '</td>';
        trObj += '<td class=" ">' + jsonResponse[i].Phone + '</td>';
        trObj += ' <td class=" ">' + jsonResponse[i].Email + '</td>';
        trObj += '<td class=" ">' + jsonResponse[i].Response + '</td>';
        trObj += '<td class=" ">' + jsonResponse[i].EnquiryDate + '</td>';
       // trObj += '<td class=" ">' + jsonResponse[i].Comments + '</td>';
        if (jsonResponse[i].ExistingCustomerId > 0) {
            existingCustomer = true;
            trObj += '<td class=" ">Yes</td>';
        }
        else {
            trObj += '<td class=" ">No</td>';
        }
       // trObj += '<td class=" ">' + jsonResponse[i].Comments + '</td>';
        trObj += '</tr>';

    }
    $('#tdInnerRowEnquiry').html(trObj);
    //Pagination(totalCount);
    CustomPaginationClick(totalCount, enquiryPageNumber, 'divCountEnquiryReport', 'divPaginationEnquiryReport', 'PrevClickEnquiry', 'NextClickEnquiry');
}
function DownloadEnquiryReport() {
    $.ajax({
        type: "POST",
        url: reportsUrl + "/GetEnquiryExcelReport",
        data: JSON.stringify({ enquiryType: 'all', pageNumber: 1, searchTerm: $('#txtEnquirySearch').val(), numberOfRecords: $('#ddlNoOfRecoredEnquiryReport').val(), fromDate: $('#dateFromEnquiry').val(), toDate: $('#dateToEnquiry').val() }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: LoadFile,
        error: OnErrorCall
       
    });
}
function LoadFile(response) {
    window.location = window.location.origin + '/Download/Enquiry.xlsx';
}
function DownloadExpenseReport() {
    $.ajax({
        type: "POST",
        url: reportsUrl + "/GetExpenseExcelReport",
        data: JSON.stringify({
            pageNumber: 1, searchTerm: $('#txtEnqSeacrh').val(),
            category: $('#ddlCategorySearch').val(), fromDate: $('#dateFromExp').val(), toDate: $('#dateToExp').val(), expenseFor: 'All'
        }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: LoadFileExpense,
        error: OnErrorCall

    });
}
function LoadFileExpense(response) {
    window.location = window.location.origin + '/Download/Expense.xlsx';
}

function DownloadTaskReport() {
    $.ajax({
        type: "POST",
        url: reportsUrl + "/GetTaskExcelReport",
        data: JSON.stringify({
            pageNumber: 1, projectName: $('#txtUserTaskProjectName').val(), phaseName: $('#txtUserTaskPhaseName').val(),
            userId: $('#ddlUsersUserTask').val(), fromDate: $('#txtUserTaskFrom').val(), toDate: $('#txtUserTaskTo').val(), numberOfrecords: '10000', userName: $("#ddlUsersUserTask  option:selected").text()
        }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: LoadFileTask,
        error: OnErrorCall

    });
}
function LoadFileTask(response) {
    window.location = window.location.origin + '/Download/UserTasks.xlsx';
}
function PrevClickExpense() {
    expensePageNumber = expensePageNumber - 1;
    GetExpenseReport();
    return false;
}

function NextClickExpense() {
    expensePageNumber = expensePageNumber + 1;
    GetExpenseReport();
    return false;
}
function PrevClickEnquiry() {
    enquiryPageNumber = enquiryPageNumber - 1;
    RefreshEnquiries();
    return false;
}

function NextClickEnquiry() {
    enquiryPageNumber = enquiryPageNumber + 1;
    RefreshEnquiries();
    return false;
}
function PrevClickUserTask() {
    userTaskPageNumber = userTaskPageNumber - 1;
    RefreshTasks();
    return false;
}

function NextClickUserTask() {
    userTaskPageNumber = userTaskPageNumber + 1;
    RefreshTasks();
    return false;
}