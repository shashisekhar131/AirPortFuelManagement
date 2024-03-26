function loadAirportData(jwtToken) {
    var airportTable = $('#airportTable').DataTable({
        lengthMenu:  [5, 10, 15, 20] , 
        pageLength: 5 
        });
        $('#airportTable tbody').on('click', '.edit-btn', function() {
            var airportId = $(this).data('airport-id');
            window.location.href = "../Views/AirportForm.html?id=" + airportId;
        });
    
    $.ajax({
        url: 'https://localhost:7053/api/Airport', 
        type: 'GET',
        headers: {
            'Authorization': 'Bearer ' + jwtToken 
        },
        success: function (data) {
            airportTable.clear();
            // Add each airport to the DataTable
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
            // Draw the updated DataTable
            airportTable.draw();
           
        },
        error: function (xhr, status, error) {
            console.log(error);
            console.log(xhr.responseText);
        }
    });
}

$(document).ready(function () {
    var jwtToken = (localStorage.getItem('jwtToken') || null);
    if(jwtToken == null) window.location.href = 'UserLogin.html';
    else{
        loadAirportData(jwtToken);
    }    
});