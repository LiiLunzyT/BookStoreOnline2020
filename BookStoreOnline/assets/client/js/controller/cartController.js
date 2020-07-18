var cart = {
    init: function () {
        cart.regEvents();
    },
    regEvents: function () {
        $('#btnContinue').off('click').on('click', function () {
            window.location.href = "/danh-muc";
        });
        $('#btnPayment').off('click').on('click', function () {
            window.location.href = "/gio-hang/thanh-toan";
        });
        $('#btnUpdate').off('click').on('click', function () {
            var listProduct = $('.txtQuantity');
            var cartList = [];
            $.each(listProduct, function (i, item) {
                cartList.push({
                    Quantity: $(item).val(),
                    book: {
                        BookID: $(item).data('bookid')
                    }
                });
            });

            $.ajax({
                url: '/Cart/Update',
                data: { cartModel: JSON.stringify(cartList) },
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/gio-hang";
                    }
                }
            })
        });

        $('#btnDeleteAll').off('click').on('click', function () {

            $.ajax({
                url: '/Cart/DeleteAll',
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/gio-hang";
                    }
                }
            })
        });

        $('.btn-delete').off('click').on('click', function (e) {
            e.preventDefault();
            $.ajax({
                data: { id: $(this).data('id') },
                url: '/Cart/Delete',
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/gio-hang";
                    }
                }
            })
        });
    }
}

cart.init();

function addItem(bookID) {
    $.ajax({
        type: 'GET',
        url: '/Cart/Increase',
        data: { bookID: bookID },
        success: function (result) {
            $('#cart-table-container').html(result)
        },
        error: function (err) {
            console.log(err)
        }
    })
}

function subItem(bookID) {
    $.ajax({
        type: 'GET',
        url: '/Cart/Decrease',
        data: { bookID: bookID },
        success: function (result) {
            $('#cart-table-container').html(result)
        },
        error: function (err) {
            console.log(err)
        }
    })
}