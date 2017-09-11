class RegistrationAppViewModel {
    constructor() {
        this.applicant = new ApplicantViewModel();
        this.isApplicationBeingSubmitted = ko.observable(false);
        this.submitLabel = ko.observable('Apply');
        
    }

    submitApplication(formElement) {
        if (!$(formElement).valid()) return;

        Analytics.trackEvent('ApplyForRegistration');

        this.isApplicationBeingSubmitted(true);
        this.submitLabel('Submitting, please wait...');

        $.post(`${applicationBaseUrl}skeepy/registration/apply`, ko.mapping.toJS(this.applicant), (token) => {
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
}