(function ($) {
    function BebeMoneyKatalogKategorileriIndex() {
        var $this = this, formAddEditBebeMoneyKatalogKategorileri, formDelete;

        function initializeModalWithForm() {
            $("#modal-create-edit-BebeMoneyKatalogKategorileri").on('loaded.bs.modal', function (e) {
                formAddEditBebeMoneyKatalogKategorileri = new Global.FormHelper($(this).find("form"), { updateTargetId: "validation-summary" });
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
            $('#grid-BebeMoneyKatalogKategorileri').DataTable({
                "aoColumnDefs": [{ 'bSortable': false, 'aTargets': [3] }],
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
        var self = new BebeMoneyKatalogKategorileriIndex();
        self.init();
    });

}(jQuery));