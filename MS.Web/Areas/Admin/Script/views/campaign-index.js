(function ($) {
    function CampignIndex() {
        var $this = this, formAddEditcampaign, formDelete;


        function initGridControlsWithEvents() {

            $('.switchBox').each(function (index, element) {

                if ($(element).data('bootstrapSwitch')) {
                    $(element).off('switch-change');
                    $(element).bootstrapSwitch('destroy');
                }

                $(element).bootstrapSwitch()
                .on('switch-change', function () {
                    var switchElement = this;

                    $.post(siteAdminDomain + 'admin/Campaign/CampaignActive', { id: this.value }, function (result) {
                        if (!result.isSuccess) {
                            $(switchElement).bootstrapSwitch('toggleState', true);
                        }
                    });
                });
            });
        }


        function attachEvent() {
            $(".deletePhoto").on("click", function () {
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

        function initTimePicker() {
            var today = new Date();

            //$(".inputdate").datepicker(
            //    {
            //        format: 'dd.MM.yyyy hh:mm:ss'
            //    });

            //$(function () {
            $('#time_timepicker_start').datetimepicker({
                stepping: 10,
                locale: 'tr',
                sideBySide: true,
                showClose: true,
                format: 'HH:mm',
                //timeFormat: 'HH:mm'
                ///format: 'dd.mm.yyyy hh:mm'
                //autoclose: true,
                //format: 'dd mm yyyy  hh:ii',
                //startDate: new Date()
            });
            $('#time_timepicker_end').datetimepicker({
                useCurrent: false, //Important! See issue #1075
                stepping: 10,
                locale: 'tr',
                sideBySide: true,
                showClose: true,
                //dateFormat: 'dd.mm.yyyy',
                //timeFormat: 'HH:mm'
                format: 'HH:mm'
                //format: 'dd mm yyyy  hh:ii',
                //autoclose: true,
                //startDate: new Date()
            });



            //$('#date_timepicker_start').datetimepicker().on('changeDate', function (ev) {
            //    $('#date_timepicker_end').datetimepicker('setStartDate', ev.Date);
            //        });

            //$('#date_timepicker_end').datetimepicker().on('changeDate', function (ev) {
            //    $('#date_timepicker_start').datetimepicker('setEndDate', ev.Date);
            //});

            $("#date_timepicker_start").on("dp.change", function (e) {
                $('#date_timepicker_end').data("DateTimePicker").minDate(e.date);
            });
            $("#date_timepicker_end").on("dp.change", function (e) {
                $('#date_timepicker_start').data("DateTimePicker").maxDate(e.date);
            });
            //});

            //$(function () {
            //$('#date_timepicker_start').datetimepicker({
            //    formatTime: 'H:i',
            //    formatDate: 'd.m.Y',
            //    startDate:	'2016/01/01',
            //    onShow: function (ct) {
            //        this.setOptions({
            //            maxDate: $('#date_timepicker_end').val() ? $('#date_timepicker_end').val() : false
            //        })
            //    }
            //});
            //$('#date_timepicker_end').datetimepicker({
            //    formatTime: 'H:i',
            //    formatDate: 'd.m.Y',
            //    startDate: '2016/01/01',
            //    onShow: function (ct) {
            //        this.setOptions({
            //            minDate: $('#date_timepicker_start').val() ? $('#date_timepicker_start').val() : false
            //        })
            //    }
            //});
            //});
        }

        function initDatePicker() {
            var today = new Date('01/01/0001');
            $("#date_picker_start").datepicker(
                {
                    startDate: new Date(),
                    format: 'dd.mm.yyyy'
                });
            $("#date_picker_end").datepicker(
               {
                   startDate: new Date(),
                   format: 'dd.mm.yyyy'
               });
        }

        function initializeModalWithForm() {
            $("#modal-create-edit-campaign").on('loaded.bs.modal', function (e) {
                formAddEditcampaign = new Global.FormHelper($(this).find("form"), { updateTargetId: "validation-summary" });
                attachEvent();
                initDatePicker();
                initTimePicker();
                //$("#UploadedImages").val();//
            }).on('hidden.bs.modal', function (e) {
                $(this).removeData('bs.modal');
            });


        }

        function initializeGrid() {
            $('#grid-campaign').DataTable({
                stateSave: true,
                bDestroy: true,
        //        "aoColumnDefs": [
        //{ 'bSortable': false, 'aTargets': [0, 7, 9] }
        //        ],
                aoColumnDefs: [
  { 'iDataSort': 10, 'aTargets': [5] },
{ 'bVisible': false, 'aTargets': [10] },
{ 'bSortable': false, 'aTargets': [0, 7, 9] }
                ],
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
        var self = new CampignIndex();
        self.init();
    });

}(jQuery));