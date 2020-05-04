(function ($) {
    function TblDeviceIndex() {
        var $this = this, formAddEditTblDevice, formDelete;

        function initializeModalWithForm() {
       
          
            $("#Camp_Noti").show();
            $("#Text_Noti").hide();
            $("#Text_Noti_WCard").hide();
            $("#Text_Noti_DeviceToken").hide();

            $("#Text_Notif").click(function (event)
            {
              
                $("#Text_Noti_WCard").hide();
                $("#Camp_Noti").hide();
                $("#Text_Noti").show();
                $("#Text_Noti_DeviceToken").hide();
            });
        
            $("#Campaign_Noti").click(function (event) {
              
                $("#Text_Noti_WCard").hide();
                $("#Camp_Noti").show();
                $("#Text_Noti").hide();
                $("#Text_Noti_DeviceToken").hide();
            });

            $("#Text_Notif_WithCard").click(function (event) {

                $("#Text_Noti_WCard").show();
                $("#Camp_Noti").hide();
                $("#Text_Noti").hide();
                $("#Text_Noti_DeviceToken").hide();
            });

            $("#Text_Notif_WithDeviceToken").click(function (event) {
                
                $("#Text_Noti_WCard").hide();
                $("#Camp_Noti").hide();
                $("#Text_Noti").hide();
                $("#Text_Noti_DeviceToken").show();
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


            $("#submitTextWCard").click(function (event) {

                var result = wcardformvalidate();
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
                else { return false; }
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

        function wcardformvalidate() {
            var messagevalidation = true;

            var campvalue = $("#TextBodyWCard").val();

            var fileValue = $("#uploadFileWCard").val();
           //// alert(fileValue);
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
                $('#campbodytexterrorWCard').text('*required');
            }
            else {
                $('#campbodytexterrorWCard').text('');

            }

            if (fileValue.trim() == null || fileValue.trim() == "") {

                messagevalidation = false;
                $('#fileerrorWCard').text('*required');
            }
            else {
                $('#fileerrorWCard').text('');

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


            $("#dtNotificationWCard").datetimepicker(
              {
                  format: 'DD.MM.YYYY HH:mm',
                  sideBySide: true,
                  defaultDate: new Date(),
              }
              );

            $("#dtNotificationWDeviceToken").datetimepicker(
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