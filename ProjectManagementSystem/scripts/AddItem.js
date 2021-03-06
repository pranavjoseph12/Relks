﻿var url = "QuotationService.asmx";
var CustomerServiceUrl = "CustomerService.asmx";
var comboItemArray;
var currentItemId = 0;
var selectedComboItem = [];
var pageNumber = 1;
var numberOfRecords = 10;
var selectedItemQuot = [];
var selectedItemEdit = 0;
var QuotationItem = {
    ItemId: 0,
    Name: '',
    Description: '',
    Quantity: '',
    Unit: '',
    UnitRate: 0,
    Rate: 0,
    Margin: 0,
    Total: 0,
    Version: 0,
    IsItem: false
};
var Quotations = {
    QuotationId: 0,
    CustomerId: 0,
    Total: 0,
    GrossMargin: 0,
    Items: [],
    Version: 1,
    IsCustomer: false
}
$(document).ready(function () {
    debugger;
    //if(isQuotationEditAccess!="True")
    //    $("#li1").css('display', 'none');

    $("#btnCustSearch").bind('click', function (e) { e.preventDefault(); LoadExistingQuotations() });
    $("#btnItemSearch").bind('click', function (e) { e.preventDefault(); LoadItems() });
    $("#btnSaveQuotation").bind('click', function () { ValidateQuotation() });
    $("#txtMargin").keypress(function (e) {
        //if the letter is not digit then display error and don't type anything
        if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
            //display error message
            //  $("#errmsg").html("Digits Only").show().fadeOut("slow");
            return false;
        }
    });
    $("#btnImportQuotation").bind('click', function (e) { e.preventDefault(); ViewImport() });
    //$('#chkEnableTax').prop('checked', false);

    $("#btnSaveItem").unbind().bind('click', function (e) { ValidateSave(); });
    LoadExistingItems();
    LoadExistingCustomers();
    LoadItems();
    //$("#liAddQuotation").find('a').click();
    //if (isQuotationViewAccess != "True") {
    //    $("#liAddQuotation").find('a').click();

    //    $("#liViewQuotation").css('display', 'none');
    //}
    //else
    LoadExistingQuotations();
    $('#txtComboSearch').on('keyup', function () {
        var query = this.value;
        SearchComboItems(query);
    });
    $('#txtComboSearchQuotation').on('keyup', function () {
        var query = this.value;
        SearchComboItemsQuotation(query);
    });

    //$("#ddlCustomerNameImport").change(function () { ImportQuotations(); });
});
function ViewImport() {
    ImportQuotations();
    $('#ddlCustomerNameImport').on('change', function () {
        // var query = this.value;
        ImportQuotations();
    });
    LoadPopUp('<b>Import Quotation</b>', $("#divImportContent").html(), '<button type="button" class="btn btn-default btnModalClose" data-dismiss="modal">Close</button><button onclick=Import() type="button" class="btn btn-default" >Import</button>');
}
function EnableTax(enable) {
    if (enable) {
        $("#tblQuotConent").find('.trItems').each(function () {
            var curTax = $(this).find('.ItemIds').find('input:hidden[name=Tax]').val();
            //ApplyTax($(this).find('td:nth-child(9)'), curTax);
            ApplyTax($(this).next('tr').next('tr').find('td:nth-child(9)'), curTax);
            ApplyTax($(this).next('tr').next('tr').next('tr').find('td:nth-child(9)'), curTax);
            var supp = $(this).next('tr').next('tr').find('td:nth-child(9)').text();
            var lab = $(this).next('tr').next('tr').next('tr').find('td:nth-child(9)').text();
            $(this).find('td:nth-child(9)').text((+supp + (+lab)).toFixed(2));
        });
    }
    else {
        $("#tblQuotConent").find('.trItems').each(function () {
            var curTax = $(this).find('.ItemIds').find('input:hidden[name=Tax]').val();
           // RemoveTax($(this));
            RemoveTax($(this).next('tr').next('tr'));
            RemoveTax($(this).next('tr').next('tr').next('tr'));
            var supp = $(this).next('tr').next('tr').find('td:nth-child(9)').text();
            var lab = $(this).next('tr').next('tr').next('tr').find('td:nth-child(9)').text();
            $(this).find('td:nth-child(9)').text((+supp + (+lab)).toFixed(2));
        });
    }
    $("#spTotal").text(FindGrossTotal().toFixed(2));
}
function ApplyTax(element, tax) {
    FindGrossTotal();
    if (element.text() != '') {
        var val = element.text();
        element.text(UpdateMargin(+val, +tax));
    }
}
function RemoveTax(element) {
    if (element.find('td:nth-child(9)').text() != '') {
        var amount = element.find('td:nth-child(6)').text();
        var margin = element.find('td:nth-child(7)').text();
        var gMargin = element.find('td:nth-child(8)').text();
        gMargin = gMargin == '' ? 0 : gMargin;
        margin = margin == '' ? 0 : margin;
        element.find('td:nth-child(9)').text(UpdateMargin(+amount, (+gMargin + +margin)));
    }
}
function ImportQuotations() {
    $.ajax({
        type: "POST",
        url: url + "/GetUserVersions",
        data: JSON.stringify({ customerId: $('#ddlCustomerNameImport').val(), isCustomer: $("#ddlCustomerNameImport").find(':selected').data('type') == "C" ? true : false }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: LoadImport,
        error: OnErrorCall
    });
}
function LoadImport(response) {
    var jsonResponse = JSON.parse(response.d);
    var html = '';
    for (var i = 0; i < jsonResponse.length; i++) {
        html = html + "<option value=" + jsonResponse[i].QuotationId + ">Version " + jsonResponse[i].Version + "</option>";
    }
    $("#ddlVersionImport").html(html);

}
var itemnumberOfRecords = 10;
var itempageNumber = 1;
function LoadItems() {
    $.ajax({
        type: "POST",
        url: url + "/GetItems",
        data: JSON.stringify({ itemName: $('#txtItem').val(), numberOfRecords: itemnumberOfRecords, pageNumber: itempageNumber }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: BindItems,
        error: OnErrorCall
    });
}
function BindItems(response) {
    var jsonResponse = JSON.parse(response.d);
    var html = '';
    var totalCount = 0;
    for (var i = 0; i < jsonResponse.length; i++) {
        var versionCount = jsonResponse[i].Version;
        totalCount = jsonResponse[i].TotalCount;

        html = html + '<tr class="odd"><td class=" ">' + jsonResponse[i].Name + '</td><td class=" ">' + jsonResponse[i].IsComboItem + '</td><td class=" ">' + jsonResponse[i].Unit + '</td><td class=" ">' + jsonResponse[i].UnitRate + '</td><td class=" "><div class="btn btn-warning small_sp" style="margin-right: 5px;" onclick="return EditItem(\' ' + jsonResponse[i].ItemId + '\');"><i class="fa fa-pencil"></i></div></td></tr>';
    }
    $("#tblViewItem").find('tbody').html(html);
    debugger;
    CustomPaginationClick(totalCount, itempageNumber, 'divCountItem', 'divPaginationItem', 'PrevClickItem', 'NextClickItem');
}
function EditItem(itemId) {
    selectedItemEdit = itemId;
    $.ajax({
        type: "POST",
        url: url + "/GetItemDetails",
        data: JSON.stringify({ itemId: itemId }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: BindEditDetails,
        error: OnErrorCall
    });
}
function BindEditDetails(response) {
    var jsonResponse = JSON.parse(response.d);
    $("#liViewItem").find('a').text('Edit Item');
    $("#btnSaveItem").text('Edit');
    $("#liViewItem").find("a").click();
    $("#txtName").val(jsonResponse.Name);
    $("#txtDescription").val(jsonResponse.Description);
    $("#txtUnit").val(jsonResponse.Unit);
    $("#txtSpecification").val(jsonResponse.Specification);
    $("#txtUnitRate").val(jsonResponse.UnitRate);
    $("#txtLabour").val(jsonResponse.Labour);
    $("#txtMarginPerc").val(jsonResponse.Margin);
    $("#txtTax").val(jsonResponse.Tax);
    if (jsonResponse.IsComboItem == true) {
        $("#ddlCombo").val('Yes');
    }
    else {
        $("#ddlCombo").val('No');
    }
    var childItemEdit = jsonResponse.ChildItems;
    ShowOrHideItemsDiv();
    if (childItemEdit.length > 0) {
        $('.chkComboItems').each(function () {
            var id = $(this).attr("id").split('_')[1];
            if (childItemEdit.indexOf(+id) >= 0)
                $(this).prop("checked", true);
        });
    }
}
function LoadExistingQuotations() {
    $.ajax({
        type: "POST",
        url: url + "/GetAllQuotations",
        data: JSON.stringify({ custName: $('#txtCustomerName').val(), numberOfRecords: numberOfRecords, pageNumber: pageNumber }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: BindAllQuotations,
        error: OnErrorCall
    });
}
function BindAllQuotations(response) {
    var jsonResponse = JSON.parse(response.d);
    var html = '';
    var totalCount = 0;
    debugger;
    for (var i = 0; i < jsonResponse.length; i++) {
        var versionCount = jsonResponse[i].Version;
        totalCount = jsonResponse[i].TotalCount;
        var ver = '';
        for (var j = 0; j < versionCount; j++) {
            ver = ver + '<a class="aVersion" onclick="ShowSelectedQuotation(' + (j + 1) + ',' + jsonResponse[i].CustomerId + ',' + jsonResponse[i].IsCustomer + ',\'' + jsonResponse[i].CustomerName + '\')">Version ' + (j + 1) + '</a>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<a class="aVersionUpload" onclick="UploadSelectedQuotation(' + (j + 1) + ',' + jsonResponse[i].CustomerId + ',' + jsonResponse[i].IsCustomer + ',\'' + jsonResponse[i].CustomerName + '\')">Upload</a>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<a class="aVersionDownload" onclick="DownloadSelectedQuotation(' + (j + 1) + ',' + jsonResponse[i].CustomerId + ',' + jsonResponse[i].IsCustomer + ',\'' + jsonResponse[i].CustomerName + '\')">Download</a> <br />';
        }
        html = html + '<tr class="odd"><td class=" ">' + jsonResponse[i].CustomerName + '</td><td class=" ">' + ver + '</td><td class=" ">';
        if (isQuotationEditAccess == "True")
            html = html + '<button id="btnAddVersion"  style="border: none; border-radius: 3px;" data-cust="' + jsonResponse[i].CustomerId + '" class="btn-primary btnAddVersion">Add</button>';
        html = html + '</td></tr>';
    }
    $("#tblViewQuotation").find('tbody').html(html);
    $(".btnAddVersion").bind('click', function (e) {
        e.preventDefault();
        var custId = $(this).data("cust");
        AddVersion(custId)
    });
    CustomPagination(totalCount, pageNumber, 'divCountQuotation', 'divPaginationQuotation');
}
function PrevClick() {
    pageNumber = pageNumber - 1;
    LoadExistingQuotations();
    return false;
}

function NextClick() {
    pageNumber = pageNumber + 1;
    LoadExistingQuotations();
    return false;
}
function PrevClickItem() {
    itempageNumber = itempageNumber - 1;
    LoadItems();
    return false;
}

function NextClickItem() {
    itempageNumber = itempageNumber + 1;
    LoadItems();
    return false;
}
function AddVersion(customerId) {
    $("#liCreateQuotation").find('a').click();
    $("#ddlCustomerName").val(customerId);
    $("#ddlCustomerName").css("disabled", "disabled");
}
function ValidateQuotation() {
    var error = '';
    if ($("#txtMargin").val() > 100) {
        error = error + 'Margin should be less than 100 \n';
    }
    if (selectedItemQuot.length == 0) {
        error = error + 'Select atleast one item';
    }
    if (error == '') {
        LoadQuotationContent();
        $("#divTax").css('display', 'block');
    }
    else {
        alert(error);
    }
}
function LoadQuotationContent() {
    $.ajax({
        type: "POST",
        url: url + "/GetItemsById",
        data: JSON.stringify({ selectedItems: selectedItemQuot }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: BindQuotationItems,
        error: OnErrorCall
    });

}
function UploadSelectedQuotation(version, customerId, isCustomer, name) {
    LoadPopUp('', '<input id="fupVersion" type="file">', '<button type="button" class="btn btn-default btnModalClose" data-dismiss="modal">Close</button><button type="button" class="btn btn-default btnUploadVersion" >Upload</button>');
    $(".btnModalClose").unbind().bind('click', function () { $("#myModal").hide(); });
    $(".btnUploadVersion").unbind().bind('click', function () {
        var files = $("#fupVersion")[0].files[0];
        var data = new FormData();
        data.append("Id", customerId);
        data.append("Version", version);
        data.append("IsCustomer", isCustomer);
        data.append("file", files);
        $.ajax({

            type: "POST",
            url: url +"/UploadFile",
            data: data,
            // dataType: "json",
            contentType: false,
            processData: false,
            success: UploadSuccess,
            error: OnErrorCall

        });
    });
    
}
function UploadSuccess(data) {
    $("#myModal").hide();
    alert('Uploaded succesfully');
}
function DownloadSelectedQuotation(version, customerId, isCustomer, name) {
    $.ajax({
        type: "POST",
        url: url + "/DownloadVersion",
        data: JSON.stringify({
            customerId: customerId, version: version, isCustomer: isCustomer
        }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var result = response.d;
            if (result.length>2) {
                if (isCustomer)
                    window.location = window.location.origin + '/Upload/Customer/Quotation_' + customerId + '_' + version + '.xlsx';
                else
                    window.location = window.location.origin + '/Upload/Enquiry/Quotation_' + customerId + '_' + version + '.xlsx';
            }
            else {
                alert('no files found');
            }
        },
        error: OnErrorCall

    });
}

var selectedCustomer = false;
var selectedVersion = '';
var selectedCustomerId = '';
var selectedName = '';

function ShowSelectedQuotation(version, customerId, customerName,name) {
    selectedCustomer = customerName;
    selectedCustomerId = customerId;
    selectedVersion = version;
    selectedName = name;
    $("#divTax").css('display', 'none');
    $.ajax({
        type: "POST",
        url: url + "/GetQuotationVersionDetails",
        data: JSON.stringify({ version: version, customerId: customerId, isCustomer: customerName }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: LoadSelectedQuotation,
        error: OnErrorCall
    });

}
function Import() {

    if ($("#ddlVersionImport").val() != null) {
        $.ajax({
            type: "POST",
            url: url + "/GetQuotationById",
            data: JSON.stringify({ quotationId: $('#ddlVersionImport').val() }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: LoadSelectedImport,
            error: OnErrorCall
        });
        $("#myModal").hide();
        $("#divTax").css('display', 'block');
    }
    else {
        alert('Please select a version');
    }
}
function LoadSelectedImport(response) {
    var dat = JSON.parse(response.d);
    var jsonResponse = dat.Items;

    var itemContents = '';
    var cnt = 0;
    var curItemCount = 0; 
    for (var i = 0; i < jsonResponse.length; i++) {
        
        curItemCount++;
        var clsName = 'trSupply';
        var curMargn = $("#txtMargin").val();
       // if (cnt == 1)
        if (jsonResponse[i].IsItem) {
            cnt++;
            itemContents = itemContents + '<tr class="trItems"><td width="3%" class="ItemIds"> <input type="hidden"   name="ItemId" value="' + jsonResponse[i].ItemId + '"><input type="hidden"   name="Tax" value="' + jsonResponse[i].Tax + '">' + cnt + '</td><td class="tdEditable" width="40%"><b>' + jsonResponse[i].Name + '</b></td><td class="tdEditable" width="7%"> ' + jsonResponse[i].Unit + '</td><td class="tdEditable tdQuantity" width="8%"> ' + jsonResponse[i].Quantity + '</td><td class="tdEditable tdUnitRate" width="8%"> ' + jsonResponse[i].UnitRate + '</td><td class="tdEditable" width="8%">' + jsonResponse[i].Total + '</td><td class="tdEditable tdMargin" width="8%">' + jsonResponse[i].Margin + '</td><td width="8%">' + $("#txtMargin").val() + '</td><td class="tdEditable tdTotal" width="8%">' + FindTotal(jsonResponse[i].Total, (+curMargn), jsonResponse[i].Margin) + ' </td></tr><tr><td width="3%"></td><td class="tdEditable" width="40%">' + jsonResponse[i].Description + '</td><td class="tdEditable" width="7%"></td><td class="tdEditable" width="8%"></td><td class="tdEditable" width="8%"></td><td class="tdEditable" width="8%"></td><td class="tdEditable" width="8%"></td><td width="8%"></td>        <td class="tdEditable" width="8%"></td></tr>';
            curItemCount = 0;
        }
        //else if (cnt == 1)
        //    itemContents = itemContents + '<tr><td width="3%"></td> <td class="tdEditable" width="40%">' + jsonResponse[i].Description + '</td> <td class="tdEditable" width="7%"></td> <td class="tdEditable" width="8%"></td> <td class="tdEditable" width="8%"></td> <td class="tdEditable" width="8%"></td> <td class="tdEditable" width="8%"></td> <td width="8%"></td> <td class="tdEditable" width="8%"></td></tr >';
        else {
            if (curItemCount == 1)
                clsName = "trSupply";
            else if (curItemCount == 2)
                clsName = "trLabour";

            itemContents = itemContents + '<tr class="' + clsName + '"><td width="3%"></td><td class="tdEditable" width="40%"><b>' + jsonResponse[i].Name + '</b></td><td class="tdEditable" width="7%"> ' + jsonResponse[i].Unit + '</td><td class="tdEditable tdQuantity" width="8%"> ' + jsonResponse[i].Quantity + '</td><td class="tdEditable tdUnitRate" width="8%"> ' + jsonResponse[i].UnitRate + '</td><td class="tdEditable" width="8%">' + jsonResponse[i].Total + '</td><td class="tdEditable tdMargin" width="8%">' + jsonResponse[i].Margin + '</td><td width="8%">' + curMargn + '</td><td class="tdEditable tdTotal" width="8%">' + FindTotal(jsonResponse[i].Total, (+curMargn), jsonResponse[i].Margin) + ' </td></tr>';
        }

    }
    $("#divCustomerQuotation").find('#tblQuotConent').html(itemContents);
    LoadPopUp('<b>' + $("#ddlCustomerNameImport option:selected").text() + '</b>', $("#divCustomerQuotation").html(), '<button type="button" class="btn btn-default btnModalClose" data-dismiss="modal">Close</button><button onclick=SaveQuotation() type="button" class="btn btn-default" >Proceed</button>');
    calculateItemTotal();
    $("#spTotal").text(dat.Total);
    MakeEditable();

}
function calculateItemTotal() {
    $("#tblQuotConent").find('.trItems').each(function () {
        var supp = $(this).next('tr').next('tr').find('td:nth-child(9)').text();
        var lab = $(this).next('tr').next('tr').next('tr').find('td:nth-child(9)').text();
        $(this).find('td:nth-child(9)').text(+supp + (+lab));
    });

}
function LoadSelectedQuotation(response) {
    var dat = JSON.parse(response.d);
    var jsonResponse = dat.Items;
    $("#divTax").css('display', 'none');
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
    LoadPopUp('', $("#divCustomerQuotation").html(), '<button type="button" class="btn btn-default btnModalClose" data-dismiss="modal">Close</button>');
    $("#spTotal").text(dat.Total);
    $(".btnModalClose").unbind().bind('click', function () { $("#myModal").hide(); })
    DisplayBreakUp();
    $("#divExport").css("display", "block");
    //var currentContent = "";
    $("#btnExportToExcel").unbind().bind('click', function () {
        DownloadQuotationExport();
    });
}
function DownloadQuotationExport() {
    var isSupplyAndLab = $("input[name='rdbExportType']:checked").val() == "1" ? true : false;
    $.ajax({
        type: "POST",
        url: url + "/GetQuotationExport",
        data: JSON.stringify({
            customerId: selectedCustomerId, version: selectedVersion, isCustomer: selectedCustomer, isSupplyAndLabour: isSupplyAndLab, customerName:selectedName
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
function BindQuotationItems(response) {
    $("#divExport").css("display", "none");
    $("#divTax").css('display', 'block');
    var jsonResponse = JSON.parse(response.d);
    //$("#myModal").find('.modal-header').html('<b>Import Quotation</b>' + modalHeader);
    //$("#myModal").find('.modal-body').html($("#divImportContent").html());
    //$("#myModal").find('.modal-footer').find('button').html('<button type="button" class="btn btn-default btnModalClose" data-dismiss="modal">Close</button><button type="button" class="btn btn-default" >Proceed</button>');
    //$("#myModal").show();
    //$("#myModal").find('.modal-header').html('<b>' + $("#ddlCustomerName").val() + '</b>' + modalHeader);
    //$("#myModal").find('.modal-body').html($("#divCustomerQuotation").html());
    var itemContents = '';
    for (var i = 0; i < jsonResponse.length; i++) {
        itemContents = itemContents + '<tr class="trItems"><td width="3%" class="ItemIds"> <input type="hidden"   name="ItemId" value="' + jsonResponse[i].ItemId + '"/><input type="hidden"   name="Tax" value="' + jsonResponse[i].Tax + '"/>' + (i + 1) + '</td><td class="" width="40%"><b>' + jsonResponse[i].Name + '</b></td><td class="" width="7%"> ' + jsonResponse[i].Unit + '</td><td class="tdQuantity" width="8%">1</td><td class="tdUnitRate" width="8%"> ' + (jsonResponse[i].UnitRate + jsonResponse[i].Labour) + '</td><td class="" width="8%">' + (jsonResponse[i].UnitRate + jsonResponse[i].Labour) + '</td><td class="tdMargin" width="8%">' + jsonResponse[i].Margin + '</td><td width="8%">' + $("#txtMargin").val() + '</td><td class="tdTotal" width="8%">' + FindTotal((jsonResponse[i].UnitRate + jsonResponse[i].Labour), $("#txtMargin").val(), jsonResponse[i].Margin) + ' </td></tr><tr class="trDesc"><td width="3%"></td><td class="tdEditable" width="40%">' + jsonResponse[i].Description + '</td><td class="" width="7%"></td><td class="" width="8%"></td><td class="" width="8%"></td><td class="" width="8%"></td><td class="" width="8%"></td><td width="8%"></td>        <td class="" width="8%"></td></tr><tr class="trSupply"> <td width="3%"></td><td class="tdEditable" width="40%">Supply</td><td class="tdEditable" width="7%"></td><td class="tdEditable" width="8%">1</td><td class="tdEditable" width="8%">' + jsonResponse[i].UnitRate + '</td><td class="tdEditable" width="8%">' + jsonResponse[i].UnitRate + '</td><td class="tdEditable" width="8%">' + jsonResponse[i].Margin + '</td><td width="8%">' + $("#txtMargin").val() + '</td><td class="tdEditable" width="8%">' + FindTotal(jsonResponse[i].UnitRate, $("#txtMargin").val(), jsonResponse[i].Margin) + '</td></tr>  <tr class="trLabour"><td width = "3%"></td><td class="tdEditable" width="40%">Labour</td><td class="tdEditable" width="7%"></td><td class="tdEditable" width="8%">1</td><td class="tdEditable" width="8%">' + jsonResponse[i].Labour + '</td><td class="tdEditable" width="8%">' + jsonResponse[i].Labour + '</td><td class="tdEditable" width="8%">' + jsonResponse[i].Margin + '</td><td width="8%">' + $("#txtMargin").val() + '</td><td class="tdEditable" width="8%">' + FindTotal(jsonResponse[i].Labour, $("#txtMargin").val(), jsonResponse[i].Margin) + '</td></tr >';
    }
    $("#divCustomerQuotation").find('#tblQuotConent').html(itemContents);
    LoadPopUp('<b>' + $("#ddlCustomerName option:selected").text() + '</b>' + modalHeader, $("#divCustomerQuotation").html(), '<button type="button" class="btn btn-default btnModalClose" data-dismiss="modal">Close</button><button onclick=SaveQuotation() type="button" class="btn btn-default" >Proceed</button>');
    $("#spTotal").text(FindGrossTotal());
    //$("#myModal").find('.modal-footer').find('button').html('<button type="button" class="btn btn-default btnModalClose" data-dismiss="modal">Close</button><button type="button" class="btn btn-default" >Proceed</button>');
    //$("#myModal").find('.modal-footer').find('button').after('<button type="button" class="btn btn-default" >Proceed</button>');
    MakeEditable();
    DisplayBreakUp();
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
            grossMargin = +grossMargin + GetMargin(+amnt,GetMarginColumnValue($(this), 8));
            itemMargin = itemMargin + GetMargin(+amnt, GetMarginColumnValue($(this), 7));
            itemTotal = itemTotal + GetMarginColumnValue($(this), 6);
        }
    });
    $("#spItemMargin").text(itemMargin.toFixed(2));
    $("#spGrossMargin").text(grossMargin.toFixed(2));
    $("#spItemTotal").text(itemTotal.toFixed(2));
    $("#spTax").text(0);
}
function GetMargin(amount, margin) {
    return +(((margin * amount) / 100).toFixed(2));
}
function FindGrossTotal() {
    var amount = 0;
    $("#tblQuotConent").find('tr').each(function () {
        if (!$(this).hasClass('trItems')) {
            amount = amount + GetColumnValue($(this));
        }
    });
    return amount.toFixed(2);
}
function GetColumnValue(element) {
    var text = element.find('td:nth-child(9)').text();
    text = text == '' ? 0 : text;
    return +text;
}
function GetMarginColumnValue(element, column) {
    var text = element.find('td:nth-child(' + column+')').text();
    text = text == '' ? 0 : text;
    return +text;
}
function MakeEditable() {
    $("#divTax").css('display', 'block');
    $(".tdEditable").each(function (e) {
        var $el = $(this);
        $el.bind("click", function () {
            var $input = $('<input/>').val($el.text());
            $input.css({ "width": "70%", "height": "90%" });
            $el.html($input);
            var save = function () {
                var colNumber = $el.parent().children().index($el);
                $el.html($input.val());
                if (colNumber == 3 || colNumber == 4 || colNumber == 6 || colNumber == 5)
                    UpdatePrice($el, colNumber);
            };
            $input.one('blur', save).focus();
        });

    });
    $("#chkEnableTax").unbind().bind('click', function () {
        EnableTax($(this).is(':checked'));
    });
}
function SaveQuotation() {
    var itemArray = [];
    currentItemId = 0;
    $(".modal-body").find(".trItems").each(function (e) {
        itemArray.push(UpdateQuotationItems($(this), true));
        itemArray.push(UpdateQuotationItems($(this).next('tr').next('tr'), false));
        itemArray.push(UpdateQuotationItems($(this).next('tr').next('tr').next('tr'), false));
    });
    Quotations.CustomerId = $("#ddlCustomerName").val();
    Quotations.GrossMargin = $("#txtMargin").val();
    Quotations.Items = itemArray;
    Quotations.Total = $("#spTotal").text();
    Quotations.QuotationId = 0;
    Quotations.Version = 1;
    Quotations.IsCustomer = $("#ddlCustomerName").find(':selected').data('type') == "C" ? true : false;
    $.ajax({
        type: "POST",
        url: url + "/AddQuotation",
        data: JSON.stringify({ quotation: Quotations }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: LoadAllQuotation,
        error: OnErrorCall
    });
}
function LoadAllQuotation() {
    window.location.href = 'Quotation.aspx';
}
function UpdatePrice(element, colNumber) {
    debugger;
    var qty = element.parent('tr').find('td:nth-child(4)').text();
    var rate = element.parent('tr').find('td:nth-child(5)').text();
    var amount = qty * rate;
    var itemMargin = element.parent('tr').find('td:nth-child(7)').text();
    var grossMargin = element.parent('tr').find('td:nth-child(8)').text();
    var rate = element.parent('tr').find('td:nth-child(5)').text();
    var qty = qty == null ? 0 : qty;
    if (colNumber == 3 || colNumber == 4) {
        element.parent('tr').find('td:nth-child(6)').text(amount);

    }
    else if (colNumber == 5)
        amount = element.parent('tr').find('td:nth-child(6)').text();
    amount = UpdateMargin(+amount, (+itemMargin + +grossMargin));
    element.parent('tr').find('td:nth-child(9)').text(amount);
    if ($("#chkEnableTax").is(':checked')) {
        var curTax = 0;
        if (element.parent('tr').length > 0)
            curTax = element.parent('tr').find('.ItemIds').find('input:hidden[name=Tax]').val();
        else
            curTax = element.parent('tr').prev('.trItems').find('.ItemIds').find('input:hidden[name=Tax]').val();
        element.parent('tr').find('td:nth-child(9)').text(UpdateMargin(+amount, +curTax));
    }
    if (!element.parent('tr').hasClass('trItems'))
        UpdateItem(element, colNumber);
    $("#spTotal").text(FindGrossTotal());
    DisplayBreakUp();
}
function UpdateItem(item, index) {
    debugger;
    var className = item.parent('tr').attr('class');
    if (index == 3) {
        var qty = item.text();
        if (className == 'trSupply') {
            item.parent('tr').prevAll('.trItems:first').nextAll('.trLabour:first').find('td:nth-child(4)').text(qty);
            UpdatePrice(item.parent('tr').prevAll('.trItems:first').nextAll('.trLabour:first').find('td:nth-child(5)'), 4);
        }
        else if (className == 'trLabour') {
            item.parent('tr').prevAll('.trItems:first').nextAll('.trSupply:first').find('td:nth-child(4)').text(qty);
            UpdatePrice(item.parent('tr').prevAll('.trItems:first').nextAll('.trSupply:first').find('td:nth-child(5)'), 4);
        }
        item.parent('tr').prevAll('.trItems:first').find('td:nth-child(4)').text(qty);
        //UpdatePrice(item.parent('tr').prevAll('.trItems:first').find('td:nth-child(5)'), 4);
    }
    else if (index != 6) {
        var changedVal = item.text();
        var otherVal = '0';
        if (className == 'trSupply') {
            otherVal = item.parent('tr').prevAll('.trItems:first').nextAll('.trLabour:first').find('td:nth-child(' + (index + 1) + ')').text();
        }
        else if (className == 'trLabour') {
            otherVal = item.parent('tr').prevAll('.trItems:first').nextAll('.trSupply:first').find('td:nth-child(' + (index + 1) + ')').text();
        }
        
        item.parent('tr').prevAll('.trItems:first').find('td:nth-child(' + (index + 1) + ')').text(+changedVal + (+otherVal));
       // UpdatePrice(item.parent('tr').prevAll('.trItems:first').find('td:nth-child(' + (index+1)+')'), index);
    }
    //else if (index == 6) {
    //    var labourV = item.parent('tr').prevAll('.trItems:first').nextAll('.trSupply:first').find('td:nth-child(9)').text();
    //    var supplyV = item.parent('tr').prevAll('.trItems:first').nextAll('.trLabour:first').find('td:nth-child(9)').text();
    //    item.parent('tr').prevAll('.trItems:first').find('td:nth-child(9)').text(+labourV + (+supplyV));
    //}
    var labourV = item.parent('tr').prevAll('.trItems:first').nextAll('.trSupply:first').find('td:nth-child(9)').text();
    var supplyV = item.parent('tr').prevAll('.trItems:first').nextAll('.trLabour:first').find('td:nth-child(9)').text();
    var sumV = +labourV + (+supplyV);
    item.parent('tr').prevAll('.trItems:first').find('td:nth-child(9)').text(sumV.toFixed(2));
}
function UpdateMargin(amount, margin) {
    return ((amount + (margin * amount) / 100).toFixed(2));
}
function LoadQuotations() {
}
function UpdateQuotationItems(element, isItem) {
    QuotationItem = {};
    QuotationItem.Name = element.find('td:nth-child(2)').text();
    QuotationItem.IsItem = isItem;
    if (isItem)
        currentItemId = element.find('.ItemIds').find('[name="ItemId"]').val();
    QuotationItem.ItemId = currentItemId;
    QuotationItem.Margin = element.find('td:nth-child(7)').text() == "" ? 0 : element.find('td:nth-child(7)').text();
    QuotationItem.Quantity = element.find('td:nth-child(4)').text() == "" ? 0 : element.find('td:nth-child(4)').text();
    QuotationItem.UnitRate = element.find('td:nth-child(5)').text() == "" ? 0 : element.find('td:nth-child(5)').text();
    QuotationItem.Rate = element.find('td:nth-child(6)').text() == "" ? 0 : element.find('td:nth-child(6)').text();
    QuotationItem.Unit = element.find('td:nth-child(3)').text();
    QuotationItem.Total = element.find('td:nth-child(9)').text() == "" ? 0 : element.find('td:nth-child(9)').text();
    if (isItem)
        QuotationItem.Description = element.next('tr').find('td:nth-child(2)').text();
    else
        QuotationItem.Description = '';
    return QuotationItem;
}

function FindTotal(amount, itemMargin, grossMargin) {
    return UpdateMargin(amount, (+itemMargin + +grossMargin));
}
function LoadPopUp(header, content, footer) {
    $("#myModal").find('.modal-header').html(header);
    $("#myModal").find('.modal-body').html(content);
    //$("#spTotal").text($("#txtTotal").val());
    $("#myModal").find('.modal-footer').find('button').html(footer);
    $("#myModal").show();
}
function SearchComboItems(searchText) {
    var filteredItems = [];
    for (var i = 0; i < comboItemArray.length; i++) {
        var item = comboItemArray[i];
        //for (var prop in item) {
        //    var detail = item[prop].toString().toLowerCase();
        //    if (detail.indexOf(term) > -1) {
        //        filteredItems.push(item);
        //        break;
        //    }
        //}
        var detail = item["Name"].toString().toLowerCase();
        if (detail.indexOf(searchText) > -1) {
            filteredItems.push(item);
        }
    }
    BindItemToHtml(filteredItems);
    CheckSelectedItems();
}
function CheckSelectedItems() {
    $.each(selectedComboItem, function (index, value) {
        $("#chkCombo_" + value).prop('checked', true);
    });

}

function SearchComboItemsQuotation(searchText) {
    var filteredItems = [];
    for (var i = 0; i < comboItemArray.length; i++) {
        var item = comboItemArray[i];
        //for (var prop in item) {
        //    var detail = item[prop].toString().toLowerCase();
        //    if (detail.indexOf(term) > -1) {
        //        filteredItems.push(item);
        //        break;
        //    }
        //}
        var detail = item["Name"].toString().toLowerCase();
        if (detail.indexOf(searchText) > -1) {
            filteredItems.push(item);
        }
    }
    BindItemToHtmlQuotation(filteredItems);
    CheckSelectedItemsQuotation();
}
function CheckSelectedItemsQuotation() {
    $.each(selectedItemQuot, function (index, value) {
        $("#chkCombo_" + value).prop('checked', true);
    });
}
function LoadExistingItems() {
    $.ajax({
        type: "POST",
        url: url + "/GetAllItems",
        data: JSON.stringify({ searchTerm: $('#txtComboSearch').val() }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: BindAllItems,
        error: OnErrorCall
    });
}
function BindAllItems(response) {

    var jsonResponse = JSON.parse(response.d);
    comboItemArray = jsonResponse;
    BindItemToHtml(jsonResponse);
    BindItemToHtmlQuotation(jsonResponse);

}
function BindItemToHtml(jsonResponse) {
    var html = '';
    for (var i = 0; i < jsonResponse.length; i++) {
        if (i % 2 == 0) {
            html = html + '<div class="col-md-12">' + jsonResponse[i].Name + '<input class="chkComboItems" data-unit="' + jsonResponse[i].UnitRate + '" data-labour="' + jsonResponse[i].Labour + '" id="chkCombo_' + jsonResponse[i].ItemId + '" type="checkbox" /></div>';
        }
        else {
            html = html + '<div class="col-md-12">' + jsonResponse[i].Name + '<input data-unit="' + jsonResponse[i].UnitRate + '" data-labour="' + jsonResponse[i].Labour + '" id="chkCombo_' + jsonResponse[i].ItemId + '" type="checkbox" class="chkComboItems" /></div>';
        }
    }
    $("#divComboSelect").html(html);
    var currUnit = 0;
    var currLabour = 0;
    $('.chkComboItems').change(function (e) {
        var id = $(this).attr("id").split('_')[1];
       
        if ($(this).is(":checked")) {
            selectedComboItem.push(id);
            var unitrate = $(this).attr("data-unit");
            var labour = $(this).attr("data-labour");
            currUnit = currUnit + (+unitrate);
            currLabour = currLabour + (+labour);
        }
        else {
            var ind = selectedComboItem.indexOf(id);
            selectedComboItem.splice(ind, 1);
            var unitrate = $(this).attr("data-unit");
            var labour = $(this).attr("data-labour");
            currUnit = currUnit - (+unitrate);
            currLabour = currLabour - (+labour);
        }
        $("#txtUnitRate").val(currUnit);
        $("#txtLabour").val(currLabour);
    })
}

function BindItemToHtmlQuotation(jsonResponse) {
    var html = '';
    for (var i = 0; i < jsonResponse.length; i++) {
        if (i % 2 == 0) {
            html = html + '<div class="col-md-12">' + jsonResponse[i].Name + '<input class="chkComboItemsQuot" id="chkCombo_' + jsonResponse[i].ItemId + '" type="checkbox" /></div>';
        }
        else {
            html = html + '<div class="col-md-12">' + jsonResponse[i].Name + '<input id="chkCombo_' + jsonResponse[i].ItemId + '" type="checkbox" class="chkComboItemsQuot" /></div>';
        }
    }
    $("#divComboSelectQuot").html(html);
    $('.chkComboItemsQuot').change(function (e) {
        var id = $(this).attr("id").split('_')[1];
        if ($(this).is(":checked")) {
            selectedItemQuot.push(id);
        }
        else {
            var ind = selectedItemQuot.indexOf(id);
            selectedItemQuot.splice(ind, 1);
        }
    })
}

function ValidateSave(e) {
    var error = '';
    if ($("#txtName").val().trim() == '') {
        error += 'Item name is required. \n';
    }
    if ($("#txtUnit").val().trim() == '') {
        error += 'Unit is required. \n';
    }
    if ($("#txtUnitRate").val().trim() == '') {
        error += 'Unit rate is required. \n';
    }
    if ($("#txtLabour").val().trim() == '') {
        error += 'Labour is required. \n';
    }
    //if (projectObj.EndDate == '') {
    //    error += 'Project End Date is required. \n';
    //}
    if (error == '') {
        AddItem();
    }
    else {
        alert(error);
        e.preventDefault();
    }
}
function AddItem() {
    $.ajax({
        type: "POST",
        url: url + "/AddItem",
        data: JSON.stringify({ name: $("#txtName").val(), description: $("#txtDescription").val(), unit: $("#txtUnit").val(), specification: $("#txtSpecification").val(), unitRate: $("#txtUnitRate").val(), labour: $("#txtLabour").val(), margin: $("#txtMarginPerc").val(), tax: $("#txtTax").val(), comboItems: selectedComboItem, selectedItem: selectedItemEdit }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccessCall,
        error: OnErrorCall
    });
    return false;
}
function ClearFields() {
    //alert('Item added succesfully');
    $("#txtName").val("");
    $("#txtDescription").val("");
    $("#txtUnit").val("");
    $("#txtSpecification").val("");
    $("#txtUnitRate").val("");
    $("#txtLabour").val("");
    $("#txtMarginPerc").val("");
    $("#txtTax").val("");

}
function ShowOrHideItemsDiv() {
    if ($('#ddlCombo').val() == 'Yes') {
        $('#divItemsId').css('display', 'block');
    }
    else {
        $('#divItemsId').css('display', 'none');
    }
    return false;
}
function OnSuccessCall(response) {
    if (selectedItemEdit == 0) {
        alert('Item Added Successfully.');
    }
    else {
        alert('Item Updated Successfully.');
        $("#liViewItem").find('a').text('Add Item');
        $("#btnSaveItem").text('Add');
        $("#liAddItem").find("a").click();
    }
    LoadExistingItems();
    ClearFields();
    //window.location.href = "Projects.aspx";
}

function OnErrorCall(response) {
    alert(response);
    alert(response.status + " " + response.statusText);
}

function LoadExistingCustomers() {
    $.ajax({
        type: "POST",
        url: url + "/GetAllCustomers",
        data: JSON.stringify({ searchTerm: $('#txtComboSearch').val() }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: BindAllCustomers,
        error: OnErrorCall
    });
}
function BindAllCustomers(response) {
    var html = '';
    var jsonResponse = JSON.parse(response.d);
    for (var i = 0; i < jsonResponse.length; i++) {
        // html = html + "<option data-type=" + jsonResponse[i].Type +" value=" + jsonResponse[i].Id + ">" + jsonResponse[i].Name+"</option>";
        html = html + "<option data-type=" + jsonResponse[i].Type + " value=" + jsonResponse[i].Id + ">" + jsonResponse[i].Name + "</option>";
    }
    $("#ddlCustomerName").html(html);
    $("#ddlCustomerNameImport").html(html);
}