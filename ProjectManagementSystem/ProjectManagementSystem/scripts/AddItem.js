var url = "QuotationService.asmx";
var CustomerServiceUrl = "CustomerService.asmx";
var comboItemArray;
var currentItemId = 0;
var selectedComboItem = [];
var pageNumber = 1;
var numberOfRecords = 10;
var selectedItemQuot = [];
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
    Version:0,
    IsItem:false
};
var Quotations = {
    QuotationId: 0,
    CustomerId: 0,
    Total: 0,
    GrossMargin: 0,
    Items: [],
    Version:1
}
$(document).ready(function () {
   
    $("#btnCustSearch").bind('click', function (e) { e.preventDefault(); LoadExistingQuotations() });
    $("#btnSaveQuotation").bind('click', function () { ValidateQuotation() });
    $("#txtTotal, #txtMargin").keypress(function (e) {
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
            ApplyTax($(this).find('td:nth-child(9)'), curTax);
            ApplyTax($(this).next('tr').next('tr').find('td:nth-child(9)'), curTax);
            ApplyTax($(this).next('tr').next('tr').next('tr').find('td:nth-child(9)'), curTax);
        });
    }

}
function ApplyTax(element,tax) {
    if (element.text() != '') {
        var val = element.text();
        element.text(UpdateMargin(+val,+tax));
    }
}
function ImportQuotations() {
    
    $.ajax({
        type: "POST",
        url: url + "/GetUserVersions",
        data: JSON.stringify({ customerId: $('#ddlCustomerNameImport').val() }),
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
    for (var i = 0; i < jsonResponse.length; i++) {
        var versionCount = jsonResponse[i].Version;
        var ver = '';
        for (var j = 0; j < versionCount; j++) {
            ver = ver + '<a class="aVersion" onclick="ShowSelectedQuotation(' + (j + 1) + ',' + jsonResponse[i].CustomerId +')">Version ' + (j+1) + '</a> <br />';
        }
        html = html + '<tr class="odd"><td class=" ">' + jsonResponse[i].CustomerName + '</td><td class=" ">' + ver + '</td><td class=" "><button id="btnAddVersion"  style="border: none; border-radius: 3px;" data-cust="' + jsonResponse[i].CustomerId +'" class="btn-primary btnAddVersion">Add</button></td></tr >';
    }
    $("#tblViewQuotation").find('tbody').html(html);
    $(".btnAddVersion").bind('click', function (e) {
        e.preventDefault();
        var custId = $(this).data("cust");
        AddVersion(custId)
    });
}
function AddVersion(customerId) {
    $("#li1").find('a').click();
    $("#ddlCustomerName").val(customerId);
    $("#ddlCustomerName").css("disabled","disabled");
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
var selectedCustomer = '';
function ShowSelectedQuotation(version, customerId, customerName) {
    selectedCustomer = customerName;
    $("#divTax").css('display', 'none');
    $.ajax({
        type: "POST",
        url: url + "/GetQuotationVersionDetails",
        data: JSON.stringify({ version: version, customerId: customerId }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: LoadSelectedQuotation,
        error: OnErrorCall
    });

}
function Import() {
    $("#myModal").hide();
    $("#divTax").css('display', 'block');
    $.ajax({
        type: "POST",
        url: url + "/GetQuotationById",
        data: JSON.stringify({ quotationId: $('#ddlVersionImport').val() }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: LoadSelectedImport,
        error: OnErrorCall
    });
}
function LoadSelectedImport(response) {
    var dat = JSON.parse(response.d);
    var jsonResponse = dat.Items;

    var itemContents = '';
    var cnt = 0;
    for (var i = 0; i < jsonResponse.length; i++) {
        cnt++;
        if (jsonResponse[i].IsItem) {
            itemContents = itemContents + '<tr class="trItems"><td width="3%" class="ItemIds"> <input type="hidden"   name="ItemId" value="' + jsonResponse[i].ItemId + '"><input type="hidden"   name="Tax" value="' + jsonResponse[i].Tax + '">' + cnt + '</td><td class="tdEditable" width="40%"><b>' + jsonResponse[i].Name + '</b></td><td class="tdEditable" width="7%"> ' + jsonResponse[i].Unit + '</td><td class="tdEditable tdQuantity" width="8%"> ' + jsonResponse[i].Quantity + '</td><td class="tdEditable tdUnitRate" width="8%"> ' + jsonResponse[i].UnitRate + '</td><td class="tdEditable" width="8%">' + jsonResponse[i].Total + '</td><td class="tdEditable tdMargin" width="8%">' + jsonResponse[i].Margin + '</td><td width="8%">' + dat.GrossMargin + '</td><td class="tdEditable tdTotal" width="8%">' + FindTotal(jsonResponse[i].Total, dat.GrossMargin, jsonResponse[i].Margin) + ' </td></tr><tr><td width="3%"></td><td class="tdEditable" width="40%">' + jsonResponse[i].Description + '</td><td class="tdEditable" width="7%"></td><td class="tdEditable" width="8%"></td><td class="tdEditable" width="8%"></td><td class="tdEditable" width="8%"></td><td class="tdEditable" width="8%"></td><td width="8%"></td>        <td class="tdEditable" width="8%"></td></tr>';
            // cnt = 0;
        }
        //else if (cnt == 1)
        //    itemContents = itemContents + '<tr><td width="3%"></td> <td class="tdEditable" width="40%">' + jsonResponse[i].Description + '</td> <td class="tdEditable" width="7%"></td> <td class="tdEditable" width="8%"></td> <td class="tdEditable" width="8%"></td> <td class="tdEditable" width="8%"></td> <td class="tdEditable" width="8%"></td> <td width="8%"></td> <td class="tdEditable" width="8%"></td></tr >';
        else
            itemContents = itemContents + '<tr><td width="3%"></td><td class="tdEditable" width="40%"><b>' + jsonResponse[i].Name + '</b></td><td class="tdEditable" width="7%"> ' + jsonResponse[i].Unit + '</td><td class="tdEditable tdQuantity" width="8%"> ' + jsonResponse[i].Quantity + '</td><td class="tdEditable tdUnitRate" width="8%"> ' + jsonResponse[i].UnitRate + '</td><td class="tdEditable" width="8%">' + jsonResponse[i].Total + '</td><td class="tdEditable tdMargin" width="8%">' + jsonResponse[i].Margin + '</td><td width="8%">' + dat.GrossMargin + '</td><td class="tdEditable tdTotal" width="8%">' + FindTotal(jsonResponse[i].Total, dat.GrossMargin, jsonResponse[i].Margin) + ' </td></tr>';

    }
    $("#divCustomerQuotation").find('#tblQuotConent').html(itemContents);
    LoadPopUp('<b>' + $("#ddlCustomerNameImport option:selected").text() + '</b>', $("#divCustomerQuotation").html(), '<button type="button" class="btn btn-default btnModalClose" data-dismiss="modal">Close</button><button onclick=SaveQuotation() type="button" class="btn btn-default" >Proceed</button>');
    $("#spTotal").text(dat.Total);
    $("#txtMargin").val(dat.GrossMargin);
    MakeEditable();

}
function LoadSelectedQuotation(response) {
    var dat = JSON.parse(response.d);
    var jsonResponse = dat.Items;

    var itemContents = '';
    var cnt = 0;
    for (var i = 0; i < jsonResponse.length; i++) {
        cnt++;
        if (jsonResponse[i].IsItem) {
            itemContents = itemContents + '<tr class="trItems"><td width="3%" class="ItemIds"> <input type="hidden"   name="ItemId" value="' + jsonResponse[i].ItemId + '"><input type="hidden"   name="Tax" value="' + jsonResponse[i].Tax + '">' + cnt + '</td><td class="tdEditable" width="40%"><b>' + jsonResponse[i].Name + '</b></td><td class="tdEditable" width="7%"> ' + jsonResponse[i].Unit + '</td><td class="tdEditable tdQuantity" width="8%"> ' + jsonResponse[i].Quantity + '</td><td class="tdEditable tdUnitRate" width="8%"> ' + jsonResponse[i].UnitRate + '</td><td class="tdEditable" width="8%">' + jsonResponse[i].Total + '</td><td class="tdEditable tdMargin" width="8%">' + jsonResponse[i].Margin + '</td><td width="8%">' + dat.GrossMargin + '</td><td class="tdEditable tdTotal" width="8%">' + FindTotal(jsonResponse[i].Total, dat.GrossMargin, jsonResponse[i].Margin) + ' </td></tr><tr><td width="3%"></td><td class="tdEditable" width="40%">' + jsonResponse[i].Description + '</td><td class="tdEditable" width="7%"></td><td class="tdEditable" width="8%"></td><td class="tdEditable" width="8%"></td><td class="tdEditable" width="8%"></td><td class="tdEditable" width="8%"></td><td width="8%"></td>        <td class="tdEditable" width="8%"></td></tr>';
           // cnt = 0;
        }
        //else if (cnt == 1)
        //    itemContents = itemContents + '<tr><td width="3%"></td> <td class="tdEditable" width="40%">' + jsonResponse[i].Description + '</td> <td class="tdEditable" width="7%"></td> <td class="tdEditable" width="8%"></td> <td class="tdEditable" width="8%"></td> <td class="tdEditable" width="8%"></td> <td class="tdEditable" width="8%"></td> <td width="8%"></td> <td class="tdEditable" width="8%"></td></tr >';
        else
            itemContents = itemContents + '<tr><td width="3%"></td><td class="tdEditable" width="40%"><b>' + jsonResponse[i].Name + '</b></td><td class="tdEditable" width="7%"> ' + jsonResponse[i].Unit + '</td><td class="tdEditable tdQuantity" width="8%"> ' + jsonResponse[i].Quantity + '</td><td class="tdEditable tdUnitRate" width="8%"> ' + jsonResponse[i].UnitRate + '</td><td class="tdEditable" width="8%">' + jsonResponse[i].Total + '</td><td class="tdEditable tdMargin" width="8%">' + jsonResponse[i].Margin + '</td><td width="8%">' + dat.GrossMargin + '</td><td class="tdEditable tdTotal" width="8%">' + FindTotal(jsonResponse[i].Total, dat.GrossMargin, jsonResponse[i].Margin) + ' </td></tr>';

    }
    $("#divCustomerQuotation").find('#tblQuotConent').html(itemContents);
    LoadPopUp('' , $("#divCustomerQuotation").html(), '<button type="button" class="btn btn-default btnModalClose" data-dismiss="modal">Close</button>');
    $("#spTotal").text(dat.Total);
    
}
function BindQuotationItems(response) {
    $("#divTax").css('display','block');
    var jsonResponse = JSON.parse(response.d);
    //$("#myModal").find('.modal-header').html('<b>Import Quotation</b>' + modalHeader);
    //$("#myModal").find('.modal-body').html($("#divImportContent").html());
    //$("#myModal").find('.modal-footer').find('button').html('<button type="button" class="btn btn-default btnModalClose" data-dismiss="modal">Close</button><button type="button" class="btn btn-default" >Proceed</button>');
    //$("#myModal").show();
    //$("#myModal").find('.modal-header').html('<b>' + $("#ddlCustomerName").val() + '</b>' + modalHeader);
    //$("#myModal").find('.modal-body').html($("#divCustomerQuotation").html());
    var itemContents = '';
    for (var i = 0; i < jsonResponse.length; i++)
    {
        itemContents = itemContents + '<tr class="trItems"><td width="3%" class="ItemIds"> <input type="hidden"   name="ItemId" value="' + jsonResponse[i].ItemId + '"><input type="hidden"   name="Tax" value="' + jsonResponse[i].Tax + '">' + (i + 1) + '</td><td class="tdEditable" width="40%"><b>' + jsonResponse[i].Name + '</b></td><td class="tdEditable" width="7%"> ' + jsonResponse[i].Unit + '</td><td class="tdEditable tdQuantity" width="8%">1</td><td class="tdEditable tdUnitRate" width="8%"> ' + jsonResponse[i].UnitRate + '</td><td class="tdEditable" width="8%">' + jsonResponse[i].UnitRate + '</td><td class="tdEditable tdMargin" width="8%">' + jsonResponse[i].Margin + '</td><td width="8%">' + $("#txtMargin").val() + '</td><td class="tdEditable tdTotal" width="8%">' + FindTotal(jsonResponse[i].UnitRate, 0, jsonResponse[i].Margin) + ' </td></tr><tr><td width="3%"></td><td class="tdEditable" width="40%">' + jsonResponse[i].Description + '</td><td class="tdEditable" width="7%"></td><td class="tdEditable" width="8%"></td><td class="tdEditable" width="8%"></td><td class="tdEditable" width="8%"></td><td class="tdEditable" width="8%"></td><td width="8%"></td>        <td class="tdEditable" width="8%"></td></tr><tr> <td width="3%"></td><td class="tdEditable" width="40%">Supply</td><td class="tdEditable" width="7%">Mtr</td><td class="tdEditable" width="8%">25</td><td class="tdEditable" width="8%"></td><td class="tdEditable" width="8%"></td><td class="tdEditable" width="8%"></td><td width="8%">' + $("#txtMargin").val() + '</td><td class="tdEditable" width="8%"></td></tr>  <tr><td width = "3%"></t><td class="tdEditable" width="40%">Labour</td><td class="tdEditable" width="7%">Mtr</td><td class="tdEditable" width="8%">25</td><td class="tdEditable" width="8%"></td><td class="tdEditable" width="8%"></td><td class="tdEditable" width="8%"></td><td width="8%">' + $("#txtMargin").val() + '</td><td class="tdEditable" width="8%"></td></tr >';
    }
    $("#divCustomerQuotation").find('#tblQuotConent').html(itemContents);
    LoadPopUp('<b>' + $("#ddlCustomerName option:selected").text() + '</b>' + modalHeader, $("#divCustomerQuotation").html(), '<button type="button" class="btn btn-default btnModalClose" data-dismiss="modal">Close</button><button onclick=SaveQuotation() type="button" class="btn btn-default" >Proceed</button>');
    $("#spTotal").text($("#txtTotal").val());
    //$("#myModal").find('.modal-footer').find('button').html('<button type="button" class="btn btn-default btnModalClose" data-dismiss="modal">Close</button><button type="button" class="btn btn-default" >Proceed</button>');
    //$("#myModal").find('.modal-footer').find('button').after('<button type="button" class="btn btn-default" >Proceed</button>');
    MakeEditable();
    
}
function MakeEditable() {
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
    amount = UpdateMargin(amount, (+itemMargin + +grossMargin));
    element.parent('tr').find('td:nth-child(9)').text(amount);
    if ($("#chkEnableTax").is(':checked')) {
        var curTax = 0;
        if (element.parent('tr').length>0)
            curTax = element.parent('tr').find('.ItemIds').find('input:hidden[name=Tax]').val();
        else
            curTax = element.parent('tr').prev('.trItems').find('.ItemIds').find('input:hidden[name=Tax]').val();
        element.parent('tr').find('td:nth-child(9)').text(UpdateMargin(amount, +curTax));
    }
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
    return UpdateMargin(amount, (itemMargin + grossMargin));
}
function LoadPopUp(header,content,footer) {
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
            html = html + '<div class="col-md-12" style="margin-top:10px;"><div class="col-md-6">' + jsonResponse[i].Name + '<input class="chkComboItems" id="chkCombo_' + jsonResponse[i].ItemId + '" type="checkbox" /></div>';
        }
        else {
            html = html + '<div class="col-md-6">' + jsonResponse[i].Name + '<input id="chkCombo_' + jsonResponse[i].ItemId + '" type="checkbox" class="chkComboItems" /></div></div>';
        }
    }
    $("#divComboSelect").html(html);
    $('.chkComboItems').change(function (e) {
        var id = $(this).attr("id").split('_')[1];
        if ($(this).is(":checked")) {
            selectedComboItem.push(id);
        }
        else {
            var ind = selectedComboItem.indexOf(id);
            selectedComboItem.splice(ind, 1);
        }
    })
}

function BindItemToHtmlQuotation(jsonResponse) {
    var html = '';
    for (var i = 0; i < jsonResponse.length; i++) {
        if (i % 2 == 0) {
            html = html + '<div class="col-md-12" style="margin-top:10px;"><div class="col-md-6">' + jsonResponse[i].Name + '<input class="chkComboItemsQuot" id="chkCombo_' + jsonResponse[i].ItemId + '" type="checkbox" /></div>';
        }
        else {
            html = html + '<div class="col-md-6">' + jsonResponse[i].Name + '<input id="chkCombo_' + jsonResponse[i].ItemId + '" type="checkbox" class="chkComboItemsQuot" /></div></div>';
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
        data: JSON.stringify({ name: $("#txtName").val(), description: $("#txtDescription").val(), unit: $("#txtUnit").val(), specification: $("#txtSpecification").val(), unitRate: $("#txtUnitRate").val(), labour: $("#txtLabour").val(), margin: $("#txtMarginPerc").val(), tax: $("#txtTax").val(), comboItems: selectedComboItem}),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccessCall,
        error: OnErrorCall
    });
    return false;
}
function ClearFields() {
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
    alert('Item Added Successfully.');
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
        html = html + "<option value=" + jsonResponse[i].CustomerId + ">" + jsonResponse[i].Name+"</option>";
    }
    $("#ddlCustomerName").html(html);
    $("#ddlCustomerNameImport").html(html);
}