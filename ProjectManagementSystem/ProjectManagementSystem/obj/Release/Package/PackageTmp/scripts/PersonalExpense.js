var url = "ExpenseService.asmx";

var projectExpPageNumber = 1;
var otherTaskExpPageNumber = 1;
var projectIncomePageNumber = 1;

$(document).ready(function () {
    //GetAllProjectExpenses();
    //GetAllProjectIncome();
    $("#btnPersonalIncomeExport").unbind().bind('click', function () { DownloadPersonalIncomeReport() });
    $("#btnPersonalExpenseExport").unbind().bind('click', function () { DownloadPersonalExpenseReport() });
});

function GetAllOtherTaskExpenses() {

}

function SaveProjectExpense() {
    var error = '';

    if ($('#txtExpName').val() == '') {
        error += 'Name is mandatory.\n';
    }

    if ($('#txtAmount').val() == '') {
        error += 'Amount is mandatory.\n';
    }

    if ($('#txtExpDate').val() == '') {
        error += 'Please select Expense Date.\n';
    }

    if (error != '') {
        alert(error);
        return false;
    }
    else {
        AddProjectExpense();
        return false;
    }
}

function SaveProjectIncome() {
    var error = '';
    if ($('#txtIncomeName').val() == '') {
        error += 'Name is mandatory.\n';
    }

    if ($('#txtIncomeAmount').val() == '') {
        error += 'Amount is mandatory.\n';
    }

    if ($('#txtIncomeDate').val() == '') {
        error += 'Please select Income Date.\n';
    }

    if (error != '') {
        alert(error);
        return false;
    }
    else {
        AddProjectIncome();
        return false;
    }
}

function AddProjectIncome() {
    $.ajax({
        type: "POST",
        url: url + "/SavePersonalIncome",
        data: JSON.stringify({
            incomeDate: $('#txtIncomeDate').val(), comments: $('#txtIncomeComments').val(), name: $('#txtIncomeName').val(), amount: $('#txtIncomeAmount').val()
        }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: SaveIncomeSuccsess,
        error: OnErrorCall
    });
}

function AddProjectExpense() {
    $.ajax({
        type: "POST",
        url: url + "/SavePersonalExpense",
        data: JSON.stringify({ expenseDate: $('#txtExpDate').val(), comments: $('#txtComments').val(), name: $('#txtExpName').val(), amount: $('#txtAmount').val() }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: SaveExpeSuccsess,
        error: OnErrorCall
    });
}

function SearchClick() {
    projectExpPageNumber = 1;
    GetAllProjectExpenses();
    return false;
}

function SearchIncomeClick() {
    projectIncomePageNumber = 1;
    GetAllProjectIncome();
    return false;
}

function GetAllProjectExpenses() {
    $.ajax({
        type: "POST",
        url: url + "/GetAllPersonalExpenses",
        data: JSON.stringify({ pageNumber: projectExpPageNumber, searchTerm: $('#txtEnqSeacrh').val(), fromDate: $('#dateFromPersonalExp').val(), toDate: $('#dateToPersonalExp').val() }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: BindProjectExpenses,
        error: OnErrorCall
    });
}

function BindProjectExpenses(response) {
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
        trObj += '<td class=" ">' + jsonResponse[i].Amount + '</td>';
        trObj += '<td class=" " style="text-align: center">';
        trObj += '<button title="Delete" onclick="return ShowDeletePopUP(\' ' + jsonResponse[i].ProjectExpenseId + '\',\' ' + jsonResponse[i].ExpenseName + '\');" class="btn btn-danger small_sp" style="margin-right: 5px;"><i class="fa fa-trash-o"></i></button>';
        trObj += ' </td></tr>';
        trObj += '';

    }
    $('#tdInnerRowProject').html(trObj);
    CustomPagination(totalCount, projectExpPageNumber, 'divCountProject', 'divPaginationProject');
}

function GetAllProjectIncome() {
    $.ajax({
        type: "POST",
        url: url + "/GetAllPersonalIncome",
        data: JSON.stringify({ pageNumber: projectIncomePageNumber, searchTerm: $('#txtIncomeProjectNameSearch').val(), fromDate: $('#dateFromPersonalIncome').val(), toDate: $('#dateToPersonalIncome').val() }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: BindProjectIncome,
        error: OnErrorCall
    });
}

function BindProjectIncome(response) {
    var jsonResponse = JSON.parse(response.d);
    var className = '';
    var trObj = '';
    var totalCount = 0;
    for (var i = 0; i < jsonResponse.length; i++) {
        $('#divTotalIncome').html('Total Income: <b>' + jsonResponse[i].TotalIncome + '</b>');
        if (i % 2 == 0) {
            className = 'odd';
        }
        else {
            className = 'even'
        }

        totalCount = jsonResponse[i].TotalCount;

        trObj += '<tr class="' + className + '">';
        trObj += '<td class=" ">' + jsonResponse[i].IncomeName + '</td>';
        trObj += '<td class=" ">' + jsonResponse[i].IncomeDate + '</td>';
        trObj += '<td class=" ">' + jsonResponse[i].Amount + '</td>';
        trObj += '<td class=" " style="text-align: center">';
        //trObj += '<button title="Delete" onclick="return ShowDeletePopUP(\' ' + jsonResponse[i].ProjectIncomeId + '\',\' ' + jsonResponse[i].IncomeName + '\');" class="btn btn-danger small_sp" style="margin-right: 5px;"><i class="fa fa-trash-o"></i></button>';
        trObj += '<button title="Delete" onclick="return ShowDeletePopUPIncome(\' ' + jsonResponse[i].ProjectIncomeId + '\',\' ' + jsonResponse[i].IncomeName + '\');" class="btn btn-danger small_sp" style="margin-right: 5px;"><i class="fa fa-trash-o"></i></button>';
        trObj += ' </td></tr>';
        trObj += '';

    }
    $('#tdInnerRowIncome').html(trObj);
    CustomPaginationClick(totalCount, projectExpPageNumber, 'divCountIncome', 'divPaginationProject', 'PrevClickIncome', 'NextClickIncome');
}

function ShowDeletePopUP(expId, expName) {
    var bodyContent = '<div class="form-group"><label>Name:</label><label style="margin-left:20px;">' + expName + '</label></div>'
    var footerContent = '<a id="newCatSubmit"   class="btn btn-block btn-success pull-right" style="width: 20%;" data-toggle="tab">Delete</a><a class="btn btn-block btn-warning pull-right classEdit"  style="width: 20%; margin-top: 0px; margin-right: 2%;">Cancel</a>';
    $("#MyModal").find('.modal-content').find('.modal-header').find('h2').text('Confirm Delete Expense?');
    $("#MyModal").find('.modal-content').find('.modal-body').html(bodyContent);
    $("#MyModal").find('.modal-content').find('.modal-footer').html(footerContent);
    $("#MyModal").find('.modal-content').find('.modal-footer').find('.btn-warning').unbind().bind('click', function () { $("#MyModal").hide(); })
    $("#MyModal").find('.modal-content').find('.modal-header').find('span').unbind().bind('click', function () { $("#MyModal").hide(); })
    $("#newCatSubmit").unbind().bind('click', function () { DeleteExpense(expId) })
    $("#MyModal").show();
    return false;
}
function ShowDeletePopUPIncome(incomeID, incomeName) {
    var bodyContent = '<div class="form-group"><label>Name:</label><label style="margin-left:20px;">' + incomeName + '</label></div>'
    var footerContent = '<a id="newCatSubmit"   class="btn btn-block btn-success pull-right" style="width: 20%;" data-toggle="tab">Delete</a><a class="btn btn-block btn-warning pull-right classEdit"  style="width: 20%; margin-top: 0px; margin-right: 2%;">Cancel</a>';
    $("#MyModal").find('.modal-content').find('.modal-header').find('h2').text('Confirm Delete Income?');
    $("#MyModal").find('.modal-content').find('.modal-body').html(bodyContent);
    $("#MyModal").find('.modal-content').find('.modal-footer').html(footerContent);
    $("#MyModal").find('.modal-content').find('.modal-footer').find('.btn-warning').unbind().bind('click', function () { $("#MyModal").hide(); })
    $("#MyModal").find('.modal-content').find('.modal-header').find('span').unbind().bind('click', function () { $("#MyModal").hide(); })
    $("#newCatSubmit").unbind().bind('click', function () { DeleteIncome(incomeID) })
    $("#MyModal").show();
    return false;
}

function DeleteExpense(expenseId) {
    $("#MyModal").hide();
    $.ajax({
        type: "POST",
        url: url + "/DeletePersonalExpense",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ "expenseId": expenseId }),
        success: function (result) {
            if (result.d == 'true') {
                projectExpPageNumber = 1;
                SearchClick();
                alert('Delete Success');
            }
            else {
                alert("Something went wrong. Please try agin later");
            }

        }
    });
}

function DeleteIncome(incomeId) {
    $("#MyModal").hide();
    $.ajax({
        type: "POST",
        url: url + "/DeletePersonalIncome",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ "incomeId": incomeId }),
        success: function (result) {
            if (result.d == 'true') {
                projectIncomePageNumber = 1;
                SearchIncomeClick();
                alert('Delete Success');
            }
            else {
                alert("Something went wrong. Please try agin later");
            }

        }
    });
}


function SaveExpeSuccsess(response) {

    if (response.d == true) {
        alert('Expense Added Successfully.');
        $("#txtExpName").val("");
        $("#txtAmount").val("");
        $("#txtExpDate").val("");

        SearchClick();
    }
    else {
        alert('Error');
    }
}

function SaveIncomeSuccsess(response) {

    if (response.d == true) {
        alert('Income Added Successfully.');
        $("#ddlProjectIncome").val("-1");
        $("#ddlPhaseIncome").val("-1");
        $("#txtIncomeAmount").val("");
        $("#txtIncomeDate").val("");
        $("#txtIncomeComments").val("");
        $("#txtIncomeName").val("");

        $('#txtExpName').val('');
        $('#txtAmount').val('');
        $('#txtExpDate').val('');

        SearchIncomeClick();
    }
    else {
        alert('Error');
    }
}

function ShowAddCategoryPopup() {
    var bodyContent = '<div class="form-group"><label>Name:</label><input id="txtCatName" /> </div>'
    var footerContent = '<a id="newCatSubmit"   class="btn btn-block btn-success pull-right" style="width: 20%;" data-toggle="tab">Add</a><a class="btn btn-block btn-warning pull-right classEdit"  style="width: 20%; margin-top: 0px; margin-right: 2%;">Cancel</a>';
    $("#MyModal").find('.modal-content').find('.modal-header').find('h2').text('Add Category');
    $("#MyModal").find('.modal-content').find('.modal-body').html(bodyContent);
    $("#MyModal").find('.modal-content').find('.modal-footer').html(footerContent);
    $("#MyModal").find('.modal-content').find('.modal-footer').find('.btn-warning').unbind().bind('click', function () { $("#MyModal").hide(); })
    $("#MyModal").find('.modal-content').find('.modal-header').find('span').unbind().bind('click', function () { $("#MyModal").hide(); })
    $("#newCatSubmit").unbind().bind('click', function () { AddCategory() })
    $("#MyModal").show();
    return false;
}

function AddCategory() {
    $.ajax({
        type: "POST",
        url: "EnquiryService.asmx/AddCategory",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ "name": $('#txtCatName').val() }),
        success: function (result) {
            if (result.d == true) {
                alert('Added Successfully');
                $("#MyModal").hide();
                GetAllExpenseCategory();
            }
            else {
                alert("Error");
            }

        }
    });
}

function PrevClick() {
    projectExpPageNumber = projectExpPageNumber - 1;
    GetAllProjectExpenses();
    return false;
}

function NextClick() {
    projectExpPageNumber = projectExpPageNumber + 1;
    GetAllProjectExpenses();
    return false;
}

function PrevClickIncome() {
    projectIncomePageNumber = projectIncomePageNumber - 1;
    GetAllProjectIncome();
    return false;
}

function NextClickIncome() {
    projectIncomePageNumber = projectIncomePageNumber + 1;
    GetAllProjectIncome();
    return false;
}


function DownloadPersonalIncomeReport() {
    $.ajax({
        type: "POST",
        url: url + "/GetPersonalIncomeReport",
        data: JSON.stringify({ pageNumber: 1, searchTerm: $('#txtIncomeProjectNameSearch').val(), fromDate: $('#dateFromPersonalIncome').val(), toDate: $('#dateToPersonalIncome').val() }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: LoadFile,
        error: OnErrorCall

    });
}
function LoadFile(response) {
    window.location = window.location.origin + '/Download/PersonalIncome.xlsx';
}
function DownloadPersonalExpenseReport() {
    $.ajax({
        type: "POST",
        url: url + "/GetPersonalExpenseReport",
        data: JSON.stringify({ pageNumber: 1, searchTerm: $('#txtEnqSeacrh').val(), fromDate: $('#dateFromPersonalExp').val(), toDate: $('#dateToPersonalExp').val() }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: LoadFileExpense,
        error: OnErrorCall

    });
}
function LoadFileExpense(response) {
    window.location = window.location.origin + '/Download/PersonalExpense.xlsx';
}
