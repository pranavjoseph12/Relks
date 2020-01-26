<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Roles.aspx.cs" Inherits="ProjectManagementSystem.Roles" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="scripts/RolePermissions.js?v=5"></script>
    <div class="content-wrapper">
        <script src="scripts/Roles.js"></script>
        <!-- Content Header (Page header) -->
        <section class="content-header page_titleborder">
            <h1>Roles
            </h1>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                <li class="active">Role</li>
            </ol>
        </section>
        <!-- Main content -->
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-default">
                        <div style="background-color: #fff; margin: 3px 25px;" class="option_nav">
                            <div style="width: 150px; float: left;">
                                <a href="AddRole.aspx"  style="font-size: 14px; width: 150px; margin-right: 10px;"
                                    class="btn btn-block btn-success"  >
                                    <i class="fa fa-plus" style=""></i>Add</a>

                            </div>
                            <div class="clearfix"></div>
                        </div>
                        <div style="display:none;" class="box-header with-border" id="RolePage" ng-app="RolePageApp" ng-controller="RolePagectrl">
                            <div class="col-md-12">
                                <div>
                                    <div class="tabl_topsearch">
                                        <%--<asp:Button ID="btnExportC" Style="display: none;" runat="server" />
                                        <button id="btnExport" class="btn btn-default pull-left" style="margin-right: 10px;"><i class="fa fa-file-excel-o"></i></button>
                                        <button onclick="return SearchClick();" class="btn btn-default pull-right"><i class="fa fa-search"></i></button>
                                        <button onclick="return SearchClick();" class="btn btn-default pull-left" style="margin-right: 10px;"><i class="fa fa-refresh"></i></button>--%>
                                        <input ng-model="filterRoleName" style="width: 250px;" placeholder="Search Role Name" type="text" />

                                        <div class="clearfix"></div>
                                    </div>
                                    <div class="data_cont_box">
                                        <div id="example1_wrapper" class="dataTables_wrapper form-inline" role="grid">
                                            <div class="row">
                                                <div class="col-xs-6">
                                                    <div id="example1_length" class="dataTables_length">
                                                        <label>
                                                            <select ng-change="FetchRoles()" ng-options="opt for opt in pageCount" ng-model="RecordCount" ng-init="RecordCount=10"
                                                                size="1" id="ddlNumberOFRecords" name="example1_length" aria-controls="example1">
                                                            </select>
                                                            records per page</label>
                                                    </div>
                                                </div>
                                                <div class="col-xs-6">
                                                </div>
                                            </div>
                                            <table style="min-height: 200px;" class="table table-bordered table-striped dataTable">
                                                <thead>
                                                    <tr role="row">
                                                        <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="Browser: activate to sort column ascending">Sl No.</th>
                                                        <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="Platform(s): activate to sort column ascending">Role Name</th>
                                                        <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="Engine version: activate to sort column ascending">View</th>
                                                        <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="CSS grade: activate to sort column ascending">Edit</th>
                                                        <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="CSS grade: activate to sort column ascending">Delete</th>
                                                    </tr>
                                                </thead>
                                                <tbody class="content-loading" role="alert" aria-live="polite" aria-relevant="all">
                                                    <tr ng-class-odd="'odd'" ng-class-even="'even'" ng-repeat="role in RoleList|filter:{RoleName:filterRoleName}">
                                                        <td>{{$index+1}}</td>
                                                        <td>{{role.RoleName}}</td>
                                                        <td>
                                                            <input type="checkbox" ng-change="stateChanged(role)" ng-checked="role.Viewable" ng-model="role.Viewable" />
                                                        </td>
                                                        <td>
                                                            <input type="checkbox" ng-change="stateChanged(role)" ng-checked="role.Editable" ng-model="role.Editable" /></td>
                                                        <td>
                                                            <input type="checkbox" ng-change="stateChanged(role)" ng-checked="role.Deletable" ng-model="role.Deletable" /></td>

                                                    </tr>
                                                </tbody>
                                            </table>
                                            <div class="row">
                                                <div class="col-xs-6">
                                                    <div class="dataTables_info" id="divCount">{{ (RoleList | filter: {RoleName:filterRoleName}).length }} entries</div>
                                                </div>
                                                <div class="col-xs-6">
                                                    <div id="divPagination" class="dataTables_paginate paging_bootstrap">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="clearfix"></div>

                        </div>
                    </div>
                </div>
            </div>
        </section>
        <!-- /.content -->
    </div>


    <div id="myModal" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Add New Role</h4>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label>Role Name:</label>
                        <asp:TextBox ID="txtRoleName" runat="server" class="form-control"></asp:TextBox>
                    </div>
                    <asp:Button OnClientClick="return Validate();"  ID="btnSave" runat="server" Text="Save" class="btn btn-block btn-success pull-right" Style="width: 45%; margin-top: 0px; margin-right: 2%;" OnClick="btnSave_Click" />
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
