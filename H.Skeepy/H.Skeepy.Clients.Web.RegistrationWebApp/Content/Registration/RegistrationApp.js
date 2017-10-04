$(function () {

    ko.applyBindings(new RegistrationAppViewModel());

    $('button[title]').each(function () {
        new Tooltip(this, { placement: 'bottom' });
    });

});