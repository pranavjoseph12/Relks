function Validate() {
    var error = '';
    if ($('#ContentPlaceHolder1_txtName').val().trim() == '') {
        error += 'Name is required.\n';
    }
    if ($('#ContentPlaceHolder1_txtContactNumber').val().trim() == '') {
        error += 'Contact Number is required.\n';
    }
    if (error != '') {
        alert(error);
        return false;
    }
    else {
        return true;
    }
}

function Clear() {
    $('#ContentPlaceHolder1_txtName').val('');
    $('#ContentPlaceHolder1_txtContactNumber').val('');
    $('#ContentPlaceHolder1_txtEmail').val('');
    $('#ContentPlaceHolder1_txtAddress').val('');
    $('#ContentPlaceHolder1_txtPinCode').val('');
    $('#ContentPlaceHolder1_ddlRating').val('1');
    return false;
}