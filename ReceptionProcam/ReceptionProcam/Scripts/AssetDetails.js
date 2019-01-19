function submitAsset() {


    var assetId = $("#hidSubmitAsset").val();
    $.ajax({
        type: "POST",
        url: "/AssetManagement/SubmitAsset?id=" + assetId,
        dataType: "text",
        success: function (data) {
            $('#confirmModal').modal('toggle');
            location.reload();
        },
        error: function () {
            alert("Error occured!!")
        }
    });
}