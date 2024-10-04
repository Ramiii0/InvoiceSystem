$(function () {
    var l = abp.localization.getResource('InvoiceSystem');

    var dataTable = $('#productTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: true,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(invoiceSystem.products.product.getList),
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
                                    invoiceSystem.products.product
                                        .delete(data.record.id)
                                        .then(function () {
                                            abp.notify.info(l('SuccessfullyDeleted'));
                                            dataTable.ajax.reload();
                                        });
                                }
                            }
                            , {
                                text: 'Edit',
                                // visible: abp.auth.isGranted('ABPClinics.Patients.Edit'),
                                action: function (data) {
                                    editModal.open({ id: data.record.id });
                                }
                            },
                            {
                                text: 'Add Discount',
                                // visible: abp.auth.isGranted('ABPClinics.Patients.Edit'),
                                action: function (data) {
                                    AddDiscount.open({ id: data.record.id });
                                }
                            }

                        ]
                }
            },
            {
                title: 'Name',
                data: "name"
            }, {
                title: 'Code',
                data: "code"
            }, {
                title: 'PartNo',
                data: "partNo"
            },
            {
                title: 'Price',
                data: "productPricingPrice",

            },
            {
                title: 'Disount %',
                data: "productDiscountDisount"
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

    var createModal = new abp.ModalManager(abp.appPath + 'Products/Create');
var editModal = new abp.ModalManager(abp.appPath + 'Products/Edit');
var AddDiscount = new abp.ModalManager(abp.appPath + 'Products/AddDiscount');

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });
    editModal.onResult(function () {
        dataTable.ajax.reload();
    });
    AddDiscount.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewProductButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});
