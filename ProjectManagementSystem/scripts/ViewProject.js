var url = "ProjectService.asmx";
var selectedTaskId = 0;
$(document).ready(function () {
    RefreshTasks();
});

function RefreshTasks(val) {
    var id = $('#ContentPlaceHolder1_hdnProjectID').val();
    $.ajax({
        type: "POST",
        url: url + "/GetAllProjectTasksByProjectd",
        data: JSON.stringify({ projectId: id }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccessCall,
        error: OnErrorCall
    });
}

function ExpandTask(taskId) {
    selectedTaskId = taskId;
    if ($('#iExpand' + taskId + '.fa-plus-square').length > 0) {
        $('#tdSubTasks' + taskId).css('display', 'block');
        $('#iExpand' + taskId).removeClass('fa-plus-square');
        $('#iExpand' + taskId).addClass('fa-minus-square');
        GetAllSubTasks(taskId);
    }
    else {
        $('#tdSubTasks' + taskId).hide();
        $('#iExpand' + taskId).addClass('fa-plus-square');
        $('#iExpand' + taskId).removeClass('fa-minus-square');
    }
    
    return false;

}

function GetAllSubTasks(id) {
    $.ajax({
        type: "POST",
        url: url + "/GetAllProjectSubTasksByTaskId",
        data: JSON.stringify({ taskId: id }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: bindSubTasks,
        error: OnErrorCall
    });
}

function bindSubTasks(response) {
    var jsonResponse = JSON.parse(response.d);
    var className = '';
    var trObj = '<tbody class="content-loading" role="alert" aria-live="polite" aria-relevant="all">';
    for (var i = 0; i < jsonResponse.length; i++) {
        if (i % 2 == 0) {
            className = 'odd';
        }
        else {
            className = 'even'
        }
        var subTaskId = jsonResponse[i].ProjectSubTaskId
        var taskId = jsonResponse[i].taskId

        trObj += '<tr style="height:20px;" class="' + className + '">';
        trObj += '<td style="height:20px;" >' + jsonResponse[i].SubTaskName + '</td>';
        trObj += '<td style="height:20px;" >' + jsonResponse[i].ExpectedStartDate + '</td>';
        trObj += ' <td style="height:20px;" >' + jsonResponse[i].ExpectedEndDate + '</td>';
        trObj += ' <td style="height:20px;" >' + jsonResponse[i].AssignedUserName + '</td>';
        if (jsonResponse[i].IsCompleted === true) {
            trObj += ' <td style="height:20px;" >Yes</td>';
        }
        else{
            trObj += ' <td style="height:20px;" >No</td>';
        }
        trObj += '</tr>';

    }

    trObj += '</tbody>';

    var subTaskHeader = '';
    subTaskHeader += '<div id="example2_wrapper" class="dataTables_wrapper form-inline" role="grid">';
    subTaskHeader += '<table style="min-height: 100px;" class="table table-bordered table-striped dataTable">';
    subTaskHeader += '<thead><tr role="row">';
    subTaskHeader += '<th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="Browser: activate to sort column ascending">Sub Task Name</th>';
    subTaskHeader += '<th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="Platform(s): activate to sort column ascending">Start Date</th>';
    subTaskHeader += '<th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="Engine version: activate to sort column ascending">End Date</th>';
    subTaskHeader += '<th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="Engine version: activate to sort column ascending">Assigned User</th>';
    subTaskHeader += '<th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="Engine version: activate to sort column ascending">Completed</th>';
    subTaskHeader += '</tr></thead>';
    subTaskHeader += trObj;
    subTaskHeader += '</table></div>';
    $('#tdSubTasks' + selectedTaskId).html(subTaskHeader);
}

function OnSuccessCall(response) {
    var jsonResponse = JSON.parse(response.d);
    var className = '';
    var trObj = '';
    for (var i = 0; i < jsonResponse.length; i++) {
        if (i % 2 == 0) {
            className = 'odd';
        }
        else {
            className = 'even'
        }
        var taskId = jsonResponse[i].ProjectTaskId

        trObj += '<tr style="height:20px;" class="' + className + '">';
        trObj += '<td style="height:20px;"><i id="iExpand' + taskId +'" onclick="return ExpandTask(' + taskId + ');" style="cursor: pointer; font-size: 24px; color: grey;" class="fa fa-plus-square"></i></td>';
        trObj += '<td style="height:20px;" >' + jsonResponse[i].TaskName + '</td>';
        
        trObj += '<td style="height:20px;" >' + jsonResponse[i].PhaseName + '</td>';
        trObj += ' <td style="height:20px;" >' + jsonResponse[i].StartDate + '</td>';
        trObj += '<td style="height:20px;" >' + jsonResponse[i].EndDate + '</td>';
        trObj += '<td style="text-align: center;height:20px;">';
        trObj += '</tr>';
        trObj += '<tr><td colspan="6"><div style="display:none;" id="tdSubTasks' + taskId + '"></div></td></tr>';
        trObj += '';

    }
    $('#tdInnerRow').html(trObj);
}

function OnErrorCall(response) {
    alert(response);
    alert(response.status + " " + response.statusText);
}