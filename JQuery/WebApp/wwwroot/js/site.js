//(function ($) {
//    $.fn.myMenu = function (options) {
//        var settings = $.extend({
//            items: [] // メニュー項目の配列
//        }, options);

//        var selectedValue = null;

//        function getSelectedValue() {
//            return selectedValue;
//        }

//        // メニューの生成
//        var $ul = this.find('.dropdown-menu');
//        $.each(settings.items, function (index, item) {
//            var $li = $('<li></li>');
//            var $button = $('<button class="dropdown-item" type="button"></button>')
//                .attr('data-value', item.value)
//                .text(item.name)
//                .on('click', function () {
//                    selectedValue = item.value;
//                    $ul.trigger('selected.mymenu', [selectedValue]);
//                });
//            $li.append($button);
//            $ul.append($li);
//        });

//        return this;
//    };
//})(jQuery);

(function ($) {
    $.fn.myMenu = function () {
        return this.each(function () {
            var $li = $(this);

            var selectedValue = $li.find('button.dropdown-item').attr('data-value');

            $li.on('click', 'button.dropdown-item', function () {
                var clickedValue = $(this).attr('data-value');
                $li.trigger('selected.mymenu', [clickedValue]);
            });
        });
    };
})(jQuery);