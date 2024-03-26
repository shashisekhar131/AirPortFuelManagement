
function populateAirportForm(id){
    $.ajax({
        url: 'https://localhost:7053/api/Airport/' + id, 
        type: 'GET', 
        headers: {
            'Authorization': 'Bearer ' + (localStorage.getItem('jwtToken') || null)
        },
        success: function(response) {
            $('#AirportName').val(response.airportName)
            $('#FuelCapacity').val(response.fuelCapacity);
       },
        error: function(xhr, status, error) {
            console.error('Error:', error);
        }
    });
}
function insertAirport(airport){ 
    $.ajax({
        url: 'https://localhost:7053/api/Airport/InsertAirport', 
        type: 'POST', 
        headers: {
            'Authorization': 'Bearer ' + (localStorage.getItem('jwtToken') || null)
        },
        contentType: 'application/json',
        data: JSON.stringify(airport), 
        success: function(response) {
            window.location.href="../Views/AirportsList.html";      
       },
        error: function(xhr, status, error) {
            console.error('Error:', error);
        }
    });
}

function updateAirport(airport){
    $.ajax({
        url: 'https://localhost:7053/api/Airport/UpdateAirport', 
        type: 'PUT', 
        headers: {
            'Authorization': 'Bearer ' + (localStorage.getItem('jwtToken') || null)
        },
        contentType: 'application/json',
        data: JSON.stringify(airport), 
        success: function(response) {
            window.location.href="../Views/AirportsList.html";     
       },
        error: function(xhr, status, error) {
            console.error('Error:', error);
        }
    });
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