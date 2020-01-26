<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddEnquiry.aspx.cs" Inherits="ProjectManagementSystem.AddEnquiry" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="scripts/RolePermissions.js?v=5"></script>
    <script src="scripts/jquery-3.2.1.min.js"></script>
    <script type="text/javascript">
        function ClearAll() {
            $('#ContentPlaceHolder1_txtName').val('');
            $('#ContentPlaceHolder1_txtContactNumber').val('');
            $('#ContentPlaceHolder1_txtNextFollowUp').val('');
            $('#ContentPlaceHolder1_txtEmail').val('');
            $('#ContentPlaceHolder1_txtAddress').val('');
            $('#ContentPlaceHolder1_ddlCustomer').val('0');
            $('#ContentPlaceHolder1_txtComments').val('');
            return false;
        }
        function Validate() {
            var error = '';
            if ($('#ContentPlaceHolder1_txtName').val().trim() == '' && $('#ContentPlaceHolder1_txtContactNumber').val().trim() == '') {
                error = 'Either Name or Number is required.\n';
            }
            if ($('#ContentPlaceHolder1_txtNextFollowUp').val() == '') {
                error += 'Next follow up date is required';
            }
            else {
                var today = new Date();
                today.setHours(0, 0, 0, 0);
                d = new Date($('#ContentPlaceHolder1_txtNextFollowUp').val());
                d.setHours(0, 0, 0, 0);

                if (d < today) {
                    error += 'Next followup date should not be less than todays date';
                }
            }

            if (error != '') {
                alert(error);
                return false;
            }
            else {
                return true;
            }
        }
        $(document).ready(function () {
            $("#ContentPlaceHolder1_txtNextFollowUp").datepicker({ format: 'yyyy-mm-dd' });
            //$("#txtEndDate").datepicker({ format: 'yyyy-mm-dd' });
            
        });
    </script>
    <div class="content-wrapper"  style="background-color:#fff;">
        <asp:HiddenField ID="hdnCenterCourses" runat="server" Value="" />
        <asp:HiddenField ID="hdnCourse" runat="server" />
        <asp:HiddenField ID="hdnCenter" runat="server" />
        <!-- Content Header (Page header) -->
        <section class="content-header page_titleborder">
            <h1>Add New Enquiry
            </h1>
            <ol class="breadcrumb">
                <li><a href="Enquiries.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
                <li class="active">Add New Enquiry</li>
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
                                        <li id="liPersonal" class="active"><a href="#tab1" data-toggle="tab"><i class="fa fa-user"></i>Enquiry Details</a></li>
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
                                                    <label>Select Customer:</label>
                                                    <asp:DropDownList ID="ddlCustomer" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged"></asp:DropDownList>
                                                </div>
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
                                                    <label>Response:</label>
                                                    <select id="ddlResponse" runat="server" class="medium_wide">
                                                        <option value="Positive">Positive</option>
                                                        <option value="Negative">Negative</option>
                                                        <option value="Neutral">Neutral</option>
                                                    </select>
                                                </div>
                                                <div class="form-group">
                                                    <label>Next  Follow up date:</label>
                                                    <asp:TextBox onkeypress="return false;" ID="txtNextFollowUp" runat="server" class="form-control"></asp:TextBox>
                                                </div>
                                                <div class="form-group">
                                                    <label>Comments:</label>
                                                    <textarea id="txtComments" runat="server" class="form-control" rows="3" ></textarea>
                                                </div>
                                            </div>
                                            <div class="clearfix"></div>
                                            <div class="col-md-6 pull-right bottom_button">
                                                <%--<a href="#tab2" onclick="ChangeTab('2');" class="btn btn-block btn-success pull-right" style="width: 49%;" data-toggle="tab">Save</a>--%>
                                                <asp:Button ID="btnSave" OnClientClick="return Validate();" runat="server" Text="Save" class="btn btn-block btn-success pull-right" style="width: 45%; margin-top: 0px; margin-right: 2%;" OnClick="btnSave_Click" />
                                                <button class="btn btn-block btn-warning pull-right" onclick="return ClearAll();" style="width: 45%; margin-top: 0px; margin-right: 2%;">Clear</button>
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
