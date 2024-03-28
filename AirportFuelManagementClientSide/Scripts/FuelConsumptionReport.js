
$(document).ready(function () {
    var fuelConsumptionTable = $('#fuelConsumptionTable').DataTable({
        lengthMenu:  [3,5,10,15] , 
        pageLength: 3
        });

        function handleSuccess(data) {            
            data.forEach(function (record) {
                var airportData = {
                    airportName: record.airportName,
                    airportFuelAvailable: record.airportFuelAvailable,
                    transactions: record.transactions
                };
    
                fuelConsumptionTable.row.add([
                    airportData.airportName,
                    renderTransactions(airportData.transactions),
                    airportData.airportFuelAvailable
                ]);
            });
    
            fuelConsumptionTable.draw();
        }
    
        function handleError(xhr, status, error) {
            console.log(error);
            console.log(xhr.responseText);
        }
    
        makeGetRequest('/Transaction/FuelConsumptionReport', handleSuccess, handleError);
   

    function renderTransactions(transactions) {
        if(transactions.length == 0) return "";
        var html = '<table class="table table-bordered">';
        html += '<thead><tr><th>Date/time</th><th>Type</th><th>Fuel</th><th>Aircraft</th></tr></thead>';
        html += '<tbody>';
    
        transactions.forEach(function (transaction) {
            html += '<tr>';
            html += `<td>${transaction.transactionTime}</td>`;
            html += `<td>${(transaction.transactionType==1)?"IN":"OUT"}</td>`;
            html += `<td>${transaction.quantity}</td>`;
            html += `<td>${(transaction.transactionType==1)?"":transaction.aircraftName}</td>`;
            html += '</tr>';
        });
    
        html += '</tbody></table>';
        return html;
    }
    
    $('#exportPdfBtn').on('click', function () {
    var columns = ["Airport", "Transactions", "Fuel Available"]; 

    var rows = [];
    // Insert data table row data in rows array
    fuelConsumptionTable.rows().data().each(function (value, index) {
        var rowData = [];
        rowData.push(value[0]); // Airport Name

        // Concatenate transaction details into a single string
        var transactions = $(value[1]).find('tr').map(function() {
            return $(this).text().trim();
        }).get().join('\n');
        rowData.push(transactions);

        rowData.push(value[2]); // Airport Fuel Available

        rows.push(rowData);
    });

    var doc = new jsPDF();
    doc.text("Fuel Consumption Report", 14, 15);

    doc.autoTable({
        head: [columns],
        body: rows,
        startY: 20
    });
    doc.save('fuel_consumption_report.pdf');
});

    
});
