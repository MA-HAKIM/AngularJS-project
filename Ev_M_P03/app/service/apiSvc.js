angular.module("traineeApp")
    .factory("apiSvc", ($http) => {
        return {
            get: (url) => {
                return $http({
                    method: "GET",
                    url: url
                });
            },
            post: (url, data) => {
                return $http({
                    method: "POST",
                    url: url,
                    data: data
                });
            },
            put: (url, data) => {
                return $http({
                    method: "PUT",
                    url: url,
                    data: data
                });
            },
            delete: (url) => {
                return $http({
                    method: "DELETE",
                    url: url

                });
            }
        }
    });