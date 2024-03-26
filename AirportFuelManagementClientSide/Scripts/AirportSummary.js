

$(document).ready(function () {

    var airportTable = $('#airportTable').DataTable({
        lengthMenu:  [5, 10, 15, 20] , 
        pageLength: 5 
        });

        $.ajax({
            url: 'https://localhost:7053/api/Airport/GetAiportSummary', 
            type: 'GET',
            headers: {
                'Authorization': 'Bearer ' + (localStorage.getItem('jwtToken') || null)
            },
            success: function (data) {
              
                data.forEach(function (airport) {
                    airportTable.row.add([
                        airport.airportName,
                        airport.fuelAvailable
                    ]);
                });
                airportTable.draw();
            },
            error: function (xhr, status, error) {
                console.log(error);
                console.log(xhr.responseText);
            }
        });

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