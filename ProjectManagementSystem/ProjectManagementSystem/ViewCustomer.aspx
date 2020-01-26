<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewCustomer.aspx.cs" Inherits="ProjectManagementSystem.ViewCustomer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="scripts/RolePermissions.js?v=5"></script>
    <div class="content-wrapper">
        <asp:HiddenField ID="hdnEnqID" Value="1" runat="server" />
        <!-- Content Header (Page header) -->
        <section class="content-header page_titleborder">
            <h1>View Student Details
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
                                            <label>Name:</label>
                                            <asp:Label ID="lblName" class="classView" runat="server" Style="font-weight: normal; color: #000;">Rose Mary</asp:Label>
                                        </div>
                                        <div class="form-group">
                                            <label>Phone Number:</label>
                                            <asp:Label ID="lblMobile" class="classView" runat="server" Style="font-weight: normal; color: #000;"></asp:Label>
                                        </div>
                                        <div class="form-group">
                                            <label>Email ID:</label>
                                            <asp:Label ID="lblEmail" runat="server" class="classView" Style="font-weight: normal; color: #000;"></asp:Label>
                                        </div>
                                        <div class="form-group">
                                            <label>Address:</label>
                                            <asp:Label runat="server" class="classView" ID="lblAddress" Style="font-weight: normal; color: #000;"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-md-6 form_cols">
                                        <div class="form-group">
                                            <label>Center:</label>
                                            <asp:Label ID="lblCenter" class="classView" runat="server" Style="font-weight: normal; color: #000;"> </asp:Label>
                                        </div>
                                        <div class="form-group">
                                            <label>PIN Code:</label>
                                            <asp:Label ID="lblPIN" class="classView" runat="server" Style="font-weight: normal; color: #000;"> </asp:Label>
                                        </div>
                                        <div class="form-group">
                                            <label>Rating:</label>
                                            <asp:Label class="classView" ID="lblRating" runat="server" Style="font-weight: normal; color: #000;"> </asp:Label>
                                        </div>
                                    </div>
                                </div>

                                <div class="tab-pane" id="tab4">
                                    <div class="box-header with-border">
                                        <section class="content-header page_titleborder">
                                            <h1>Projects
                                            </h1>

                                        </section>
                                        <div class="col-md-12 form_cols">
                                        </div>
                                    </div>

                                </div>

                                <div class="tab-pane" id="Div1">
                                    <div class="box-header with-border">
                                        <section class="content-header page_titleborder">
                                            <h1>Enquiries
                                            </h1>

                                        </section>
                                        <div class="col-md-12 form_cols">
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
