function ValidatePage() {
    if ($('#ContentPlaceHolder1_txtName').val() == '' || $('#ContentPlaceHolder1_txtPhone').val() == '' ||
        $('#ContentPlaceHolder1_txtEmail').val() == '' || $('#ContentPlaceHolder1_txtPassword').val() == '') {
        alert('Please enter all the fields');
        return false;
    }
}