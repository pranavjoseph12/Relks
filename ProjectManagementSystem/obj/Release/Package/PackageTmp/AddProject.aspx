<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddProject.aspx.cs" Inherits="ProjectManagementSystem.AddProject" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="scripts/AddProject.js?v=2"></script>
    <script src="scripts/jquery-1.10.2.min.js"></script>
    <script src="scripts/RolePermissions.js?v=5"></script>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datetimepicker/4.17.47/css/bootstrap-datetimepicker.min.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datetimepicker/4.17.47/js/bootstrap-datetimepicker.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            GetAllUsersForAssignment();
            //$("#txtAddProjectStartDate").datetimepicker({ format: 'YYYY-MM-DD HH:mm' });
            $("#txtAddProjectStartDate").datepicker({ format: 'yyyy-mm-dd' }).on('change', function () {
                $('.datepicker').hide();
            });
            $("#txtEndDate").datepicker({ format: 'yyyy-mm-dd' }).on('change', function () {
                $('.datepicker').hide();
            });
            //$("#txtEndDate").datetimepicker({ format: 'YYYY-MM-DD HH:mm' });
        });
    </script>
    <div class="content-wrapper" style="background-color: #fff;">
        <!-- Content Header (Page header) -->
        <section class="content-header page_titleborder">
            <h1>Add New Project
            </h1>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                <li class="active">Add Project</li>
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
                                        <li id="liPersonal" class="active"><a href="#tab1" data-toggle="tab"><i class="fa fa-user"></i>Project Details</a></li>
                                        <li id="li1"><a href="#tab2" data-toggle="tab"><i class="fa fa-graduation-cap"></i>Contact Details</a></li>
                                        <li id="liUserProjectAccess"><a href="#tab3" data-toggle="tab"><i class="fa fa-graduation-cap"></i>User Access</a></li>
                                        <li id="liCourse"><a href="#tab4" data-toggle="tab"><i class="fa fa-graduation-cap"></i>Task Details</a></li>
                                    </ul>
                                </span>
                                <div class="clearfix"></div>
                            </div>
                            <div>
                                <div class="tab-content">
                                    <div class="tab-pane active" id="tab1">
                                        <div class="box-header with-border">

                                            <div class="col-md-6 form_cols">
                                                <div style="display:none;" class="form-group">
                                                    <label>Make this project as hiddden?</label>
                                                    <label class="radio-inline">
                                                        <input name="hdnProject" id="rbtnHdnYes" value="yes" type="radio" />Yes
                                                    </label>
                                                    <label style="width: 100px;" class="radio-inline">
                                                        <input name="hdnProject" id="rbtnHdnNo" checked="true" value="no" type="radio" />No
                                                    </label>
                                                </div>
                                                <div class="form-group">
                                                    <label>Project Name:</label>
                                                    <input id="txtAddProjectName" class="form-control"></input>
                                                </div>
                                                <div class="form-group">
                                                    <label>Project Owner:</label>
                                                    <input id="txt" class="form-control"></input>
                                                </div>
                                                <div class="form-group">
                                                    <label>Address:</label>
                                                    <textarea id="txtAddAddress" class="form-control" rows="3"></textarea>
                                                </div>
                                                <div class="form-group">
                                                    <label>PIN Code:</label>
                                                    <input id="txtAddPinCode" class="form-control"></input>
                                                </div>
                                            </div>
                                            <div class="col-md-6 form_cols">
                                                <div class="form-group">
                                                    <label>Project Start Date:</label>
                                                    <input autocomplete="off" onkeypress="return false;" id="txtAddProjectStartDate" class="form-control"></input>
                                                </div>
                                                <div class="form-group">
                                                    <label>Project End Date:</label>
                                                    <input autocomplete="off" onkeypress="return false;" id="txtEndDate" class="form-control"></input>
                                                </div>
                                                <div class="form-group">
                                                    <label>Estimate:</label>
                                                    <input id="txtEstimate" class="form-control"></input>
                                                </div>
                                                <div class="form-group">
                                                    <label>Comments:</label>
                                                    <textarea id="txtComments" class="form-control" rows="3"></textarea>
                                                </div>
                                            </div>
                                            <div class="clearfix"></div>
                                            <div class="col-md-6 pull-right bottom_button">
                                                <a href="#tab2" onclick="ChangeTab('2');" class="btn btn-block btn-success pull-right" style="width: 49%;" data-toggle="tab">Next</a>
                                                <button class="btn btn-block btn-warning pull-right" style="width: 49%; margin-top: 0px; margin-right: 2%;">Clear</button>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="tab-pane" id="tab2">
                                        <div class="box-header with-border">
                                            <div class="col-md-6 form_cols">
                                                <div class="form-group">
                                                    <label>Primary Contact Name:</label>
                                                    <input id="txtPrimaryContactName" class="form-control"></input>
                                                </div>

                                                <div class="form-group">
                                                    <label>Primary Contact Number:</label>
                                                    <input id="txtPrimaryContactNumber" class="form-control"></input>
                                                </div>
                                                <div class="form-group">
                                                    <label>Other Contact1 Name:</label>
                                                    <input id="txtOtherContact1Name" class="form-control"></input>
                                                </div>

                                                <div class="form-group">
                                                    <label>Other Contact1 Number:</label>
                                                    <input id="txtOtherContact1Number" class="form-control"></input>
                                                </div>
                                            </div>
                                            <div class="col-md-6 form_cols">
                                                <div class="form-group">
                                                    <label>Other Contact2 Name:</label>
                                                    <input id="txtOtherContact2Name" class="form-control"></input>
                                                </div>

                                                <div class="form-group">
                                                    <label>Other Contact2 Number:</label>
                                                    <input id="txtOtherContact2Number" class="form-control"></input>
                                                </div>
                                                <div class="form-group">
                                                    <label>Other Contact3 Name:</label>
                                                    <input id="txtOtherContact3Name" class="form-control"></input>
                                                </div>

                                                <div class="form-group">
                                                    <label>Other Contact3 Number:</label>
                                                    <input id="txtOtherContact3Number" class="form-control"></input>
                                                </div>
                                            </div>
                                            <div class="clearfix"></div>
                                            <div class="col-md-6 pull-right bottom_button">
                                                <a onclick="ChangeTab(4);" href="#tab3" class="btn btn-block btn-success pull-right" style="width: 49%;" data-toggle="tab">Next</a>
                                                <button class="btn btn-block btn-warning pull-right" style="width: 49%; margin-top: 0px; margin-right: 2%;">Clear</button>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="tab-pane" id="tab3">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div>
                                                    <div style="font-weight: bold; font-size: 20px;">
                                                        Select the users to whom this Project can be visible
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <table style="min-height: 200px;" class="table table-bordered table-striped dataTable">
                                                    <thead>
                                                        <tr role="row">
                                                            <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="Browser: activate to sort column ascending">Name</th>
                                                            <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="CSS grade: activate to sort column ascending">Email</th>
                                                            <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="CSS grade: activate to sort column ascending">Phone</th>
                                                            <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="CSS grade: activate to sort column ascending">Allow Access</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody id="tdInnerProjectUserAccess" role="alert" aria-live="polite" aria-relevant="all">
                                                        <tr>
                                                            <td>Binu Varghese
                                                            </td>
                                                            <td>binu@test.com
                                                            </td>
                                                            <td>9809085023
                                                            </td>
                                                            <td>
                                                                <input type="checkbox" />
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                                <div class="col-md-6  pull-right">
                                                    <a onclick="ChangeTab(5);" href="#tab4" class="btn btn-block btn-success pull-right" style="width: 49%;" data-toggle="tab">Next</a>
                                                    <button class="btn btn-block btn-warning pull-right" style="width: 49%; margin-top: 0px; margin-right: 2%;">Clear</button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="tab-pane" id="tab4">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div>
                                                    <div>
                                                        <button onclick="return ShowAddPhasePopUp();" style="margin-top: 10px; height: 35px;" class="btn btn-block btn-success pull-left">
                                                            <i class="fa fa-plus" style="margin-right: 10px;"></i>
                                                            Add New Phase
                                                        </button>
                                                    </div>
                                                </div>
                                                <div id="divAddProjectPhase" style="padding: 15px;">
                                                </div>
                                            </div>
                                            <div style="margin-top: 50px;" class="col-md-12">
                                                <div class="col-md-6 pull-right bottom_button">
                                                    <a onclick="return SaveAddProject();" href="#tab4" class="btn btn-block btn-success pull-right" style="width: 49%;" data-toggle="tab">Save</a>
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
        </section>
        <!-- /.content -->
    </div>
</asp:Content>
