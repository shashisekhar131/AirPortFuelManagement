function fetchAirports() {
    $.ajax({
        url: 'https://localhost:7053/api/Airport',
        type: 'GET',
        headers: {
            'Authorization': 'Bearer ' + (localStorage.getItem('jwtToken') || null)
        },
        success: function (data) {
         
            $('#airportName').empty();
            $.each(data, function (index, airport) {
                $('#airportName').append('<option value="' + airport.airportId + '">' + airport.airportName + '</option>');
            });
            $('#airportName').val(data[0].airportId);

        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
}

function fetchAircrafts() {
    $.ajax({
        url: 'https://localhost:7053/api/Aircraft',
        type: 'GET',
        headers: {
            'Authorization': 'Bearer ' + (localStorage.getItem('jwtToken') || null)
        },
        success: function (data) {                  

            $('#aircraftName').empty();
            $.each(data, function (index, aircraft) {
                $('#aircraftName').append('<option value="' + aircraft.aircraftId + '">' + aircraft.aircraftNumber + '</option>');
            });
            $('#aircraftName').val(data[0].aircraftId);

        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
}

$(document).ready(function () {          

// fetch the data and populate into dropdowns 
fetchAirports();
fetchAircrafts();
$('#aircraftName').prop('disabled', true); 
$('#transactionType').val("IN");
$('#transactionType').change(function() {
    if ($(this).val() === "IN") {
        $('#aircraftName').prop('disabled', true); 
    } else {
        $('#aircraftName').prop('disabled', false); 
    }
});
$('#transactionForm').submit(function (event) {
    event.preventDefault(); 
    
    var  Transaction = {
        TransactionType: (($('#transactionType').val() == "IN")?1:2),
        AirportId: parseInt($('#airportName').val()),
        AircraftId: (($('#transactionType').val() == "IN")?0:parseInt($('#aircraftName').val())) ,
        Quantity: parseFloat($('#quantity').val()),
    };


    $.ajax({
        url: 'https://localhost:7053/api/Transaction/InsertTransaction', 
        type: 'POST',
        headers: {
            'Authorization': 'Bearer ' + (localStorage.getItem('jwtToken') || null)
        },
        contentType: 'application/json',
        data: JSON.stringify(Transaction),
        success: function (response) {
        console.log(response);
        window.location.href="../Views/TransactionsList.html";
        },
        error: function (xhr, status, error) {
            if (xhr.status === 500) {
                $('#toastContainer .toast').toast('show');
              } else {
                alert("An error occurred while processing your request. Please try again later.");
              }         }
    });
});
});