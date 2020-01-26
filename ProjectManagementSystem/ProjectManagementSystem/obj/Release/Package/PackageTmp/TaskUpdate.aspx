<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TaskUpdate.aspx.cs" Inherits="ProjectManagementSystem.TaskUpdate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="scripts/RolePermissions.js?v=5"></script>
    <script src="scripts/jquery-3.2.1.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#txtStartDate").datetimepicker({ format: 'YYYY-MM-DD HH:mm' });
            $("#txtEndDate").datetimepicker({ format: 'YYYY-MM-DD HH:mm' });
        });

        function AssignTaskToUser() {
            if ($('#ddlProjects').val() == "-1" || $('#ddlPhases').val() == "-1" || $('#ddlTasks').val() == "-1" || $('#ddlUsers').val() == "-1" ||
                $('#txtSubTaskName').val() == "" || $('#txtStartDate').val() == "" || $('#txtEndDate').val() == "") {
                alert(' Please enter all the fields');
                return false;
            }
            else {
                $.ajax({
                    type: "POST",
                    url: "ProjectService.asmx/AddSubTaskToUser",
                    data: JSON.stringify({
                        taskId: $('#ddlTasks').val(), startDate: $('#txtStartDate').val(),
                        endDate: $('#txtEndDate').val(), userId: $('#ddlUsers').val(), subTaskName: $('#txtSubTaskName').val(),
                        comments: $('#txtComments').val()
                    }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: AddUserSuccess,
                    error: OnErrorCall
                });
                return false;
            }
        }

        function AddUserSuccess(response) {
            alert('Task Assigned Successfully');
            window.location.href = 'TaskUpdate.aspx';
        }



    </script>

    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header page_titleborder">
            <h1>Task Update
            </h1>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                <li class="active">Task Update</li>
            </ol>
        </section>
        <!-- Main content -->
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-default">
                        <div class="panel panel-primary">
                            <div class="panel-heading option_navnew">
                                <span class="pull-left">
                                    <!-- Tabs -->
                                    <ul class="nav panel-tabs">
                                        <li id="li1" class="active"><a href="#tab1" data-toggle="tab"><i class="fa fa-user"></i>Activity Details</a></li>
                                    </ul>
                                </span>
                                <div class="clearfix"></div>
                            </div>
                        </div>
                       <%-- <div style="background-color: #fff;" class="option_nav">
                            <div class="pull-left col-md-6">
                                <div class="form-group">
                                    <label for="inputPassword3" class="col-sm-3 control-label">Category</label>
                                    <div class="col-sm-9">
                                        <select id="ddlProjectType">
                                            <option value="Select">SELECT</option>
                                            <option value="Project">Project Work</option>
                                            <option value="Other">Other Work</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                            </div>
                            <div class="clearfix"></div>
                        </div>--%>
                        <div class="box-header with-border">
                            <div class="col-md-12">
                                <section class="content">
                                    <div class="row">
                                        <div id="divProjectTask" style="display: block;" class="col-md-12">
                                            <div class="box box-default">
                                                <div>
                                                    <div>
                                                        <div class="col-md-12">
                                                            <div class="tab-content">
                                                                <div class="tab-pane active" id="tab1">
                                                                    <div class="box-header with-border">
                                                                        <div class="col-md-6 form_cols">
                                                                            <div class="form-group">
                                                                                <label>Project Name:</label><label id="lblProjectStartEnd"> </label>
                                                                                <select id="ddlProjects">
                                                                                    <option>Select</option>
                                                                                </select>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label>Phase:</label><label id="lblPhaseStartEnd"></label>
                                                                                <select id="ddlPhases">
                                                                                    <option>Select</option>
                                                                                </select>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label>Task:</label><label id="lblTaskStartEnd"></label>
                                                                                <select id="ddlTasks">
                                                                                    <option>Select</option>
                                                                                </select>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label>User:</label>
                                                                                <select id="ddlUsers">
                                                                                    <option>Select</option>
                                                                                </select>
                                                                            </div>
                                                                        </div>
                                                                        <%--<div class="form-group">
                                                                            <label>Sub Task:</label>
                                                                            <select id="ddlSubTasks">
                                                                                <option>Select</option>
                                                                            </select>
                                                                        </div>--%>
                                                                        <div class="col-md-6 form_cols">
                                                                            <div style="display: none;" id="divOtherProjectTask" class="form-group">
                                                                                <label>Sub Task Name:</label>
                                                                                <input type="text" id="txtSubTaskName" class="form-control"></input>
                                                                            </div>

                                                                            <div class="form-group">
                                                                                <label>Start Date:</label>
                                                                                <input onkeypress="return false;" type="text" id="txtStartDate" class="form-control"></input>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label>End Date:</label>
                                                                                <input onkeypress="return false;" type="text" id="txtEndDate" class="form-control"></input>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label>Comments:</label>
                                                                                <input id="txtComments" class="form-control"></input>
                                                                            </div>
                                                                        </div>
                                                                    <div class="clearfix"></div>
                                                                    <div class="col-md-6 pull-right bottom_button">
                                                                        <button onclick="return AssignTaskToUser();" class="btn btn-block btn-success pull-right" style="width: 49%;" data-toggle="tab">Assign</button>
                                                                    </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="divOtherTask" style="display: none;" class="col-md-12">
                                            <div class="box box-default">
                                                <div>
                                                    <div>
                                                        <div class="col-md-12">
                                                            <div class="tab-content">
                                                                <div class="tab-pane active" id="Div2">
                                                                    <div class="box-header with-border">
                                                                        <div class="col-md-6 form_cols">
                                                                            <div class="form-group">
                                                                                <label>Type:</label>
                                                                                <select id="ddlOtherProjects">
                                                                                    <option>SELECT</option>
                                                                                </select>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label>Task:</label>
                                                                                <select id="ddlOtherTasks">
                                                                                    <option>SELECT</option>
                                                                                </select>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label>Sub Task:</label>
                                                                                <select id="ddlOtherSubTasks">
                                                                                    <option>SELECT</option>
                                                                                </select>
                                                                            </div>
                                                                            <div style="display: none;" id="divOtherNewTask" class="form-group">
                                                                                <label>Task Name:</label>
                                                                                <asp:TextBox runat="server" class="form-control"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-6 form_cols">
                                                                            <div class="form-group">
                                                                                <label>Start Date:</label>
                                                                                <asp:TextBox ID="TextBox6" runat="server" class="form-control"></asp:TextBox>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label>End Date:</label>
                                                                                <asp:TextBox onkeypress="if ( isNaN( String.fromCharCode(event.keyCode) )) return false;" ID="TextBox7" runat="server" class="form-control"></asp:TextBox>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label>Task Completed:</label>
                                                                                <select>
                                                                                    <option>YES</option>
                                                                                    <option>NO</option>
                                                                                </select>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label>Comments:</label>
                                                                                <asp:TextBox ID="TextBox9" runat="server" class="form-control"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="clearfix"></div>
                                                                        <div class="col-md-6 pull-right bottom_button">
                                                                            <a href="#tab2" onclick="ChangeTab('2');" class="btn btn-block btn-success pull-right" style="width: 49%;" data-toggle="tab">Save</a>
                                                                            <button class="btn btn-block btn-warning pull-right" style="width: 49%; margin-top: 0px; margin-right: 2%;">Clear</button>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </section>
                            </div>
                            <div class="clearfix"></div>

                        </div>
                    </div>
                </div>
            </div>
        </section>
        <!-- /.content -->
    </div>
    <script src="scripts/jquery-1.10.2.min.js"></script>
    <script src="scripts/TaskUpdate.js?v=4"></script>
</asp:Content>
