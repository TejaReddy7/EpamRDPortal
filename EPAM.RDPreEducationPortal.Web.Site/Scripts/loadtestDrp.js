$(document).ready(function () {
    setTimeout(function () {
        $("#RecruitmentId").val(1).trigger("change");
        LoadTestDrp();
    }, 300);
});
function LoadTestDrp() {
    if ($("#TestId").val() == null || $("#TestId").val() == 'undefined') {
        setTimeout(function () {
            LoadTestDrp();
        }, 500);

    } else {
        $("#TestId").val(2).trigger("change");
    }
}