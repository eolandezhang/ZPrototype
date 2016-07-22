var addOrEdit = 'edit';
$(function () {
    Init_dialog_PID();
    InitMenu();
    $('#PID').click(function () {
        $('#dialog_PID').dialog('open');
    });
    $('#add').click(function () {
        $('#add').click(function () {
            $('#Table_menu input[type=text]').val('');
            $('#Table_menu textarea').val('');
            $('#TARGET').val('_self');
            $('#OPEN').val('true');
            addOrEdit = 'add';
        });
    });
    $('#save').click(function () {
        if (addOrEdit == 'add') {
            Add();
        } else if (addOrEdit == 'edit') {
            Edit();
        }
    });
    $('#del').click(function () {
        $.messager.confirm('消息', '确认删除？', function (r) {
            if (r) {
                Del();
            }
        });
    });
});

function InitMenu() {
    $.getJSON('/api/Menu/GetData',
                   function (data) {
                       $.fn.zTree.init($("#menu"), {
                           view: {
                               showIcon: false,
                               showLine: true,
                               showTitle: true
                           },
                           data: {
                               simpleData: {
                                   enable: true
                               },
                               key: {
                                   title: "title"
                               }
                           },
                           callback: {
                               beforeClick: beforeClick,
                               onClick: onClick
                           }
                       }, eval(data));
                   });
    $.getJSON('/api/Menu/GetData',
        function (data) {
            $.fn.zTree.init($("#tree_Doc_MNG_PID"), {
                view: {
                    showIcon: false,
                    showLine: true,
                    showTitle: true
                },
                data: {
                    simpleData: {
                        enable: true
                    },
                    key: {
                        title: "title"
                    }
                },
                callback: {
                    beforeClick: beforeClick_pid,
                    onClick: onClick_pid
                }
            }, eval(data));
        });
}

function beforeClick(treeId, treeNode, clickFlag) {
    $('#menu a').removeAttr('href');
    addOrEdit = 'edit';
    return (treeNode.click != false);
}
function onClick(event, treeId, treeNode, clickFlag) {

    $('#ID').val(treeNode.id);
    $('#PID').val(treeNode.pname);
    $('#PID').attr('pId', treeNode.pId);
    $('#NAME').val(treeNode.name);
    $('#TITLE').val(treeNode.title);
    $('#URL').val(treeNode.url);
    $('#SORT').val(treeNode.sort);
    $('#TARGET').val(treeNode.target == '_self' ? '_self' : '_blank');
    $('#OPEN').val(treeNode.open == true ? 'true' : 'false');    
}

function beforeClick_pid(treeId, treeNode, clickFlag) {
    $('#tree_Doc_MNG_PID a').removeAttr('href');
    return (treeNode.click != false);
}
function onClick_pid(event, treeId, treeNode, clickFlag) {
    $('#PID').val(treeNode.name);
    $('#PID').attr('pid', treeNode.id);
    $('#dialog_PID').dialog('close');
}
function Init_dialog_PID() {
    $('#dialog_PID').dialog({
        width: 300,
        height: 400,
        closed: true
    });
}

function Add() {
    if ($('#PID').attr('pid') == null ||
        $('#NAME').val() == null) {
        $.messager.show({
            title: '消息',
            msg: '请填写必填信息',
            showType: 'show'
        });
        return;
    }
    var j = {
        'PID': $('#PID').attr('pId'),
        'NAME': $('#NAME').val(),
        'TITLE': $('#TITLE').val(),
        'ICON': null,
        'URL': $('#URL').val(),
        'OPEN': $('#OPEN').val(),
        'TARGET': $('#TARGET').val(),
        'SORT': $('#SORT').val()
    };
    $.ajax({
        type: 'post',
        url: '/api/Menu/Add',
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
                InitMenu();
            } else if (data == 0) {
                $.messager.show({
                    title: '消息',
                    msg: '重复',
                    showType: 'show'
                });
            }
        }
    });
}

function Edit() {
    if ($('#PID').attr('pId') == null ||
        $('#NAME').val() == null) {
        $.messager.show({
            title: '消息',
            msg: '请填写必填信息',
            showType: 'show'
        });
        return;
    }
    var j = {
        'ID': $('#ID').val(),
        'PID': $('#PID').attr('pId'),
        'NAME': $('#NAME').val(),
        'TITLE': $('#CATEGORY_DESC').val(),
        'ICON': null,
        'URL': $('#URL').val(),
        'OPEN': $('#OPEN').val(),
        'TARGET': $('#TARGET').val(),
        'SORT': $('#SORT').val()
    };
    
    $.ajax({
        type: 'post',
        url: '/api/Menu/Edit',
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
                InitMenu();
            } else if (data == 0) {
                $.messager.show({
                    title: '消息',
                    msg: '重复',
                    showType: 'show'
                });
            }
        }
    });
}

function Del() {
    if ($('#ID').val() == null) {
        $.messager.show({
            title: '消息',
            msg: '请选择目录',
            showType: 'show'
        });
        return;
    }
    $.ajax({
        type: 'get',
        url: '/api/Menu/Delete?ID=' + $('#ID').val(),
        dataType: 'json',
        success: function (data) {
            if (data > 0) {
                $.messager.show({
                    title: '消息',
                    msg: '成功',
                    showType: 'show'
                });
                InitMenu();
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