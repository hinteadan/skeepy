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
        setTimeout(() => { this.isApplicationBeingSubmitted(false); }, 1000);
    }
}