function readRecordId() {
    console.log("location.search", window.location.search);
    var params = new URLSearchParams(window.location.search);
    console.log(params);

    if (!params.get("id")) {
        alert("Could not determine Opportunity record ID.");
    }

    let opportunityId = params.get("id");
    console.log("opportunityId:", opportunityId);
    return opportunityId;
}