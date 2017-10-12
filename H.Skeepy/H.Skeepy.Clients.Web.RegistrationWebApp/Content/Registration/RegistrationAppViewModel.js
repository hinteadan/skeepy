class RegistrationAppViewModel {
    constructor() {
        this.applicant = new ApplicantViewModel();
        this.isApplicationBeingSubmitted = ko.observable(false);
        this.submitLabel = ko.observable('Apply');

        this.isFetchingFacebookInfo = ko.observable(false);
        this.facebookError = ko.observable(false);
    }

    submitApplication(formElement) {
        if (!$(formElement).valid()) return;

        Analytics.trackEvent('ApplyForRegistration');

        this.isApplicationBeingSubmitted(true);
        this.submitLabel('Submitting, please wait...');

        $.ajax(`${applicationBaseUrl}/skeepy/registration/apply`, {
            data: JSON.stringify(ko.mapping.toJS(this.applicant)),
            contentType: 'application/json',
            type: 'POST',
        })
            .done(token => {
                Analytics.trackEvent('ApplyForRegistrationSucceeded', token);
                window.location.href = `${applicationBaseUrl}/application/success/${token}`;
            })
            .fail(() => {
                this.isApplicationBeingSubmitted(false);
                this.submitLabel('Application failed, please try again');
                setTimeout(() => { this.submitLabel('Apply'); }, 5000);
                Analytics.trackEvent('ApplyForRegistrationFailed');
            })
            .always(() => { });
    }

    fetchFromFacebook() {
        Analytics.trackEvent('FetchRegistrationInfoFromFacebook');
        this.isFetchingFacebookInfo(true);
        GetFB
            .then(fb => fb.fetchUserDetails())
            .then(response => {
                console.log(response);
                this.applicant.withFacebookDetails(response);
                this.applicant.email(response.email || '');
                this.applicant.firstName(response.first_name || '');
                this.applicant.lastName(response.last_name || '');
                $('#applicationForm').valid();
                this.isFetchingFacebookInfo(false);
                Analytics.trackEvent('FetchRegistrationInfoFromFacebookSucceeded');
            }, () => {
                this.isFetchingFacebookInfo(false);
                this.facebookError(true);
                setTimeout(() => {
                    this.facebookError(false);
                }, 5000);
                Analytics.trackEvent('FetchRegistrationInfoFromFacebookFailed');
            });
    }
}