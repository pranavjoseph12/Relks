var url = "EnquiryService.asmx";

function AddFollowUp() {
    if ($('#ContentPlaceHolder1_txtFollowUpComment').val().trim() == '') {
        alert(' Comment Required');
        return false;
    }
    else if ($('#txtNextDate').val().trim() == '') {
        alert(' Next Date Required');
        return false;
    }
    else {
        var today = new Date();
        today.setHours(0, 0, 0, 0);
        d = new Date($('#txtNextDate').val());
        d.setHours(0, 0, 0, 0);

        if (d < today) {
            alert('Next followup date should not be less than todays date');
            return false;
        }
    }
    $.ajax({
        type: "POST",
        url: url + "/AddFollowUp",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ enqID: $('#ContentPlaceHolder1_hdnEnqID').val(), comment: $('#ContentPlaceHolder1_txtFollowUpComment').val(), nextFollowUpDate: $('#txtNextDate').val(), responseType: $('#ResponseType').val() }),
        success: function (result) {
            if (result.d == true) {
                alert("FollowUp added to the enquiry.");
                $("#MyModal").hide();
                window.location.reload();
            }
            else {
                alert("Error");
            }

        }
    });
}