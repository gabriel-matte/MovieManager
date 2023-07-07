function toggleSearchParams() {
    $("#search-container").hasClass("d-none") ? $("#search-container").removeClass("d-none") : $("#search-container").addClass("d-none");
}


function search() {

    //TODO: add validation to search fields before POST
    //TODO: once validation implemented, change isValid variable to reflect it
    var isValid = true;

    var form = $("#search-form")[0];

    if (isValid)
        form.submit();
    else
        return false;

}