//Работа с данными таблицы Indicators с помощью jqGrid плагина JavaScript библиотеки jQuery
$(function () {
    $("#jqGrid").jqGrid({
        url: "/GridIndicators/GetIndicators",
        datatype: 'json',
        mtype: 'Get',
        colNames: ['IndicatorID', 'IndicatorId1', 'IndicatorId2', 'IndicatorId3', 'IndicatorName', 'IndicatorUnit', 'IndicatorDescription', 'Year' ],

        colModel: [
            { key: true, hidden: true, name: 'IndicatorID', index: 'IndicatorID', editable: true },
            { key: false, name: 'IndicatorId1', index: 'IndicatorId1', sortable: true, editable: true },
            { key: false, name: 'IndicatorId2', index: 'IndicatorId2', sortable: true, editable: true },
            { key: false, name: 'IndicatorId3', index: 'IndicatorId3', sortable: true, editable: true },
            { key: false, name: 'IndicatorName', index: 'IndicatorName', sortable: true, editable: true },
            { key: false, name: 'IndicatorUnit', index: 'IndicatorUnit', sortable: true, editable: true },
            { key: false, name: 'IndicatorDescription', index: 'IndicatorDescription', sortable: true, editable: true },
            { key: false, name: 'Year', index: 'Year', sortable: true, editable: true }],
        pager: jQuery('#jqControls'),
        rowNum: 15,
        rowList: [15, 25, 35, 45],
        sortname: "IndicatorName",
        sortorder: "desc", // порядок сортировки,
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
        search: true,
        searchtext: "Найти",
        refresh: true,
        refreshtext: "Обновить"
    },
        {
            zIndex: 100,
            url: '/GridIndicators/Edit',
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
            zIndex: 100,
            url: "/GridIndicators/Create",
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
            url: "/GridIndicators/Delete",
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
            zIndex: 100,
            caption: "Поиск",
            sopt: ['cn']
        }

        );

});
function unformatNumber(cellvalue, options) {

    return cellvalue.replace(".", ",");
}