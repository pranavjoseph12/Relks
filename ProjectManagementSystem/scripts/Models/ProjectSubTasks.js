var globalPhaseId = -100;
var addTaskSelectedPhaseId = 0;
var globalTaskId = -100;
var ProjectSubTasksModel = { ProjectSubTaskId: "", ProjectId: "", TaskName: "", StartDate: "", EndDate: "", Description: "", IsCompleted: "", AssignedUserId: "" };
var ProjectTaskModel = {
    ProjectTaskId: "", ProjectPhaseId: 0, TaskName: "", StartDate: "", EndDate: "", AssignedUserId: 0,
    Description: '', SubTasks: []
};
var ProjectPhaseModel = { PhaseId: "", ProjectId: "", PhaseName: "", StartDate: "", EndDate: "", ProjectTasks: [], Description:"" };

//var ProjectPhaseModel = new Object();
//projectModel.PhaseId = 0;
//projectModel.ProjectId = 0;
//projectModel.PhaseName = '';
//projectModel.StartDate = '';
//projectModel.EndDate = '';
//projectModel.ProjectTasks = [];
//projectModel.Description = '';

var projectModel = {
    ProjectId: "", ProjectName: "", PrimaryContactName: "", PrimaryNumber: "",
    Address: "", PinCode: "", Contact1Name: "", Contact1number: "", Contact2Name: "", Contact2Number: "",
    Contact3Name: "", Contact3Number: "", StartDate: "", EndDate: "", ProjectEstimate: "", IsCompleted: false,
    ProjectPhases:[], Comments :"", IsHidden: false
};