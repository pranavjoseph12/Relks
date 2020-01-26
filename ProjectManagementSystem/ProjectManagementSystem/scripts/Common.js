var modalHeader = '<button type="button" class="close btnModalClose"  data-dismiss="modal">&times;</button>';
$(document).ready(function () {
    $(".btnModalClose").on('click', function () { $("#myModal").hide(); });
});
