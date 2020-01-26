var url = "ProjectService.asmx";
var userServiceUrl = "UserService.asmx";

function SaveAddProject() {
    var projectObj = projectModel;
    projectObj.ProjectId = 0;
    //if ($('#rbtnHdnYes').prop('checked') == true) {
    //    projectObj.IsHidden = true;
    //}
    //else {
    //    projectObj.IsHidden = false;
    //}
    projectObj.IsHidden = false;

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
    projectObj.Comments = $('textarea#txtComments').val();
    var error = '';
    if (projectObj.ProjectName.trim() == '') {
        error += 'Project name is required. \n';
    }
    if (projectObj.PrimaryContactName.trim() == '') {
        error += 'Primary Contact name is required. \n';
    }
    if (projectObj.PrimaryNumber.trim() == '') {
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

    if (error == '') {
        var d1 = new Date($('#txtAddProjectStartDate').val());
        d2 = new Date($('#txtEndDate').val());

        if (d1 >= d2) {
            alert('Project End date should be less than start date');
            return false;
        }
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

    AddProject(projectObj, selectedUsers);
    return false;
}
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

function AddProject(projectObj, selectedUsers) {
    $.ajax({
        type: "POST",
        url: url + "/AddProject",
        data: JSON.stringify({ requestObj: projectObj, selectedUsers: selectedUsers }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccessCall,
        error: OnErrorCall
    });
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
        if (jsonResponse[i].UserId == '1') {
            trObj += '<td class=" "><input id="' + jsonResponse[i].UserId + '" disabled="true" checked="checked"  type="checkbox" /></td>';
        }
        else {
            trObj += '<td class=" "><input id="' + jsonResponse[i].UserId + '"  type="checkbox" /></td>';
        }
        trObj += '</tr>';

    }
    $('#tdInnerProjectUserAccess').html(trObj);
}

function OnSuccessCall(response) {
    alert('Project Added Successfully.');
    window.location.href = "Projects.aspx";
}

function OnErrorCall(response) {
    alert(response);
    alert(response.status + " " + response.statusText);
}




function AddNewTaskToPhase() {
    if ($('#txtAddProjectTaskName').val().trim() == '' || $('#txtAddProjectTaskStartDate').val().trim() == '' || $('#txtAddProjectTaskEndDate').val().trim() == '') {
        alert('Name, Start Date and End Date are mandatory');
        return false;
    }
    else {
        var d1 = new Date($('#txtAddProjectTaskStartDate').val());
        //d1.setHours(0, 0, 0, 0);
        d2 = new Date($('#txtAddProjectTaskEndDate').val());
        //d2.setHours(0, 0, 0, 0);

        if (d1 >= d2) {
            alert('Task Start date should be less than End date');
            return false;
        }

        var j = 0;
        $.each(projectModel.ProjectPhases, function (key, value) {
            if (value.PhaseId == addTaskSelectedPhaseId) {
                var d3 = new Date(projectModel.ProjectPhases[j].StartDate);
                d4 = new Date(projectModel.ProjectPhases[j].EndDate);
                if (d1 < d3 || d2 > d4) {
                    alert('Task start and End date should fall within the Phase start and End date');
                    return false;
                }
            }
            j++;
        });

        //i = 0;
        //$.each(projectModel.ProjectPhases, function (key, value) {
        //    if (value.PhaseId == addTaskSelectedPhaseId) {
        //        projectModel.ProjectPhases[i].ProjectTasks.push(taskObj);
        //    }
        //    i++;
        //});


    }

    var taskObj = {
        ProjectTaskId: "", ProjectPhaseId: 0, TaskName: "", StartDate: "", EndDate: "", AssignedUserId: 0,
        Description: '', SubTasks: []
    };

    globalTaskId = globalTaskId + 1;
    taskObj.ProjectPhaseId = addTaskSelectedPhaseId;
    taskObj.ProjectTaskId = globalTaskId + 1;
    //taskObj.ProjectTaskId = 'N' + globalTaskId;
    taskObj.TaskName = $('#txtAddProjectTaskName').val();
    taskObj.StartDate = $('#txtAddProjectTaskStartDate').val();
    taskObj.EndDate = $('#txtAddProjectTaskEndDate').val();
    taskObj.Description = $('#txtAddProjectTaskDescription').val();
    var i = 0;
    $.each(projectModel.ProjectPhases, function (key, value) {
        if (value.PhaseId == addTaskSelectedPhaseId) {
            projectModel.ProjectPhases[i].ProjectTasks.push(taskObj);
        }
        i++;
    });
    //projectModel.ProjectPhases[addTaskSelectedPhaseId].ProjectTasks.push(taskObj);
    var tableTaskContent = '<tr class="odd"><td class=" ">' + taskObj.TaskName + '</td><td class=" ">' + taskObj.Description + '</td><td class=" ">' + taskObj.StartDate + '</td>';
    tableTaskContent += '<td class=" ">' + taskObj.EndDate + '</td><td class=" " style="text-align: center">';
    tableTaskContent += '<button onclick=\'return EditTaskPopup("' + addTaskSelectedPhaseId + '","' + taskObj.ProjectTaskId + '","' + taskObj.TaskName + '","' + taskObj.StartDate + '","' + taskObj.EndDate + '","' + taskObj.Description + '")\' ';
    tableTaskContent += 'class="btn btn-warning small_sp" style="margin-right: 5px;"><i class="fa fa-pencil"></i></button>';
    tableTaskContent += '<button onclick="return false;" class="btn btn-danger small_sp" style="margin-right: 5px;"><i class="fa fa-trash-o"></i></button>';
    tableTaskContent += '</td></tr>';
    $('#tdInnerRowPhase' + addTaskSelectedPhaseId).html($('#tdInnerRowPhase' + addTaskSelectedPhaseId).html() + tableTaskContent);
    $('#ModalAddTask').hide();
    return false;
}