$(function () {
    var l = abp.localization.getResource('InvoiceSystem');

    var dataTable = $('#productsalesTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: true,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(invoiceSystem.reports.report.getItemSalesReport),
            columnDefs: [
            {
                title: 'Name',
                    data: "productName"
            }, {
                    title: 'Number Of Sales',
                    data: "numberOfSales"
            }
               
            ,
            {
                title: 'Date', data: "creationTime",
                render: function (data) {
                    return luxon
                        .DateTime
                        .fromISO(data, {
                            locale: abp.localization.currentCulture.name
                        }).toLocaleString(luxon.DateTime.DATETIME_SHORT);
                }
            }
            ]
        })
    );

  
    $('#NewInvoiceButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});


