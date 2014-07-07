﻿var SelectedItemId = [];

$('.ls-item-check').click(function (event) {
    event.stopPropagation();
    if ($(this).is(':checked')) {
        $('.item-act').addClass('item-act-selected');
        $(this).parents('tr').addClass('ls-item-selected');

        SelectedItemId = [];
        $('.ls-item-check:checked').each(function () {
            SelectedItemId.push($(this).attr('itemid'));
        });
    } else {
        $(this).parents('tr').removeClass('ls-item-selected');
        if ($('.ls-item-check:checked').length == 0) {
            $('.item-act').removeClass('item-act-selected');
        }

        SelectedItemId = [];
        $('.ls-item-check:checked').each(function () {
            SelectedItemId.push($(this).attr('itemid'));
        });
    }

});

function listItemClick(e) {
    event.stopPropagation();
    var itemChecker = $(e).find('.ls-item-check');
    itemChecker.trigger('click');
}

$(".datepicker").datepicker();
$(".chosen-select").chosen({ disable_search_threshold: 6 });