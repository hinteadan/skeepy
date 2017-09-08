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
            console.log(`Submitted with token: ${token}`);
        })
        .fail(() => {
            this.isApplicationBeingSubmitted(false);
            this.submitLabel('Application failed, please try again');
            setTimeout(() => { this.submitLabel('Apply'); }, 5000);
        })
        .always(() => { });
    }
}