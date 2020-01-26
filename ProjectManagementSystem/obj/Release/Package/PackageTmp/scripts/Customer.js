var url = "CustomerService.asmx";
var pageNumber = 1;

$(document).ready(function () {
    //RefreshCustomer();
    $("#btnCustomerExportToExcel").unbind().bind('click', function () { DownloadCustomerReport() });
});


function RefreshCustomer() {
    $.ajax({
        type: "POST",
        url: url + "/GetAllCustomers",
        data: JSON.stringify({ pageNumber: pageNumber, searchTerm: $('#txtEnqSeacrh').val(), numberOfRecords: $('#ddlNumberOFRecords').val(), rating :0 }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccessCall,
        error: OnErrorCall
    });
}
var selectedCustomer = false;
var selectedVersion = '';
var selectedCustomerId = '';
var selectedName = '';
function ShowSelectedQuotation(version, customerId, name) {
   // $("#divTax").css('display', 'none');
    selectedCustomer = true;
    selectedCustomerId = customerId;
    selectedVersion = version;
    selectedName = name;
    $.ajax({
        type: "POST",
        url: "QuotationService.asmx/GetQuotationVersionDetails",
        data: JSON.stringify({ version: version, customerId: customerId, isCustomer: true}),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: LoadSelectedQuotation,
        error: OnErrorCall
    });

}
function LoadSelectedQuotation(response) {
    var dat = JSON.parse(response.d);
    var jsonResponse = dat.Items;

    var itemContents = '';
    var cnt = 0;
    for (var i = 0; i < jsonResponse.length; i++) {
        cnt++;
        if (jsonResponse[i].IsItem) {
            itemContents = itemContents + '<tr class="trItems"><td width="3%" class="ItemIds"> <input type="hidden"   name="ItemId" value="' + jsonResponse[i].ItemId + '"><input type="hidden"   name="Tax" value="' + jsonResponse[i].Tax + '">' + cnt + '</td><td class="tdEditable" width="40%"><b>' + jsonResponse[i].Name + '</b></td><td class="tdEditable" width="7%"> ' + jsonResponse[i].Unit + '</td><td class="tdEditable tdQuantity" width="8%"> ' + jsonResponse[i].Quantity + '</td><td class="tdEditable tdUnitRate" width="8%"> ' + jsonResponse[i].UnitRate + '</td><td class="tdEditable" width="8%">' + jsonResponse[i].Rate + '</td><td class="tdEditable tdMargin" width="8%">' + jsonResponse[i].Margin + '</td><td width="8%">' + dat.GrossMargin + '</td><td class="tdEditable tdTotal" width="8%">' + jsonResponse[i].ItemTotal + ' </td></tr><tr><td width="3%"></td><td class="tdEditable" width="40%">' + jsonResponse[i].Description + '</td><td class="tdEditable" width="7%"></td><td class="tdEditable" width="8%"></td><td class="tdEditable" width="8%"></td><td class="tdEditable" width="8%"></td><td class="tdEditable" width="8%"></td><td width="8%"></td>        <td class="tdEditable" width="8%"></td></tr>';
            // cnt = 0;
        }
        //else if (cnt == 1)
        //    itemContents = itemContents + '<tr><td width="3%"></td> <td class="tdEditable" width="40%">' + jsonResponse[i].Description + '</td> <td class="tdEditable" width="7%"></td> <td class="tdEditable" width="8%"></td> <td class="tdEditable" width="8%"></td> <td class="tdEditable" width="8%"></td> <td class="tdEditable" width="8%"></td> <td width="8%"></td> <td class="tdEditable" width="8%"></td></tr >';
        else
            itemContents = itemContents + '<tr><td width="3%"></td><td class="tdEditable" width="40%"><b>' + jsonResponse[i].Name + '</b></td><td class="tdEditable" width="7%"> ' + jsonResponse[i].Unit + '</td><td class="tdEditable tdQuantity" width="8%"> ' + jsonResponse[i].Quantity + '</td><td class="tdEditable tdUnitRate" width="8%"> ' + jsonResponse[i].UnitRate + '</td><td class="tdEditable" width="8%">' + jsonResponse[i].Rate + '</td><td class="tdEditable tdMargin" width="8%">' + jsonResponse[i].Margin + '</td><td width="8%">' + dat.GrossMargin + '</td><td class="tdEditable tdTotal" width="8%">' + jsonResponse[i].ItemTotal + ' </td></tr>';

    }
    $("#divCustomerQuotation").find('#tblQuotConent').html(itemContents);
    $("#spQUotationTotal").text(dat.Total);
    $(".btnModalClose").unbind().bind('click', function () { $("#myModal").hide(); })
    DisplayBreakUp();
    LoadPopUp('', $("#divCustomerQuotation").html(), '<button type="button" class="btn btn-default btnModalClose" data-dismiss="modal">Close</button>');
    $("#btnExportToExcel").css("display", "block");
    //var currentContent = "";
    $("#myModal").find("#btnExportToExcel").unbind().bind('click', function () {
        DownloadQuotationExport();
    });
}
function DownloadQuotationExport() {
    var isSupplyAndLab = $("input[name='rdbExportType']:checked").val() == "1" ? true : false;
    $.ajax({
        type: "POST",
        url: "QuotationService.asmx/GetQuotationExport",
        data: JSON.stringify({
            customerId: selectedCustomerId, version: selectedVersion, isCustomer: selectedCustomer, isSupplyAndLabour: isSupplyAndLab, customerName: selectedName
        }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: LoadFileTask,
        error: OnErrorCall

    });
}
function LoadFileTask(response) {
    window.location = window.location.origin + '/Download/Quotation.xlsx';
}
function DisplayBreakUp() {
    var itemMargin = 0;
    var grossMargin = 0;
    var itemTotal = 0;
    var tax = 0;
    $("#tblQuotConent").find('tr').each(function () {
        if (!$(this).hasClass('trItems')) {
            var amnt = $(this).find('td:nth-child(6)').text();
            amnt = amnt == '' ? 0 : amnt;
            grossMargin = +grossMargin + GetMargin(+amnt, GetMarginColumnValue($(this), 8));
            itemMargin = itemMargin + GetMargin(+amnt, GetMarginColumnValue($(this), 7));
            itemTotal = itemTotal + GetMarginColumnValue($(this), 6);
        }
    });
    $("#spItemMargin").text(itemMargin.toFixed(2));
    $("#spGrossMargin").text(grossMargin.toFixed(2));
    $("#spItemTotal").text(itemTotal.toFixed(2));
    $("#spTax").text(0);
}
function GetMarginColumnValue(element, column) {
    var text = element.find('td:nth-child(' + column + ')').text();
    text = text == '' ? 0 : text;
    return +text;
}
function GetMargin(amount, margin) {
    return +(((margin * amount) / 100).toFixed(2));
}
function LoadPopUp(header, content, footer) {
    $("#myModal").find('.modal-header').html(header);
    $("#myModal").find('.modal-body').html(content);
    //$("#spTotal").text($("#txtTotal").val());
    $("#myModal").find('.modal-footer').find('button').html(footer);
    $("#myModal").show();
}
function OnSuccessCall(response) {
    var jsonResponse = JSON.parse(response.d);
    var className = '';
    var trObj = '';
    var totalCount = 0;
    for (var i = 0; i < jsonResponse.length; i++) {
        if (i % 2 == 0) {
            className = 'odd';
        }
        else {
            className = 'even'
        }

        totalCount = jsonResponse[i].TotalCount;

        trObj += '<tr class="' + className + '">';
        trObj += '<td class=" ">' + jsonResponse[i].Name + '</td>';
        trObj += '<td class=" ">' + jsonResponse[i].Phone + '</td>';
        trObj += ' <td class=" ">' + jsonResponse[i].Email + '</td>';
        trObj += '<td class=" ">' + jsonResponse[i].Rating + '</td>';
        trObj += '<td class=" " style="text-align: center">';
        if (isQuotationViewAccess == "True") {
            var version = jsonResponse[i].QuotationVersion;
            for (var j = 0; j < version; j++) {
                trObj = trObj + '<a class="aVersion" onclick="ShowSelectedQuotation(' + (j + 1) + ',' + jsonResponse[i].CustomerId + ',\'' + jsonResponse[i].Name + '\')">Version ' + (j + 1) + '</a> <br />';
            }
        }
        trObj += '</td>';
        trObj += '<td class=" " style="text-align: center">';
        trObj += '<div title="View Details" onclick="window.location.href=\'ViewCustomer.aspx?CustomerID=' + jsonResponse[i].CustomerId + '\';" class="btn btn-success small_sp" style="margin-right: 5px;"><i class="fa fa-search"></i></div>';
        //trObj += '<button onclick="return false;" class="btn btn-warning small_sp" style="margin-right: 5px;"><i class="fa fa-pencil"></i></button>';
        if (isCustomerDeleteAccess == 'True') {
            trObj += '<button title="Delete" onclick="return ShowDeletePopUP(\' ' + jsonResponse[i].CustomerId + '\',\' ' + jsonResponse[i].Name + '\',\' ' + jsonResponse[i].Phone + '\');" class="btn btn-danger small_sp" style="margin-right: 5px;"><i class="fa fa-trash-o"></i></button>';
        }
        trObj += ' </td></tr>';
        trObj += '';

    }
    $('#tdInnerRow').html(trObj);
    Pagination(totalCount);
}

function PrevClick() {
    pageNumber = pageNumber - 1;
    RefreshCustomer();
    return false;
}

function NextClick() {
    pageNumber = pageNumber + 1;
    RefreshCustomer();
    return false;
}

function Search() {
    RefreshCustomer();
    return false;
}

function OnErrorCall(response) {
    alert(response);
    alert(response.status + " " + response.statusText);
}

function ShowDeletePopUP(CustomerId, Name, Phone) {
    var bodyContent = '<div class="form-group"><label>Name:</label><label style="margin-left:20px;">' + Name + '</label></div>'
    bodyContent += '<div class="form-group"><label>Phone:</label><label style="margin-left:20px;">' + Phone + '</label></div>'
    var footerContent = '<a id="newCatSubmit"   class="btn btn-block btn-success pull-right" style="width: 20%;" data-toggle="tab">Delete</a><a class="btn btn-block btn-warning pull-right classEdit"  style="width: 20%; margin-top: 0px; margin-right: 2%;">Cancel</a>';
    $("#MyModal").find('.modal-content').find('.modal-header').find('h2').text('Confirm Delete Customer?');
    $("#MyModal").find('.modal-content').find('.modal-body').html(bodyContent);
    $("#MyModal").find('.modal-content').find('.modal-footer').html(footerContent);
    $("#MyModal").find('.modal-content').find('.modal-footer').find('.btn-warning').unbind().bind('click', function () { $("#MyModal").hide(); })
    $("#MyModal").find('.modal-content').find('.modal-header').find('span').unbind().bind('click', function () { $("#MyModal").hide(); })
    $("#newCatSubmit").unbind().bind('click', function () { DeleteCustomer(CustomerId) })
    $("#MyModal").show();
    return false;
}

function DeleteCustomer(CustomerId) {
    $.ajax({
        type: "POST",
        url: url + "/DeleteCustomer",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ "customerId": CustomerId }),
        success: function (result) {
            if (result.d == true) {
                RefreshCustomer();
                alert('Deleted Successfully');
                $("#MyModal").hide();
            }
            else {
            }
        }
    });
}

function DownloadCustomerReport() {
    $.ajax({
        type: "POST",
        url: url + "/GetCustomerExcelReport",
        data: JSON.stringify({ pageNumber: 1, searchTerm: $('#txtEnqSeacrh').val(), numberOfRecords: 10000, rating: 0 }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: LoadFile,
        error: OnErrorCall

    });
}
function LoadFile(response) {
    window.location = window.location.origin + '/Download/Customer.xlsx';
}

