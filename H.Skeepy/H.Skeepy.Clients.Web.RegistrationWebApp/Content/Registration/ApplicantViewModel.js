class ApplicantViewModel {
    constructor() {
        this.email = ko.observable('');
        this.firstName = ko.observable('');
        this.lastName = ko.observable('');
        this.facebookDetails = {
            details: ko.observableArray([])
        };
    }

    withFacebookDetails(facebookDictionary) {

        this.facebookDetails.details.removeAll();
        for (var prop in facebookDictionary) {
            this.facebookDetails.details.push({
                key: prop,
                value: facebookDictionary[prop]
            });
        }
        return this;
    }
}