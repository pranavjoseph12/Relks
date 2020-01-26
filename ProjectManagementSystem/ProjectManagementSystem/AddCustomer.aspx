<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddCustomer.aspx.cs" Inherits="ProjectManagementSystem.AddCustomer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="scripts/AddCustomer.js"></script>
    <script src="scripts/RolePermissions.js?v=5"></script>
    <div class="content-wrapper" style="background-color: #fff;">
        <!-- Content Header (Page header) -->
        <section class="content-header page_titleborder">
            <h1>Add New Student
            </h1>
            <ol class="breadcrumb">
                <li><a href="Enquiries.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
                <li class="active">Add New Student</li>
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
                                        <li id="liPersonal" class="active"><a href="#tab1" data-toggle="tab"><i class="fa fa-user"></i>Student Details</a></li>
                                    </ul>
                                </span>
                                <div class="clearfix"></div>
                            </div>
                            <div>
                                <div class="tab-content">
                                    <div class="tab-pane active" id="tab1">
                                        <div class="box-header with-border">

                                            <div class="col-md-6 form_cols">
                                                <div class="form-group">
                                                    <label>Name:</label>
                                                    <asp:TextBox ID="txtName" runat="server" class="form-control"></asp:TextBox>
                                                </div>
                                                <div class="form-group">
                                                    <label>Contact Number:</label>
                                                    <asp:TextBox ID="txtContactNumber" runat="server" class="form-control"></asp:TextBox>
                                                </div>
                                                <div class="form-group">
                                                    <label>Email ID:</label>
                                                    <asp:TextBox ID="txtEmail" runat="server" class="form-control"></asp:TextBox>
                                                </div>
                                                <div class="form-group">
                                                    <label>Address:</label>
                                                    <textarea runat="server" id="txtAddress" class="form-control" rows="3"></textarea>
                                                </div>
                                            </div>
                                            <div class="col-md-6 form_cols">
                                                 <div class="form-group">
                                                    <label>Center:</label>
                                                     <asp:DropDownList ID="ddlCourse" runat="server" AutoPostBack="false" ></asp:DropDownList>
                                 
                                                </div>
                                                <div class="form-group">
                                                    <label>PIN Code:</label>
                                                    <asp:TextBox ID="txtPinCode" onkeypress="if ( isNaN( String.fromCharCode(event.keyCode) )) return false;" runat="server" class="form-control"></asp:TextBox>
                                                </div>
                                                <div class="form-group">
                                                    <label>Rating:</label>
                                                    <asp:DropDownList  runat="server" ID="ddlRating">
                                                        <asp:ListItem Value="1">1</asp:ListItem>
                                                        <asp:ListItem Value="2">2</asp:ListItem>
                                                        <asp:ListItem Value="3">3</asp:ListItem>
                                                        <asp:ListItem Value="4">4</asp:ListItem>
                                                        <asp:ListItem Value="5">5</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="clearfix"></div>
                                            <div class="col-md-6 pull-right bottom_button">
                                                <%--<a href="#tab2" onclick="ChangeTab('2');" class="btn btn-block btn-success pull-right" style="width: 49%;" data-toggle="tab">Save</a>--%>
                                                <asp:Button ID="btnSave" OnClientClick="return Validate();" runat="server" Text="Save" class="btn btn-block btn-success pull-right" Style="width: 45%; margin-top: 0px; margin-right: 2%;" OnClick="btnSave_Click" />
                                                <button onclick="return Clear();" class="btn btn-block btn-warning pull-right" style="width: 45%; margin-top: 0px; margin-right: 2%;">Clear</button>
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
