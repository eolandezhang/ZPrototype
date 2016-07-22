var PMES_USER_ID = '';
$(function () {
    PMES_USER_ID = $('#lbl_description').html();
    Table_Tasks();
    Init_Table_Tasks();
});
function Table_Tasks() {
    $('#Table_Tasks').datagrid({
        title: '任务',
        singleSelect: true,
        width: '966',
        height: 'auto',
        fitColumns: false,
        idField: 'PACKAGE_WF_STEP_ID',
        columns: [[
            {
                field: 'ACTION', title: '', align: 'center', width: 70,
                formatter: function (value, row, index) {
                    var url = '/Package/Preview.aspx?packageNo=' + row.PACKAGE_NO + '&factoryId=' + row.FACTORY_ID + '&versionNo=' + row.VERSION_NO + '&productTypeId=' + row.PRODUCT_TYPE_ID + '&produceProcTypeId=' + row.PRODUCT_PROC_TYPE_ID;
                    return "<a href="+url+" style=\"color:blue;\"><img src=\"/Scripts/jquery-easyui-1.3.4/themes/icons/pencil.png\" />&nbsp;审核</a>";
                }
            },
            { field: 'UPDATE_DATE', title: '任务日期', align: 'center', width: 140 },
            { field: 'PACKAGE_NO', title: '编号', align: 'left', width: 200 },
            { field: 'VERSION_NO', title: '版本', align: 'center', width: 50 },
            { field: 'FACTORY_ID', title: '厂别', align: 'center', width: 60 },
            { field: 'PURPOSE', title: '目的', align: 'left', width: 260 },
            { field: 'WF_SET_STEP_NAME', title: '当前步骤', align: 'center', width: 140 }
        ]]
    });
}
function Init_Table_Tasks() {
    var j = {
        'PMES_USER_ID': PMES_USER_ID        
    };
    $.ajax({
        type: 'get',
        url: '/api/Tasks/GetTasks',
        data: j,
        dataType: 'json',
        success: function (data) {
            $('#Table_Tasks').datagrid("loadData", data);
        }
    });
}