<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ProjectManagementSystem.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="UTF-8">
    <title>AdminLTE 2 | IRS Login</title>
    <meta content='width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no' name='viewport'>
    <!-- Bootstrap 3.3.2 -->
    <link href="bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <!-- FontAwesome 4.3.0 -->
    <link href="https://maxcdn.bootstrapcdn.com/font-awesome/4.3.0/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <!-- Ionicons 2.0.0 -->
    <link href="http://code.ionicframework.com/ionicons/2.0.0/css/ionicons.min.css" rel="stylesheet" type="text/css" />
    <!-- Theme style -->
    <link href="dist/css/AdminLTE.min.css" rel="stylesheet" type="text/css" />
    <!-- AdminLTE Skins. Choose a skin from the css/skins 
         folder instead of downloading all of them to reduce the load. -->
    <link href="dist/css/skins/_all-skins.min.css" rel="stylesheet" type="text/css" />
    <!-- iCheck -->
    <link href="plugins/iCheck/flat/blue.css" rel="stylesheet" type="text/css" />
    <!-- Morris chart -->
    <link href="plugins/morris/morris.css" rel="stylesheet" type="text/css" />
    <!-- jvectormap -->
    <link href="plugins/jvectormap/jquery-jvectormap-1.2.2.css" rel="stylesheet" type="text/css" />
    <!-- Date Picker -->
    <link href="plugins/datepicker/datepicker3.css" rel="stylesheet" type="text/css" />
    <!-- Daterange picker -->
    <link href="plugins/daterangepicker/daterangepicker-bs3.css" rel="stylesheet" type="text/css" />
    <!-- bootstrap wysihtml5 - text editor -->
    <link href="plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.min.css" rel="stylesheet" type="text/css" />

    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.3.0/respond.min.js"></script>
    <![endif]-->
    <link href="css/custom.css" rel="stylesheet" type="text/css" />

    <link href='https://fonts.googleapis.com/css?family=Roboto' rel='stylesheet' type='text/css'>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.3.0/css/font-awesome.min.css">
    <link rel="stylesheet" href="css/login.css">
</head>
<body class="skin-blue">
    <script type="text/javascript">
        display_c();
        function display_c() {
            var refresh = 1000; // Refresh rate in milli seconds
            mytime = setTimeout('display_ct()', refresh)
        }

        function display_ct() {
            var strcount
            var x = new Date()
            var x1 = x.toUTCString();// changing the display to UTC string

            var currentTime = new Date()
            var hours = currentTime.getHours()
            var minutes = currentTime.getMinutes()

            if (minutes < 10)
                minutes = "0" + minutes

            var suffix = "am";
            if (hours >= 12) {
                suffix = "pm";
                hours = hours - 12;
            }
            if (hours == 0) {
                hours = 12;
            }

            document.getElementById('ct').innerHTML = hours + ":" + minutes + " " + suffix;
            tt = display_c();
        }
    </script>
    <style type="text/css">
        .navbar-time {
            color: #fff;
            font-size: 30px;
            text-shadow: none;
            padding: 14px 10px 12px 10px;
            font-weight: lighter;
            /*border-left: 1px solid rgba(255,255,255,.4);
	border-right: 1px solid rgba(255,255,255,.4);*/
            background: #0e4d9d;
            height: auto;
        }
    </style>

    <form id="form1" runat="server">
        <div style="background-color: #367fa9;" class="wrapper">

            <header class="main-header">
                <!-- Logo -->
                <a href="Login.aspx" class="logo">
                    <img src="images/irs_logo.png" alt="logo"></a>
                <!-- Header Navbar: style can be found in header.less -->
                <div class="navbar-header pull-left">
                    <a href="#" class="navbar-time">
                        <small>
                            <i class="fa fa-clock-o"></i>
                            <span id="ct"></span>
                        </small>
                    </a>
                </div>
            </header>

        </div>
        <!-- ./wrapper -->

        <div id="login_cotainer">
            <div id="login_mainbox">
                <div style="text-align: center; padding-right: 32px;" class="logitem_container">
                    <img src="images/company-logo.png" width="250" height="200" />
                    <h4 style="border-bottom: 1px solid #CCC; color: #478fca!important; padding: 10px;" class="header blue lighter bigger">
                        <i class="ant-icon fa fa-hand-o-right green" style="color: #69aa46!important;"></i>
                        Please Enter Your Information
                    </h4>
                    <div>
                        <div class="input-group gap_class02">
                            <asp:TextBox ID="txtUserName" style="width:100%;" runat="server" placeholder="What's your username?" value="" name="username" class="form-control"></asp:TextBox>
                        </div>

                        <div class="input-group gap_class">
                            <asp:TextBox ID="txtPassword" style="width:100%;"  runat="server" type="password" placeholder="Password" value="" name="username" class="form-control01"></asp:TextBox>
                        </div>

                        <div class="clear"></div>
                    </div>
                      <div class="error">
                            <asp:Label ID="lblError" Text="Invalid User name or Password" runat="server" ></asp:Label></div>
                    <div style="text-align: left; padding: 20px;">
                        <input style="position: relative!important; opacity: 1!important;" type="checkbox" />
                        <label>Remember Me</label>

                       <asp:Button ID="btnSubmit" runat="server" name="LogIn" type="button" class="login_button" Text="Log Me In" OnClick="btnSubmit_Click" />
                    </div>
                </div>
                <div class="toolbar clearfix">
                    <div style="padding: 9px 0 11px; background-color: #367fa9;">
                        <a onclick="alert('Please contact Admin to reset/get your password.');" style="font-size: 15px; font-weight: 400; text-shadow: 1px 0 1px rgba(0,0,0,.25); padding: 9px 0 11px 10px; color: #FE9;" href="#" data-target="#forgot-box" class="forgot-password-link">
                            <i class="ant-icon fa fa-arrow-left"></i>
                            I forgot my password
                        </a>
                    </div>

                    <div>
                    </div>
                </div>
                <div class="clear"></div>
            </div>
        </div>
    </form>
    <!-- jQuery 2.1.3 -->
    <script src="plugins/jQuery/jQuery-2.1.3.min.js"></script>
    <!-- jQuery UI 1.11.2 -->
    <script src="http://code.jquery.com/ui/1.11.2/jquery-ui.min.js" type="text/javascript"></script>
    <!-- Resolve conflict in jQuery UI tooltip with Bootstrap tooltip -->
    <script>
        $.widget.bridge('uibutton', $.ui.button);
    </script>
    <!-- Bootstrap 3.3.2 JS -->
    <script src="bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
    <!-- Morris.js charts -->
    <script src="http://cdnjs.cloudflare.com/ajax/libs/raphael/2.1.0/raphael-min.js"></script>
    <script src="plugins/morris/morris.min.js" type="text/javascript"></script>
    <!-- Sparkline -->
    <script src="plugins/sparkline/jquery.sparkline.min.js" type="text/javascript"></script>
    <!-- jvectormap -->
    <script src="plugins/jvectormap/jquery-jvectormap-1.2.2.min.js" type="text/javascript"></script>
    <script src="plugins/jvectormap/jquery-jvectormap-world-mill-en.js" type="text/javascript"></script>
    <!-- jQuery Knob Chart -->
    <script src="plugins/knob/jquery.knob.js" type="text/javascript"></script>
    <!-- daterangepicker -->
    <script src="plugins/daterangepicker/daterangepicker.js" type="text/javascript"></script>
    <!-- datepicker -->
    <script src="plugins/datepicker/bootstrap-datepicker.js" type="text/javascript"></script>
    <!-- Bootstrap WYSIHTML5 -->
    <script src="plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.all.min.js" type="text/javascript"></script>
    <!-- iCheck -->
    <script src="plugins/iCheck/icheck.min.js" type="text/javascript"></script>
    <!-- Slimscroll -->
    <script src="plugins/slimScroll/jquery.slimscroll.min.js" type="text/javascript"></script>
    <!-- FastClick -->
    <script src='plugins/fastclick/fastclick.min.js'></script>
    <!-- AdminLTE App -->
    <script src="dist/js/app.min.js" type="text/javascript"></script>

    <!-- AdminLTE dashboard demo (This is only for demo purposes) -->
    <script src="dist/js/pages/dashboard.js" type="text/javascript"></script>

    <!-- AdminLTE for demo purposes -->
    <script src="dist/js/demo.js" type="text/javascript"></script>
</body>

</html>
