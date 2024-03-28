
function loadAircraftData() {
   
    var aircraftTable = $('#aircraftTable').DataTable({
        lengthMenu:  [3, 5, 10, 15, 20] , 
        pageLength: 3 
        });
    $('#aircraftTable tbody').on('click', '.edit-btn', function() {
        var aircraftId = $(this).data('aircraft-id');
        window.location.href = "../Views/AircraftForm.html?id=" + aircraftId;
    });

    function handleSuccess(data) {
        console.log(data);
            aircraftTable.clear();
            data.forEach(function (aircraft) {
                var editButton = '<button class="btn btn-primary btn-sm edit-btn" data-aircraft-id="' + aircraft.aircraftId + '">Edit</button>';

                aircraftTable.row.add([
                    aircraft.aircraftId,
                    aircraft.aircraftNumber,
                    aircraft.airLine,
                    aircraft.sourceName,
                    aircraft.destinationName,
                    editButton
                ]);
                
            });
            aircraftTable.draw(); 
    }

    function handleError(xhr, status, error) {
        console.log(error);
        console.log(xhr.responseText);
    }

    makeGetRequest('/Aircraft', handleSuccess, handleError);

}

$(document).ready(function () {    
        loadAircraftData();      
});