﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Enquiries.aspx.cs" Inherits="ProjectManagementSystem.Enquiries" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField runat="server" ID="hdnEnquiryTpe" Value="all" />
    <script src="scripts/Enquiries.js?v=2"></script>
    <script src="scripts/RolePermissions.js?v=5"></script>
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header page_titleborder">
            <h1>Enquiries
            </h1>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                <li class="active">Add Staff</li>
            </ol>
        </section>
        <!-- Main content -->
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-default">
                        <div style="background-color: #fff; margin: 3px 25px;" class="option_nav">
                            <div id="divAddEnquiryButton" style="width: 150px; float: left;">
                                <a href="AddEnquiry.aspx" style="font-size: 14px; width: 150px;" class="btn btn-block btn-success"><i class="fa fa-plus" style="margin-right: 10px;"></i>Add</a>
                            </div>
                            <div style="margin-left:20px;" class="pull-left">
                                <label class="radio-inline">
                                    <input runat="server" onclick="return ChangeDueType('all');" name="optionsDue" id="rbtnDueAll" value="all" type="radio" />All
                                </label>
                                <label style="width: 100px;" class="radio-inline">
                                    <input runat="server" onclick="return ChangeDueType('today');" name="optionsDue" id="rbtnDueToday" value="today" type="radio" />Due Today
                                </label>
                                <label class="radio-inline">
                                    <input runat="server" onclick="return ChangeDueType('overdue');" name="optionsDue" id="rbtOverdue" value="yesterday" type="radio" />Overdue
                                </label>
                                <div class="clearfix"></div>
                            </div>
                            <div class="clearfix"></div>
                        </div>
                        <div class="box-header with-border">
                            <div class="col-md-12">
                                <div>
                                    <div class="tabl_topsearch">
                                        <asp:Button ID="btnExportC" Style="display: none;" runat="server" />
                                        <%--<button id="btnExport" class="btn btn-default pull-left" style="margin-right: 10px;"><i class="fa fa-file-excel-o"></i></button>--%>
                                        <button onclick="return SearchClick();" class="btn btn-default pull-right"><i class="fa fa-search"></i></button>
                                        <button onclick="return SearchClick();" class="btn btn-default pull-left" style="margin-right: 10px;"><i class="fa fa-refresh"></i></button>
                                        <input onkeypress="if ( event.keyCode == 13) return SearchClick();" style="width: 250px;" placeholder="Search Name Or Mobile" type="text" id="txtEnqSeacrh" />
                                        <%--<div style="width: 150px; float: right;" class="input-group date" id="divDateTo">
                                            <input onkeypress="if ( event.keyCode == 13) return searchClick();" id="dateTo" placeholder="Date To" class="form-control" />
                                        </div>
                                        <i class="fa fa-exchange pull-right" style="margin-top: 14px; margin-right: 10px;"></i>
                                        <div style="width: 150px; float: right;" class="input-group date" id="divDateFrom">
                                            <input onkeypress="if ( event.keyCode == 13) return searchClick();" id="dateFrom" placeholder="Date From" class="form-control" />
                                        </div>--%>
                                        <div class="clearfix"></div>
                                    </div>
                                    <div class="data_cont_box">
                                        <div id="example1_wrapper" class="dataTables_wrapper form-inline" role="grid">
                                            <div class="row">
                                                <div class="col-xs-6">
                                                    <div id="example1_length" class="dataTables_length">
                                                        <label>
                                                            <select onchange="searchClick(); return false;" size="1" id="ddlNumberOFRecords" name="example1_length" aria-controls="example1">
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
                                                        <%--<th id="tdCheckAll" class="sorting_asc" role="columnheader" rowspan="1" colspan="1" aria-sort="ascending" aria-label="Rendering engine: activate to sort column descending">
                                                            <input class="selectAll" data-toggle="toggle" type="checkbox" id="chkAll" />
                                                        </th>--%>
                                                        <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="Browser: activate to sort column ascending">Name</th>
                                                        <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="Platform(s): activate to sort column ascending">Contact Number</th>
                                                        <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="Engine version: activate to sort column ascending">Email</th>
                                                        <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="CSS grade: activate to sort column ascending">Response</th>
                                                        <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="CSS grade: activate to sort column ascending">Enquiry Date</th>
                                                        <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="CSS grade: activate to sort column ascending">Existing Customer</th>
                                                        <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="CSS grade: activate to sort column ascending">Quotation</th>
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
              <div id="divCustomerQuotation" style="display:none">
<%--        <div class="form-group" id="divTax">
        <label class="col-sm-3 control-label">Enable Tax</label>
        <div style="margin-bottom: 10px;" class="col-sm-9">
            <input id="chkEnableTax" style="height:20px;width:14px;box-shadow:0 0 0 !important;" type="checkbox" />
        </div>
    </div>--%>
                  <div id="divExport" style="margin-bottom: 10px;" class="col-sm-12">
               <div class="col-sm-4">
               <input style="padding:2px" type="radio" name="rdbExportType" value="1"/>Supply and Labour together</div>
                <div class="col-sm-4"><input style="padding:2px" type="radio" name="rdbExportType" checked="checked" value="2"/>Else</div>
            <div class="col-sm-4"><input type="button" id="btnExportToExcel"  style="border: none; height: 35px; border-radius: 3px;background-color: #f39c12 !important;" class="btn-warning" value="Export"/></div>
        </div>
        <table id="tblHeader" class="table table-striped table-bordered" style="width:100%">
            <thead>
                <tr>
                    <th width="3%"><b>Sl No.</b></th>
                    <th width="40%"><b>Description</b></th>
                    <th width="7%"><b>Unit</b></th>
                    <th width="8%"><b>QTY</b></th>
                    <th width="8%"><b>Rate</b></th>
                    <th width="8%"><b>Amount</b></th>
                    <th width="8%"><b>Item Margin</b></th>
                    <th width="8%"><b>Gross Margin</b></th>
                    <th width="8%"><b>Total Amount</b></th>
                </tr>
            </thead>
        </table>
        <div id="divContentQuotation">
            <table id="tblQuotConent" class="table table-striped table-bordered" style="width:100%">
                <tbody>
                  

                </tbody>
            </table>
             <label>Item margin &nbsp;&nbsp;&nbsp; <span id="spItemMargin"></span></label><br/>
            <label>Gross margin &nbsp;&nbsp;&nbsp;<span id="spGrossMargin"></span></label><br/>
            <%--<label>Tax &nbsp;&nbsp;&nbsp;<span id="spTax"></span></label><br/>--%>
            <label>Item Total &nbsp;&nbsp;&nbsp;<span id="spItemTotal"></span></label><br/>
            <label><b>TOTAL &nbsp;&nbsp;&nbsp;</b> <span id="spQUotationTotal"></span></label>
        </div>
    </div>
        </section>
           <div class="modal" id="myModal">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">

                    <h4 class="modal-title">Modal Header</h4>
                </div>
                <div class="modal-body" style="max-height:450px;overflow:auto">
                    <!--<p>This is a large modal.</p>-->
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default btnModalClose" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
        <!-- /.content -->
    </div>
</asp:Content>
