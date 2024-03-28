

$(document).ready(function () {

    var airportTable = $('#airportTable').DataTable({
        lengthMenu:  [5, 10, 15, 20] , 
        pageLength: 5 
        });

        function handleSuccess(data) {            
            data.forEach(function (airport) {
                airportTable.row.add([
                    airport.airportName,
                    airport.fuelAvailable
                ]);
            });
            airportTable.draw();
        }
    
        function handleError(xhr, status, error) {
            console.log(error);
            console.log(xhr.responseText);
        }
    
        makeGetRequest('/Airport/GetAiportSummary', handleSuccess, handleError);
    
    $('#exportPdfBtn').on('click', function () {
     var columns = ["AirportName", "Fuel Available"];
     var rows = [];
     airportTable.rows().data().each(function (value, index) {
         rows.push(value);
     });

     var doc = new jsPDF();
     doc.text("fuel consumption report", 14, 15);
     doc.autoTable({
         head: [columns],
         body: rows,
         startY: 20
     });
     doc.save('fuel_consumption_report.pdf');
   });
    
});