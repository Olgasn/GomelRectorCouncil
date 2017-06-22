//Работа с данными таблицы Achievements с помощью jqGrid плагина JavaScript библиотеки jQuery
var lastsel;
$("#Year").val($("#currentYear").val());
$(function () {
    $("#jqGrid").jqGrid({
        url: "GridIndicators/GetIndicators?currentYear="+$("#currentYear").val(),
        datatype: 'json',
        mtype: 'Get',
        colNames: ['IndicatorId', 'Код', 'Раздел', 'Подраздел', 'Пункт', 'Показатель', 'Единица измерения', 'Тип показателя', 'Описание'],
        width: '100%',
        colModel: [
            { key: true, hidden: true, name: 'IndicatorId', index: 'IndicatorId', editable: true, search: false },
            { key: false, hidden: true, name: 'IndicatorCode', index: 'IndicatorCode', editable: false, search: true },
            { key: false, name: 'IndicatorId1', index: 'IndicatorId1', sortable: true, width: '12%', editable: true, search: false },
            { key: false, name: 'IndicatorId2', index: 'IndicatorId2', sortable: true, width: '12%', editable: true, search: false },
            { key: false, name: 'IndicatorId3', index: 'IndicatorId3', sortable: true, width: '12%', editable: true, search: false },
            { key: false, name: 'IndicatorName', index: 'IndicatorName', sortable: true, editable: true, search: false },
            { key: false, name: 'IndicatorUnit', index: 'IndicatorUnit', sortable: true, width: '17%', editable: true, search: false },
            { key: false, name: 'IndicatorType', index: 'IndicatorType', formatter: replaceNumber, width: '17%', sortable: true, editable: true, edittype:'select', editoptions:{value:{0:'min',1:'max'}}, search: false },            
            { key: false, name: 'IndicatorDescription', index: 'IndicatorDescription', sortable: true, editable: true, edittype: 'textarea', search: false }],
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
        editurl: "GridIndicators/Edit?Year=" + $("#Year").val(),
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
            url: "GridIndicators/Create?Year=" + $("#Year").val(),
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
            url: "GridIndicators/Delete?Year=" + $("#Year").val(),
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