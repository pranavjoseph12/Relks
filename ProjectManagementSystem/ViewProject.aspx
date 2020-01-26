<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewProject.aspx.cs" Inherits="ProjectManagementSystem.ViewProject" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="scripts/ViewProject.js"></script>
    <script src="scripts/RolePermissions.js?v=5"></script>
    <div class="content-wrapper">
        <asp:HiddenField ID="hdnProjectID" Value="1" runat="server" />
        <!-- Content Header (Page header) -->
        <section class="content-header page_titleborder">
            <h1>View Project Details
            </h1>


        </section>
        <!-- Main content -->
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-default">
                        <div class="panel panel-primary">
                            <div>
                                <div class="box-header with-border">

                                    <div class="col-md-6 form_cols">
                                        <div class="form-group">
                                            <label>Project Name:</label>
                                            <asp:Label ID="lblName" class="classView" runat="server" Style="font-weight: normal; color: #000;">Rose Mary</asp:Label>
                                        </div>
                                        <div class="form-group">
                                            <label>Address:</label>
                                            <asp:Label runat="server" class="classView" ID="lblAddress" Style="font-weight: normal; color: #000;"></asp:Label>
                                        </div>
                                        <div class="form-group">
                                            <label>PIN Code:</label>
                                            <asp:Label runat="server" class="classView" ID="lblPinCode" Style="font-weight: normal; color: #000;"></asp:Label>
                                        </div>
                                        <div class="form-group">
                                            <label>Estimate:</label>
                                            <asp:Label ID="lblEstimate" class="classView" runat="server" Style="font-weight: normal; color: #000;"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-md-6 form_cols">

                                        <div class="form-group">
                                            <label>Expected Start Date:</label>
                                            <asp:Label class="classView" ID="lblExpectedStart" runat="server" Style="font-weight: normal; color: #000;"> </asp:Label>
                                        </div>
                                        <div class="form-group">
                                            <label>Expected End Date:</label>
                                            <asp:Label ID="lblExpectedEnd" class="classView" runat="server" Style="font-weight: normal; color: #000;"> </asp:Label>
                                        </div>
                                         <div class="form-group">
                                            <label>Actual Start Date:</label>
                                            <asp:Label ID="lblActualStart" runat="server" class="classView" Style="font-weight: normal; color: #000;"></asp:Label>
                                        </div>
                                        <div class="form-group">
                                            <label>Actual End Date:</label>
                                            <asp:Label class="classView" ID="lbalActualEnd" runat="server" Style="font-weight: normal; color: #000;"> </asp:Label>
                                        </div>
                                        <div class="form-group">
                                            <label>Comments:</label>
                                            <asp:Label class="lblComments" ID="Label1" runat="server" Style="font-weight: normal; color: #000;"> </asp:Label>
                                        </div>
                                    </div>
                                </div>

                                <br />
                                <div class="box-header with-border">

                                    <div class="col-md-6 form_cols">
                                        <div class="form-group">
                                            <label>Primary Contact Person:</label>
                                            <asp:Label ID="lblPrimaryContactName" class="classView" runat="server" Style="font-weight: normal; color: #000;">Rose Mary</asp:Label>
                                        </div>
                                        <div class="form-group">
                                            <label>Primary Contact Number:</label>
                                            <asp:Label runat="server" class="classView" ID="lblPrimaryContactNumber" Style="font-weight: normal; color: #000;"></asp:Label>
                                        </div>
                                        <div class="form-group">
                                            <label>Other Contact 1:</label>
                                            <asp:Label runat="server" class="classView" ID="lblOtherContact1Name" Style="font-weight: normal; color: #000;"></asp:Label>
                                        </div>
                                        <div class="form-group">
                                            <label>Other Contact 1 Number:</label>
                                            <asp:Label ID="lblOtherContact1Number" class="classView" runat="server" Style="font-weight: normal; color: #000;"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-md-6 form_cols">

                                        <div class="form-group">
                                            <label>Other Contact 2:</label>
                                            <asp:Label ID="lblOtherContact2Name" runat="server" class="classView" Style="font-weight: normal; color: #000;"></asp:Label>
                                        </div>
                                        <div class="form-group">
                                            <label>Other Contact 2 Number:</label>
                                            <asp:Label ID="lblOtherContact2Number" class="classView" runat="server" Style="font-weight: normal; color: #000;"> </asp:Label>
                                        </div>
                                         <div class="form-group">
                                            <label>Other Contact 3:</label>
                                            <asp:Label class="classView" ID="lblOtherContact3Name" runat="server" Style="font-weight: normal; color: #000;"> </asp:Label>
                                        </div>
                                        <div class="form-group">
                                            <label>Other Contact 3 Number:</label>
                                            <asp:Label class="classView" ID="lblOtherContact3Number" runat="server" Style="font-weight: normal; color: #000;"> </asp:Label>
                                        </div>
                                    </div>
                                </div>

                                <div class="tab-pane" id="tab4">
                                    <div class="box-header with-border">
                                        <section class="content-header page_titleborder">
                                            <h1>Project Tasks
                                            </h1>

                                        </section>
                                        <div class="col-md-12 form_cols">
                                           <%-- <div style="float: right">
                                                <i id="btnAddExpCategory" style="cursor: pointer; font-size: 24px; color: grey;" class="fa fa-plus-square"></i>
                                            </div>
                                            <asp:GridView BackColor="White" AutoGenerateColumns="false" class="table table-bordered table-striped dataTable" ID="grdFollowUp" runat="server">
                                                <Columns>
                                                    <asp:BoundField DataField="TaskName" HeaderText="Task Name" />
                                                    <asp:BoundField DataField="PhaseName" HeaderText="Phase" />
                                                    <asp:BoundField DataField="StartDate" HeaderText="Start Date" />
                                                    <asp:BoundField DataField="EndDate" HeaderText="End Date" />
                                                </Columns>
                                                <AlternatingRowStyle CssClass="odd" />
                                                <RowStyle CssClass="even" />
                                            </asp:GridView>--%>

                                            <div class="data_cont_box">
                                        <div id="example1_wrapper" class="dataTables_wrapper form-inline" role="grid">
                                            <table style="min-height: 100px;" class="table table-bordered table-striped dataTable">
                                                <thead>
                                                    <tr role="row">
                                                        <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="CSS grade: activate to sort column ascending">&nbsp;</th>
                                                        <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="Browser: activate to sort column ascending">Task Name</th>
                                                        <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="Platform(s): activate to sort column ascending">Phase Name</th>
                                                        <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="Engine version: activate to sort column ascending">Start Date</th>
                                                        <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="CSS grade: activate to sort column ascending">End Date</th>
                                                    </tr>
                                                </thead>
                                                <tbody id="tdInnerRow" class="content-loading" role="alert" aria-live="polite" aria-relevant="all">
                                                </tbody>
                                            </table>
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
