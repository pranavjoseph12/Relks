<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="ProjectManagementSystem.Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="scripts/Admin.js?v=1"></script>
    <script src="scripts/jquery-1.10.2.min.js"></script>
    <script src="scripts/RolePermissions.js?v=5"></script>
    <div class="content-wrapper" style="background-color: #fff;">
        <!-- Content Header (Page header) -->
        <section class="content-header page_titleborder">
            <h1>Admin
            </h1>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                <li class="active">Admin</li>
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
                                        <li id="liPersonal" class="active"><a href="#tab1" data-toggle="tab"><i class="fa fa-user"></i>User Details</a></li>
                                        <li id="li1"><a href="#tab2" data-toggle="tab"><i class="fa fa-graduation-cap"></i>Roles</a></li>
                                    </ul>
                                </span>
                                <div class="clearfix"></div>
                            </div>
                            <div>
                                <div class="tab-content">
                                    <div class="tab-pane active" id="tab1">
                                        <section class="content">
                                            <div class="row">
                                                <div id="divAddUserId" style="float: left; margin-left: 25px; margin-bottom: 10px;">
                                                    <a href="#" onclick="return AddUserPopUP();" class="btn btn-block btn-success"><i class="fa fa-plus" style="margin-right: 10px;"></i>Add User</a>
                                                </div>
                                                <div class="col-md-12">
                                                    <div class="box box-default">
                                                        <table style="min-height: 200px;" class="table table-bordered table-striped dataTable">
                                                            <thead>
                                                                <tr role="row">
                                                                    <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="Browser: activate to sort column ascending">Name</th>
                                                                    <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="Platform(s): activate to sort column ascending">Contact Number</th>
                                                                    <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="Engine version: activate to sort column ascending">Email</th>
                                                                    <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="CSS grade: activate to sort column ascending">&nbsp;</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody id="tdInnerRowUsers" class="content-loading" role="alert" aria-live="polite" aria-relevant="all">
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                        </section>
                                    </div>
                                    <div class="tab-pane" id="tab2">
                                        <section class="content">
                                            <div class="row">
                                                <div id="divAddRoleId" style="float: left; margin-left: 25px; margin-bottom: 10px;">
                                                    <a href="AddRole.aspx" class="btn btn-block btn-success"><i class="fa fa-plus" style="margin-right: 10px;"></i>Add Role</a>
                                                </div>
                                                <div class="col-md-12">
                                                    <div class="box box-default">
                                                        <table style="min-height: 200px;" class="table table-bordered table-striped dataTable">
                                                            <thead>
                                                                <tr role="row">
                                                                    <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="Browser: activate to sort column ascending">Role Name</th>
                                                                    <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="CSS grade: activate to sort column ascending">&nbsp;</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody id="tdInnerRowRoles" class="content-loading" role="alert" aria-live="polite" aria-relevant="all">
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                        </section>
                                    </div>
                                    <div class="tab-pane" id="tab3">
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
