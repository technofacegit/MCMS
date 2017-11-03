﻿(function ($) {
    function TblDeviceIndex() {
        var $this = this, formAddEditTblDevice, formDelete;

        function initializeModalWithForm() {
       
          
            $("#Camp_Noti").show();
            $("#Text_Noti").hide();

            $("#Text_Notif").click(function (event)
            {
              
                $("#Camp_Noti").hide();
                $("#Text_Noti").show();
            });
        
            $("#Campaign_Noti").click(function (event) {
              
                $("#Camp_Noti").show();
                $("#Text_Noti").hide();
            });

            $("#submit").click(function (event) {

                var result = campformvalidate();
              
                if (result == true) {
                    var valued = '';
                    var oTable = $('#grid-TblDevice').dataTable();
                    $(".selected").each(function () {
                        if ($(this).find('input:checkbox').prop('checked')) {
                            valued = valued + $(this).find('input:checkbox').attr('value') + ',';

                        }
                    });
              
                    $("#tockenvalueforcamp").val(valued);
              
                 
                    //var rowcollection = oTable.$(".selected").find('input:checkbox').prop('checked');
                    //alert(rowcollection);

                    //rowcollection.each(function (index, elem) {
                    //    var checkbox_value = $(elem).val();
                    //    $("#tockenvalueforcamp").append("," + checkbox_value);
                    //});
                }
                else { return false}
             
            });


            $("#submitText").click(function (event) {

                var result = Textformvalidate();
                if (result == true) {
                    var valued = '';
                    var oTable = $('#grid-TblDevice').dataTable();
                    $(".selected").each(function () {
                        if ($(this).find('input:checkbox').prop('checked')) {
                            valued = valued + $(this).find('input:checkbox').attr('value') + ',';

                        }
                    });
                   
                    $("#tockenvalueforText").val(valued);
                    //var rowcollection = oTable.$(".selected").find('input:checkbox').prop('checked');
                    //alert(rowcollection);

                    
                }
                else { return false;}
            });

        }

        function campformvalidate()
        {
            var messagevalidation = true;
           
            var campvalue = $("#CampNO").val();
            
            var campbodyvalue = $("#CampBody").val();

            var checkboxvalidation = 'false';
            $(".selected").each(function () {
                if ($(this).find('input:checkbox').prop('checked') == true)
                { checkboxvalidation = true }
              });

            //if (checkboxvalidation == 'false') {
              
            //   // alert('Please select at least one record from list !!');
            //    $('#diverror').show();
            //    messagevalidation = false;
            //    return false;
            //}
            if (campvalue.trim() == null || campvalue.trim() == "") {
               
                messagevalidation = false;
                $('#camperror').text('*required');
            }
            else
            {
                $('#camperror').text('');
               
            }
            if (campbodyvalue.trim() == null || campbodyvalue.trim() == "") {
                messagevalidation = false;
                $('#campbodyerror').text('*required');
            }
            else {
                $('#campbodyerror').text('');

            }
          
           
            return messagevalidation;

        }

        function Textformvalidate()
            {
                var messagevalidation = true;
           
                var campvalue = $("#TextBody").val();
            
                var checkboxvalidation = 'false';
                $(".selected").each(function () {
                    if ($(this).find('input:checkbox').prop('checked') == true)
                    { checkboxvalidation = true }
                });

                //if (checkboxvalidation == 'false') {
                //  //  alert('Please select at least one record from list !!');
                //    $('#diverror').show();
                //    messagevalidation = false;
                //    return false;
                //}
               
                if (campvalue.trim() == null || campvalue.trim() == "") {
               
                    messagevalidation = false;
                    $('#campbodytexterror').text('*required');
                }
                else
                {
                    $('#campbodytexterror').text('');
               
                }
              
                return messagevalidation;

            }

        function initializeGrid() {
            $('#grid-TblDevice').DataTable({
                "aoColumnDefs": [{ 'bSortable': false, 'aTargets': [0] }],
                "fnDrawCallback": function (oSettings) {

                }
            });
        }

        function initDatePicker() {

            $("#dtNotification").datetimepicker(
                {
                    format: 'DD.MM.YYYY HH:mm',
                    sideBySide: true,
                    defaultDate: new Date(),
                }
                );


            $("#dtNotificationCamp").datetimepicker(
               {
                   format: 'DD.MM.YYYY HH:mm',
                   sideBySide: true,
                   defaultDate: new Date(),
               }
               );

        }


        $this.init = function () {
            initializeGrid();
            initializeModalWithForm();
            initDatePicker();

        };
    }

    $(function () {
        var self = new TblDeviceIndex();
        self.init();
    });

}(jQuery));