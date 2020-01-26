function AddProjectPhase() {
    if ($('#txtAddPhaseName').val().trim() == '' || $('#txtAddPhaseStartDate').val().trim() == '' || $('#txtAddPhaseEndDate').val().trim() == '') {
        alert('Name, Start Date and End Date are mandatory');
        return false;
    }
    else {
        var d1 = new Date($('#txtAddPhaseStartDate').val());
        //d1.setHours(0, 0, 0, 0);
        d2 = new Date($('#txtAddPhaseEndDate').val());
        //d2.setHours(0, 0, 0, 0);

        if (d1 >= d2) {
            alert('End date should be less than start date');
            return false;
        }

        var d3 = new Date($('#txtAddProjectStartDate').val());
        d4 = new Date($('#txtEndDate').val());
        if (d1 < d3 || d2 > d4) {
            alert('Phase start and End date should fall within the Project start and End date');
            return false;
        }
    }

    var dummyphaseObj = { PhaseId: "", ProjectId: "", PhaseName: "", StartDate: "", EndDate: "", ProjectTasks: [], Description:"" };
    var phaseObj = dummyphaseObj;
    phaseObj.PhaseId = globalPhaseId + 1;
    phaseObj.ProjectId = 0;
    phaseObj.PhaseName = $('#txtAddPhaseName').val();
    phaseObj.StartDate = $('#txtAddPhaseStartDate').val();
    phaseObj.EndDate = $('#txtAddPhaseEndDate').val();
    phaseObj.Description = $('#txtAddPhaseDescription').val();
    projectModel.ProjectPhases.push(phaseObj);
    var manDivHtmlStart = '<div style="margin-top:35px;"><div style="border: 1px solid #f4f4f4" class="box-header with-border">';
    var manDivHtmlEnd = '</div></div>';
    var phaseNumberDiv = '<div class=""><section style="padding: 0px;" class="content-header"><h1 id="h1PhaseId' + phaseObj.PhaseId + '" style="color: #195592;">' + phaseObj.PhaseName +'</h1></section></div>';
    var divPhaseHeaderDiv = '<div id="divPhaseHeaderId' + phaseObj.PhaseId + '" style="font-size: 18px; color: #195592;" class="col-md-12 box-header with-border">';
    divPhaseHeaderDiv += phaseObj.PhaseName + ' ( ' + phaseObj.StartDate + ' - ' + phaseObj.EndDate + ' ) ';
    divPhaseHeaderDiv += '<button onclick=\'return EditPhasePopup("' + phaseObj.PhaseName + '","' + phaseObj.StartDate + '","' + phaseObj.EndDate + '","' + phaseObj.PhaseId + '","' + phaseObj.Description +'")\' ';
    divPhaseHeaderDiv += ' class="btn btn-warning small_sp" style="margin-right: 5px;"><i class="fa fa-pencil"></i></button>';
    divPhaseHeaderDiv += '<button onclick="addTaskSelectedPhaseId = ' + phaseObj.PhaseId + '; return showAddTaskPopup();" class="btn btn-block btn-success pull-right" style="width: 100px; height: 25px; padding: 0px;">';
    divPhaseHeaderDiv += '<i class="fa fa-plus" style="margin-right: 10px;"></i>';
    divPhaseHeaderDiv += 'Add Task </button> </div>';
    var tableTasksHeader = '<table class="table table-bordered table-striped dataTable">';
    tableTasksHeader += '<thead><tr role="row">';
    tableTasksHeader += '<th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="Browser: activate to sort column ascending">Task Name</th>';
    tableTasksHeader += '<th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="Platform(s): activate to sort column ascending">Description</th>';
    tableTasksHeader += '<th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="Engine version: activate to sort column ascending">Start Date</th>';
    tableTasksHeader += '<th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="CSS grade: activate to sort column ascending">End Date</th>';
    tableTasksHeader += '<th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="CSS grade: activate to sort column ascending">&nbsp;</th>';
    tableTasksHeader += '</tr></thead><tbody id="tdInnerRowPhase' + phaseObj.PhaseId +'" role="alert" aria-live="polite" aria-relevant="all">'
    tableTasksHeader += '</tbody></table>';
    $('#divAddProjectPhase').html($('#divAddProjectPhase').html() + manDivHtmlStart + phaseNumberDiv + divPhaseHeaderDiv + tableTasksHeader + manDivHtmlEnd);
    globalPhaseId = globalPhaseId + 1;
    $('#ModalAddPhase').hide();
    return false;
}

function showAddTaskPopup() {
    var i = 0;
    $.each(projectModel.ProjectPhases, function (key, value) {
        if (value.PhaseId == addTaskSelectedPhaseId) {
            $('#lblAddPhaseStartDate').text(projectModel.ProjectPhases[i].StartDate);
            $('#lblAddPhaseEndDate').text(projectModel.ProjectPhases[i].EndDate);
        }
        i++;
    });
    $('#ModalAddTask').show();
    return false;
}
function ShowTaskDeletePopUP(expId, expName) {
    var bodyContent = '<div class="form-group"><label>Name:</label><label style="margin-left:20px;">' + expName + '</label></div>'
    var footerContent = '<a id="btnDeleteTask"   class="btn btn-block btn-success pull-right" style="width: 20%;" data-toggle="tab">Delete</a><a class="btn btn-block btn-warning pull-right classEdit"  style="width: 20%; margin-top: 0px; margin-right: 2%;">Cancel</a>';
    $("#MyModal").find('.modal-content').find('.modal-header').find('h2').text('Confirm Delete Task?');
    $("#MyModal").find('.modal-content').find('.modal-body').html(bodyContent);
    $("#MyModal").find('.modal-content').find('.modal-footer').html(footerContent);
    $("#MyModal").find('.modal-content').find('.modal-footer').find('.btn-warning').unbind().bind('click', function () { $("#MyModal").hide(); })
    $("#MyModal").find('.modal-content').find('.modal-header').find('span').unbind().bind('click', function () { $("#MyModal").hide(); })
    $("#btnDeleteTask").unbind().bind('click', function () { DeleteTask(expId) })
    $("#MyModal").show();
    return false;
}

function DeleteTask(TaskId) {
    $("#MyModal").hide();
    $.ajax({
        type: "POST",
        url: url + "/DeleteTask",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ "taskId": TaskId }),
        success: function (result) {
            if (result.d == 'true') {
                alert('Delete Success');
                window.location.href = "Projects.aspx";
            }
            else {
                alert("Something went wrong. Please try agin later");
            }

        }
    });
}
function EditTaskPopup(phaseId, taskId, name, startDate, endDate, description) {
    var bodyContent = '<div><div class="col-md-6 form_cols">';
    bodyContent += '<div class="form-group"><label>Task Name:</label><input id="txtEditProjectTaskName" class="form-control"></input></div>';
    bodyContent += '<div class="form-group"><label>Description:</label><input id="txtEditProjectTaskDescription" class="form-control"></input></div>';
    bodyContent += '</div><div class="col-md-6 form_cols">';
    bodyContent += '<div class="form-group"><label>Start Date:</label><input id="txtEditProjectTaskStartDate" class="form-control"></input></div>';
    bodyContent += '<div class="form-group"><label>End Date:</label><input id="txtEditProjectTaskEndDate" class="form-control"></input></div>';
    bodyContent += '</div></div>';

    var footerContent = '<a id="addUser"   class="btn btn-block btn-success pull-right" style="width: 20%;" data-toggle="tab">Add</a><a class="btn btn-block btn-warning pull-right classEdit"  style="width: 20%; margin-top: 0px; margin-right: 2%;">Cancel</a>';
    $("#MyModal").find('.modal-content').find('.modal-header').find('h2').text('Edit Project Task');
    $("#MyModal").find('.modal-content').find('.modal-body').html(bodyContent);
    $("#MyModal").find('.modal-content').find('.modal-footer').html(footerContent);
    $("#MyModal").find('.modal-content').find('.modal-footer').find('.btn-warning').unbind().bind('click', function () { $("#MyModal").hide(); })
    $("#MyModal").find('.modal-content').find('.modal-header').find('span').unbind().bind('click', function () { $("#MyModal").hide(); })
    $("#addUser").unbind().bind('click', function () { SaveEditedProjectTask(phaseId, taskId) });
    $('#txtEditProjectTaskName').val(name);
    $('#txtEditProjectTaskDescription').val(description);
    $('#txtEditProjectTaskStartDate').val(startDate);
    $('#txtEditProjectTaskEndDate').val(endDate);
    $("#txtEditProjectTaskStartDate").datetimepicker({ format: 'YYYY-MM-DD HH:mm' });
    $("#txtEditProjectTaskEndDate").datetimepicker({ format: 'YYYY-MM-DD HH:mm' });
    $("#MyModal").show();
    return false;
}

function EditPhasePopup(phaseName, startDate, endDate, phaseId, description) {
    $('#lblPhaseEditProjectStart').text($('#txtAddProjectStartDate').val());
    $('#lblPhaseEditProjectEnd').text($('#txtEndDate').val());
    $('#txtPhaseEditName').val(phaseName);
    $('#txtPhaseEditDescription').val(description);
    $('#txtPhaseEditStartDate').val(startDate);
    $('#txtPhaseEditEndDate').val(endDate);
    $('#btnUpdatePhaseEdit').click(function () {
        return SaveEditProjectPhase(phaseId);
    });
    $('#modalUpdatePhaseFooter').html('<button id="btnUpdatePhaseEdit" onclick="return SaveEditProjectPhase(' + phaseId+ ')" class="btn btn-block btn-success pull-right" style="width: 49%; margin-top: 0px; margin-right: 2%;">Update</button>');
    //$("#btnUpdatePhaseEdit").unbind().bind('click', function () {  });
    $('#ModalEditPhase').show();
    return false;
}


    function ShowAddPhasePopUp() {
        var projectObj = projectModel;
        if ($('#txtAddProjectStartDate').val() == '' || $('#txtEndDate').val() == '') {
            alert('Please enter Project Start and End Date to Add Phase');
            return false;
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
            alert('Please add aleast one task in all the added Phases');
            return false;
        }

        $('#lblAddPhaseProjectStartDate').text($('#txtAddProjectStartDate').val());
        $('#lblAddPhaseProjectEndDate').text($('#txtEndDate').val());
        $('#ModalAddPhase').show();
        return false;
    }


    function SaveEditProjectPhase(phaseId) {
        if ($('#txtPhaseEditName').val().trim() == '' || $('#txtPhaseEditStartDate').val().trim() == '' || $('#txtPhaseEditEndDate').val().trim() == '') {
            alert('Name, Start Date and End Date are mandatory');
            return false;
        }
        else {
            var d1 = new Date($('#txtPhaseEditStartDate').val());
            //d1.setHours(0, 0, 0, 0);
            d2 = new Date($('#txtPhaseEditEndDate').val());
            //d2.setHours(0, 0, 0, 0);

            if (d1 >= d2) {
                alert('End date should be less than start date');
                return false;
            }

            var d3 = new Date($('#txtAddProjectStartDate').val());
            d4 = new Date($('#txtEndDate').val());
            if (d1 < d3 || d2 > d4) {
                alert('Phase start and End date should fall within the Project start and End date');
                return false;
            }
        }

        $('#ModalEditPhase').hide();
        $.each(projectModel.ProjectPhases, function (key, phase) {
            if (phase.PhaseId == phaseId) {
                phase.PhaseName = $('#txtPhaseEditName').val().trim();
                phase.StartDate = $('#txtPhaseEditStartDate').val().trim();
                phase.EndDate = $('#txtPhaseEditEndDate').val().trim();
                phase.Description = $('#txtPhaseEditDescription').val().trim();
                $('#h1PhaseId' + phaseId).html($('#txtPhaseEditName').val().trim());

                var divPhaseHeaderDiv = phase.PhaseName + ' ( ' + $('#txtPhaseEditStartDate').val().trim() + ' - ' + $('#txtPhaseEditEndDate').val().trim() + ' ) ';
                divPhaseHeaderDiv += '<button onclick=\'return EditPhasePopup("' + phase.PhaseName + '","' + phase.StartDate + '","' + phase.EndDate + '","' + phase.PhaseId + '","' + phase.Description + '")\' ';
                divPhaseHeaderDiv += ' class="btn btn-warning small_sp" style="margin-right: 5px;"><i class="fa fa-pencil"></i></button>';
                divPhaseHeaderDiv += '<button onclick="addTaskSelectedPhaseId = ' + phase.PhaseId + '; return showAddTaskPopup();" class="btn btn-block btn-success pull-right" style="width: 100px; height: 25px; padding: 0px;">';
                divPhaseHeaderDiv += '<i class="fa fa-plus" style="margin-right: 10px;"></i>';
                divPhaseHeaderDiv += 'Add Task </button> </div>';

                $('#divPhaseHeaderId' + phaseId).html(divPhaseHeaderDiv);
            }
        });

        //var divPhaseHeaderDiv = '<div id="divPhaseHeaderId' + phaseObj.PhaseId + '" style="font-size: 18px; color: #195592;" class="col-md-12 box-header with-border">';
        
        return false;
    }


    function SaveEditedProjectTask(phaseId, taskId) {
        var tableTaskContent = '';
        $.each(projectModel.ProjectPhases, function (key, phase) {
            if (phase.PhaseId == phaseId) {
                $.each(phase.ProjectTasks, function (key, taskObj) {
                    if (taskObj.ProjectTaskId == taskId) {
                        taskObj.TaskName = $('#txtEditProjectTaskName').val();
                        taskObj.Description = $('#txtEditProjectTaskDescription').val();
                        taskObj.StartDate = $('#txtEditProjectTaskStartDate').val();
                        taskObj.EndDate = $('#txtEditProjectTaskEndDate').val();
                    }
                    tableTaskContent += '<tr class="odd"><td class=" ">' + taskObj.TaskName + '</td><td class=" ">' + taskObj.Description + '</td><td class=" ">' + taskObj.StartDate + '</td>';
                    tableTaskContent += '<td class=" ">' + taskObj.EndDate + '</td><td class=" " style="text-align: center">';
                    tableTaskContent += '<button onclick=\'return EditTaskPopup("' + phaseId + '","' + taskObj.ProjectTaskId + '","' + taskObj.TaskName + '","' + taskObj.StartDate + '","' + taskObj.EndDate + '","' + taskObj.Description + '")\' ';
                    tableTaskContent += 'class="btn btn-warning small_sp" style="margin-right: 5px;"><i class="fa fa-pencil"></i></button>';
                    tableTaskContent += '<button onclick="return false;" class="btn btn-danger small_sp" style="margin-right: 5px;"><i class="fa fa-trash-o"></i></button>';
                    tableTaskContent += '</td></tr>';
                });
            }
        });

    
        $('#tdInnerRowPhase' + phaseId).html(tableTaskContent);
        $("#MyModal").hide();
        return false;
    }