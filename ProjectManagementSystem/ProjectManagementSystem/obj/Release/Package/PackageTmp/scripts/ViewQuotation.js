$(document).ready(function () {
    $(".aVersion").bind('click', function (e) { ShowQuotation(e) });
});

function ShowQuotation(element) {
    $("#myModal").find('.modal-header').html('<b>'+$(element.target).text()+'</b>' + modalHeader);
    $("#myModal").find('.modal-body').html($("#divCustomerQuotation").html());
    $("#spTotal").text(100);
    $("#myModal").find('.modal-footer').find('button').html('<button type="button" class="btn btn-default btnModalClose" data-dismiss="modal">Close</button>');
    //$("#myModal").find('.modal-footer').find('button').after('<button type="button" class="btn btn-default" >Proceed</button>');
    $("#myModal").show();
}