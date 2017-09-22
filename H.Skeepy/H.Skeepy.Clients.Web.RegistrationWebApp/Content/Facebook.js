class FacebookApi {

    constructor(fb) {
        this._fb = fb;

        this._loginAndAuthorizeApp = () => {
            return new Promise((yey, ney) => {
                this._fb.login(response => {
                    if (response.status !== 'connected' || !response.authResponse) return ney(response);

                    yey(response);

                }, { scope: 'public_profile,email' });
            });
        };
    }

    fetchUserDetails() {
        return new Promise((yey, ney) => {

            this._fb.getLoginStatus(response => {
                if (response.status !== 'connected') {
                    return yey(this._loginAndAuthorizeApp()
                        .then(() => this.fetchUserDetails(), ney));
                }

                this._fb.api('/me', { fields: 'id,email,first_name,last_name,middle_name,name,link' }, response => {
                    yey(response);
                });

            });

        });
    }

}

(function () {

    var fbApi = null;
    var fbPromiseResolve = null;

    window.GetFB = new Promise((yey, ney) => {
        if (fbApi) {
            yey(fbApi);
            return;
        };
        fbPromiseResolve = yey;
    });

    window.fbAsyncInit = function () {
        FB.init({
            appId: '702173189978497',
            autoLogAppEvents: false,
            xfbml: true,
            version: 'v2.10'
        });
        FB.AppEvents.logPageView();

        fbApi = new FacebookApi(FB);
        fbPromiseResolve(fbApi);
    };

})();

(function (d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) { return; }
    js = d.createElement(s); js.id = id;
    js.src = "//connect.facebook.net/en_US/sdk.js";
    fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'facebook-jssdk'));