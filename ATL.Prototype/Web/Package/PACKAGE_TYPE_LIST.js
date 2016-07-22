var pageSize = 20;
var editIndex_PACKAGE_TYPE_LIST = undefined;
var addOrEdit_PACKAGE_TYPE_LIST = null;
$(function () {
    Init_Table_PACKAGE_TYPE_LIST(1, pageSize);  
    Dialog_PACKAGE_TYPE_LIST();

});
function Table_PACKAGE_TYPE_LIST(data, pageSize, pageNumber) {
    if (data.length != 0) {
        var str = { total: data[0].TOTAL, rows: data };
    } else {
        var str = { total: 0, rows: data };
    }
    $('#Table_PACKAGE_TYPE_LIST').datagrid({
        title: 'PACKAGE_TYPE_LIST',
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
                addOrEdit_PACKAGE_TYPE_LIST = 'add';
                $('#PACKAGE_TYPE_ID').val('').attr('readonly', false);
                $('#FACTORY_ID').val('').attr('readonly', false);
                $('#PACKAGE_TYPE_DESC').val('').attr('readonly', false);
                $('#PACKAGE_CODE').val('').attr('readonly', false);
                $('#VALID_FLAG').val('').attr('readonly', false);
                $('#UPDATE_USER').val('').attr('readonly', false);
                $('#UPDATE_DATE').val('').attr('readonly', false);
                $('#GROUPS_LIMIT').val('').attr('readonly', false);
                $('#UPDATE_USER').attr('readonly', true);
                $('#UPDATE_DATE').attr('readonly', true);
                $('#Dialog_PACKAGE_TYPE_LIST').dialog('open');
            }
        }, {
            text: '修改',
            iconCls: 'icon-edit',
            handler: function () {
                addOrEdit_PACKAGE_TYPE_LIST = 'edit';
                var x = $('#Table_PACKAGE_TYPE_LIST').datagrid('getSelected');
                if (x == null) return;
                //范例：
                //$('#PACKAGE_TYPE_ID').val(x.PACKAGE_TYPE_ID).attr('readonly', true);
                //$('#FACTORY_ID').combobox('setValue',x.FACTORY_ID).combobox('readonly', true);
                //$('#VALID_FLAG').attr('checked', x.VALID_FLAG == 1 ? true : false);               
                $('#PACKAGE_TYPE_ID').val(x.PACKAGE_TYPE_ID).attr('readonly', true);
                $('#FACTORY_ID').val(x.FACTORY_ID).attr('readonly', true);
                $('#PACKAGE_TYPE_DESC').val(x.PACKAGE_TYPE_DESC);
                $('#PACKAGE_CODE').val(x.PACKAGE_CODE);
                $('#VALID_FLAG').val(x.VALID_FLAG);
                $('#UPDATE_USER').val(x.UPDATE_USER);
                $('#UPDATE_DATE').val(x.UPDATE_DATE);
                $('#GROUPS_LIMIT').val(x.GROUPS_LIMIT);
                $('#UPDATE_USER').attr('readonly', true);
                $('#UPDATE_DATE').attr('readonly', true);
                $('#Dialog_PACKAGE_TYPE_LIST').dialog('open');
            }
        }, {
            text: '删除',
            iconCls: 'icon-cancel',
            handler: function () {
                Deleterow_PACKAGE_TYPE_LIST();
            }
        }, {
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                $('#Table_PACKAGE_TYPE_LIST').datagrid('endEdit', editIndex_PACKAGE_TYPE_LIST);
                var changedRow = $('#Table_PACKAGE_TYPE_LIST').datagrid('getChanges');
                if (changedRow.length > 0) {
                    for (var i = 0; i < changedRow.length; i++) {
                        Edit_Cell_PACKAGE_TYPE_LIST(changedRow[i]);
                    }
                }
                editIndex_PACKAGE_TYPE_LIST = undefined;
                $('#Table_PACKAGE_TYPE_LIST').datagrid('clearSelections');
            }
        }, {
            text: '取消',
            iconCls: 'icon-undo',
            handler: function () {
                $('#Table_PACKAGE_TYPE_LIST').datagrid('rejectChanges');
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
            //        var openstr = '<a style=\"cursor:pointer\"  onclick="opendetail_PACKAGE_TYPE_LIST(' + index + ')">打开</a>&nbsp;';
            //       if (row.editing) {
            //            var c = '<a style=\"cursor:pointer\"  onclick="cancelrow_PACKAGE_TYPE_LIST(' + index + ')">取消</a>';
            //            return openstr + c;
            //        } else {
            //            var e = '<a style=\"cursor:pointer\" onclick="editrow_PACKAGE_TYPE_LIST(' + index + ')">编辑</a> ';
            //            return openstr + e;
            //        }
            //    }
            //},
            { field: 'PACKAGE_TYPE_ID', title: 'PACKAGE_TYPE_ID', align: 'left', styler: function (value, row, index) { return 'color:blue'; } },
            { field: 'FACTORY_ID', title: 'FACTORY_ID', align: 'left', styler: function (value, row, index) { return 'color:blue'; } }
        ]],
        columns: [[
            { field: 'PACKAGE_TYPE_DESC', title: 'PACKAGE_TYPE_DESC', align: 'left', editor: 'text' },
            { field: 'PACKAGE_CODE', title: 'PACKAGE_CODE', align: 'left', editor: 'text' },
            { field: 'VALID_FLAG', title: 'VALID_FLAG', align: 'left', editor: { type: 'checkbox', options: { on: '1', off: '0' } } },
            { field: 'UPDATE_USER', title: 'UPDATE_USER', align: 'left' },
            { field: 'UPDATE_DATE', title: 'UPDATE_DATE', align: 'left' },
            { field: 'GROUPS_LIMIT', title: 'GROUPS_LIMIT', align: 'left', editor: 'text' }


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
            editrow_PACKAGE_TYPE_LIST(rowIndex);
        },
        onDblClickRow: function (rowIndex, rowData) { },
        rowStyler: function (index, row) { },
        onBeforeEdit: function (index, row) {
            editIndex_PACKAGE_TYPE_LIST = index;
            row.editing = true;
            $('#Table_PACKAGE_TYPE_LIST').datagrid('refreshRow', index);
        },
        onAfterEdit: function (index, row) {
            row.editing = false;
            $('#Table_PACKAGE_TYPE_LIST').datagrid('refreshRow', index);
        },
        onCancelEdit: function (index, row) {
            row.editing = false;
            $('#Table_PACKAGE_TYPE_LIST').datagrid('refreshRow', index);
        }
    });
    $('#Table_PACKAGE_TYPE_LIST').datagrid("loadData", str).datagrid('acceptChanges');

    var pg = $('#Table_PACKAGE_TYPE_LIST').datagrid("getPager");
    if (pg) {
        $(pg).pagination({
            onSelectPage: function (pageNumber, pageSize) {
                Init_Table_PACKAGE_TYPE_LIST(pageNumber, pageSize); //翻页的关键点
            }
        });
    }
}
function editrow_PACKAGE_TYPE_LIST(index) {
    if (editIndex_PACKAGE_TYPE_LIST != undefined)
        $('#Table_PACKAGE_TYPE_LIST').datagrid('endEdit', editIndex_PACKAGE_TYPE_LIST);
    $('#Table_PACKAGE_TYPE_LIST').datagrid('beginEdit', index).datagrid('selectRow', index);
}
function Deleterow_PACKAGE_TYPE_LIST() {
    var row = $('#Table_PACKAGE_TYPE_LIST').datagrid('getSelected');
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
            Delete_PACKAGE_TYPE_LIST(row);
            $('#Table_PACKAGE_TYPE_LIST').datagrid('deleteRow', $('#Table_PACKAGE_TYPE_LIST').datagrid('getRowIndex'));
        }
    });
}
function Init_Table_PACKAGE_TYPE_LIST(pageNumber, pageSize) {
    $.ajax({
        type: 'get',
        url: '/api/PACKAGE_TYPE_LIST/GetDataPage',
        data: { 'pageSize': pageSize, 'pageNumber': pageNumber },
        dataType: 'json',
        success: function (data) {
            Table_PACKAGE_TYPE_LIST(data, pageSize, pageNumber);
        }
    });
}
function Dialog_PACKAGE_TYPE_LIST() {
    //选择框，勾选状态无法更改的解决办法：
    //$('#VALID_FLAG').click(
    //    function () {
    //        if ($('#VALID_FLAG').attr('checked') == 'checked') {
    //            $('#VALID_FLAG').attr('checked', false);
    //        } else {
    //            $('#VALID_FLAG').attr('checked', true);
    //        }
    //    });
    $('#Dialog_PACKAGE_TYPE_LIST').dialog({
        toolbar: [{
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                if (addOrEdit_PACKAGE_TYPE_LIST == 'add') {
                    Add_PACKAGE_TYPE_LIST();
                }
                else if (addOrEdit_PACKAGE_TYPE_LIST == 'edit') {
                    Edit_PACKAGE_TYPE_LIST();
                }
            }
        }]
    });
}
function Add_PACKAGE_TYPE_LIST() {
    if ($('#PACKAGE_TYPE_ID').val() == '' && $('#FACTORY_ID').val() == '') {
        $.messager.show({
            title: '消息',
            msg: '请填写',
            showType: 'show'
        });
        return;
    }
    var j = {
        'PACKAGE_TYPE_ID': $('#PACKAGE_TYPE_ID').val(),
        'FACTORY_ID': $('#FACTORY_ID').val(),
        'PACKAGE_TYPE_DESC': $('#PACKAGE_TYPE_DESC').val(),
        'PACKAGE_CODE': $('#PACKAGE_CODE').val(),
        'VALID_FLAG': $('#VALID_FLAG').val(),
        'UPDATE_USER': $('#UPDATE_USER').val(),
        'UPDATE_DATE': $('#UPDATE_DATE').val(),
        'GROUPS_LIMIT': $('#GROUPS_LIMIT').val()
    };
    $.ajax({
        type: 'post',
        url: '/api/PACKAGE_TYPE_LIST/PostAdd',
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
                Init_Table_PACKAGE_TYPE_LIST(1, pageSize);
                $('#Dialog_PACKAGE_TYPE_LIST').dialog('close');
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
function Edit_PACKAGE_TYPE_LIST() {
    if ($('#PACKAGE_TYPE_ID').val() == '' && $('#FACTORY_ID').val() == '') {
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
        'PACKAGE_TYPE_ID': $('#PACKAGE_TYPE_ID').val(),
        'FACTORY_ID': $('#FACTORY_ID').val(),
        'PACKAGE_TYPE_DESC': $('#PACKAGE_TYPE_DESC').val(),
        'PACKAGE_CODE': $('#PACKAGE_CODE').val(),
        'VALID_FLAG': $('#VALID_FLAG').val(),
        'UPDATE_USER': $('#UPDATE_USER').val(),
        'UPDATE_DATE': $('#UPDATE_DATE').val(),
        'GROUPS_LIMIT': $('#GROUPS_LIMIT').val()
    };
    $.ajax({
        type: 'post',
        url: '/api/PACKAGE_TYPE_LIST/PostEdit',
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
                Init_Table_PACKAGE_TYPE_LIST(1, pageSize);
                $('#Dialog_PACKAGE_TYPE_LIST').dialog('close');
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
function Edit_Cell_PACKAGE_TYPE_LIST(row) {
    var j = {
        'PACKAGE_TYPE_ID': row.PACKAGE_TYPE_ID,
        'FACTORY_ID': row.FACTORY_ID,
        'PACKAGE_TYPE_DESC': row.PACKAGE_TYPE_DESC,
        'PACKAGE_CODE': row.PACKAGE_CODE,
        'VALID_FLAG': row.VALID_FLAG,
        'UPDATE_USER': row.UPDATE_USER,
        'UPDATE_DATE': row.UPDATE_DATE,
        'GROUPS_LIMIT': row.GROUPS_LIMIT
    };
    $.ajax({
        type: 'post',
        url: '/api/PACKAGE_TYPE_LIST/PostEdit',
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
function Delete_PACKAGE_TYPE_LIST(row) {
    var j = {
        'PACKAGE_TYPE_ID': row.PACKAGE_TYPE_ID,
        'FACTORY_ID': row.FACTORY_ID
    };
    $.ajax({
        type: 'post',
        url: '/api/PACKAGE_TYPE_LIST/PostDelete',
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
                Init_Table_PACKAGE_TYPE_LIST(1, pageSize);
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