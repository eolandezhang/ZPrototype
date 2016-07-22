$(function () {
    Table_Users(1, 10);
    Init_Table_Users(1, 10);
    Dialog_Users();
    $('#btn_search').click(function () {
        Search();
    });
});

function Table_Users(pageNumber, pageSize) {
    $('#Table_Users').datagrid({
        title: '用户管理',
        singleSelect: true, //只能选择单行
        width: '840',
        height: 'auto',
        fitColumns: true,
        toolbar: [{
            text: '新增',
            iconCls: 'icon-add',
            handler: function () {
                addOrEdit = 'add';
                $('#USERNAME').attr('readonly', false);
                $('#Dialog_Users').dialog('open');
            }
        }, {
            text: '修改',
            iconCls: 'icon-edit',
            handler: function () {
                addOrEdit = 'edit';
                var x = $('#Table_Users').datagrid('getSelected');
                if (x == null) return;
                $('#USERNAME').val(x.USERNAME).attr('readonly', true);
                $('#DESCRIPTION').val(x.DESCRIPTION);
                $('#CNNAME').val(x.CNNAME);
                $('#DEPARTMENT').val(x.DEPARTMENT);
                $('#TITLE').val(x.TITLE);
                $('#MAIL').val(x.MAIL);
                $('#Dialog_Users').dialog('open');
            }
        }, {
            text: '删除',
            iconCls: 'icon-cancel',
            handler: function () {
                Deleterow();
            }
        }, {
            text: '保存当前页',
            iconCls: 'icon-save',
            handler: function () {
                $('#Table_Users').datagrid('endEdit', editIndex);
                var changedRow = $('#Table_Users').datagrid('getChanges');
                if (changedRow.length > 0) {
                    for (var i = 0; i < changedRow.length; i++) {
                        Edit_Cell(changedRow[i]);
                    }
                }
                editIndex = undefined;
                $('#Table_Users').datagrid('clearSelections');
            }
        }],
        rownumbers: true,
        pagination: true,
        pageNumber: pageNumber,
        pageSize: pageSize,
        pageList: [10, 20, 30],
        showFooter: true,
        frozenColumns: [[
            { field: 'USERNAME', title: '用户名', width: 100, align: 'left' },
            { field: 'DESCRIPTION', title: '工号', width: 60, align: 'left' },
            { field: 'CNNAME', title: '姓名', width: 80, align: 'left' },
            { field: 'DEPARTMENT', title: '部门', width: 100, align: 'left' },
            { field: 'TITLE', title: '职位', width: 100, align: 'left' }
        ]],
        columns: [[
            { field: 'MAIL', title: '邮箱', width: 180, align: 'left' },
            { field: 'CREATEDAT', title: '创建日期', width: 120, align: 'left' },
            { field: 'CREATEDBY', title: '创建人', width: 100, align: 'left' },
            { field: 'MODIFIEDAT', title: '修改日期', width: 120, align: 'left' },
            { field: 'MODIFIEDBY', title: '修改人', width: 100, align: 'left' }
        ]]
    });

    var pg = $('#Table_Users').datagrid("getPager");
    if (pg) {
        $(pg).pagination({
            onSelectPage: function (pageNumber, pageSize) {
                Init_Table_Users(pageNumber, pageSize); //翻页的关键点
            }
        });
    }


}
function Deleterow() {
    var row = $('#Table_Users').datagrid('getSelected');
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
            Delete(row.USERNAME);            
        }
    });
}

function Init_Table_Users(pageNumber, pageSize) {
    $.ajax({
        type: 'get',
        url: '/api/USERS/GetDataPage',
        data: { 'pageSize': pageSize, 'pageNumber': pageNumber },
        dataType: 'json',
        success: function (data) {            
            $('#Table_Users').datagrid("loadData", { total: data[0].TOTAL, rows: data });
        }
    });
}

function Dialog_Users() {
    $('#Dialog_Users').dialog({
        title: '用户',
        width: 400,
        height: 400,
        modal: false,
        closed: true,
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
        },
        {
            text: '获取AD用户信息',
            handler: function () {
                var x = $('#USERNAME').val();
                if (x == '') {
                    $.messager.show({
                        title: '消息',
                        msg: '请填写用户名',
                        showType: 'show'
                    });
                    return;
                }

                $('#loading-mask').fadeIn();
                $.ajax({
                    type: 'get',
                    url: '/api/Users/GetADUserInfo',
                    data: { 'userName': $('#USERNAME').val() },
                    dataType: 'json',
                    success: function (data) {
                        $('#CNNAME').val(data.CNNAME);
                        $('#MAIL').val(data.MAIL);
                        $('#DESCRIPTION').val(data.DESCRIPTION);
                        $('#TITLE').val(data.TITLE);
                        $('#DEPARTMENT').val(data.DEPARTMENT);
                        $('#loading-mask').fadeOut();
                    }
                });
            }
        }]
    });
}
function Add() {
    if ($('#USERNAME').val() == '') {
        $.messager.show({
            title: '消息',
            msg: '请填写用户名',
            showType: 'show'
        });
    }
    var j = {
        'USERNAME': $('#USERNAME').val(),
        'DESCRIPTION': $('#DESCRIPTION').val(),
        'CNNAME': $('#CNNAME').val(),
        'DEPARTMENT': $('#DEPARTMENT').val(),
        'TITLE': $('#TITLE').val(),
        'MAIL': $('#MAIL').val()
    };
    $.ajax({
        type: 'post',
        url: '/api/Users/Add',
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
                Init_Table_Users(1, 10);
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
    var j = {
        'USERNAME': $('#USERNAME').val(),
        'DESCRIPTION': $('#DESCRIPTION').val(),
        'CNNAME': $('#CNNAME').val(),
        'DEPARTMENT': $('#DEPARTMENT').val(),
        'TITLE': $('#TITLE').val(),
        'MAIL': $('#MAIL').val()
    };
    $.ajax({
        type: 'post',
        url: '/api/Users/Edit',
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
                Init_Table_Users(1, 10);
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
        'USERNAME': row.USERNAME,
        'DESCRIPTION': row.DESCRIPTION,
        'CNNAME': row.CNNAME,
        'DEPARTMENT': row.DEPARTMENT,
        'TITLE': row.TITLE,
        'MAIL': row.MAIL
    };
    $.ajax({
        type: 'post',
        url: '/api/Users/Edit',
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

function Delete(username) {
    $.ajax({
        type: 'get',
        url: '/api/Users/Delete',
        data: { 'username': username },
        dataType: 'json',
        success: function (data) {
            if (data > 0) {
                $.messager.show({
                    title: '消息',
                    msg: '成功',
                    showType: 'show'
                });
                Init_Table_Users(1, 10);
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

function Search() {
    var name = $('#USERNAME_SEARCH').val();
    var j = { 'userName': name };
    $.ajax({
        type: 'get',
        url: '/api/Users/Search',
        data: j,
        dataType: 'json',
        contentType: 'application/json',
        success: function (data) {
            if (data.length > 0) {
                Table_Users(data, 1, 10);
            }
            else {
                $('#Table_Users').datagrid('loadData', []);
            }

        }
    });
}