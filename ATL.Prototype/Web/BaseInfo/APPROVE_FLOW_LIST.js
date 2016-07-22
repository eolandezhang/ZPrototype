var pageSize = 20;
var editIndex_APPROVE_FLOW_LIST = undefined;
var addOrEdit_APPROVE_FLOW_LIST = null;
$(function () {
    Table_APPROVE_FLOW_LIST(1, pageSize)
    Init_Table_APPROVE_FLOW_LIST(1, pageSize);
    Dialog_APPROVE_FLOW_LIST();
});

//分页(二选一)
function Table_APPROVE_FLOW_LIST(pageNumber, pageSize) {
    $('#Table_APPROVE_FLOW_LIST').datagrid({
        title: 'APPROVE_FLOW_LIST',
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
                addOrEdit_APPROVE_FLOW_LIST = 'add';
                $('#APPROVE_FLOW_ID').val('').attr('readonly', false);
                $('#FACTORY_ID').val('').attr('readonly', false);
                $('#UPDATE_USER').val('').attr('readonly', false);
                $('#UPDATE_DATE').val('').attr('readonly', false);
                $('#VALID_FLAG').val('').attr('readonly', false);
                $('#APPROVE_FLOW_DESC').val('').attr('readonly', false);
                $('#OWNER_APPROVE1').val('').attr('readonly', false);
                $('#OWNER_APPROVE2').val('').attr('readonly', false);
                $('#PROTO_APPROVE1').val('').attr('readonly', false);
                $('#PROTO_APPROVE2').val('').attr('readonly', false);
                $('#UPDATE_USER').attr('readonly', true);
                $('#UPDATE_DATE').attr('readonly', true);
                $('#Dialog_APPROVE_FLOW_LIST').dialog('open');
            }
        }, {
            text: '修改',
            iconCls: 'icon-edit',
            handler: function () {
                addOrEdit_APPROVE_FLOW_LIST = 'edit';
                var x = $('#Table_APPROVE_FLOW_LIST').datagrid('getSelected');
                if (x == null) return;
                //范例：
                //$('#PACKAGE_TYPE_ID').val(x.PACKAGE_TYPE_ID).attr('readonly', true);
                //$('#FACTORY_ID').combobox('setValue',x.FACTORY_ID).combobox('readonly', true);
                //$('#VALID_FLAG').attr('checked', x.VALID_FLAG == 1 ? true : false);               
                $('#APPROVE_FLOW_ID').val(x.APPROVE_FLOW_ID).attr('readonly', true);
                $('#FACTORY_ID').val(x.FACTORY_ID).attr('readonly', true);
                $('#UPDATE_USER').val(x.UPDATE_USER);
                $('#UPDATE_DATE').val(x.UPDATE_DATE);
                $('#VALID_FLAG').val(x.VALID_FLAG);
                $('#APPROVE_FLOW_DESC').val(x.APPROVE_FLOW_DESC);
                $('#OWNER_APPROVE1').val(x.OWNER_APPROVE1);
                $('#OWNER_APPROVE2').val(x.OWNER_APPROVE2);
                $('#PROTO_APPROVE1').val(x.PROTO_APPROVE1);
                $('#PROTO_APPROVE2').val(x.PROTO_APPROVE2);
                $('#UPDATE_USER').attr('readonly', true);
                $('#UPDATE_DATE').attr('readonly', true);
                $('#Dialog_APPROVE_FLOW_LIST').dialog('open');
            }
        }, {
            text: '删除',
            iconCls: 'icon-cancel',
            handler: function () {
                Deleterow_APPROVE_FLOW_LIST();
            }
        }, {
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                $('#Table_APPROVE_FLOW_LIST').datagrid('endEdit', editIndex_APPROVE_FLOW_LIST);
                var changedRow = $('#Table_APPROVE_FLOW_LIST').datagrid('getChanges');
                if (changedRow.length > 0) {
                    for (var i = 0; i < changedRow.length; i++) {
                        Edit_Cell_APPROVE_FLOW_LIST(changedRow[i]);
                    }
                }
                editIndex_APPROVE_FLOW_LIST = undefined;
                $('#Table_APPROVE_FLOW_LIST').datagrid('clearSelections');
            }
        }, {
            text: '取消',
            iconCls: 'icon-undo',
            handler: function () {
                $('#Table_APPROVE_FLOW_LIST').datagrid('rejectChanges');
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
            { field: 'APPROVE_FLOW_ID', title: '编号', align: 'left',width:140, styler: function (value, row, index) { return 'color:blue'; } },
            { field: 'FACTORY_ID', title: '厂别', align: 'left', width: 60, styler: function (value, row, index) { return 'color:blue'; } }
        ]],
        columns: [[            
            { field: 'APPROVE_FLOW_DESC', title: '描述', align: 'left', editor: 'text' },
            { field: 'OWNER_APPROVE1', title: 'OWNER_APPROVE1', align: 'left', width: 140, editor: 'text' },
            { field: 'OWNER_APPROVE2', title: 'OWNER_APPROVE2', align: 'left', width: 140, editor: 'text' },
            { field: 'PROTO_APPROVE1', title: 'PROTO_APPROVE1', align: 'left', width: 140, editor: 'text' },
            { field: 'PROTO_APPROVE2', title: 'PROTO_APPROVE2', align: 'left', width: 140, editor: 'text' },
            { field: 'UPDATE_USER', title: '最后更新者', align: 'left', width: 140 },
            { field: 'UPDATE_DATE', title: '最后更新日期', align: 'left', width: 140 },
            { field: 'VALID_FLAG', title: '状态', align: 'left', width: 140, editor: { type: 'checkbox', options: { on: '1', off: '0' } } }
        ]],
        onClickRow: function (rowIndex, rowData) {
            editrow_APPROVE_FLOW_LIST(rowIndex);
        },
        onDblClickRow: function (rowIndex, rowData) { },
        rowStyler: function (index, row) { },
        onBeforeEdit: function (index, row) {
            editIndex_APPROVE_FLOW_LIST = index;
            row.editing = true;
            $('#Table_APPROVE_FLOW_LIST').datagrid('refreshRow', index);
        },
        onAfterEdit: function (index, row) {
            row.editing = false;
            $('#Table_APPROVE_FLOW_LIST').datagrid('refreshRow', index);
        },
        onCancelEdit: function (index, row) {
            row.editing = false;
            $('#Table_APPROVE_FLOW_LIST').datagrid('refreshRow', index);
        }
    });
    var pg = $('#Table_APPROVE_FLOW_LIST').datagrid("getPager");
    if (pg) {
        $(pg).pagination({
            onSelectPage: function (pageNumber, pageSize) {
                Init_Table_APPROVE_FLOW_LIST(pageNumber, pageSize); //翻页的关键点
            }
        });
    }
}

function editrow_APPROVE_FLOW_LIST(index) {
    if (editIndex_APPROVE_FLOW_LIST != undefined)
        $('#Table_APPROVE_FLOW_LIST').datagrid('endEdit', editIndex_APPROVE_FLOW_LIST);
    $('#Table_APPROVE_FLOW_LIST').datagrid('beginEdit', index).datagrid('selectRow', index);
}
function Deleterow_APPROVE_FLOW_LIST() {
    var row = $('#Table_APPROVE_FLOW_LIST').datagrid('getSelected');
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
            Delete_APPROVE_FLOW_LIST(row);
            $('#Table_APPROVE_FLOW_LIST').datagrid('deleteRow', $('#Table_APPROVE_FLOW_LIST').datagrid('getRowIndex'));
        }
    });
}

//分页(二选一)
function Init_Table_APPROVE_FLOW_LIST(pageNumber, pageSize) {
    $.ajax({
        type: 'get',
        url: '/api/APPROVE_FLOW_LIST/GetDataPage',
        data: { 'pageSize': pageSize, 'pageNumber': pageNumber },
        dataType: 'json',
        success: function (data) {
            if (data.length != 0) {
                var str = { total: data[0].TOTAL, rows: data };
            } else {
                var str = { total: 0, rows: data };
            }
            $('#Table_APPROVE_FLOW_LIST').datagrid("loadData", str).datagrid('acceptChanges');
        }
    });
}
function Dialog_APPROVE_FLOW_LIST() {
    //选择框，勾选状态无法更改的解决办法：
    //$('#VALID_FLAG').click(
    //    function () {
    //        if ($('#VALID_FLAG').attr('checked') == 'checked') {
    //            $('#VALID_FLAG').attr('checked', false);
    //        } else {
    //            $('#VALID_FLAG').attr('checked', true);
    //        }
    //    });
    $('#Dialog_APPROVE_FLOW_LIST').dialog({
        toolbar: [{
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                if (addOrEdit_APPROVE_FLOW_LIST == 'add') {
                    Add_APPROVE_FLOW_LIST();
                }
                else if (addOrEdit_APPROVE_FLOW_LIST == 'edit') {
                    Edit_APPROVE_FLOW_LIST();
                }
            }
        }]
    });
}
function Add_APPROVE_FLOW_LIST() {
    if ($('#APPROVE_FLOW_ID').val() == '' && $('#FACTORY_ID').val() == '') {
        $.messager.show({
            title: '消息',
            msg: '请填写',
            showType: 'show'
        });
        return;
    }
    var j = {
        'APPROVE_FLOW_ID': $('#APPROVE_FLOW_ID').val(),
        'FACTORY_ID': $('#FACTORY_ID').val(),
        'UPDATE_USER': $('#UPDATE_USER').val(),
        'UPDATE_DATE': $('#UPDATE_DATE').val(),
        'VALID_FLAG': $('#VALID_FLAG').val(),
        'APPROVE_FLOW_DESC': $('#APPROVE_FLOW_DESC').val(),
        'OWNER_APPROVE1': $('#OWNER_APPROVE1').val(),
        'OWNER_APPROVE2': $('#OWNER_APPROVE2').val(),
        'PROTO_APPROVE1': $('#PROTO_APPROVE1').val(),
        'PROTO_APPROVE2': $('#PROTO_APPROVE2').val()
    };
    $.ajax({
        type: 'post',
        url: '/api/APPROVE_FLOW_LIST/PostAdd',
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
                Init_Table_APPROVE_FLOW_LIST(1, pageSize);
                $('#Dialog_APPROVE_FLOW_LIST').dialog('close');
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
function Edit_APPROVE_FLOW_LIST() {
    if ($('#APPROVE_FLOW_ID').val() == '' && $('#FACTORY_ID').val() == '') {
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
        'APPROVE_FLOW_ID': $('#APPROVE_FLOW_ID').val(),
        'FACTORY_ID': $('#FACTORY_ID').val(),
        'UPDATE_USER': $('#UPDATE_USER').val(),
        'UPDATE_DATE': $('#UPDATE_DATE').val(),
        'VALID_FLAG': $('#VALID_FLAG').val(),
        'APPROVE_FLOW_DESC': $('#APPROVE_FLOW_DESC').val(),
        'OWNER_APPROVE1': $('#OWNER_APPROVE1').val(),
        'OWNER_APPROVE2': $('#OWNER_APPROVE2').val(),
        'PROTO_APPROVE1': $('#PROTO_APPROVE1').val(),
        'PROTO_APPROVE2': $('#PROTO_APPROVE2').val()
    };
    $.ajax({
        type: 'post',
        url: '/api/APPROVE_FLOW_LIST/PostEdit',
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
                Init_Table_APPROVE_FLOW_LIST(1, pageSize);
                $('#Dialog_APPROVE_FLOW_LIST').dialog('close');
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
function Edit_Cell_APPROVE_FLOW_LIST(row) {
    var j = {
        'APPROVE_FLOW_ID': row.APPROVE_FLOW_ID,
        'FACTORY_ID': row.FACTORY_ID,
        'UPDATE_USER': row.UPDATE_USER,
        'UPDATE_DATE': row.UPDATE_DATE,
        'VALID_FLAG': row.VALID_FLAG,
        'APPROVE_FLOW_DESC': row.APPROVE_FLOW_DESC,
        'OWNER_APPROVE1': row.OWNER_APPROVE1,
        'OWNER_APPROVE2': row.OWNER_APPROVE2,
        'PROTO_APPROVE1': row.PROTO_APPROVE1,
        'PROTO_APPROVE2': row.PROTO_APPROVE2
    };
    $.ajax({
        type: 'post',
        url: '/api/APPROVE_FLOW_LIST/PostEdit',
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
function Delete_APPROVE_FLOW_LIST(row) {
    var j = {
        'APPROVE_FLOW_ID': row.APPROVE_FLOW_ID,
        'FACTORY_ID': row.FACTORY_ID
    };
    $.ajax({
        type: 'post',
        url: '/api/APPROVE_FLOW_LIST/PostDelete',
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
                Init_Table_APPROVE_FLOW_LIST(1, pageSize);
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