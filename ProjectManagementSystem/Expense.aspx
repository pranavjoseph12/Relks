<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Expense.aspx.cs" Inherits="ProjectManagementSystem.Expense" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="scripts/RolePermissions.js?v=5"></script>
    <script src="scripts/jquery-3.2.1.min.js"></script>
    <script src="scripts/Expense.js?v=9"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#txtExpDate").datepicker({ format: 'yyyy-mm-dd' });
            $("#txtExpdateOther").datepicker({ format: 'yyyy-mm-dd' });
            $("#txtIncomeDate").datepicker({ format: 'yyyy-mm-dd' });
            $("#dateToIncome").datepicker({ format: 'yyyy-mm-dd' });
            $("#dateFromIncome").datepicker({ format: 'yyyy-mm-dd' });
        });

    </script>

    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header page_titleborder">
            <h1>Expense Manager
            </h1>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                <li class="active">Expense Manager</li>
            </ol>
        </section>
        <!-- Main content -->
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-default">
                        <div style="margin-bottom: 0px;" class="panel">
                            <div class="panel-heading">
                                <%--<div class="pull-left col-md-6">
                                    <div class="form-group">
                                        <label for="inputPassword3" class="col-sm-3 control-label">Expense For: </label>
                                        <div class="col-sm-9">
                                            <label class="radio-inline">
                                                <input style="box-shadow: none;" onclick="return ChangeType('project');" name="optionsDue" id="rbtnProjecgt" value="Project" type="radio" />Project
                                            </label>
                                            <label style="width: 100px;" class="radio-inline">
                                                <input style="box-shadow: none;" onclick="return ChangeType('other');" name="optionsDue" id="rbtnOther" value="Other" type="radio" />Other
                                            </label>
                                            <div class="clearfix"></div>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>--%>
                                <div class="col-md-6">
                                    <div style="width: 150px; float: left;">
                                        <a onclick="return ShowAddCategoryPopup();" href="#" style="font-size: 14px;" class="btn btn-block btn-success"><i class="fa fa-plus" style="margin-right: 10px;"></i>Add Category</a>
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                            </div>
                        </div>
                        <div style="background-color: #fff;" class="option_nav">
                        </div>
                        <div class="box-header with-border">
                            <div class="col-md-12">

                                <section class="content">
                                    <div class="row">
                                        <div id="divProjectTask" style="display: block;" class="col-md-12">
                                            <div class="panel-heading option_navnew">
                                                <span class="pull-left">
                                                    <!-- Tabs -->
                                                    <ul class="nav panel-tabs">
                                                        <li id="liPersonal" class="active"><a href="#tab1" data-toggle="tab"><i class="fa fa-user"></i>Add Expense</a></li>
                                                        <li id="liViewExpense"><a href="#tab2" data-toggle="tab"><i class="fa fa-graduation-cap"></i>View Expense</a></li>
                                                         <li id="liAddIncome" ><a href="#tab6" data-toggle="tab"><i class="fa fa-user"></i>Add Income</a></li>
                                                        <li id="liViewIncome"><a href="#tab7" data-toggle="tab"><i class="fa fa-graduation-cap"></i>View Income</a></li>
                                                    </ul>
                                                </span>
                                                <div class="clearfix"></div>
                                            </div>
                                            <div class="box box-default">
                                                <div>
                                                    <div>
                                                        <div class="col-md-12">
                                                            <div class="tab-content">
                                                                <div class="tab-pane active" id="tab1">
                                                                    <div class="box-header with-border">
                                                                        <div class="col-md-6 form_cols">
                                                                            <div class="form-group">
                                                                                <label>Expense Name:</label>
                                                                                <input id="txtExpName" />
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label>Expense Category:</label>
                                                                                <select id="ddlExpenseCategory">
                                                                                    <option value="-1">Select</option>
                                                                                </select>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label>Project Name:</label>
                                                                                <select id="ddlProjects">
                                                                                    <option>Select</option>
                                                                                </select>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label>Phase:</label>
                                                                                <select id="ddlPhases">
                                                                                    <option>Select</option>
                                                                                </select>
                                                                            </div>

                                                                        </div>
                                                                        <div class="col-md-6 form_cols">
                                                                            <div class="form-group">
                                                                                <label>Amount:</label>
                                                                                <input type="text" id="txtAmount" class="form-control"></input>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label>Expense Date:</label>
                                                                                <input onkeypress="return false;" type="text" id="txtExpDate" class="form-control"></input>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label>Comments:</label>
                                                                                <input id="txtComments" class="form-control"></input>
                                                                            </div>
                                                                        </div>
                                                                        <div class="clearfix"></div>
                                                                        <div class="col-md-6 pull-right bottom_button">
                                                                            <button onclick="return SaveProjectExpense();" class="btn btn-block btn-success pull-right" style="width: 49%;" data-toggle="tab">Save</button>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="tab-pane" id="tab2">
                                                                    <br />
                                                                    <div class="row">
                                                                        <div class="dataTables_info" id="divExpense">Total Expense:1000</div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div style="padding-left: 0px;" class="col-md-12">
                                                                            <div style="border-top: none;" class="box box-default">
                                                                                <div style="padding-left: 0px;" class="box-header with-border">
                                                                                    <div class="col-md-12">
                                                                                        <div>
                                                                                            <div class="tabl_topsearch">
                                                                                                <button onclick="return SearchClick();" class="btn btn-default pull-right"><i class="fa fa-search"></i></button>
                                                                                                <button onclick="return SearchClick();" class="btn btn-default pull-left" style="margin-right: 10px;"><i class="fa fa-refresh"></i></button>
                                                                                                <input onkeypress="if ( event.keyCode == 13) return SearchClick();" style="width: 250px;" placeholder="Search Project Name" type="text" id="txtEnqSeacrh" />
                                                                                                <select onchange="return SearchClick();" id="ddlCategorySearch">
                                                                                                    <option value="-1">SELECT</option>
                                                                                                </select>
                                                                                                <div class="clearfix"></div>
                                                                                            </div>
                                                                                            <div class="data_cont_box">
                                                                                                <div id="example1_wrapper" class="dataTables_wrapper form-inline" role="grid">
                                                                                                    <table style="min-height: 200px;" class="table table-bordered table-striped dataTable">
                                                                                                        <thead>
                                                                                                            <tr role="row">
                                                                                                                <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="Browser: activate to sort column ascending">Name</th>
                                                                                                                <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="Platform(s): activate to sort column ascending">Date</th>
                                                                                                                <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="Engine version: activate to sort column ascending">Project</th>
                                                                                                                <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="CSS grade: activate to sort column ascending">Phase</th>
                                                                                                                <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="CSS grade: activate to sort column ascending">Amount</th>
                                                                                                                <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="CSS grade: activate to sort column ascending">Category</th>
                                                                                                                <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="CSS grade: activate to sort column ascending">&nbsp;</th>
                                                                                                            </tr>
                                                                                                        </thead>
                                                                                                        <tbody id="tdInnerRowProject" class="content-loading" role="alert" aria-live="polite" aria-relevant="all">
                                                                                                        </tbody>
                                                                                                    </table>
                                                                                                    <div class="row">
                                                                                                        <div class="col-xs-6">
                                                                                                            <div class="dataTables_info" id="divCountProject">0 entries</div>
                                                                                                        </div>
                                                                                                        <div class="col-xs-6">
                                                                                                            <div id="divPaginationProject" class="dataTables_paginate paging_bootstrap">
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
                                                                </div>
                                                                 <div class="tab-pane" id="tab6">
                                                                    <div class="box-header with-border">
                                                                        <div class="col-md-6 form_cols">
                                                                            <div class="form-group">
                                                                                <label>Income Name:</label>
                                                                                <input id="txtIncomeName" />
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label>Project Name:</label>
                                                                                <select id="ddlProjectIncome">
                                                                                    <option>Select</option>
                                                                                </select>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label>Phase:</label>
                                                                                <select id="ddlPhaseIncome">
                                                                                    <option>Select</option>
                                                                                </select>
                                                                            </div>

                                                                        </div>
                                                                        <div class="col-md-6 form_cols">
                                                                            <div class="form-group">
                                                                                <label>Amount:</label>
                                                                                <input type="text" id="txtIncomeAmount" class="form-control"></input>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label>Date:</label>
                                                                                <input onkeypress="return false;" type="text" id="txtIncomeDate" class="form-control"></input>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label>Comments:</label>
                                                                                <input id="txtIncomeComments" class="form-control"></input>
                                                                            </div>
                                                                        </div>
                                                                        <div class="clearfix"></div>
                                                                        <div class="col-md-6 pull-right bottom_button">
                                                                            <button onclick="return SaveProjectIncome();" class="btn btn-block btn-success pull-right" style="width: 49%;" data-toggle="tab">Save</button>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="tab-pane" id="tab7">
                                                                    <br />
                                                                    <div class="row">
                                                                        <div class="dataTables_info" id="divTotalIncome">Total Income:</div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div style="padding-left: 0px;" class="col-md-12">
                                                                            <div style="border-top: none;" class="box box-default">
                                                                                <div style="padding-left: 0px;" class="box-header with-border">
                                                                                    <div class="col-md-12">
                                                                                        <div>
                                                                                            <div class="tabl_topsearch">
                                                                                                <button onclick="return SearchIncomeClick();" class="btn btn-default pull-right"><i class="fa fa-search"></i></button>
                                                                                                <button onclick="return SearchIncomeClick();" class="btn btn-default pull-left" style="margin-right: 10px;"><i class="fa fa-refresh"></i></button>
                                                                                                <input onkeypress="if ( event.keyCode == 13) return SearchIncomeClick();" style="width: 250px;" placeholder="Search Project Name" type="text" id="txtIncomeProjectNameSearch" />
                                                                                                 <input type="button" id="btnIncomeExportToExcel"  style="border: none; height: 35px; border-radius: 3px;;background-color: #f39c12 !important;" class="btn-warning" value="Export"/>
                                                                                               <div style="width: 150px; float: right;" class="input-group date" id="divDateToIncome">
                                                                                                <input onkeypress="if ( event.keyCode == 13) return searchClick();" id="dateToIncome" placeholder="Date To" class="form-control" />
                                                                                                </div>
                                                                                                <i class="fa fa-exchange pull-right" style="margin-top: 14px; margin-right: 10px;"></i>
                                                                                                <div style="width: 150px; float: right;" class="input-group date" id="divDateFromIncome">
                                                                                                    <input onkeypress="if ( event.keyCode == 13) return searchClick();" id="dateFromIncome" placeholder="Date From" class="form-control" />
                                                                                                </div>
                                                                                                <div class="clearfix"></div>
                                                                                            </div>
                                                                                            <div class="data_cont_box">
                                                                                                <div id="Div5" class="dataTables_wrapper form-inline" role="grid">
                                                                                                    <table style="min-height: 200px;" class="table table-bordered table-striped dataTable">
                                                                                                        <thead>
                                                                                                            <tr role="row">
                                                                                                                <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="Browser: activate to sort column ascending">Name</th>
                                                                                                                <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="Platform(s): activate to sort column ascending">Date</th>
                                                                                                                <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="Engine version: activate to sort column ascending">Project</th>
                                                                                                                <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="CSS grade: activate to sort column ascending">Phase</th>
                                                                                                                <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="CSS grade: activate to sort column ascending">Amount</th>
                                                                                                                <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="CSS grade: activate to sort column ascending">&nbsp;</th>
                                                                                                            </tr>
                                                                                                        </thead>
                                                                                                        <tbody id="tdInnerRowIncome" class="content-loading" role="alert" aria-live="polite" aria-relevant="all">
                                                                                                        </tbody>
                                                                                                    </table>
                                                                                                    <div class="row">
                                                                                                        <div class="col-xs-6">
                                                                                                            <div class="dataTables_info" id="divCountIncome">0 entries</div>
                                                                                                        </div>
                                                                                                        <div class="col-xs-6">
                                                                                                            <div id="divPaginationIncome" class="dataTables_paginate paging_bootstrap">
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
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="divOtherTask" style="display: none;" class="col-md-12">
                                            <div class="panel-heading option_navnew">
                                                <span class="pull-left">
                                                    <!-- Tabs -->
                                                    <ul class="nav panel-tabs">
                                                        <li id="li2" class="active"><a href="#tab3" data-toggle="tab"><i class="fa fa-user"></i>Add Expense</a></li>
                                                        <li id="li3"><a href="#tab4" data-toggle="tab"><i class="fa fa-graduation-cap"></i>View Expense</a></li>
                                                    </ul>
                                                </span>
                                                <div class="clearfix"></div>
                                            </div>
                                            <div class="box box-default">
                                                <div>
                                                    <div>
                                                        <div class="col-md-12">
                                                            <div class="tab-content">
                                                                <div class="tab-pane active" id="tab3">
                                                                    <div class="box-header with-border">
                                                                        <div class="col-md-6 form_cols">
                                                                            <div class="form-group">
                                                                                <label>Expense Name:</label>
                                                                                <input id="txtOtherExpName" />
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label>Expense Category:</label>
                                                                                <select id="ddlOtherExpenseCategory">
                                                                                    <option value="-1">Select</option>
                                                                                </select>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label>Activity Name:</label>
                                                                                <select id="ddlOtherActivity">
                                                                                    <option>Select</option>
                                                                                </select>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-6 form_cols">
                                                                            <div class="form-group">
                                                                                <label>Amount:</label>
                                                                                <input type="text" id="txtAmountOther" class="form-control"></input>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label>Expense Date:</label>
                                                                                <input onkeypress="return false;" type="text" id="txtExpdateOther" class="form-control"></input>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label>Comments:</label>
                                                                                <input id="txtCommentsOtherExp" class="form-control"></input>
                                                                            </div>
                                                                        </div>
                                                                        <div class="clearfix"></div>
                                                                        <div class="col-md-6 pull-right bottom_button">
                                                                            <button onclick="return SaveOtherExpense();" class="btn btn-block btn-success pull-right" style="width: 49%;" data-toggle="tab">Save</button>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="tab-pane" id="tab4">
                                                                    <div class="row">
                                                                        <div style="padding-left: 0px;" class="col-md-12">
                                                                            <div style="border-top: none;" class="box box-default">
                                                                                <div style="padding-left: 0px;" class="box-header with-border">
                                                                                    <div class="col-md-12">
                                                                                        <div>
                                                                                            <div class="tabl_topsearch">
                                                                                                <button onclick="return SearchOtherClick();" class="btn btn-default pull-right"><i class="fa fa-search"></i></button>
                                                                                                <button onclick="return SearchOtherClick();" class="btn btn-default pull-left" style="margin-right: 10px;"><i class="fa fa-refresh"></i></button>
                                                                                                <input onkeypress="if ( event.keyCode == 13) return SearchOtherClick();" style="width: 250px;" placeholder="Search Task Name" type="text" id="txtOtherTaskSearch" />
                                                                                                <select onchange="return SearchOtherClick();" id="ddlOtherTaskCatSearch">
                                                                                                    <option value="-1">SELECT</option>
                                                                                                </select>
                                                                                                <div class="clearfix"></div>
                                                                                            </div>
                                                                                            <div class="data_cont_box">
                                                                                                <div id="Div3" class="dataTables_wrapper form-inline" role="grid">
                                                                                                    <table style="min-height: 200px;" class="table table-bordered table-striped dataTable">
                                                                                                        <thead>
                                                                                                            <tr role="row">
                                                                                                                <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="Browser: activate to sort column ascending">Name</th>
                                                                                                                <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="Platform(s): activate to sort column ascending">Date</th>
                                                                                                                <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="Engine version: activate to sort column ascending">Task Name</th>
                                                                                                                <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="CSS grade: activate to sort column ascending">Amount</th>
                                                                                                                <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="CSS grade: activate to sort column ascending">Category</th>
                                                                                                                <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="CSS grade: activate to sort column ascending">&nbsp;</th>
                                                                                                            </tr>
                                                                                                        </thead>
                                                                                                        <tbody id="tdInnerRowOtherTask" class="content-loading" role="alert" aria-live="polite" aria-relevant="all">
                                                                                                        </tbody>
                                                                                                    </table>
                                                                                                    <div class="row">
                                                                                                        <div class="col-xs-6">
                                                                                                            <div class="dataTables_info" id="divCountOtherTask">0 entries</div>
                                                                                                        </div>
                                                                                                        <div class="col-xs-6">
                                                                                                            <div id="divPaginationOtherTask" class="dataTables_paginate paging_bootstrap">
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
</asp:Content>
