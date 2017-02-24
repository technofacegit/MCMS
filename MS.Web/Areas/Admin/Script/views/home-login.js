
(function ($) {
    function HomeLogin() {
     
        var $this = this, formLogin;

        function initializeForm() {
            formLogin = new Global.FormValidationReset($('#login-box form'));
        }

        $this.init = function () {
            initializeForm();
        };
    }

    $(function () {
        var self = new HomeLogin();
        self.init();
    });

}(jQuery));