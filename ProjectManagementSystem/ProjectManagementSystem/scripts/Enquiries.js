var url = "EnquiryService.asmx";
var pageNumber = 1;

$(document).ready(function () {
    if ($('#ContentPlaceHolder1_hdnEnquiryTpe').val() == 'today') {
        $('#ContentPlaceHolder1_rbtnDueToday').attr('checked', true);
        ChangeDueType('today');
    }
    else if ($('#ContentPlaceHolder1_hdnEnquiryTpe').val() == 'overdue') {
        $('#ContentPlaceHolder1_rbtOverdue').attr('checked', true);
        ChangeDueType('overdue');
    }
    else {
        $('#ContentPlaceHolder1_rbtnDueAll').attr('checked', true);
        ChangeDueType('overdue');
    }
    if (isEnquiryEditAccess == 'False') {
        $('#divAddEnquiryButton').css('display', 'none');
    }
    $("#btnTestVox").unbind().bind('click', function () {
        $.ajax({
            type: "POST",
            url: "http://localhost/ProjectManagementSystem/VoxBayBridge.asmx/InitiateIncomingCall",
            data: JSON.stringify({ calledNumber: '1234567', callerNumber: '8910', CallUUID: 'VBT2'}),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: OnSuccessCall1,
            error: OnErrorCall
        });
    });
    //Search();
});
function OnSuccessCall1(response) {
}
function Search() {
    if ($('#ContentPlaceHolder1_rbtnDueToday').is(':checked') == true) {
        RefreshEnquiries('today');
    }
    else if ($('#ContentPlaceHolder1_rbtOverdue').is(':checked') == true) {
        RefreshEnquiries('overdue');
    }
    else {
        RefreshEnquiries('all');
    }
    return false;
}

function SearchClick() {
    pageNumber = 1;
    Search();
    return false;
}

function ChangeDueType(enquiryType) {
    pageNumber = 1;
    RefreshEnquiries(enquiryType);
}

function RefreshEnquiries(enquiryType) {
    $.ajax({
        type: "POST",
        url: url + "/GetAllEnquiries",
        data: JSON.stringify({ enquiryType: enquiryType, pageNumber: pageNumber, searchTerm: $('#txtEnqSeacrh').val(), numberOfRecords: $('#ddlNumberOFRecords').val() }),
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
    var existingCustomer = false;
    for (var i = 0; i < jsonResponse.length; i++) {
        existingCustomer = false;
        if (i % 2 == 0) {
            className = 'odd';
        }
        else {
            className = 'even'
        }

        totalCount = jsonResponse[i].TotalEnquiries;

        trObj += '<tr class="' + className + '">';
        trObj += '<td class=" ">' + jsonResponse[i].Name + '</td>';
        trObj += '<td class=" ">' + jsonResponse[i].Phone + '</td>';
        trObj += ' <td class=" ">' + jsonResponse[i].Email + '</td>';
        trObj += '<td class=" ">' + jsonResponse[i].Response + '</td>';
        trObj += '<td class=" ">' + jsonResponse[i].EnquiryDate + '</td>';
        if (jsonResponse[i].ExistingCustomerId > 0) {
            existingCustomer = true;
            trObj += '<td class=" "><a onclick="window.Open(ViewCustomer.aspx?CustomerID=' + jsonResponse[i].ExistingCustomerId + ');"  href="#">Yes</a></td>';
        }
        else {
            trObj += '<td class=" ">No</td>';
        }
        trObj += '<td class=" " style="text-align: center">';
        trObj += '<div title="View Details" onclick="window.location.href=\'ViewEnquiry.aspx?EnquiryID=' + jsonResponse[i].EnquiryId + '&IsEditMode=false\';" class="btn btn-success small_sp" style="margin-right: 5px;"><i class="fa fa-search"></i></div>';
        //trObj += '<button onclick="return false;" class="btn btn-warning small_sp" style="margin-right: 5px;"><i class="fa fa-pencil"></i></button>';
        if (isEnquiryDeleteAccess == 'True') {
            trObj += '<button title="Delete" onclick="return ShowDeletePopUP(\' ' + jsonResponse[i].EnquiryId + '\',\' ' + jsonResponse[i].Name + '\',\' ' + jsonResponse[i].Phone + '\');" class="btn btn-danger small_sp" style="margin-right: 5px;"><i class="fa fa-trash-o"></i></button>';
        }
        if (existingCustomer) {
            trObj += '<div title="Convert to Student" class="btn btn-primary small_sp" style="margin-right: 5px;"><i class="fa fa-exchange"></i></button>';
        }
        else if (isCustomerEditAccess == 'True') {
            trObj += '<div title="Convert to Student" onclick="window.location.href=\'AddCustomer.aspx?EnquiryID=' + jsonResponse[i].EnquiryId + '\';" class="btn btn-primary small_sp" style="margin-right: 5px;"><i class="fa fa-exchange"></i></button>';
        }
        
        trObj += ' </td></tr>';
        trObj += '';

    }
    $('#tdInnerRow').html(trObj);
    Pagination(totalCount);
}

function OnErrorCall(response) {
    alert(response);
    alert(response.status + " " + response.statusText);
}

function ShowDeletePopUP(EnquiryId, EnquiryName, Phone) {
    var bodyContent = '<div class="form-group"><label>Name:</label><label style="margin-left:20px;">' + EnquiryName + '</label></div>'
    bodyContent += '<div class="form-group"><label>Phone:</label><label style="margin-left:20px;">' + Phone + '</label></div>'
    var footerContent = '<a id="newCatSubmit"   class="btn btn-block btn-success pull-right" style="width: 20%;" data-toggle="tab">Delete</a><a class="btn btn-block btn-warning pull-right classEdit"  style="width: 20%; margin-top: 0px; margin-right: 2%;">Cancel</a>';
    $("#MyModal").find('.modal-content').find('.modal-header').find('h2').text('Confirm Delete Enquiry?');
    $("#MyModal").find('.modal-content').find('.modal-body').html(bodyContent);
    $("#MyModal").find('.modal-content').find('.modal-footer').html(footerContent);
    $("#MyModal").find('.modal-content').find('.modal-footer').find('.btn-warning').unbind().bind('click', function () { $("#MyModal").hide(); })
    $("#MyModal").find('.modal-content').find('.modal-header').find('span').unbind().bind('click', function () { $("#MyModal").hide(); })
    $("#newCatSubmit").unbind().bind('click', function () { DeleteEnquiry(EnquiryId) })
    $("#MyModal").show();
    return false;
}

function DeleteEnquiry(enquiryID) {
    $("#MyModal").hide();
    $.ajax({
        type: "POST",
        url: url + "/DeleteEnquiry",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ "enquiryID": enquiryID }),
        success: function (result) {
            if (result.d == true) {
                pageNumber = 1;
                Search();
                alert('Delete Success');
            }
            else {
                alert("Category already exist");
            }

        }
    });
}

function PrevClick() {
    pageNumber = pageNumber - 1;
    Search();
    return false;
}

function NextClick() {
    pageNumber = pageNumber + 1;
    Search();
    return false;
}
