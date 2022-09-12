(function () {

    angular.module('dashboardModule').controller('dashboardController',
        [
            '$scope', '$rootScope',
            function ($scope, $rootScope) {

                $scope.options1 = {
                    "chart": {
                        "color": ['#00FFFF', '#1F1F1F'],
                        "type": "pieChart",
                        "height": 400,
                        "showLabels": false,
                        "duration": 500,
                        "labelThreshold": 0.01,
                        "labelSunbeamLayout": true,
                        "legend": {
                            "margin": {
                                "top": 5,
                                "right": 35,
                                "bottom": 5,
                                "left": 0
                            },
                        "dispatch": {},
                        "width": 400,
                        "height": 20,
                        "align": true,
                        "maxKeyLength": 20,
                        "rightAlign": true,
                        "padding": 32,
                        "updateState": true,
                        "radioButtonMode": false,
                        "expanded": false,
                        "vers": "classic"
                        },
                        "dispatch": {},
                        "pie": {
                            "dispatch": {},
                            "arcsRadius": [],
                            "width": 500,
                            "height": 500,
                            "showLabels": true,
                            "title": false,
                            "titleOffset": 0,
                            "labelThreshold": 0.02,
                            "id": 5923,
                            "endAngle": false,
                            "startAngle": false,
                            "padAngle": false,
                            "cornerRadius": 0,
                            "donutRatio": 0.5,
                            "labelsOutside": false,
                            "labelSunbeamLayout": false,
                            "donut": false,
                            "growOnHover": true,
                            "pieLabelsOutside": false,
                            "donutLabelsOutside": false,
                            "margin": {
                                "top": 0,
                                "right": 0,
                                "bottom": 0,
                                "left": 0
                            },
                            "labelType": "key"
                        },
                        "tooltip": {
                            "duration": 0,
                            "gravity": "w",
                            "distance": 25,
                            "snapDistance": 0,
                            "classes": null,
                            "chartContainer": null,
                            "enabled": true,
                            "hideDelay": 200,
                            "headerEnabled": false,
                            "fixedTop": null,
                            "offset": {
                                "left": 0,
                                "top": 0
                            },
                            "hidden": true,
                            "data": null,
                            "id": "nvtooltip-65816"
                        },
                        "arcsRadius": [],
                        "width": null,
                        "title": false,
                        "titleOffset": 0,
                        "endAngle": false,
                        "startAngle": false,
                        "padAngle": false,
                        "cornerRadius": 0,
                        "donutRatio": 0.25,
                        "labelsOutside": false,
                        "donut": false,
                        "growOnHover": true,
                        "pieLabelsOutside": false,
                        "donutLabelsOutside": false,
                        "margin": {
                            "top": 30,
                            "right": 20,
                            "bottom": 20,
                            "left": 20
                        },
                        "labelType": "key",
                        "noData": null,
                        "showLegend": false,
                        "legendPosition": "top",
                        "defaultState": null
                    },
                    "title": {
                        "enable": false,
                        "text": "Write Your Title",
                        "className": "h4",
                        "css": {
                            "width": "nullpx",
                            "textAlign": "center"
                        }
                    },
                    "subtitle": {
                        "enable": false,
                        "text": "Write Your Subtitle",
                        "css": {
                            "width": "nullpx",
                            "textAlign": "center"
                        }
                    },
                    "caption": {
                        "enable": false,
                        "text": "Figure 1. Write Your Caption text.",
                        "css": {
                            "width": "nullpx",
                            "textAlign": "center"
                        }
                    },
                    "styles": {
                        "classes": {
                            "with-3d-shadow": true,
                            "with-transitions": true,
                            "gallery": false
                        },
                        "css": {}
                    }
                };

                $scope.data1 = [
                    {
                        key: "efficiency",
                        y: 7,
                    },
                    {
                        key: "empty",
                        y: 3
                    }
                ];

                $scope.options2 = $scope.options1;
                $scope.data2 = $scope.data1;
                $scope.options2.height = 180;


            }]);

}());



