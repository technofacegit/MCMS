(function ($) {
    function CampaignImageUpload() {
        var $this = this, formCampaign;

        function initializeModalWithForm() {
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
                    maxFileSize: 10485760,
                    multiple: true,
                    maxFileCount: 20,
                    formData: { folder: "temp" },
                    onSuccess: function (files, data, xhr, pd) {
                                //$("#divimages").html('');
                            var filename = JSON.parse(data);
                            if (data.length > 0) {
                                $("#divimages").append("<div class='row'>");
                                $("#divimages").append("<div class='single col-lg-4' rel='" + filename[1] + "'><img src='" + filename[0] + filename[1] + "' alt=''/></div>");
                                $("#divimages").append(pd.del);
                                $("#divimages").append("</div>");
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

        function initializeModalWithForm2() {

            $("#modal-add2-photos").on('loaded.bs.modal', function (e) {

                var path = siteAdminDomain + "Json/UploadPhotos";

                uploadObj = $("#modal-add2-photos .PhotosUpload").uploadFile({
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
                    maxFileSize: 10485760,
                    multiple: true,
                    maxFileCount: 20,
                    formData: { folder: "temp" },
                    onSuccess: function (files, data, xhr, pd) {
                        var filename = JSON.parse(data);
                        if (data.length > 0) {
                            $("#divimages2").append("<div class='row'>");
                            $("#divimages2").append("<div class='single col-lg-4' rel='" + filename[1] + "'><img src='" + filename[0] + filename[1] + "' alt=''/></div>");
                            $("#divimages2").append(pd.del);
                            $("#divimages2").append("</div>");
                        }

                        setTimeout(function () {
                            updatePhotos2();
                        }, 2);

                    },
                    onError: function () {
                        alert("Error in file uploading, please try again.");
                    },
                    afterUploadAll: function () {

                        $('#modal-add2-photos').modal('hide');
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
                                    if ($('#divimages2 .single').length > 0) {
                                        //    $("#btnSavePhotos").show();
                                    }
                                    else {
                                        //  $("#btnSavePhotos").hide();
                                    }
                                    updatePhotos2();
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


        function updatePhotos2() {
            if ($("#divimages2 .single").length > 0) {
                $("#UploadedImages2").val('');
                $("#divimages2 .single").each(function (index) {
                    var src = $(this).find("img").attr("src");
                    $("#UploadedImages2").val($("#UploadedImages2").val() + "##" + $(this).attr("rel"));
                });
            }
        }


        function initializeForm() {

            formCampaign = new Global.FormHelper($(this).find("form"));


            $(".deletePhoto").on("click", function () {

                if (confirm("Photo will be deleted permanently, please confirm?")) {
                    var path = "http://localhost/migros/Json/DeleteLivePhotos";
                    var obj = $(this);
                    $.ajax({
                        url: path,
                        type: "post",
                        dataType: "json",
                        ContentType: "text/html",
                        data: { type: 'Category', folder: "CategoryImage", photoId: $(this).attr("rel") },
                        success: function (data) {
                            if (data.success) {
                                $(obj).prev().remove();
                                $(obj).remove();
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
            $this.init = function () {

                initializeForm();
                initializeModalWithForm();
                initializeModalWithForm2();

            };
        
    }

        $(function () {
            var self = new CampaignImageUpload();
            self.init();
        });

    } (jQuery)
);