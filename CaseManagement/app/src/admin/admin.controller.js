angular.module("caseManagement")
    .controller("AdminCtrl", function (DropDown, $injector, $mdToast) {
        var vm = this;
        vm.dropDown = {};
        vm.dropDownList = [];
        var nameKey = "";

        vm.dropDowns = [
            { id: "gender", name: "Gender" },
            { id: "race", name: "Race" },
            { id: "language", name: "Language" },
            { id: "ethnicity", name: "Ethnicity" },
            { id: "visitResult", name: "Visit Result" },
            { id: "learnAbout", name: "Learn Abouts" },
            { id: "need", name: "Need" },
            { id: "referralType", name: "Referral Type" },
            { id: "contactType", name: "Contact Type" },
            { id: "responder", name: "Responder" },
        ];

        vm.getList = function (chosen) {
            vm.dropDown = vm.dropDowns.filter(function (d) { return d.id === chosen; })[0];
            DropDown([chosen])[chosen].$promise.then(function (data) {
                nameKey = vm.__getNameKey(data[0]);
                data.forEach(function (d) {
                    d.name = d[nameKey];
                });

                vm.dropDownList = data;
            });
        };

        vm.save = function () {
            var idKey = vm.dropDown.id + "Id";
            var resource = vm.__getResource(vm.dropDown.name);
            var item = {};
            item[nameKey] = vm.name;
            item[idKey] = vm.__getNextId(vm.dropDownList, idKey);
            resource.save(item, function () {
                vm.getList(vm.dropDown.id);
                vm.name = "";
            });
        };

        vm.remove = function (item) {
            var idKey = vm.dropDown.id + "Id";
            var resource = vm.__getResource(vm.dropDown.name);
            resource.remove({ id: item[idKey] },
                function () {
                    vm.getList(vm.dropDown.id);
                }, function (error) {
                    $mdToast.show(
                        $mdToast.simple()
                         .textContent("Unable to delete item because it is currently assigned to clients")
                         .position('top right')
                         .hideDelay(1500)
                    );
                }
            );
        };

        vm.__getNextId = function (list, key) {
            var ids = list.map(function (d) { return +d[key]; });
            var maxId = Math.max.apply(null, ids.filter(function (d) { return d !== 99; }));
            var nextId = maxId === 98 ? 100 : maxId + 1;
            return nextId;
        };

        vm.__getNameKey = function (object) {
            for (var key in object) {
                if (object.hasOwnProperty(key)) {
                    if (key.toLowerCase().indexOf("name") > -1) {
                        return key;
                    }
                }
            }
        };

        vm.__getResource = function (name) {
            return $injector.get(name.replace(/ /g, ''));
        };
    });