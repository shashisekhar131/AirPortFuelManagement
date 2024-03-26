function populateAircraftForm(id){
    $.ajax({
        url: 'https://localhost:7053/api/Aircraft/' + id, 
        type: 'GET', 
        headers: {
            'Authorization': 'Bearer ' + (localStorage.getItem('jwtToken') || null)
        },
        success: function(response) {
            $('#AircraftNumber').val(response.aircraftNumber);
            $('#AirLine').val(response.airLine);
            $('#Source').append($('<option>', { value: response.source, text: response.source })).val(response.source);
            $('#Destination').append($('<option>', { value: response.destination, text: response.destination })).val(response.destination);
 
       },
        error: function(xhr, status, error) {
            console.error('Error:', error);
        }
    });
}

function insertAircraft(aircraft){  

    $.ajax({
        url: 'https://localhost:7053/api/Aircraft/InsertAircraft', 
        type: 'POST', 
        headers: {
            'Authorization': 'Bearer ' + (localStorage.getItem('jwtToken') || null)
        },
        contentType: 'application/json',
        data: JSON.stringify(aircraft), 
        success: function(response) {
            window.location.href="../Views/AircraftsList.html"; 
       },
        error: function(xhr, status, error) {
            console.error('Error:', error);
        }
    });
}

function updateAircraft(aircraft){
    

    $.ajax({
        url: 'https://localhost:7053/api/Aircraft/UpdateAircraft', 
        type: 'PUT', 
        headers: {
            'Authorization': 'Bearer ' + (localStorage.getItem('jwtToken') || null)
        },
        contentType: 'application/json',
        data: JSON.stringify(aircraft), 
        success: function(response) {
            window.location.href="../Views/AircraftsList.html";   
       },
        error: function(xhr, status, error) {
            console.error('Error:', error);
        }
    });
}

function populateDropdowns(){
    $.ajax({
        url: 'https://localhost:7053/api/Airport', 
        type: 'GET',
        headers: {
            'Authorization': 'Bearer ' + (localStorage.getItem('jwtToken') || null)
        },
        success: function (data) {          
            $('#Source').empty();
            $('#Destination').empty();
            
            data.forEach(function (airport) {
                var option = $('<option>');
                option.val(airport.airportId); 
                option.text(airport.airportName);       

                $('#Source').append(option);
                $('#Destination').append(option.clone());
            });
                
            $('#Source').val(data[0].airportId);
            $('#Destination').val(data[0].airportId);
        },
        error: function (xhr, status, error) {
            console.log(error);
            console.log(xhr.responseText);
        }
    });
}

$(document).ready(function(){
 
    populateDropdowns();    

    var id = parseInt(new URLSearchParams(window.location.search).get('id'));
    if(!isNaN(id)){
        populateAircraftForm(id);
    }else{
        id =0;
    }

    $('#aircraftForm').submit(function(event){       
  
        event.preventDefault();
        var aircraft = {
            AircraftId:id,
            AircraftNumber: $('#AircraftNumber').val(),
            AirLine: $('#AirLine').val(),
            Source: $('#Source').val(),
            Destination: $('#Destination').val()
        };

        if(id!=0) updateAircraft(aircraft);
        else insertAircraft(aircraft);       
    });
});


