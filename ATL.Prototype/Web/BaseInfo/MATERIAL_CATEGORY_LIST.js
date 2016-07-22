var pageSize = 20;
var editIndex_MATERIAL_CATEGORY_LIST = undefined;
var addOrEdit_MATERIAL_CATEGORY_LIST = null;
$(function () {
    //Table_MATERIAL_CATEGORY_LIST_();
    //Init_Table_MATERIAL_CATEGORY_LIST_();
    Table_MATERIAL_CATEGORY_LIST(1, pageSize)
    Init_Table_MATERIAL_CATEGORY_LIST(1, pageSize);
    Dialog_MATERIAL_CATEGORY_LIST();
});
//不分页
function Table_MATERIAL_CATEGORY_LIST_() {
    $('#Table_MATERIAL_CATEGORY_LIST').datagrid({
        title: 'MATERIAL_CATEGORY_LIST',
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
                addOrEdit_MATERIAL_CATEGORY_LIST = 'add';
                $('#MATERIAL_CATEGORY_ID').val('').attr('readonly', false);
                $('#FACTORY_ID').val('').attr('readonly', false);
                $('#PRODUCT_TYPE_ID').val('').attr('readonly', false);
                $('#PRODUCT_PROC_TYPE_ID').val('').attr('readonly', false);
                $('#UPDATE_USER').val('').attr('readonly', false);
                $('#UPDATE_DATE').val('').attr('readonly', false);
                $('#AORC').val('').attr('readonly', false);
                $('#MATERIAL_CATEGORY_NAME').val('').attr('readonly', false);
                $('#MATERIAL_CATEGORY_DESC').val('').attr('readonly', false);
                $('#VALID_FLAG').val('').attr('readonly', false);
                $('#UPDATE_USER').attr('readonly', true);
                $('#UPDATE_DATE').attr('readonly', true);
                $('#Dialog_MATERIAL_CATEGORY_LIST').dialog('open');
            }
        }, {
            text: '修改',
            iconCls: 'icon-edit',
            handler: function () {
                addOrEdit_MATERIAL_CATEGORY_LIST = 'edit';
                var x = $('#Table_MATERIAL_CATEGORY_LIST').datagrid('getSelected');
                if (x == null) return;
                //范例：
                //$('#PACKAGE_TYPE_ID').val(x.PACKAGE_TYPE_ID).attr('readonly', true);
                //$('#FACTORY_ID').combobox('setValue',x.FACTORY_ID).combobox('readonly', true);
                //$('#VALID_FLAG').attr('checked', x.VALID_FLAG == 1 ? true : false);               
                $('#MATERIAL_CATEGORY_ID').val(x.MATERIAL_CATEGORY_ID).attr('readonly', true);
                $('#FACTORY_ID').val(x.FACTORY_ID).attr('readonly', true);
                $('#PRODUCT_TYPE_ID').val(x.PRODUCT_TYPE_ID).attr('readonly', true);
                $('#PRODUCT_PROC_TYPE_ID').val(x.PRODUCT_PROC_TYPE_ID).attr('readonly', true);
                $('#UPDATE_USER').val(x.UPDATE_USER);
                $('#UPDATE_DATE').val(x.UPDATE_DATE);
                $('#AORC').val(x.AORC);
                $('#MATERIAL_CATEGORY_NAME').val(x.MATERIAL_CATEGORY_NAME);
                $('#MATERIAL_CATEGORY_DESC').val(x.MATERIAL_CATEGORY_DESC);
                $('#VALID_FLAG').val(x.VALID_FLAG);
                $('#UPDATE_USER').attr('readonly', true);
                $('#UPDATE_DATE').attr('readonly', true);
                $('#Dialog_MATERIAL_CATEGORY_LIST').dialog('open');
            }
        }, {
            text: '删除',
            iconCls: 'icon-cancel',
            handler: function () {
                Deleterow_MATERIAL_CATEGORY_LIST();
            }
        }, {
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                $('#Table_MATERIAL_CATEGORY_LIST').datagrid('endEdit', editIndex_MATERIAL_CATEGORY_LIST);
                var changedRow = $('#Table_MATERIAL_CATEGORY_LIST').datagrid('getChanges');
                if (changedRow.length > 0) {
                    for (var i = 0; i < changedRow.length; i++) {
                        Edit_Cell_MATERIAL_CATEGORY_LIST(changedRow[i]);
                    }
                }
                editIndex_MATERIAL_CATEGORY_LIST = undefined;
                $('#Table_MATERIAL_CATEGORY_LIST').datagrid('clearSelections');
            }
        }, {
            text: '取消',
            iconCls: 'icon-undo',
            handler: function () {
                $('#Table_MATERIAL_CATEGORY_LIST').datagrid('rejectChanges');
            }
        }],
        frozenColumns: [[
            { field: 'MATERIAL_CATEGORY_ID', title: 'MATERIAL_CATEGORY_ID', align: 'left', styler: function (value, row, index) { return 'color:blue'; } },
            { field: 'FACTORY_ID', title: 'FACTORY_ID', align: 'left', styler: function (value, row, index) { return 'color:blue'; } },
            { field: 'PRODUCT_TYPE_ID', title: 'PRODUCT_TYPE_ID', align: 'left', styler: function (value, row, index) { return 'color:blue'; } },
            { field: 'PRODUCT_PROC_TYPE_ID', title: 'PRODUCT_PROC_TYPE_ID', align: 'left', styler: function (value, row, index) { return 'color:blue'; } }
        ]],
        columns: [[            
            { field: 'AORC', title: 'AORC', align: 'left', editor: 'text' },
            { field: 'MATERIAL_CATEGORY_NAME', title: 'MATERIAL_CATEGORY_NAME', align: 'left', editor: 'text' },
            { field: 'MATERIAL_CATEGORY_DESC', title: 'MATERIAL_CATEGORY_DESC', align: 'left', editor: 'text' },
            { field: 'UPDATE_USER', title: 'UPDATE_USER', align: 'left' },
            { field: 'UPDATE_DATE', title: 'UPDATE_DATE', align: 'left' },
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
            editrow_MATERIAL_CATEGORY_LIST(rowIndex);
        },
        onDblClickRow: function (rowIndex, rowData) { },
        rowStyler: function (index, row) { },
        onBeforeEdit: function (index, row) {
            editIndex_MATERIAL_CATEGORY_LIST = index;
            row.editing = true;
            $('#Table_MATERIAL_CATEGORY_LIST').datagrid('refreshRow', index);
        },
        onAfterEdit: function (index, row) {
            row.editing = false;
            $('#Table_MATERIAL_CATEGORY_LIST').datagrid('refreshRow', index);
        },
        onCancelEdit: function (index, row) {
            row.editing = false;
            $('#Table_MATERIAL_CATEGORY_LIST').datagrid('refreshRow', index);
        }
    });
}
function Init_Table_MATERIAL_CATEGORY_LIST_() {
    $.ajax({
        type: 'get',
        url: '/api/MATERIAL_CATEGORY_LIST/GetData',
        data: {},
        dataType: 'json',
        success: function (data) {
            $('#Table_MATERIAL_CATEGORY_LIST').datagrid("loadData", data).datagrid('acceptChanges');
        }
    });
}
//分页
function Table_MATERIAL_CATEGORY_LIST(pageNumber, pageSize) {
    $('#Table_MATERIAL_CATEGORY_LIST').datagrid({
        title: 'MATERIAL_CATEGORY_LIST',
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
                addOrEdit_MATERIAL_CATEGORY_LIST = 'add';
                $('#MATERIAL_CATEGORY_ID').val('').attr('readonly', false);
                $('#FACTORY_ID').val('').attr('readonly', false);
                $('#PRODUCT_TYPE_ID').val('').attr('readonly', false);
                $('#PRODUCT_PROC_TYPE_ID').val('').attr('readonly', false);
                $('#UPDATE_USER').val('').attr('readonly', false);
                $('#UPDATE_DATE').val('').attr('readonly', false);
                $('#AORC').val('').attr('readonly', false);
                $('#MATERIAL_CATEGORY_NAME').val('').attr('readonly', false);
                $('#MATERIAL_CATEGORY_DESC').val('').attr('readonly', false);
                $('#VALID_FLAG').val('').attr('readonly', false);
                $('#UPDATE_USER').attr('readonly', true);
                $('#UPDATE_DATE').attr('readonly', true);
                $('#Dialog_MATERIAL_CATEGORY_LIST').dialog('open');
            }
        }, {
            text: '修改',
            iconCls: 'icon-edit',
            handler: function () {
                addOrEdit_MATERIAL_CATEGORY_LIST = 'edit';
                var x = $('#Table_MATERIAL_CATEGORY_LIST').datagrid('getSelected');
                if (x == null) return;
                //范例：
                //$('#PACKAGE_TYPE_ID').val(x.PACKAGE_TYPE_ID).attr('readonly', true);
                //$('#FACTORY_ID').combobox('setValue',x.FACTORY_ID).combobox('readonly', true);
                //$('#VALID_FLAG').attr('checked', x.VALID_FLAG == 1 ? true : false);               
                $('#MATERIAL_CATEGORY_ID').val(x.MATERIAL_CATEGORY_ID).attr('readonly', true);
                $('#FACTORY_ID').val(x.FACTORY_ID).attr('readonly', true);
                $('#PRODUCT_TYPE_ID').val(x.PRODUCT_TYPE_ID).attr('readonly', true);
                $('#PRODUCT_PROC_TYPE_ID').val(x.PRODUCT_PROC_TYPE_ID).attr('readonly', true);
                $('#UPDATE_USER').val(x.UPDATE_USER);
                $('#UPDATE_DATE').val(x.UPDATE_DATE);
                $('#AORC').val(x.AORC);
                $('#MATERIAL_CATEGORY_NAME').val(x.MATERIAL_CATEGORY_NAME);
                $('#MATERIAL_CATEGORY_DESC').val(x.MATERIAL_CATEGORY_DESC);
                $('#VALID_FLAG').val(x.VALID_FLAG);
                $('#UPDATE_USER').attr('readonly', true);
                $('#UPDATE_DATE').attr('readonly', true);
                $('#Dialog_MATERIAL_CATEGORY_LIST').dialog('open');
            }
        }, {
            text: '删除',
            iconCls: 'icon-cancel',
            handler: function () {
                Deleterow_MATERIAL_CATEGORY_LIST();
            }
        }, {
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                $('#Table_MATERIAL_CATEGORY_LIST').datagrid('endEdit', editIndex_MATERIAL_CATEGORY_LIST);
                var changedRow = $('#Table_MATERIAL_CATEGORY_LIST').datagrid('getChanges');
                if (changedRow.length > 0) {
                    for (var i = 0; i < changedRow.length; i++) {
                        Edit_Cell_MATERIAL_CATEGORY_LIST(changedRow[i]);
                    }
                }
                editIndex_MATERIAL_CATEGORY_LIST = undefined;
                $('#Table_MATERIAL_CATEGORY_LIST').datagrid('clearSelections');
            }
        }, {
            text: '取消',
            iconCls: 'icon-undo',
            handler: function () {
                $('#Table_MATERIAL_CATEGORY_LIST').datagrid('rejectChanges');
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
            //        var openstr = '<a style=\"cursor:pointer\"  onclick="opendetail_MATERIAL_CATEGORY_LIST(' + index + ')">打开</a>&nbsp;';
            //       if (row.editing) {
            //            var c = '<a style=\"cursor:pointer\"  onclick="cancelrow_MATERIAL_CATEGORY_LIST(' + index + ')">取消</a>';
            //            return openstr + c;
            //        } else {
            //            var e = '<a style=\"cursor:pointer\" onclick="editrow_MATERIAL_CATEGORY_LIST(' + index + ')">编辑</a> ';
            //            return openstr + e;
            //        }
            //    }
            //},
            { field: 'MATERIAL_CATEGORY_ID', title: 'MATERIAL_CATEGORY_ID', align: 'left', styler: function (value, row, index) { return 'color:blue'; } },
            { field: 'FACTORY_ID', title: 'FACTORY_ID', align: 'left', styler: function (value, row, index) { return 'color:blue'; } },
            { field: 'PRODUCT_TYPE_ID', title: 'PRODUCT_TYPE_ID', align: 'left', styler: function (value, row, index) { return 'color:blue'; } },
            { field: 'PRODUCT_PROC_TYPE_ID', title: 'PRODUCT_PROC_TYPE_ID', align: 'left', styler: function (value, row, index) { return 'color:blue'; } }
        ]],
        columns: [[            
            { field: 'AORC', title: 'AORC', align: 'left', editor: 'text' },
            { field: 'MATERIAL_CATEGORY_NAME', title: 'MATERIAL_CATEGORY_NAME', align: 'left', editor: 'text' },
            { field: 'MATERIAL_CATEGORY_DESC', title: 'MATERIAL_CATEGORY_DESC', align: 'left', editor: 'text' },
            { field: 'UPDATE_USER', title: 'UPDATE_USER', align: 'left' },
            { field: 'UPDATE_DATE', title: 'UPDATE_DATE', align: 'left' },
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
            editrow_MATERIAL_CATEGORY_LIST(rowIndex);
        },
        onDblClickRow: function (rowIndex, rowData) { },
        rowStyler: function (index, row) { },
        onBeforeEdit: function (index, row) {
            editIndex_MATERIAL_CATEGORY_LIST = index;
            row.editing = true;
            $('#Table_MATERIAL_CATEGORY_LIST').datagrid('refreshRow', index);
        },
        onAfterEdit: function (index, row) {
            row.editing = false;
            $('#Table_MATERIAL_CATEGORY_LIST').datagrid('refreshRow', index);
        },
        onCancelEdit: function (index, row) {
            row.editing = false;
            $('#Table_MATERIAL_CATEGORY_LIST').datagrid('refreshRow', index);
        }
    });
    var pg = $('#Table_MATERIAL_CATEGORY_LIST').datagrid("getPager");
    if (pg) {
        $(pg).pagination({
            onSelectPage: function (pageNumber, pageSize) {
                Init_Table_MATERIAL_CATEGORY_LIST(pageNumber, pageSize); //翻页的关键点
            }
        });
    }
}

function editrow_MATERIAL_CATEGORY_LIST(index) {
    if (editIndex_MATERIAL_CATEGORY_LIST != undefined)
        $('#Table_MATERIAL_CATEGORY_LIST').datagrid('endEdit', editIndex_MATERIAL_CATEGORY_LIST);
    $('#Table_MATERIAL_CATEGORY_LIST').datagrid('beginEdit', index).datagrid('selectRow', index);
}
function Deleterow_MATERIAL_CATEGORY_LIST() {
    var row = $('#Table_MATERIAL_CATEGORY_LIST').datagrid('getSelected');
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
            Delete_MATERIAL_CATEGORY_LIST(row);
            $('#Table_MATERIAL_CATEGORY_LIST').datagrid('deleteRow', $('#Table_MATERIAL_CATEGORY_LIST').datagrid('getRowIndex'));
        }
    });
}
function Init_Table_MATERIAL_CATEGORY_LIST(pageNumber, pageSize) {
    $.ajax({
        type: 'get',
        url: '/api/MATERIAL_CATEGORY_LIST/GetDataPage',
        data: { 'pageSize': pageSize, 'pageNumber': pageNumber },
        dataType: 'json',
        success: function (data) {
            if (data.length != 0) {
                var str = { total: data[0].TOTAL, rows: data };
            } else {
                var str = { total: 0, rows: data };
            }
            $('#Table_MATERIAL_CATEGORY_LIST').datagrid("loadData", str).datagrid('acceptChanges');
        }
    });
}
function Dialog_MATERIAL_CATEGORY_LIST() {
    //选择框，勾选状态无法更改的解决办法：
    //$('#VALID_FLAG').click(
    //    function () {
    //        if ($('#VALID_FLAG').attr('checked') == 'checked') {
    //            $('#VALID_FLAG').attr('checked', false);
    //        } else {
    //            $('#VALID_FLAG').attr('checked', true);
    //        }
    //    });
    $('#Dialog_MATERIAL_CATEGORY_LIST').dialog({
        toolbar: [{
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                if (addOrEdit_MATERIAL_CATEGORY_LIST == 'add') {
                    Add_MATERIAL_CATEGORY_LIST();
                }
                else if (addOrEdit_MATERIAL_CATEGORY_LIST == 'edit') {
                    Edit_MATERIAL_CATEGORY_LIST();
                }
            }
        }]
    });
}
function Add_MATERIAL_CATEGORY_LIST() {
    if ($('#MATERIAL_CATEGORY_ID').val() == '' && $('#FACTORY_ID').val() == '' && $('#PRODUCT_TYPE_ID').val() == '' && $('#PRODUCT_PROC_TYPE_ID').val() == '') {
        $.messager.show({
            title: '消息',
            msg: '请填写',
            showType: 'show'
        });
        return;
    }
    var j = {
        'MATERIAL_CATEGORY_ID': $('#MATERIAL_CATEGORY_ID').val(),
        'FACTORY_ID': $('#FACTORY_ID').val(),
        'PRODUCT_TYPE_ID': $('#PRODUCT_TYPE_ID').val(),
        'PRODUCT_PROC_TYPE_ID': $('#PRODUCT_PROC_TYPE_ID').val(),
        'UPDATE_USER': $('#UPDATE_USER').val(),
        'UPDATE_DATE': $('#UPDATE_DATE').val(),
        'AORC': $('#AORC').val(),
        'MATERIAL_CATEGORY_NAME': $('#MATERIAL_CATEGORY_NAME').val(),
        'MATERIAL_CATEGORY_DESC': $('#MATERIAL_CATEGORY_DESC').val(),
        'VALID_FLAG': $('#VALID_FLAG').val()
    };
    $.ajax({
        type: 'post',
        url: '/api/MATERIAL_CATEGORY_LIST/PostAdd',
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
                Init_Table_MATERIAL_CATEGORY_LIST(1, pageSize);
                $('#Dialog_MATERIAL_CATEGORY_LIST').dialog('close');
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
function Edit_MATERIAL_CATEGORY_LIST() {
    if ($('#MATERIAL_CATEGORY_ID').val() == '' && $('#FACTORY_ID').val() == '' && $('#PRODUCT_TYPE_ID').val() == '' && $('#PRODUCT_PROC_TYPE_ID').val() == '') {
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
        'MATERIAL_CATEGORY_ID': $('#MATERIAL_CATEGORY_ID').val(),
        'FACTORY_ID': $('#FACTORY_ID').val(),
        'PRODUCT_TYPE_ID': $('#PRODUCT_TYPE_ID').val(),
        'PRODUCT_PROC_TYPE_ID': $('#PRODUCT_PROC_TYPE_ID').val(),
        'UPDATE_USER': $('#UPDATE_USER').val(),
        'UPDATE_DATE': $('#UPDATE_DATE').val(),
        'AORC': $('#AORC').val(),
        'MATERIAL_CATEGORY_NAME': $('#MATERIAL_CATEGORY_NAME').val(),
        'MATERIAL_CATEGORY_DESC': $('#MATERIAL_CATEGORY_DESC').val(),
        'VALID_FLAG': $('#VALID_FLAG').val()
    };
    $.ajax({
        type: 'post',
        url: '/api/MATERIAL_CATEGORY_LIST/PostEdit',
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
                Init_Table_MATERIAL_CATEGORY_LIST(1, pageSize);
                $('#Dialog_MATERIAL_CATEGORY_LIST').dialog('close');
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
function Edit_Cell_MATERIAL_CATEGORY_LIST(row) {
    var j = {
        'MATERIAL_CATEGORY_ID': row.MATERIAL_CATEGORY_ID,
        'FACTORY_ID': row.FACTORY_ID,
        'PRODUCT_TYPE_ID': row.PRODUCT_TYPE_ID,
        'PRODUCT_PROC_TYPE_ID': row.PRODUCT_PROC_TYPE_ID,
        'UPDATE_USER': row.UPDATE_USER,
        'UPDATE_DATE': row.UPDATE_DATE,
        'AORC': row.AORC,
        'MATERIAL_CATEGORY_NAME': row.MATERIAL_CATEGORY_NAME,
        'MATERIAL_CATEGORY_DESC': row.MATERIAL_CATEGORY_DESC,
        'VALID_FLAG': row.VALID_FLAG
    };
    $.ajax({
        type: 'post',
        url: '/api/MATERIAL_CATEGORY_LIST/PostEdit',
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
function Delete_MATERIAL_CATEGORY_LIST(row) {
    var j = {
        'MATERIAL_CATEGORY_ID': row.MATERIAL_CATEGORY_ID,
        'FACTORY_ID': row.FACTORY_ID,
        'PRODUCT_TYPE_ID': row.PRODUCT_TYPE_ID,
        'PRODUCT_PROC_TYPE_ID': row.PRODUCT_PROC_TYPE_ID
    };
    $.ajax({
        type: 'post',
        url: '/api/MATERIAL_CATEGORY_LIST/PostDelete',
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
                Init_Table_MATERIAL_CATEGORY_LIST(1, pageSize);
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