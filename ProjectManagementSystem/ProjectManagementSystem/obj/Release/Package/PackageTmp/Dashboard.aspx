<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="ProjectManagementSystem.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <script type="text/javascript">
    </script>
    <script src="scripts/jquery-3.2.1.min.js"></script>
    <script src="scripts/RolePermissions.js"></script>
    <script src="scripts/Dashboard.js"></script>
    <style type="text/css">
        .AdminDashboard {
            display: none;
        }

        .roleCounsellor {
            display: none;
        }
    </style>

    <div class="content-wrapper" style="background-color: #fff;">
        <asp:HiddenField ID="hdnRole" Value="binu" runat="server" />
        <!-- Content Header (Page header) -->
        <section class="content-header page_titleborder">
            <h1>Dashboard
            <small>Control panel</small>
            </h1>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                <li class="active">Dashboard</li>
            </ol>
        </section>

        <!-- Main content -->
        <section class="content">

            <div class="progress roleCounsellor" style="height: 35px; background-color: red;">
                <div class="progress-bar" role="progressbar" aria-valuenow="70"
                    aria-valuemin="0" aria-valuemax="100" style="width: 0%; background-color: green">
                    <div id="divTargetPercentage" style="margin-top: 5px; font-size: 17px;">0% Target Achieved</div>
                </div>
            </div>
            <div id="divRow" class="row AdminDashboard">
                <i class="fa fa-spinner" style="font-size:50px;text-align:center;margin-left:50%;"></i>
            </div>
            <div>
                <div id="divPieChartPaymentAdmin" class="AdminDashboard" style="width: 900px; height: 500px;"></div>
            </div>
            <div>
                <div id="divStudentJoiningAdmin" class="AdminDashboard" style="width: 900px; height: 500px;"></div>
            </div>
            <div class="roleCounsellor">
                <div>
                    <div id="piechart" style="width: 900px; height: 500px;"></div>
                </div>
                <%-- <div>
                    <div id="piechart1" style="width: 900px; height: 500px;"></div>
                </div>--%>
            </div>
        </section>
        <!-- /.content -->
    </div>
    <!-- /.content-wrapper -->

    <div class="AdminDashboard">
        <div id="divCentersTile" style="display: none;">
            <div class="col-lg-4 col-xs-6">
                <!-- small box -->
                <div class="{{ClassName}}">
                    <div class="inner">
                        <h3>{{BranchCode}}</h3>
                        <p>{{Branch}}</p>

                    </div>
                    <div class="icon">
                        <i class="ion ion-android-people"></i>
                    </div>
                    <div class="dash_otr_optbox">
                        <div class="dash_otr_optbox01">Enquiries: <strong>{{EnquiryCount}}</strong></div>
                        <div class="dash_otr_optbox02">No.Students: <strong>{{StudentCount}}</strong></div>
                        <div class="clearfix"></div>
                    </div>
                    <%--<div class="small-box-footer">
                        <div class="col-xs-6" style="padding: 10px; box-sizing: 10px;"><span class="pull-left">CAMPAIGN </span><span class="label bg-yellow pull-left dash_other01">{{CampaignCount}}</span></div>
                        <div class="col-xs-6" style="padding: 10px; box-sizing: 10px;"><span class="label bg-yellow pull-right dash_other01">{{AmountDue}}</span><span class="pull-right">AMOUNT DUE</span></div>
                        <div class="clearfix"></div>
                    </div>--%>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
