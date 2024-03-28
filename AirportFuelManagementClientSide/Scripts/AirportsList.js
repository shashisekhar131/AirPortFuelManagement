function loadAirportData(jwtToken) {
    var airportTable = $('#airportTable').DataTable({
        lengthMenu:  [5, 10, 15, 20] , 
        pageLength: 5 
        });
        $('#airportTable tbody').on('click', '.edit-btn', function() {
            var airportId = $(this).data('airport-id');
            window.location.href = "../Views/AirportForm.html?id=" + airportId;
        });
    
        function handleSuccess(data) {
            airportTable.clear();
            data.forEach(function (airport) {
                var editButton = '<button class="btn btn-primary btn-sm edit-btn" data-airport-id="' + airport.airportId + '">Edit</button>';

                airportTable.row.add([
                    airport.airportId,
                    airport.airportName,
                    airport.fuelCapacity,
                    airport.fuelAvailable,
                    editButton
                ]);
            });
            airportTable.draw();
        }
    
        function handleError(xhr, status, error) {
            alert(status);
            console.log(error,status);
            console.log(xhr.responseText);
        }
    
        makeGetRequest('/Airport', handleSuccess, handleError);
    
}

$(document).ready(function () {
    var jwtToken = (localStorage.getItem('jwtToken') || null);
    if(jwtToken == null) window.location.href = 'UserLogin.html';
    else{
        loadAirportData(jwtToken);
    }    
});