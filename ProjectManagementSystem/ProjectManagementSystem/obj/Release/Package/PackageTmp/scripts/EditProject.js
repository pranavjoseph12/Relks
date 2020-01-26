var url = "ProjectService.asmx";
var userServiceUrl = "UserService.asmx";

$(document).ready(function () {
    GetAllProjectDetails();
    GetAllUsersForAssignment();
});

function ChangeTab(tabName) {
    if (tabName == '1') {
        $('#liPersonal').addClass('active');
        $('#liCourse').removeClass('active');
        $('#li1').removeClass('active');
        $('#liUserProjectAccess').removeClass('active');
    }
    else if (tabName == '2') {
        $('#liPersonal').removeClass('active');
        $('#li1').addClass('active');
        $('#liCourse').removeClass('active');
        $('#liUserProjectAccess').removeClass('active');

    }
    else if (tabName == '4') {
        $('#liPersonal').removeClass('active');
        $('#li1').removeClass('active');
        $('#liUserProjectAccess').addClass('active');
        $('#liCourse').removeClass('active');
    }
    else if (tabName == '5') {
        $('#liPersonal').removeClass('active');
        $('#li1').removeClass('active');
        $('#liCourse').addClass('active');
        $('#liUserProjectAccess').removeClass('active');
    }
    return false;
}

function GetAllUsersForAssignment() {
    $.ajax({
        type: "POST",
        url: userServiceUrl + "/GetAllUsers",
        data: '',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: BindUsers,
        error: OnErrorCall
    });
    return false;
}

function BindUsers(response) {
    var jsonResponse = JSON.parse(response.d);
    var className = '';
    var trObj = '';
    var userWithAccsess = $('#ContentPlaceHolder1_hdnUsersWithAccess').val().split(',');
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
        trObj += '<td class=" ">' + jsonResponse[i].Email + '</td>';
        trObj += ' <td class=" ">' + jsonResponse[i].Phone + '</td>';
        if (userWithAccsess.indexOf(jsonResponse[i].UserId.toString()) > -1) {
            trObj += '<td class=" "><input id="' + jsonResponse[i].UserId + '" checked="checked" type="checkbox" /></td>';
        }
        else {
            trObj += '<td class=" "><input id="' + jsonResponse[i].UserId + '"  type="checkbox" /></td>';
        }
        trObj += '</tr>';

    }
    $('#tdInnerProjectUserAccess').html(trObj);
}

function GetAllProjectDetails() {
    $.ajax({
        type: "POST",
        url: url + "/GetCompleteProjectDetailWithTasks",
        data: JSON.stringify({ projectId: GetParameterValues('ProjectID') }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccessCall,
        error: OnErrorCall
    });
    return false;
}

function MarkAsCompleted() {
    $.ajax({
        type: "POST",
        url: url + "/MarkProjectAsCompleted",
        data: JSON.stringify({ projectId: GetParameterValues('ProjectID') }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.d === 'true') {
                //pageNumber = 1;
                alert('Project Marked as completed');
            }
            else {
                alert("Something went wrong. Please try again.");
            }
            
        },
        error: OnErrorCall
    });
    return false;
}


function OnSuccessCall(response) {
    var jsonResponse = JSON.parse(response.d);
    projectModel = jsonResponse;
    //var projectObj = projectModel;
    $('#txtAddProjectName').val(jsonResponse.ProjectName);
    $('#txtPrimaryContactName').val(jsonResponse.PrimaryContactName);
    $('#txtPrimaryContactNumber').val(jsonResponse.PrimaryNumber);
    $('textarea#txtAddAddress').val(jsonResponse.Address);
    $('#txtAddPinCode').val(jsonResponse.PinCode);
    //if (jsonResponse.IsHidden === true) {
    //    $('#rbtnHdnYes').attr('checked', true);
    //}
    //else {
    //    $('#rbtnHdnNo').attr('checked', true);
    //}
    
    $('#txtOtherContact1Name').val(jsonResponse.Contact1Name);
    $('#txtOtherContact1Number').val(jsonResponse.Contact1number);
    $('#txtOtherContact2Name').val(jsonResponse.Contact2Name);
    $('#txtOtherContact2Number').val(jsonResponse.Contact2Number);
    $('#txtOtherContact3Name').val(jsonResponse.Contact3Name);
    $('#txtOtherContact3Number').val(jsonResponse.Contact3Number);
    $('#txtAddProjectStartDate').val(jsonResponse.EstimatedStartDate);
    $('#txtEndDate').val(jsonResponse.EstimatedEndDate);
    $('#txtEstimate').val('');
    $('textarea#txtComments').val(jsonResponse.Comments);
    $("#txtAddProjectStartDate").datetimepicker({ format: 'YYYY-MM-DD HH:mm' });
    $("#txtEndDate").datetimepicker({ format: 'YYYY-MM-DD HH:mm' });
    BindPhaseAndTasks(jsonResponse);
}

function BindPhaseAndTasks(project) {
    var i = 1;
    $.each(project.ProjectPhases, function (key, phaseObj) {
        var manDivHtmlStart = '<div style="margin-top:35px;"><div style="border: 1px solid #f4f4f4" class="box-header with-border">';
        var manDivHtmlEnd = '</div></div>';
        var phaseNumberDiv = '<div class=""><section style="padding: 0px;" class="content-header"><h1 id="h1PhaseId' + phaseObj.PhaseId + '" style="color: #195592;">' + phaseObj.PhaseName + '</h1></section></div>';
        var divPhaseHeaderDiv = '<div id="divPhaseHeaderId' + phaseObj.PhaseId + '" style="font-size: 18px; color: #195592;" class="col-md-12 box-header with-border">';
        divPhaseHeaderDiv += phaseObj.PhaseName + ' ( ' + phaseObj.StartDate + ' - ' + phaseObj.EndDate + ' ) ';
        divPhaseHeaderDiv += '<button onclick=\'return EditPhasePopup("' + phaseObj.PhaseName + '","' + phaseObj.StartDate + '","' + phaseObj.EndDate + '","' + phaseObj.PhaseId + '","' + phaseObj.Description +'")\' ';
        divPhaseHeaderDiv += ' class="btn btn-warning small_sp" style="margin-right: 5px;"><i class="fa fa-pencil"></i></button>';
        divPhaseHeaderDiv += '<button onclick="addTaskSelectedPhaseId = ' + phaseObj.PhaseId + '; $(\'#ModalAddTask\').show();return false;" class="btn btn-block btn-success pull-right" style="width: 100px; height: 25px; padding: 0px;">';
        divPhaseHeaderDiv += '<i class="fa fa-plus" style="margin-right: 10px;"></i>';
        divPhaseHeaderDiv += 'Add Task </button> </div>';
        var tableTasksHeader = '<table class="table table-bordered table-striped dataTable">';
        tableTasksHeader += '<thead><tr role="row">';
        tableTasksHeader += '<th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="Browser: activate to sort column ascending">Task Name</th>';
        tableTasksHeader += '<th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="Platform(s): activate to sort column ascending">Description</th>';
        tableTasksHeader += '<th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="Engine version: activate to sort column ascending">Start Date</th>';
        tableTasksHeader += '<th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="CSS grade: activate to sort column ascending">End Date</th>';
        tableTasksHeader += '<th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="CSS grade: activate to sort column ascending">&nbsp;</th>';
        tableTasksHeader += '</tr></thead><tbody id="tdInnerRowPhase' + phaseObj.PhaseId + '" role="alert" aria-live="polite" aria-relevant="all">'
        tableTasksHeader += '</tbody></table>';
        $('#divAddProjectPhase').html($('#divAddProjectPhase').html() + manDivHtmlStart + phaseNumberDiv + divPhaseHeaderDiv + tableTasksHeader + manDivHtmlEnd);
    });
    BindTasks(project);
}
function BindTasks(project) {
    $.each(project.ProjectPhases, function (key, phaseObj) {
        $.each(phaseObj.ProjectTasks, function (key1, taskObj) {
            var tableTaskContent = '<tr class="odd"><td class=" ">' + taskObj.TaskName + '</td><td class=" ">' + taskObj.Description + '</td><td class=" ">' + taskObj.StartDate + '</td>';
            tableTaskContent += '<td class=" ">' + taskObj.EndDate + '</td><td class=" " style="text-align: center">';
            tableTaskContent += '<button onclick=\'return EditTaskPopup("' + taskObj.ProjectPhaseId + '","' + taskObj.ProjectTaskId + '","' + taskObj.TaskName + '","' + taskObj.StartDate + '","' + taskObj.EndDate + '","' + taskObj.Description + '")\' ';
            tableTaskContent += 'class="btn btn-warning small_sp" style="margin-right: 5px;"><i class="fa fa-pencil"></i></button>';
            tableTaskContent += '<button onclick=\'return ShowTaskDeletePopUP("' + taskObj.ProjectTaskId + '","' + taskObj.TaskName + '")\' ;" class="btn btn-danger small_sp" style="margin-right: 5px;"><i class="fa fa-trash-o"></i></button>';
            tableTaskContent += '</td></tr>';
            $('#tdInnerRowPhase' + taskObj.ProjectPhaseId).html($('#tdInnerRowPhase' + taskObj.ProjectPhaseId).html() + tableTaskContent);
        });
    });
}



function GetParameterValues(param) {
    var url = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < url.length; i++) {
        var urlparam = url[i].split('=');
        if (urlparam[0] == param) {
            return urlparam[1];
        }
    }
}




function AddNewTaskToPhase() {
    if ($('#txtAddProjectTaskName').val() == '' || $('#txtAddProjectTaskStartDate').val() == '' || $('#txtAddProjectTaskEndDate').val() == '') {
        //alert('Name, Start Date and End Date are mandatory');
        //return false;
    }
    var taskObj = {
        ProjectTaskId: "", ProjectPhaseId: 0, TaskName: "", StartDate: "", EndDate: "", AssignedUserId: 0,
        Description: '', SubTasks: []
    };

    taskObj.ProjectPhaseId = addTaskSelectedPhaseId;
    taskObj.ProjectTaskId = globalTaskId + 1;
    globalTaskId = globalTaskId + 1;
    //taskObj.ProjectTaskId = 'N' + globalTaskId;
    taskObj.TaskName = $('#txtAddProjectTaskName').val();
    taskObj.StartDate = $('#txtAddProjectTaskStartDate').val();
    taskObj.EndDate = $('#txtAddProjectTaskEndDate').val();
    taskObj.Description = $('#txtAddProjectTaskDescription').val();
    var i = 0;
    $.each(projectModel.ProjectPhases, function (key, value) {
        if (value.PhaseId == addTaskSelectedPhaseId) {
            projectModel.ProjectPhases[i].ProjectTasks.push(taskObj);
            var tableTaskContent = '<tr class="odd"><td class=" ">' + taskObj.TaskName + '</td><td class=" ">' + taskObj.Description + '</td><td class=" ">' + taskObj.StartDate + '</td>';
            tableTaskContent += '<td class=" ">' + taskObj.EndDate + '</td><td class=" " style="text-align: center">';
            tableTaskContent += '<button onclick=\'return EditTaskPopup("' + addTaskSelectedPhaseId + '","' + taskObj.ProjectTaskId + '","' + taskObj.TaskName + '","' + taskObj.StartDate + '","' + taskObj.EndDate + '","' + taskObj.Description + '")\' ';
            tableTaskContent += 'class="btn btn-warning small_sp" style="margin-right: 5px;"><i class="fa fa-pencil"></i></button>';
            tableTaskContent += '<button onclick="return false;" class="btn btn-danger small_sp" style="margin-right: 5px;"><i class="fa fa-trash-o"></i></button>';
            tableTaskContent += '</td></tr>';
            $('#tdInnerRowPhase' + addTaskSelectedPhaseId).html($('#tdInnerRowPhase' + addTaskSelectedPhaseId).html() + tableTaskContent);
        }

        i++;
    });

    $('#ModalAddTask').hide();
    return false;
}


function SaveEditProject() {
    var projectObj = projectModel;
    projectObj.ProjectName = $('#txtAddProjectName').val();
    projectObj.PrimaryContactName = $('#txtPrimaryContactName').val();
    projectObj.PrimaryNumber = $('#txtPrimaryContactNumber').val();
    projectObj.Address = $('textarea#txtAddAddress').val();
    projectObj.PinCode = $('#txtAddPinCode').val();
    projectObj.Contact1Name = $('#txtOtherContact1Name').val();
    projectObj.Contact1number = $('#txtOtherContact1Number').val();
    projectObj.Contact2Name = $('#txtOtherContact2Name').val();
    projectObj.Contact2Number = $('#txtOtherContact2Number').val();
    projectObj.Contact3Name = $('#txtOtherContact3Name').val();
    projectObj.Contact3Number = $('#txtOtherContact3Number').val();
    projectObj.StartDate = $('#txtAddProjectStartDate').val();
    projectObj.EndDate = $('#txtEndDate').val();
    projectObj.ProjectEstimate = $('#txtEstimate').val();
    projectObj.IsCompleted = false;
    //if ($('#rbtnHdnYes').prop('checked') == true) {
    //    projectObj.IsHidden = true;
    //}
    //else {
    //    projectObj.IsHidden = false;
    //}
    projectObj.IsHidden = false;
    projectObj.Comments = $('textarea#txtComments').val();
    var error = '';
    if (projectObj.ProjectName == '') {
        error += 'Project name is required. \n';
    }
    if (projectObj.PrimaryContactName == '') {
        error += 'Primary Contact name is required. \n';
    }
    if (projectObj.PrimaryNumber == '') {
        error += 'Primary Contact number is required. \n';
    }
    if (projectObj.StartDate == '') {
        error += 'Project Start Date is required. \n';
    }
    if (projectObj.EndDate == '') {
        error += 'Project End Date is required. \n';
    }



    var taskmissing = false;
    if (projectModel.ProjectPhases.length > 0) {
        $.each(projectModel.ProjectPhases, function (key, phase) {
            if (phase.ProjectTasks.length == 0) {
                taskmissing = true;
            }
        });
    }
    if (taskmissing) {
        error += 'All the phases must contain atleast one task. \n';
    }

    var selectedUsers = '';
    $('#tdInnerProjectUserAccess input:checked').each(function () {
        selectedUsers += $(this).attr('id') + ',';
    });

    if (selectedUsers == '') {
        error += 'Please select atlest one user to proceed.';
    }

    if (error != '') {
        alert(error);
        return false;
    }

    UpdateProject(projectObj, selectedUsers);
    return false;
}

function UpdateProject(projectObj, selectedUsers) {
    $.ajax({
        type: "POST",
        url: url + "/UpdateProject",
        data: JSON.stringify({ requestObj: projectObj, selectedUsers: selectedUsers }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccessUpdate,
        error: OnErrorCall
    });
    return false;
}

function OnSuccessUpdate(response) {
    alert('Updated Successfully.');
    window.location.href = "Projects.aspx";
}

function OnErrorCall(response) {
    alert(response);
    alert(response.status + " " + response.statusText);
}