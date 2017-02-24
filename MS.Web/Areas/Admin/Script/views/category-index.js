(function ($) {
    function CategoryIndex() {
        var $this = this, formAddEditcategory,formDelete;


        function initGridControlsWithEvents() {
           
            $('.switchBox').each(function (index, element) {

                if ($(element).data('bootstrapSwitch')) {
                    $(element).off('switch-change');
                    $(element).bootstrapSwitch('destroy');
                }

                $(element).bootstrapSwitch()
                .on('switch-change', function () {
                    var switchElement = this;
                    
                    $.post(siteAdminDomain+'admin/CampaignCategory/CategoryActive', { id: this.value }, function (result) {
                        if (!result.isSuccess) {
                            $(switchElement).bootstrapSwitch('toggleState', true);
                        }
                    });
                });
            });
        }


        function attachEvent()
        {
            $(".deletePhoto").on("click", function () {
                if (confirm("Photo will be deleted permanently, please confirm?")) {
                    var obj = $(this);
                    $.ajax({
                        url:siteAdminDomain+"Json/DeletePhotos",
                        type: "post",
                        dataType: "json",
                        ContentType: "text/html",
                        data: { folder: "CategoryImage", filename: $(this).attr("rel") },
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

            $(".deleteBackPhoto").on("click", function () {
                if (confirm("Photo will be deleted permanently, please confirm?")) {
                    var obj = $(this);
                    $.ajax({
                        url: siteAdminDomain + "Json/DeletePhotos",
                        type: "post",
                        dataType: "json",
                        ContentType: "text/html",
                        data: { folder: "CategoryImage", filename: $(this).attr("rel") },
                        success: function (data) {
                            if (data.success) {
                                $(obj).prev().remove();
                                $(obj).remove();
                                $("#UploadedBackImages").val('');

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
          $("#modal-create-edit-category").on('loaded.bs.modal', function (e) {
              formAddEditcategory = new Global.FormHelper($(this).find("form"), { updateTargetId: "validation-summary" });
              attachEvent();
              //$("#UploadedImages").val();//
            }).on('hidden.bs.modal', function (e) {
                $(this).removeData('bs.modal');
            });

            //$("#modal-delete-AccommodationResort").on('loaded.bs.modal', function (e) {
            //    formDelete = new Global.FormHelper($(this).find("form"), { updateTargetId: "validation-summary" });
            //}).on('hidden.bs.modal', function (e) {
            //    $(this).removeData('bs.modal');
            //});
        }

        function initializeGrid() {

            $('#grid-category').DataTable({
                stateSave: true,
                bDestroy: true,
                "aoColumnDefs": [{ 'bSortable': false, 'aTargets': [0,3] }],
                "fnDrawCallback": function (oSettings) {
                    initGridControlsWithEvents();
                }
            });
          }



        $this.init = function () {
             initializeGrid();
             initializeModalWithForm();
            
        };
    }

    $(function () {
        var self = new CategoryIndex();
        self.init();
    });

}(jQuery));