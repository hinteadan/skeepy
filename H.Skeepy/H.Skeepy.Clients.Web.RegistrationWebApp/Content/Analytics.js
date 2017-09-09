class Analytics {

    static trackEvent(name, label, value) {
        if (!name) return;
        if (window.appInsights) {
            window.appInsights.trackEvent(name, label, value);
        }
    }

}