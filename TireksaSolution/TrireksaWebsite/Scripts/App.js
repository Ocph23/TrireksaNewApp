       // <script src="~/Scripts/angular.js"/>
    var app = angular.module("mainApp", []).controller("mainController",
        function ($scope,$http) {
            $scope.SelectFile = function ()
            {
              
                var f = document.getElementById("file");
                var res = f.files[0];
              


                var form = new FormData();
                form.append("file", res)
                form.append("Id", 1);

                var settings = {
                    "async": true,
                    "crossDomain": true,
                    "url": "http://localhost:51804/api/UploadPhoto/Post",
                    "method": "POST",
                    "headers": {
                        "cache-control": "no-cache",
                    },
                    "processData": false,
                    "contentType": false,
                    "mimeType": "multipart/form-data",
                    "data": form
                }

                $.ajax(settings).done(function (response,data) {
                    console.log(response);
                });


            
            }
          
        }
        );
