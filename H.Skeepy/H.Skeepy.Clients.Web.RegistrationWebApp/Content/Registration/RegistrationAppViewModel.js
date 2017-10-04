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

        $.post(`${applicationBaseUrl}/skeepy/registration/apply`, ko.mapping.toJS(this.applicant), (token) => {
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
        this.isFetchingFacebookInfo(true);
        GetFB
            .then(fb => fb.fetchUserDetails())
            .then(response => {
                this.applicant.email(response.email || '');
                this.applicant.firstName(response.first_name || '');
                this.applicant.lastName(response.last_name || '');
                $('#applicationForm').valid();
                this.isFetchingFacebookInfo(false);
            }, () => {
                this.isFetchingFacebookInfo(false);
                this.facebookError(true);
                setTimeout(() => {
                    this.facebookError(false);
                }, 5000);
            });
    }
}