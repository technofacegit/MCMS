(function ($) {
    function CategoryImageUpload() {
        var $this = this, formCategory;

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

        function initializeImageBackgroundModalWithForm() {

            $("#modal-addbag-photos").on('loaded.bs.modal', function (e) {
                
                var path = siteAdminDomain+"Json/UploadPhotos";

                uploadObj = $("#modal-addbag-photos .PhotosUpload").uploadFile({
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
                    maxFileSize: 1048576,//8388608
                    multiple: false,
                    maxFileCount: 1,
                    formData: { folder: "temp" },
                    onSuccess: function (files, data, xhr, pd) {
                        var filename = JSON.parse(data);
                        if (data.length > 0) {
                            $("#divbackimagesbag").html('');
                            $("#divbackimagesbag").append("<div class='single' rel='" + filename[1] + "'><img src='" + filename[0] + filename[1] + "' alt=''/></div>");
                            $("#divbackimagesbag").append(pd.del);
                        }

                        setTimeout(function () {
                           
                            if ($("#divbackimagesbag .single").length > 0) {
                                $("#UploadedBackImages").val('');
                                $("#divbackimagesbag .single").each(function (index) {
                                    var src = $(this).find("img").attr("src");
                                  
                                    $("#UploadedBackImages").val($("#UploadedBackImages").val() + "##" + $(this).attr("rel"));
                                });
                            }
                        }, 2);

                    },
                    onError: function () {
                        alert("Error in file uploading, please try again.");
                    },
                    afterUploadAll: function () {
                       
                        $('#modal-addbag-photos').modal('hide');
                    },
                    deleteCallback: function (data, pd) {
                        var datavalue = JSON.parse(data)[1];
                        var path = siteAdminDomain+"/Json/DeletePhotos";
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
                                    $("#UploadedBackImages").val('');
                                    if ($("#divbackimagesbag .single").length > 0) {
                                        
                                        $("#divbackimagesbag .single").each(function (index) {
                                            var src = $(this).find("img").attr("src");
                                           
                                            $("#UploadedBackImages").val($("#UploadedBackImages").val() + "##" + $(this).attr("rel"));
                                        });
                                    }
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
        function updatebgPhotos() {
            if ($("#divimagesbag .single").length > 0) {
                $("#UploadedImages").val('');
                $("#divimages .single").each(function (index) {
                    var src = $(this).find("img").attr("src");
                    $("#UploadedImages").val($("#UploadedImages").val() + "##" + $(this).attr("rel"));
                });
            }
        }

        function initializeNewImageModalWithForm() {
        $("#modal-new-add-photos").on('loaded.bs.modal', function (e) {
                
            var path = siteAdminDomain + "Json/UploadPhotos";
              
            uploadObj = $("#modal-new-add-photos .PhotosUpload").uploadFile({
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
                        $("#divnewboardimagesbag").html('');
                        $("#divnewboardimagesbag").append("<div class='single' rel='" + filename[1] + "'><img src='" + filename[0] + filename[1] + "' alt=''/></div>");
                        $("#divnewboardimagesbag").append(pd.del);
                    }

                    setTimeout(function () {
                        updatenewPhotos();
                    }, 2);

                },
                onError: function () {
                    alert("Error in file uploading, please try again.");
                },
                afterUploadAll: function () {
                       
                    $('#modal-new-add-photos').modal('hide');
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
                                if ($('#divnewboardimagesbag .single').length > 0) {
                                    //    $("#btnSavePhotos").show();
                                }
                                else {
                                    //  $("#btnSavePhotos").hide();
                                }
                                updatenewPhotos();
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



    function updatenewPhotos() {
        $("#UploadedNewBoardImages").val('');
        if ($("#divnewboardimagesbag .single").length > 0) {
               
            $("#divnewboardimagesbag .single").each(function (index) {
                var src = $(this).find("img").attr("src");
                $("#UploadedNewBoardImages").val($("#UploadedNewBoardImages").val() + "##" + $(this).attr("rel"));
            });
        }
    }

    function initializeNewBackImageModalWithForm() {
        $("#modal-new-addbag-photos").on('loaded.bs.modal', function (e) {

            var path = siteAdminDomain + "Json/UploadPhotos";

            uploadObj = $("#modal-new-addbag-photos .PhotosUpload").uploadFile({
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
                        $("#divnewbackimagesbag").html('');
                        $("#divnewbackimagesbag").append("<div class='single' rel='" + filename[1] + "'><img src='" + filename[0] + filename[1] + "' alt=''/></div>");
                        $("#divnewbackimagesbag").append(pd.del);
                    }

                    setTimeout(function () {
                        updatenewbackPhotos();
                    }, 2);

                },
                onError: function () {
                    alert("Error in file uploading, please try again.");
                },
                afterUploadAll: function () {

                    $('#modal-new-addbag-photos').modal('hide');
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
                                if ($('#divnewbackimagesbag .single').length > 0) {
                                    //    $("#btnSavePhotos").show();
                                }
                                else {
                                    //  $("#btnSavePhotos").hide();
                                }
                                updatenewbackPhotos();
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



    function updatenewbackPhotos() {
        $("#UploadedNewBackImages").val('');
        if ($("#divnewbackimagesbag .single").length > 0) {

            $("#divnewbackimagesbag .single").each(function (index) {
                var src = $(this).find("img").attr("src");
                $("#UploadedNewBackImages").val($("#UploadedNewBackImages").val() + "##" + $(this).attr("rel"));
            });
        }
    }

        function initializeForm() {
          
           // formCategory = new Global.FormValidationReset($('#category-box form'));
            formCategory = new Global.FormHelper($(this).find("form"));
           // CKEDITOR.env.isCompatible = true;
           // CKEDITOR.replace('SalesPoints');
           // attachEventCKEditor('SalesPoints');
          

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
             initializeImageBackgroundModalWithForm();
             initializeNewImageModalWithForm()
             initializeNewBackImageModalWithForm();
            
        };
    }

    $(function () {
        var self = new CategoryImageUpload();
        self.init();
    });

}(jQuery));