// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function activateModal(schoolId, schoolName) {
    $('#confirmDeleteModal').modal({
        backdrop: 'static',
        keyboard: false
    });
    $('#confirmDeleteModalTitle').text("Delete " + schoolName);
    $('#deleteSchoolBtn').attr('href', '/School/Delete/' + schoolId);
}

$("#searchSchool").click(() => {
    if ($("#SearchWord").val().trim().length < 1)
        event.preventDefault();
});
