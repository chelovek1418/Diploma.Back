﻿var config = {
    userStore: new Oidc.WebStorageStateStore({ store: window.localStorage }),
    authority: "https://localhost:44361/",
    client_id: "client_id_js",
    redirect_uri: "https://localhost:44378/home/signin",
    post_logout_redirect_uri: "https://localhost:44378/Home/Index",
    response_type: "code",
    scope: "openid rc.scope StudentPerfomanceApi"
};

var userManager = new Oidc.UserManager(config);

var signIn = function () {
    userManager.signinRedirect();
};

var signOut = function () {
    userManager.signoutRedirect();
}; 

userManager.getUser().then(user => {
    console.log("user:", user);
    if (user) {
        axios.defaults.headers.common["Authorization"] = "Bearer " + user.access_token;
    }
});

var callApi = function () {
    axios.get("https://localhost:44322/secret")
        .then(res => {
            console.log(res);
        });
};

var refreshing = false;

axios.interceptors.response.use(
    function (response) { return response; },
    function (error) {
        console.log(error.response);

        var axiosConfig = error.response.config;

        if (error.response.status === 401) {
            if (!refreshing) {
                refreshing = true;

                return userManager.signinSilent().then(user => {
                    axios.defaults.headers.common["Authorization"] = "Bearer " + user.access_token;
                    axiosConfig.headers["Authorization"] = "Bearer " + user.access_token;
                    return axios(axiosConfig);
                });
            }
        }

        return Promise.reject(error);
    });