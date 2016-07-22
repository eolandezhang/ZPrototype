var pageSize = 20;
var editIndex_PARAMETER_LIST = undefined;
var addOrEdit_PARAMETER_LIST = null;
var editIndex_PARAM_TYPE_LIST = undefined;
var addOrEdit_PARAM_TYPE_LIST = null;
$(function () {
    Table_PARAMETER_LIST_();
    $('#Table_PARAMETER_LIST').datagrid("loadData", []);
    Dialog_PARAMETER_LIST();
    Table_PARAM_TYPE_LIST_();
    InitCurrentFactoryId();
    Dialog_PARAM_TYPE_LIST();
});
//参数
function Table_PARAMETER_LIST_() {
    $('#Table_PARAMETER_LIST').datagrid({
        title: '参数',
        singleSelect: true,
        width: '840',
        height: '500',
        fitColumns: false,
        toolbar: [{
            text: '新增',
            iconCls: 'icon-add',
            handler: function () {
                //范例：
                //$('#PACKAGE_TYPE_ID').val('').attr('readonly', false);
                //$('#FACTORY_ID').combobox('clear').combobox('readonly', false);
                //$('#VALID_FLAG').attr('checked', true);
                addOrEdit_PARAMETER_LIST = 'add';
                $('#PARAMETER_ID').val('').attr('readonly', false);
                $('#UPDATE_USER').val('').attr('readonly', false);
                $('#UPDATE_DATE').val('').attr('readonly', false);
                $('#PARAM_NAME').val('').attr('readonly', false);
                $('#PARAM_DESC').val('').attr('readonly', false);
                $('#PARAM_IO').val('1').attr('readonly', false);
                $('#SOURCE').val('0').attr('readonly', false);
                $('#IS_SPEC_PARAM').val('0').attr('readonly', false);
                $('#IS_FIRST_CHECK_PARAM').val('0').attr('readonly', false);
                $('#IS_PROC_MON_PARAM').val('0').attr('readonly', false);
                $('#IS_OUTPUT_PARAM').val('0').attr('readonly', false);
                $('#IS_VERSION_CTRL').val('0').attr('readonly', false);
                $('#MEASURE_METHOD').val('0').attr('readonly', false);
                $('#IS_GROUP_PARAM').val('0').attr('readonly', false);
                $('#PARAM_DATATYPE').val('STRING').attr('readonly', false);
                $('#PARAM_UNIT').val('').attr('readonly', false);
                $('#TARGET').val('').attr('readonly', false);
                $('#USL').val('').attr('readonly', false);
                $('#LSL').val('').attr('readonly', false);
                $('#VALID_FLAG').val('1').attr('readonly', false);
                $('#SAMPLING_FREQUENCY').val('').attr('readonly', false);
                $('#CONTROL_METHOD').val('').attr('readonly', false);                
                $('#UPDATE_USER').attr('readonly', true);
                $('#UPDATE_DATE').attr('readonly', true);
                $('#Dialog_PARAMETER_LIST').dialog('open');
            }
        }, {
            text: '修改',
            iconCls: 'icon-edit',
            handler: function () {
                addOrEdit_PARAMETER_LIST = 'edit';
                var x = $('#Table_PARAMETER_LIST').datagrid('getSelected');
                if (x == null) return;
                //范例：
                //$('#PACKAGE_TYPE_ID').val(x.PACKAGE_TYPE_ID).attr('readonly', true);
                //$('#FACTORY_ID').combobox('setValue',x.FACTORY_ID).combobox('readonly', true);
                //$('#VALID_FLAG').attr('checked', x.VALID_FLAG == 1 ? true : false);               
                $('#PARAMETER_ID').val(x.PARAMETER_ID).attr('readonly', true);
                $('#UPDATE_USER').val(x.UPDATE_USER);
                $('#UPDATE_DATE').val(x.UPDATE_DATE);
                $('#PARAM_NAME').val(x.PARAM_NAME);
                $('#PARAM_DESC').val(x.PARAM_DESC);
                $('#PARAM_TYPE_ID').val(x.PARAM_TYPE_ID);
                $('#PARAM_IO').val(x.PARAM_IO);
                $('#SOURCE').val(x.SOURCE);
                $('#IS_SPEC_PARAM').val(x.IS_SPEC_PARAM);
                $('#IS_FIRST_CHECK_PARAM').val(x.IS_FIRST_CHECK_PARAM);
                $('#IS_PROC_MON_PARAM').val(x.IS_PROC_MON_PARAM);
                $('#IS_OUTPUT_PARAM').val(x.IS_OUTPUT_PARAM);
                $('#IS_VERSION_CTRL').val(x.IS_VERSION_CTRL);
                $('#MEASURE_METHOD').val(x.MEASURE_METHOD);
                $('#IS_GROUP_PARAM').val(x.IS_GROUP_PARAM);
                $('#PARAM_DATATYPE').val(x.PARAM_DATATYPE);
                $('#PARAM_UNIT').val(x.PARAM_UNIT);
                $('#TARGET').val(x.TARGET);
                $('#USL').val(x.USL);
                $('#LSL').val(x.LSL);
                $('#VALID_FLAG').val(x.VALID_FLAG);
                $('#SAMPLING_FREQUENCY').val(x.SAMPLING_FREQUENCY);
                $('#CONTROL_METHOD').val(x.CONTROL_METHOD);                
                $('#UPDATE_USER').attr('readonly', true);
                $('#UPDATE_DATE').attr('readonly', true);
                $('#Dialog_PARAMETER_LIST').dialog('open');
            }
        }, {
            text: '删除',
            iconCls: 'icon-cancel',
            handler: function () {
                Deleterow_PARAMETER_LIST();
            }
        }, {
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
        frozenColumns: [[            
            { field: 'PARAMETER_ID', title: '编号', align: 'left', width: 180, styler: function (value, row, index) { return 'color:blue'; } }
        ]],
        columns: [[
            {
                field: 'VALID_FLAG', title: '停用', align: 'left', width: 30, editor: { type: 'checkbox', options: { on: '0', off: '1' } },
                formatter: function (value, row, index) {
                    switch (value) {
                        case '0':
                            return "停用";
                        case '1':
                            return "";
                    }
                }
            },
            { field: 'PARAM_DESC', title: '中文名', align: 'left', width: 250, editor: 'text' },            
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
    var x = $('#Table_PARAM_TYPE_LIST').datagrid('getSelected');
    if (x == null) return;
    var paramTypeId = x.PARAM_TYPE_ID;
    var factoryId = $('#FACTORY_ID_SEARCH').combobox('getValue');
    var productTypeId = $('#PRODUCT_TYPE_ID_SEARCH').combobox('getValue');
    var produceProcTypeId = $('#PRODUCT_PROC_TYPE_ID_SEARCH').combobox('getValue');
    var j = {
        'FACTORY_ID': factoryId,
        'PRODUCT_TYPE_ID': productTypeId,
        'PRODUCT_PROC_TYPE_ID': produceProcTypeId,
        'PARAM_TYPE_ID': paramTypeId,
        'queryStr': ''
    };
    $.ajax({
        type: 'get',
        url: '/api/PARAMETER_LIST/GetDataByPType',
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
function Deleterow_PARAMETER_LIST() {
    var row = $('#Table_PARAMETER_LIST').datagrid('getSelected');
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
            Delete_PARAMETER_LIST(row);
            $('#Table_PARAMETER_LIST').datagrid('deleteRow', $('#Table_PARAMETER_LIST').datagrid('getRowIndex'));
        }
    });
}
function Dialog_PARAMETER_LIST() {
    //选择框，勾选状态无法更改的解决办法：
    //$('#VALID_FLAG').click(
    //    function () {
    //        if ($('#VALID_FLAG').attr('checked') == 'checked') {
    //            $('#VALID_FLAG').attr('checked', false);
    //        } else {
    //            $('#VALID_FLAG').attr('checked', true);
    //        }
    //    });
    $('#Dialog_PARAMETER_LIST').dialog({
        toolbar: [{
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                if (addOrEdit_PARAMETER_LIST == 'add') {
                    Add_PARAMETER_LIST();
                }
                else if (addOrEdit_PARAMETER_LIST == 'edit') {
                    Edit_PARAMETER_LIST();
                }
            }
        }]
    });
}
function Add_PARAMETER_LIST() {
    var x = $('#Table_PARAM_TYPE_LIST').datagrid('getSelected');
    if (x == null) return;
    var paramTypeId = x.PARAM_TYPE_ID;
    var factoryId = $('#FACTORY_ID_SEARCH').combobox('getValue');
    var productTypeId = $('#PRODUCT_TYPE_ID_SEARCH').combobox('getValue');
    var produceProcTypeId = $('#PRODUCT_PROC_TYPE_ID_SEARCH').combobox('getValue');

    var j = {
        'PARAMETER_ID': $('#PARAMETER_ID').val(),
        'FACTORY_ID': factoryId,
        'PRODUCT_TYPE_ID': productTypeId,
        'PRODUCT_PROC_TYPE_ID': produceProcTypeId,
        'UPDATE_USER': $('#UPDATE_USER').val(),
        'UPDATE_DATE': $('#UPDATE_DATE').val(),
        'PARAM_NAME': $('#PARAM_NAME').val(),
        'PARAM_DESC': $('#PARAM_DESC').val(),
        'PARAM_TYPE_ID': paramTypeId,
        'PARAM_IO': $('#PARAM_IO').val(),
        'SOURCE': $('#SOURCE').val(),
        'IS_SPEC_PARAM': $('#IS_SPEC_PARAM').val(),
        'IS_FIRST_CHECK_PARAM': $('#IS_FIRST_CHECK_PARAM').val(),
        'IS_PROC_MON_PARAM': $('#IS_PROC_MON_PARAM').val(),
        'IS_OUTPUT_PARAM': $('#IS_OUTPUT_PARAM').val(),
        'IS_VERSION_CTRL': $('#IS_VERSION_CTRL').val(),
        'MEASURE_METHOD': $('#MEASURE_METHOD').val(),
        'IS_GROUP_PARAM': $('#IS_GROUP_PARAM').val(),
        'PARAM_DATATYPE': $('#PARAM_DATATYPE').val(),
        'PARAM_UNIT': $('#PARAM_UNIT').val(),
        'TARGET': $('#TARGET').val(),
        'USL': $('#USL').val(),
        'LSL': $('#LSL').val(),
        'VALID_FLAG': $('#VALID_FLAG').val(),
        'SAMPLING_FREQUENCY': $('#SAMPLING_FREQUENCY').val(),
        'CONTROL_METHOD': $('#CONTROL_METHOD').val()
    };
    $.ajax({
        type: 'post',
        url: '/api/PARAMETER_LIST/PostAdd',
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
                Init_Table_PARAMETER_LIST_();
                $('#Dialog_PARAMETER_LIST').dialog('close');
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
function Edit_PARAMETER_LIST() {
    var x = $('#Table_PARAM_TYPE_LIST').datagrid('getSelected');
    if (x == null) return;
    var y = $('#Table_PARAMETER_LIST').datagrid('getSelected');
    if (y == null) return;
    var paramTypeId = x.PARAM_TYPE_ID;
    var factoryId = $('#FACTORY_ID_SEARCH').combobox('getValue');
    var productTypeId = $('#PRODUCT_TYPE_ID_SEARCH').combobox('getValue');
    var produceProcTypeId = $('#PRODUCT_PROC_TYPE_ID_SEARCH').combobox('getValue');

    //范例：
    //'FACTORY_ID':$('#FACTORY_ID').combobox('getValue'),  
    //'VALID_FLAG': $('#VALID_FLAG').attr('checked') == 'checked' ? 1 : 0,
    var j = {
        'PARAMETER_ID': y.PARAMETER_ID,
        'FACTORY_ID': factoryId,
        'PRODUCT_TYPE_ID': productTypeId,
        'PRODUCT_PROC_TYPE_ID': produceProcTypeId,
        'UPDATE_USER': $('#UPDATE_USER').val(),
        'UPDATE_DATE': $('#UPDATE_DATE').val(),
        'PARAM_NAME': $('#PARAM_NAME').val(),
        'PARAM_DESC': $('#PARAM_DESC').val(),
        'PARAM_TYPE_ID': paramTypeId,
        'PARAM_IO': $('#PARAM_IO').val(),
        'SOURCE': $('#SOURCE').val(),
        'IS_SPEC_PARAM': $('#IS_SPEC_PARAM').val(),
        'IS_FIRST_CHECK_PARAM': $('#IS_FIRST_CHECK_PARAM').val(),
        'IS_PROC_MON_PARAM': $('#IS_PROC_MON_PARAM').val(),
        'IS_OUTPUT_PARAM': $('#IS_OUTPUT_PARAM').val(),
        'IS_VERSION_CTRL': $('#IS_VERSION_CTRL').val(),
        'MEASURE_METHOD': $('#MEASURE_METHOD').val(),
        'IS_GROUP_PARAM': $('#IS_GROUP_PARAM').val(),
        'PARAM_DATATYPE': $('#PARAM_DATATYPE').val(),
        'PARAM_UNIT': $('#PARAM_UNIT').val(),
        'TARGET': $('#TARGET').val(),
        'USL': $('#USL').val(),
        'LSL': $('#LSL').val(),
        'VALID_FLAG': $('#VALID_FLAG').val(),
        'SAMPLING_FREQUENCY': $('#SAMPLING_FREQUENCY').val(),
        'CONTROL_METHOD': $('#CONTROL_METHOD').val()
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
                Init_Table_PARAMETER_LIST_();
                $('#Dialog_PARAMETER_LIST').dialog('close');
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
function Delete_PARAMETER_LIST(row) {
    var j = {
        'PARAMETER_ID': row.PARAMETER_ID,
        'FACTORY_ID': row.FACTORY_ID,
        'PRODUCT_TYPE_ID': row.PRODUCT_TYPE_ID,
        'PRODUCT_PROC_TYPE_ID': row.PRODUCT_PROC_TYPE_ID
    };
    $.ajax({
        type: 'post',
        url: '/api/PARAMETER_LIST/PostDelete',
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
                Init_Table_PARAMETER_LIST_();
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

//参数类别
function Table_PARAM_TYPE_LIST_() {
    $('#Table_PARAM_TYPE_LIST').datagrid({
        title: '参数类别',
        singleSelect: true,
        width: '240',
        height: '428',
        fitColumns: false,
        toolbar: [{
            text: '新增',
            handler: function () {
                //范例：
                //$('#PACKAGE_TYPE_ID').val('').attr('readonly', false);
                //$('#FACTORY_ID').combobox('clear').combobox('readonly', false);
                //$('#VALID_FLAG').attr('checked', true);
                addOrEdit_PARAM_TYPE_LIST = 'add';
                $('#PARAM_TYPE_ID').val('').attr('readonly', false);
                $('#UPDATE_USER_PARAM_TYPE_LIST').val('').attr('readonly', false);
                $('#UPDATE_DATE_PARAM_TYPE_LIST').val('').attr('readonly', false);
                $('#PARAM_TYPE_DESC').val('').attr('readonly', false);
                $('#VALID_FLAG_FLAG_PARAM_TYPE_LIST').val('').attr('readonly', false);
                $('#UPDATE_USER_PARAM_TYPE_LIST').attr('readonly', true);
                $('#UPDATE_DATE_PARAM_TYPE_LIST').attr('readonly', true);
                $('#Dialog_PARAM_TYPE_LIST').dialog('open');
            }
        }, {
            text: '修改',
            handler: function () {
                addOrEdit_PARAM_TYPE_LIST = 'edit';
                var x = $('#Table_PARAM_TYPE_LIST').datagrid('getSelected');
                if (x == null) return;
                $('#PARAM_TYPE_ID').val(x.PARAM_TYPE_ID).attr('readonly', true);
                $('#UPDATE_USER_PARAM_TYPE_LIST').val(x.UPDATE_USER);
                $('#UPDATE_DATE_PARAM_TYPE_LIST').val(x.UPDATE_DATE);
                $('#PARAM_TYPE_DESC').val(x.PARAM_TYPE_DESC);
                $('#VALID_FLAG_FLAG_PARAM_TYPE_LIST').val(x.VALID_FLAG);
                $('#UPDATE_USER_PARAM_TYPE_LIST').attr('readonly', true);
                $('#UPDATE_DATE_PARAM_TYPE_LIST').attr('readonly', true);
                $('#Dialog_PARAM_TYPE_LIST').dialog('open');
            }
        }, {
            text: '删除',
            handler: function () {
                Deleterow_PARAM_TYPE_LIST();
            }
        }, '-', {
            text: '保存',
            handler: function () {
                $('#Table_PARAM_TYPE_LIST').datagrid('endEdit', editIndex_PARAM_TYPE_LIST);
                var changedRow = $('#Table_PARAM_TYPE_LIST').datagrid('getChanges');
                if (changedRow.length > 0) {
                    for (var i = 0; i < changedRow.length; i++) {
                        Edit_Cell_PARAM_TYPE_LIST(changedRow[i]);
                    }
                }
                editIndex_PARAM_TYPE_LIST = undefined;
                $('#Table_PARAM_TYPE_LIST').datagrid('clearSelections');
            }
        }, {
            text: '取消',
            handler: function () {
                $('#Table_PARAM_TYPE_LIST').datagrid('rejectChanges');
            }
        }],
        frozenColumns: [[
            { field: 'PARAM_TYPE_ID', title: '编号', align: 'left', width: 100, styler: function (value, row, index) { return 'color:blue'; } }
        ]],
        columns: [[

            { field: 'PARAM_TYPE_DESC', title: '名称', align: 'left', width: 100, editor: 'text' },
            { field: 'VALID_FLAG', title: '停用/启用', align: 'left', editor: { type: 'checkbox', options: { on: '1', off: '0' } } },
            { field: 'UPDATE_USER', title: '最后更新者', align: 'left', width: 100 },
            { field: 'UPDATE_DATE', title: '最后更新日期', align: 'left', width: 100 }
        ]],
        onClickRow: function (rowIndex, rowData) {
            editrow_PARAM_TYPE_LIST(rowIndex);
            Init_Table_PARAMETER_LIST_();
        },
        onDblClickRow: function (rowIndex, rowData) { },
        rowStyler: function (index, row) { },
        onBeforeEdit: function (index, row) {
            editIndex_PARAM_TYPE_LIST = index;
            row.editing = true;
            $('#Table_PARAM_TYPE_LIST').datagrid('refreshRow', index);
        },
        onAfterEdit: function (index, row) {
            row.editing = false;
            $('#Table_PARAM_TYPE_LIST').datagrid('refreshRow', index);
        },
        onCancelEdit: function (index, row) {
            row.editing = false;
            $('#Table_PARAM_TYPE_LIST').datagrid('refreshRow', index);
        }
    });
}
function Init_Table_PARAM_TYPE_LIST_() {
    var factoryId = $('#FACTORY_ID_SEARCH').combobox('getValue');
    var productTypeId = $('#PRODUCT_TYPE_ID_SEARCH').combobox('getValue');
    var produceProcTypeId = $('#PRODUCT_PROC_TYPE_ID_SEARCH').combobox('getValue');
    var j = {
        'FACTORY_ID': factoryId,
        'PRODUCT_TYPE_ID': productTypeId,
        'PRODUCT_PROC_TYPE_ID': produceProcTypeId,
        'queryStr': ''
    };
    $.ajax({
        type: 'get',
        url: '/api/PARAM_TYPE_LIST/GetDataType',
        data: j,
        dataType: 'json',
        success: function (data) {
            $('#Table_PARAM_TYPE_LIST').datagrid("loadData", data).datagrid('acceptChanges');
        }
    });
}
function editrow_PARAM_TYPE_LIST(index) {
    if (editIndex_PARAM_TYPE_LIST != undefined)
        $('#Table_PARAM_TYPE_LIST').datagrid('endEdit', editIndex_PARAM_TYPE_LIST);
    $('#Table_PARAM_TYPE_LIST').datagrid('beginEdit', index).datagrid('selectRow', index);
}
function Deleterow_PARAM_TYPE_LIST() {
    var row = $('#Table_PARAM_TYPE_LIST').datagrid('getSelected');
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
            Delete_PARAM_TYPE_LIST(row);
            $('#Table_PARAM_TYPE_LIST').datagrid('deleteRow', $('#Table_PARAM_TYPE_LIST').datagrid('getRowIndex'));
        }
    });
}
function Dialog_PARAM_TYPE_LIST() {
    //选择框，勾选状态无法更改的解决办法：
    //$('#VALID_FLAG').click(
    //    function () {
    //        if ($('#VALID_FLAG').attr('checked') == 'checked') {
    //            $('#VALID_FLAG').attr('checked', false);
    //        } else {
    //            $('#VALID_FLAG').attr('checked', true);
    //        }
    //    });
    $('#Dialog_PARAM_TYPE_LIST').dialog({
        toolbar: [{
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                if (addOrEdit_PARAM_TYPE_LIST == 'add') {
                    Add_PARAM_TYPE_LIST();
                }
                else if (addOrEdit_PARAM_TYPE_LIST == 'edit') {
                    Edit_PARAM_TYPE_LIST();
                }
            }
        }]
    });
}
function Add_PARAM_TYPE_LIST() {
    var factoryId = $('#FACTORY_ID_SEARCH').combobox('getValue');
    var productTypeId = $('#PRODUCT_TYPE_ID_SEARCH').combobox('getValue');
    var produceProcTypeId = $('#PRODUCT_PROC_TYPE_ID_SEARCH').combobox('getValue');
    var j = {
        'PARAM_TYPE_ID': $('#PARAM_TYPE_ID').val(),
        'FACTORY_ID': factoryId,
        'PRODUCT_TYPE_ID': productTypeId,
        'PRODUCT_PROC_TYPE_ID': produceProcTypeId,
        'UPDATE_USER': $('#UPDATE_USER_FLAG_PARAM_TYPE_LIST').val(),
        'UPDATE_DATE': $('#UPDATE_DATE_FLAG_PARAM_TYPE_LIST').val(),
        'PARAM_TYPE_DESC': $('#PARAM_TYPE_DESC').val(),
        'VALID_FLAG': $('#VALID_FLAG_FLAG_PARAM_TYPE_LIST').val()
    };
    $.ajax({
        type: 'post',
        url: '/api/PARAM_TYPE_LIST/PostAdd',
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
                Init_Table_PARAM_TYPE_LIST_();
                $('#Dialog_PARAM_TYPE_LIST').dialog('close');
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
function Edit_PARAM_TYPE_LIST() {
    var x = $('#Table_PARAM_TYPE_LIST').datagrid('getSelected');
    if (x == null) return;
    var paramTypeId = x.PARAM_TYPE_ID;
    var factoryId = $('#FACTORY_ID_SEARCH').combobox('getValue');
    var productTypeId = $('#PRODUCT_TYPE_ID_SEARCH').combobox('getValue');
    var produceProcTypeId = $('#PRODUCT_PROC_TYPE_ID_SEARCH').combobox('getValue');
    var j = {
        'PARAM_TYPE_ID': paramTypeId,
        'FACTORY_ID': factoryId,
        'PRODUCT_TYPE_ID': productTypeId,
        'PRODUCT_PROC_TYPE_ID': produceProcTypeId,
        'UPDATE_USER': $('#UPDATE_USER_FLAG_PARAM_TYPE_LIST').val(),
        'UPDATE_DATE': $('#UPDATE_DATE_FLAG_PARAM_TYPE_LIST').val(),
        'PARAM_TYPE_DESC': $('#PARAM_TYPE_DESC').val(),
        'VALID_FLAG': $('#VALID_FLAG_FLAG_PARAM_TYPE_LIST').val()
    };
    $.ajax({
        type: 'post',
        url: '/api/PARAM_TYPE_LIST/PostEdit',
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
                Init_Table_PARAM_TYPE_LIST_();
                $('#Dialog_PARAM_TYPE_LIST').dialog('close');
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
function Edit_Cell_PARAM_TYPE_LIST(row) {
    var j = {
        'PARAM_TYPE_ID': row.PARAM_TYPE_ID,
        'FACTORY_ID': row.FACTORY_ID,
        'PRODUCT_TYPE_ID': row.PRODUCT_TYPE_ID,
        'PRODUCT_PROC_TYPE_ID': row.PRODUCT_PROC_TYPE_ID,
        'UPDATE_USER': row.UPDATE_USER,
        'UPDATE_DATE': row.UPDATE_DATE,
        'PARAM_TYPE_DESC': row.PARAM_TYPE_DESC,
        'VALID_FLAG': row.VALID_FLAG
    };
    $.ajax({
        type: 'post',
        url: '/api/PARAM_TYPE_LIST/PostEdit',
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
function Delete_PARAM_TYPE_LIST(row) {
    var j = {
        'PARAM_TYPE_ID': row.PARAM_TYPE_ID,
        'FACTORY_ID': row.FACTORY_ID,
        'PRODUCT_TYPE_ID': row.PRODUCT_TYPE_ID,
        'PRODUCT_PROC_TYPE_ID': row.PRODUCT_PROC_TYPE_ID
    };
    $.ajax({
        type: 'post',
        url: '/api/PARAM_TYPE_LIST/PostDelete',
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
                Init_Table_PARAM_TYPE_LIST_();
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
    Init_Table_PARAM_TYPE_LIST_();
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
            Init_Table_PARAM_TYPE_LIST_();
        }
    });
}