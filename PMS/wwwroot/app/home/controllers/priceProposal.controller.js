(function () {

    angular.module('homeModule').controller('priceProposalController',
        [
            '$scope', '$rootScope', 'homeService', 'pmsModels', '$state',
            function ($scope, $rootScope, homeService, pmsModels, $state,) {


                $scope.goto = function () {
                    $scope.model.distance = Number(distance.substr(0, distance.length - 3));
                }
                $scope.model = new pmsModels.CalculatePriceProposalViewModel();
                $scope.model.distance = document.getElementById("dvDistance").value;

                $scope.user = sessionStorage.getItem('Fullname');
                $scope.role = sessionStorage.getItem('Role');

                $scope.createPriceProposal = function () {
                    $scope.model.addressFrom = document.getElementById("travelFrom").value;
                    $scope.model.addressTo = document.getElementById("travelTo").value;
                    $scope.model.distance = document.getElementById("dvDistance").value;
                    if ($scope.model.distance.substr(($scope.model.distance.length) - 2, 2) == "km") {
                        $scope.model.distance = Number(distance.substr(0, distance.length - 3));
                    };
                    homeService.createPriceProposal($scope.model)
                        .then(function (result) {
                            $state.go('Home', {
                            });
                        });
                };

                $scope.reloadTable = function () {
                    $scope.userDataTable.reload();
                };

                $scope.back = function () {
                    $state.go('Home', {
                    });
                }

            }]);
}());


let travelFrom;
let addressFrom;
let travelTo;
let addressTo;
let directionsDisplay;
let directionsService;
let distance;

function Autocmplete() {
    travelFrom = new google.maps.places.Autocomplete(
        document.getElementById('travelFrom'),
        {
            types: ['establishment'],
            componentRestrictions: { 'country': ['MKD'] },
            fields: ['place_id', 'geometry', 'name']
        });

    travelTo = new google.maps.places.Autocomplete(
        document.getElementById('travelTo'),
        {
            types: ['establishment'],
            componentRestrictions: { 'country': ['MKD'] },
            fields: ['place_id', 'geometry', 'name']
        });

    travelFrom.addListener('place_changed', onPlaceFromChanged);
    travelTo.addListener('place_changed', onPlaceToChanged);

    function onPlaceFromChanged() {
        var place = travelFrom.getPlace();
        if (!place.gemotry) {
            document.getElementById('travelFrom').placeholder = 'Enter a place'
            addressFrom = place.name;
        }
        else {
            document.getElementById('details').innerHTML = place.name;
        }
    }

    function onPlaceToChanged() {
        var place = travelTo.getPlace();
        if (!place.gemotry) {
            document.getElementById('travelTo').placeholder = 'Enter a place'
            addressTo = place.name;
        }
        else {
            document.getElementById('details').innerHTML = place.name;
        }
    }
    directionsService = new google.maps.DirectionsService();
}


function GetRoute() {
    var source, destination;
    directionsDisplay = new google.maps.DirectionsRenderer({ 'draggable': true });
    

    google.maps.event.addDomListener(window, 'load', function () {
        new google.maps.places.SearchBox(document.getElementById('travelfrom'));
        new google.maps.places.SearchBox(document.getElementById('travelto'));
        directionsDisplay = new google.maps.DirectionsRenderer({ 'draggable': true });
    });

    directionsDisplay.setMap(map);
    source = addressFrom;
    destination = addressTo;

    var request = {
        origin: source,
        destination: destination,
        travelMode: google.maps.TravelMode.DRIVING
    };

    directionsService.route(request, function (response, status) {
        if (status == google.maps.DirectionsStatus.OK) {
            directionsDisplay.setDirections(response);
        }
    });

    var service = new google.maps.DistanceMatrixService();
    service.getDistanceMatrix({
        origins: [source],
        destinations: [destination],
        travelMode: google.maps.TravelMode.DRIVING,
        unitSystem: google.maps.UnitSystem.METRIC,
        avoidHighways: false,
        avoidTolls: false
    }, function (response, status) {

        if (status == google.maps.DistanceMatrixStatus.OK &&
            response.rows[0].elements[0].status != "ZERO_RESULTS") {
            distance = response.rows[0].elements[0].distance.text;
            var duration = response.rows[0].elements[0].duration.value;
            //var dvDistance = document.getElementById("dvDistance");
            var a = distance.length;
            var b = distance.substr( 0, distance.length - 3);
            var c = parseInt(b);
            document.getElementById("dvDistance").value = c;
            document.getElementById("time").value = parseFloat(duration / 60).toFixed(2);
        }
    });
}
