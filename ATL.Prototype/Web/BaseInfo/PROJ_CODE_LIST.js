var pageSize = 20;
var editIndex_PROJ_CODE_LIST = undefined;
var addOrEdit_PROJ_CODE_LIST = null;
$(function () {
    //Table_PROJ_CODE_LIST_();
    //Init_Table_PROJ_CODE_LIST_();
    Table_PROJ_CODE_LIST(1, pageSize)
    Init_Table_PROJ_CODE_LIST(1, pageSize);
    Dialog_PROJ_CODE_LIST();
});
//不分页
function Table_PROJ_CODE_LIST_() {
    $('#Table_PROJ_CODE_LIST').datagrid({
        title: 'PROJ_CODE_LIST',
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
                addOrEdit_PROJ_CODE_LIST = 'add';
                $('#PROJ_CODE_ID').val('').attr('readonly', false);
                $('#FACTORY_ID').val('').attr('readonly', false);
                $('#PRODUCT_TYPE_ID').val('').attr('readonly', false);
                $('#PRODUCT_PROC_TYPE_ID').val('').attr('readonly', false);
                $('#UPDATE_USER').val('').attr('readonly', false);
                $('#UPDATE_DATE').val('').attr('readonly', false);
                $('#PROJ_CODE_NAME').val('').attr('readonly', false);
                $('#PROJ_CODE_DESC').val('').attr('readonly', false);
                $('#VALID_FLAG').val('').attr('readonly', false);
                $('#UPDATE_USER').attr('readonly', true);
                $('#UPDATE_DATE').attr('readonly', true);
                $('#Dialog_PROJ_CODE_LIST').dialog('open');
            }
        }, {
            text: '修改',
            iconCls: 'icon-edit',
            handler: function () {
                addOrEdit_PROJ_CODE_LIST = 'edit';
                var x = $('#Table_PROJ_CODE_LIST').datagrid('getSelected');
                if (x == null) return;
                //范例：
                //$('#PACKAGE_TYPE_ID').val(x.PACKAGE_TYPE_ID).attr('readonly', true);
                //$('#FACTORY_ID').combobox('setValue',x.FACTORY_ID).combobox('readonly', true);
                //$('#VALID_FLAG').attr('checked', x.VALID_FLAG == 1 ? true : false);               
                $('#PROJ_CODE_ID').val(x.PROJ_CODE_ID).attr('readonly', true);
                $('#FACTORY_ID').val(x.FACTORY_ID).attr('readonly', true);
                $('#PRODUCT_TYPE_ID').val(x.PRODUCT_TYPE_ID).attr('readonly', true);
                $('#PRODUCT_PROC_TYPE_ID').val(x.PRODUCT_PROC_TYPE_ID).attr('readonly', true);
                $('#UPDATE_USER').val(x.UPDATE_USER);
                $('#UPDATE_DATE').val(x.UPDATE_DATE);
                $('#PROJ_CODE_NAME').val(x.PROJ_CODE_NAME);
                $('#PROJ_CODE_DESC').val(x.PROJ_CODE_DESC);
                $('#VALID_FLAG').val(x.VALID_FLAG);
                $('#UPDATE_USER').attr('readonly', true);
                $('#UPDATE_DATE').attr('readonly', true);
                $('#Dialog_PROJ_CODE_LIST').dialog('open');
            }
        }, {
            text: '删除',
            iconCls: 'icon-cancel',
            handler: function () {
                Deleterow_PROJ_CODE_LIST();
            }
        }, {
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                $('#Table_PROJ_CODE_LIST').datagrid('endEdit', editIndex_PROJ_CODE_LIST);
                var changedRow = $('#Table_PROJ_CODE_LIST').datagrid('getChanges');
                if (changedRow.length > 0) {
                    for (var i = 0; i < changedRow.length; i++) {
                        Edit_Cell_PROJ_CODE_LIST(changedRow[i]);
                    }
                }
                editIndex_PROJ_CODE_LIST = undefined;
                $('#Table_PROJ_CODE_LIST').datagrid('clearSelections');
            }
        }, {
            text: '取消',
            iconCls: 'icon-undo',
            handler: function () {
                $('#Table_PROJ_CODE_LIST').datagrid('rejectChanges');
            }
        }],
        frozenColumns: [[
            { field: 'PROJ_CODE_ID', title: 'PROJ_CODE_ID', align: 'left', styler: function (value, row, index) { return 'color:blue'; } },
            { field: 'FACTORY_ID', title: 'FACTORY_ID', align: 'left', styler: function (value, row, index) { return 'color:blue'; } },
            { field: 'PRODUCT_TYPE_ID', title: 'PRODUCT_TYPE_ID', align: 'left', styler: function (value, row, index) { return 'color:blue'; } },
            { field: 'PRODUCT_PROC_TYPE_ID', title: 'PRODUCT_PROC_TYPE_ID', align: 'left', styler: function (value, row, index) { return 'color:blue'; } }
        ]],
        columns: [[
            { field: 'UPDATE_USER', title: 'UPDATE_USER', align: 'left' },
            { field: 'UPDATE_DATE', title: 'UPDATE_DATE', align: 'left' },
            { field: 'PROJ_CODE_NAME', title: 'PROJ_CODE_NAME', align: 'left', editor: 'text' },
            { field: 'PROJ_CODE_DESC', title: 'PROJ_CODE_DESC', align: 'left', editor: 'text' },
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
        ]],
        onClickRow: function (rowIndex, rowData) {
            editrow_PROJ_CODE_LIST(rowIndex);
        },
        onDblClickRow: function (rowIndex, rowData) { },
        rowStyler: function (index, row) { },
        onBeforeEdit: function (index, row) {
            editIndex_PROJ_CODE_LIST = index;
            row.editing = true;
            $('#Table_PROJ_CODE_LIST').datagrid('refreshRow', index);
        },
        onAfterEdit: function (index, row) {
            row.editing = false;
            $('#Table_PROJ_CODE_LIST').datagrid('refreshRow', index);
        },
        onCancelEdit: function (index, row) {
            row.editing = false;
            $('#Table_PROJ_CODE_LIST').datagrid('refreshRow', index);
        }
    });
}
function Init_Table_PROJ_CODE_LIST_() {
    $.ajax({
        type: 'get',
        url: '/api/PROJ_CODE_LIST/GetData',
        data: {},
        dataType: 'json',
        success: function (data) {
            $('#Table_PROJ_CODE_LIST').datagrid("loadData", data).datagrid('acceptChanges');
        }
    });
}
//分页
function Table_PROJ_CODE_LIST(pageNumber, pageSize) {
    $('#Table_PROJ_CODE_LIST').datagrid({
        title: 'PROJ_CODE_LIST',
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
                addOrEdit_PROJ_CODE_LIST = 'add';
                $('#PROJ_CODE_ID').val('').attr('readonly', false);
                $('#FACTORY_ID').val('').attr('readonly', false);
                $('#PRODUCT_TYPE_ID').val('').attr('readonly', false);
                $('#PRODUCT_PROC_TYPE_ID').val('').attr('readonly', false);
                $('#UPDATE_USER').val('').attr('readonly', false);
                $('#UPDATE_DATE').val('').attr('readonly', false);
                $('#PROJ_CODE_NAME').val('').attr('readonly', false);
                $('#PROJ_CODE_DESC').val('').attr('readonly', false);
                $('#VALID_FLAG').val('').attr('readonly', false);
                $('#UPDATE_USER').attr('readonly', true);
                $('#UPDATE_DATE').attr('readonly', true);
                $('#Dialog_PROJ_CODE_LIST').dialog('open');
            }
        }, {
            text: '修改',
            iconCls: 'icon-edit',
            handler: function () {
                addOrEdit_PROJ_CODE_LIST = 'edit';
                var x = $('#Table_PROJ_CODE_LIST').datagrid('getSelected');
                if (x == null) return;
                //范例：
                //$('#PACKAGE_TYPE_ID').val(x.PACKAGE_TYPE_ID).attr('readonly', true);
                //$('#FACTORY_ID').combobox('setValue',x.FACTORY_ID).combobox('readonly', true);
                //$('#VALID_FLAG').attr('checked', x.VALID_FLAG == 1 ? true : false);               
                $('#PROJ_CODE_ID').val(x.PROJ_CODE_ID).attr('readonly', true);
                $('#FACTORY_ID').val(x.FACTORY_ID).attr('readonly', true);
                $('#PRODUCT_TYPE_ID').val(x.PRODUCT_TYPE_ID).attr('readonly', true);
                $('#PRODUCT_PROC_TYPE_ID').val(x.PRODUCT_PROC_TYPE_ID).attr('readonly', true);
                $('#UPDATE_USER').val(x.UPDATE_USER);
                $('#UPDATE_DATE').val(x.UPDATE_DATE);
                $('#PROJ_CODE_NAME').val(x.PROJ_CODE_NAME);
                $('#PROJ_CODE_DESC').val(x.PROJ_CODE_DESC);
                $('#VALID_FLAG').val(x.VALID_FLAG);
                $('#UPDATE_USER').attr('readonly', true);
                $('#UPDATE_DATE').attr('readonly', true);
                $('#Dialog_PROJ_CODE_LIST').dialog('open');
            }
        }, {
            text: '删除',
            iconCls: 'icon-cancel',
            handler: function () {
                Deleterow_PROJ_CODE_LIST();
            }
        }, {
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                $('#Table_PROJ_CODE_LIST').datagrid('endEdit', editIndex_PROJ_CODE_LIST);
                var changedRow = $('#Table_PROJ_CODE_LIST').datagrid('getChanges');
                if (changedRow.length > 0) {
                    for (var i = 0; i < changedRow.length; i++) {
                        Edit_Cell_PROJ_CODE_LIST(changedRow[i]);
                    }
                }
                editIndex_PROJ_CODE_LIST = undefined;
                $('#Table_PROJ_CODE_LIST').datagrid('clearSelections');
            }
        }, {
            text: '取消',
            iconCls: 'icon-undo',
            handler: function () {
                $('#Table_PROJ_CODE_LIST').datagrid('rejectChanges');
            }
        }],
        rownumbers: true, //显示行号        
        pagination: true, //显示翻页工具栏
        pageNumber: pageNumber, //重点:传入当前页数
        pageSize: pageSize, //重点:传入每一页的大小
        pageList: [pageSize, pageSize * 2, pageSize * 3], //可以调整每一页的大小
        method: 'get', //默认值为post,根据需要
        //sortName: 'USER_NUM', //单页排序的列
        //sortOrder: 'asc', //升序
        //remoteSort: false, //使用客户端排序
        showFooter: false,
        //idField: 'USER_NAME',
        frozenColumns: [[
            //{
            //    field: 'ACTION',
            //    formatter: function (value, row, index) {                   
            //        var openstr = '<a style=\"cursor:pointer\"  onclick="opendetail_PROJ_CODE_LIST(' + index + ')">打开</a>&nbsp;';
            //       if (row.editing) {
            //            var c = '<a style=\"cursor:pointer\"  onclick="cancelrow_PROJ_CODE_LIST(' + index + ')">取消</a>';
            //            return openstr + c;
            //        } else {
            //            var e = '<a style=\"cursor:pointer\" onclick="editrow_PROJ_CODE_LIST(' + index + ')">编辑</a> ';
            //            return openstr + e;
            //        }
            //    }
            //},
            { field: 'PROJ_CODE_ID', title: 'PROJ_CODE_ID', align: 'left', styler: function (value, row, index) { return 'color:blue'; } },
            { field: 'FACTORY_ID', title: 'FACTORY_ID', align: 'left', styler: function (value, row, index) { return 'color:blue'; } },
            { field: 'PRODUCT_TYPE_ID', title: 'PRODUCT_TYPE_ID', align: 'left', styler: function (value, row, index) { return 'color:blue'; } },
            { field: 'PRODUCT_PROC_TYPE_ID', title: 'PRODUCT_PROC_TYPE_ID', align: 'left', styler: function (value, row, index) { return 'color:blue'; } }
        ]],
        columns: [[
            { field: 'UPDATE_USER', title: 'UPDATE_USER', align: 'left' },
            { field: 'UPDATE_DATE', title: 'UPDATE_DATE', align: 'left' },
            { field: 'PROJ_CODE_NAME', title: 'PROJ_CODE_NAME', align: 'left', editor: 'text' },
            { field: 'PROJ_CODE_DESC', title: 'PROJ_CODE_DESC', align: 'left', editor: 'text' },
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
        ]],
        onClickRow: function (rowIndex, rowData) {
            editrow_PROJ_CODE_LIST(rowIndex);
        },
        onDblClickRow: function (rowIndex, rowData) { },
        rowStyler: function (index, row) { },
        onBeforeEdit: function (index, row) {
            editIndex_PROJ_CODE_LIST = index;
            row.editing = true;
            $('#Table_PROJ_CODE_LIST').datagrid('refreshRow', index);
        },
        onAfterEdit: function (index, row) {
            row.editing = false;
            $('#Table_PROJ_CODE_LIST').datagrid('refreshRow', index);
        },
        onCancelEdit: function (index, row) {
            row.editing = false;
            $('#Table_PROJ_CODE_LIST').datagrid('refreshRow', index);
        }
    });
    var pg = $('#Table_PROJ_CODE_LIST').datagrid("getPager");
    if (pg) {
        $(pg).pagination({
            onSelectPage: function (pageNumber, pageSize) {
                Init_Table_PROJ_CODE_LIST(pageNumber, pageSize); //翻页的关键点
            }
        });
    }
}
//function opendetail_PROJ_CODE_LIST(index) {
//    $('#Table_PACKAGE_BASE_INFO').datagrid('selectRow', index);
//    var row = $('#Table_PACKAGE_BASE_INFO').datagrid('getSelected');
//    window.open('/Package/PackageDetails.aspx?PACKAGE_NO=' + row.PACKAGE_NO + '&FACTORY_ID=' + row.FACTORY_ID + '&VERSION_NO=' + row.VERSION_NO, '_blank');
//}
//function cancelrow_PROJ_CODE_LIST(index) {
//    $('#Table_PACKAGE_BASE_INFO').datagrid('cancelEdit', index);
//}
function editrow_PROJ_CODE_LIST(index) {
    if (editIndex_PROJ_CODE_LIST != undefined)
        $('#Table_PROJ_CODE_LIST').datagrid('endEdit', editIndex_PROJ_CODE_LIST);
    $('#Table_PROJ_CODE_LIST').datagrid('beginEdit', index).datagrid('selectRow', index);
}
function Deleterow_PROJ_CODE_LIST() {
    var row = $('#Table_PROJ_CODE_LIST').datagrid('getSelected');
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
            Delete_PROJ_CODE_LIST(row);
            $('#Table_PROJ_CODE_LIST').datagrid('deleteRow', $('#Table_PROJ_CODE_LIST').datagrid('getRowIndex'));
        }
    });
}
function Init_Table_PROJ_CODE_LIST(pageNumber, pageSize) {
    $.ajax({
        type: 'get',
        url: '/api/PROJ_CODE_LIST/GetDataPage',
        data: { 'pageSize': pageSize, 'pageNumber': pageNumber },
        dataType: 'json',
        success: function (data) {
            if (data.length != 0) {
                var str = { total: data[0].TOTAL, rows: data };
            } else {
                var str = { total: 0, rows: data };
            }
            $('#Table_PROJ_CODE_LIST').datagrid("loadData", str).datagrid('acceptChanges');
        }
    });
}
function Dialog_PROJ_CODE_LIST() {
    //选择框，勾选状态无法更改的解决办法：
    //$('#VALID_FLAG').click(
    //    function () {
    //        if ($('#VALID_FLAG').attr('checked') == 'checked') {
    //            $('#VALID_FLAG').attr('checked', false);
    //        } else {
    //            $('#VALID_FLAG').attr('checked', true);
    //        }
    //    });
    $('#Dialog_PROJ_CODE_LIST').dialog({
        toolbar: [{
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                if (addOrEdit_PROJ_CODE_LIST == 'add') {
                    Add_PROJ_CODE_LIST();
                }
                else if (addOrEdit_PROJ_CODE_LIST == 'edit') {
                    Edit_PROJ_CODE_LIST();
                }
            }
        }]
    });
}
function Add_PROJ_CODE_LIST() {
    if ($('#PROJ_CODE_ID').val() == '' && $('#FACTORY_ID').val() == '' && $('#PRODUCT_TYPE_ID').val() == '' && $('#PRODUCT_PROC_TYPE_ID').val() == '') {
        $.messager.show({
            title: '消息',
            msg: '请填写',
            showType: 'show'
        });
        return;
    }
    var j = {
        'PROJ_CODE_ID': $('#PROJ_CODE_ID').val(),
        'FACTORY_ID': $('#FACTORY_ID').val(),
        'PRODUCT_TYPE_ID': $('#PRODUCT_TYPE_ID').val(),
        'PRODUCT_PROC_TYPE_ID': $('#PRODUCT_PROC_TYPE_ID').val(),
        'UPDATE_USER': $('#UPDATE_USER').val(),
        'UPDATE_DATE': $('#UPDATE_DATE').val(),
        'PROJ_CODE_NAME': $('#PROJ_CODE_NAME').val(),
        'PROJ_CODE_DESC': $('#PROJ_CODE_DESC').val(),
        'VALID_FLAG': $('#VALID_FLAG').val()
    };
    $.ajax({
        type: 'post',
        url: '/api/PROJ_CODE_LIST/PostAdd',
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
                Init_Table_PROJ_CODE_LIST(1, pageSize);
                $('#Dialog_PROJ_CODE_LIST').dialog('close');
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
function Edit_PROJ_CODE_LIST() {
    if ($('#PROJ_CODE_ID').val() == '' && $('#FACTORY_ID').val() == '' && $('#PRODUCT_TYPE_ID').val() == '' && $('#PRODUCT_PROC_TYPE_ID').val() == '') {
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
        'PROJ_CODE_ID': $('#PROJ_CODE_ID').val(),
        'FACTORY_ID': $('#FACTORY_ID').val(),
        'PRODUCT_TYPE_ID': $('#PRODUCT_TYPE_ID').val(),
        'PRODUCT_PROC_TYPE_ID': $('#PRODUCT_PROC_TYPE_ID').val(),
        'UPDATE_USER': $('#UPDATE_USER').val(),
        'UPDATE_DATE': $('#UPDATE_DATE').val(),
        'PROJ_CODE_NAME': $('#PROJ_CODE_NAME').val(),
        'PROJ_CODE_DESC': $('#PROJ_CODE_DESC').val(),
        'VALID_FLAG': $('#VALID_FLAG').val()
    };
    $.ajax({
        type: 'post',
        url: '/api/PROJ_CODE_LIST/PostEdit',
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
                Init_Table_PROJ_CODE_LIST(1, pageSize);
                $('#Dialog_PROJ_CODE_LIST').dialog('close');
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
function Edit_Cell_PROJ_CODE_LIST(row) {
    var j = {
        'PROJ_CODE_ID': row.PROJ_CODE_ID,
        'FACTORY_ID': row.FACTORY_ID,
        'PRODUCT_TYPE_ID': row.PRODUCT_TYPE_ID,
        'PRODUCT_PROC_TYPE_ID': row.PRODUCT_PROC_TYPE_ID,
        'UPDATE_USER': row.UPDATE_USER,
        'UPDATE_DATE': row.UPDATE_DATE,
        'PROJ_CODE_NAME': row.PROJ_CODE_NAME,
        'PROJ_CODE_DESC': row.PROJ_CODE_DESC,
        'VALID_FLAG': row.VALID_FLAG
    };
    $.ajax({
        type: 'post',
        url: '/api/PROJ_CODE_LIST/PostEdit',
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
function Delete_PROJ_CODE_LIST(row) {
    var j = {
        'PROJ_CODE_ID': row.PROJ_CODE_ID,
        'FACTORY_ID': row.FACTORY_ID,
        'PRODUCT_TYPE_ID': row.PRODUCT_TYPE_ID,
        'PRODUCT_PROC_TYPE_ID': row.PRODUCT_PROC_TYPE_ID
    };
    $.ajax({
        type: 'post',
        url: '/api/PROJ_CODE_LIST/PostDelete',
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
                Init_Table_PROJ_CODE_LIST(1, pageSize);
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