(function ($) {

    var defaults = {
        id: 0,
        title: '会议主题',
        potision: 1,
        people: '',
        level: 0,
        editable: true,
        titleColor: '',
        start: new Date(),
        end: (new Date()).setHours(1),
        memo: '',
        positions: [],
        submit: function () {
        },
        close: function () {
        },
        remove: function () {
        }
    };

    $.fn.editPanel = function (options) {
        var args = Array.prototype.slice.call(arguments, 1); // for a possible method call
        var res = this; // what this function will return (this jQuery object by default)

        this.each(function (i, _element) { // loop each DOM element involved
            var element = $(_element);
            var calendar = element.data('editPanel'); // get the existing calendar object (if any)
            var singleRes; // the returned value of this single method call

            // a method call
            if (typeof options === 'string') {
                if (calendar && $.isFunction(calendar[options])) {
                    singleRes = calendar[options].apply(calendar, args);
                    if (!i) {
                        res = singleRes; // record the first method call result
                    }
                    if (options === 'destroy') { // for the destroy method, must remove Calendar object data
                        element.removeData('editPanel');
                    }
                }
            }
            // a new calendar initialization
            else if (!calendar) { // don't initialize twice
                calendar = new EditPanel(element, options);
                element.data('editPanel', calendar);
            }
        });

    }

    function mergeOptions(target) {

        function mergeIntoTarget(name, value) {
            if ($.isPlainObject(value) && $.isPlainObject(target[name])) {
                // merge into a new object to avoid destruction
                target[name] = mergeOptions({}, target[name], value); // combine. `value` object takes precedence
            }
            else if (value !== undefined) { // only use values that are set and not undefined
                target[name] = value;
            }
        }

        for (var i = 1; i < arguments.length; i++) {
            $.each(arguments[i], mergeIntoTarget);
        }

        return target;
    }

    function EditPanel(element, instanceOptions) {
        var t = this;

        var options = mergeOptions({}, defaults, instanceOptions);

        var eventId = options.id;
        var eventTitle = options.title;
        var eventPosition = options.position;
        var eventStartTime = options.start;
        var eventEndTime = options.end;
        var eventPeople = options.people;
        var eventMemo = options.memo;
        var jsEvent = options.jsEvent;
        var positions = options.positions;
        var level = options.level;
        var eventMemo = options.memo;
        var editAble = options.editable;
        var titleColor = options.titleColor;

        var editPanelHtml =
            '<div id="editPanel"><div class="ep-content">'
                + '<div class="ep-dt ep-title"><input type="text" id="eveTitle" class="ep-input ep-title" style="color:' + titleColor + '" value="' + eventTitle + '" /></div>'
                + '<div class="ep-dt ep-position"><span class="ep-tip">地点</span><select id="evePosition" class="ep-select ep-select-pos chosen-select"></select></div>'
                + '<div class="ep-dt ep-start"><span class="ep-tip">从</span><input type="text" id="eveStartDate" class="datepicker ep-input ep-date ep-date-start"/><select id="eveStartSp" class="ep-select ep-select-time chosen-select" ><option value="0">上午</option><option value="1">下午</option></select><input type="text" id="eveStartTime" class="ep-input ep-time"/></div>'
                + '<div class="ep-dt ep-end"><span class="ep-tip">到</span><input type="text" id="eveEndDate" class="datepicker ep-input ep-date ep-date-start"/><select id="eveEndSp" class="ep-select ep-select-time chosen-select" ><option value="0">上午</option><option value="1">下午</option></select><input type="text" id="eveEndTime" class="ep-input ep-time"/></div>'
                + '<div class="ep-dt ep-people"><span class="ep-tip">人员</span><input type="text" id="evePeople" class="ep-input ep-people"/></div>'
                + '<div class="ep-dt ep-memo"><span class="ep-tip">备注</span><input type="text" id="eveMemo" class="ep-input ep-memo" /></div>'
            + '</div><div class="ep-action">';

        if (eventId && editAble) {
            editPanelHtml += '<span id="eveDelete" class="epc-bt ep-action-del">删除</span>';
        }

        if (!eventId || editAble) {
            editPanelHtml += '<span id="eveSubmit" class="epc-bt ep-action-sub">提交</span><span class="epc-bt ep-action-sp">|</span>';
        }

        editPanelHtml += '<span id="eveClose" class="epc-bt ep-action-clo">取消</span><div class="clear"></div></div>';

        var editPanel = $(editPanelHtml);

        $.each(positions, function (index, data) {
            if (data) {
                editPanel.find('#evePosition')
                .append('<option value=' + data.value + '>' + data.text + '</option>');
            }
        });

        editPanel.find('#evePosition').val(eventPosition);

        if (eventStartTime.hours() > 12) {
            editPanel.find('#eveStartSp').val(1);
        } else {
            editPanel.find('#eveStartSp').val(0);
        }
        editPanel.find('#eveStartTime').val(eventStartTime.format('h:mm'));

        if (eventEndTime.hours() > 12) {
            editPanel.find('#eveEndSp').val(1);
        } else {

            editPanel.find('#eveEndSp').val(0);
        }
        editPanel.find('#eveEndTime').val(eventEndTime.format('h:mm'));

        editPanel.find('#eveStartDate').val(eventStartTime.format('YYYY-MM-DD'));
        editPanel.find('#eveEndDate').val(eventEndTime.format('YYYY-MM-DD'));
        editPanel.find('#evePeople').val(eventPeople);
        editPanel.find('#eveMemo').val(eventMemo);

        editPanel.find('#eveDelete').click(function () {
            options.remove(this, options);
            destroy();
        });

        editPanel.find('#eveClose').click(function () {
            options.close(this, options);
            destroy();
        });

        editPanel.find('#eveSubmit').click(function () {
            var start = moment($('#eveStartDate').val() + ' ' + $('#eveStartTime').val());
            var end = moment($('#eveEndDate').val() + ' ' + $('#eveEndTime').val());

            options.submit(editPanel, {
                id: options.id,
                title: $('#eveTitle').val(),
                position: $('#evePosition').val(),
                start: $('#eveStartSp').val() == 0 ? start : start.add('hour', 12),
                end: $('#eveEndSp').val() == 0 ? end : end.add('hour', 12),
                people: $('#evePeople').val(),
                memo: $('#eveMemo').val(),
                level: options.level
            });
            destroy();
        });

        element.append(editPanel);

        editPanel.find(".datepicker").datepicker();
        editPanel.find(".chosen-select").chosen({ disable_search_threshold: 6 });

        $(".ep-select-time").chosen().change(function () {
            var startSp = $('#eveStartSp').val();
            var endSp = $('#eveEndSp').val();
            var startHour = parseInt($('#eveStartTime').val().split(':')[0]) + startSp * 12;
            var endHour = parseInt($('#eveEndTime').val().split(':')[0]) + endSp * 12;

            if (startHour > endHour) {
                if ($(this).attr('id') == 'eveStartSp') {
                    if (startSp == 1) {
                        $(this).val(0);
                    }
                } else {
                    if (endSp == 0) {
                        $(this).val(1);
                    }
                }

                $('.ep-select-time').trigger("chosen:updated");
            }
        });

        editPanel.find(".ep-time").change(function () {
            var val = $(this).val();
            var reg = /(\d{1,2}):(\d{1,2})/;

            if (reg.test(val) == false) {
                val = moment().format('H:mm');
            }

            var hour = val.split(':')[0];
            var minute = val.split(':')[1];

            if (hour > 12) {
                $(this).parent().find('.ep-select-time').val(1).trigger("chosen:updated");
                val = (hour - 12) + ':' + minute;
            }

            if ($(this).attr('id') == 'eveStartTime') {
                var endTime = $('#eveEndTime').val();
                if ($(this).parent().find('.ep-select-time').val() == 1
                && endTime.split(':')[0] < hour) {
                    $(this).parent().find('.ep-select-time').val(0).trigger("chosen:updated");
                }
            }

            if ($(this).attr('id') == 'eveEndTime') {
                var startTime = $('#eveStartTime').val();
                if ($(this).parent().find('.ep-select-time').val() == 0
                && startTime.split(':')[0] > hour) {
                    $(this).parent().find('.ep-select-time').val(1).trigger("chosen:updated");
                }
            }

            $(this).val(val);
        });

        editPanel.find(".datepicker").change(
        function (e) {
            var val = $(this).val();
            var curDate = new Date();

            if (/\d{2}-\d{1,2}-\d{1,2}/.test(val) == false
            && /\d{2}\\\d{1,2}\\\d{1,2}/.test(val) == false) {
                val = eventStartTime.format('YYYY-MM-DD');
            }

            val = moment(val).format('YYYY-MM-DD');

            $(this).val(val);

            if ($(this).attr('id') == 'eveStartDate') {
                $('#eveEndDate').val(val);
            }
        });

        var hiddenX = jsEvent.pageX - jsEvent.clientX;
        var hiddenY = jsEvent.pageY - jsEvent.clientY;
        var panelX = jsEvent.pageX + 10;
        var panelY = jsEvent.pageY - editPanel.outerHeight() / 2;

        if (document.body.clientWidth <= jsEvent.clientX + editPanel.outerWidth() + 20) {
            panelX = jsEvent.pageX - editPanel.outerWidth() - 10;
        }


        if (window.innerHeight < jsEvent.clientY + editPanel.outerHeight() / 2) {
            panelY = window.innerHeight + hiddenY - editPanel.outerHeight() - 10;
        }

        if (jsEvent.clientY < editPanel.outerHeight() / 2) {
            panelY = 10 + hiddenY;
        }

        editPanel.css({ 'left': panelX, 'top': panelY });

        function destroy() {
            editPanel.remove();
            element.find('.datepicker-container').remove();
            element.removeData('editPanel');
        }

        t.destroy = destroy;
    }
})(jQuery);

	
