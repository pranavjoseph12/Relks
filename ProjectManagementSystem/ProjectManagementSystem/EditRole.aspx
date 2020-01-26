<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditRole.aspx.cs" Inherits="ProjectManagementSystem.EditRole" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="scripts/RolePermissions.js?v=5"></script>
    <script type="text/javascript">
        function ValidatePage() {
            var isValid = true;
            var errorMessage = '';

            if ($('#ContentPlaceHolder1_txtName').val() == '') {
                //alert('Error - Course Name is required');
                errorMessage = 'Role Name is required. \n';
                isValid = false;
            }

            if (errorMessage !== '') {
                alert(errorMessage);
            }
            return isValid;
        }
    </script>

    <div class="content-wrapper">

        <!-- Content Header (Page header) -->
        <section class="content-header page_titleborder">
            <h1>Add Role
            
            </h1>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                <li class="active">Add Role</li>
            </ol>
        </section>
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-default">
                        <div class="box-header with-border">

                            <div class="col-md-4 form_cols">
                                <div class="form-group">
                                    <label>Role Name:</label>
                                    <asp:TextBox ID="txtName" runat="server" class="form-control"></asp:TextBox>
                                </div>
                                <%--<div class="form-group">
                                    <label>Access to All the Centers:</label>
                                    <label class="radio-inline">
                                        <input runat="server" name="optionsAccess" id="rbtnAccesssYes" value="Yes" type="radio" />Yes
                                    </label>
                                    <label class="radio-inline">
                                        <input runat="server" name="optionsAccess" id="rbtnAccesssNo" value="No" checked="" type="radio" />No
                                    </label>
                                </div>--%>
                            </div>
                            <div class="col-md-8 form_cols">
                                <div>
                                    <table class="table table-bordered table-striped dataTable">
                                        <thead>
                                            <tr role="row">
                                                <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="Browser: activate to sort column ascending">Action</th>
                                                <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="Platform(s): activate to sort column ascending">View</th>
                                                <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="Engine version: activate to sort column ascending">Edit</th>
                                                <th class="sorting" role="columnheader" tabindex="0" aria-controls="example1" rowspan="1" colspan="1" aria-label="Engine version: activate to sort column ascending">Delete</th>
                                            </tr>
                                        </thead>
                                        <tbody role="alert" aria-live="polite" aria-relevant="all">
                                            <tr>
                                                <td>Enquiry Management </td>
                                                <td>View
                        <asp:CheckBox ID="chkEnquiryView" name="optionEnquiry" runat="server"></asp:CheckBox>
                                                </td>
                                                <td>Add/Edit
                        <asp:CheckBox ID="chkEnquiryEdit" name="optionEnquiry" runat="server"></asp:CheckBox>
                                                </td>
                                                <td>Delete
                        <asp:CheckBox ID="chkEnquiryDelete" name="optionEnquiry" runat="server"></asp:CheckBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Customer </td>
                                                <td>View
                        <asp:CheckBox ID="chkCustomerView" name="optionCustomer" runat="server"></asp:CheckBox>
                                                </td>
                                                <td>Add/Edit
                        <asp:CheckBox ID="chkCustomerEdit" name="optionCustomer" runat="server"></asp:CheckBox>
                                                </td>
                                                <td>Delete
                        <asp:CheckBox ID="chkCustomerDelete" name="optionCustomer" runat="server"></asp:CheckBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Projects</td>
                                                <td>View
                        <asp:CheckBox ID="chkProjectsView" name="optionProjects" runat="server"></asp:CheckBox>
                                                </td>
                                                <td>Add/Edit
                        <asp:CheckBox ID="chkProjectsEdit" name="optionProjects" runat="server"></asp:CheckBox>
                                                </td>
                                                <td>Delete
                        <asp:CheckBox ID="chkProjectsDelete" name="optionProjects" runat="server"></asp:CheckBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Other Activity </td>
                                                <td>View
                        <asp:CheckBox ID="chkOtherActivityView" name="optionOtherActivity" runat="server"></asp:CheckBox>
                                                </td>
                                                <td>Add/Edit
                        <asp:CheckBox ID="chkOtherActivityEdit" name="optionOtherActivity" runat="server"></asp:CheckBox>
                                                </td>
                                                <td>Delete
                        <asp:CheckBox ID="chkOtherActivityDelete" name="optionOtherActivity" runat="server"></asp:CheckBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Task Update </td>
                                                <td>View
                        <asp:CheckBox ID="chkTaskUpdateView" name="optionTaskUpdate" runat="server"></asp:CheckBox>
                                                </td>
                                                <td>Add/Edit
                        <asp:CheckBox ID="chkTaskUpdateEdit" name="optionTaskUpdate" runat="server"></asp:CheckBox>
                                                </td>
                                                <td>Delete
                        <asp:CheckBox ID="chkTaskUpdateDelete" name="optionTaskUpdate" runat="server"></asp:CheckBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Admin </td>
                                                <td>View
                        <asp:CheckBox ID="chkAdminView" name="optionAdmin" runat="server"></asp:CheckBox>
                                                </td>
                                                <td>Add/Edit
                        <asp:CheckBox ID="chkAdminEdit" name="optionAdmin" runat="server"></asp:CheckBox>
                                                </td>
                                                <td>Delete
                        <asp:CheckBox ID="chkAdminDelete" name="optionAdmin" runat="server"></asp:CheckBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Expense </td>
                                                <td>View
                        <asp:CheckBox ID="chkExpenseView" name="optionExpense" runat="server"></asp:CheckBox>
                                                </td>
                                                <td>Add/Edit
                        <asp:CheckBox ID="chkExpenseEdit" name="optionExpense" runat="server"></asp:CheckBox>
                                                </td>
                                                <td>Delete
                        <asp:CheckBox ID="chkExpenseDelete" name="optionExpense" runat="server"></asp:CheckBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Roles </td>
                                                <td>View
                        <asp:CheckBox ID="chkRolesView" name="optionRoles" runat="server"></asp:CheckBox>
                                                </td>
                                                <td>Add/Edit
                        <asp:CheckBox ID="chkRolesEdit" name="optionRoles" runat="server"></asp:CheckBox>
                                                </td>
                                                <td>Delete
                        <asp:CheckBox ID="chkRolesDelete" name="optionRoles" runat="server"></asp:CheckBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Report </td>
                                                <td>View
                        <asp:CheckBox ID="chkReportView" name="optionReport" runat="server"></asp:CheckBox>
                                                </td>
                                                <td>Add/Edit
                        <asp:CheckBox ID="chkReportEdit" name="optionReport" runat="server"></asp:CheckBox>
                                                </td>
                                                <td>Delete
                        <asp:CheckBox ID="chkReportDelete" name="optionReport" runat="server"></asp:CheckBox>
                                                </td>
                                            </tr>
                                             <tr>
                                                <td>Assign User To Project </td>
                                                <td>View
                        <asp:CheckBox ID="chkUserAccessProjectView" name="optionAssignUserProject" runat="server"></asp:CheckBox>
                                                </td>
                                                <td>Add/Edit
                        <asp:CheckBox ID="chkUserAccessProjectEdit" name="optionAssignUserProject" runat="server"></asp:CheckBox>
                                                </td>
                                                <td>Delete
                        <asp:CheckBox ID="chkUserAccessProjectDelete" name="optionAssignUserProject" runat="server"></asp:CheckBox>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                            <div class="clearfix"></div>
                            <div class="col-md-6 pull-right">
                                <asp:Button ID="btnAdd" OnClientClick="return ValidatePage();" runat="server" class="btn btn-block btn-success pull-right" Style="width: 49%;" type="button" Text="Update" value="Submit" OnClick="btnAdd_Click" />
                                <button class="btn btn-block btn-warning pull-right" style="width: 49%; margin-top: 0px; margin-right: 2%;">Clear</button>
                            </div>
                        </div>
                    </div>
                </div>

            </div>

        </section>
    </div>
</asp:Content>
