var createState = function () {
    return "StateValuegwegtwdf13rro2inojnMfneiffWFEdwwfe";
};

var createNonce = function () {
    return "NonceValueafgewedfifKNHIgwegweNSf";
};


var signIn = function () {
    var redirectUri = "https://localhost:44378/Home/SignIn";
    var responseType = "id_token token";
    var scope = "openid api1";
    var authUrl = "/connect/authorize/callback" +
                    "?client_id=client_id_js" +
                    "&redirect_uri=" + encodeURIComponent(redirectUri) +
                    "&response_type=" + encodeURIComponent(responseType) +
                    "&scope=" + encodeURIComponent(scope) +
                    "&nonce=" + createNonce() +
                    "&state=" + createState();

    var returnUrl = encodeURIComponent(authUrl);


    window.location.href = "https://localhost:44361/Auth/Login?ReturnUrl=" + returnUrl;

};