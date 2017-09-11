class SetPasswordAppViewModel {
    constructor() {
        this.password = ko.observable('');
        this.passwordConfirm = ko.observable('');
        this.submitLabel = ko.observable('Set Password');
        this.isFormBeingSubmitted = ko.observable(false);
    }

    submitPassword(formElement) {
        if (!$(formElement).valid()) return;

        Analytics.trackEvent('SetPassword');

        this.isFormBeingSubmitted(true);
        this.submitLabel('Submitting, please wait...');

        $.post(`${applicationBaseUrl}skeepy/registration/pass/${registrationToken}`, this.password(), () => {
            Analytics.trackEvent('SetPasswordSucceeded');
            window.location.href = `${applicationBaseUrl}/password/success`;
        })
            .fail(response => {
                this.isFormBeingSubmitted(false);
                this.submitLabel('Password set failed, please try again');
                setTimeout(() => { this.submitLabel('Set Password'); }, 5000);
                Analytics.trackEvent('SetPasswordFailed');
                $('#validationErrors').append(`<li>${response.statusText}</li>`);
            })
            .always(() => { });
    }
}