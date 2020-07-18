var common = {
    init: function () {
        common.registerEvent();
    },
    registerEvent: function () {
        $(".txtSearch").autocomplete({
            minLength: 1,
            source: function (request, response) {
                $.ajax({
                    url: "/Book/ListName",
                    dataType: "json",
                    data: {
                        q: request.term
                    },
                    success: function (res) {
                        console.log(res.data)
                        response(res.data);
                    }
                });
            },
            appendTo: "#search_mini_form",
            focus: function (event, ui) {
                $(".txtSearch").val(ui.item.label);
                return true;
            },
            select: function (event, ui) {
                $(".txtSearch").val(ui.item.label);
                return true;
            }
        }).autocomplete("instance")._renderItem = function (ul, item) {
            return $('<li>')
                .append("<a>" + item.label + "</a>")
                .appendTo(ul);
        };
    }
}
common.init();