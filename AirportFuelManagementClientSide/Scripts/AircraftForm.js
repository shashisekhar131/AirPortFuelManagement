function populateAircraftForm(id){

    function handleSuccess(response) {
        console.log(response);
        $('#AircraftNumber').val(response.aircraftNumber);
        $('#AirLine').val(response.airLine);
        $('#Source').append($('<option>', { value: response.sourceId, text: response.sourceName })).val(response.sourceId);
        $('#Destination').append($('<option>', { value: response.destinationId, text: response.destinationName })).val(response.destinationId);

    }

    function handleError(xhr, status, error) {
        console.log(error);
        console.log(xhr.responseText);
    }

    makeGetRequest('/Aircraft/' + id, handleSuccess, handleError);
    
}

function insertAircraft(aircraft){  
    function handleSuccess(response) {
        window.location.href="../Views/AircraftsList.html"; 
   }

    function handleError(xhr, status, error) {
        console.log(error);
        console.log(xhr.responseText);
    }

    makePostRequest('/Aircraft/InsertAircraft',aircraft, handleSuccess, handleError);

}

function updateAircraft(aircraft){
    
    function handleSuccess(response) {
        window.location.href="../Views/AircraftsList.html"; 
   }

    function handleError(xhr, status, error) {
        console.log(error);
        console.log(xhr.responseText);
    }

    makePutRequest('/Aircraft/UpdateAircraft',aircraft, handleSuccess, handleError);
}

function populateDropdowns(){

    function handleSuccess(data) {
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
    }

    function handleError(xhr, status, error) {
        console.log(error);
        console.log(xhr.responseText);
    }

    makeGetRequest('/Airport', handleSuccess, handleError);
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
            SourceId: $('#Source').val(),
            DestinationId: $('#Destination').val()
        };

        if(id!=0) updateAircraft(aircraft);
        else insertAircraft(aircraft);       
    });
});


