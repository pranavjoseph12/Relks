var url = "VoxBayBridge.asmx";
$(document).ready(function () {
    GetAllCallDetails();
});
function GetAllCallDetails() {
    $.ajax({
        type: "POST",
        url: url + "/GetAllCallDetails",
        data: JSON.stringify({ Id: 1 }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: BindCallDetails,
        error: OnErrorCall
    });
}

function BindCallDetails(response) {
    var jsonResponse = JSON.parse(response.d);
    var className = '';
    var trObj = '';
    var totalCount = 0;
    for (var i = 0; i < jsonResponse.length; i++) {
        if (jsonResponse[i].CallType == "OUT") {
            className = 'odd';
        }
        else {
            className = 'even'
        }

        totalCount = jsonResponse[i].TotalCount;

        trObj += '<tr class="' + className + '">';
        trObj += '<td class=" ">' + (i+1) + '</td>';
        trObj += '<td class=" ">' + jsonResponse[i].AgentName + '</td>';
        trObj += ' <td class=" ">' + jsonResponse[i].Duration + '</td>';
        trObj += '<td class=" ">' + jsonResponse[i].CallTime + '</td>';
        trObj += '<td class=" "><audio controls src = "https://pbx.voxbaysolutions.com/callrecordings/' + jsonResponse[i].Recording + '" >Your browser does not support the <code> audio</code>element.</audio ></td></tr>';
       // trObj += '<td class=" "><audio controls src = "https://mymusiq.in/files/Malayalam_Songs/2018_Malayalam_Movies/Joseph/Poomuthole%20(MyMusiQ.IN).mp3" >Your browser does not support the <code> audio</code>element.</audio ></td></tr>';
        trObj += '';

    }
    $('#tdInnerRowCallDetails').html(trObj);
    //CustomPagination(totalCount, projectExpPageNumber, 'divCountProject', 'divPaginationProject');
}