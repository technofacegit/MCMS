(function ($) {
    function MilKatalogIndex() {
        var $this = this, formAddEditMilKatalog, formDelete;
        function attachEvent() {
            $(".deletePhoto").on("click", function () {
                if (confirm("Photo will be deleted permanently, please confirm?")) {
                    var obj = $(this);
                    $.ajax({
                        url: siteAdminDomain + "Json/DeletePhotos",
                        type: "post",
                        dataType: "json",
                        ContentType: "text/html",
                        data: { folder: "KazancBackgroundImage", filename: $(this).attr("rel") },
                        success: function (data) {
                            if (data.success) {
                                $(obj).prev().remove();
                                $(obj).remove();
                                $("#UploadedImages").val('');

                            }
                            else {
                                alert('Request could not be submitted. Please try again.');
                            }

                        },
                        error: function (data) {
                            alert('Request could not be submitted. Please try again.');
                            return false;
                        }
                    });
                }
            });
        }


        function initializeModalWithForm() {
            $("#modal-create-edit-MilKatalog").on('loaded.bs.modal', function (e) {
                formAddEditMilKatalog = new Global.FormHelper($(this).find("form"), { updateTargetId: "validation-summary" });
             
                attachEvent();
               
            }).on('hidden.bs.modal', function (e) {
                $(this).removeData('bs.modal');
            });

            $("#modal-add-photos").on('loaded.bs.modal', function (e) {

                var path = siteAdminDomain + "Json/UploadPhotos";

                uploadObj = $("#modal-add-photos .PhotosUpload").uploadFile({
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
                    allowedTypes: "png,gif,jpg,jpeg",
                    maxFileSize: 1048576,
                    multiple: false,
                    maxFileCount: 1,
                    formData: { folder: "temp" },
                    onSuccess: function (files, data, xhr, pd) {
                        var filename = JSON.parse(data);
                        if (data.length > 0) {
                            $("#divimages").html('');
                            $("#divimages").append("<div class='single' rel='" + filename[1] + "'><img src='" + filename[0] + filename[1] + "' alt=''/></div>");
                            $("#divimages").append(pd.del);
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
            $('#grid-MilKatalog').DataTable({
                "aoColumnDefs": [{ 'bSortable': false, 'aTargets': [5] }],
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
        var self = new MilKatalogIndex();
        self.init();
    });

}(jQuery));