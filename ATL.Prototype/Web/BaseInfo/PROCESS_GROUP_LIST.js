var pageSize = 20;
var editIndex_PROCESS_GROUP_LIST = undefined;
var addOrEdit_PROCESS_GROUP_LIST = null;
$(function () {
    //Table_PROCESS_GROUP_LIST_();
    //Init_Table_PROCESS_GROUP_LIST_();
    Table_PROCESS_GROUP_LIST_();
    Dialog_PROCESS_GROUP_LIST();
    InitCurrentFactoryId();
});
//不分页
function Table_PROCESS_GROUP_LIST_() {
    $('#Table_PROCESS_GROUP_LIST').datagrid({
        title: '工序组',
        singleSelect: true,
        width: '600',
        height: 'auto',
        fitColumns: false,
        toolbar: [{
            text: '新增',
            iconCls: 'icon-add',
            handler: function () {
                //范例：
                //$('#PACKAGE_TYPE_ID').val('').attr('readonly', false);
                //$('#FACTORY_ID').combobox('clear').combobox('readonly', false);
                //$('#VALID_FLAG').attr('checked', true);
                addOrEdit_PROCESS_GROUP_LIST = 'add';
                $('#PROCESS_GROUP_ID').val('').attr('readonly', false);
                $('#FACTORY_ID').val('').attr('readonly', false);
                $('#PRODUCT_TYPE_ID').val('').attr('readonly', false);
                $('#PRODUCT_PROC_TYPE_ID').val('').attr('readonly', false);
                $('#UPDATE_USER').val('').attr('readonly', false);
                $('#UPDATE_DATE').val('').attr('readonly', false);
                $('#PROCESS_GROUP_NAME').val('').attr('readonly', false);
                $('#PROCESS_GROUP_DESC').val('').attr('readonly', false);
                $('#SEQUENCE_NO').val('').attr('readonly', false);
                $('#VALID_FLAG').val('1').attr('readonly', false);
                $('#UPDATE_USER').attr('readonly', true);
                $('#UPDATE_DATE').attr('readonly', true);
                $('#Dialog_PROCESS_GROUP_LIST').dialog('open');
            }
        }, {
            text: '修改',
            iconCls: 'icon-edit',
            handler: function () {
                addOrEdit_PROCESS_GROUP_LIST = 'edit';
                var x = $('#Table_PROCESS_GROUP_LIST').datagrid('getSelected');
                if (x == null) return;
                //范例：
                //$('#PACKAGE_TYPE_ID').val(x.PACKAGE_TYPE_ID).attr('readonly', true);
                //$('#FACTORY_ID').combobox('setValue',x.FACTORY_ID).combobox('readonly', true);
                //$('#VALID_FLAG').attr('checked', x.VALID_FLAG == 1 ? true : false);               
                $('#PROCESS_GROUP_ID').val(x.PROCESS_GROUP_ID).attr('readonly', true);
                $('#FACTORY_ID').val(x.FACTORY_ID).attr('readonly', true);
                $('#PRODUCT_TYPE_ID').val(x.PRODUCT_TYPE_ID).attr('readonly', true);
                $('#PRODUCT_PROC_TYPE_ID').val(x.PRODUCT_PROC_TYPE_ID).attr('readonly', true);
                $('#UPDATE_USER').val(x.UPDATE_USER);
                $('#UPDATE_DATE').val(x.UPDATE_DATE);
                $('#PROCESS_GROUP_NAME').val(x.PROCESS_GROUP_NAME);
                $('#PROCESS_GROUP_DESC').val(x.PROCESS_GROUP_DESC);
                $('#SEQUENCE_NO').val(x.SEQUENCE_NO);
                $('#VALID_FLAG').val(x.VALID_FLAG);
                $('#UPDATE_USER').attr('readonly', true);
                $('#UPDATE_DATE').attr('readonly', true);
                $('#Dialog_PROCESS_GROUP_LIST').dialog('open');
            }
        }, {
            text: '删除',
            iconCls: 'icon-cancel',
            handler: function () {
                Deleterow_PROCESS_GROUP_LIST();
            }
        }, {
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                $('#Table_PROCESS_GROUP_LIST').datagrid('endEdit', editIndex_PROCESS_GROUP_LIST);
                var changedRow = $('#Table_PROCESS_GROUP_LIST').datagrid('getChanges');
                if (changedRow.length > 0) {
                    for (var i = 0; i < changedRow.length; i++) {
                        Edit_Cell_PROCESS_GROUP_LIST(changedRow[i]);
                    }
                }
                editIndex_PROCESS_GROUP_LIST = undefined;
                $('#Table_PROCESS_GROUP_LIST').datagrid('clearSelections');
            }
        }, {
            text: '取消',
            iconCls: 'icon-undo',
            handler: function () {
                $('#Table_PROCESS_GROUP_LIST').datagrid('rejectChanges');
            }
        }],
        frozenColumns: [[
            { field: 'PROCESS_GROUP_ID', title: '编号', align: 'left', styler: function (value, row, index) { return 'color:blue'; } }
        ]],
        columns: [[
            { field: 'SEQUENCE_NO', title: '序号', align: 'left', editor: 'text' },
            { field: 'PROCESS_GROUP_DESC', title: '中文名', align: 'left', editor: 'text' },
            { field: 'PROCESS_GROUP_NAME', title: '英文名', align: 'left', editor: 'text' },
            {
                field: 'VALID_FLAG', title: '停用/启用', align: 'left', editor: { type: 'checkbox', options: { on: '1', off: '0' } }
            },
            { field: 'UPDATE_USER', title: '最后更新者', align: 'left' },
            { field: 'UPDATE_DATE', title: '最后更新日期', align: 'left' }
        ]],
        onClickRow: function (rowIndex, rowData) {
            editrow_PROCESS_GROUP_LIST(rowIndex);
        },
        onDblClickRow: function (rowIndex, rowData) { },
        rowStyler: function (index, row) { },
        onBeforeEdit: function (index, row) {
            editIndex_PROCESS_GROUP_LIST = index;
            row.editing = true;
            $('#Table_PROCESS_GROUP_LIST').datagrid('refreshRow', index);
        },
        onAfterEdit: function (index, row) {
            row.editing = false;
            $('#Table_PROCESS_GROUP_LIST').datagrid('refreshRow', index);
        },
        onCancelEdit: function (index, row) {
            row.editing = false;
            $('#Table_PROCESS_GROUP_LIST').datagrid('refreshRow', index);
        }
    });
}
function Init_Table_PROCESS_GROUP_LIST_() {
    var factoryId = $('#FACTORY_ID_SEARCH').combobox('getValue');
    var productTypeId = $('#PRODUCT_TYPE_ID_SEARCH').combobox('getValue');
    var produceProcTypeId = $('#PRODUCT_PROC_TYPE_ID_SEARCH').combobox('getValue');
    var j = {
        'factoryId': factoryId,
        'productTypeId': productTypeId,
        'produceProcTypeId': produceProcTypeId,
        'queryStr': ''
    };
    $.ajax({
        type: 'get',
        url: '/api/PROCESS_GROUP_LIST/GetData',
        data: j,
        dataType: 'json',
        success: function (data) {
            $('#Table_PROCESS_GROUP_LIST').datagrid("loadData", data).datagrid('acceptChanges');
        }
    });
}
function editrow_PROCESS_GROUP_LIST(index) {
    if (editIndex_PROCESS_GROUP_LIST != undefined)
        $('#Table_PROCESS_GROUP_LIST').datagrid('endEdit', editIndex_PROCESS_GROUP_LIST);
    $('#Table_PROCESS_GROUP_LIST').datagrid('beginEdit', index).datagrid('selectRow', index);
}
function Deleterow_PROCESS_GROUP_LIST() {
    var row = $('#Table_PROCESS_GROUP_LIST').datagrid('getSelected');
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
            Delete_PROCESS_GROUP_LIST(row);
            $('#Table_PROCESS_GROUP_LIST').datagrid('deleteRow', $('#Table_PROCESS_GROUP_LIST').datagrid('getRowIndex'));
        }
    });
}
function Dialog_PROCESS_GROUP_LIST() {
    //选择框，勾选状态无法更改的解决办法：
    //$('#VALID_FLAG').click(
    //    function () {
    //        if ($('#VALID_FLAG').attr('checked') == 'checked') {
    //            $('#VALID_FLAG').attr('checked', false);
    //        } else {
    //            $('#VALID_FLAG').attr('checked', true);
    //        }
    //    });
    $('#Dialog_PROCESS_GROUP_LIST').dialog({
        toolbar: [{
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                if (addOrEdit_PROCESS_GROUP_LIST == 'add') {
                    Add_PROCESS_GROUP_LIST();
                }
                else if (addOrEdit_PROCESS_GROUP_LIST == 'edit') {
                    Edit_PROCESS_GROUP_LIST();
                }
            }
        }]
    });
}
function Validate() {
    if (!
        ($('#PROCESS_GROUP_ID').validatebox('isValid') &&
        $('#PROCESS_GROUP_DESC').validatebox('isValid') &&
        $('#PROCESS_GROUP_NAME').validatebox('isValid') &&
        $('#SEQUENCE_NO').validatebox('isValid')
        )) {
        $.messager.show({
            title: '消息',
            msg: '输入不正确',
            showType: 'show'
        });
        return false;
    }
    if ($('#PROCESS_GROUP_ID').val() == '' && $('#PROCESS_GROUP_DESC').val() == '') {
        $.messager.show({
            title: '消息',
            msg: '请填写',
            showType: 'show'
        });
        return false;
    }
    return true;
}
function Add_PROCESS_GROUP_LIST() {
    if (!Validate()) {
        return;
    }
    var factoryId = $('#FACTORY_ID_SEARCH').combobox('getValue');
    var productTypeId = $('#PRODUCT_TYPE_ID_SEARCH').combobox('getValue');
    var produceProcTypeId = $('#PRODUCT_PROC_TYPE_ID_SEARCH').combobox('getValue');
    if (factoryId == "" ||
        productTypeId == "" ||
        produceProcTypeId == ""
        ) {
        return;
    }
    var j = {
        'PROCESS_GROUP_ID': $('#PROCESS_GROUP_ID').val(),
        'FACTORY_ID': factoryId,
        'PRODUCT_TYPE_ID': productTypeId,
        'PRODUCT_PROC_TYPE_ID': produceProcTypeId,
        'UPDATE_USER': $('#UPDATE_USER').val(),
        'UPDATE_DATE': $('#UPDATE_DATE').val(),
        'PROCESS_GROUP_NAME': $('#PROCESS_GROUP_NAME').val(),
        'PROCESS_GROUP_DESC': $('#PROCESS_GROUP_DESC').val(),
        'SEQUENCE_NO': $('#SEQUENCE_NO').val(),
        'VALID_FLAG': $('#VALID_FLAG').val()
    };
    $.ajax({
        type: 'post',
        url: '/api/PROCESS_GROUP_LIST/PostAdd',
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
                Init_Table_PROCESS_GROUP_LIST_();
                $('#Dialog_PROCESS_GROUP_LIST').dialog('close');
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
function Edit_PROCESS_GROUP_LIST() {
    if (!Validate()) {
        return;
    }
    var factoryId = $('#FACTORY_ID_SEARCH').combobox('getValue');
    var productTypeId = $('#PRODUCT_TYPE_ID_SEARCH').combobox('getValue');
    var produceProcTypeId = $('#PRODUCT_PROC_TYPE_ID_SEARCH').combobox('getValue');
    if (factoryId == "" ||
        productTypeId == "" ||
        produceProcTypeId == ""
        ) {
        return;
    }
    var j = {
        'PROCESS_GROUP_ID': $('#PROCESS_GROUP_ID').val(),
        'FACTORY_ID': factoryId,
        'PRODUCT_TYPE_ID': productTypeId,
        'PRODUCT_PROC_TYPE_ID': produceProcTypeId,
        'UPDATE_USER': $('#UPDATE_USER').val(),
        'UPDATE_DATE': $('#UPDATE_DATE').val(),
        'PROCESS_GROUP_NAME': $('#PROCESS_GROUP_NAME').val(),
        'PROCESS_GROUP_DESC': $('#PROCESS_GROUP_DESC').val(),
        'SEQUENCE_NO': $('#SEQUENCE_NO').val(),
        'VALID_FLAG': $('#VALID_FLAG').val()
    };
    $.ajax({
        type: 'post',
        url: '/api/PROCESS_GROUP_LIST/PostEdit',
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
                Init_Table_PROCESS_GROUP_LIST_();
                $('#Dialog_PROCESS_GROUP_LIST').dialog('close');
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
function Edit_Cell_PROCESS_GROUP_LIST(row) {
    var j = {
        'PROCESS_GROUP_ID': row.PROCESS_GROUP_ID,
        'FACTORY_ID': row.FACTORY_ID,
        'PRODUCT_TYPE_ID': row.PRODUCT_TYPE_ID,
        'PRODUCT_PROC_TYPE_ID': row.PRODUCT_PROC_TYPE_ID,
        'UPDATE_USER': row.UPDATE_USER,
        'UPDATE_DATE': row.UPDATE_DATE,
        'PROCESS_GROUP_NAME': row.PROCESS_GROUP_NAME,
        'PROCESS_GROUP_DESC': row.PROCESS_GROUP_DESC,
        'SEQUENCE_NO': row.SEQUENCE_NO,
        'VALID_FLAG': row.VALID_FLAG
    };
    $.ajax({
        type: 'post',
        url: '/api/PROCESS_GROUP_LIST/PostEdit',
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
function Delete_PROCESS_GROUP_LIST(row) {
    var j = {
        'PROCESS_GROUP_ID': row.PROCESS_GROUP_ID,
        'FACTORY_ID': row.FACTORY_ID,
        'PRODUCT_TYPE_ID': row.PRODUCT_TYPE_ID,
        'PRODUCT_PROC_TYPE_ID': row.PRODUCT_PROC_TYPE_ID
    };
    $.ajax({
        type: 'post',
        url: '/api/PROCESS_GROUP_LIST/PostDelete',
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
                Init_Table_PROCESS_GROUP_LIST_();
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
    Init_Table_PROCESS_GROUP_LIST_();    
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
            Init_Table_PROCESS_GROUP_LIST_();
        }
    });
}
