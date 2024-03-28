 
    function deleteTransactions() {
        function handleSuccess(data) {            
            location.reload();    
        }
    
        function handleError(xhr, status, error) {
            console.log(error);
            console.log(xhr.responseText);
        }
    
        makeDeleteRequest('/Transaction/RemoveAllTransactions', handleSuccess, handleError);
     
      }

    
    $(document).ready(function () {
       
        $('#confirmDeleteBtn').click(function() {
            deleteTransactions();            
          });
          
        var transactionTable = $('#transactionTable').DataTable({
            lengthMenu:  [3,5,10,15] , 
            pageLength: 3
            });
        $('#transactionTable tbody').on('click', '.reverse-transaction-btn', function() {
            var id = $(this).data('transaction-id');
            window.location.href = "../Views/ReverseTransactionForm.html?parentTransactionId=" + id;
        });
        
        loadTransactionData();        
    
        function loadTransactionData() {

            function handleSuccess(data) {            
                console.log(data);

                transactionTable.clear();
                // Add each transaction to the DataTable
                data.forEach(function (transaction) {
                    var reverseTransactionBtn = '<button class="btn btn-primary btn-sm reverse-transaction-btn" data-transaction-id="' + transaction.transactionId + '">reverse</button>';

                    transactionTable.row.add([
                        transaction.transactionId,
                        transaction.transactionTime,
                        (transaction.transactionType == 1)?"IN":"OUT",
                        transaction.airportName,
                        (transaction.transactionType == 1)?"":transaction.aircraftName,
                        transaction.quantity,
                        transaction.transactionIdparent,
                        reverseTransactionBtn
                    ]);
                });
                // Draw the updated DataTable
                transactionTable.draw();
        
            }
        
            function handleError(xhr, status, error) {
                console.log(error);
                console.log(xhr.responseText);
            }
        
            makeGetRequest('/Transaction', handleSuccess, handleError);           
        }
    });