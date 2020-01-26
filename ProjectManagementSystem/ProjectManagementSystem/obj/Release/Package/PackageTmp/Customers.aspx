<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Customers.aspx.cs" Inherits="ProjectManagementSystem.Customer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="scripts/Customer.js"></script>
    <script src="scripts/RolePermissions.js?v=5"></script>
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header page_titleborder">
            <h1>Students
            </h1>
            <ol class="breadcrumb">
                <li><a href="Enquiries.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
                <li class="active">Students</li>
            </ol>
        </section>
        <!-- Main content -->
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-default">
                        <div style="background-color: #fff; margin: 3px 25px;" class="option_nav">
                            <div id="divIdAddCustomer" style="width: 150px; float: left;">
                                <a href="AddCustomer.aspx" style="font-size: 14px; width: 150px;" class="btn btn-block btn-success"><i class="fa fa-plus" style="margin-right: 10px;"></i>Add</a>
                            </div>
                            <div class="clearfix"></div>
                        </div>
                        <div class="box-header with-border">
                            <div class="col-md-12">
                                <div>
                                    <div class="tabl_topsearch">
                                        <asp:Button ID="btnExportC" Style="display: none;" runat="server" />
                                        <%--<button id="btnExport" class="btn btn-default pull-left" style="margin-right: 10px;"><i class="fa fa-file-excel-o"></i></button>--%>
                                        <button onclick="return Search();" class="btn btn-default pull-right"><i class="fa fa-search"></i></button>
                                        <button onclick="return Search();" class="btn btn-default pull-left" style="margin-right: 10px;"><i class="fa fa-refresh"></i></button>
                                        <input onkeypress="if ( event.keyCode == 13) return Search();" style="width: 250px;" placeholder="Search Name" type="text" id="txtEnqSeacrh" />
                                        <input type="button" id="btnCustomerExportToExcel"  style="border: none; height: 35px; border-radius: 3px;;background-color: #f39c12 !important;" class="btn-warning" value="Export"/>
                                        <div class="clearfix"></div>
                                    </div>
                                    <div class="data_cont_box">
                                        <div id="example1_wrapper" class="dataTables_wrapper form-inline" role="grid">
                                            <div class="row">
                                                <div class="col-xs-6">
                                                    <div id="example1_length" class="dataTables_length">
                                                        <label>
                                                            <select onchange="Search(); return false;" size="1" id="ddlNumberOFRecords" name="example1_length" aria-controls="example1">
                                                                <option value="10" selected="selected">10</option>
                                                                <option value="25">25</option>
                                                                <option value="50">50</option>
                                                                <option value="100">100</option>
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
                                                        <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="Browser: activate to sort column ascending">Name</th>
                                                        <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="Platform(s): activate to sort column ascending">Contact Number</th>
                                                        <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="Engine version: activate to sort column ascending">Email</th>
                                                        <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="CSS grade: activate to sort column ascending">Rating</th>
                                                        <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="CSS grade: activate to sort column ascending">&nbsp;</th>
                                                    </tr>
                                                </thead>
                                                <tbody id="tdInnerRow" class="content-loading" role="alert" aria-live="polite" aria-relevant="all">
                                                </tbody>
                                            </table>
                                            <div class="row">
                                                <div class="col-xs-6">
                                                    <div class="dataTables_info" id="divCount">0 entries</div>
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
</asp:Content>
