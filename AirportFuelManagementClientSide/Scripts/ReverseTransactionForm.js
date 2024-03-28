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


function fetchParentTransaction(id) {
        
    function handleSuccess(data) {     
        console.log(data)
        $('#airportName').append($('<option>', {
            value: data.airportId,
            text: data.airportName
        }));
        $('#aircraftName').append($('<option>', {
            value: data.aircraftId,
            text: data.aircraftName
        }));
        $('#quantity').val(data.quantity);
        $('#parentID').val(id);

        // If transaction type is 'IN', set it and disable the aircraft dropdown
        if (data.transactionType === 1) {
            $('#transactionType').val('OUT');            
            fetchAircrafts(); // Call the function to populate aircraft dropdown
            $('#aircraftName').val(data.aircraftId);
        } else { // If transaction type is 'OUT', set it and populate the aircraft dropdown
            $('#transactionType').val('IN');
            $('#aircraftName').prop('disabled', true);

        }

        // Disable all form fields to make it read-only
        $('#airportName').prop('disabled', true);
        $('#transactionType').prop('disabled', true);
        $('#parentID').prop('disabled',true);
    }

    function handleError(xhr, status, error) {
        console.log(error);
        console.log(xhr.responseText);
    }

    makeGetRequest( '/Transaction/GetTransactionById/' + id, handleSuccess, handleError);

}

$(document).ready(function () {          

// fetch the data and populate into dropdowns 
const id = new URLSearchParams(window.location.search).get('parentTransactionId');

fetchParentTransaction(id);


$('#transactionForm').submit(function (event) {
    event.preventDefault(); 
    
    var  Transaction = {
        transactionType: (($('#transactionType').val() == "IN")?1:2),
        aircraftId:($('#transactionType').val() == "IN")?null:parseInt($('#aircraftName').val()) ,
        airportId: parseInt($('#airportName').val()),
        quantity: parseFloat($('#quantity').val()),
        TransactionIdparent:id
    };
    function handleSuccess(response) {
        if(response.item2)window.location.href="../Views/TransactionsList.html";
        $('.toast-body').html(response.item1);
        $('#toastContainer .toast').toast('show'); 
    }

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