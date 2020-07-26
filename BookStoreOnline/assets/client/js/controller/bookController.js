var book = {
    init: function () {
        console.log('asdasd')
        book.regEvents();
    },
    regEvents: function () {
        $('.addItem').off('click').on('click', function (e) {
            e.preventDefault();
            $.ajax({
                data: { bookID: $(this).data('id'), quantity: 1  },
                url: '/Cart/AddItem',
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        let c = document.querySelectorAll('.cart-float-count')
                        c.forEach(item => item.innerHTML = res.count)

                        $.post(res.Url, function (partial) {
                            $('#cart-float-expand').html(partial)
                        })
                    }
                }
            })
        });
    }
}
book.init();