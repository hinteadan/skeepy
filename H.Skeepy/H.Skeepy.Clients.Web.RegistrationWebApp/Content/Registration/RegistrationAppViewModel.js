class RegistrationAppViewModel {
    constructor() {
        this.applicant = new ApplicantViewModel();
    }

    submitApplication(formElement) {
        if (!$(formElement).valid()) return;
        console.log(this.applicant);
    }
}