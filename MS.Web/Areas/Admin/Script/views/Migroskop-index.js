(function ($) {
    function MigroskopIndex() {
        var $this = this, formAddEditImagefile, formDelete;

        function initializeModalWithForm() {
            $("#modal-create-edit-Image").on('loaded.bs.modal', function (e) {

                formAddEditImagefile = new Global.FormHelper($(this).find("form"), { updateTargetId: "validation-summary" });
                $("select.select2").select2();
                var path = siteAdminDomain;

            }).on('hidden.bs.modal', function (e) {
                $(this).removeData('bs.modal');
            });

            $("#modal-add-photos").on('loaded.bs.modal', function (e) {

                var path = siteAdminDomain + "Json/UploadPdf";

                uploadObj = $("#modal-add-photos .PdfUpload").uploadFile({
                    url: path,
                    dataType: "json",
                    contentType: "text/html",
                    autoSubmit: true,
                    showPreivew: true,
                    dragDropStr: "",
                    previewHeight: 20,
                    previewWidth: 20,
                    showAbort: true,
                    showDelete: true,
                    allowedTypes: "zip",
                    maxFileSize: 110211010110,
                    multiple: true,
                    maxFileCount: 10,
                    formData: { folder: "temp" },
                    onSuccess: function (files, data, xhr, pd) {
                        var filename = JSON.parse(data);
                        if (data.length > 0) {
                            $("#divimages").append("<div class='single' rel='" + filename[1] + "'><img src='" + filename[0] + filename[1] + "' alt=''/></div>");
                            $("#divimages").append(pd.del);
                          //  $("#divimages").append("<br><br><br><br>");
                        }
                       

                        setTimeout(function () {
                            updatePhotos();
                        }, 2);

                    },
                    onError: function () {
                        alert("Error in file uploading, please try again.");
                    },
                    afterUploadAll: function () {

                        $('#modal-add-photos').modal('hide');
                    },
                    deleteCallback: function (data, pd) {

                        var datavalue = JSON.parse(data)[1];
                        var path = siteAdminDomain + "Json/DeletePhotos";
                        $.ajax({
                            url: path,
                            type: "post",
                            dataType: "json",
                            ContentType: "text/html",
                            data: { folder: "temp", filename: datavalue },
                            success: function (data) {
                                if (data.success) {

                                    $("div[rel='" + datavalue + "']").next().remove();
                                    $("div[rel='" + datavalue + "']").remove();
                                    if ($('#divimages .single').length > 0) {
                                        //    $("#btnSavePhotos").show();
                                    }
                                    else {
                                        //  $("#btnSavePhotos").hide();
                                    }
                                    updatePhotos();
                                }
                            },
                            error: function (data) {
                                alert('Request could not be submitted. Please try again.');
                                return false;
                            }
                        });
                    }
                });


            }).on('hidden.bs.modal', function (e) {
                $(this).removeData('bs.modal');
            });


        }

        function updatePhotos() {
            $("#UploadedImages").val('');
            if ($("#divimages .single").length > 0) {

                $("#divimages .single").each(function (index) {
                    var src = $(this).find("img").attr("src");
                    $("#UploadedImages").val($("#UploadedImages").val() + "##" + $(this).attr("rel"));
                });
            }
        }

        function initializeGrid() {
            $('#grid-Image').DataTable({
                "aoColumnDefs": [{ 'bSortable': false, 'aTargets': [0] }],
                "fnDrawCallback": function (oSettings) {

                }
            });
        }


        $this.init = function () {
            initializeGrid();
            initializeModalWithForm();

        };
    }

    $(function () {
        var self = new MigroskopIndex();
        self.init();
    });

}(jQuery));