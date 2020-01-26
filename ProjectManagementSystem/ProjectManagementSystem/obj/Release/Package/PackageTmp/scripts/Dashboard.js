$(document).ready(function () {
    if (1 == 1) {//($('#ContentPlaceHolder1_hdnRole').val() == 'Admin') {
        $('.AdminDashboard').css('display', 'block');
        $('.roleCounsellor').css('display', 'none');
        LoadAdminDashboard();
        //LoadPaymentPieChartForAdmin();
        //LoadStudntMonthlyJoiningReport();
    }
    else if ($('#ContentPlaceHolder1_hdnRole').val() == 'Counsellor') {
        $('.AdminDashboard').css('display', 'none');
        $('.roleCounsellor').css('display', 'block');
        LoadCounsellorDashboard();
    }



    function OnSuccessCall(response) {
        var jsonResponse = response.d;

        var innerHtmlValue = $('#divCentersTile').html();
        var finalHtml = '';
        for (var i = 0; i < jsonResponse.length; i++) {
            innerHtmlValue = $('#divCentersTile').html();
            innerHtmlValue = innerHtmlValue.replace('{{BranchCode}}', jsonResponse[i].BranchCode);
            innerHtmlValue = innerHtmlValue.replace('{{Branch}}', jsonResponse[i].BranchName);
            innerHtmlValue = innerHtmlValue.replace('{{EnquiryCount}}', jsonResponse[i].EnquiryCount);
            innerHtmlValue = innerHtmlValue.replace('{{StudentCount}}', jsonResponse[i].StudentCount);
            if (i % 3 == 0) {
                innerHtmlValue = innerHtmlValue.replace('{{ClassName}}', 'small-box bg-aqua');
            }
            else if (i % 3 == 1) {
                innerHtmlValue = innerHtmlValue.replace('{{ClassName}}', 'small-box bg-green');
            }
            else if (i % 3 == 2) {
                innerHtmlValue = innerHtmlValue.replace('{{ClassName}}', 'small-box bg-blue');
            }
            else {
                innerHtmlValue = innerHtmlValue.replace('{{ClassName}}', 'small-box bg-aqua');
            }
            finalHtml = finalHtml += innerHtmlValue;
        }

        $('#divRow').html(finalHtml);
    }


    function OnErrorCall(response) {
        alert(response.status + " " + response.statusText);
    }

    function bindData() {

    }

    function LoadAdminDashboard() {
        var url = "IRSService.asmx";
        $.ajax({
            type: "POST",
            url: "../Dashboard.asmx/GetCenterHeadDashBoardData",
            data: JSON.stringify({ centerId: 1 }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: OnSuccessCall,
            error: OnErrorCall
        });
    }

    function LoadStudntMonthlyJoiningReport() {

    }

    function LoadCenterHeadDashboard() {
        var url = "IRSService.asmx";
        $.ajax({
            type: "POST",
            url: "../IRSService.asmx/LoadCenterHeadDashboard",
            data: "",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: OnSuccessCall,
            error: OnErrorCall
        });
    }

    function LoadMonthlyTarget() {
        var url = "IRSService.asmx";
        $.ajax({
            type: "POST",
            url: "../IRSService.asmx/LoadMonthlyTarget",
            data: "",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                var targetPercentage = 0;
                var divWidth = '';
                targetPercentage = (response.d.TargetAchieved / response.d.MonthlyTarget) * 100;
                if (targetPercentage > 100) {
                    divWidth = '100%';
                }
                else {
                    divWidth = targetPercentage + '%';
                }
                $('#divTargetPercentage').html(divWidth + ' Target Achieved');
                $('.progress-bar').css('width', divWidth);
            },
            error: function (response) {
                alert(response.status + " " + response.statusText);
            }
        });
    }

    function LoadCounsellorDashboard() {
        LoadMonthlyTarget();
        //LoadEnquiryCount();
        google.charts.load('current', { 'packages': ['corechart'] });
        google.charts.setOnLoadCallback(drawChart);
    }
    function drawChart() {
        $.ajax({
            type: "POST",
            url: "../IRSService.asmx/LoadEnquiryCount",
            data: "",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                var data = google.visualization.arrayToDataTable([
                    ['Task', 'Hours per Day'],
                    ['Due Yesterday', response.d.PendingYesterdayCount],
                    ['Due Today', response.d.PendingTodayCount],
                    ['Pending in Future', response.d.PendingFutureCount],
                ]);

                var options = {
                    title: 'My Enquiry Pending Report'
                };
                var chart = new google.visualization.PieChart(document.getElementById('piechart'));
                chart.draw(data, options);
            },
            error: function (response) {
                alert(response.status + " " + response.statusText);
            }

        });

    }

    function LoadPaymentPieChartForAdmin() {
        google.charts.load('current', { 'packages': ['corechart'] });
        google.charts.setOnLoadCallback(drawChartPaymentAdmin);
    }

    function drawChartPaymentAdmin() {
        $.ajax({
            type: "POST",
            url: "../IRSService.asmx/LoadEnquiryCount",
            data: "",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                var data = google.visualization.arrayToDataTable([
                    ['Task', 'Hours per Day'],
                    ['Due Yesterday', response.d.PendingYesterdayCount],
                    ['Due Today', response.d.PendingTodayCount],
                    ['Pending in Future', response.d.PendingFutureCount],
                ]);

                var options = {
                    title: 'Enquiry Pending Report'
                };
                var chart = new google.visualization.PieChart(document.getElementById('divPieChartPaymentAdmin'));
                chart.draw(data, options);
            },
            error: function (response) {
                alert(response.status + " " + response.statusText);
            }

        });

    }

});