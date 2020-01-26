<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewEnquiry.aspx.cs" Inherits="ProjectManagementSystem.ViewEnquiry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript" src="Script/jquery-3.2.1.min.js"></script>
    <script src="scripts/RolePermissions.js?v=5"></script>
    <script src="scripts/ViewEnquiry.js?v=1"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#btnAddExpCategory").unbind().bind('click', function () { ShowCategoryPopUP(); });
        });
        function ShowCategoryPopUP() {
            var bodyContent = '<div class="form-group"><label>Comment:</label>   <textarea id="txtFollowUpComment" runat="server" class="form-control" rows="3"></textarea></div>';
            bodyContent += '<div class="form-group"><label>Next Follow Up Date:</label><input type="text" onkeypress="return false;" id="txtNextDate" class="medium_wide "/></div>';
            bodyContent += '<div class="form-group"><label>Response:</label><select id="ResponseType" class="medium_wide "><option value="Positive">Positive</option><option value="Negative">Negative</option><option value="Neutral">Neutral</option></select></div>';
            var footerContent = '<a id="newCatSubmit"   class="btn btn-block btn-success pull-right" style="width: 20%;" data-toggle="tab">Add</a><a class="btn btn-block btn-warning pull-right classEdit"  style="width: 20%; margin-top: 0px; margin-right: 2%;">Close</a>';
            $("#MyModal").find('.modal-content').find('.modal-header').find('h2').text('Add Follow Up');
            $("#MyModal").find('.modal-content').find('.modal-body').html(bodyContent);
            $("#MyModal").find('.modal-content').find('.modal-footer').html(footerContent);
            $("#MyModal").find('.modal-content').find('.modal-footer').find('.btn-warning').unbind().bind('click', function () { $("#MyModal").hide(); })
            $("#MyModal").find('.modal-content').find('.modal-header').find('span').unbind().bind('click', function () { $("#MyModal").hide(); })
            $("#newCatSubmit").unbind().bind('click', function () { AddFollowUp() })
            $("#MyModal").show();
            $('#txtNextDate').datepicker({ format: 'yyyy-mm-dd' });

        }
    </script>
    <div class="content-wrapper">
        <asp:HiddenField ID="hdnEnqID" Value="1" runat="server" />
        <!-- Content Header (Page header) -->
        <section class="content-header page_titleborder">
            <h1>View Enquiry
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
                                            <label>Address:</label>
                                            <asp:Label runat="server" class="classView" ID="lblAddress" Style="font-weight: normal; color: #000;"></asp:Label>
                                        </div>
                                        <div class="form-group">
                                            <label>Phone Number:</label>
                                            <asp:Label ID="lblMobile" class="classView" runat="server" Style="font-weight: normal; color: #000;"></asp:Label>
                                        </div>
                                          <div class="form-group">
                                            <label>Comments:</label>
                                            <asp:Label ID="txtComments" class="classView" runat="server" Style="font-weight: normal; color: #000;"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-md-6 form_cols">

                                        <div class="form-group">
                                            <label>Email ID:</label>
                                            <asp:Label ID="lblEmail" runat="server" class="classView" Style="font-weight: normal; color: #000;"></asp:Label>
                                        </div>
                                        <div class="form-group">
                                            <label>Enquiry Date:</label>
                                            <asp:Label ID="lblDateOfEnquiry" class="classView" runat="server" Style="font-weight: normal; color: #000;"> </asp:Label>
                                        </div>
                                         <div class="form-group">
                                            <label>Response:</label>
                                            <asp:Label class="classView" ID="lblResponse" runat="server" Style="font-weight: normal; color: #000;"> </asp:Label>
                                        </div>
                                       <%-- <div class="form-group">
                                            <label>Assigned Staff:</label>
                                            <asp:Label class="classView" ID="lblAssignedStaff" runat="server" Style="font-weight: normal; color: #000;"> </asp:Label>
                                        </div>--%>
                                        <div class="form-group">
                                            <label>Next Followup date:</label>
                                            <asp:Label class="classView" ID="lblNextFollowup" runat="server" Style="font-weight: normal; color: #000;"> </asp:Label>
                                        </div>
                                         <div class="form-group">
                                            <label>Center:</label>
                                            <asp:Label class="classView" ID="lblCenter" runat="server" Style="font-weight: normal; color: #000;"> </asp:Label>
                                        </div>
                                    </div>
                                </div>

                                <div class="tab-pane" id="tab4">
                                    <div class="box-header with-border">
                                        <section class="content-header page_titleborder">
                                            <h1>Follow Up Details
                                            </h1>

                                        </section>
                                        <div class="col-md-12 form_cols">
                                            <div style="float: right">
                                                <i id="btnAddExpCategory" style="cursor: pointer; font-size: 24px; color: grey;" class="fa fa-plus-square"></i>
                                            </div>
                                            <asp:GridView BackColor="White" AutoGenerateColumns="false" class="table table-bordered table-striped dataTable" ID="grdFollowUp" runat="server">
                                                <Columns>
                                                    <asp:BoundField DataField="DateAdded" HeaderText="Date" />
                                                    <asp:BoundField DataField="Comments" HeaderText="Comments" />
                                                    <asp:BoundField DataField="Response" HeaderText="Response" />
                                                    <asp:BoundField DataField="NextFollowUpDate" HeaderText="Next Follow Up Date" />
                                                </Columns>
                                                <AlternatingRowStyle CssClass="odd" />
                                                <RowStyle CssClass="even" />
                                            </asp:GridView>

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
