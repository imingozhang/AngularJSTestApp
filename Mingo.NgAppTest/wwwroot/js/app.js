
var myApp = angular.module("myModule", [])
    .controller("myController", function ($scope, $http) {
        var myData = {
            text: "This is a sample Text. Extra TEXT! The third teXt",
            subText: "text"
        };

        $scope.reqdata = myData;

        var url = '/api/findpattern/check';
        var header_config = {
            headers: {
                'Content-Type': 'application/json; charset=utf-8;'
            }
        };

        $scope.checkString = function () { 
            $scope.output_msg = null;
            $scope.output = null;

            if (!$scope.reqdata.text) {
                $scope.output_msg = "Please enter an input text.";
                return false;
            }

            if (!$scope.reqdata.subText) {
                $scope.output_msg = "Please enter the subtext for searching.";
                return false;
            }

            $scope.output_msg = null;
            //Call the api services
            $http.post(url, JSON.stringify(myData), header_config).then(
                function (response) {
                    if (response.data) {
                        $scope.output_msg = "Post data submitted successfully.";
                        $scope.output = response.data;
                        console.log('Success');
                    } else {
                        $scope.output_msg = "Post data submitted unsuccessfully.";
                        console.log('Failed');
                    }
                }
            );
        };

    });