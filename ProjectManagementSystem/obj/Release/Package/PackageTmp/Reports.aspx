<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="ProjectManagementSystem.Reports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="scripts/jquery-1.10.2.min.js"></script>
    <script src="scripts/RolePermissions.js?v=5"></script>
    <script src="scripts/Reports.js?v=3"></script>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datetimepicker/4.17.47/css/bootstrap-datetimepicker.min.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datetimepicker/4.17.47/js/bootstrap-datetimepicker.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#txtAddProjectStartDate").datetimepicker({ format: 'YYYY-MM-DD HH:mm' });
            $("#txtEndDate").datetimepicker({ format: 'YYYY-MM-DD HH:mm' });
            $("#dateFromExp").datepicker({ format: 'yyyy-mm-dd' });
            $("#dateToExp").datepicker({ format: 'yyyy-mm-dd' });
            $("#dateFromEnquiry").datepicker({ format: 'yyyy-mm-dd' });
            $("#dateToEnquiry").datepicker({ format: 'yyyy-mm-dd' });
        });
    </script>
    <div class="content-wrapper" style="background-color: #fff;">
        <!-- Content Header (Page header) -->
        <section class="content-header page_titleborder">
            <h1>Reports
            </h1>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                <li class="active">Reports</li>
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
                                        <li id="liPersonal" class="active"><a href="#tab1" data-toggle="tab"><i class="fa fa-user"></i>Enquiry Report</a></li>
                                        <li id="li1"><a href="#tab2" data-toggle="tab"><i class="fa fa-graduation-cap"></i>Expense Report</a></li>
                                        <%--<li id="liCourse"><a href="#tab3" data-toggle="tab"><i class="fa fa-graduation-cap"></i>Task Report</a></li>--%>
                                        <li id="liUserTask"><a href="#tab4" data-toggle="tab"><i class="fa fa-graduation-cap"></i>User Task Report</a></li>
                                    </ul>
                                </span>
                                <div class="clearfix"></div>
                            </div>
                            <div>
                                <div class="tab-content">
                                    <div class="tab-pane active" id="tab1">
                                        <section class="content">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="box box-default">
                                                        <div class="box-header with-border">
                                                            <div class="col-md-12">
                                                                <div>
                                                                    <div class="tabl_topsearch">
                                                                        <asp:Button ID="Button1" Style="display: none;" runat="server" />
                                                                        <button onclick="return SearchClickEnquiry();" class="btn btn-default pull-right"><i class="fa fa-search"></i></button>
                                                                        <button onclick="return SearchClickEnquiry();" class="btn btn-default pull-left" style="margin-right: 10px;"><i class="fa fa-refresh"></i></button>
                                                                        <input onkeypress="if ( event.keyCode == 13) return SearchClickEnquiry();" style="width: 250px;" placeholder="Search Name Or Mobile" type="text" id="txtEnquirySearch" />

                                                                        <div style="width: 150px; float: right;" class="input-group date" id="divDateToEnquiry">
                                                                            <input onkeypress="if ( event.keyCode == 13) return searchClick();" id="dateToEnquiry" placeholder="Date To" class="form-control" />
                                                                        </div>
                                                                        <i class="fa fa-exchange pull-right" style="margin-top: 14px; margin-right: 10px;"></i>
                                                                        <div style="width: 150px; float: right;" class="input-group date" id="divDateFromEnquiry">
                                                                            <input onkeypress="if ( event.keyCode == 13) return SearchClickEnquiry();" id="dateFromEnquiry" placeholder="Date From" class="form-control" />
                                                                        </div>
                                                                        <input type="button" id="btnEnquiryExportToExcel"  style="border: none; height: 35px; border-radius: 3px;;background-color: #f39c12 !important;" class="btn-warning" value="Export">
                                                                        <div class="clearfix"></div>
                                                                    </div>
                                                                    <div class="data_cont_box">
                                                                        <div id="Div1" class="dataTables_wrapper form-inline" role="grid">
                                                                            <div class="row">
                                                                                <div class="col-xs-6">
                                                                                    <div id="Div2" class="dataTables_length">
                                                                                        <label>
                                                                                            <select onchange="searchClick(); return false;" size="1" id="ddlNoOfRecoredEnquiryReport" name="example1_length" aria-controls="example1">
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
                                                                                        <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="CSS grade: activate to sort column ascending">Response</th>
                                                                                        <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="CSS grade: activate to sort column ascending">Enquiry Date</th>
                                                                                        <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="CSS grade: activate to sort column ascending">Existing Customer</th>
                                                                                       <%-- <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="CSS grade: activate to sort column ascending">Comments</th>--%>
                                                                                    </tr>
                                                                                </thead>
                                                                                <tbody id="tdInnerRowEnquiry" class="content-loading" role="alert" aria-live="polite" aria-relevant="all">
                                                                                </tbody>
                                                                            </table>
                                                                            <div class="row">
                                                                                <div class="col-xs-6">
                                                                                    <div class="dataTables_info" id="divCountEnquiryReport">0 entries</div>
                                                                                </div>
                                                                                <div class="col-xs-6">
                                                                                    <div id="divPaginationEnquiryReport" class="dataTables_paginate paging_bootstrap">
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
                                    </div>
                                    <div class="tab-pane" id="tab2">
                                        <section class="content">
                                            <div class="row">

                                                <div class="col-md-12">
                                                    <div class="box box-default">
                                                        <div class="box-header with-border">
                                                            <br />
                                                            <div class="col-md-12">
                                                                <div class="dataTables_info" id="divExpense">Total Expense:1000</div>
                                                            </div>
                                                            <div class="col-md-12">
                                                                <div>
                                                                    <div class="tabl_topsearch">
                                                                        <asp:Button ID="btnExportC" Style="display: none;" runat="server" />
                                                                        <%--<button id="btnExport" class="btn btn-default pull-left" style="margin-right: 10px;"><i class="fa fa-file-excel-o"></i></button>--%>
                                                                        <button onclick="return SearchClick();" class="btn btn-default pull-right"><i class="fa fa-search"></i></button>
                                                                        <button onclick="return SearchClick();" class="btn btn-default pull-left" style="margin-right: 10px;"><i class="fa fa-refresh"></i></button>
                                                                        <input onkeypress="if ( event.keyCode == 13) return SearchClick();" style="width: 200px;" placeholder="Search Project or Task Name" type="text" id="txtEnqSeacrh" />
                                                                        <select onchange="return SearchClick();" id="ddlCategorySearch">
                                                                            <option value="-1">SELECT</option>
                                                                        </select>
                                                                       
                                                                        <div style="width: 100px; float: right;" class="input-group date" id="divDateTo">
                                                                            <input onkeypress="if ( event.keyCode == 13) return searchClick();" id="dateToExp" placeholder="Date To" class="form-control" />
                                                                        </div>
                                                                        <i class="fa fa-exchange pull-right" style="margin-top: 14px; margin-right: 10px;"></i>
                                                                        <div style="width: 100px; float: right;" class="input-group date" id="divDateFrom">
                                                                            <input onkeypress="if ( event.keyCode == 13) return searchClick();" id="dateFromExp" placeholder="Date From" class="form-control" />
                                                                        </div>
                                                                         <input type="button" id="btnExpenseExportToExcel"  style="border: none; height: 35px; border-radius: 3px;background-color: #f39c12 !important;" class="btn-warning" value="Export"/>
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
                                                                                        <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="Browser: activate to sort column ascending">Expense Name</th>
                                                                                        <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="Platform(s): activate to sort column ascending">Date</th>
                                                                                        <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="Engine version: activate to sort column ascending">Project/Other Task</th>
                                                                                        <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="CSS grade: activate to sort column ascending">Phase</th>
                                                                                        <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="CSS grade: activate to sort column ascending">Amount</th>
                                                                                        <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="CSS grade: activate to sort column ascending">Category</th>
                                                                                    <%--    <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="CSS grade: activate to sort column ascending">&nbsp;</th>--%>
                                                                                        <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="CSS grade: activate to sort column ascending">Comments</th>
                                                                                    </tr>
                                                                                </thead>
                                                                                <tbody id="tdInnerRowExpense" class="content-loading" role="alert" aria-live="polite" aria-relevant="all">
                                                                                </tbody>
                                                                            </table>
                                                                            <div class="row">
                                                                                <div class="col-xs-6">
                                                                                    <div class="dataTables_info" id="divCountExpense">0 entries</div>
                                                                                </div>
                                                                                <div class="col-xs-6">
                                                                                    <div id="divPaginationExpense" class="dataTables_paginate paging_bootstrap">
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
                                    </div>
                                    <div class="tab-pane" id="tab3">
                                        <section class="content">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="box box-default">
                                                        <div class="box-header with-border">
                                                            <div class="col-md-12">
                                                                <div>
                                                                    <div class="tabl_topsearch">
                                                                        <asp:Button ID="Button2" Style="display: none;" runat="server" />
                                                                        <%--<button id="btnExport" class="btn btn-default pull-left" style="margin-right: 10px;"><i class="fa fa-file-excel-o"></i></button>--%>
                                                                        <button onclick="return SearchClickTasks();" class="btn btn-default pull-right"><i class="fa fa-search"></i></button>
                                                                        <button onclick="return SearchClickTasks();" class="btn btn-default pull-left" style="margin-right: 10px;"><i class="fa fa-refresh"></i></button>
                                                                        <input onkeypress="if ( event.keyCode == 13) return SearchClickTasks();" style="width: 250px;" placeholder="Search Project Name" type="text" id="txtReportTaskProjectSearch" />
                                                                        <input onkeypress="if ( event.keyCode == 13) return SearchClickTasks();" style="width: 250px;" placeholder="Search User" type="text" id="txtReportTaskUserSearch" />
                                                                        <div style="width: 150px; float: right;" class="input-group date" id="div7">
                                                                            <input onkeypress="if ( event.keyCode == 13) return SearchClickTasks();" id="txtReportTaskStartDate" placeholder="Date To" class="form-control" />
                                                                        </div>
                                                                        <i class="fa fa-exchange pull-right" style="margin-top: 14px; margin-right: 10px;"></i>
                                                                        <div style="width: 150px; float: right;" class="input-group date" id="div8">
                                                                            <input onkeypress="if ( event.keyCode == 13) return SearchClickTasks();" id="txtReportTaskEndDate" placeholder="Date From" class="form-control" />
                                                                        </div>
                                                                       
                                                                        <div class="clearfix"></div>
                                                                    </div>
                                                                    <div class="data_cont_box">
                                                                        <div id="Div3" class="dataTables_wrapper form-inline" role="grid">
                                                                            <div class="row">
                                                                                <div class="col-xs-6">
                                                                                    <div id="Div4" class="dataTables_length">
                                                                                        <label>
                                                                                            <select onchange="searchClick(); return false;" size="1" id="Select1" name="example1_length" aria-controls="example1">
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
                                                                                        <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="Browser: activate to sort column ascending">User Name</th>
                                                                                        <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="Platform(s): activate to sort column ascending">Project</th>
                                                                                        <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="Platform(s): activate to sort column ascending">Task Name</th>
                                                                                        <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="Engine version: activate to sort column ascending">Start Date</th>
                                                                                        <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="CSS grade: activate to sort column ascending">End Date</th>
                                                                                        <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="CSS grade: activate to sort column ascending">Current Status</th>
                                                                                    </tr>
                                                                                </thead>
                                                                                <tbody id="Tbody1" class="content-loading" role="alert" aria-live="polite" aria-relevant="all">
                                                                                </tbody>
                                                                            </table>
                                                                            <div class="row">
                                                                                <div class="col-xs-6">
                                                                                    <div class="dataTables_info" id="div5">0 entries</div>
                                                                                </div>
                                                                                <div class="col-xs-6">
                                                                                    <div id="div6" class="dataTables_paginate paging_bootstrap">
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
                                    </div>
                                    <div class="tab-pane" id="tab4">
                                        <section class="content">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="box box-default">
                                                        <div class="box-header with-border">
                                                            <div class="col-md-12">
                                                                <div>
                                                                    <div class="tabl_topsearch">
                                                                        <asp:Button ID="Button3" Style="display: none;" runat="server" />
                                                                        <%--<button id="btnExport" class="btn btn-default pull-left" style="margin-right: 10px;"><i class="fa fa-file-excel-o"></i></button>--%>
                                                                        <button onclick="return SearchClickUserTasks();" class="btn btn-default pull-right"><i class="fa fa-search"></i></button>
                                                                        <button onclick="return SearchClickUserTasks();" class="btn btn-default pull-left" style="margin-right: 10px;"><i class="fa fa-refresh"></i></button>
                                                                        <select style="width: 100px;" onchange="return SearchClickUserTasks();" id="ddlUsersUserTask">
                                                                            <option value="-1">SELECT</option>
                                                                        </select>
                                                                         
                                                                        <input onkeypress="if ( event.keyCode == 13) return SearchClickUserTasks();" style="width: 125px;" placeholder="Project Name" type="text" id="txtUserTaskProjectName" />
                                                                        <input onkeypress="if ( event.keyCode == 13) return SearchClickUserTasks();" style="width: 125px;" placeholder="Phase Name" type="text" id="txtUserTaskPhaseName" />
                                                                        <input type="button" id="btnTaskExportToExcel"  style="border: none; height: 35px; border-radius: 3px;background-color: #f39c12 !important;" class="btn-warning" value="Export"/>
                                                                        <div style="width: 125px; float: right;" class="input-group date" id="div10">
                                                                            <input autocomplete="off" onkeypress="if ( event.keyCode == 13) return SearchClickTasks();" id="txtUserTaskTo" placeholder="Date To" class="form-control" />
                                                                        </div>
                                                                        <i class="fa fa-exchange pull-right" style="margin-top: 14px; margin-right: 10px;"></i>
                                                                        <div style="width: 125px; float: right;" class="input-group date" id="div11">
                                                                            <input autocomplete="off" onkeypress="if ( event.keyCode == 13) return SearchClickUserTasks();" id="txtUserTaskFrom" placeholder="Date From" class="form-control" />
                                                                        </div>
                                                                         
                                                                        <div class="clearfix"></div>
                                                                    </div>
                                                                    <div class="data_cont_box">
                                                                        <div id="Div12" class="dataTables_wrapper form-inline" role="grid">
                                                                            <div class="row">
                                                                                <div class="col-xs-6">
                                                                                    <div id="Div13" class="dataTables_length">
                                                                                        <label>
                                                                                            <select onchange="SearchClickUserTasks(); return false;" size="1" id="ddlUserTaskRecordCount" name="example1_length" aria-controls="example1">
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
                                                                                        <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="Browser: activate to sort column ascending">Project</th>
                                                                                        <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="Browser: activate to sort column ascending">Phase</th>
                                                                                        <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="Platform(s): activate to sort column ascending">Task Name</th>
                                                                                        <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="Engine version: activate to sort column ascending">Start Date</th>
                                                                                        <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="CSS grade: activate to sort column ascending">End Date</th>
                                                                                        <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="CSS grade: activate to sort column ascending">Is Completed</th>
                                                                                        <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="CSS grade: activate to sort column ascending">Completed Date</th>
                                                                                        <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="CSS grade: activate to sort column ascending"></th>
                                                                                    </tr>
                                                                                </thead>
                                                                                <tbody id="tdInnerRowUserTaskReport" class="content-loading" role="alert" aria-live="polite" aria-relevant="all">
                                                                                    <tr>
                                                                                        <td colspan="7">Please select user</td>
                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
                                                                            <div class="row">
                                                                                <div class="col-xs-12">
                                                                                    <div class="dataTables_info" id="divTotalHoursWorked">
                                                                                        Total hours Worked : 0
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-xs-6">
                                                                                    <div class="dataTables_info" id="divUserTaskReportCount">0 entries</div>
                                                                                </div>
                                                                                <div class="col-xs-6">
                                                                                    <div id="divUserTaskReportPagination" class="dataTables_paginate paging_bootstrap">
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
