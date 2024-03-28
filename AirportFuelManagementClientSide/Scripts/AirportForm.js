
function populateAirportForm(id){

    function handleSuccess(response) {
        $('#AirportName').val(response.airportName)
        $('#FuelCapacity').val(response.fuelCapacity);
    }

    function handleError(xhr, status, error) {
        console.log(error);
        console.log(xhr.responseText);
    }

    makeGetRequest('/Airport/' + id, handleSuccess, handleError);
}
function insertAirport(airport){ 
    function handleSuccess(response) {
        window.location.href="../Views/AirportsList.html"; 
   }

    function handleError(xhr, status, error) {
        console.log(error);
        console.log(xhr.responseText);
    }

    makePostRequest('/Airport/InsertAirport',airport, handleSuccess, handleError);
 
}

function updateAirport(airport){
    function handleSuccess(response) {
        window.location.href="../Views/AirportsList.html";     
    }

    function handleError(xhr, status, error) {
        console.log(error);
        console.log(xhr.responseText);
    }

    makePutRequest('/Airport/UpdateAirport',airport, handleSuccess, handleError);
}

$(document).ready(function(){
 
    var id = parseInt(new URLSearchParams(window.location.search).get('id'));
    if(!isNaN(id)){
        populateAirportForm(id);
    }else{
        id =0;
    }

    $('#airportForm').submit(function(event){
       event.preventDefault(); 
       var airport = {   
       airportId:id,
       airportName: $('#AirportName').val(),
       fuelCapacity:parseInt($('#FuelCapacity').val())
      };

      if(id!=0) updateAirport(airport);
      else insertAirport(airport);
    
   });
});