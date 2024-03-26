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
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
}


function fetchParentTransaction(id) {
    $.ajax({
        url: 'https://localhost:7053/api/Transaction/GetTransactionById/' + id,
        type: 'GET',
        headers: {
            'Authorization': 'Bearer ' + (localStorage.getItem('jwtToken') || null)
        },
        success: function (data) {         
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
            $('#aircraftName').prop('disabled', true);
        } else { // If transaction type is 'OUT', set it and populate the aircraft dropdown
            $('#transactionType').val('IN');
            fetchAircrafts(); // Call the function to populate aircraft dropdown
            $('#aircraftName').val(data.aircraftId);
        }

        // Disable all form fields to make it read-only
        $('#airportName').prop('disabled', true);
        $('#transactionType').prop('disabled', true);
        $('#parentID').prop('disabled',true);
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
}

$(document).ready(function () {          

// fetch the data and populate into dropdowns 
const id = new URLSearchParams(window.location.search).get('parentTransactionId');

fetchParentTransaction(id);


$('#transactionForm').submit(function (event) {
    event.preventDefault(); 
    
    var  Transaction = {
        transactionType: (($('#transactionType').val() == "IN")?1:2),
        airportId: parseInt($('#airportName').val()),
        aircraftId: (($('#transactionType').val() == "IN")?0:parseInt($('#aircraftName').val())) ,
        quantity: parseFloat($('#quantity').val()),
        TransactionIdparent:id
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