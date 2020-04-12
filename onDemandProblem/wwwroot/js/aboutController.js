(function () {
    "use strict";

    // Getting the existing module
    angular.module("myApp")
        .controller("aboutController", ["$mdDialog", aboutController])
        .controller("DialogCtrl", ["$scope", "$mdDialog", DialogController]);

    function DialogController($scope, $mdDialog) {
        $scope.title = $mdDialog.title;
        $scope.hide = function () {
            $mdDialog.hide();
        };

        $scope.cancel = function () {
            $mdDialog.cancel();
        };

        $scope.answer = function (answer) {
            $mdDialog.hide(answer);
        };
    }

    function aboutController($mdDialog) {

        var vm = this;
        vm.errorMessage = "No error";

        // Angular material dialog =>
        vm._mdDialog = $mdDialog;
        vm.customFullscreen = false;
        vm._mdDialog.title = "This is the title";
        vm.openDialog = function (event) {
                vm._mdDialog.show({
                controller: DialogController,   
                templateUrl: '/views/dialog1.tmpl.html',
                parent: angular.element(document.body),
                targetEvent: event,
                clickOutsideToClose: true,
                fullscreen: vm.customFullscreen // Only for -xs, -sm breakpoints.
            })
            .then(function (answer) {
                vm.errorMessage = 'You said the information was "' + answer + '".';
            }, function () {
                vm.errorMessage = 'You cancelled the dialog.';
            });
        };
        // <= Angular material dialog
    }
})();