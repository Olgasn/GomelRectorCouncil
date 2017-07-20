//Работа с данными таблицы Achievements с помощью jqGrid плагина JavaScript библиотеки jQuery
var lastsel;
$("#Year").val($("#currentYear").val());
$(function () {
    $("#jqGrid").jqGrid({
        url: "GridAchievements/GetAchievements?currentYear=" + $("#currentYear").val(),
        datatype: 'json',
        mtype: 'Get',
        colNames: ['AchievementId', 'Код', 'Показатель', 'Значение'],
        width: '100%',
        colModel: [
            { key: true, hidden: true, name: 'AchievementId', index: 'IndicatorId', editable: true, search: false },
            { key: false, name: 'IndicatorCode', index: 'IndicatorCode', sortable: true, width: '20%', editable: false, search: true },
            { key: false, name: 'IndicatorName', index: 'IndicatorName', editable: false, search: false },
            { key: false, name: 'IndicatorValue', index: 'IndicatorValue', width: '30%', editable: true, search: false }],
        pager: jQuery('#jqControls'),
        rowNum: 15,
        rowList: [15, 25, 35, 45],
        sortname: "IndicatorCode",
        sortorder: "asc", // порядок сортировки,
        height: '100%',
        viewrecords: true,
        onSelectRow: function (id)
        {
            if (id && id !== lastsel)
            {
                jQuery('#jqGrid').jqGrid('restoreRow', lastsel);
                jQuery('#jqGrid').jqGrid('editRow', id, true);
                lastsel = id;
            }
        },
        editurl: "GridAchievements/Edit?Year=" + $("#Year").val(),
        caption: 'Перечень показателей',
        emptyrecords: 'Нет показателей для отображения',
        jsonReader: {
            root: "rows",
            page: "page",
            total: "total",
            records: "records",
            repeatitems: false,
            Id: "0"
        },
        autowidth: true,
        multiselect: false
    }).navGrid('#jqControls',
    {
        edit: true,
        edittext: "Редактировать",
        view: true,
        viewtext: "Смотреть",
        add: true,
        addtext: "Добавить",
        del: true,
        deltext: "Удалить",
        refresh: true,
        refreshtext: "Обновить"
    },
        {
            zIndex: 200,
            url: "GridIndicators/Edit?Year=" + $("#Year").val(),
            closeOnEscape: true,
            closeAfterEdit: true,
            recreateForm: true,
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            }
        },
        {
            zIndex: 200,
            url: "GridAchievements/Create?Year=" + $("#Year").val(),
            closeOnEscape: true,
            closeAfterAdd: true,
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            }
        },
        {
            zIndex: 200,
            url: "GridAchievements/Delete?Year=" + $("#Year").val(),
            closeOnEscape: true,
            closeAfterDelete: true,
            recreateForm: true,
            msg: "Вы уверены, что хотите удалить запись? ",
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            }
        },
        {
            zIndex: 200,
            caption: "Поиск",
            sopt: ['cn']
        }

        );

});
function replaceNumber(cellvalue, options) {

    return (cellvalue == 0) ? 'min' : 'max';


}