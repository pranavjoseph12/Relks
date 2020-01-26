<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Quotation.aspx.cs" Inherits="ProjectManagementSystem.Quotation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style type="text/css">
        .chkComboItems , .chkComboItemsQuot
        {
                float: left;
                margin-left: 8px;
                margin-right: 5px;
        }
    </style>
    <div class="wrapper">

      

        <!-- Right side column. Contains the navbar and content of the page -->
        <div class="content-wrapper">
            <!-- Content Header (Page header) -->
            <section class="content-header page_titleborder">
                <h1>Quotation</h1>

                <ol class="breadcrumb">
                    <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                    <li class="active">Quotation</li>
                </ol>
            </section>

            <!-- Main content -->
            <section class="content">
                <div class="row">
                    <div class="col-md-12">
                        <div class="panel-heading option_navnew">
                            <span class="pull-left">
                                <!-- Tabs -->
                                <ul class="nav panel-tabs">
                                    <li id="liPersonal" class="active"><a href="#tab1" data-toggle="tab"><i class="fa fa-user"></i>View</a></li>
                                    <li id="li1"><a href="#tab2" data-toggle="tab"><i class="fa fa-user"></i>Create</a></li>
                                    <li id="li2"><a href="#tab3" data-toggle="tab"><i class="fa fa-user"></i>Add Item</a></li>
                                </ul>
                            </span>
                            <div class="clearfix"></div>
                        </div>
                        <div class="box box-default">
                            <div class="col-md-12" style="background-color: #fff;">
                                <div class="tab-content">
                                    <div class="tab-pane active" id="tab1">
                                        <h4>Customer Quotations</h4>
                                        <div class="box-header with-border">

                                            <div class="data_cont_box">

                                                <div id="example1_wrapper" class="dataTables_wrapper form-inline" role="grid">
                                                    <div class="row">
                                                        <div class="col-xs-12">
                                                            <div class="dataTables_filter" id="example1_filter">
                                                                <label class="pull-left">

                                                                    <input placeholder="Customer Name" aria-controls="example1" type="text" id="txtCustomerName">
                                                                    <button style="border: none; border-radius: 3px;" id="btnCustSearch" class="btn-primary">Search</button>
                                                                </label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <table id="tblViewQuotation" class="table table-bordered table-striped dataTable">
                                                        <thead>
                                                            <tr role="row">
                                                                <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="End Date">Customer Name</th>
                                                                <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="Allocated Students">Versions</th>
                                                                <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="Joined Student">Add New Version</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody role="alert" aria-live="polite" aria-relevant="all">
                                                        
                                                        </tbody>
                                                    </table>
                                                    <div class="row">
                                                        <div class="col-xs-6">
                                                            <div class="dataTables_info" id="example1_info">Showing 1 to 3 of 3 entries</div>
                                                        </div>
                                                        <div class="col-xs-6">
                                                            <div class="dataTables_paginate paging_bootstrap">
                                                                <ul class="pagination pull-right">
                                                                    <li class="prev disabled"><a href="#">← Previous</a></li>
                                                                    <li class="next disabled"><a href="#">Next → </a></li>
                                                                </ul>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>



                                            </div>
                                        </div>
                                    </div>

                                    <div class="tab-pane" id="tab2">
                                        <h4>Create Quotation</h4>
                                        <div class="box-header with-border">

                                            <div style="" class="col-md-6 form_cols">
                                                <div class="form-group" style="padding-bottom:30px;">
                                                    <button class="btn btn-block btn-info pull-left" id="btnImportQuotation" style="width: 100px;">Import</button>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-ms-6" control-label">Customer/ Enquiry Name</label>
                                                    <div style="margin-bottom: 10px;" class="col-ms-6">
                                                        <select id="ddlCustomerName">
                                                            <option>Customer 1</option>
                                                            <option>Customer 2</option>
                                                        </select>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-ms-6 control-label">Total</label>
                                                    <div style="margin-bottom: 10px;" class="col-ms-6">
                                                        <input id="txtTotal" type="text" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-ms-6 control-label">Margin(%)</label>
                                                    <div style="margin-bottom: 10px;" class="col-ms-6">
                                                        <input id="txtMargin" type="text" />
                                                    </div>
                                                </div>

                                            </div>
                                            <div id="div1" class="col-md-6 form_cols" style="border: 1px solid #e8e8e8;margin-bottom:5px;">
                                                <div class="col-md-12" style="font-weight: bold;">
                                                    <div class="col-md-6">Select Items</div><div class="col-md-6">
                                                        <input class="form-control" id="txtComboSearchQuotation" type="text" placeholder="Search..">
                                                    </div>
                                                </div>
                                                <div class="col-md-12" id="divComboSelectQuot" style="height:200px;overflow:auto;margin-top:20px;">
                                                    <div class="col-md-12" style="margin-top:10px;">
                                                        <div class="col-md-6">
                                                            Item 1
                                                            <input type="checkbox" />
                                                        </div>
                                                        <div class="col-md-6">
                                                            Item 2
                                                            <input type="checkbox" />
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div style="margin-top:10px;" class="col-md-12">
                                                        <div class="col-md-6">
                                                            Item 3
                                                            <input type="checkbox" />
                                                        </div>
                                                        <div class="col-md-6">
                                                            Item 4
                                                            <input type="checkbox" />
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div style="margin-top:10px;" class="col-md-12" style="margin-top:10px;">
                                                        <div class="col-md-6">
                                                            Item 5
                                                            <input type="checkbox" />
                                                        </div>
                                                        <div class="col-md-6">
                                                            Item 6
                                                            <input type="checkbox" />
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div style="margin-top:10px;" class="col-md-12">
                                                        <div class="col-md-6">
                                                            Item 7
                                                            <input type="checkbox" />
                                                        </div>
                                                        <div class="col-md-6">
                                                            Item 8
                                                            <input type="checkbox" />
                                                        </div>
                                                    </div>
                                                    <div style="margin-top:10px;" class="col-md-12" style="margin-top:10px;">
                                                        <div class="col-md-6">
                                                            Item 9
                                                            <input type="checkbox" />
                                                        </div>
                                                        <div class="col-md-6">
                                                            Item 2
                                                            <input type="checkbox" />
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div style="margin-top:10px;" class="col-md-12">
                                                        <div class="col-md-6">
                                                            Item 3
                                                            <input type="checkbox" />
                                                        </div>
                                                        <div class="col-md-6">
                                                            Item 7
                                                            <input type="checkbox" />
                                                        </div>
                                                    </div>
                                                    <div style="margin-top:10px;" class="col-md-12">
                                                        <div class="col-md-6">
                                                            Item 3
                                                            <input type="checkbox" />
                                                        </div>
                                                        <div class="col-md-6">
                                                            Item 7
                                                            <input type="checkbox" />
                                                        </div>
                                                    </div>
                                                    <div style="margin-top:10px;" class="col-md-12">
                                                        <div class="col-md-6">
                                                            Item 3
                                                            <input type="checkbox" />
                                                        </div>
                                                        <div class="col-md-6">
                                                            Item 7
                                                            <input type="checkbox" />
                                                        </div>
                                                    </div>
                                                    <div style="margin-top:10px;" class="col-md-12">
                                                        <div class="col-md-6">
                                                            Item 3
                                                            <input type="checkbox" />
                                                        </div>
                                                        <div class="col-md-6">
                                                            Item 7
                                                            <input type="checkbox" />
                                                        </div>
                                                    </div>
                                                    <br />
                                                </div>
                                            </div>
                                            <div class="clearfix"></div>
                                            <div style="margin-left: 20%;" class="col-md-6">
                                                <input type="button" class="btn btn-block btn-info pull-right" value="Save" id="btnSaveQuotation" style="width: 100px;  margin-right: 15px;"/>
                                                <button class="btn btn-block btn-default pull-right" style="width: 100px; margin-top: 0px; margin-right: 2%;">Cancel</button>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="tab-pane" id="tab3">
                                        <h4>Add Item</h4>
                                        <div class="box-header with-border">

                                            <div style="" class="col-md-6 form_cols">
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">Item Name</label>
                                                    <div style="margin-bottom: 10px;" class="col-sm-9">
                                                        <input id="txtName" type="text" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">Combo Item</label>
                                                    <div style="margin-bottom: 10px;" class="col-sm-9">
                                                        <select id="ddlCombo" onchange="return ShowOrHideItemsDiv();">
                                                            <option value="No">No</option>
                                                            <option value="Yes">Yes</option>
                                                        </select>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">Description</label>
                                                    <div style="margin-bottom: 10px;" class="col-sm-9">
                                                        <textarea id="txtDescription" rows="2"></textarea>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">Unit</label>
                                                    <div style="margin-bottom: 10px;" class="col-sm-9">
                                                        <input id="txtUnit" type="text" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">Specification</label>
                                                    <div style="margin-bottom: 10px;" class="col-sm-9">
                                                        <input id="txtSpecification" type="text" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">Unit Rate</label>
                                                    <div style="margin-bottom: 10px;" class="col-sm-9">
                                                        <input id="txtUnitRate" type="text" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">Labour</label>
                                                    <div style="margin-bottom: 10px;" class="col-sm-9">
                                                        <input  id="txtLabour" type="text" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">Margin (%)</label>
                                                    <div style="margin-bottom: 10px;" class="col-sm-9">
                                                        <input id="txtMarginPerc" type="text" />
                                                    </div>
                                                </div>
                                                <!--<div class="form-group">
                                                    <label class="col-sm-3 control-label">Enable Tax</label>
                                                    <div style="margin-bottom: 10px;" class="col-sm-9">
                                                        <input id="chkEnableTax" style="height:20px;width:14px;box-shadow:0 0 0 !important;" type="checkbox" />
                                                    </div>
                                                </div>-->
                                                <div id="divTaxRequired" class="form-group" >
                                                    <label class="col-sm-3 control-label">Tax (%)</label>
                                                    <div style="margin-bottom: 10px;" class="col-sm-9">
                                                        <input id="txtTax" type="text" />
                                                    </div>
                                                </div>

                                            </div>
                                            <div id="divItemsId" style="display:none;border: 1px solid #e8e8e8;margin-bottom:5px;" class="col-md-6 form_cols">
                                                <div class="col-md-12" style="font-weight: bold;">
                                                    <div class="col-md-6">Select Items</div><div class="col-md-6">
                                                        <input class="form-control" id="txtComboSearch" type="text" placeholder="Search..">
                                                    </div>
                                                </div>
                                                <div id="divComboSelect" class="col-md-12" style="height:200px;overflow:auto;margin-top:20px;">
                                                    <div class="col-md-12" style="margin-top:10px;">
                                                        <div class="col-md-6">
                                                            Item 1
                                                            <input type="checkbox" />
                                                        </div>
                                                        <div class="col-md-6">
                                                            Item 2
                                                            <input type="checkbox" />
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div style="margin-top:10px;" class="col-md-12">
                                                        <div class="col-md-6">
                                                            Item 3
                                                            <input type="checkbox" />
                                                        </div>
                                                        <div class="col-md-6">
                                                            Item 4
                                                            <input type="checkbox" />
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div style="margin-top:10px;" class="col-md-12" style="margin-top:10px;">
                                                        <div class="col-md-6">
                                                            Item 5
                                                            <input type="checkbox" />
                                                        </div>
                                                        <div class="col-md-6">
                                                            Item 6
                                                            <input type="checkbox" />
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div style="margin-top:10px;" class="col-md-12">
                                                        <div class="col-md-6">
                                                            Item 7
                                                            <input type="checkbox" />
                                                        </div>
                                                        <div class="col-md-6">
                                                            Item 8
                                                            <input type="checkbox" />
                                                        </div>
                                                    </div>
                                                    <div style="margin-top:10px;" class="col-md-12" style="margin-top:10px;">
                                                        <div class="col-md-6">
                                                            Item 9
                                                            <input type="checkbox" />
                                                        </div>
                                                        <div class="col-md-6">
                                                            Item 2
                                                            <input type="checkbox" />
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div style="margin-top:10px;" class="col-md-12">
                                                        <div class="col-md-6">
                                                            Item 3
                                                            <input type="checkbox" />
                                                        </div>
                                                        <div class="col-md-6">
                                                            Item 7
                                                            <input type="checkbox" />
                                                        </div>
                                                    </div>
                                                    <div style="margin-top:10px;" class="col-md-12">
                                                        <div class="col-md-6">
                                                            Item 3
                                                            <input type="checkbox" />
                                                        </div>
                                                        <div class="col-md-6">
                                                            Item 7
                                                            <input type="checkbox" />
                                                        </div>
                                                    </div>
                                                    <div style="margin-top:10px;" class="col-md-12">
                                                        <div class="col-md-6">
                                                            Item 3
                                                            <input type="checkbox" />
                                                        </div>
                                                        <div class="col-md-6">
                                                            Item 7
                                                            <input type="checkbox" />
                                                        </div>
                                                    </div>
                                                    <div style="margin-top:10px;" class="col-md-12">
                                                        <div class="col-md-6">
                                                            Item 3
                                                            <input type="checkbox" />
                                                        </div>
                                                        <div class="col-md-6">
                                                            Item 7
                                                            <input type="checkbox" />
                                                        </div>
                                                    </div>
                                                    <br />
                                                </div>
                                            </div>
                                            <div class="clearfix"></div>
                                            <div style="margin-left: 20%;" class="col-md-6">
                                                <input type="button" id="btnSaveItem" class="btn btn-block btn-info pull-right" style="width: 100px; margin-right: 15px;" value="Save" />
                                                <input type="button" class="btn btn-block btn-default pull-right" style="width: 100px; margin-top: 0px; margin-right: 2%;" value="Cancel">
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
        <!-- /.content-wrapper -->
    </div>

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
    <div id="divCustomerQuotation" style="display:none">
        <div class="form-group" id="divTax">
        <label class="col-sm-3 control-label">Enable Tax</label>
        <div style="margin-bottom: 10px;" class="col-sm-9">
            <input id="chkEnableTax" style="height:20px;width:14px;box-shadow:0 0 0 !important;" type="checkbox" />
        </div>
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
                   <%-- <tr>
                        <td width="3%">1</td>
                        <td class="tdEditable" width="40%"><b>Main Cable</b></td>
                        <td class="tdEditable" width="7%"></td>
                        <td class="tdEditable" width="8%"></td>
                        <td class="tdEditable" width="8%"></td>
                        <td class="tdEditable" width="8%"></td>
                        <td class="tdEditable" width="8%"></td>
                        <td width="8%"></td>
                        <td class="tdEditable" width="8%"></td>
                    </tr>--%>
                    <%--<tr>
                        <td width="3%"></td>
                        <td class="tdEditable" width="40%">Supply and laying of one no. 11kv 3.5 x 180 sqmm XLPE power cable with conductors of best conductivity Aluminium conforming to relevant IS, 11KV grade clamping through the compund  wall with clamps provided 1.5M and  through constructed cable  trench of size 50x100 cm and covering the trench with gratings such that cable can be seen from above , from  premises entrance to electrical room etc or by cable tray as required as approved by the site engineer in charge /consultant.(Civil works have to be carried-out by the client as per the direction of Electrical Contractor/ Consultant)</td>
                        <td class="tdEditable" width="7%"></td>
                        <td class="tdEditable" width="8%"></td>
                        <td class="tdEditable" width="8%"></td>
                        <td class="tdEditable" width="8%"></td>
                        <td class="tdEditable" width="8%"></td>
                        <td width="8%"></td>
                        <td class="tdEditable" width="8%"></td>
                    </tr>--%>
                    <tr>
                        <td width="3%"></td>
                        <td class="tdEditable" width="40%"><b>3.5C x180 sq.mm.</b></td>
                        <td class="tdEditable" width="7%"></td>
                        <td class="tdEditable" width="8%"></td>
                        <td class="tdEditable" width="8%"></td>
                        <td class="tdEditable" width="8%"></td>
                        <td class="tdEditable" width="8%"></td>
                        <td width="8%"></td>
                        <td class="tdEditable" width="8%"></td>
                    </tr>
                   <%-- <tr>
                        <td width="3%"></td>
                        <td class="tdEditable" width="40%">Supply</td>
                        <td class="tdEditable" width="7%">Mtr</td>
                        <td class="tdEditable" width="8%">25</td>
                        <td class="tdEditable" width="8%"></td>
                        <td class="tdEditable" width="8%"></td>
                        <td class="tdEditable" width="8%"></td>
                        <td width="8%"></td>
                        <td class="tdEditable" width="8%"></td>
                    </tr>--%>
                  <%--  <tr>
                        <td width="3%"></td>
                        <td class="tdEditable" width="40%">Labour</td>
                        <td class="tdEditable" width="7%">Mtr</td>
                        <td class="tdEditable" width="8%">25</td>
                        <td class="tdEditable" width="8%"></td>
                        <td class="tdEditable" width="8%"></td>
                        <td class="tdEditable" width="8%"></td>
                        <td width="8%"></td>
                        <td class="tdEditable" width="8%"></td>
                    </tr>--%>


                    <tr>
                        <td width="3%">2</td>
                        <td class="tdEditable" width="40%"><b>MAIN CABLE TERMINATION </b></td>
                        <td class="tdEditable" width="7%"></td>
                        <td class="tdEditable" width="8%"></td>
                        <td class="tdEditable" width="8%"></td>
                        <td class="tdEditable" width="8%"></td>
                        <td class="tdEditable" width="8%"></td>
                        <td width="8%"></td>
                        <td class="tdEditable" width="8%"></td>
                    </tr>
                    <tr>
                        <td width="3%"></td>
                        <td class="tdEditable" width="40%">Supply and making  end terminations for 11kV grade X.L.P.E cables of the following size including supply of heat shrinkable type end sealing kit of approved make and quality with all required accessories such as cable lugs etc. as required and earthing properly</td>
                        <td class="tdEditable" width="7%"></td>
                        <td class="tdEditable" width="8%"></td>
                        <td class="tdEditable" width="8%"></td>
                        <td class="tdEditable" width="8%"></td>
                        <td class="tdEditable" width="8%"></td>
                        <td width="8%"></td>
                        <td class="tdEditable" width="8%"></td>
                    </tr>
                    <tr>
                        <td width="3%"></td>
                        <td class="tdEditable" width="40%"><b>Outdoor End Termination for  3.5CX180 sq.mm Cable</b></td>
                        <td class="tdEditable" width="7%"></td>
                        <td class="tdEditable" width="8%"></td>
                        <td class="tdEditable" width="8%"></td>
                        <td class="tdEditable" width="8%"></td>
                        <td class="tdEditable" width="8%"></td>
                        <td width="8%"></td>
                        <td class="tdEditable" width="8%"></td>
                    </tr>
                    <tr>
                        <td width="3%"></td>
                        <td class="tdEditable" width="40%">Supply</td>
                        <td class="tdEditable" width="7%">Each</td>
                        <td class="tdEditable" width="8%">1</td>
                        <td class="tdEditable" width="8%"></td>
                        <td class="tdEditable" width="8%"></td>
                        <td class="tdEditable" width="8%"></td>
                        <td width="8%"></td>
                        <td class="tdEditable" width="8%"></td>
                    </tr>
                    <tr>
                        <td width="3%"></td>
                        <td class="tdEditable" width="40%">Labour</td>
                        <td class="tdEditable" width="7%">Each</td>
                        <td class="tdEditable" width="8%">1</td>
                        <td class="tdEditable" width="8%"></td>
                        <td class="tdEditable" width="8%"></td>
                        <td class="tdEditable" width="8%"></td>
                        <td width="8%"></td>
                        <td class="tdEditable" width="8%"></td>
                    </tr>
                    <tr>
                        <td width="3%"></td>
                        <td class="tdEditable" width="40%"><b>Indoor End Termination for  3.5CX180 sq.mm Cable</b></td>
                        <td class="tdEditable" width="7%"></td>
                        <td class="tdEditable" width="8%"></td>
                        <td class="tdEditable" width="8%"></td>
                        <td class="tdEditable" width="8%"></td>
                        <td class="tdEditable" width="8%"></td>
                        <td width="8%"></td>
                        <td class="tdEditable" width="8%"></td>
                    </tr>
                    <tr>
                        <td width="3%"></td>
                        <td class="tdEditable" width="40%">Supply</td>
                        <td class="tdEditable" width="7%">Each</td>
                        <td class="tdEditable" width="8%">1</td>
                        <td class="tdEditable" width="8%"></td>
                        <td class="tdEditable" width="8%"></td>
                        <td class="tdEditable" width="8%"></td>
                        <td width="8%"></td>
                        <td class="tdEditable" width="8%"></td>
                    </tr>
                    <tr>
                        <td width="3%"></td>
                        <td class="tdEditable" width="40%">Labour</td>
                        <td class="tdEditable" width="7%">Each</td>
                        <td class="tdEditable" width="8%">1</td>
                        <td class="tdEditable" width="8%"></td>
                        <td class="tdEditable" width="8%"></td>
                        <td class="tdEditable" width="8%"></td>
                        <td width="8%"></td>
                        <td class="tdEditable" width="8%"></td>
                    </tr>

                </tbody>
            </table>
            <label><b>TOTAL &nbsp;&nbsp;&nbsp;</b> <span id="spTotal"></span></label>
        </div>
    </div>
    <div id="divImportContent" style="display:none">
        <div style="" class="col-md-6 form_cols">

            <div class="form-group">
                <label class="col-ms-6" control-label">Customer Name</label>
                <div style="margin-bottom: 10px;" class="col-ms-6">
                    <select id="ddlCustomerNameImport" onchange="ImportQuotations()">
                        <option>Customer 1</option>
                        <option>Customer 2</option>
                    </select>
                </div>
            </div>
            <div class="form-group">
                <label class="col-ms-6 control-label">Version</label>
                <div style="margin-bottom: 10px;" class="col-ms-6">
                    <select id="ddlVersionImport">
                        
                    </select>
                </div>
            </div>
        </div>
    </div>
    <!-- ./wrapper -->
    <!-- jQuery 2.1.3 -->
    <script src="plugins/jQuery/jQuery-2.1.3.min.js"></script>
    <!-- jQuery UI 1.11.2 -->
    <script src="http://code.jquery.com/ui/1.11.2/jquery-ui.min.js" type="text/javascript"></script>
    <!-- Resolve conflict in jQuery UI tooltip with Bootstrap tooltip -->
    <script>
        $.widget.bridge('uibutton', $.ui.button);
    </script>
    <!-- Bootstrap 3.3.2 JS -->
    <script src="bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
    <!-- Morris.js charts -->
    <script src="http://cdnjs.cloudflare.com/ajax/libs/raphael/2.1.0/raphael-min.js"></script>
    <script src="plugins/morris/morris.min.js" type="text/javascript"></script>
    <!-- Sparkline -->
    <script src="plugins/sparkline/jquery.sparkline.min.js" type="text/javascript"></script>
    <!-- jvectormap -->
    <script src="plugins/jvectormap/jquery-jvectormap-1.2.2.min.js" type="text/javascript"></script>
    <script src="plugins/jvectormap/jquery-jvectormap-world-mill-en.js" type="text/javascript"></script>
    <!-- jQuery Knob Chart -->
    <script src="plugins/knob/jquery.knob.js" type="text/javascript"></script>
    <!-- daterangepicker -->
    <script src="plugins/daterangepicker/daterangepicker.js" type="text/javascript"></script>
    <!-- datepicker -->
    <script src="plugins/datepicker/bootstrap-datepicker.js" type="text/javascript"></script>
    <!-- Bootstrap WYSIHTML5 -->
    <script src="plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.all.min.js" type="text/javascript"></script>
    <!-- iCheck -->
    <script src="plugins/iCheck/icheck.min.js" type="text/javascript"></script>
    <!-- Slimscroll -->
    <script src="plugins/slimScroll/jquery.slimscroll.min.js" type="text/javascript"></script>
    <!-- FastClick -->
    <script src='plugins/fastclick/fastclick.min.js'></script>
    <!-- AdminLTE App -->
    <script src="dist/js/app.min.js" type="text/javascript"></script>
<%--    <script src="script/Custom/Common.js"></script>
    <script src="script/Custom/SaveQuotation.js"></script>
    <script src="script/Custom/ViewQuotation.js"></script>
    <script src="script/Custom/AddItem.js"></script>--%>
    <script src="scripts/RolePermissions.js"></script>
    <script src="scripts/AddItem.js"></script>
    <script src="scripts/Common.js"></script>
    <script src="scripts/ViewQuotation.js"></script>
    <script src="scripts/SaveQuotation.js"></script>
</asp:Content>
