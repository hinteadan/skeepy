class RegistrationAppViewModel {
    constructor() {
        this.applicant = new ApplicantViewModel();
        this.isApplicationBeingSubmitted = ko.observable(false);
        this.submitLabel = ko.observable('Apply');
        
    }

    submitApplication(formElement) {
        if (!$(formElement).valid()) return;

        this.isApplicationBeingSubmitted(true);
        this.submitLabel('Submitting, please wait...');

        $.post('skeepy/registration/apply', ko.mapping.toJS(this.applicant), (token) => {
            window.location.href = `application/success/${token}`;
        })
        .fail(() => {
            this.isApplicationBeingSubmitted(false);
            this.submitLabel('Application failed, please try again');
            setTimeout(() => { this.submitLabel('Apply'); }, 5000);
        })
        .always(() => { });
    }
}