<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="CallDetails.aspx.cs" Inherits="ProjectManagementSystem.CallDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style type="text/css">
        .odd{
            background-color:#7E9BE0 !important
        }
    </style>
    <script src="scripts/CallDetails.js"></script>
    <script src="scripts/RolePermissions.js?v=5"></script>
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header page_titleborder">
            <h1>Call History
            </h1>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                <li class="active">Call history</li>
            </ol>
        </section>
        <!-- Main content -->
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-default">
                        <div class="box-header with-border">
                            <div class="col-md-12">
                                <div>
                                   
                                    <div class="data_cont_box">
                                        <div id="example1_wrapper" class="dataTables_wrapper form-inline" role="grid">
                                            
                                            <table style="min-height: 200px;" class="table table-bordered table-striped dataTable">
                                                <thead>
                                                    <tr role="row">
                                                        <%--<th id="tdCheckAll" class="sorting_asc" role="columnheader" rowspan="1" colspan="1" aria-sort="ascending" aria-label="Rendering engine: activate to sort column descending">
                                                            <input class="selectAll" data-toggle="toggle" type="checkbox" id="chkAll" />
                                                        </th>--%>
                                                        <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="Browser: activate to sort column ascending">sl No</th>
                                                        <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="Platform(s): activate to sort column ascending">Agent Name</th>
                                                        <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="Engine version: activate to sort column ascending">Duration</th>
                                                        <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="CSS grade: activate to sort column ascending">Time</th>
                                                        <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="CSS grade: activate to sort column ascending">Call Recording</th>
                                                    </tr>
                                                </thead>
                                                <tbody id="tdInnerRowCallDetails" class="content-loading" role="alert" aria-live="polite" aria-relevant="all">
                                                </tbody>
                                            </table>
                                           <%-- <div class="row">
                                                <div class="col-xs-6">
                                                    <div class="dataTables_info" id="divCount">0 entries</div>
                                                </div>
                                                <div class="col-xs-6">
                                                    <div id="divPagination" class="dataTables_paginate paging_bootstrap">
                                                    </div>
                                                </div>
                                            </div>--%>
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
