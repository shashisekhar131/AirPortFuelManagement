function fetchAirports() {

    function handleSuccess(data) {            
        $('#airportName').empty();
        $.each(data, function (index, airport) {
            $('#airportName').append('<option value="' + airport.airportId + '">' + airport.airportName + '</option>');
        });
        $('#airportName').val(data[0].airportId);

    }

    function handleError(xhr, status, error) {
        console.log(error);
        console.log(xhr.responseText);
    }

    makeGetRequest('/Airport', handleSuccess, handleError);

}

function fetchAircrafts() {

    function handleSuccess(data) {     
       
        $('#aircraftName').empty();
        $.each(data, function (index, aircraft) {
            $('#aircraftName').append('<option value="' + aircraft.aircraftId + '">' + aircraft.aircraftNumber + '</option>');
        });
        $('#aircraftName').val(data[0].aircraftId);
    }

    function handleError(xhr, status, error) {
        console.log(error);
        console.log(xhr.responseText);
    }

    makeGetRequest('/Aircraft', handleSuccess, handleError);

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
        AircraftId:($('#transactionType').val() == "IN")?null:parseInt($('#aircraftName').val()) ,
        Quantity: parseFloat($('#quantity').val()),
    };

    function handleSuccess(response) {
        if(response.item2)window.location.href="../Views/TransactionsList.html";
        $('.toast-body').html(response.item1);
        $('#toastContainer .toast').toast('show');   }

    function handleError(xhr, status, error) {
        if (xhr.status === 500) {
            $('#toastContainer .toast').toast('show');
          } else {
            alert("An error occurred while processing your request. Please try again later.");
          }  
    }

    makePostRequest('/Transaction/InsertTransaction',Transaction, handleSuccess, handleError);

});
});