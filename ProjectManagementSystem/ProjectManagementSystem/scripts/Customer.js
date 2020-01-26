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
    $("#MyModal").find('.modal-content').find('.modal-header').find('h2').text('Confirm Delete Student?');
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
    window.location = window.location.origin + '/Download/Student.xlsx';
}

