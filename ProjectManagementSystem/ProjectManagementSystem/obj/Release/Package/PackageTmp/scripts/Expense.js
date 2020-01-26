var url = "ProjectService.asmx";
var otherTaskUrl = "OtherActivityService.asmx";

var projectExpPageNumber = 1;
var otherTaskExpPageNumber = 1;
var projectIncomePageNumber = 1;
var editExpenseId = 0;
$(document).ready(function () {
    $("#btnIncomeExportToExcel").unbind().bind('click', function () { DownloadProjectIncomeReport() });
    GetAllProjectNamesAndId();
    GetAllExpenseCategory();
    GetAllOtherTaskNamesAndId();
    $('#ddlProjects').change(function () {
        $('#divOtherProjectTask').css('display', 'none');
        if ($('#ddlProjects').val() != "-1") {
            GetAllPhasesById($('#ddlProjects').val());
        }
        else {
            $('#ddlPhases').html('<option value="-1">SELECT</option>');
        }
    });

    $('#ddlProjectIncome').change(function () {
        //$('#divOtherProjectTask').css('display', 'none');
        if ($('#ddlProjectIncome').val() != "-1") {
            GetAllPhasesForIncomeById($('#ddlProjectIncome').val());
        }
        else {
            $('#ddlPhaseIncome').html('<option value="-1">SELECT</option>');
        }
    });

    $("#liPersonal ,#li1 , #li4 ,#li5").unbind().bind('click', function () { ResetToAddExpense(); });
    //GetAllProjectExpenses();
    //GetAllOtherTaskExpenses();
});

function SaveProjectExpense() {
    var error = '';
    if ($('#ddlProjects').val() == '-1') {
        error += 'Please select Project.\n';
    }

    if ($('#ddlExpenseCategory').val() == '-1') {
        error += 'Please select Expense Category.\n';
    }

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
    if ($('#ddlProjectIncome').val() == '-1') {
        error += 'Please select Project.\n';
    }

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
        url: url + "/SaveProjectIncome",
        data: JSON.stringify({
            projectId: $('#ddlProjectIncome').val(), phase: $('#ddlPhaseIncome').val(),
            incomeDate: $('#txtIncomeDate').val(), comments: $('#txtIncomeComments').val(), name: $('#txtIncomeName').val(), amount: $('#txtIncomeAmount').val()
        }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: SaveIncomeSuccsess,
        error: OnErrorCall
    });
}

function SaveOtherExpense() {
    var error = '';
    if ($('#ddlOtherActivity').val() == '-1') {
        error += 'Please select one Activity.\n';
    }

    if ($('#ddlOtherExpenseCategory').val() == '-1') {
        error += 'Please select Expense Category.\n';
    }

    if ($('#txtOtherExpName').val() == '') {
        error += 'Name is mandatory.\n';
    }

    if ($('#txtAmountOther').val() == '') {
        error += 'Amount is mandatory.\n';
    }

    if ($('#txtExpdateOther').val() == '') {
        error += 'Please select Expense Date.\n';
    }

    if (error != '') {
        alert(error);
        return false;
    }
    else {
        AddOtherExpense();
        return false;
    }
}

function AddProjectExpense() {
    $.ajax({
        type: "POST",
        url: url + "/SaveProjectExpense",
        data: JSON.stringify({ projectId: $('#ddlProjects').val(), phase: $('#ddlPhases').val(), category: $('#ddlExpenseCategory').val(), expenseDate: $('#txtExpDate').val(), comments: $('#txtComments').val(), name: $('#txtExpName').val(), amount: $('#txtAmount').val(), expenseID: editExpenseId }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: SaveExpeSuccsess,
        error: OnErrorCall
    });
    
}

function AddOtherExpense() {
    $.ajax({
        type: "POST",
        url: otherTaskUrl + "/SaveOtherTaskExpense",
        data: JSON.stringify({ otherTaskId: $('#ddlOtherActivity').val(), category: $('#ddlOtherExpenseCategory').val(), expenseDate: $('#txtExpdateOther').val(), comments: $('#txtCommentsOtherExp').val(), name: $('#txtOtherExpName').val(), amount: $('#txtAmountOther').val() }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: SaveOtherExpSuccsess,
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
        url: url + "/GetAllProjectExpenses",
        data: JSON.stringify({ pageNumber: projectExpPageNumber, searchTerm: $('#txtEnqSeacrh').val(), category: $('#ddlCategorySearch').val() }),
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
        trObj += ' <td class=" ">' + jsonResponse[i].ProjectName + '</td>';
        trObj += '<td class=" ">' + jsonResponse[i].PhaseName + '</td>';
        trObj += '<td class=" ">' + jsonResponse[i].Amount + '</td>';
        trObj += '<td class=" ">' + jsonResponse[i].Category + '</td>';
        trObj += '<td class=" " style="text-align: center"><div  class="btn btn-warning small_sp" style="margin-right: 5px;" onclick="return EditExpense(\' ' + jsonResponse[i].ProjectExpenseId + '\',\' ' + jsonResponse[i].ExpenseName + '\',\' ' + jsonResponse[i].Category + '\',\' ' + jsonResponse[i].ProjectName + '\',\' ' + jsonResponse[i].PhaseName + '\',\' ' + jsonResponse[i].Amount + '\',\' ' + jsonResponse[i].ExpenseDate + '\',\' ' + jsonResponse[i].Comments + '\');"><i class="fa fa-pencil"></i></div>';
        trObj += '<button title="Delete" onclick="return ShowDeletePopUP(\' ' + jsonResponse[i].ProjectExpenseId + '\',\' ' + jsonResponse[i].ExpenseName + '\');" class="btn btn-danger small_sp" style="margin-right: 5px;"><i class="fa fa-trash-o"></i></button>';
        trObj += ' </td></tr>';
        trObj += '';

    }
    $('#tdInnerRowProject').html(trObj);
    CustomPagination(totalCount, projectExpPageNumber, 'divCountProject', 'divPaginationProject');
}
function ResetToAddExpense() {
    $("#liPersonal").find('a').text("Add Expense");
    editExpenseId = 0;
    $("#txtExpName").val('');
    $("#txtAmount").val('');
    $("#txtComments").val('');
    $("#txtExpDate").val('');
    $('#ddlExpenseCategory option').removeAttr('selected');
    $('#ddlProjects option').removeAttr('selected');
    $('#ddlPhases option').removeAttr('selected');
}

function EditExpense(id, ExpenseName, Category, ProjectName, Phase, AMt, ExpenseDate, Comments) {
    editExpenseId = id;
    $("#tab1").addClass("active");
    $("#liPersonal").find('a').text("Edit Expense");
    $("#tab2").removeClass("active");
    //$('.tab-pane a[href="#' + tab1 + '"]').tab('show');
    $('#liPersonal').tab('show');
    $("#txtExpName").val($.trim(ExpenseName));
    $("#txtAmount").val($.trim(AMt));
    $("#txtComments").val($.trim(Comments));
    
    var expDate = $.trim(ExpenseDate);
    var dateParts = expDate.split("/");
    $("#txtExpDate").val($.trim(expDate));
    var dateObject = new Date(dateParts[2], dateParts[1] - 1, dateParts[0]); 
    $("#txtExpDate").datepicker('setDate', dateObject);
    $("#ddlExpenseCategory option").each(function () {
        if ($.trim($(this).text()) == $.trim(Category)) {
            $(this).attr('selected', 'selected');
            $("#ddlExpenseCategory").val($(this).val()).trigger('change');
        }
    });
    $("#ddlProjects option").each(function () {
        if ($.trim($(this).text()) == $.trim(ProjectName)) {
            $(this).attr('selected', 'selected');
            $("#ddlProjects").val($(this).val()).trigger('change');
        }
    });
    $("#ddlPhases option").each(function () {
        if ($.trim($(this).text()) == $.trim(Phase)) {
            $(this).attr('selected', 'selected');
            $("#ddlPhases").val($(this).val()).trigger('change');
        }
    });
}
function GetAllProjectIncome() {
    $.ajax({
        type: "POST",
        url: url + "/GetAllProjectIncome",
        data: JSON.stringify({ pageNumber: projectIncomePageNumber, searchTerm: $('#txtIncomeProjectNameSearch').val(), fromDate: $('#dateFromIncome').val(), toDate: $('#dateToIncome').val() }),
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
        trObj += ' <td class=" ">' + jsonResponse[i].ProjectName + '</td>';
        trObj += '<td class=" ">' + jsonResponse[i].PhaseName + '</td>';
        trObj += '<td class=" ">' + jsonResponse[i].Amount + '</td>';
        trObj += '<td class=" " style="text-align: center">';
        trObj += '<button title="Delete" onclick="return ShowDeletePopUPIncome(\' ' + jsonResponse[i].ProjectIncomeId + '\',\' ' + jsonResponse[i].IncomeName + '\');" class="btn btn-danger small_sp" style="margin-right: 5px;"><i class="fa fa-trash-o"></i></button>';
        //trObj += '<button title="Delete" onclick="return false" class="btn btn-danger small_sp" style="margin-right: 5px;"><i class="fa fa-trash-o"></i></button>';
        trObj += ' </td></tr>';
        trObj += '';

    }
    $('#tdInnerRowIncome').html(trObj);
    CustomPaginationClick(totalCount, projectExpPageNumber, 'divCountIncome', 'divPaginationProject','PrevClickIncome', 'NextClickIncome');
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

function ShowDeletePopUPIncome(incomeId, incomeName) {
    var bodyContent = '<div class="form-group"><label>Name:</label><label style="margin-left:20px;">' + incomeName + '</label></div>'
    var footerContent = '<a id="newCatSubmit"   class="btn btn-block btn-success pull-right" style="width: 20%;" data-toggle="tab">Delete</a><a class="btn btn-block btn-warning pull-right classEdit"  style="width: 20%; margin-top: 0px; margin-right: 2%;">Cancel</a>';
    $("#MyModal").find('.modal-content').find('.modal-header').find('h2').text('Confirm Delete Income?');
    $("#MyModal").find('.modal-content').find('.modal-body').html(bodyContent);
    $("#MyModal").find('.modal-content').find('.modal-footer').html(footerContent);
    $("#MyModal").find('.modal-content').find('.modal-footer').find('.btn-warning').unbind().bind('click', function () { $("#MyModal").hide(); })
    $("#MyModal").find('.modal-content').find('.modal-header').find('span').unbind().bind('click', function () { $("#MyModal").hide(); })
    $("#newCatSubmit").unbind().bind('click', function () { DeleteIncome(incomeId) })
    $("#MyModal").show();
    return false;
}
function DeleteExpense(expenseId) {
    $("#MyModal").hide();
    $.ajax({
        type: "POST",
        url: url + "/DeleteProjectExpense",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ "expenseId": expenseId }),
        success: function (result) {
            if (result.d ==  'true') {
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
        url: url + "/DeleteIncome",
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

function GetAllOtherTaskExpenses() {
    $.ajax({
        type: "POST",
        url: otherTaskUrl + "/GetAllOtherTaskExpenses",
        data: JSON.stringify({ pageNumber: otherTaskExpPageNumber, searchTerm: $('#txtOtherTaskSearch').val(), category: $('#ddlOtherTaskCatSearch').val() }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: BindOtherTaskExpenses,
        error: OnErrorCall
    });
}


function BindOtherTaskExpenses(response) {
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
        trObj += '<td class=" ">' + jsonResponse[i].ExpenseName + '</td>';
        trObj += '<td class=" ">' + jsonResponse[i].ExpenseDate + '</td>';
        trObj += ' <td class=" ">' + jsonResponse[i].OtherTaskName + '</td>';
        trObj += '<td class=" ">' + jsonResponse[i].Amount + '</td>';
        trObj += '<td class=" ">' + jsonResponse[i].Category + '</td>';
        trObj += '<td class=" " style="text-align: center">';
        trObj += '<button title="Delete" onclick="return ShowDeletePopUP(\' ' + jsonResponse[i].EnquiryId + '\',\' ' + jsonResponse[i].Name + '\',\' ' + jsonResponse[i].Phone + '\');" class="btn btn-danger small_sp" style="margin-right: 5px;"><i class="fa fa-trash-o"></i></button>';
        trObj += ' </td></tr>';
        trObj += '';

    }
    $('#tdInnerRowOtherTask').html(trObj);
    CustomPagination(totalCount, otherTaskExpPageNumber, 'divCountOtherTask', 'divPaginationOtherTask');
}


function SaveExpeSuccsess(response) {

    if (response.d == true) {

        
        if (editExpenseId == 0) {
            alert('Expense Added Successfully.');
        }
        else {
            ResetToAddExpense();
            alert('Expense Updated Successfully.');
        }
        $("#ddlOtherExpenseCategory").val("-1");
        $("#ddlOtherActivity").val("-1");
        $("#txtOtherExpName").val("");
        $("#txtAmountOther").val("");
        $("#txtExpdateOther").val("");
        $("#txtCommentsOtherExp").val("");

        $('#ddlProjects').val(-1);
        $('#ddlExpenseCategory').val(-1);

        $('#txtExpName').val('');

        $('#txtAmount').val('');

        $('#txtExpDate').val('');


        SearchClick();
        ResetToAddExpense();
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

        $('#ddlProjectIncome').val(-1);

        $('#txtIncomeName').val('');

        $('#txtIncomeAmount').val(-1);

        $('#txtIncomeDate').val('');
        SearchIncomeClick();
    }
    else {
        alert('Error');
    }
}

function SaveOtherExpSuccsess(response) {
    if (response.d == true) {
        alert('Expense Added Successfully.');
        $("#ddlExpenseCategory").val("-1");
        $("#ddlProjects").val("-1");
        $("#ddlPhases").val("-1");
        $("#txtExpName").val("");
        $("#txtAmount").val("");
        $("#txtExpDate").val("");
        $("#txtComments").val("");
        SearchClick();
    }
    else {
        alert('Error');
    }
}

function GetAllProjectNamesAndId() {
    $.ajax({
        type: "POST",
        url: url + "/GetAllProjectNamesAndId",
        data: '',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: BindProjects,
        error: OnErrorCall
    });
}

function GetAllExpenseCategory() {
    $.ajax({
        type: "POST",
        url: url + "/GetAllExpenseCategory",
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
    projectObj += "<option value='-1'>SELECT</option>";
    $.each(jsonResponse, function (key, expense) {
        projectObj += "<option value='" + expense.CategoryId + "'>" + expense.CategoryName + "</option>";
    });
    $('#ddlExpenseCategory').html(projectObj);
    $('#ddlCategorySearch').html(projectObj);
    $('#ddlOtherExpenseCategory').html(projectObj);
    
}

function GetAllOtherTaskNamesAndId() {
    $.ajax({
        type: "POST",
        url: otherTaskUrl + "/GetAllOtherTaskNamesAndId",
        data: '',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: BindOtherTasks,
        error: OnErrorCall
    });
}


function BindOtherTasks(response) {
    var jsonResponse = JSON.parse(response.d);
    var projectObj = '';
    projectObj += "<option value='-1'>SELECT</option>";
    $.each(jsonResponse, function (key, expense) {
        projectObj += "<option value='" + expense.OtherTaskId + "'>" + expense.TaskName + "</option>";
    });
    $('#ddlOtherActivity').html(projectObj);
}



function GetAllPhasesForIncomeById(projectId) {
    $.ajax({
        type: "POST",
        url: url + "/GetAllProjectPhasesById",
        data: JSON.stringify({ projectId: projectId }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: BindPhasesForIncome,
        error: OnErrorCall
    });
}
function GetAllPhasesById(projectId) {
    $.ajax({
        type: "POST",
        url: url + "/GetAllProjectPhasesById",
        data: JSON.stringify({ projectId: projectId }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: BindPhases,
        error: OnErrorCall
    });
}

function BindProjects(response) {
    var jsonResponse = JSON.parse(response.d);
    var projectObj = '';
    projectObj += "<option value='-1'>SELECT</option>";
    $.each(jsonResponse, function (key, project) {
        projectObj += "<option value='" + project.ProjectId + "'>" + project.ProjectName + "</option>";
    });
    $('#ddlProjects').html(projectObj);
    $('#ddlPhases').html("<option value='-1'>SELECT</option>");
    $('#ddlTasks').html("<option value='-1'>SELECT</option>");

    $('#ddlProjectIncome').html(projectObj);
    $('#ddlPhaseIncome').html("<option value='-1'>SELECT</option>");
}

function BindPhases(response) {
    var jsonResponse = JSON.parse(response.d);
    var phaseHtml = '';
    phaseHtml += "<option value='-1'>SELECT</option>";
    $.each(jsonResponse, function (key, phase) {
        phaseHtml += "<option value='" + phase.ProjectPhaseId + "'>" + phase.Name + "</option>";
    });
    $('#ddlPhases').html(phaseHtml);
    $('#ddlTasks').html("<option value='-1'>SELECT</option>");
}

function BindPhasesForIncome(response) {
    var jsonResponse = JSON.parse(response.d);
    var phaseHtml = '';
    phaseHtml += "<option value='-1'>SELECT</option>";
    $.each(jsonResponse, function (key, phase) {
        phaseHtml += "<option value='" + phase.ProjectPhaseId + "'>" + phase.Name + "</option>";
    });
    $('#ddlPhaseIncome').html(phaseHtml);
}

function ChangeType(val) {
    if (val == 'project') {
        $('#divProjectTask').css('display', 'block');
        $('#divOtherTask').css('display', 'none');
    }
    else {
        $('#divProjectTask').css('display', 'none');
        $('#divOtherTask').css('display', 'block');
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
function DownloadProjectIncomeReport() {
    $.ajax({
        type: "POST",
        url: url + "/GetProjectIncomeReport",
        data: JSON.stringify({ pageNumber: 1, searchTerm: $('#txtIncomeProjectNameSearch').val(), fromDate: $('#dateFromIncome').val(), toDate: $('#dateToIncome').val()}),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: LoadFile,
        error: OnErrorCall

    });
}
function LoadFile(response) {
    window.location = window.location.origin + '/Download/ProjectIncome.xlsx';
}

