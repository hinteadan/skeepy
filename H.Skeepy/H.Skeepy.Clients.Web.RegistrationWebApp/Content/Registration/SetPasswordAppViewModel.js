class SetPasswordAppViewModel {
    constructor() {
        this.password = ko.observable('');
        this.passwordConfirm = ko.observable('');
        this.submitLabel = ko.observable('Set Password');
        this.isFormBeingSubmitted = ko.observable(false);
    }

    submitPassword() {
    }
}