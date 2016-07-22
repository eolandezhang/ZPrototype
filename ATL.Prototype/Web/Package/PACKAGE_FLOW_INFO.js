var pageSize = 20;
var editIndex_PACKAGE_FLOW_INFO = undefined;
var addOrEdit_PACKAGE_FLOW_INFO = null;
var factoryId;
var packageNo;
var versionNo;
var productTypeId;
var produceProcTypeId;
var queryStr = ' ';

$(function () {
    packageNo = $.request.queryString["packageNo"];
    factoryId = $.request.queryString["factoryId"];
    versionNo = $.request.queryString["versionNo"];
    productTypeId = $.request.queryString["productTypeId"];
    produceProcTypeId = $.request.queryString["produceProcTypeId"];
    document.title = packageNo + '-' + versionNo + '设计';
    Dialog_PACKAGE_FLOW_INFO();
    InitTabs("工序信息", packageNo, factoryId, versionNo, productTypeId, produceProcTypeId);
    Init_Table_PACKAGE_GROUPS();
    Table_PACKAGE_FLOW_INFO([]);
    Init_PROCESS_ID();
    //Init_tt();
    Dialog_PACKAGE_FLOW_INFO_BatchAdd();
});
function Table_PACKAGE_FLOW_INFO(data) {
    $('#Table_PACKAGE_FLOW_INFO').datagrid({
        title: '工序',
        singleSelect: true,
        width: '864',
        height: '500',
        nowrap: false,
        toolbar: [{
            text: '批量新增',
            iconCls: 'icon-add',
            handler: function () {
                GROUP_NO_BATCHADD();
                PROCESS_ID_BATCHADD();
                $('#Dialog_PACKAGE_FLOW_INFO_BatchAdd').dialog('open');
            }
        },
        {
            text: '新增',
            iconCls: 'icon-add',
            handler: function () {
                addOrEdit_PACKAGE_FLOW_INFO = 'add';
                $('#grp').show();
                GROUP_NO_BATCHEDIT_ADD();
                SUB_GROUP_NO();
                var x = $('#Table_PACKAGE_GROUPS').datagrid('getSelected');
                if (x != null) {
                    $('#GROUP_NO_BATCHEDIT').combobox('setValue', x.GROUP_NO);
                }
                $('#PROCESS_ID').combogrid('clear').combogrid('readonly', false);
                $('#PROC_SEQUENCE_NO').val('');
                $('#PREVIOUS_PROCESS_ID').combogrid('clear');
                $('#NEXT_PROCESS_ID').combogrid('clear');
                $('#UPDATE_USER').val('');
                $('#UPDATE_DATE').val('');
                $('#PKG_PROC_DESC').val('');
                $('#UPDATE_USER').attr('readonly', true);
                $('#UPDATE_DATE').attr('readonly', true);
                $('#Dialog_PACKAGE_FLOW_INFO').dialog('open');
            }
        }, {
            text: '修改',
            iconCls: 'icon-edit',
            handler: function () {
                addOrEdit_PACKAGE_FLOW_INFO = 'edit';
                SUB_GROUP_NO();
                $('#grp').show();
                var x = $('#Table_PACKAGE_FLOW_INFO').datagrid('getSelected');
                if (x == null) {
                    $.messager.show({
                        title: '消息',
                        msg: '请先选择分组,然后选择工序',
                        showType: 'show'
                    });
                    return;
                }
                $('#PROCESS_ID').combogrid('setValue', x.PROCESS_ID).combogrid('readonly', true);
                $('#PROC_SEQUENCE_NO').val(x.PROC_SEQUENCE_NO);
                $('#PREVIOUS_PROCESS_ID').combogrid('setValue', x.PREVIOUS_PROCESS_ID);
                $('#NEXT_PROCESS_ID').combogrid('setValue', x.NEXT_PROCESS_ID);
                $('#UPDATE_USER').val(x.UPDATE_USER);
                $('#UPDATE_DATE').val(x.UPDATE_DATE);
                $('#PKG_PROC_DESC').val(x.PKG_PROC_DESC);
                $('#SUB_GROUP_NO').combobox('setValues', x.SUB_GROUP_NO == null ? '' : x.SUB_GROUP_NO.split(','));
                $('#UPDATE_USER').attr('readonly', true);
                $('#UPDATE_DATE').attr('readonly', true);
                $('#Dialog_PACKAGE_FLOW_INFO').dialog('open');
                GROUP_NO_BATCHEDIT(x.GROUP_NO);
            }
        }, {
            text: '删除',
            iconCls: 'icon-cancel',
            handler: function () {
                Deleterow_PACKAGE_FLOW_INFO();
            }
        }, {
            text: '批量删除',
            iconCls: 'icon-cancel',
            handler: function () {
                var x = $('#Table_PACKAGE_GROUPS').datagrid('getSelected');
                if (x == null) {
                    $.messager.show({
                        title: '消息',
                        msg: '请选择分组',
                        showType: 'show'
                    });
                    return;
                }
                var groupNo = x.GROUP_NO;

                $('#GROUP_NO_BatchDel').combobox({
                    panelHeight: 200,
                    valueField: 'GROUP_NO',
                    textField: 'GROUP_NO',
                    multiple: true,
                    method: 'get',
                    url: '/api/PACKAGE_GROUPS/GetData?factoryId=' + factoryId + '&packageNo=' + packageNo + '&versionNo=' + versionNo + "&queryStr= AND GROUP_NO!='" + groupNo + "'",
                    editable: false
                });

                $('#Dialog_PACKAGE_FLOW_INFO_BatchDel').dialog({
                    toolbar: [{
                        text: '保存',
                        iconCls: 'icon-save',
                        handler: function () {
                            var p = $('#Table_PACKAGE_FLOW_INFO_BatchDel').datagrid('getSelections');
                            var processArr = [];
                            var processStr = '';
                            $.each(p, function (index, value) {
                                processArr.push(value.PROCESS_ID);
                            });
                            processStr = $.unique(processArr).sort().join(',');
                            var groups = $('#GROUP_NO_BatchDel').combobox('getValues') + '';
                            $('#loading-mask').show();
                            var j = {
                                'PACKAGE_NO': packageNo,
                                'GROUP_NO': groupNo,
                                'GROUP_NOS': groups,
                                'FACTORY_ID': factoryId,
                                'VERSION_NO': versionNo,
                                'PROCESS_IDS': processStr
                            };
                            $.ajax({
                                type: 'post',
                                url: '/api/PACKAGE_FLOW_INFO/PostDelete_Batch',
                                data: JSON.stringify(j),
                                dataType: 'json',
                                contentType: 'application/json',
                                success: function (data) {
                                    $('#loading-mask').hide();
                                    if (data > 0) {
                                        $.messager.show({
                                            title: '消息',
                                            msg: '成功',
                                            showType: 'show'
                                        });
                                        Init_Table_PACKAGE_FLOW_INFO(factoryId, packageNo, versionNo, queryStr);
                                        $('#Dialog_PACKAGE_FLOW_INFO_BatchDel').dialog('close');
                                        //Init_tt();
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
                    }]
                });

                $('#Table_PACKAGE_FLOW_INFO_BatchDel').datagrid({
                    title: '',
                    singleSelect: true,
                    width: '280',
                    height: '294',
                    nowrap: false,
                    singleSelect: false,
                    url: '/api/PACKAGE_FLOW_INFO/GetDataNoPage?factoryId=' + factoryId + '&packageNo=' + packageNo + '&versionNo=' + versionNo + '&queryStr=&groupNo=' + groupNo,
                    method: 'get',
                    columns: [[{ checkbox: true },
                        { field: 'PROCESS_DESC', title: '工序', align: 'left', width: 230 }
                    ]]
                });
                $('#Dialog_PACKAGE_FLOW_INFO_BatchDel').dialog('open');
            }
        }, '-', {
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                $('#Table_PACKAGE_FLOW_INFO').datagrid('endEdit', editIndex_PACKAGE_FLOW_INFO);
                var changedRow = $('#Table_PACKAGE_FLOW_INFO').datagrid('getChanges');
                if (changedRow.length > 0) {
                    for (var i = 0; i < changedRow.length; i++) {
                        Edit_Cell_PACKAGE_FLOW_INFO(changedRow[i]);
                    }
                }
                editIndex_PACKAGE_FLOW_INFO = undefined;
                $('#Table_PACKAGE_FLOW_INFO').datagrid('acceptChanges');
            }
        }, {
            text: '取消',
            iconCls: 'icon-undo',
            handler: function () {
                $('#Table_PACKAGE_FLOW_INFO').datagrid('rejectChanges');
            }
        }],
        frozenColumns: [[
            { field: 'GROUP_NO', title: '组', width: 30, align: 'center' },
            {
                field: 'PROCESS_DESC', title: '工序', align: 'left', width: 150,
                styler: function (value, row, index) { return 'color:blue'; }
            }
        ]],
        columns: [[
            {
                field: 'PROC_SEQUENCE_NO', title: '序号', align: 'left', width: 60,
                editor: {
                    type: 'validatebox',
                    options: {
                        required: false,
                        validType: 'number'
                    }
                }
            },
            {
                field: 'PREVIOUS_PROCESS_ID', title: '前一工序', align: 'left', width: 150,
                editor: {
                    type: 'combobox',
                    options: {
                        valueField: 'PROCESS_ID',
                        textField: 'PROCESS_DESC',
                        url: '/api/PROCESS_LIST/GetDataByFactoryIdAndTypeId?factoryId=' + factoryId + "&productTypeId=" + productTypeId + '&produceProcTypeId=' + produceProcTypeId,
                        method: 'get',
                        required: false
                    }
                },
                formatter: function (value, row, index) {
                    return row.PROCESS_DESC_P;
                }
            },
            {
                field: 'NEXT_PROCESS_ID', title: '后一工序', align: 'left', width: 150,
                editor: {
                    type: 'combobox',
                    options: {
                        valueField: 'PROCESS_ID',
                        textField: 'PROCESS_DESC',
                        url: '/api/PROCESS_LIST/GetDataByFactoryIdAndTypeId?factoryId=' + factoryId + "&productTypeId=" + productTypeId + '&produceProcTypeId=' + produceProcTypeId,
                        method: 'get',
                        required: false
                    }
                },
                formatter: function (value, row, index) {
                    return row.PROCESS_DESC_N;
                }
            },
            {
                field: 'SUB_GROUP_NO', title: '分组归类', align: 'left', width: 55,
                editor: {
                    type: 'combobox',
                    options: {
                        valueField: 'GROUP_NO',
                        textField: 'GROUP_NO',
                        url: '/api/PACKAGE_GROUPS/GetData?factoryId=' + factoryId + '&packageNo=' + packageNo + '&versionNo=' + versionNo + '&queryStr=',
                        method: 'get',
                        required: false,
                        multiple: true
                    }
                }
            },
            {
                field: 'PKG_PROC_DESC', title: '说明', align: 'left', width: 200,
                editor: {
                    type: 'validatebox',
                    options: {
                        required: false,
                        validType: ['maxLength[100]']
                    }
                }
            },
            { field: 'UPDATE_USER', title: '最后修改者', align: 'left', width: 70 },
            { field: 'UPDATE_DATE', title: '最后修改日期', align: 'left', width: 130 },
            {
                field: 'PROCESS_ID', title: '工序编号', align: 'left', width: 55,
                styler: function (value, row, index) { return 'color:blue'; }
            }
        ]],
        onClickRow: function (rowIndex, rowData) {
            editrow_PACKAGE_FLOW_INFO(rowIndex);
        },
        onDblClickRow: function (rowIndex, rowData) { },
        rowStyler: function (index, row) { },
        onBeforeEdit: function (index, row) {
            editIndex_PACKAGE_FLOW_INFO = index;
            row.editing = true;
            $('#Table_PACKAGE_FLOW_INFO').datagrid('refreshRow', index);
        },
        onAfterEdit: function (index, row) {
            row.editing = false;
            $('#Table_PACKAGE_FLOW_INFO').datagrid('refreshRow', index);
        },
        onCancelEdit: function (index, row) {
            row.editing = false;
            $('#Table_PACKAGE_FLOW_INFO').datagrid('refreshRow', index);
        }
    });
    $('#Table_PACKAGE_FLOW_INFO').datagrid("loadData", data).datagrid('acceptChanges');
}
function editrow_PACKAGE_FLOW_INFO(index) {
    if (editIndex_PACKAGE_FLOW_INFO != undefined)
        $('#Table_PACKAGE_FLOW_INFO').datagrid('endEdit', editIndex_PACKAGE_FLOW_INFO);
    $('#Table_PACKAGE_FLOW_INFO').datagrid('beginEdit', index).datagrid('selectRow', index);
}
function Deleterow_PACKAGE_FLOW_INFO() {
    var row = $('#Table_PACKAGE_FLOW_INFO').datagrid('getSelected');
    if (row == null) {
        $.messager.show({
            title: '消息',
            msg: '请选择',
            showType: 'show'
        });
        return;
    }
    $.messager.confirm('确认', '将会删除相关工序明细:<br />参数信息,参数设定信息,物料信息,设备信息,附图信息,BOM信息.确认删除?', function (r) {
        if (r) {
            Delete_PACKAGE_FLOW_INFO(row);
        }
    });
}
function Init_Table_PACKAGE_FLOW_INFO(factoryId, packageNo, versionNo, queryStr) {
    var x = $('#Table_PACKAGE_GROUPS').datagrid('getSelected');
    if (x == null) return;
    var GROUP_NO = x.GROUP_NO;
    $.ajax({
        type: 'get',
        url: '/api/PACKAGE_FLOW_INFO/GetDataNoPage',
        data: {
            'factoryId': factoryId,
            'packageNo': packageNo,
            'versionNo': versionNo,
            'queryStr': queryStr,
            'groupNo': GROUP_NO
        },
        dataType: 'json',
        success: function (data) {
            Table_PACKAGE_FLOW_INFO(data);
        }
    });
}
function Dialog_PACKAGE_FLOW_INFO() {
    $('#Dialog_PACKAGE_FLOW_INFO').dialog({
        toolbar: [{
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                if (addOrEdit_PACKAGE_FLOW_INFO == 'edit') {
                    Edit_PACKAGE_FLOW_INFO();
                } else if (addOrEdit_PACKAGE_FLOW_INFO == 'add') {
                    BatchAddOneProcess();
                }

            }
        }]
    });
}
function Validate() {
    if (!(
        $('#PROC_SEQUENCE_NO').validatebox('isValid') &&
        $('#PKG_PROC_DESC').validatebox('isValid')
        )) {
        $.messager.show({
            title: '消息',
            msg: '填写不正确',
            showType: 'show'
        });
        return false;
    }

    return true;
}

function Edit_PACKAGE_FLOW_INFO() {
    var x = $('#Table_PACKAGE_FLOW_INFO').datagrid('getSelected');
    if (!Validate()) return;
    var g = $('#GROUP_NO_BATCHEDIT').combobox('getValues') + '';
    var groupNos = '';
    if (g.length != 0) {
        groupNos = g;
    }
    var j = {
        'PACKAGE_NO': x.PACKAGE_NO,
        'GROUP_NO': x.GROUP_NO,
        'FACTORY_ID': x.FACTORY_ID,
        'VERSION_NO': x.VERSION_NO,
        'PROCESS_ID': x.PROCESS_ID,
        'PROC_SEQUENCE_NO': $('#PROC_SEQUENCE_NO').val(),
        'PREVIOUS_PROCESS_ID': $('#PREVIOUS_PROCESS_ID').combogrid('getValue'),
        'NEXT_PROCESS_ID': $('#NEXT_PROCESS_ID').combogrid('getValue'),
        'UPDATE_USER': $('#UPDATE_USER').val(),
        'UPDATE_DATE': $('#UPDATE_DATE').val(),
        'PKG_PROC_DESC': $('#PKG_PROC_DESC').val(),
        'SUB_GROUP_NO': $('#SUB_GROUP_NO').combobox('getValues') + '',
        'GROUP_NOS': groupNos
    };
    $.ajax({
        type: 'post',
        url: '/api/PACKAGE_FLOW_INFO/PostEdit',
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
                Init_Table_PACKAGE_FLOW_INFO(factoryId, packageNo, versionNo, queryStr);
                $('#Dialog_PACKAGE_FLOW_INFO').dialog('close');
            } else if (data == -1) {
                $.messager.show({
                    title: '消息',
                    msg: '无权限',
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
function Edit_Cell_PACKAGE_FLOW_INFO(row) {
    var j = {
        'PACKAGE_NO': row.PACKAGE_NO,
        'GROUP_NO': row.GROUP_NO,
        'FACTORY_ID': row.FACTORY_ID,
        'VERSION_NO': row.VERSION_NO,
        'PROCESS_ID': row.PROCESS_ID,
        'PROC_SEQUENCE_NO': row.PROC_SEQUENCE_NO,
        'PREVIOUS_PROCESS_ID': row.PREVIOUS_PROCESS_ID,
        'NEXT_PROCESS_ID': row.NEXT_PROCESS_ID,
        'UPDATE_USER': row.UPDATE_USER,
        'UPDATE_DATE': row.UPDATE_DATE,
        'PKG_PROC_DESC': row.PKG_PROC_DESC,
        'SUB_GROUP_NO': row.SUB_GROUP_NO
    };
    $.ajax({
        type: 'post',
        url: '/api/PACKAGE_FLOW_INFO/PostEdit',
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
                Init_Table_PACKAGE_FLOW_INFO(factoryId, packageNo, versionNo, queryStr);
            } else if (data == -1) {
                $.messager.show({
                    title: '消息',
                    msg: '无权限',
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
function Delete_PACKAGE_FLOW_INFO(row) {
    var j = {
        'PACKAGE_NO': row.PACKAGE_NO,
        'GROUP_NO': row.GROUP_NO,
        'FACTORY_ID': row.FACTORY_ID,
        'VERSION_NO': row.VERSION_NO,
        'PROCESS_ID': row.PROCESS_ID
    };
    $.ajax({
        type: 'post',
        url: '/api/PACKAGE_FLOW_INFO/PostDelete',
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
                Init_Table_PACKAGE_FLOW_INFO(factoryId, packageNo, versionNo, queryStr);
                //Init_tt();
            } else if (data == -1) {
                $.messager.show({
                    title: '消息',
                    msg: '无权限',
                    showType: 'show'
                });
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
//分组归类
function SUB_GROUP_NO() {
    $('#SUB_GROUP_NO').combobox({
        panelHeight: 200,
        valueField: 'GROUP_NO',
        textField: 'GROUP_NO',
        multiple: true,
        method: 'get',
        url: '/api/PACKAGE_GROUPS/GetData?factoryId=' + factoryId + '&packageNo=' + packageNo + '&versionNo=' + versionNo + '&queryStr=',
        editable: false
    });
}


//分组
function Table_PACKAGE_GROUPS(data) {
    $('#Table_PACKAGE_GROUPS').datagrid({
        title: '分组',
        singleSelect: true,
        width: '100',
        height: '500',
        fitColumns: true,
        frozenColumns: [[
            { field: 'GROUP_NO', title: '组别', align: 'left', width: 40, styler: function (value, row, index) { return 'color:blue'; } }
        ]],
        columns: [[
            { field: 'GROUP_QTY', title: '数量', align: 'left', width: 40, editor: 'text' }
        ]],
        onClickRow: function (index, value) {
            Init_Table_PACKAGE_FLOW_INFO(factoryId, packageNo, versionNo, queryStr);
        }
    });
    $('#Table_PACKAGE_GROUPS').datagrid("loadData", data).datagrid('acceptChanges');
}
function Init_Table_PACKAGE_GROUPS() {
    $.ajax({
        type: 'get',
        url: '/api/PACKAGE_GROUPS/GetData',
        data: {
            'factoryId': factoryId,
            'packageNo': packageNo,
            'versionNo': versionNo,
            'queryStr': queryStr
        },
        dataType: 'json',
        success: function (data) {
            Table_PACKAGE_GROUPS(data);
        }
    });
}

//工序
function Init_PROCESS_ID() {
    $.ajax({
        type: 'get',
        url: '/api/PROCESS_LIST/GetDataByFactoryIdAndTypeId?factoryId=' + factoryId + "&productTypeId=" + productTypeId + '&produceProcTypeId=' + produceProcTypeId,
        success: function (data) {
            $('#PROCESS_ID').combogrid({
                idField: 'PROCESS_ID',
                textField: 'PROCESS_DESC',
                multiple: false,
                method: 'get',
                panelWidth: 350,
                panelHeight: 250,
                data: data,
                columns: [[{ checkbox: true },
                { field: 'PROCESS_ID', title: '编号', width: 130 },
                { field: 'PROCESS_DESC', title: '名称', width: 150 }
                ]],
                onClickRow: function (rowIndex, rowData) {
                    $('#PROC_SEQUENCE_NO').val(rowData.SEQUENCE_NO);
                    $('#PREVIOUS_PROCESS_ID').combogrid('grid').datagrid('clearSelections');
                    $('#NEXT_PROCESS_ID').combogrid('grid').datagrid('clearSelections');
                    $('#PREVIOUS_PROCESS_ID').combogrid('setValue', rowData.PREVIOUS_PROCESS_ID);
                    $('#NEXT_PROCESS_ID').combogrid('setValue', rowData.NEXT_PROCESS_ID);
                }
            });
            $('#PREVIOUS_PROCESS_ID').combogrid({
                idField: 'PROCESS_ID',
                textField: 'PROCESS_DESC',
                multiple: false,
                method: 'get',
                panelWidth: 350,
                panelHeight: 250,
                data: data,
                columns: [[{ checkbox: true },
                { field: 'PROCESS_ID', title: '编号', width: 130 },
                { field: 'PROCESS_DESC', title: '名称', width: 150 }
                ]]
            });

            //$('#PREVIOUS_PROCESS_ID').combobox({
            //    panelHeight: 200,
            //    valueField: 'PROCESS_ID',
            //    textField: 'PROCESS_DESC',
            //    multiple: false,
            //    editable: true,
            //    data: data
            //});

            $('#NEXT_PROCESS_ID').combogrid({
                idField: 'PROCESS_ID',
                textField: 'PROCESS_DESC',
                multiple: false,
                method: 'get',
                panelWidth: 350,
                panelHeight: 250,
                data: data,
                columns: [[{ checkbox: true },
                { field: 'PROCESS_ID', title: '编号', width: 130 },
                { field: 'PROCESS_DESC', title: '名称', width: 150 }
                ]]
            });
            //$('#NEXT_PROCESS_ID').combobox({
            //    panelHeight: 200,
            //    valueField: 'PROCESS_ID',
            //    textField: 'PROCESS_DESC',
            //    multiple: false,
            //    editable: true,
            //    data: data
            //});
        }
    });

}


//批量添加多个工序到多个分组中
function Dialog_PACKAGE_FLOW_INFO_BatchAdd() {
    $('#Dialog_PACKAGE_FLOW_INFO_BatchAdd').dialog({
        toolbar: [{
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                BatchAdd();
            }
        }]
    });
}
//分组下拉框
function GROUP_NO_BATCHADD() {
    $('#GROUP_NO_BATCHADD').combobox({
        panelHeight: 200,
        valueField: 'GROUP_NO',
        textField: 'GROUP_NO',
        multiple: true,
        method: 'get',
        url: '/api/PACKAGE_GROUPS/GetData?factoryId=' + factoryId + '&packageNo=' + packageNo + '&versionNo=' + versionNo + '&queryStr=',
        editable: false
    });
}
//工序下拉框
function PROCESS_ID_BATCHADD() {
    $('#PROCESS_ID_BATCHADD').combogrid({
        url: '/api/PROCESS_LIST/GetDataByFactoryIdAndTypeId?factoryId=' + factoryId + "&productTypeId=" + productTypeId + '&produceProcTypeId=' + produceProcTypeId,
        idField: 'PROCESS_ID',
        textField: 'PROCESS_DESC',
        multiple: true,
        method: 'get',
        panelWidth: 350,
        panelHeight: 250,
        columns: [[{ checkbox: true },
        { field: 'PROCESS_ID', title: '编号', width: 130 },
        { field: 'PROCESS_DESC', title: '名称', width: 150 }
        ]]
    });
}


//分组——批量新增修改
function GROUP_NO_BATCHEDIT(groupNo) {
    $('#GROUP_NO_BATCHEDIT').combobox({
        panelHeight: 100,
        valueField: 'GROUP_NO',
        textField: 'GROUP_NO',
        multiple: true,
        method: 'get',
        url: '/api/PACKAGE_GROUPS/GetData?factoryId=' + factoryId + '&packageNo=' + packageNo + '&versionNo=' + versionNo + '&queryStr= AND GROUP_NO!=\'' + groupNo + '\'',
        editable: false
    });
}


//为多个分组,增加一个工序
function BatchAdd() {
    if (!Validate()) return;
    var x = $('#GROUP_NO_BATCHADD').combobox('getValues');
    var y = $('#PROCESS_ID_BATCHADD').combogrid('getValues');
    if (x == '' || y == '') {
        $.messager.show({
            title: '消息',
            msg: '请选择',
            showType: 'show'
        });
        return;
    }
    $('#loading-mask').show();
    var j = {
        'PACKAGE_NO': packageNo,
        'FACTORY_ID': factoryId,
        'VERSION_NO': versionNo,
        'PRODUCT_TYPE_ID': productTypeId,
        'PRODUCT_PROC_TYPE_ID': produceProcTypeId,
        'GROUP_NOS': x + '',
        'PROCESS_IDS': y + '',
        'PRODUCT_TYPE_ID': productTypeId,
        'PRODUCT_PROC_TYPE_ID': produceProcTypeId
    }
    $.ajax({
        type: 'post',
        url: '/api/PACKAGE_FLOW_INFO/PostAddBatch',
        data: JSON.stringify(j),
        dataType: 'json',
        contentType: 'application/json',
        success: function (data) {
            $('#loading-mask').hide();
            if (data > 0) {
                $.messager.show({
                    title: '消息',
                    msg: '成功',
                    showType: 'show'
                });
                $('#Dialog_PACKAGE_FLOW_INFO_BatchAdd').dialog('close');
                Init_Table_PACKAGE_FLOW_INFO(factoryId, packageNo, versionNo, queryStr);
                //Init_tt();
            } else if (data == -1) {
                $.messager.show({
                    title: '消息',
                    msg: '无权限',
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
function GROUP_NO_BATCHEDIT_ADD() {
    $('#GROUP_NO_BATCHEDIT').combobox({
        panelHeight: 100,
        valueField: 'GROUP_NO',
        textField: 'GROUP_NO',
        multiple: true,
        method: 'get',
        url: '/api/PACKAGE_GROUPS/GetData?factoryId=' + factoryId + '&packageNo=' + packageNo + '&versionNo=' + versionNo + '&queryStr=',
        editable: false
    });
}
function BatchAddOneProcess() {
    var x = $('#GROUP_NO_BATCHEDIT').combobox('getValues') + '';
    var z = $('#PROCESS_ID').combogrid('getValue');
    if (x == '' || z == '') {
        $.messager.show({
            title: '消息',
            msg: '请选择分组和工序',
            showType: 'show'
        });
        return;
    }
    $('#loading-mask').show();
    var j = {
        'PACKAGE_NO': packageNo,
        'GROUP_NOS': x + '',
        'FACTORY_ID': factoryId,
        'VERSION_NO': versionNo,
        'PROCESS_ID': z,
        'PROC_SEQUENCE_NO': $('#PROC_SEQUENCE_NO').val(),
        'PREVIOUS_PROCESS_ID': $('#PREVIOUS_PROCESS_ID').combogrid('getValue'),
        'NEXT_PROCESS_ID': $('#NEXT_PROCESS_ID').combogrid('getValue'),
        'PRODUCT_PROC_TYPE_ID': produceProcTypeId,
        'PKG_PROC_DESC': $('#PKG_PROC_DESC').val(),
        'SUB_GROUP_NO': $('#SUB_GROUP_NO').combobox('getValues') + '',
        'PRODUCT_TYPE_ID': productTypeId,
        'PRODUCT_PROC_TYPE_ID': produceProcTypeId
    }
    $.ajax({
        type: 'post',
        url: '/api/PACKAGE_FLOW_INFO/PostAddBatchOneProcess',
        data: JSON.stringify(j),
        dataType: 'json',
        contentType: 'application/json',
        success: function (data) {
            $('#loading-mask').hide();
            if (data > 0) {
                $.messager.show({
                    title: '消息',
                    msg: '成功',
                    showType: 'show'
                });
                $('#Dialog_PACKAGE_FLOW_INFO_BatchAdd').dialog('close');
                Init_Table_PACKAGE_FLOW_INFO(factoryId, packageNo, versionNo, queryStr);
                //Init_tt();
            } else if (data == -1) {
                $.messager.show({
                    title: '消息',
                    msg: '无权限',
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




//初始化标签,没有工序就隐藏工序明细
function Init_tt() {
    $.ajax({
        type: 'get',
        url: '/api/PACKAGE_FLOW_INFO/GetDataByPackageId',
        data: {
            'factoryId': factoryId,
            'packageNo': packageNo,
            'versionNo': versionNo
        },
        dataType: 'json',
        success: function (data) {
            if (data.length == 0) {
                $('#tt').tabs('disableTab', '工序明细');
            } else {
                $('#tt').tabs('enableTab', '工序明细');
            }
        }
    });
}
