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

        $('#btnDeleteAll').off('click').on('click', function () {
            if (confirm('Bạn có chắc muốn làm mới giỏ hàng không?')) {
                $.ajax({
                    type: 'GET',
                    url: '/Cart/DeleteAll',
                    success: function (result) {
                        $('#cart-table-container').html(result)
                    },
                    error: function (err) {
                        console.log(err)
                    }
                })
            } else {

            }
            
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

function removeItem(bookID) {
    if (confirm('Bạn có muốn bỏ cuốn sách này khỏi giỏ hàng không?')) {
        $.ajax({
            type: 'POST',
            url: '/Cart/Remove',
            data: { bookID: bookID },
            success: function (result) {
                if (result.status) {
                    $.ajax({
                        type: 'POST',
                        url: '/Cart/CartPartial',
                        success: function (result) {
                            $('#cart-table-container').html(result)
                        },
                        error: function (err) {
                            console.log(err)
                        }
                    })
                }
            },
            error: function (err) {
                console.log(err)
            }
        })
    } else {

    }
    
}