<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="ProjectManagementSystem.Profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="scripts/Profile.js"></script>
    <script src="scripts/RolePermissions.js?v=5"></script>
    <div class="content-wrapper">
        <section class="content-header page_titleborder">
            <h1>User Profile
            </h1>
        </section>
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
                                            <asp:TextBox ID="txtName" class="classView" runat="server" Style="font-weight: normal; color: #000;"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label>Phone:</label>
                                            <asp:TextBox runat="server" class="classView" ID="txtPhone" Style="font-weight: normal; color: #000;"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label>Email(Login Id):</label>
                                            <asp:TextBox runat="server" class="classView" ID="txtEmail" Style="font-weight: normal; color: #000;"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label>Password:</label>
                                            <asp:TextBox ID="txtPassword" class="classView" runat="server" Style="font-weight: normal; color: #000;"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label>Role:</label>
                                            <asp:Label ID="lblRole" runat="server" class="classView" Style="font-weight: normal; color: #000;"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>

                                </div>
                                <br />
                            </div>
                        </div>
                    </div>

                </div>
                <div class="col-md-6 pull-right">
                    <asp:Button ID="btnAdd" OnClientClick="return ValidatePage();" runat="server" class="btn btn-block btn-success pull-right" Style="width: 49%;" type="button" Text="Update" value="Submit" OnClick="btnAdd_Click" />
                    <button class="btn btn-block btn-warning pull-right" style="width: 49%; margin-top: 0px; margin-right: 2%;">Clear</button>
                </div>
        </section>
    </div>
</asp:Content>
