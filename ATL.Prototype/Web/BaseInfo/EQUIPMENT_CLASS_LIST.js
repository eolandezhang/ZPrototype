var pageSize = 20;
var editIndex_EQUIPMENT_CLASS_LIST = undefined;
var addOrEdit_EQUIPMENT_CLASS_LIST = null;
$(function () {
    //Table_EQUIPMENT_CLASS_LIST_();
    //Init_Table_EQUIPMENT_CLASS_LIST_();
    Table_EQUIPMENT_CLASS_LIST(1, pageSize)
    Init_Table_EQUIPMENT_CLASS_LIST(1, pageSize);
    Dialog_EQUIPMENT_CLASS_LIST();
});
//不分页
function Table_EQUIPMENT_CLASS_LIST_() {
    $('#Table_EQUIPMENT_CLASS_LIST').datagrid({
        title: 'EQUIPMENT_CLASS_LIST',
        singleSelect: true,
        width: '840',
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
                addOrEdit_EQUIPMENT_CLASS_LIST = 'add';
                $('#EQUIPMENT_CLASS_ID').val('').attr('readonly', false);
                $('#FACTORY_ID').val('').attr('readonly', false);
                $('#UPDATE_USER').val('').attr('readonly', false);
                $('#UPDATE_DATE').val('').attr('readonly', false);
                $('#EQUIPMENT_CLASS_NAME').val('').attr('readonly', false);
                $('#EQUIPMENT_CLASS_DESC').val('').attr('readonly', false);
                $('#CLASSIFIED_BY').val('').attr('readonly', false);
                $('#VALID_FLAG').val('').attr('readonly', false);
                $('#UPDATE_USER').attr('readonly', true);
                $('#UPDATE_DATE').attr('readonly', true);
                $('#Dialog_EQUIPMENT_CLASS_LIST').dialog('open');
            }
        }, {
            text: '修改',
            iconCls: 'icon-edit',
            handler: function () {
                addOrEdit_EQUIPMENT_CLASS_LIST = 'edit';
                var x = $('#Table_EQUIPMENT_CLASS_LIST').datagrid('getSelected');
                if (x == null) return;
                //范例：
                //$('#PACKAGE_TYPE_ID').val(x.PACKAGE_TYPE_ID).attr('readonly', true);
                //$('#FACTORY_ID').combobox('setValue',x.FACTORY_ID).combobox('readonly', true);
                //$('#VALID_FLAG').attr('checked', x.VALID_FLAG == 1 ? true : false);               
                $('#EQUIPMENT_CLASS_ID').val(x.EQUIPMENT_CLASS_ID).attr('readonly', true);
                $('#FACTORY_ID').val(x.FACTORY_ID).attr('readonly', true);
                $('#UPDATE_USER').val(x.UPDATE_USER);
                $('#UPDATE_DATE').val(x.UPDATE_DATE);
                $('#EQUIPMENT_CLASS_NAME').val(x.EQUIPMENT_CLASS_NAME);
                $('#EQUIPMENT_CLASS_DESC').val(x.EQUIPMENT_CLASS_DESC);
                $('#CLASSIFIED_BY').val(x.CLASSIFIED_BY);
                $('#VALID_FLAG').val(x.VALID_FLAG);
                $('#UPDATE_USER').attr('readonly', true);
                $('#UPDATE_DATE').attr('readonly', true);
                $('#Dialog_EQUIPMENT_CLASS_LIST').dialog('open');
            }
        }, {
            text: '删除',
            iconCls: 'icon-cancel',
            handler: function () {
                Deleterow_EQUIPMENT_CLASS_LIST();
            }
        }, {
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                $('#Table_EQUIPMENT_CLASS_LIST').datagrid('endEdit', editIndex_EQUIPMENT_CLASS_LIST);
                var changedRow = $('#Table_EQUIPMENT_CLASS_LIST').datagrid('getChanges');
                if (changedRow.length > 0) {
                    for (var i = 0; i < changedRow.length; i++) {
                        Edit_Cell_EQUIPMENT_CLASS_LIST(changedRow[i]);
                    }
                }
                editIndex_EQUIPMENT_CLASS_LIST = undefined;
                $('#Table_EQUIPMENT_CLASS_LIST').datagrid('clearSelections');
            }
        }, {
            text: '取消',
            iconCls: 'icon-undo',
            handler: function () {
                $('#Table_EQUIPMENT_CLASS_LIST').datagrid('rejectChanges');
            }
        }],
        frozenColumns: [[
            { field: 'EQUIPMENT_CLASS_ID', title: 'EQUIPMENT_CLASS_ID', align: 'left', styler: function (value, row, index) { return 'color:blue'; } },
            { field: 'FACTORY_ID', title: 'FACTORY_ID', align: 'left', styler: function (value, row, index) { return 'color:blue'; } }
        ]],
        columns: [[
            { field: 'UPDATE_USER', title: 'UPDATE_USER', align: 'left' },
            { field: 'UPDATE_DATE', title: 'UPDATE_DATE', align: 'left' },
            { field: 'EQUIPMENT_CLASS_NAME', title: 'EQUIPMENT_CLASS_NAME', align: 'left', editor: 'text' },
            { field: 'EQUIPMENT_CLASS_DESC', title: 'EQUIPMENT_CLASS_DESC', align: 'left', editor: 'text' },
            { field: 'CLASSIFIED_BY', title: 'CLASSIFIED_BY', align: 'left', editor: 'text' },
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
            editrow_EQUIPMENT_CLASS_LIST(rowIndex);
        },
        onDblClickRow: function (rowIndex, rowData) { },
        rowStyler: function (index, row) { },
        onBeforeEdit: function (index, row) {
            editIndex_EQUIPMENT_CLASS_LIST = index;
            row.editing = true;
            $('#Table_EQUIPMENT_CLASS_LIST').datagrid('refreshRow', index);
        },
        onAfterEdit: function (index, row) {
            row.editing = false;
            $('#Table_EQUIPMENT_CLASS_LIST').datagrid('refreshRow', index);
        },
        onCancelEdit: function (index, row) {
            row.editing = false;
            $('#Table_EQUIPMENT_CLASS_LIST').datagrid('refreshRow', index);
        }
    });
}
function Init_Table_EQUIPMENT_CLASS_LIST_() {
    $.ajax({
        type: 'get',
        url: '/api/EQUIPMENT_CLASS_LIST/GetData',
        data: {},
        dataType: 'json',
        success: function (data) {
            $('#Table_EQUIPMENT_CLASS_LIST').datagrid("loadData", data).datagrid('acceptChanges');
        }
    });
}
//分页
function Table_EQUIPMENT_CLASS_LIST(pageNumber, pageSize) {
    $('#Table_EQUIPMENT_CLASS_LIST').datagrid({
        title: 'EQUIPMENT_CLASS_LIST',
        singleSelect: true,
        width: '840',
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
                addOrEdit_EQUIPMENT_CLASS_LIST = 'add';
                $('#EQUIPMENT_CLASS_ID').val('').attr('readonly', false);
                $('#FACTORY_ID').val('').attr('readonly', false);
                $('#UPDATE_USER').val('').attr('readonly', false);
                $('#UPDATE_DATE').val('').attr('readonly', false);
                $('#EQUIPMENT_CLASS_NAME').val('').attr('readonly', false);
                $('#EQUIPMENT_CLASS_DESC').val('').attr('readonly', false);
                $('#CLASSIFIED_BY').val('').attr('readonly', false);
                $('#VALID_FLAG').val('').attr('readonly', false);
                $('#UPDATE_USER').attr('readonly', true);
                $('#UPDATE_DATE').attr('readonly', true);
                $('#Dialog_EQUIPMENT_CLASS_LIST').dialog('open');
            }
        }, {
            text: '修改',
            iconCls: 'icon-edit',
            handler: function () {
                addOrEdit_EQUIPMENT_CLASS_LIST = 'edit';
                var x = $('#Table_EQUIPMENT_CLASS_LIST').datagrid('getSelected');
                if (x == null) return;
                //范例：
                //$('#PACKAGE_TYPE_ID').val(x.PACKAGE_TYPE_ID).attr('readonly', true);
                //$('#FACTORY_ID').combobox('setValue',x.FACTORY_ID).combobox('readonly', true);
                //$('#VALID_FLAG').attr('checked', x.VALID_FLAG == 1 ? true : false);               
                $('#EQUIPMENT_CLASS_ID').val(x.EQUIPMENT_CLASS_ID).attr('readonly', true);
                $('#FACTORY_ID').val(x.FACTORY_ID).attr('readonly', true);
                $('#UPDATE_USER').val(x.UPDATE_USER);
                $('#UPDATE_DATE').val(x.UPDATE_DATE);
                $('#EQUIPMENT_CLASS_NAME').val(x.EQUIPMENT_CLASS_NAME);
                $('#EQUIPMENT_CLASS_DESC').val(x.EQUIPMENT_CLASS_DESC);
                $('#CLASSIFIED_BY').val(x.CLASSIFIED_BY);
                $('#VALID_FLAG').val(x.VALID_FLAG);
                $('#UPDATE_USER').attr('readonly', true);
                $('#UPDATE_DATE').attr('readonly', true);
                $('#Dialog_EQUIPMENT_CLASS_LIST').dialog('open');
            }
        }, {
            text: '删除',
            iconCls: 'icon-cancel',
            handler: function () {
                Deleterow_EQUIPMENT_CLASS_LIST();
            }
        }, {
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                $('#Table_EQUIPMENT_CLASS_LIST').datagrid('endEdit', editIndex_EQUIPMENT_CLASS_LIST);
                var changedRow = $('#Table_EQUIPMENT_CLASS_LIST').datagrid('getChanges');
                if (changedRow.length > 0) {
                    for (var i = 0; i < changedRow.length; i++) {
                        Edit_Cell_EQUIPMENT_CLASS_LIST(changedRow[i]);
                    }
                }
                editIndex_EQUIPMENT_CLASS_LIST = undefined;
                $('#Table_EQUIPMENT_CLASS_LIST').datagrid('clearSelections');
            }
        }, {
            text: '取消',
            iconCls: 'icon-undo',
            handler: function () {
                $('#Table_EQUIPMENT_CLASS_LIST').datagrid('rejectChanges');
            }
        }],
        rownumbers: true,
        pagination: true,
        pageNumber: pageNumber,
        pageSize: pageSize,
        pageList: [pageSize, pageSize * 2, pageSize * 3],
        method: 'get',
        //sortName: 'USER_NUM', //单页排序的列
        //sortOrder: 'asc', //升序
        //remoteSort: false, //使用客户端排序
        showFooter: false,
        //idField: 'USER_NAME',
        frozenColumns: [[
            { field: 'EQUIPMENT_CLASS_ID', title: 'EQUIPMENT_CLASS_ID', align: 'left', styler: function (value, row, index) { return 'color:blue'; } },
            { field: 'FACTORY_ID', title: 'FACTORY_ID', align: 'left', styler: function (value, row, index) { return 'color:blue'; } }
        ]],
        columns: [[

            {
                field: 'EQUIPMENT_CLASS_NAME', title: 'EQUIPMENT_CLASS_NAME', align: 'left', editor: 'text',
                editor: {
                    type: 'validatebox',
                    options: {
                        required: false,
                        validType: ['maxLength[20]']
                    }
                }
            },
            {
                field: 'EQUIPMENT_CLASS_DESC', title: 'EQUIPMENT_CLASS_DESC', align: 'left', editor: 'text',
                editor: {
                    type: 'validatebox',
                    options: {
                        required: false,
                        validType: ['maxLength[25]']
                    }
                }
            },
            {
                field: 'CLASSIFIED_BY', title: 'CLASSIFIED_BY', align: 'left', editor: 'text',
                editor: {
                    type: 'validatebox',
                    options: {
                        required: false,
                        validType: ['maxLength[50]']
                    }
                }
            },
            {
                field: 'VALID_FLAG', title: 'VALID_FLAG', align: 'left', editor: { type: 'checkbox', options: { on: '1', off: '0' } },
                formatter: function (value, row, index) {
                    switch (value) {
                        case '0':
                            return '';
                        case '1':
                            return '是';
                    }
                }
            },
            { field: 'UPDATE_USER', title: 'UPDATE_USER', align: 'left' },
            { field: 'UPDATE_DATE', title: 'UPDATE_DATE', align: 'left' }
        ]],
        onClickRow: function (rowIndex, rowData) {
            editrow_EQUIPMENT_CLASS_LIST(rowIndex);
        },
        onDblClickRow: function (rowIndex, rowData) { },
        rowStyler: function (index, row) { },
        onBeforeEdit: function (index, row) {
            editIndex_EQUIPMENT_CLASS_LIST = index;
            row.editing = true;
            $('#Table_EQUIPMENT_CLASS_LIST').datagrid('refreshRow', index);
        },
        onAfterEdit: function (index, row) {
            row.editing = false;
            $('#Table_EQUIPMENT_CLASS_LIST').datagrid('refreshRow', index);
        },
        onCancelEdit: function (index, row) {
            row.editing = false;
            $('#Table_EQUIPMENT_CLASS_LIST').datagrid('refreshRow', index);
        }
    });
    var pg = $('#Table_EQUIPMENT_CLASS_LIST').datagrid("getPager");
    if (pg) {
        $(pg).pagination({
            onSelectPage: function (pageNumber, pageSize) {
                Init_Table_EQUIPMENT_CLASS_LIST(pageNumber, pageSize); //翻页的关键点
            }
        });
    }
}
function Init_Table_EQUIPMENT_CLASS_LIST(pageNumber, pageSize) {
    $.ajax({
        type: 'get',
        url: '/api/EQUIPMENT_CLASS_LIST/GetDataPage',
        data: { 'pageSize': pageSize, 'pageNumber': pageNumber },
        dataType: 'json',
        success: function (data) {
            if (data.length != 0) {
                var str = { total: data[0].TOTAL, rows: data };
            } else {
                var str = { total: 0, rows: data };
            }
            $('#Table_EQUIPMENT_CLASS_LIST').datagrid("loadData", str).datagrid('acceptChanges');
        }
    });
}

//function opendetail_EQUIPMENT_CLASS_LIST(index) {
//    $('#Table_PACKAGE_BASE_INFO').datagrid('selectRow', index);
//    var row = $('#Table_PACKAGE_BASE_INFO').datagrid('getSelected');
//    window.open('/Package/PackageDetails.aspx?PACKAGE_NO=' + row.PACKAGE_NO + '&FACTORY_ID=' + row.FACTORY_ID + '&VERSION_NO=' + row.VERSION_NO, '_blank');
//}
//function cancelrow_EQUIPMENT_CLASS_LIST(index) {
//    $('#Table_PACKAGE_BASE_INFO').datagrid('cancelEdit', index);
//}
function editrow_EQUIPMENT_CLASS_LIST(index) {
    if (editIndex_EQUIPMENT_CLASS_LIST != undefined)
        $('#Table_EQUIPMENT_CLASS_LIST').datagrid('endEdit', editIndex_EQUIPMENT_CLASS_LIST);
    $('#Table_EQUIPMENT_CLASS_LIST').datagrid('beginEdit', index).datagrid('selectRow', index);
}
function Deleterow_EQUIPMENT_CLASS_LIST() {
    var row = $('#Table_EQUIPMENT_CLASS_LIST').datagrid('getSelected');
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
            Delete_EQUIPMENT_CLASS_LIST(row);
        }
    });
}
function Dialog_EQUIPMENT_CLASS_LIST() {
    //选择框，勾选状态无法更改的解决办法：
    //$('#VALID_FLAG').click(
    //    function () {
    //        if ($('#VALID_FLAG').attr('checked') == 'checked') {
    //            $('#VALID_FLAG').attr('checked', false);
    //        } else {
    //            $('#VALID_FLAG').attr('checked', true);
    //        }
    //    });
    $('#Dialog_EQUIPMENT_CLASS_LIST').dialog({
        toolbar: [{
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                if (addOrEdit_EQUIPMENT_CLASS_LIST == 'add') {
                    Add_EQUIPMENT_CLASS_LIST();
                }
                else if (addOrEdit_EQUIPMENT_CLASS_LIST == 'edit') {
                    Edit_EQUIPMENT_CLASS_LIST();
                }
            }
        }]
    });
}
function Validate_EQUIPMENT_CLASS_LIST() {
    if (!(
        $('#EQUIPMENT_CLASS_ID').validatebox('isValid') &&
        $('#FACTORY_ID').validatebox('isValid') &&
        $('#UPDATE_USER').validatebox('isValid') &&
        $('#UPDATE_DATE').validatebox('isValid') &&
        $('#EQUIPMENT_CLASS_NAME').validatebox('isValid') &&
        $('#EQUIPMENT_CLASS_DESC').validatebox('isValid') &&
        $('#CLASSIFIED_BY').validatebox('isValid') &&
        $('#VALID_FLAG').validatebox('isValid')
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
function Add_EQUIPMENT_CLASS_LIST() {
    //if (!Validate_EQUIPMENT_CLASS_LIST()) {
    //    return;
    //}
    if ($('#EQUIPMENT_CLASS_ID').val() == '' && $('#FACTORY_ID').val() == '') {
        $.messager.show({
            title: '消息',
            msg: '请填写',
            showType: 'show'
        });
        return;
    }
    var j = {
        'EQUIPMENT_CLASS_ID': $('#EQUIPMENT_CLASS_ID').val(),
        'FACTORY_ID': $('#FACTORY_ID').val(),
        'UPDATE_USER': $('#UPDATE_USER').val(),
        'UPDATE_DATE': $('#UPDATE_DATE').val(),
        'EQUIPMENT_CLASS_NAME': $('#EQUIPMENT_CLASS_NAME').val(),
        'EQUIPMENT_CLASS_DESC': $('#EQUIPMENT_CLASS_DESC').val(),
        'CLASSIFIED_BY': $('#CLASSIFIED_BY').val(),
        'VALID_FLAG': $('#VALID_FLAG').val()
    };
    $.ajax({
        type: 'post',
        url: '/api/EQUIPMENT_CLASS_LIST/PostAdd',
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
                //Init_Table_EQUIPMENT_CLASS_LIST_();
                Init_Table_EQUIPMENT_CLASS_LIST(1, pageSize);
                $('#Dialog_EQUIPMENT_CLASS_LIST').dialog('close');
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
function Edit_EQUIPMENT_CLASS_LIST() {
    //if (!Validate_EQUIPMENT_CLASS_LIST()) {
    //    return;
    //}
    if ($('#EQUIPMENT_CLASS_ID').val() == '' && $('#FACTORY_ID').val() == '') {
        $.messager.show({
            title: '消息',
            msg: '请填写',
            showType: 'show'
        });
        return;
    }
    //范例：
    //'FACTORY_ID':$('#FACTORY_ID').combobox('getValue'),  
    //'VALID_FLAG': $('#VALID_FLAG').attr('checked') == 'checked' ? 1 : 0,
    var j = {
        'EQUIPMENT_CLASS_ID': $('#EQUIPMENT_CLASS_ID').val(),
        'FACTORY_ID': $('#FACTORY_ID').val(),
        'UPDATE_USER': $('#UPDATE_USER').val(),
        'UPDATE_DATE': $('#UPDATE_DATE').val(),
        'EQUIPMENT_CLASS_NAME': $('#EQUIPMENT_CLASS_NAME').val(),
        'EQUIPMENT_CLASS_DESC': $('#EQUIPMENT_CLASS_DESC').val(),
        'CLASSIFIED_BY': $('#CLASSIFIED_BY').val(),
        'VALID_FLAG': $('#VALID_FLAG').val()
    };
    $.ajax({
        type: 'post',
        url: '/api/EQUIPMENT_CLASS_LIST/PostEdit',
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
                //Init_Table_EQUIPMENT_CLASS_LIST_();
                Init_Table_EQUIPMENT_CLASS_LIST(1, pageSize);
                $('#Dialog_EQUIPMENT_CLASS_LIST').dialog('close');
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
function Edit_Cell_EQUIPMENT_CLASS_LIST(row) {
    var j = {
        'EQUIPMENT_CLASS_ID': row.EQUIPMENT_CLASS_ID,
        'FACTORY_ID': row.FACTORY_ID,
        'UPDATE_USER': row.UPDATE_USER,
        'UPDATE_DATE': row.UPDATE_DATE,
        'EQUIPMENT_CLASS_NAME': row.EQUIPMENT_CLASS_NAME,
        'EQUIPMENT_CLASS_DESC': row.EQUIPMENT_CLASS_DESC,
        'CLASSIFIED_BY': row.CLASSIFIED_BY,
        'VALID_FLAG': row.VALID_FLAG
    };
    $.ajax({
        type: 'post',
        url: '/api/EQUIPMENT_CLASS_LIST/PostEdit',
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
function Delete_EQUIPMENT_CLASS_LIST(row) {
    var j = {
        'EQUIPMENT_CLASS_ID': row.EQUIPMENT_CLASS_ID,
        'FACTORY_ID': row.FACTORY_ID
    };
    $.ajax({
        type: 'post',
        url: '/api/EQUIPMENT_CLASS_LIST/PostDelete',
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
                //Init_Table_EQUIPMENT_CLASS_LIST_();
                Init_Table_EQUIPMENT_CLASS_LIST(1, pageSize);
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