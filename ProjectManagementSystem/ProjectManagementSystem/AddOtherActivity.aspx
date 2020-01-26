<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddOtherActivity.aspx.cs" Inherits="ProjectManagementSystem.AddOtherActivity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="scripts/jquery-3.2.1.min.js"></script>
    <script src="scripts/AddOtherActivity.js"></script>
    <script src="scripts/RolePermissions.js?v=5"></script>
    <div class="content-wrapper" style="background-color: #fff;">
        <!-- Content Header (Page header) -->
        <section class="content-header page_titleborder">
            <h1>Add Other Activity
            </h1>
            <ol class="breadcrumb">
                <li><a href="Enquiries.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
                <li class="active">Add Other Activity</li>
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
                                        <li id="liPersonal" class="active"><a href="#tab1" data-toggle="tab"><i class="fa fa-user"></i>Activity Details</a></li>
                                    </ul>
                                </span>
                                <div class="clearfix"></div>
                            </div>
                            <div>
                                <div class="tab-content">
                                    <div class="tab-pane active" id="tab1">
                                        <div class="box-header with-border">
                                            <section class="content">
                                                <div class="row">
                                                    <div style="float: left; margin-left: 25px; margin-bottom: 10px;">
                                                        <a href="#" onclick="return AddUserPopUP();" class="btn btn-block btn-success"><i class="fa fa-plus" style="margin-right: 10px;"></i>Add Activity Type</a>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <div class="box box-default">
                                                            <div class="col-md-6 form_cols">

                                                                <div class="form-group">
                                                                    <label>Activity Name:</label>
                                                                    <input id="txtTaskNAme" class="form-control" />
                                                                </div>
                                                                <div class="form-group">
                                                                    <label>Activity Type:</label>
                                                                    <select id="ddlActivityType">
                                                                    </select>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label>Assignee:</label>
                                                                    <select id="ddlUsers">
                                                                    </select>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-6 form_cols">
                                                                <div class="form-group">
                                                                    <label>Expected Start Date:</label>
                                                                    <input onkeypress="return false;" id="txtStartDate" class="form-control"></input>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label>Expected End Date:</label>
                                                                     <input onkeypress="return false;"  id="txtEndDate" class="form-control"></input>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label>Comments:</label>
                                                                    <input id="txtComments" class="form-control" />
                                                                </div>
                                                            </div>
                                                            <div class="clearfix"></div>
                                                            <div class="col-md-6 pull-right bottom_button">
                                                                <a href="#tab2" onclick="return AssignOtherTaskToUser()" class="btn btn-block btn-success pull-right" style="width: 49%;" data-toggle="tab">Save</a>
                                                                <button class="btn btn-block btn-warning pull-right" style="width: 49%; margin-top: 0px; margin-right: 2%;">Clear</button>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </section>
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
