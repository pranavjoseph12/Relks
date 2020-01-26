var Roleurl = "RoleService.asmx";
var pageNumber = 1;


var app = angular.module('RolePageApp', []);

angular.element(document).ready(function () {
    //angular.bootstrap(document.getElementById("RolePage"), ['RolePageApp']);
});

app.controller('RolePagectrl', function ($scope, $http, $interval) {
    $scope.pageCount = [10, 25, 50, 100];
    $scope.FetchRoles = function () {
        $.ajax({
            type: "POST",
            url: Roleurl + "/GetRoles",
            data: JSON.stringify({ pageNumber: 1, pageSize: $scope.RecordCount }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                $scope.RoleList = JSON.parse(response.d);
                $scope.Pagination($scope.RoleList.length > 0 ? $scope.RoleList[0].RecordCount : 0);
            },
            error: function (error) {
                alert(error);
            }
        });


        $http.get(Roleurl + "/GetRoles", { params: { 'pageNumber': pageNumber, 'pageSize': $scope.RecordCount } }).then(function (response) {
            $scope.RoleList = response.data;
            $scope.Pagination($scope.RoleList.length > 0 ? $scope.RoleList[0].RecordCount : 0);
        });
    }
    $scope.RecordCount = $scope.pageCount[0];
    $scope.stateChanged = function (role) {

        $.ajax({
            type: "POST",
            url: Roleurl + "/UpdateRoles",
            data: JSON.stringify({ deletable: role.Deletable, viewable: role.Viewable, editable: role.Editable, roleID: role.RoleID }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                //$scope.RoleList = JSON.parse(response.d);
                $scope.response = JSON.parse(response.d);
                //$scope.Pagination($scope.RoleList.length > 0 ? $scope.RoleList[0].RecordCount : 0);
            },
            error: function (error) {
                //alert(error);
            }
        });

        //$http.get(Roleurl + "/UpdateRoles", { params: { 'deletable': role.Deletable, 'viewable': role.Viewable, 'editable': role.Editable, 'roleID': role.RoleID } }).then(function (response) {
        //    $scope.response = response.data;
        //});
    }
    $scope.FetchRoles();

    $scope.Pagination = function (totalCount) {
        var pagination = '<ul class="pagination pull-right"><li class="prev disabled"><a onclick="return false;" href="#">← Previous</a></li><li class="next"><a href="#">Next → </a></li></ul>';
        var startNumber = 1;
        if (totalCount > 0) {
            if (pageNumber == 1) {
                if (pageNumber * $scope.RecordCount < totalCount) {
                    pagination = '<ul class="pagination pull-right"><li class="prev disabled"><a onclick="return false;" href="#">← Previous</a></li><li class="next"><a  onclick="NextClick();" href="#">Next → </a></li></ul>';
                }
                else {
                    pagination = '<ul class="pagination pull-right"><li class="prev disabled"><a onclick="return false;" href="#">← Previous</a></li><li class="next disabled"><a onclick="return false;" href="#">Next → </a></li></ul>';
                }
            }
            else {
                startNumber = ((pageNumber - 1) * $scope.RecordCount) + 1;
                if (pageNumber * $scope.RecordCount < totalCount) {
                    pagination = '<ul class="pagination pull-right"><li class="prev"><a  onclick="PrevClick();" href="#">← Previous</a></li><li class="next"><a onclick="NextClick();" href="#">Next → </a></li></ul>';
                }
                else {
                    pagination = '<ul class="pagination pull-right"><li class="prev"><a  onclick="PrevClick();" href="#">← Previous</a></li><li class="next disabled"><a onclick="return false;" href="#">Next → </a></li></ul>';
                }
            }
            if (pageNumber * $scope.RecordCount < totalCount) {
                startNumber = ((pageNumber - 1) * $scope.RecordCount) + 1;
                $('#divCount').html('Showing ' + startNumber + ' to ' + pageNumber * $scope.RecordCount + ' of ' + totalCount + ' entries');
            }
            else {
                startNumber = ((pageNumber - 1) * $scope.RecordCount) + 1;
                $('#divCount').html('Showing ' + startNumber + ' to ' + totalCount + ' of ' + totalCount + ' entries');
            }
        }
        else {
            $('#divCount').html('No records');
        }
        $('#divPagination').html(pagination);
    }




})


function Validate() {
    if ($('#ContentPlaceHolder1_txtRoleName').val() != '') {
        return true;
    } else {
        alert('Please enter role name.');
        return false;
    }
}

function PrevClick() {
    pageNumber = pageNumber - 1;
    angular.element($("#RolePage")).scope().FetchRoles();
    return false;
}



function NextClick() {
    pageNumber = pageNumber + 1;
    angular.element($("#RolePage")).scope().FetchRoles();
    return false;
}