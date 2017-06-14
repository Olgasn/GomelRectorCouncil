//Работа с данными таблицы Indicators с помощью jqGrid плагина JavaScript библиотеки jQuery
$("#Year").val($("#currentYear").val());
$(function () {
    $("#jqGrid").jqGrid({
        url: "GridIndicators/GetIndicators?currentYear="+$("#currentYear").val(),
        datatype: 'json',
        mtype: 'Get',
        colNames: ['IndicatorID', 'Код', 'Раздел', 'Подраздел', 'Пункт', 'Показатель', 'Единица измерения', 'Тип показателя', 'Описание'],

        colModel: [
            { key: true, hidden: true, name: 'IndicatorID', index: 'IndicatorID', editable: true, search: false },
            { key: false, hidden: true, name: 'IndicatorCode', index: 'IndicatorCode', editable: false, search: true },
            { key: false, name: 'IndicatorId1', index: 'IndicatorId1', sortable: true, width: 40, editable: true, search: false },
            { key: false, name: 'IndicatorId2', index: 'IndicatorId2', sortable: true, width: 40, editable: true, search: false },
            { key: false, name: 'IndicatorId3', index: 'IndicatorId3', sortable: true, width: 40, editable: true, search: false },
            { key: false, name: 'IndicatorName', index: 'IndicatorName', sortable: true, editable: true, search: false },
            { key: false, name: 'IndicatorUnit', index: 'IndicatorUnit', sortable: true, editable: true, search: false },
            { key: false, name: 'IndicatorType', index: 'IndicatorType', sortable: true, editable: true, search: false },            
            { key: false, name: 'IndicatorDescription', index: 'IndicatorDescription', sortable: true, editable: true, search: false }],
        pager: jQuery('#jqControls'),
        rowNum: 15,
        rowList: [15, 25, 35, 45],
        sortname: "IndicatorCode",
        sortorder: "asc", // порядок сортировки,
        height: '100%',
        viewrecords: true,
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
            url: "GridIndicators/Edit?currentYear="+$("#currentYear").val(),
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
            zIndex: 100,
            url: "GridIndicators/Delete?currentYear = "+$("#currentYear").val(),
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
function unformatNumber(cellvalue, options) {

    return cellvalue.replace(".", ",");
}