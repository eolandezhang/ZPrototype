var pageSize = 20;
var editIndex_RECIPE_LIST = undefined;
var addOrEdit_RECIPE_LIST = null;
var editIndex_RECIPE_TYPE_LIST = undefined;
var addOrEdit_RECIPE_TYPE_LIST = null;
$(function () {
    Table_RECIPE_LIST_();
    Dialog_RECIPE_LIST();
    InitCurrentFactoryId();
    Table_RECIPE_TYPE_LIST_();
    Dialog_RECIPE_TYPE_LIST();
});

function Table_RECIPE_LIST_() {
    $('#Table_RECIPE_LIST').datagrid({
        title: '配方',
        singleSelect: true,
        width: '840',
        height: 'auto',
        fitColumns: false,
        toolbar: [{
            text: '新增',
            iconCls: 'icon-add',
            handler: function () {
                addOrEdit_RECIPE_LIST = 'add';
                $('#RECIPE_ID').val('').attr('readonly', false);
                $('#UPDATE_USER').val('').attr('readonly', true);
                $('#UPDATE_DATE').val('').attr('readonly', true);
                $('#VALID_FLAG').val(1).attr('readonly', false);
                $('#RECIPE_NAME').val('').attr('readonly', false);
                $('#RECIPE_DESC').val('').attr('readonly', false);
                $('#SOLID_CONTENT').val('').attr('readonly', false);
                $('#SCP_VAR').val('').attr('readonly', false);
                $('#BASE_RECIPE').val('').attr('readonly', false);
                $('#STAGE').val('').attr('readonly', false);
                $('#IS_HIGH_VISCOSITY').val('').attr('readonly', false);
                $('#PROC_CONDITION').val('').attr('readonly', false);
                $('#OTHER_CONDITION').val('').attr('readonly', false);
                $('#Dialog_RECIPE_LIST').dialog('open');
            }
        }, {
            text: '修改',
            iconCls: 'icon-edit',
            handler: function () {
                addOrEdit_RECIPE_LIST = 'edit';
                var x = $('#Table_RECIPE_LIST').datagrid('getSelected');
                if (x == null) return;
                $('#RECIPE_ID').val(x.RECIPE_ID).attr('readonly', true);
                $('#FACTORY_ID').val(x.FACTORY_ID).attr('readonly', true);
                $('#PRODUCT_TYPE_ID').val(x.PRODUCT_TYPE_ID).attr('readonly', true);
                $('#PRODUCT_PROC_TYPE_ID').val(x.PRODUCT_PROC_TYPE_ID).attr('readonly', true);
                $('#UPDATE_USER').val(x.UPDATE_USER);
                $('#UPDATE_DATE').val(x.UPDATE_DATE);
                $('#RECIPE_TYPE_ID').val(x.RECIPE_TYPE_ID);
                $('#VALID_FLAG').val(x.VALID_FLAG);
                $('#RECIPE_NAME').val(x.RECIPE_NAME);
                $('#RECIPE_DESC').val(x.RECIPE_DESC);
                $('#SOLID_CONTENT').val(x.SOLID_CONTENT);
                $('#SCP_VAR').val(x.SCP_VAR);
                $('#BASE_RECIPE').val(x.BASE_RECIPE);
                $('#STAGE').val(x.STAGE);
                $('#IS_HIGH_VISCOSITY').val(x.IS_HIGH_VISCOSITY);
                $('#PROC_CONDITION').val(x.PROC_CONDITION);
                $('#OTHER_CONDITION').val(x.OTHER_CONDITION);
                $('#UPDATE_USER').attr('readonly', true);
                $('#UPDATE_DATE').attr('readonly', true);
                $('#Dialog_RECIPE_LIST').dialog('open');
            }
        }, {
            text: '删除',
            iconCls: 'icon-cancel',
            handler: function () {
                Deleterow_RECIPE_LIST();
            }
        }, {
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                $('#Table_RECIPE_LIST').datagrid('endEdit', editIndex_RECIPE_LIST);
                var changedRow = $('#Table_RECIPE_LIST').datagrid('getChanges');
                if (changedRow.length > 0) {
                    for (var i = 0; i < changedRow.length; i++) {
                        Edit_Cell_RECIPE_LIST(changedRow[i]);
                    }
                }
                editIndex_RECIPE_LIST = undefined;
                $('#Table_RECIPE_LIST').datagrid('clearSelections');
            }
        }, {
            text: '取消',
            iconCls: 'icon-undo',
            handler: function () {
                $('#Table_RECIPE_LIST').datagrid('rejectChanges');
            }
        }],
        frozenColumns: [[
            { field: 'RECIPE_ID', title: '编号', align: 'left', styler: function (value, row, index) { return 'color:blue'; } }
        ]],
        columns: [[
            { field: 'RECIPE_NAME', title: '英文名', align: 'left', editor: 'text' },
            { field: 'RECIPE_DESC', title: '中文名', align: 'left', editor: 'text' },
            { field: 'SOLID_CONTENT', title: 'SOLID_CONTENT', align: 'left', editor: 'text' },
            { field: 'SCP_VAR', title: 'SCP_VAR', align: 'left', editor: 'text' },
            { field: 'BASE_RECIPE', title: 'BASE_RECIPE', align: 'left', editor: 'text' },
            { field: 'STAGE', title: 'STAGE', align: 'left', editor: 'text' },
            { field: 'IS_HIGH_VISCOSITY', title: 'IS_HIGH_VISCOSITY', align: 'left', editor: 'text' },
            { field: 'PROC_CONDITION', title: 'PROC_CONDITION', align: 'left', editor: 'text' },
            { field: 'OTHER_CONDITION', title: 'OTHER_CONDITION', align: 'left', editor: 'text' },
            { field: 'UPDATE_USER', title: '最后更新者', align: 'left' },
            { field: 'UPDATE_DATE', title: '最后更新日期', align: 'left' },
            { field: 'VALID_FLAG', title: '状态', align: 'left', editor: { type: 'checkbox', options: { on: '1', off: '0' } } }
        ]],
        onClickRow: function (rowIndex, rowData) {
            editrow_RECIPE_LIST(rowIndex);
        },
        onDblClickRow: function (rowIndex, rowData) { },
        rowStyler: function (index, row) { },
        onBeforeEdit: function (index, row) {
            editIndex_RECIPE_LIST = index;
            row.editing = true;
            $('#Table_RECIPE_LIST').datagrid('refreshRow', index);
        },
        onAfterEdit: function (index, row) {
            row.editing = false;
            $('#Table_RECIPE_LIST').datagrid('refreshRow', index);
        },
        onCancelEdit: function (index, row) {
            row.editing = false;
            $('#Table_RECIPE_LIST').datagrid('refreshRow', index);
        },
        onRowContextMenu: function (e, rowIndex, rowData) {
            e.preventDefault();
        }
    });
}
function Init_Table_RECIPE_LIST_() {
    var x = $('#Table_RECIPE_TYPE_LIST').datagrid('getSelected');
    if (x == null) return;
    var factoryId = $('#FACTORY_ID_SEARCH').combobox('getValue');
    var productTypeId = $('#PRODUCT_TYPE_ID_SEARCH').combobox('getValue');
    var produceProcTypeId = $('#PRODUCT_PROC_TYPE_ID_SEARCH').combobox('getValue');
    var j = {
        'FACTORY_ID': factoryId,
        'PRODUCT_TYPE_ID': productTypeId,
        'PRODUCT_PROC_TYPE_ID': produceProcTypeId,
        'RECIPE_TYPE_ID': x.RECIPE_TYPE_ID,
        'queryStr': ''
    };
    $.ajax({
        type: 'get',
        url: '/api/RECIPE_LIST/GetDataByTypeId',
        data: j,
        dataType: 'json',
        success: function (data) {           
            $('#Table_RECIPE_LIST').datagrid("loadData", data).datagrid('acceptChanges');
        }
    });
}
function editrow_RECIPE_LIST(index) {
    if (editIndex_RECIPE_LIST != undefined)
        $('#Table_RECIPE_LIST').datagrid('endEdit', editIndex_RECIPE_LIST);
    $('#Table_RECIPE_LIST').datagrid('beginEdit', index).datagrid('selectRow', index);
}
function Deleterow_RECIPE_LIST() {
    var row = $('#Table_RECIPE_LIST').datagrid('getSelected');
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
            Delete_RECIPE_LIST(row);
        }
    });
}
function Dialog_RECIPE_LIST() {
    $('#Dialog_RECIPE_LIST').dialog({
        toolbar: [{
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                if (addOrEdit_RECIPE_LIST == 'add') {
                    Add_RECIPE_LIST();
                }
                else if (addOrEdit_RECIPE_LIST == 'edit') {
                    Edit_RECIPE_LIST();
                }
            }
        }]
    });
}
function Validate_RECIPE_LIST() {
    if (!(
        $('#RECIPE_ID').validatebox('isValid') &&
        $('#FACTORY_ID').validatebox('isValid') &&
        $('#PRODUCT_TYPE_ID').validatebox('isValid') &&
        $('#PRODUCT_PROC_TYPE_ID').validatebox('isValid') &&
        $('#UPDATE_USER').validatebox('isValid') &&
        $('#UPDATE_DATE').validatebox('isValid') &&
        $('#RECIPE_TYPE_ID').validatebox('isValid') &&
        $('#VALID_FLAG').validatebox('isValid') &&
        $('#RECIPE_NAME').validatebox('isValid') &&
        $('#RECIPE_DESC').validatebox('isValid') &&
        $('#SOLID_CONTENT').validatebox('isValid') &&
        $('#SCP_VAR').validatebox('isValid') &&
        $('#BASE_RECIPE').validatebox('isValid') &&
        $('#STAGE').validatebox('isValid') &&
        $('#IS_HIGH_VISCOSITY').validatebox('isValid') &&
        $('#PROC_CONDITION').validatebox('isValid') &&
        $('#OTHER_CONDITION').validatebox('isValid')
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
function Add_RECIPE_LIST() {
    var x = $('#Table_RECIPE_TYPE_LIST').datagrid('getSelected');
    if (x == null) return;
    var factoryId = $('#FACTORY_ID_SEARCH').combobox('getValue');
    var productTypeId = $('#PRODUCT_TYPE_ID_SEARCH').combobox('getValue');
    var produceProcTypeId = $('#PRODUCT_PROC_TYPE_ID_SEARCH').combobox('getValue');
    var j = {
        'RECIPE_ID': $('#RECIPE_ID').val(),
        'FACTORY_ID': factoryId,
        'PRODUCT_TYPE_ID': productTypeId,
        'PRODUCT_PROC_TYPE_ID': produceProcTypeId,
        'UPDATE_USER': $('#UPDATE_USER').val(),
        'UPDATE_DATE': $('#UPDATE_DATE').val(),
        'RECIPE_TYPE_ID': x.RECIPE_TYPE_ID,
        'VALID_FLAG': $('#VALID_FLAG').val(),
        'RECIPE_NAME': $('#RECIPE_NAME').val(),
        'RECIPE_DESC': $('#RECIPE_DESC').val(),
        'SOLID_CONTENT': $('#SOLID_CONTENT').val(),
        'SCP_VAR': $('#SCP_VAR').val(),
        'BASE_RECIPE': $('#BASE_RECIPE').val(),
        'STAGE': $('#STAGE').val(),
        'IS_HIGH_VISCOSITY': $('#IS_HIGH_VISCOSITY').val(),
        'PROC_CONDITION': $('#PROC_CONDITION').val(),
        'OTHER_CONDITION': $('#OTHER_CONDITION').val()
    };
    $.ajax({
        type: 'post',
        url: '/api/RECIPE_LIST/PostAdd',
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
                Init_Table_RECIPE_LIST_();
                //Init_Table_RECIPE_LIST(1,pageSize);
                $('#Dialog_RECIPE_LIST').dialog('close');
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
function Edit_RECIPE_LIST() {
    //if (!Validate_RECIPE_LIST()) {
    //    return;
    //}
    var x = $('#Table_RECIPE_LIST').datagrid('getSelected');
    if (x == null) return;

    var j = {
        'RECIPE_ID': x.RECIPE_ID,
        'FACTORY_ID': x.FACTORY_ID,
        'PRODUCT_TYPE_ID': x.PRODUCT_TYPE_ID,
        'PRODUCT_PROC_TYPE_ID': x.PRODUCT_PROC_TYPE_ID,
        'UPDATE_USER': x.UPDATE_USER,
        'UPDATE_DATE': x.UPDATE_DATE,
        'RECIPE_TYPE_ID': x.RECIPE_TYPE_ID,
        'VALID_FLAG': $('#VALID_FLAG').val(),
        'RECIPE_NAME': $('#RECIPE_NAME').val(),
        'RECIPE_DESC': $('#RECIPE_DESC').val(),
        'SOLID_CONTENT': $('#SOLID_CONTENT').val(),
        'SCP_VAR': $('#SCP_VAR').val(),
        'BASE_RECIPE': $('#BASE_RECIPE').val(),
        'STAGE': $('#STAGE').val(),
        'IS_HIGH_VISCOSITY': $('#IS_HIGH_VISCOSITY').val(),
        'PROC_CONDITION': $('#PROC_CONDITION').val(),
        'OTHER_CONDITION': $('#OTHER_CONDITION').val()
    };
    $.ajax({
        type: 'post',
        url: '/api/RECIPE_LIST/PostEdit',
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
                Init_Table_RECIPE_LIST_();
                //Init_Table_RECIPE_LIST(1,pageSize);
                $('#Dialog_RECIPE_LIST').dialog('close');
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
function Edit_Cell_RECIPE_LIST(row) {
    var j = {
        'RECIPE_ID': row.RECIPE_ID,
        'FACTORY_ID': row.FACTORY_ID,
        'PRODUCT_TYPE_ID': row.PRODUCT_TYPE_ID,
        'PRODUCT_PROC_TYPE_ID': row.PRODUCT_PROC_TYPE_ID,
        'UPDATE_USER': row.UPDATE_USER,
        'UPDATE_DATE': row.UPDATE_DATE,
        'RECIPE_TYPE_ID': row.RECIPE_TYPE_ID,
        'VALID_FLAG': row.VALID_FLAG,
        'RECIPE_NAME': row.RECIPE_NAME,
        'RECIPE_DESC': row.RECIPE_DESC,
        'SOLID_CONTENT': row.SOLID_CONTENT,
        'SCP_VAR': row.SCP_VAR,
        'BASE_RECIPE': row.BASE_RECIPE,
        'STAGE': row.STAGE,
        'IS_HIGH_VISCOSITY': row.IS_HIGH_VISCOSITY,
        'PROC_CONDITION': row.PROC_CONDITION,
        'OTHER_CONDITION': row.OTHER_CONDITION
    };
    $.ajax({
        type: 'post',
        url: '/api/RECIPE_LIST/PostEdit',
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
function Delete_RECIPE_LIST(row) {
    var j = {
        'RECIPE_ID': row.RECIPE_ID,
        'FACTORY_ID': row.FACTORY_ID,
        'PRODUCT_TYPE_ID': row.PRODUCT_TYPE_ID,
        'PRODUCT_PROC_TYPE_ID': row.PRODUCT_PROC_TYPE_ID
    };
    $.ajax({
        type: 'post',
        url: '/api/RECIPE_LIST/PostDelete',
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
                Init_Table_RECIPE_LIST_();
                //Init_Table_RECIPE_LIST(1,pageSize);
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
    Init_Table_RECIPE_TYPE_LIST_();

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

        }
    });
}


function Table_RECIPE_TYPE_LIST_() {
    $('#Table_RECIPE_TYPE_LIST').datagrid({
        title: '类型',
        singleSelect: true,
        width: '240',
        height: '400',
        fitColumns: false,
        toolbar: [{
            text: '新增',
            iconCls: 'icon-add',
            handler: function () {
                addOrEdit_RECIPE_TYPE_LIST = 'add';
                $('#RECIPE_TYPE_ID').val('').attr('readonly', false);
                $('#FACTORY_ID').val('').attr('readonly', false);
                $('#PRODUCT_TYPE_ID').val('').attr('readonly', false);
                $('#PRODUCT_PROC_TYPE_ID').val('').attr('readonly', false);
                $('#UPDATE_USER').val('').attr('readonly', false);
                $('#UPDATE_DATE').val('').attr('readonly', false);
                $('#VALID_FLAG').val('').attr('readonly', false);
                $('#RECIPE_TYPE_NAME').val('').attr('readonly', false);
                $('#RECIPE_TYPE_DESC').val('').attr('readonly', false);
                $('#UPDATE_USER').attr('readonly', true);
                $('#UPDATE_DATE').attr('readonly', true);
                $('#Dialog_RECIPE_TYPE_LIST').dialog('open');
            }
        }, {
            text: '修改',
            handler: function () {
                addOrEdit_RECIPE_TYPE_LIST = 'edit';
                var x = $('#Table_RECIPE_TYPE_LIST').datagrid('getSelected');
                if (x == null) return;
                $('#RECIPE_TYPE_ID').val(x.RECIPE_TYPE_ID).attr('readonly', true);
                $('#FACTORY_ID').val(x.FACTORY_ID).attr('readonly', true);
                $('#PRODUCT_TYPE_ID').val(x.PRODUCT_TYPE_ID).attr('readonly', true);
                $('#PRODUCT_PROC_TYPE_ID').val(x.PRODUCT_PROC_TYPE_ID).attr('readonly', true);
                $('#UPDATE_USER').val(x.UPDATE_USER);
                $('#UPDATE_DATE').val(x.UPDATE_DATE);
                $('#VALID_FLAG').val(x.VALID_FLAG);
                $('#RECIPE_TYPE_NAME').val(x.RECIPE_TYPE_NAME);
                $('#RECIPE_TYPE_DESC').val(x.RECIPE_TYPE_DESC);
                $('#UPDATE_USER').attr('readonly', true);
                $('#UPDATE_DATE').attr('readonly', true);
                $('#Dialog_RECIPE_TYPE_LIST').dialog('open');
            }
        }, {
            text: '删除',
            handler: function () {
                Deleterow_RECIPE_TYPE_LIST();
            }
        }, {
            text: '保存',
            handler: function () {
                $('#Table_RECIPE_TYPE_LIST').datagrid('endEdit', editIndex_RECIPE_TYPE_LIST);
                var changedRow = $('#Table_RECIPE_TYPE_LIST').datagrid('getChanges');
                if (changedRow.length > 0) {
                    for (var i = 0; i < changedRow.length; i++) {
                        Edit_Cell_RECIPE_TYPE_LIST(changedRow[i]);
                    }
                }
                editIndex_RECIPE_TYPE_LIST = undefined;
                $('#Table_RECIPE_TYPE_LIST').datagrid('clearSelections');
            }
        }, {
            text: '取消',
            handler: function () {
                $('#Table_RECIPE_TYPE_LIST').datagrid('rejectChanges');
            }
        }],
        frozenColumns: [[
            { field: 'RECIPE_TYPE_ID', title: '类型', align: 'left', styler: function (value, row, index) { return 'color:blue'; } }
        ]],
        columns: [[
            { field: 'RECIPE_TYPE_NAME', title: '英文名', align: 'left', editor: 'text' },
            { field: 'RECIPE_TYPE_DESC', title: '中文名', align: 'left', editor: 'text' },
{ field: 'UPDATE_USER', title: '最后更新者', align: 'left' },
            { field: 'UPDATE_DATE', title: '最后更新日期', align: 'left' },
            { field: 'VALID_FLAG', title: 'VALID_FLAG', align: 'left', editor: { type: 'checkbox', options: { on: '1', off: '0' } } }
            //下拉列表范例：
            //editor: {
            //            type: 'combobox',
            //            options: {
            //                valueField: 'FACTORY_ID',
            //                textField: 'FACTORY_NAME',
            //                url: '/api/FACTORY_LIST/GetData',
            //                method: 'get',
            //                required: false
            //            }
            //        }

            //选择框
            //editor: { type: 'checkbox', options: { on: '1', off: '0' } }

            //editor: {
            //        type: 'validatebox',
            //        options: {
            //            required: false,
            //            validType: ['maxLength[10]']//'number'
            //        }
            //    }

            //formatter: function (value,row,index) {
             //      switch (value) {
             //           case '0':
             //               return '';
             //           case '1':
             //               return '是';
             //       }
             //   }
        ]],
        onClickRow: function (rowIndex, rowData) {
            editrow_RECIPE_TYPE_LIST(rowIndex);
            Init_Table_RECIPE_LIST_();
        },
        onDblClickRow: function (rowIndex, rowData) { },
        rowStyler: function (index, row) { },
        onBeforeEdit: function (index, row) {
            editIndex_RECIPE_TYPE_LIST = index;
            row.editing = true;
            $('#Table_RECIPE_TYPE_LIST').datagrid('refreshRow', index);
        },
        onAfterEdit: function (index, row) {
            row.editing = false;
            $('#Table_RECIPE_TYPE_LIST').datagrid('refreshRow', index);
        },
        onCancelEdit: function (index, row) {
            row.editing = false;
            $('#Table_RECIPE_TYPE_LIST').datagrid('refreshRow', index);
        },
        onRowContextMenu: function (e, rowIndex, rowData) {
            e.preventDefault();
        }
    });
}
function Init_Table_RECIPE_TYPE_LIST_() {
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
        url: '/api/RECIPE_TYPE_LIST/GetDataByFactoryId',
        data: j,
        dataType: 'json',
        success: function (data) {
            $('#Table_RECIPE_TYPE_LIST').datagrid("loadData", data).datagrid('acceptChanges');
        }
    });
}
function editrow_RECIPE_TYPE_LIST(index) {
    if (editIndex_RECIPE_TYPE_LIST != undefined)
        $('#Table_RECIPE_TYPE_LIST').datagrid('endEdit', editIndex_RECIPE_TYPE_LIST);
    $('#Table_RECIPE_TYPE_LIST').datagrid('beginEdit', index).datagrid('selectRow', index);
}
function Deleterow_RECIPE_TYPE_LIST() {
    var row = $('#Table_RECIPE_TYPE_LIST').datagrid('getSelected');
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
            Delete_RECIPE_TYPE_LIST(row);
        }
    });
}
function Dialog_RECIPE_TYPE_LIST() {
    $('#Dialog_RECIPE_TYPE_LIST').dialog({
        toolbar: [{
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                if (addOrEdit_RECIPE_TYPE_LIST == 'add') {
                    Add_RECIPE_TYPE_LIST();
                }
                else if (addOrEdit_RECIPE_TYPE_LIST == 'edit') {
                    Edit_RECIPE_TYPE_LIST();
                }
            }
        }]
    });
}
function Validate_RECIPE_TYPE_LIST() {
    if (!(
        $('#RECIPE_TYPE_ID').validatebox('isValid') &&
        $('#FACTORY_ID').validatebox('isValid') &&
        $('#PRODUCT_TYPE_ID').validatebox('isValid') &&
        $('#PRODUCT_PROC_TYPE_ID').validatebox('isValid') &&
        $('#UPDATE_USER').validatebox('isValid') &&
        $('#UPDATE_DATE').validatebox('isValid') &&
        $('#VALID_FLAG').validatebox('isValid') &&
        $('#RECIPE_TYPE_NAME').validatebox('isValid') &&
        $('#RECIPE_TYPE_DESC').validatebox('isValid')
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
function Add_RECIPE_TYPE_LIST() {
    var factoryId = $('#FACTORY_ID_SEARCH').combobox('getValue');
    var productTypeId = $('#PRODUCT_TYPE_ID_SEARCH').combobox('getValue');
    var produceProcTypeId = $('#PRODUCT_PROC_TYPE_ID_SEARCH').combobox('getValue');
    var j = {
        'RECIPE_TYPE_ID': $('#RECIPE_TYPE_ID').val(),
        'FACTORY_ID': factoryId,
        'PRODUCT_TYPE_ID': productTypeId,
        'PRODUCT_PROC_TYPE_ID': produceProcTypeId,
        'UPDATE_USER': $('#UPDATE_USER_RECIPE_TYPE_LIST').val(),
        'UPDATE_DATE': $('#UPDATE_DATE_RECIPE_TYPE_LIST').val(),
        'VALID_FLAG': $('#VALID_FLAG_RECIPE_TYPE_LIST').val(),
        'RECIPE_TYPE_NAME': $('#RECIPE_TYPE_NAME').val(),
        'RECIPE_TYPE_DESC': $('#RECIPE_TYPE_DESC').val()
    };
    $.ajax({
        type: 'post',
        url: '/api/RECIPE_TYPE_LIST/PostAdd',
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
                Init_Table_RECIPE_TYPE_LIST_();
                $('#Dialog_RECIPE_TYPE_LIST').dialog('close');
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
function Edit_RECIPE_TYPE_LIST() {
    //if (!Validate_RECIPE_TYPE_LIST()) {
    //    return;
    //}
    var x = $('#Table_RECIPE_TYPE_LIST').datagrid('getSelected');
    if (x == null) return;

    var j = {
        'RECIPE_TYPE_ID': x.RECIPE_TYPE_ID,
        'FACTORY_ID': x.FACTORY_ID,
        'PRODUCT_TYPE_ID': x.PRODUCT_TYPE_ID,
        'PRODUCT_PROC_TYPE_ID': x.PRODUCT_PROC_TYPE_ID,
        'UPDATE_USER': x.UPDATE_USER,
        'UPDATE_DATE': x.UPDATE_DATE,
        'VALID_FLAG': $('#VALID_FLAG_RECIPE_TYPE_LIST').val(),
        'RECIPE_TYPE_NAME': $('#RECIPE_TYPE_NAME').val(),
        'RECIPE_TYPE_DESC': $('#RECIPE_TYPE_NAME').val()
    };
    $.ajax({
        type: 'post',
        url: '/api/RECIPE_TYPE_LIST/PostEdit',
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
                Init_Table_RECIPE_TYPE_LIST_();
                $('#Dialog_RECIPE_TYPE_LIST').dialog('close');
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
function Edit_Cell_RECIPE_TYPE_LIST(row) {
    var j = {
        'RECIPE_TYPE_ID': row.RECIPE_TYPE_ID,
        'FACTORY_ID': row.FACTORY_ID,
        'PRODUCT_TYPE_ID': row.PRODUCT_TYPE_ID,
        'PRODUCT_PROC_TYPE_ID': row.PRODUCT_PROC_TYPE_ID,
        'UPDATE_USER': row.UPDATE_USER,
        'UPDATE_DATE': row.UPDATE_DATE,
        'VALID_FLAG': row.VALID_FLAG,
        'RECIPE_TYPE_NAME': row.RECIPE_TYPE_NAME,
        'RECIPE_TYPE_DESC': row.RECIPE_TYPE_DESC
    };
    $.ajax({
        type: 'post',
        url: '/api/RECIPE_TYPE_LIST/PostEdit',
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
function Delete_RECIPE_TYPE_LIST(row) {
    var j = {
        'RECIPE_TYPE_ID': row.RECIPE_TYPE_ID,
        'FACTORY_ID': row.FACTORY_ID,
        'PRODUCT_TYPE_ID': row.PRODUCT_TYPE_ID,
        'PRODUCT_PROC_TYPE_ID': row.PRODUCT_PROC_TYPE_ID
    };
    $.ajax({
        type: 'post',
        url: '/api/RECIPE_TYPE_LIST/PostDelete',
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
                Init_Table_RECIPE_TYPE_LIST_();
                //Init_Table_RECIPE_TYPE_LIST(1,pageSize);
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
