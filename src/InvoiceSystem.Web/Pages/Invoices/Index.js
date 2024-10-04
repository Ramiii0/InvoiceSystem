$(function () {
    var l = abp.localization.getResource('InvoiceSystem');

    var dataTable = $('#invoiceTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: true,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(invoiceSystem.invoices.invoice.getList),
            columnDefs: [{
                title: 'Actions',
                rowAction: {
                    items:
                        [
                            {
                                text: 'Delete',
                                confirmMessage: function (data) {
                                    return `Are you sure to delete the book ${data.record.name}?`; 
                                },
                                action: function (data) {
                                    invoiceSystem.invoices.invoice
                                        .delete(data.record.id)
                                        .then(function () {
                                            abp.notify.info(l('SuccessfullyDeleted'));
                                            dataTable.ajax.reload();
                                        });
                                }
                            }
                            ,{
                                text: 'Edit',
                               // visible: abp.auth.isGranted('ABPClinics.Patients.Edit'),
                                action: function (data) {
                                    editModal.open({ id: data.record.id });
                                }
                            }

                        ]
                }
            },
            {
                title: 'Customer Name',
                data: "customerName"
            }, {
                title: 'Invoice Number',
                data: "invoiceNo"
            }, {
                title: 'Invoice Amount',
                data: "invoiceAmount"
            },
            {
                title: 'Total Discount',
                data: "totalDiscount",
                
                },
                {
                    title: 'Net Amount',
                    data: "netAmount"
                },
            {
                title: 'Invoice Date', data: "creationTime",
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

    var createModal = new abp.ModalManager(abp.appPath + 'Invoices/Create');
    var editModal = new abp.ModalManager(abp.appPath + 'Invoices/Edit');

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });
    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewInvoiceButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});
