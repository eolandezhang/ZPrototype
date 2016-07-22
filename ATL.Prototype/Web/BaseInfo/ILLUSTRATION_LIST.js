var editIndex = undefined;
var addOrEdit = null;
var ILLUSTRATION_ID = null;
var ILLUSTRATION_DESC = null;
var FACTORY_ID = null;
var PRODUCT_TYPE_ID = null;
var PRODUCT_PROC_TYPE_ID = null;
var PROCESS_ID = null;
var IMG_LENGTH = null;

var editIndex_ILLUSTRATION_PARAM_INFO = undefined;
var addOrEdit_ILLUSTRATION_PARAM_INFO = null;
var editIndex_PARAMETER_LIST = undefined;

$(function () {
    Table_ILLUSTRATION_LIST([]);
    Dialog_ILLUSTRATION_LIST();
    Init_FACTORY_ID_SEARCH();
    Table_PROCESS_LIST([]);
    InitCurrentFactoryId();
    Table_ILLUSTRATION_PARAM_INFO_();
    $('#Table_ILLUSTRATION_PARAM_INFO').datagrid("loadData", []);
    Dialog_ILLUSTRATION_PARAM_INFO();
    Table_PARAMETER_LIST_();
});
function Table_ILLUSTRATION_LIST(data) {
    $('#Table_ILLUSTRATION_LIST').datagrid({
        title: '图片类型',
        singleSelect: true,
        width: '600',
        height: '250',
        fitColumns: false,
        autoRowHeight: false,
        toolbar: [{
            text: '新增',
            iconCls: 'icon-add',
            handler: function () {
                addOrEdit = 'add';
                ILLUSTRATION_DATA();

                var x = $('#Table_PROCESS_LIST').datagrid('getSelected');
                if (x == null) return;
                var processId = x.PROCESS_ID;

                var factoryId = $('#FACTORY_ID_SEARCH').combobox('getValue');
                var productTypeId = $('#PRODUCT_TYPE_ID_SEARCH').combobox('getValue');
                var produceProcTypeId = $('#PRODUCT_PROC_TYPE_ID_SEARCH').combobox('getValue');

                $('#PROCESS_ID').val(processId);
                $('#FACTORY_ID').val(factoryId);
                $('#PRODUCT_TYPE_ID').val(productTypeId);
                $('#PRODUCT_PROC_TYPE_ID').val(produceProcTypeId);

                $('#ILLUSTRATION_ID').val('').attr('readonly', false);
                $('#UPDATE_USER').val('').attr('readonly', true).css('border', 'none');
                $('#UPDATE_DATE').val('').attr('readonly', true).css('border', 'none');

                $('#ILLUSTRATION_DESC').val('').attr('readonly', false);
                $('#VALID_FLAG').val(1);
                $('#IMG_LENGTH').val('');

                $('#Dialog_ILLUSTRATION_LIST').dialog('open');
            }
        }, {
            text: '修改',
            iconCls: 'icon-edit',
            handler: function () {
                addOrEdit = 'edit';
                var x = $('#Table_ILLUSTRATION_LIST').datagrid('getSelected');
                if (x == null) return;
                ILLUSTRATION_DATA();

                $('#ILLUSTRATION_ID').val(x.ILLUSTRATION_ID).attr('readonly', true);
                $('#FACTORY_ID').val(x.FACTORY_ID);
                $('#PRODUCT_TYPE_ID').val(x.PRODUCT_TYPE_ID);
                $('#PRODUCT_PROC_TYPE_ID').val(x.PRODUCT_PROC_TYPE_ID);
                $('#UPDATE_USER').val(x.UPDATE_USER).attr('readonly', true).css('border', 'none');;
                $('#UPDATE_DATE').val(x.UPDATE_DATE).attr('readonly', true).css('border', 'none');;
                $('#PROCESS_ID').val(x.PROCESS_ID);
                $('#ILLUSTRATION_DESC').val(x.ILLUSTRATION_DESC);
                $('#VALID_FLAG').val(x.VALID_FLAG);
                $('#IMG_LENGTH').val(x.IMG_LENGTH);
                $('#uploadimg').hide();
                $('#Dialog_ILLUSTRATION_LIST').dialog('open');
            }
        }
        , {
            text: '删除',
            iconCls: 'icon-cancel',
            handler: function () {
                Deleterow();
            }
        }, {
            text: '保存当前页',
            iconCls: 'icon-save',
            handler: function () {
                $('#Table_ILLUSTRATION_LIST').datagrid('endEdit', editIndex);
                var changedRow = $('#Table_ILLUSTRATION_LIST').datagrid('getChanges');
                if (changedRow.length > 0) {
                    for (var i = 0; i < changedRow.length; i++) {
                        Edit_Cell(changedRow[i]);
                    }
                }
                editIndex = undefined;
                $('#Table_ILLUSTRATION_LIST').datagrid('clearSelections');
            }
        }],
        frozenColumns: [[
                { field: 'ILLUSTRATION_ID', title: '图片编号', align: 'left', styler: function (value, row, index) { return 'color:blue'; } }
        ]],
        columns: [[
                { field: 'ILLUSTRATION_DESC', title: '图片说明', align: 'left', editor: 'text' },
                { field: 'VALID_FLAG', title: '启用', align: 'left', editor: { type: 'checkbox', options: { on: '1', off: '0' } } },
                { field: 'IMG_LENGTH', title: '行数', align: 'left', editor: 'text' },
                { field: 'UPDATE_USER', title: '最后更新人', align: 'left' },
                { field: 'UPDATE_DATE', title: '最后更新日期', align: 'left' }
        ]],
        onClickRow: function (rowIndex, rowData) {
            editrow(rowIndex);
            $('#showimg').html('<img src=\"ILLUSTRATION_LIST_ShowImg.ashx?ILLUSTRATION_ID=' + rowData.ILLUSTRATION_ID + '&FACTORY_ID=' + rowData.FACTORY_ID + '&PRODUCT_TYPE_ID=' + rowData.PRODUCT_TYPE_ID + '&PRODUCT_PROC_TYPE_ID=' + rowData.PRODUCT_PROC_TYPE_ID + '\" \>');
            Init_Table_ILLUSTRATION_PARAM_INFO_();
        },
        onDblClickRow: function (rowIndex, rowData) { },
        rowStyler: function (index, row) { },
        onBeforeEdit: function (index, row) {
            editIndex = index;
            row.editing = true;
            $('#Table_ILLUSTRATION_LIST').datagrid('refreshRow', index);
        },
        onAfterEdit: function (index, row) {
            row.editing = false;
            $('#Table_ILLUSTRATION_LIST').datagrid('refreshRow', index);
        },
        onCancelEdit: function (index, row) {
            row.editing = false;
            $('#Table_ILLUSTRATION_LIST').datagrid('refreshRow', index);
        }
    });

    $('#Table_ILLUSTRATION_LIST').datagrid("loadData", data).datagrid('acceptChanges');
}
function editrow(index) {
    if (editIndex != undefined)
        $('#Table_ILLUSTRATION_LIST').datagrid('endEdit', editIndex);
    $('#Table_ILLUSTRATION_LIST').datagrid('beginEdit', index).datagrid('selectRow', index);

}
function Deleterow() {
    var row = $('#Table_ILLUSTRATION_LIST').datagrid('getSelected');
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
            Delete(row);

        }
    });
}
function Init_Table_ILLUSTRATION_LIST() {
    var x = $('#Table_PROCESS_LIST').datagrid('getSelected');
    if (x == null) return;
    var processId = x.PROCESS_ID;
    var factoryId = $('#FACTORY_ID_SEARCH').combobox('getValue');
    var productTypeId = $('#PRODUCT_TYPE_ID_SEARCH').combobox('getValue');
    var produceProcTypeId = $('#PRODUCT_PROC_TYPE_ID_SEARCH').combobox('getValue');
    $.ajax({
        type: 'get',
        url: '/api/ILLUSTRATION_LIST/GetDataByFactoryIdAndTypeAndProcessId',
        data: {
            'FACTORY_ID': factoryId,
            'PRODUCT_TYPE_ID': productTypeId,
            'PRODUCT_PROC_TYPE_ID': produceProcTypeId,
            'PROCESS_ID': processId
        },
        dataType: 'json',
        success: function (data) {
            Table_ILLUSTRATION_LIST(data);
        }
    });
}
function Dialog_ILLUSTRATION_LIST() {
    $('#Dialog_ILLUSTRATION_LIST').dialog({
        toolbar: [{
            text: 'Save',
            iconCls: 'icon-save',
            handler: function () {
                if (addOrEdit == 'add') {
                    Add();
                }
                else if (addOrEdit == 'edit') {
                    Edit();
                }
            }
        }]
    });
}
function Validate_ILLUSTRATION_LIST() {
    if (!(
        $('#IMG_LENGTH').validatebox('isValid') &&
        $('#ILLUSTRATION_ID').validatebox('isValid') &&
        $('#ILLUSTRATION_DESC').validatebox('isValid')
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
function Add() {
    if (!Validate_ILLUSTRATION_LIST()) {
        return;
    }
    FACTORY_ID = $('#FACTORY_ID_SEARCH').combobox('getValue');
    PRODUCT_TYPE_ID = $('#PRODUCT_TYPE_ID_SEARCH').combobox('getValue');
    PRODUCT_PROC_TYPE_ID = $('#PRODUCT_PROC_TYPE_ID_SEARCH').combobox('getValue');
    ILLUSTRATION_ID = $('#ILLUSTRATION_ID').val();
    ILLUSTRATION_DESC = $('#ILLUSTRATION_DESC').val();
    var x = $('#Table_PROCESS_LIST').datagrid('getSelected');
    if (x == null) return;
    PROCESS_ID = x.PROCESS_ID;
    IMG_LENGTH = $('#IMG_LENGTH').val();
    if (ILLUSTRATION_ID == '' || ILLUSTRATION_DESC == '' || FACTORY_ID == '' || PRODUCT_TYPE_ID == '' || PRODUCT_PROC_TYPE_ID == '' || IMG_LENGTH == '') {
        $.messager.show({
            title: '消息',
            msg: '请填写',
            showType: 'show'
        });
        return;
    };
    var j = {
        'ILLUSTRATION_ID': ILLUSTRATION_ID,
        'FACTORY_ID': FACTORY_ID,
        'PRODUCT_TYPE_ID': PRODUCT_TYPE_ID,
        'PRODUCT_PROC_TYPE_ID': PRODUCT_PROC_TYPE_ID,
        'PROCESS_ID': PROCESS_ID,
        'ILLUSTRATION_DESC': ILLUSTRATION_DESC,
        'VALID_FLAG': $('#VALID_FLAG').val(),
        'IMG_LENGTH': $('#IMG_LENGTH').val(),
        'UPDATE_USER': $('#UPDATE_USER').val(),
        'UPDATE_DATE': $('#UPDATE_DATE').val(),
        'path': $('#url').val()
    };
    $.ajax({
        type: 'post',
        url: '/api/ILLUSTRATION_LIST/PostAdd',
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
                Init_Table_ILLUSTRATION_LIST();
                $('#Dialog_ILLUSTRATION_LIST').dialog('close');
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
function Edit() {
    if (!Validate_ILLUSTRATION_LIST()) {
        return;
    }
    var x = $('#Table_ILLUSTRATION_LIST').datagrid('getSelected');
    if (x == null) return;
    ILLUSTRATION_DESC = $('#ILLUSTRATION_DESC').val();
    IMG_LENGTH = $('#IMG_LENGTH').val();
    if (ILLUSTRATION_DESC == '' || IMG_LENGTH == '') {
        $.messager.show({
            title: '消息',
            msg: '请填写',
            showType: 'show'
        });
    };

    var j = {
        'ILLUSTRATION_ID': x.ILLUSTRATION_ID,
        'FACTORY_ID': x.FACTORY_ID,
        'PRODUCT_TYPE_ID': x.PRODUCT_TYPE_ID,
        'PRODUCT_PROC_TYPE_ID': x.PRODUCT_PROC_TYPE_ID,
        'PROCESS_ID': x.PROCESS_ID,
        'ILLUSTRATION_DESC': $('#ILLUSTRATION_DESC').val(),
        'VALID_FLAG': $('#VALID_FLAG').val(),
        'IMG_LENGTH': $('#IMG_LENGTH').val(),
        'UPDATE_USER': $('#UPDATE_USER').val(),
        'UPDATE_DATE': $('#UPDATE_DATE').val(),
        'path': $('#url').val()
    };
    $.ajax({
        type: 'post',
        url: '/api/ILLUSTRATION_LIST/PostEdit',
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
                Init_Table_ILLUSTRATION_LIST();
                $('#Dialog_ILLUSTRATION_LIST').dialog('close');
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
function Edit_Cell(row) {
    var j = {
        'ILLUSTRATION_ID': row.ILLUSTRATION_ID,
        'FACTORY_ID': row.FACTORY_ID,
        'PRODUCT_TYPE_ID': row.PRODUCT_TYPE_ID,
        'PRODUCT_PROC_TYPE_ID': row.PRODUCT_PROC_TYPE_ID,
        'UPDATE_USER': row.UPDATE_USER,
        'UPDATE_DATE': row.UPDATE_DATE,
        'PROCESS_ID': row.PROCESS_ID,
        'ILLUSTRATION_DESC': row.ILLUSTRATION_DESC,
        'ILLUSTRATION_DATA': row.ILLUSTRATION_DATA,
        'VALID_FLAG': row.VALID_FLAG,
        'IMG_LENGTH': row.IMG_LENGTH
    };
    $.ajax({
        type: 'post',
        url: '/api/ILLUSTRATION_LIST/PostEdit',
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
function Delete(row) {
    var j = {
        'ILLUSTRATION_ID': row.ILLUSTRATION_ID,
        'FACTORY_ID': row.FACTORY_ID,
        'PRODUCT_TYPE_ID': row.PRODUCT_TYPE_ID,
        'PRODUCT_PROC_TYPE_ID': row.PRODUCT_PROC_TYPE_ID
    };
    $.ajax({
        type: 'post',
        url: '/api/ILLUSTRATION_LIST/PostDelete',
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
                Init_Table_ILLUSTRATION_LIST();
                $('#showimg').html('');
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
function ILLUSTRATION_DATA() {
    $('#ILLUSTRATION_DATA').click(function () {
        if (!Validate_ILLUSTRATION_LIST()) {
            return;
        }
        FACTORY_ID = $('#FACTORY_ID_SEARCH').combobox('getValue');
        PRODUCT_TYPE_ID = $('#PRODUCT_TYPE_ID_SEARCH').combobox('getValue');
        PRODUCT_PROC_TYPE_ID = $('#PRODUCT_PROC_TYPE_ID_SEARCH').combobox('getValue');
        ILLUSTRATION_ID = $('#ILLUSTRATION_ID').val();
        ILLUSTRATION_DESC = $('#ILLUSTRATION_DESC').val();
        var x = $('#Table_PROCESS_LIST').datagrid('getSelected');
        if (x == null) return;
        PROCESS_ID = x.PROCESS_ID;
        IMG_LENGTH = $('#IMG_LENGTH').val();
        if (ILLUSTRATION_ID == '' || ILLUSTRATION_DESC == '' || FACTORY_ID == '' || PRODUCT_TYPE_ID == '' || PRODUCT_PROC_TYPE_ID == '' || IMG_LENGTH == '') {
            $.messager.show({
                title: '消息',
                msg: '请填写',
                showType: 'show'
            });
            return;
        };

        $('#url').val('');
        $('#Dialog_ILLUSTRATION_LIST_UploadImg').dialog('open');
    });
}

function InitCurrentFactoryId() {
    $.ajax({
        type: 'get',
        url: '/api/Users/GetCurrentUser',
        dataType: 'json',
        contentType: 'application/json',
        success: function (data) {
            if (data != null) {
                $('#FACTORY_ID_SEARCH').combobox('setValue', data.FACTORY_ID);
                Init_PRODUCT_TYPE_ID_SEARCH();
                $('#PRODUCT_TYPE_ID_SEARCH').combobox('setValue', 'CE');
                Init_PRODUCT_PROC_TYPE_ID_SEARCH();
                $('#PRODUCT_PROC_TYPE_ID_SEARCH').combobox('setValue', 'M6S');
                Init_Table_PROCESS_LIST();
            }
            else {
                $('#FACTORY_ID_SEARCH').combobox('clear');
                $('#PRODUCT_TYPE_ID_SEARCH').combobox('clear');
                $('#PRODUCT_PROC_TYPE_ID_SEARCH').combobox('clear');
                Table_PROCESS_LIST([]);
                Table_ILLUSTRATION_LIST([]);
            }
        }
    });
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
            Table_ILLUSTRATION_LIST([]);
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
            { field: 'PROCESS_ID', title: '编号', align: 'left', width: 60 },
                { field: 'PROCESS_DESC', title: '工序', align: 'left', width: 140 }
        ]],
        onClickRow: function (rowIndex, rowData) {
            Init_Table_ILLUSTRATION_LIST();
            $('#showimg').html('');
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

KindEditor.ready(function (K) {
    var editor = K.editor({
        allowFileManager: false
    });
    K('#image').click(function () {
        editor = K.editor({
            uploadJson: '/BaseInfo/ILLUSTRATION_LIST.ashx?ILLUSTRATION_ID=' + ILLUSTRATION_ID + '&FACTORY_ID=' + FACTORY_ID + '&PRODUCT_TYPE_ID=' + PRODUCT_TYPE_ID + '&PRODUCT_PROC_TYPE_ID=' + PRODUCT_PROC_TYPE_ID + '&ILLUSTRATION_DESC=' + $('#ILLUSTRATION_DESC').val() + '&IMG_LENGTH=' + $('#IMG_LENGTH').val() + '&VALID_FLAG=' + $('#VALID_FLAG').val() + '&PROCESS_ID=' + PROCESS_ID + '&addOrEdit=' + addOrEdit,
            allowFileManager: true
        });
        editor.loadPlugin('image', function () {
            editor.plugin.imageDialog({
                showRemote: false,
                imageUrl: K('#url').val(),
                clickFn: function (url, title, width, height, border, align) {
                    K('#url').val(url);
                    editor.hideDialog();
                    $('#Dialog_ILLUSTRATION_LIST_UploadImg').dialog('close');
                    $('#Dialog_ILLUSTRATION_LIST').dialog('close');
                    Init_Table_ILLUSTRATION_LIST();
                }
            });
        });
    });
});

function Table_ILLUSTRATION_PARAM_INFO_() {
    $('#Table_ILLUSTRATION_PARAM_INFO').datagrid({
        title: '图片参数',
        singleSelect: true,
        width: '600',
        height: '300',
        fitColumns: false,
        toolbar: [{
            text: '新增',
            iconCls: 'icon-add',
            handler: function () {

                var factoryId = $('#FACTORY_ID_SEARCH').combobox('getValue');
                var productTypeId = $('#PRODUCT_TYPE_ID_SEARCH').combobox('getValue');
                var produceProcTypeId = $('#PRODUCT_PROC_TYPE_ID_SEARCH').combobox('getValue');
                if (factoryId == '' || productTypeId == '' || produceProcTypeId == '') {
                    return;
                }

                addOrEdit_ILLUSTRATION_PARAM_INFO = 'add';
                PARAMETER_ID();
                $('#PARAMETER_ID').combobox('readonly', false);
                $('#PARAM_ORDER_NO').val('').attr('readonly', false);
                $('#TARGET').val('').attr('readonly', false);
                $('#USL').val('').attr('readonly', false);
                $('#LSL').val('').attr('readonly', false);
                $('#UPDATE_USER_ILLUSTRATION_PARAM_INFO').val('').attr('readonly', true);
                $('#UPDATE_DATE_ILLUSTRATION_PARAM_INFO').val('').attr('readonly', true);
                $('#Dialog_ILLUSTRATION_PARAM_INFO').dialog('open');
            }
        }, {
            text: '修改',
            iconCls: 'icon-edit',
            handler: function () {
                addOrEdit_ILLUSTRATION_PARAM_INFO = 'edit';
                var x = $('#Table_ILLUSTRATION_PARAM_INFO').datagrid('getSelected');
                if (x == null) return;

                var y = $('#Table_ILLUSTRATION_LIST').datagrid('getSelected');
                if (y == null) return;

                PARAMETER_ID();
                $('#PARAMETER_ID').combobox('setValue', x.PARAMETER_ID).combobox('readonly', true);
                $('#PARAM_ORDER_NO').val(x.PARAM_ORDER_NO);
                $('#TARGET').val(x.TARGET);
                $('#USL').val(x.USL);
                $('#LSL').val(x.LSL);
                $('#UPDATE_USER_ILLUSTRATION_PARAM_INFO').val(x.UPDATE_USER).attr('readonly', true);
                $('#UPDATE_DATE_ILLUSTRATION_PARAM_INFO').val(x.UPDATE_DATE).attr('readonly', true);
                $('#Dialog_ILLUSTRATION_PARAM_INFO').dialog('open');
            }
        }, {
            text: '删除',
            iconCls: 'icon-cancel',
            handler: function () {
                Deleterow_ILLUSTRATION_PARAM_INFO();
            }
        }, {
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                $('#Table_ILLUSTRATION_PARAM_INFO').datagrid('endEdit', editIndex_ILLUSTRATION_PARAM_INFO);
                var changedRow = $('#Table_ILLUSTRATION_PARAM_INFO').datagrid('getChanges');
                if (changedRow.length > 0) {
                    for (var i = 0; i < changedRow.length; i++) {
                        Edit_Cell_ILLUSTRATION_PARAM_INFO(changedRow[i]);
                    }
                }
                editIndex_ILLUSTRATION_PARAM_INFO = undefined;
                $('#Table_ILLUSTRATION_PARAM_INFO').datagrid('clearSelections');
            }
        }, {
            text: '取消',
            iconCls: 'icon-undo',
            handler: function () {
                $('#Table_ILLUSTRATION_PARAM_INFO').datagrid('rejectChanges');
            }
        }],
        columns: [[
            {
                field: 'PARAMETER_ID', title: '参数', align: 'left', style: function (value, row, index) { return 'color:blue'; },
                formatter: function (value, row, index) {
                    return (value == null ? '' : value) + " " + (row.PARAM_DESC == null ? '' : row.PARAM_DESC);
                }
            },
            {
                field: 'PARAM_ORDER_NO', title: '顺序', align: 'left',
                editor: {
                    type: 'validatebox',
                    options: {
                        required: false,
                        validType: 'number'
                    }
                }
            },
            {
                field: 'TARGET', title: '参数值', align: 'left',
                editor: {
                    type: 'validatebox',
                    options: {
                        required: false,
                        validType: 'number'
                    }
                }
            },
            {
                field: 'USL', title: '上限', align: 'left',
                editor: {
                    type: 'validatebox',
                    options: {
                        required: false,
                        validType: 'number'
                    }
                }
            },
            {
                field: 'LSL', title: '下限', align: 'left',
                editor: {
                    type: 'validatebox',
                    options: {
                        required: false,
                        validType: 'number'
                    }
                }
            },
            { field: 'UPDATE_USER', title: '最后更新者', align: 'left' },
            { field: 'UPDATE_DATE', title: '最后更新日期', align: 'left' }
        ]],
        onClickRow: function (rowIndex, rowData) {
            editrow_ILLUSTRATION_PARAM_INFO(rowIndex);
            Init_Table_PARAMETER_LIST_();
        },
        onDblClickRow: function (rowIndex, rowData) { },
        rowStyler: function (index, row) { },
        onBeforeEdit: function (index, row) {
            editIndex_ILLUSTRATION_PARAM_INFO = index;
            row.editing = true;
            $('#Table_ILLUSTRATION_PARAM_INFO').datagrid('refreshRow', index);
        },
        onAfterEdit: function (index, row) {
            row.editing = false;
            $('#Table_ILLUSTRATION_PARAM_INFO').datagrid('refreshRow', index);
        },
        onCancelEdit: function (index, row) {
            row.editing = false;
            $('#Table_ILLUSTRATION_PARAM_INFO').datagrid('refreshRow', index);
        }
    });
}
function Init_Table_ILLUSTRATION_PARAM_INFO_() {
    var x = $('#Table_ILLUSTRATION_LIST').datagrid('getSelected');
    if (x == null) return;
    var illustrationId = x.ILLUSTRATION_ID;

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
        'ILLUSTRATION_ID': illustrationId,
        'PRODUCT_TYPE_ID': productTypeId,
        'PRODUCT_PROC_TYPE_ID': produceProcTypeId,
        'FACTORY_ID': factoryId,
        'queryStr': ''
    };
    $.ajax({
        type: 'get',
        url: '/api/ILLUSTRATION_PARAM_INFO/GetDataByImgId',
        data: j,
        dataType: 'json',
        success: function (data) {
            $('#Table_ILLUSTRATION_PARAM_INFO').datagrid("loadData", data).datagrid('acceptChanges');
        }
    });
}
function editrow_ILLUSTRATION_PARAM_INFO(index) {
    if (editIndex_ILLUSTRATION_PARAM_INFO != undefined)
        $('#Table_ILLUSTRATION_PARAM_INFO').datagrid('endEdit', editIndex_ILLUSTRATION_PARAM_INFO);
    $('#Table_ILLUSTRATION_PARAM_INFO').datagrid('beginEdit', index).datagrid('selectRow', index);
}
function Deleterow_ILLUSTRATION_PARAM_INFO() {
    var row = $('#Table_ILLUSTRATION_PARAM_INFO').datagrid('getSelected');
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
            Delete_ILLUSTRATION_PARAM_INFO(row);
            $('#Table_ILLUSTRATION_PARAM_INFO').datagrid('deleteRow', $('#Table_ILLUSTRATION_PARAM_INFO').datagrid('getRowIndex'));
        }
    });
}
function Dialog_ILLUSTRATION_PARAM_INFO() {
    //选择框，勾选状态无法更改的解决办法：
    //$('#VALID_FLAG').click(
    //    function () {
    //        if ($('#VALID_FLAG').attr('checked') == 'checked') {
    //            $('#VALID_FLAG').attr('checked', false);
    //        } else {
    //            $('#VALID_FLAG').attr('checked', true);
    //        }
    //    });
    $('#Dialog_ILLUSTRATION_PARAM_INFO').dialog({
        toolbar: [{
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                if (addOrEdit_ILLUSTRATION_PARAM_INFO == 'add') {
                    Add_ILLUSTRATION_PARAM_INFO();
                }
                else if (addOrEdit_ILLUSTRATION_PARAM_INFO == 'edit') {
                    Edit_ILLUSTRATION_PARAM_INFO();
                }
            }
        }]
    });
}
function Validate_ILLUSTRATION_PARAM_INFO() {
    if (!(
        $('#PARAM_ORDER_NO').validatebox('isValid') &&
        $('#TARGET').validatebox('isValid') &&
        $('#USL').validatebox('isValid') &&
        $('#LSL').validatebox('isValid')
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
function Add_ILLUSTRATION_PARAM_INFO() {
    if (!Validate_ILLUSTRATION_PARAM_INFO()) {
        return;
    }
    var x = $('#Table_ILLUSTRATION_LIST').datagrid('getSelected');
    if (x == null) return;
    var illustrationId = x.ILLUSTRATION_ID;

    var factoryId = $('#FACTORY_ID_SEARCH').combobox('getValue');
    var productTypeId = $('#PRODUCT_TYPE_ID_SEARCH').combobox('getValue');
    var produceProcTypeId = $('#PRODUCT_PROC_TYPE_ID_SEARCH').combobox('getValue');

    var parameterId = $('#PARAMETER_ID').combobox('getValue');

    if (factoryId == "" ||
        productTypeId == "" ||
        produceProcTypeId == "" ||
        parameterId == ""
        ) {
        return;
    }
    var j = {
        'ILLUSTRATION_ID': illustrationId,
        'FACTORY_ID': factoryId,
        'PRODUCT_TYPE_ID': productTypeId,
        'PRODUCT_PROC_TYPE_ID': produceProcTypeId,
        'PARAMETER_ID': parameterId,
        'PARAM_ORDER_NO': $('#PARAM_ORDER_NO').val(),
        'TARGET': $('#TARGET').val(),
        'USL': $('#USL').val(),
        'LSL': $('#LSL').val(),
        'UPDATE_USER': $('#UPDATE_USER_ILLUSTRATION_PARAM_INFO').val(),
        'UPDATE_DATE': $('#UPDATE_DATE_ILLUSTRATION_PARAM_INFO').val()
    };

    $.ajax({
        type: 'post',
        url: '/api/ILLUSTRATION_PARAM_INFO/PostAdd',
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
                Init_Table_ILLUSTRATION_PARAM_INFO_();
                $('#Dialog_ILLUSTRATION_PARAM_INFO').dialog('close');
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
function Edit_ILLUSTRATION_PARAM_INFO() {
    if (!Validate_ILLUSTRATION_PARAM_INFO()) {
        return;
    }
    var x = $('#Table_ILLUSTRATION_LIST').datagrid('getSelected');
    if (x == null) return;
    var illustrationId = x.ILLUSTRATION_ID;

    var factoryId = $('#FACTORY_ID_SEARCH').combobox('getValue');
    var productTypeId = $('#PRODUCT_TYPE_ID_SEARCH').combobox('getValue');
    var produceProcTypeId = $('#PRODUCT_PROC_TYPE_ID_SEARCH').combobox('getValue');
    var parameterId = $('#PARAMETER_ID').combobox('getValue');

    if (factoryId == "" ||
        productTypeId == "" ||
        produceProcTypeId == "" ||
        parameterId == ""
        ) {
        return;
    }
    var j = {
        'ILLUSTRATION_ID': illustrationId,
        'FACTORY_ID': factoryId,
        'PRODUCT_TYPE_ID': productTypeId,
        'PRODUCT_PROC_TYPE_ID': produceProcTypeId,
        'PARAMETER_ID': parameterId,
        'PARAM_ORDER_NO': $('#PARAM_ORDER_NO').val(),
        'TARGET': $('#TARGET').val(),
        'USL': $('#USL').val(),
        'LSL': $('#LSL').val(),
        'UPDATE_USER': $('#UPDATE_USER_ILLUSTRATION_PARAM_INFO').val(),
        'UPDATE_DATE': $('#UPDATE_DATE_ILLUSTRATION_PARAM_INFO').val()
    };
    $.ajax({
        type: 'post',
        url: '/api/ILLUSTRATION_PARAM_INFO/PostEdit',
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
                Init_Table_ILLUSTRATION_PARAM_INFO_();
                $('#Dialog_ILLUSTRATION_PARAM_INFO').dialog('close');
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
function Edit_Cell_ILLUSTRATION_PARAM_INFO(row) {
    var j = {
        'ILLUSTRATION_ID': row.ILLUSTRATION_ID,
        'FACTORY_ID': row.FACTORY_ID,
        'PRODUCT_TYPE_ID': row.PRODUCT_TYPE_ID,
        'PRODUCT_PROC_TYPE_ID': row.PRODUCT_PROC_TYPE_ID,
        'PARAMETER_ID': row.PARAMETER_ID,
        'PARAM_ORDER_NO': row.PARAM_ORDER_NO,
        'TARGET': row.TARGET,
        'USL': row.USL,
        'LSL': row.LSL,
        'UPDATE_USER': row.UPDATE_USER,
        'UPDATE_DATE': row.UPDATE_DATE
    };
    $.ajax({
        type: 'post',
        url: '/api/ILLUSTRATION_PARAM_INFO/PostEdit',
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
function Delete_ILLUSTRATION_PARAM_INFO(row) {
    var j = {
        'ILLUSTRATION_ID': row.ILLUSTRATION_ID,
        'PARAMETER_ID': row.PARAMETER_ID,
        'PRODUCT_TYPE_ID': row.PRODUCT_TYPE_ID,
        'PRODUCT_PROC_TYPE_ID': row.PRODUCT_PROC_TYPE_ID,
        'FACTORY_ID': row.FACTORY_ID
    };
    $.ajax({
        type: 'post',
        url: '/api/ILLUSTRATION_PARAM_INFO/PostDelete',
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
                Init_Table_ILLUSTRATION_PARAM_INFO(1, pageSize);
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

    $('#PARAMETER_ID').combobox({
        panelHeight: 200,
        valueField: 'PARAMETER_ID',
        textField: 'PARAM_DESC',
        multiple: false,
        method: 'get',
        url: '/api/PROCESS_PARAM_INFO/GetDataByProcessIdQuery?FACTORY_ID=' + factoryId + '&PRODUCT_TYPE_ID=' + productTypeId + '&PRODUCT_PROC_TYPE_ID=' + produceProcTypeId + '&PROCESS_ID=' + processId + '&queryStr=' + "AND IS_ILLUSTRATION_PARAM='1'",
        editable: false,
        onSelect: function (record) {
        }
    });
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
            { field: 'PARAM_DESC', title: '参数', align: 'left', width: 100 },
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
    var x = $('#Table_ILLUSTRATION_PARAM_INFO').datagrid('getSelected');
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