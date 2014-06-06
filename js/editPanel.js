(function($) {

var defaults = {
	id: 0,
	title: '会议主题',
	potision: 0,
	people: '',
	start: new Date(),
	end : (new Date()).setHours(1),
	memo: '',
	submit: function(){	
	},
	close: function(){
	},
	remove: function(){
	}
};

$.fn.editPanel = function(options){
	var args = Array.prototype.slice.call(arguments, 1); // for a possible method call
	var res = this; // what this function will return (this jQuery object by default)
	
	this.each(function(i, _element) { // loop each DOM element involved
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

	for (var i=1; i<arguments.length; i++) {
		$.each(arguments[i], mergeIntoTarget);
	}

	return target;
}

function EditPanel(element, instanceOptions){
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
	
	var editPanel = $('<div id="editPanel"><div class="ep-content"><div class="ep-dt ep-title"><input type="text" id="eveTitle" class="ep-input ep-title" value="会议主题" /></div><div class="ep-dt ep-position"><span class="ep-tip">地点</span><select id="evePostion" class="ep-select ep-select-pos chosen-select"><option value="0">会议室1</option><option value="1">会议室2</option><option value="2">会议室3</option></select></div><div class="ep-dt ep-start"><span class="ep-tip">从</span><input type="text" id="eveStartDate" class="datepicker ep-input ep-date ep-date-start"/><select id="eveStartSp" class="ep-select ep-select-time chosen-select" ><option value="0">上午</option><option value="1">下午</option></select><input type="text" id="eveStartTime" class="ep-input ep-time"/></div><div class="ep-dt ep-end"><span class="ep-tip">到</span><input type="text" id="eveEndDate" class="datepicker ep-input ep-date ep-date-start"/><select id="eveEndSp" class="ep-select ep-select-time chosen-select" ><option value="0">上午</option><option value="1">下午</option></select><input type="text" id="eveEndTime" class="ep-input ep-time"/></div><div class="ep-dt ep-people"><span class="ep-tip">人员</span><input type="text" id="evePeople" class="ep-input ep-people"/></div><div class="ep-dt ep-memo"><span class="ep-tip">备注</span><input type="text" id="eveMemo" class="ep-input ep-memo" /></div></div><div class="ep-dt"><span id="eveDelete" class="ep-action ep-action-del">删除</span><span id="eveSubmit" class="ep-action ep-action-sub">提交</span><span class="ep-action ep-action-sp">|</span><span id="eveClose" class="ep-action ep-action-clo">取消</span><div class="clear"></div></div>');
	editPanel.find('#eveTitle').val(eventTitle);

	if(eventStartTime.hours() > 12){
		editPanel.find('#eveStartTime').val(eventStartTime.clone().add('hours',-12).format('HH:mm'));
		editPanel.find('#eveStartSp').val(1);
	}else{
		editPanel.find('#eveStartTime').val(eventStartTime.format('HH:mm'));
		editPanel.find('#eveStartSp').val(0);
	}
	editPanel.find("#eveStartSp").trigger("chosen:updated");
	
	if(eventEndTime.hours() > 12){
		editPanel.find('#eveEndTime').val(eventEndTime.clone().add('hours',-12).format('HH:mm'));
		$('#eveEndSp').val(1);
	}else{
		editPanel.find('#eveEndTime').val(eventEndTime.format('HH:mm'));
		editPanel.find('#eveEndSp').val(0);
	}
	editPanel.find("#eveEndSp").trigger("chosen:updated");
	
	editPanel.find('#eveStartDate').val(eventStartTime.format('YYYY-MM-DD'));
	editPanel.find('#eveEndDate').val(eventEndTime.format('YYYY-MM-DD'));
	editPanel.find('#evePeople').val(eventPeople);
	
	editPanel.find('#evePostion').val(eventPosition);
	editPanel.find("#evePostion").trigger("chosen:updated");

	element.append(editPanel);

	var hiddenX = jsEvent.pageX - jsEvent.clientX;
	var hiddenY = jsEvent.pageY - jsEvent.clientY;
	var panelX = jsEvent.pageX + 10;
	var panelY = jsEvent.pageY - editPanel.outerHeight()  / 2;

	if(document.body.clientWidth <= jsEvent.clientX + editPanel.outerWidth() + 20){
		panelX = jsEvent.pageX - editPanel.outerWidth() - 10;
	}
	
	
	if(window.innerHeight < jsEvent.clientY + editPanel.outerHeight() / 2){
		panelY = window.innerHeight +  hiddenY - editPanel.outerHeight() - 10;
	}

	if(jsEvent.clientY < editPanel.outerHeight() / 2){
		panelY = 10 + hiddenY;
	}
	
	editPanel.css({'left': panelX , 'top': panelY});
	
	editPanel.find('#eveDelete').click(function(){
		options.remove();
		destroy();
	});
	
	editPanel.find('#eveClose').click(function(){
		options.close();
		destroy();
	});

	element.find(".datepicker").datepicker();
	element.find(".chosen-select").chosen({disable_search_threshold: 6});
	
	function destroy(){
		editPanel.remove();
		element.find('.datepicker-container').remove();
		element.removeData('editPanel');
	}
	
	t.destroy = destroy;
}
})(jQuery);

	
