class RegistrationAppViewModel {
    constructor() {
        this.applicant = new ApplicantViewModel();
    }

    submitApplication(formElement) {
        console.log(this.applicant);
    }
}