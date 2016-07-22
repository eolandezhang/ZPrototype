var pageSize = 20;
var editIndex_PROCESS_PARAM_INFO = undefined;
var addOrEdit_PROCESS_PARAM_INFO = null;
var editIndex_PARAMETER_LIST = undefined;
$(function () {
    //Table_PROCESS_PARAM_INFO_();
    //Init_Table_PROCESS_PARAM_INFO_();

    Table_PROCESS_PARAM_INFO_();
    Dialog_PROCESS_PARAM_INFO();
    InitCurrentFactoryId();
    Table_PARAMETER_LIST_();
});

function Table_PROCESS_PARAM_INFO_() {
    $('#Table_PROCESS_PARAM_INFO').datagrid({
        title: '工序参数',
        singleSelect: true,
        width: '600',
        height: '368',
        fitColumns: false,
        toolbar: [{
            text: '新增',
            iconCls: 'icon-add',
            handler: function () {
                var x = $('#Table_PROCESS_LIST').datagrid('getSelected');
                if (x == null) return;
                addOrEdit_PROCESS_PARAM_INFO = 'add';
                PARAMETER_ID();

                $('#IS_ILLUSTRATION_PARAM').val('0').attr('readonly', false);
                $('#UPDATE_USER').val('');
                $('#UPDATE_DATE').val('');
                $('#IS_SC_PARAM').val('').attr('readonly', false);
                $('#PARAM_ORDER_NO').val('').attr('readonly', false);
                $('#DISP_ORDER_IN_SC').val('').attr('readonly', false);
                $('#UPDATE_USER').attr('readonly', true);
                $('#UPDATE_DATE').attr('readonly', true);
                $('#Dialog_PROCESS_PARAM_INFO').dialog('open');
            }
        }, {
            text: '修改',
            iconCls: 'icon-edit',
            handler: function () {
                addOrEdit_PROCESS_PARAM_INFO = 'edit';
                var x = $('#Table_PROCESS_PARAM_INFO').datagrid('getSelected');
                if (x == null) return;
                //范例：
                //$('#PACKAGE_TYPE_ID').val(x.PACKAGE_TYPE_ID).attr('readonly', true);
                //$('#FACTORY_ID').combobox('setValue',x.FACTORY_ID).combobox('readonly', true);
                //$('#VALID_FLAG').attr('checked', x.VALID_FLAG == 1 ? true : false);               
                PARAMETER_ID();
                $('#PARAMETER_ID').combogrid('setValue', x.PARAMETER_ID).combogrid('readonly', true);
                $('#IS_ILLUSTRATION_PARAM').val(x.IS_ILLUSTRATION_PARAM);
                $('#UPDATE_USER').val(x.UPDATE_USER);
                $('#UPDATE_DATE').val(x.UPDATE_DATE);
                $('#IS_SC_PARAM').val(x.IS_SC_PARAM);
                $('#PARAM_ORDER_NO').val(x.PARAM_ORDER_NO);
                $('#DISP_ORDER_IN_SC').val(x.DISP_ORDER_IN_SC);
                $('#UPDATE_USER').attr('readonly', true);
                $('#UPDATE_DATE').attr('readonly', true);
                $('#Dialog_PROCESS_PARAM_INFO').dialog('open');
            }
        }, {
            text: '删除',
            iconCls: 'icon-cancel',
            handler: function () {
                Deleterow_PROCESS_PARAM_INFO();
            }
        }, {
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                $('#Table_PROCESS_PARAM_INFO').datagrid('endEdit', editIndex_PROCESS_PARAM_INFO);
                var changedRow = $('#Table_PROCESS_PARAM_INFO').datagrid('getChanges');
                if (changedRow.length > 0) {
                    for (var i = 0; i < changedRow.length; i++) {
                        Edit_Cell_PROCESS_PARAM_INFO(changedRow[i]);
                    }
                }
                editIndex_PROCESS_PARAM_INFO = undefined;
                $('#Table_PROCESS_PARAM_INFO').datagrid('clearSelections');
            }
        }, {
            text: '取消',
            iconCls: 'icon-undo',
            handler: function () {
                $('#Table_PROCESS_PARAM_INFO').datagrid('rejectChanges');
            }
        }],
        columns: [[
             {
                 field: 'PARAM_ORDER_NO', title: '序号', align: 'left',
                 editor: {
                     type: 'validatebox',
                     options: {
                         required: false,
                         validType: 'number'
                     }
                 }
             },
             {
                 field: 'PARAMETER_ID', title: '编号', align: 'left', width: 300, formatter: function (value, row, index) {
                     return value + " " + row.PARAM_DESC;
                 },
                 styler: function (value, row, index) { return 'color:blue'; }
             },
            {
                field: 'IS_SC_PARAM', title: '在规格牌中', align: 'left', editor: { type: 'checkbox', options: { on: '1', off: '0' } },
                formatter: function (value, row, index) {
                    switch (value) {
                        case '0':
                            return '';
                        case '1':
                            return '是';
                    }
                }
            },
            {
                field: 'DISP_ORDER_IN_SC', title: '规格牌序号', align: 'left',
                editor: {
                    type: 'validatebox',
                    options: {
                        required: false,
                        validType: 'number'
                    }
                }
            },
            {
                field: 'IS_ILLUSTRATION_PARAM', title: '是图片参数', align: 'left',
                editor: { type: 'checkbox', options: { on: '1', off: '0' } },
                formatter: function (value, row, index) {
                    switch (value) {
                        case '0':
                            return '';
                        case '1':
                            return '是';
                    }
                }
            },
            { field: 'UPDATE_USER', title: '最后更新者', align: 'left' },
            { field: 'UPDATE_DATE', title: '最后更新日期', align: 'left' }
        ]],
        onClickRow: function (rowIndex, rowData) {
            editrow_PROCESS_PARAM_INFO(rowIndex);
            Init_Table_PARAMETER_LIST_();
        },
        onDblClickRow: function (rowIndex, rowData) { },
        rowStyler: function (index, row) { },
        onBeforeEdit: function (index, row) {
            editIndex_PROCESS_PARAM_INFO = index;
            row.editing = true;
            $('#Table_PROCESS_PARAM_INFO').datagrid('refreshRow', index);
        },
        onAfterEdit: function (index, row) {
            row.editing = false;
            $('#Table_PROCESS_PARAM_INFO').datagrid('refreshRow', index);
        },
        onCancelEdit: function (index, row) {
            row.editing = false;
            $('#Table_PROCESS_PARAM_INFO').datagrid('refreshRow', index);
        }
    });
}
function Init_Table_PROCESS_PARAM_INFO_() {
    var x = $('#Table_PROCESS_LIST').datagrid('getSelected');
    if (x == null) return;
    var processId = x.PROCESS_ID;
    var factoryId = $('#FACTORY_ID_SEARCH').combobox('getValue');
    var productTypeId = $('#PRODUCT_TYPE_ID_SEARCH').combobox('getValue');
    var produceProcTypeId = $('#PRODUCT_PROC_TYPE_ID_SEARCH').combobox('getValue');
    var j = {
        'FACTORY_ID': factoryId,
        'PRODUCT_TYPE_ID': productTypeId,
        'PRODUCT_PROC_TYPE_ID': produceProcTypeId,
        'PROCESS_ID': processId
    };
    $.ajax({
        type: 'get',
        url: '/api/PROCESS_PARAM_INFO/GetDataByProcessId',
        data: j,
        dataType: 'json',
        success: function (data) {
            $('#Table_PROCESS_PARAM_INFO').datagrid("loadData", data).datagrid('acceptChanges');
        }
    });
}

function editrow_PROCESS_PARAM_INFO(index) {
    if (editIndex_PROCESS_PARAM_INFO != undefined)
        $('#Table_PROCESS_PARAM_INFO').datagrid('endEdit', editIndex_PROCESS_PARAM_INFO);
    $('#Table_PROCESS_PARAM_INFO').datagrid('beginEdit', index).datagrid('selectRow', index);
}
function Deleterow_PROCESS_PARAM_INFO() {
    var row = $('#Table_PROCESS_PARAM_INFO').datagrid('getSelected');
    if (row == null) {
        $.messager.show({
            title: '消息',
            msg: '请选择',
            showType: 'show'
        });
        return;
    }
    $.messager.confirm('确认', '是否真的删除?', function (r) {
        if (r) {
            Delete_PROCESS_PARAM_INFO(row);
            $('#Table_PROCESS_PARAM_INFO').datagrid('deleteRow', $('#Table_PROCESS_PARAM_INFO').datagrid('getRowIndex'));
        }
    });
}
function Dialog_PROCESS_PARAM_INFO() {
    //选择框，勾选状态无法更改的解决办法：
    //$('#VALID_FLAG').click(
    //    function () {
    //        if ($('#VALID_FLAG').attr('checked') == 'checked') {
    //            $('#VALID_FLAG').attr('checked', false);
    //        } else {
    //            $('#VALID_FLAG').attr('checked', true);
    //        }
    //    });
    $('#Dialog_PROCESS_PARAM_INFO').dialog({
        toolbar: [{
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                if (addOrEdit_PROCESS_PARAM_INFO == 'add') {
                    Add_PROCESS_PARAM_INFO();
                }
                else if (addOrEdit_PROCESS_PARAM_INFO == 'edit') {
                    Edit_PROCESS_PARAM_INFO();
                }
            }
        }]
    });
}
function Validate_PROCESS_PARAM_INFO() {
    if (!(
        $('#PARAM_ORDER_NO').validatebox('isValid') &&
        $('#DISP_ORDER_IN_SC').validatebox('isValid')
        )) {
        $.messager.show({
            title: '消息',
            msg: '输入不正确',
            showType: 'show'
        });
        return false;
    }
    return true;
}
function Add_PROCESS_PARAM_INFO() {
    if (!Validate_PROCESS_PARAM_INFO()) {
        return;
    }
    var x = $('#Table_PROCESS_LIST').datagrid('getSelected');
    if (x == null) return;
    var processId = x.PROCESS_ID;

    var factoryId = $('#FACTORY_ID_SEARCH').combobox('getValue');
    var productTypeId = $('#PRODUCT_TYPE_ID_SEARCH').combobox('getValue');
    var produceProcTypeId = $('#PRODUCT_PROC_TYPE_ID_SEARCH').combobox('getValue');
    var parameterId = $('#PARAMETER_ID').combogrid('getValue');

    if (factoryId == "" ||
       productTypeId == "" ||
       produceProcTypeId == "" ||
        parameterId == ""
       ) {
        return;
    }

    var j = {
        'PROCESS_ID': processId,
        'FACTORY_ID': factoryId,
        'PRODUCT_TYPE_ID': productTypeId,
        'PRODUCT_PROC_TYPE_ID': produceProcTypeId,
        'PARAMETER_ID': parameterId,
        'IS_ILLUSTRATION_PARAM': $('#IS_ILLUSTRATION_PARAM').val(),
        'UPDATE_USER': $('#UPDATE_USER').val(),
        'UPDATE_DATE': $('#UPDATE_DATE').val(),
        'IS_SC_PARAM': $('#IS_SC_PARAM').val(),
        'PARAM_ORDER_NO': $('#PARAM_ORDER_NO').val(),
        'DISP_ORDER_IN_SC': $('#DISP_ORDER_IN_SC').val()
    };
    $.ajax({
        type: 'post',
        url: '/api/PROCESS_PARAM_INFO/PostAdd',
        data: JSON.stringify(j),
        dataType: 'json',
        contentType: 'application/json',
        success: function (data) {
            if (data > 0) {
                $.messager.show({
                    title: '消息',
                    msg: '成功',
                    showType: 'show'
                });
                Init_Table_PROCESS_PARAM_INFO_();
                //$('#Dialog_PROCESS_PARAM_INFO').dialog('close');
            } else {
                $.messager.show({
                    title: '消息',
                    msg: '失败',
                    showType: 'show'
                });
            }
        }
    });
}
function Edit_PROCESS_PARAM_INFO() {
    if (!Validate_PROCESS_PARAM_INFO()) {
        return;
    }
    var x = $('#Table_PROCESS_LIST').datagrid('getSelected');
    if (x == null) return;
    var processId = x.PROCESS_ID;
    var factoryId = $('#FACTORY_ID_SEARCH').combobox('getValue');
    var productTypeId = $('#PRODUCT_TYPE_ID_SEARCH').combobox('getValue');
    var produceProcTypeId = $('#PRODUCT_PROC_TYPE_ID_SEARCH').combobox('getValue');
    var parameterId = $('#PARAMETER_ID').combogrid('getValue');

    if (factoryId == "" ||
       productTypeId == "" ||
        parameterId == "" ||
       produceProcTypeId == ""
       ) {
        return;
    }

    var j = {
        'PROCESS_ID': processId,
        'FACTORY_ID': factoryId,
        'PRODUCT_TYPE_ID': productTypeId,
        'PRODUCT_PROC_TYPE_ID': produceProcTypeId,
        'PARAMETER_ID': parameterId,
        'IS_ILLUSTRATION_PARAM': $('#IS_ILLUSTRATION_PARAM').val(),
        'UPDATE_USER': $('#UPDATE_USER').val(),
        'UPDATE_DATE': $('#UPDATE_DATE').val(),
        'IS_SC_PARAM': $('#IS_SC_PARAM').val(),
        'PARAM_ORDER_NO': $('#PARAM_ORDER_NO').val(),
        'DISP_ORDER_IN_SC': $('#DISP_ORDER_IN_SC').val()
    };
    $.ajax({
        type: 'post',
        url: '/api/PROCESS_PARAM_INFO/PostEdit',
        data: JSON.stringify(j),
        dataType: 'json',
        contentType: 'application/json',
        success: function (data) {
            if (data > 0) {
                $.messager.show({
                    title: '消息',
                    msg: '成功',
                    showType: 'show'
                });
                Init_Table_PROCESS_PARAM_INFO_();
                $('#Dialog_PROCESS_PARAM_INFO').dialog('close');
            } else {
                $.messager.show({
                    title: '消息',
                    msg: '失败',
                    showType: 'show'
                });
            }
        }
    });
}
function Edit_Cell_PROCESS_PARAM_INFO(row) {
    var j = {
        'PROCESS_ID': row.PROCESS_ID,
        'FACTORY_ID': row.FACTORY_ID,
        'PRODUCT_TYPE_ID': row.PRODUCT_TYPE_ID,
        'PRODUCT_PROC_TYPE_ID': row.PRODUCT_PROC_TYPE_ID,
        'PARAMETER_ID': row.PARAMETER_ID,
        'IS_ILLUSTRATION_PARAM': row.IS_ILLUSTRATION_PARAM,
        'UPDATE_USER': row.UPDATE_USER,
        'UPDATE_DATE': row.UPDATE_DATE,
        'IS_SC_PARAM': row.IS_SC_PARAM,
        'PARAM_ORDER_NO': row.PARAM_ORDER_NO,
        'DISP_ORDER_IN_SC': row.DISP_ORDER_IN_SC
    };
    $.ajax({
        type: 'post',
        url: '/api/PROCESS_PARAM_INFO/PostEdit',
        data: JSON.stringify(j),
        dataType: 'json',
        contentType: 'application/json',
        success: function (data) {
            if (data > 0) {
                $.messager.show({
                    title: '消息',
                    msg: '成功',
                    showType: 'show'
                });
            } else {
                $.messager.show({
                    title: '消息',
                    msg: '失败',
                    showType: 'show'
                });
            }
        }
    });
}
function Delete_PROCESS_PARAM_INFO(row) {
    var j = {
        'PROCESS_ID': row.PROCESS_ID,
        'PARAMETER_ID': row.PARAMETER_ID,
        'PRODUCT_TYPE_ID': row.PRODUCT_TYPE_ID,
        'PRODUCT_PROC_TYPE_ID': row.PRODUCT_PROC_TYPE_ID,
        'FACTORY_ID': row.FACTORY_ID
    };
    $.ajax({
        type: 'post',
        url: '/api/PROCESS_PARAM_INFO/PostDelete',
        data: JSON.stringify(j),
        dataType: 'json',
        contentType: 'application/json',
        success: function (data) {
            if (data > 0) {
                $.messager.show({
                    title: '消息',
                    msg: '成功',
                    showType: 'show'
                });
                Init_Table_PROCESS_PARAM_INFO_();
            } else if (data == 0) {
                $.messager.show({
                    title: '消息',
                    msg: '失败',
                    showType: 'show'
                });
            }
        }
    });
}

function InitCurrentFactoryId() {
    Init_FACTORY_ID_SEARCH();
    $('#FACTORY_ID_SEARCH').combobox('setValue', 'SSL-P');
    Init_PRODUCT_TYPE_ID_SEARCH();
    $('#PRODUCT_TYPE_ID_SEARCH').combobox('setValue', 'CE');
    Init_PRODUCT_PROC_TYPE_ID_SEARCH();
    $('#PRODUCT_PROC_TYPE_ID_SEARCH').combobox('setValue', 'M6S');
    Init_Table_PROCESS_LIST();

}

//厂别
function Init_FACTORY_ID_SEARCH() {
    $('#FACTORY_ID_SEARCH').combobox({
        valueField: 'FACTORY_ID',
        textField: 'FACTORY_DESC',
        url: '/api/FACTORY_LIST/GetData',
        method: 'get',
        panelHeight: 100,
        editable: false,
        onSelect: function (record) {
            Init_PRODUCT_TYPE_ID_SEARCH();
            $('#PRODUCT_PROC_TYPE_ID_SEARCH').combobox('clear');
            Table_PROCESS_LIST([]);
        }
    });
}
//产品类型（下拉框）
function Init_PRODUCT_TYPE_ID_SEARCH() {
    var factoryId = $('#FACTORY_ID_SEARCH').combobox('getValue');
    if (factoryId == '') {
        return;
    }
    $('#PRODUCT_TYPE_ID_SEARCH').combobox({
        panelHeight: 100,
        valueField: 'PRODUCT_TYPE_ID',
        textField: 'PRODUCT_TYPE_ID',
        method: 'get',
        url: '/api/PRODUCT_TYPE_LIST/GetDataByFactoryId?FACTORY_ID=' + factoryId,
        editable: false,
        required: true,
        onSelect: function (record) {
            Init_PRODUCT_PROC_TYPE_ID_SEARCH();
        }
    });
}
//产品工艺（下拉框）
function Init_PRODUCT_PROC_TYPE_ID_SEARCH() {
    var factoryId = $('#FACTORY_ID_SEARCH').combobox('getValue');
    var productTypeId = $('#PRODUCT_TYPE_ID_SEARCH').combobox('getValue');
    if (factoryId == '' || productTypeId == '') {
        return;
    }
    $('#PRODUCT_PROC_TYPE_ID_SEARCH').combobox({
        panelHeight: 100,
        valueField: 'PRODUCT_PROC_TYPE_ID',
        textField: 'PRODUCT_PROC_TYPE_ID',
        method: 'get',
        url: '/api/PRODUCT_PROC_TYPE_LIST/GetDataByProductTypeId?FACTORY_ID=' + factoryId + '&PRODUCT_TYPE_ID=' + productTypeId,
        editable: false,
        required: true,
        onSelect: function (record) {
            Init_Table_PROCESS_LIST();
        }
    });
}

//工序
function Table_PROCESS_LIST(data) {
    $('#Table_PROCESS_LIST').datagrid({
        title: '工序',
        singleSelect: true,
        width: '240',
        height: '428',
        fitColumns: true,
        autoRowHeight: false,
        columns: [[
            { field: 'PROCESS_ID', title: '编号', align: 'left', width: 40 },
            { field: 'SEQUENCE_NO', title: '序号', align: 'left', width: 40 },
            { field: 'PROCESS_DESC', title: '工序', align: 'left', width: 100 }
        ]],
        onClickRow: function (rowIndex, rowData) {
            Init_Table_PROCESS_PARAM_INFO_();
            $('#Table_PARAMETER_LIST').datagrid("loadData", []);
        }
    });
    $('#Table_PROCESS_LIST').datagrid("loadData", data);
}
function Init_Table_PROCESS_LIST() {
    var factoryId = $('#FACTORY_ID_SEARCH').combobox('getValue');
    var productTypeId = $('#PRODUCT_TYPE_ID_SEARCH').combobox('getValue');
    var produceProcTypeId = $('#PRODUCT_PROC_TYPE_ID_SEARCH').combobox('getValue');
    $.ajax({
        type: 'get',
        url: '/api/PROCESS_LIST/GetDataByFactoryIdAndTypeId',
        data: {
            'factoryId': factoryId,
            'productTypeId': productTypeId,
            'produceProcTypeId': produceProcTypeId
        },
        dataType: 'json',
        success: function (data) {
            Table_PROCESS_LIST(data);
        }
    });
}

function PARAMETER_ID() {
    var x = $('#Table_PROCESS_LIST').datagrid('getSelected');
    if (x == null) return;
    var processId = x.PROCESS_ID;
    var factoryId = $('#FACTORY_ID_SEARCH').combobox('getValue');
    var productTypeId = $('#PRODUCT_TYPE_ID_SEARCH').combobox('getValue');
    var produceProcTypeId = $('#PRODUCT_PROC_TYPE_ID_SEARCH').combobox('getValue');

    if (factoryId == "" ||
       productTypeId == "" ||
       produceProcTypeId == ""
       ) {
        return;
    }
    $('#PARAMETER_ID').combogrid({
        url: '/api/PARAMETER_LIST/GetDataByType?FACTORY_ID=' + factoryId + '&PRODUCT_TYPE_ID=' + productTypeId + '&PRODUCT_PROC_TYPE_ID=' + produceProcTypeId + '&queryStr=',
        idField: 'PARAMETER_ID',
        textField: 'PARAM_DESC',
        multiple: false,
        method: 'get',
        panelWidth: 400,
        panelHeight: 300,
        columns: [[
        { field: 'PARAMETER_ID', title: '编号', width: 150 },
        { field: 'PARAM_DESC', title: '名称', width: 200 }
        ]],
        onClickRow: function (rowIndex, rowData) {
        }
    });
    //$('#PARAMETER_ID').combobox({
    //    panelHeight: 200,
    //    valueField: 'PARAMETER_ID',
    //    textField: 'PARAM_DESC',
    //    multiple: false,
    //    method: 'get',
    //    url: '/api/PARAMETER_LIST/GetDataByPType?FACTORY_ID=' + factoryId + '&PRODUCT_TYPE_ID=' + productTypeId + '&PRODUCT_PROC_TYPE_ID=' + produceProcTypeId + '&PARAM_TYPE_ID=02-PROCESS&queryStr=',
    //    editable: false,
    //    onSelect: function (record) {
    //    }
    //});
}






//参数
function Table_PARAMETER_LIST_() {
    $('#Table_PARAMETER_LIST').datagrid({
        title: '参数',
        singleSelect: true,
        width: '600',
        height: '130',
        fitColumns: false,
        toolbar: [{
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                $('#Table_PARAMETER_LIST').datagrid('endEdit', editIndex_PARAMETER_LIST);
                var changedRow = $('#Table_PARAMETER_LIST').datagrid('getChanges');
                if (changedRow.length > 0) {
                    for (var i = 0; i < changedRow.length; i++) {
                        Edit_Cell_PARAMETER_LIST(changedRow[i]);
                    }
                }
                editIndex_PARAMETER_LIST = undefined;
                $('#Table_PARAMETER_LIST').datagrid('clearSelections');
            }
        }, {
            text: '取消',
            iconCls: 'icon-undo',
            handler: function () {
                $('#Table_PARAMETER_LIST').datagrid('rejectChanges');
            }
        }],
        columns: [[
            {
                field: 'PARAM_IO', title: '输入/输出', align: 'left', width: 70,
                editor: {
                    type: 'combobox',
                    options: {
                        valueField: 'value',
                        textField: 'label',
                        required: false,
                        data: [{
                            label: '输入',
                            value: '1'
                        }, {
                            label: '输出',
                            value: '2'
                        }]
                    }
                },
                formatter: function (value, row, index) {
                    switch (value) {
                        case '1':
                            return "输入";
                        case '2':
                            return "输出";
                    }
                }
            },
            {
                field: 'SOURCE', title: 'SOURCE', align: 'left', width: 40, editor: { type: 'checkbox', options: { on: '1', off: '0' } },
                formatter: function (value, row, index) {
                    switch (value) {
                        case '0':
                            return "";
                        case '1':
                            return "是";
                    }
                }
            },
            {
                field: 'IS_SPEC_PARAM', title: '规格牌', align: 'left', width: 40, editor: { type: 'checkbox', options: { on: '1', off: '0' } },
                formatter: function (value, row, index) {
                    switch (value) {
                        case '0':
                            return "";
                        case '1':
                            return "是";
                    }
                }
            },
            {
                field: 'IS_FIRST_CHECK_PARAM', title: '首件', align: 'left', width: 30, editor: {
                    type: 'checkbox', options: { on: '1', off: '0' }
                },
                formatter: function (value, row, index) {
                    switch (value) {
                        case '0':
                            return "";
                        case '1':
                            return "是";
                    }
                }
            },
            {
                field: 'IS_PROC_MON_PARAM', title: '过程', align: 'left', width: 30, editor: { type: 'checkbox', options: { on: '1', off: '0' } },
                formatter: function (value, row, index) {
                    switch (value) {
                        case '0':
                            return "";
                        case '1':
                            return "是";
                    }
                }
            },
            {
                field: 'IS_OUTPUT_PARAM', title: '出货', align: 'left', width: 30, editor: { type: 'checkbox', options: { on: '1', off: '0' } },
                formatter: function (value, row, index) {
                    switch (value) {
                        case '0':
                            return "";
                        case '1':
                            return "是";
                    }
                }
            },
            {
                field: 'IS_VERSION_CTRL', title: '版本控制', align: 'left', width: 60, editor: { type: 'checkbox', options: { on: '1', off: '0' } },
                formatter: function (value, row, index) {
                    switch (value) {
                        case '0':
                            return "";
                        case '1':
                            return "是";
                    }
                }
            },
            {
                field: 'MEASURE_METHOD', title: '测量方法', align: 'left', width: 60, editor: { type: 'checkbox', options: { on: '1', off: '0' } },
                formatter: function (value, row, index) {
                    switch (value) {
                        case '0':
                            return "";
                        case '1':
                            return "是";
                    }
                }
            },
            {
                field: 'IS_GROUP_PARAM', title: '分组参数', align: 'left', width: 60, editor: { type: 'checkbox', options: { on: '1', off: '0' } },
                formatter: function (value, row, index) {
                    switch (value) {
                        case '0':
                            return "";
                        case '1':
                            return "是";
                    }
                }
            },
            {
                field: 'PARAM_DATATYPE', title: '数据类型', align: 'left', width: 100, editor: {
                    type: 'combobox',
                    options: {
                        valueField: 'value',
                        textField: 'label',
                        required: false,
                        data: [{
                            label: '字符串',
                            value: 'STRING'
                        }, {
                            label: '数值',
                            value: 'NUMBER'
                        }]
                    }
                },
                formatter: function (value, row, index) {
                    switch (value) {
                        case 'STRING':
                            return "字符串";
                        case 'NUMBER':
                            return "数值";
                    }
                }
            },
            { field: 'PARAM_UNIT', title: '单位', align: 'left', width: 40, editor: 'text' },
            { field: 'TARGET', title: '目标值', align: 'left', width: 100, editor: 'text' },
            { field: 'USL', title: '上限', align: 'left', width: 100, editor: 'text' },
            { field: 'LSL', title: '下限', align: 'left', width: 100, editor: 'text' },

            { field: 'SAMPLING_FREQUENCY', title: '抽样频率', align: 'left', width: 100, editor: 'text' },
            { field: 'CONTROL_METHOD', title: '控制方法', align: 'left', width: 100, editor: 'text' },
            { field: 'UPDATE_USER', title: '最后更信者', align: 'left', width: 100 },
            { field: 'UPDATE_DATE', title: '最后更新日期', align: 'left', width: 100 },
            { field: 'PARAM_NAME', title: '英文名', align: 'left', width: 100, editor: 'text' }
        ]],
        onClickRow: function (rowIndex, rowData) {
            editrow_PARAMETER_LIST(rowIndex);
        },
        onDblClickRow: function (rowIndex, rowData) { },
        rowStyler: function (index, row) { },
        onBeforeEdit: function (index, row) {
            editIndex_PARAMETER_LIST = index;
            row.editing = true;
            $('#Table_PARAMETER_LIST').datagrid('refreshRow', index);
        },
        onAfterEdit: function (index, row) {
            row.editing = false;
            $('#Table_PARAMETER_LIST').datagrid('refreshRow', index);
        },
        onCancelEdit: function (index, row) {
            row.editing = false;
            $('#Table_PARAMETER_LIST').datagrid('refreshRow', index);
        }
    });
}
function Init_Table_PARAMETER_LIST_() {
    var x = $('#Table_PROCESS_PARAM_INFO').datagrid('getSelected');
    if (x == null) return;
    var parameterId = x.PARAMETER_ID;
    var factoryId = $('#FACTORY_ID_SEARCH').combobox('getValue');
    var productTypeId = $('#PRODUCT_TYPE_ID_SEARCH').combobox('getValue');
    var produceProcTypeId = $('#PRODUCT_PROC_TYPE_ID_SEARCH').combobox('getValue');
    var j = {
        'FACTORY_ID': factoryId,
        'PRODUCT_TYPE_ID': productTypeId,
        'PRODUCT_PROC_TYPE_ID': produceProcTypeId,
        'PARAMETER_ID': parameterId
    };
    $.ajax({
        type: 'get',
        url: '/api/PARAMETER_LIST/GetDataById',
        data: j,
        dataType: 'json',
        success: function (data) {
            $('#Table_PARAMETER_LIST').datagrid("loadData", data).datagrid('acceptChanges');
        }
    });
}
function editrow_PARAMETER_LIST(index) {
    if (editIndex_PARAMETER_LIST != undefined)
        $('#Table_PARAMETER_LIST').datagrid('endEdit', editIndex_PARAMETER_LIST);
    $('#Table_PARAMETER_LIST').datagrid('beginEdit', index).datagrid('selectRow', index);
}
function Edit_Cell_PARAMETER_LIST(row) {
    var j = {
        'PARAMETER_ID': row.PARAMETER_ID,
        'FACTORY_ID': row.FACTORY_ID,
        'PRODUCT_TYPE_ID': row.PRODUCT_TYPE_ID,
        'PRODUCT_PROC_TYPE_ID': row.PRODUCT_PROC_TYPE_ID,
        'UPDATE_USER': row.UPDATE_USER,
        'UPDATE_DATE': row.UPDATE_DATE,
        'PARAM_NAME': row.PARAM_NAME,
        'PARAM_DESC': row.PARAM_DESC,
        'PARAM_TYPE_ID': row.PARAM_TYPE_ID,
        'PARAM_IO': row.PARAM_IO,
        'SOURCE': row.SOURCE,
        'IS_SPEC_PARAM': row.IS_SPEC_PARAM,
        'IS_FIRST_CHECK_PARAM': row.IS_FIRST_CHECK_PARAM,
        'IS_PROC_MON_PARAM': row.IS_PROC_MON_PARAM,
        'IS_OUTPUT_PARAM': row.IS_OUTPUT_PARAM,
        'IS_VERSION_CTRL': row.IS_VERSION_CTRL,
        'MEASURE_METHOD': row.MEASURE_METHOD,
        'IS_GROUP_PARAM': row.IS_GROUP_PARAM,
        'PARAM_DATATYPE': row.PARAM_DATATYPE,
        'PARAM_UNIT': row.PARAM_UNIT,
        'TARGET': row.TARGET,
        'USL': row.USL,
        'LSL': row.LSL,
        'VALID_FLAG': row.VALID_FLAG,
        'SAMPLING_FREQUENCY': row.SAMPLING_FREQUENCY,
        'CONTROL_METHOD': row.CONTROL_METHOD
    };
    $.ajax({
        type: 'post',
        url: '/api/PARAMETER_LIST/PostEdit',
        data: JSON.stringify(j),
        dataType: 'json',
        contentType: 'application/json',
        success: function (data) {
            if (data > 0) {
                $.messager.show({
                    title: '消息',
                    msg: '成功',
                    showType: 'show'
                });
            } else {
                $.messager.show({
                    title: '消息',
                    msg: '失败',
                    showType: 'show'
                });
            }
        }
    });
}