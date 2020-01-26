$(document).ready(function () {
    var url = "ProjectService.asmx";
    GetAllUsers();
    GetAllProjectNamesAndId();

    //if (GetParameterValues('ProjectID') != null && GetParameterValues('ProjectID') != undefined) {
    //    $('#divProjectTask').css('display', 'block');
    //    $('#divOtherTask').css('display', 'none');
    //    $("#ddlProjectType").val('Project');
    //    GetAllProjectNamesAndId();
    //}

    function GetAllUsers() {
        $.ajax({
            type: "POST",
            url: "UserService.asmx/GetAllUsersForAssignment",
            data: '',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: BindUsers,
            error: OnErrorCall
        });
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

    function GetAllProjectTasksByPhasesId(phaseId) {
        $.ajax({
            type: "POST",
            url: url + "/GetAllProjectTasksByPhasesId",
            data: JSON.stringify({ phaseId: phaseId }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: BindTasks,
            error: OnErrorCall
        });
    }

    function GetAllProjectSubTasksByTaskId(taskId) {
        $.ajax({
            type: "POST",
            url: url + "/GetAllProjectSubTasksByTaskIdAndUserId",
            data: JSON.stringify({ taskId: taskId }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: BindSubTasks,
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

    function BindProjects(response) {
        var jsonResponse = JSON.parse(response.d);
        var projectObj = '';
        if (GetParameterValues('ProjectID') != null && GetParameterValues('ProjectID') != undefined) {
            //projectObj += "<option value='-1'>SELECT</option>";
            $.each(jsonResponse, function (key, project) {
                if (GetParameterValues('ProjectID') === project.ProjectId.toString()) {
                    projectObj += "<option value='" + project.ProjectId + "'>" + project.ProjectName + "</option>";
                    GetAllPhasesById(GetParameterValues('ProjectID'));
                }
            });
        }
        else {
            projectObj += "<option value='-1'>SELECT</option>";
            $.each(jsonResponse, function (key, project) {
                projectObj += "<option value='" + project.ProjectId + "'>" + project.ProjectName + "</option>";
            });
            $('#ddlPhases').html("<option value='-1'>SELECT</option>");
        }
        $('#ddlProjects').html(projectObj);
        $('#ddlTasks').html("<option value='-1'>SELECT</option>");

        //if (GetParameterValues('ProjectID') != null && GetParameterValues('ProjectID') != undefined) {
        //    $("#ddlProjects").val(GetParameterValues('ProjectID'));
        //    GetAllPhasesById(GetParameterValues('ProjectID'));
        //}
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

    function BindTasks(response) {
        var jsonResponse = JSON.parse(response.d);
        var taskHtml = '';
        taskHtml += "<option value='-1'>SELECT</option>";
        $.each(jsonResponse, function (key, phase) {
            taskHtml += "<option value='" + phase.ProjectTaskId + "'>" + phase.TaskName + "</option>";
        });
        $('#ddlTasks').html(taskHtml);
    }

    function BindSubTasks(response) {
        var jsonResponse = JSON.parse(response.d);
        var subTaskHtml = '';
        subTaskHtml += "<option value='-1'>SELECT</option>";
        $.each(jsonResponse, function (key, subTask) {
            subTaskHtml += "<option value='" + subTask.ProjectSubTaskId + "'>" + subTask.SubTaskName + "</option>";
        });
        subTaskHtml += "<option value='0'>New Sub Task</option>";
        $('#ddlSubTasks').html(subTaskHtml);
    }

    $('#ddlProjectType').change(function () {
        if ($('#ddlProjectType').val() == "Select") {
            $('#divProjectTask').css('display', 'none');
            $('#divOtherTask').css('display', 'none');
        }
        else if ($('#ddlProjectType').val() == "Project") {
            $('#divProjectTask').css('display', 'block');
            $('#divOtherTask').css('display', 'none');
            GetAllProjectNamesAndId();
        }
        else {
            $('#divProjectTask').css('display', 'none');
            $('#divOtherTask').css('display', 'block');
        }
    });

    $('#ddlProjects').change(function () {
        $('#ddlTasks').html("<option value='-1'>SELECT</option>");
        //$('#ddlSubTasks').html("<option value='-1'>SELECT</option>");
        $('#divOtherProjectTask').css('display', 'none');
        if ($('#ddlProjects').val() != "-1") {
            GetAllPhasesById($('#ddlProjects').val());
        }
        else {
            $('#ddlPhases').html('<option value="-1">SELECT</option>');
        }
        ClearStartEndDate();
    });

    $('#ddlPhases').change(function () {
        if ($('#ddlPhases').val() != "-1") {
            GetAllProjectTasksByPhasesId($('#ddlPhases').val());
        }
        else {
            $('#ddlTasks').html("<option value='-1'>SELECT</option>");
            //$('#ddlSubTasks').html("<option value='-1'>SELECT</option>");
            $('#divOtherProjectTask').css('display', 'none');
        }
        ClearStartEndDate();
    });

    $('#ddlTasks').change(function () {
        if ($('#ddlTasks').val() != "-1") {
            //GetAllProjectSubTasksByTaskId($('#ddlTasks').val());
            $('#divOtherProjectTask').css('display', 'block');
            GetStartAndEndDateByTaskId($('#ddlTasks').val());
        }
        else {
            //$('#ddlSubTasks').html("<option value='-1'>SELECT</option>");
            $('#divOtherProjectTask').css('display', 'none');
            ClearStartEndDate();
        }
        
    });

    function GetStartAndEndDateByTaskId(taskId)
    {
        $.ajax({
            type: "POST",
            url: url + "/GetStartAndEndDateByTaskId",
            data: JSON.stringify({ taskId: taskId }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                var jsonResponse = JSON.parse(response.d);
                $('#lblProjectStartEnd').text('(' + jsonResponse.ProjectStartDate + '-' + jsonResponse.ProjectEndDate + ')');
                $('#lblPhaseStartEnd').text('(' + jsonResponse.PhaseStartDate + '-' + jsonResponse.PhaseEndDate + ')');
                $('#lblTaskStartEnd').text('(' + jsonResponse.TaskStartDate + '-' + jsonResponse.TaskEndDate + ')');
            },
            error: OnErrorCall
        });
    }

    function ClearStartEndDate() {
        $('#lblProjectStartEnd').text('');
        $('#lblPhaseStartEnd').text('');
        $('#lblTaskStartEnd').text('');
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

});
