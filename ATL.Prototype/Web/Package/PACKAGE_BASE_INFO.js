var pageSize = 10;
var editIndex_PACKAGE_BASE_INFO = undefined;
var addOrEdit_PACKAGE_BASE_INFO = null;
var factoryId = '';
var queryStr = '';

$(function () {
    Table_PACKAGE_BASE_INFO(1, pageSize);
    Init_Table_PACKAGE_BASE_INFO(1, pageSize, "");
    Dialog_PACKAGE_BASE_INFO();
    InitSearch();
    Init_VERSION_NO();
    //$('#VERSION_NO').bind('keyup', function () {
    //    var v = $(this).val().toUpperCase();
    //    $(this).val(v);
    //});
    $('#ORDER_TYPE').bind('change', function () {
        if ($(this).val() == 'SO#') {
            $('#SO_NO').attr('readonly', false);
        } else {
            $('#SO_NO').val('').attr('readonly', true);
        }
    });
});
function Table_PACKAGE_BASE_INFO(pageNumber, pageSize) {
    $('#Table_PACKAGE_BASE_INFO').datagrid({
        title: '',
        singleSelect: true, //只能选择单行
        width: '966',
        height: 'auto',
        fitColumns: false,
        autoRowHeight: false,
        toolbar: [{
            text: '新增',
            iconCls: 'icon-add',
            handler: function () {
                addOrEdit_PACKAGE_BASE_INFO = 'add';
                InitDialogItems_InitAdd();
                $('#Dialog_PACKAGE_BASE_INFO').dialog({ 'title': '新增' });
                $('#Dialog_PACKAGE_BASE_INFO').dialog('open');
                $('#Tab_Dialog_PACKAGE_BASE_INFO').tabs('select', '基本信息');
            }
        }, {
            text: '修改',
            iconCls: 'icon-edit',
            handler: function () {
                addOrEdit_PACKAGE_BASE_INFO = 'edit';
                var x = $('#Table_PACKAGE_BASE_INFO').datagrid('getSelected');
                if (x == null) return;
                InitDialogItems_InitEdit(x);
                $('#Dialog_PACKAGE_BASE_INFO').dialog({ 'title': '修改' });
                $('#Dialog_PACKAGE_BASE_INFO').dialog('open');
                $('#Tab_Dialog_PACKAGE_BASE_INFO').tabs('select', '基本信息');
            }
        }, {
            text: '删除',
            iconCls: 'icon-cancel',
            handler: function () {
                Deleterow_PACKAGE_BASE_INFO();
            }
        }, '-', {
            text: '复制',
            iconCls: 'icon-copy',
            handler: function () {
                addOrEdit_PACKAGE_BASE_INFO = 'copy';
                var x = $('#Table_PACKAGE_BASE_INFO').datagrid('getSelected');
                if (x == null) return;
                InitDialogItems_InitEdit(x);
                $('#VERSION_NO').val('').attr('readonly', true);
                $('#PACKAGE_NO').attr('readonly', false);
                $('#Dialog_PACKAGE_BASE_INFO').dialog({ 'title': '复制' });
                $('#Dialog_PACKAGE_BASE_INFO').dialog('open');
            }
        }, {
            text: '导出',
            iconCls: 'icon-export',
            handler: function () {
                var p = $('#Table_PACKAGE_BASE_INFO').datagrid('getSelected');
                if (p == null) return;
                window.location.href = '/Package/ExportPKG.ashx?PACKAGE_NO=' + p.PACKAGE_NO + '&FACTORY_ID=' + p.FACTORY_ID + '&VERSION_NO=' + p.VERSION_NO;
            }
        }, '-', {
            text: '启用/查看审批流程',
            iconCls: 'icon-ok',
            handler: function () {
                var x = $('#Table_PACKAGE_BASE_INFO').datagrid('getSelected');
                if (x == null) return;
                if (x.APPROVE_FLOW_ID == '') {
                    $.messager.show({
                        title: '消息',
                        msg: '请选择审批流程',
                        showType: 'show'
                    });
                }
                var j = {
                    'PACKAGE_NO': x.PACKAGE_NO,
                    'VERSION_NO': x.VERSION_NO,
                    'FACTORY_ID': x.FACTORY_ID
                };
                $.ajax({
                    type: 'get',
                    url: '/api/Preview/Init_PACKAGE_WF_STEP',
                    data: j,
                    dataType: 'json',
                    success: function (data) {
                        if (data == 0) {
                            $.messager.show({
                                title: '消息',
                                msg: '未开启审批流程',
                                showType: 'show'
                            });
                        } else if (data == -1) {
                            $.messager.show({
                                title: '消息',
                                msg: '无权限',
                                showType: 'show'
                            });
                        } else {
                            window.location.href = '/Package/Preview.aspx?packageNo=' + x.PACKAGE_NO + '&factoryId=' + x.FACTORY_ID + '&versionNo=' + x.VERSION_NO + '&productTypeId=' + x.PRODUCT_TYPE_ID + '&produceProcTypeId=' + x.PRODUCT_PROC_TYPE_ID;
                        }
                    }
                });


            }
        }, {
            text: '已发布并启用的',
            iconCls: 'icon-search',
            handler: function () {
                Init_Table_PACKAGE_BASE_INFO(1, pageSize, " AND STATUS='5' AND VALID_FLAG='1' ");
            }
        }
        //'-', {
        //    text: '保存',
        //    iconCls: 'icon-save',
        //    handler: function () {
        //        $('#Table_PACKAGE_BASE_INFO').datagrid('endEdit', editIndex_PACKAGE_BASE_INFO);
        //        var changedRow = $('#Table_PACKAGE_BASE_INFO').datagrid('getChanges');
        //        if (changedRow.length > 0) {
        //            for (var i = 0; i < changedRow.length; i++) {
        //                Edit_Cell_PACKAGE_BASE_INFO(changedRow[i]);
        //            }
        //        }
        //        editIndex_PACKAGE_BASE_INFO = undefined;
        //        $('#Table_PACKAGE_BASE_INFO').datagrid('acceptChanges');
        //    }
        //}, {
        //    text: '取消',
        //    iconCls: 'icon-undo',
        //    handler: function () {
        //        $('#Table_PACKAGE_BASE_INFO').datagrid('rejectChanges');
        //    }
        //},
        ],//工具栏,参考jEasyUI.com说明
        rownumbers: false, //显示行号        
        pagination: true, //显示翻页工具栏
        pageNumber: pageNumber, //重点:传入当前页数
        pageSize: pageSize, //重点:传入每一页的大小
        pageList: [pageSize * 1, pageSize * 2, pageSize * 3], //可以调整每一页的大小
        method: 'get', //默认值为post,根据需要
        //sortName: 'USER_NUM', //单页排序的列
        //sortOrder: 'asc', //升序
        //remoteSort: false, //使用客户端排序
        showFooter: false,
        //idField: 'USER_NAME',
        frozenColumns: [[
            {
                field: 'ACTION', width: 60,
                formatter: function (value, row, index) {
                    var openstr = '<a style=\"cursor:pointer;color:blue;\"  onclick="opendetail_PACKAGE_BASE_INFO(' + index + ')">打开</a>&nbsp;' + '<a style=\"cursor:pointer;color:blue;\"  onclick="version_PACKAGE_BASE_INFO(' + index + ')">版本</a>&nbsp;';
                    return openstr;
                }
            },
            {
                field: 'STATUS', title: ' ', align: 'left', width: 60,
                formatter: function (value, row, index) {
                    switch (value) {
                        case '1':
                            return "<span style=\"color:green;\">草稿</span>";
                        case '2':
                            return "<span style=\"color:blue;\">已送审</span>";
                        case '3':
                            return "<span style=\"color:pink;\">已退回</span>";
                        case '4':
                            return "<span style=\"color:orange;\">已签审</span>";
                        case '5':
                            return "<span style=\"color:red;\">已发布</span>";
                    }
                }
            },
            {
                field: 'VALID_FLAG', title: '', align: 'left', width: 20,
                formatter: function (value, row, index) {
                    switch (value) {
                        case '0':
                            return "关";
                        case '1':
                            return "<span style=\"color:red;\">开</span>";
                    }
                }
            },
            {
                field: 'DELETE_FLAG', title: '删', align: 'left', width: 20,
                formatter: function (value, row, index) {
                    switch (value) {
                        case '0':
                            return "";
                        case '1':
                            return "<img src=\"/Images/bin.png\"/>";
                    }
                }
            }, {
                field: 'PREPARED_BY', title: '制作人', align: 'left', width: 55,
                styler: function (value, row, index) {
                    if (value == $('#lbl_description').html()) {
                        return 'color:#cc00cc';
                    }
                }
            },
            {
                field: 'PACKAGE_NO', title: '文件编号', align: 'left', width: 100
            },
            {
                field: 'FACTORY_ID', title: '厂别', align: 'left', width: 40
            },
            {
                field: 'VERSION_NO', title: '版本', align: 'left', width: 30
            }
        ]],
        columns: [[
            { field: 'PACKAGE_TYPE_ID', title: '类型', align: 'left', width: 35 },
            { field: 'BATTERY_TYPE', title: '电池类型', align: 'left', width: 55 },
            { field: 'BATTERY_QTY', title: '数量', align: 'left', width: 40 },
            { field: 'BATTERY_LAYERS', title: '层数', align: 'left', width: 30 },
            { field: 'ORDER_TYPE', title: '订单类型', align: 'left', width: 80 },
            { field: 'SO_NO', title: 'SO#', align: 'left', width: 80 },
            { field: 'PURPOSE', title: '生产用途', align: 'left', width: 100 },
            { field: 'OUTPUT_TARGET_DATE', title: '要求出货日期', align: 'left', width: 80 },
            {
                field: 'IS_URGENT', title: '紧急需求', align: 'left', width: 100,
                formatter: function (value, row, index) {
                    switch (value) {
                        case '0':
                            return "否";
                        case '1':
                            return "是";
                    }
                }
            },
            { field: 'REASON_FORURGENT', title: '加急原因', align: 'left', width: 100 },
            { field: 'GROUPS_PURPOSE', title: '分组目的', align: 'left', width: 100 },
            { field: 'PRODUCT_TYPE_ID', title: '产品类型', align: 'left', width: 55 },
            { field: 'PRODUCT_PROC_TYPE_ID', title: '产品工艺', align: 'left', width: 55 },
            { field: 'BATTERY_MODEL', title: '品种', align: 'left', width: 55 },
            { field: 'BATTERY_PARTNO', title: '电池料号', align: 'left', width: 80 },
            { field: 'PROJECT_CODE', title: '项目代码', align: 'left', width: 70 },
            { field: 'CUSTOMER_CODE', title: '客户代码', align: 'left', width: 55 },
            { field: 'EFFECT_DATE', title: '文件生效日期', align: 'left', width: 120 },
            { field: 'PREPARED_DATE', title: '制作日期', align: 'left', width: 120 },
            { field: 'UPDATE_USER', title: '最后更新人', align: 'left', width: 80 },
            { field: 'UPDATE_DATE', title: '最后更新日期', align: 'left', width: 120 },
            { field: 'APPROVE_FLOW_ID', title: '审批流程', align: 'left', width: 55 }
        ]],
        onClickRow: function (rowIndex, rowData) {
            //editrow_PACKAGE_BASE_INFO(rowIndex);
        },
        onDblClickRow: function (rowIndex, rowData) { },
        rowStyler: function (index, row) { },
        onBeforeEdit: function (index, row) {
            editIndex_PACKAGE_BASE_INFO = index;
            row.editing = true;
            $('#Table_PACKAGE_BASE_INFO').datagrid('refreshRow', index);
        },
        onAfterEdit: function (index, row) {
            row.editing = false;
            $('#Table_PACKAGE_BASE_INFO').datagrid('refreshRow', index);
        },
        onCancelEdit: function (index, row) {
            row.editing = false;
            $('#Table_PACKAGE_BASE_INFO').datagrid('refreshRow', index);
        }
    });

    var pg = $('#Table_PACKAGE_BASE_INFO').datagrid("getPager");
    if (pg) {
        $(pg).pagination({
            onSelectPage: function (pageNumber, pageSize) {
                Init_Table_PACKAGE_BASE_INFO(pageNumber, pageSize, ''); //翻页的关键点
            }
        });
    }
}
function Init_Table_PACKAGE_BASE_INFO(pageNumber, pageSize, queryStr) {
    $.ajax({
        type: 'get',
        url: '/api/PACKAGE_BASE_INFO/GetDataPage',
        data: {
            'pageSize': pageSize,
            'pageNumber': pageNumber,
            'queryStr': queryStr
        },
        dataType: 'json',
        success: function (data) {
            if (data.length != 0) {
                var str = { total: data[0].TOTAL, rows: data };
            } else {
                var str = { total: 0, rows: [] };
            }
            $('#Table_PACKAGE_BASE_INFO').datagrid("loadData", str);//.datagrid('acceptChanges');
        }
    });
}
function opendetail_PACKAGE_BASE_INFO(index) {
    $('#Table_PACKAGE_BASE_INFO').datagrid('selectRow', index);
    var row = $('#Table_PACKAGE_BASE_INFO').datagrid('getSelected');
    window.open('/Package/PackageProcessDetails.aspx?packageNo=' + row.PACKAGE_NO + '&factoryId=' + row.FACTORY_ID + '&versionNo=' + row.VERSION_NO + '&productTypeId=' + row.PRODUCT_TYPE_ID + '&produceProcTypeId=' + row.PRODUCT_PROC_TYPE_ID, '_self');
}
function cancelrow_PACKAGE_BASE_INFO(index) {
    $('#Table_PACKAGE_BASE_INFO').datagrid('cancelEdit', index);
}
function version_PACKAGE_BASE_INFO(index) {
    $('#Table_PACKAGE_BASE_INFO').datagrid('selectRow', index);
    var row = $('#Table_PACKAGE_BASE_INFO').datagrid('getSelected');
    queryStr = " AND PACKAGE_NO='" + row.PACKAGE_NO + "' ";

    Init_Table_PACKAGE_BASE_INFO(1, pageSize, queryStr);
}
function editrow_PACKAGE_BASE_INFO(index) {
    if (editIndex_PACKAGE_BASE_INFO != undefined)
        $('#Table_PACKAGE_BASE_INFO').datagrid('endEdit', editIndex_PACKAGE_BASE_INFO);
    $('#Table_PACKAGE_BASE_INFO').datagrid('beginEdit', index).datagrid('selectRow', index);
}
function Deleterow_PACKAGE_BASE_INFO() {
    var row = $('#Table_PACKAGE_BASE_INFO').datagrid('getSelected');
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
            Delete_PACKAGE_BASE_INFO(row);
        }
    });
}

function Dialog_PACKAGE_BASE_INFO() {
    $('#Dialog_PACKAGE_BASE_INFO').dialog({
        modal: false,
        width: 700,
        height: 460,
        toolbar: [{
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                if (addOrEdit_PACKAGE_BASE_INFO == 'add') {
                    Add_PACKAGE_BASE_INFO();
                }
                else if (addOrEdit_PACKAGE_BASE_INFO == 'edit') {
                    Edit_PACKAGE_BASE_INFO();
                }
                else if (addOrEdit_PACKAGE_BASE_INFO == 'copy') {
                    Copy_PACKAGE_BASE_INFO();
                }
            }
        }]
    });
}
function Validate() {    
    //必填项        
    if ($('#PACKAGE_NO').val() == '' ||
        $('#FACTORY_ID').val() == '' ||
        $('#VERSION_NO').val() == '' ||
        $('#PRODUCT_TYPE_ID').combobox('getValue') == '' ||
        $('#PRODUCT_PROC_TYPE_ID').combobox('getValue') == '' ||
        $('#BATTERY_MODEL').combobox('getValue') == '' ||
        $('#BATTERY_LAYERS').val() == '' ||
        $('#ORDER_TYPE').val() == '' ||
        $('#PROJECT_CODE').combobox('getValue') == '') {
        $.messager.show({
            title: '消息',
            msg: '请填写必填项',
            showType: 'show'
        });
        return false;
    }
    
    //验证输入合法性
    var v_PACKAGE_NO = $('#PACKAGE_NO').validatebox('isValid');
    var v_VERSION_NO = $('#VERSION_NO').validatebox('isValid');
    //var v_BATTERY_TYPE = $('#BATTERY_TYPE').validatebox('isValid');
    var v_BATTERY_LAYERS = $('#BATTERY_LAYERS').validatebox('isValid');
    //var v_BATTERY_QTY = $('#BATTERY_QTY').validatebox('isValid');
    //var v_ORDER_TYPE = $('#ORDER_TYPE').validatebox('isValid');
    var v_SO_NO = $('#SO_NO').validatebox('isValid');
    var v_PURPOSE = $('#PURPOSE').validatebox('isValid');
    var v_REASON_FORURGENT = $('#REASON_FORURGENT').validatebox('isValid');
    var v_GROUPS_PURPOSE = $('#GROUPS_PURPOSE').validatebox('isValid');
    var v_PRODUCT_CHANGE_HL = $('#PRODUCT_CHANGE_HL').validatebox('isValid');
    var v_PROCESS_CHANGE_HL = $('#PROCESS_CHANGE_HL').validatebox('isValid');
    var v_MATERIAL_CHANGE_HL = $('#MATERIAL_CHANGE_HL').validatebox('isValid');
    var v_OTHER_CHANGE_HL = $('#OTHER_CHANGE_HL').validatebox('isValid');

    if (!(v_PACKAGE_NO &&
        v_VERSION_NO &&
        //v_BATTERY_TYPE &&
        v_BATTERY_LAYERS &&
        //v_BATTERY_QTY &&
        //v_ORDER_TYPE &&
        v_SO_NO &&
        v_PURPOSE &&
        v_REASON_FORURGENT &&
        v_GROUPS_PURPOSE &&
        v_PRODUCT_CHANGE_HL &&
        v_PROCESS_CHANGE_HL &&
        v_MATERIAL_CHANGE_HL &&
        v_OTHER_CHANGE_HL
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
function Add_PACKAGE_BASE_INFO() {
    if (!Validate()) {
        return;
    }
    var j = {
        'PACKAGE_NO': $('#PACKAGE_NO').val(),
        'FACTORY_ID': $('#FACTORY_ID').val(),
        'VERSION_NO': $('#VERSION_NO').val(),
        'GROUPS_PURPOSE': $('#GROUPS_PURPOSE').val(),
        'PRODUCT_TYPE_ID': $('#PRODUCT_TYPE_ID').combobox('getValue'),
        'PRODUCT_PROC_TYPE_ID': $('#PRODUCT_PROC_TYPE_ID').combobox('getValue'),
        'PACKAGE_TYPE_ID': $('#PACKAGE_TYPE_ID').combobox('getValue'),
        'EFFECT_DATE': $('#EFFECT_DATE').val(),
        'BATTERY_MODEL': $('#BATTERY_MODEL').combobox('getValue'),
        'BATTERY_TYPE': $('#BATTERY_TYPE').val(),
        'BATTERY_LAYERS': $('#BATTERY_LAYERS').val(),
        'BATTERY_QTY': $('#BATTERY_QTY').val(),
        'BATTERY_PARTNO': $('#BATTERY_PARTNO').combobox('getValue').toUpperCase(),
        'PROJECT_CODE': $('#PROJECT_CODE').combobox('getValue').toUpperCase(),
        'CUSTOMER_CODE': $('#CUSTOMER_CODE').combobox('getValue'),
        'PURPOSE': $('#PURPOSE').val(),
        'ORDER_TYPE': $('#ORDER_TYPE').val(),
        'SO_NO': $('#SO_NO').val(),
        'IS_URGENT': $('#IS_URGENT').val(),
        'OUTPUT_TARGET_DATE': $('#OUTPUT_TARGET_DATE').datebox('getValue'),
        'REASON_FORURGENT': $('#REASON_FORURGENT').val(),
        'PREPARED_BY': $('#PREPARED_BY').val(),
        'PREPARED_DATE': $('#PREPARED_DATE').val(),
        'APPROVE_FLOW_ID': $('#APPROVE_FLOW_ID').combobox('getValue'),
        'UPDATE_USER': $('#UPDATE_USER').val(),
        'UPDATE_DATE': $('#UPDATE_DATE').val(),
        'VALID_FLAG': $('#VALID_FLAG').val(),
        'DELETE_FLAG': $('#DELETE_FLAG').val(),
        'STATUS': $('#STATUS').val(),
        'PRODUCT_CHANGE_HL': $('#PRODUCT_CHANGE_HL').val(),
        'PROCESS_CHANGE_HL': $('#PROCESS_CHANGE_HL').val(),
        'MATERIAL_CHANGE_HL': $('#MATERIAL_CHANGE_HL').val(),
        'OTHER_CHANGE_HL': $('#OTHER_CHANGE_HL').val()
    };
    $.ajax({
        type: 'post',
        url: '/api/PACKAGE_BASE_INFO/PostAdd',
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
                Init_Table_PACKAGE_BASE_INFO(1, pageSize, '');
                $('#Dialog_PACKAGE_BASE_INFO').dialog('close');
                window.location.href = '/Package/PackageProcessDetails.aspx?packageNo=' + j.PACKAGE_NO + '&factoryId=' + j.FACTORY_ID + '&versionNo=' + j.VERSION_NO + '&productTypeId=' + j.PRODUCT_TYPE_ID + '&produceProcTypeId=' + j.PRODUCT_PROC_TYPE_ID;
            } else if (data == -1) {
                $.messager.show({
                    title: '消息',
                    msg: '无权限',
                    showType: 'show'
                });
            } else if (data == -2) {
                $.messager.show({
                    title: '消息',
                    msg: '输入不正确',
                    showType: 'show'
                });
            } else if (data == 0) {
                $.messager.show({
                    title: '消息',
                    msg: '重复',
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
function Edit_PACKAGE_BASE_INFO() {
    if (!Validate()) {
        return;
    }
    var j = {
        'PACKAGE_NO': $('#PACKAGE_NO').val(),
        'FACTORY_ID': $('#FACTORY_ID').val(),
        'VERSION_NO': $('#VERSION_NO').val(),
        'GROUPS_PURPOSE': $('#GROUPS_PURPOSE').val(),
        'PRODUCT_TYPE_ID': $('#PRODUCT_TYPE_ID').combobox('getValue'),
        'PRODUCT_PROC_TYPE_ID': $('#PRODUCT_PROC_TYPE_ID').combobox('getValue'),
        'PACKAGE_TYPE_ID': $('#PACKAGE_TYPE_ID').combobox('getValue'),
        'EFFECT_DATE': $('#EFFECT_DATE').val(),
        'BATTERY_MODEL': $('#BATTERY_MODEL').combobox('getValue'),
        'BATTERY_TYPE': $('#BATTERY_TYPE').val(),
        'BATTERY_LAYERS': $('#BATTERY_LAYERS').val(),
        'BATTERY_QTY': $('#BATTERY_QTY').val(),
        'BATTERY_PARTNO': $('#BATTERY_PARTNO').combobox('getValue').toUpperCase(),
        'PROJECT_CODE': $('#PROJECT_CODE').combobox('getValue').toUpperCase(),
        'CUSTOMER_CODE': $('#CUSTOMER_CODE').combobox('getValue'),
        'PURPOSE': $('#PURPOSE').val(),
        'ORDER_TYPE': $('#ORDER_TYPE').val(),
        'SO_NO': $('#SO_NO').val(),
        'IS_URGENT': $('#IS_URGENT').val(),
        'OUTPUT_TARGET_DATE': $('#OUTPUT_TARGET_DATE').datebox('getValue'),
        'REASON_FORURGENT': $('#REASON_FORURGENT').val(),
        'PREPARED_BY': $('#PREPARED_BY').val(),
        'PREPARED_DATE': $('#PREPARED_DATE').val(),
        'APPROVE_FLOW_ID': $('#APPROVE_FLOW_ID').combobox('getValue'),
        'UPDATE_USER': $('#UPDATE_USER').val(),
        'UPDATE_DATE': $('#UPDATE_DATE').val(),
        'VALID_FLAG': $('#VALID_FLAG').val(),
        'DELETE_FLAG': $('#DELETE_FLAG').val(),
        'STATUS': $('#STATUS').val(),
        'PRODUCT_CHANGE_HL': $('#PRODUCT_CHANGE_HL').val(),
        'PROCESS_CHANGE_HL': $('#PROCESS_CHANGE_HL').val(),
        'MATERIAL_CHANGE_HL': $('#MATERIAL_CHANGE_HL').val(),
        'OTHER_CHANGE_HL': $('#OTHER_CHANGE_HL').val()
    };
    $.ajax({
        type: 'post',
        url: '/api/PACKAGE_BASE_INFO/PostEdit',
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
                Init_Table_PACKAGE_BASE_INFO(1, pageSize, '');
                $('#Dialog_PACKAGE_BASE_INFO').dialog('close');
            } else if (data == -1) {
                $.messager.show({
                    title: '消息',
                    msg: '无权限',
                    showType: 'show'
                });
            } else if (data == -2) {
                $.messager.show({
                    title: '消息',
                    msg: '输入不正确',
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
function Edit_Cell_PACKAGE_BASE_INFO(row) {
    var j = {
        'PACKAGE_NO': row.PACKAGE_NO,
        'FACTORY_ID': row.FACTORY_ID,
        'VERSION_NO': row.VERSION_NO,
        'GROUPS_PURPOSE': row.GROUPS_PURPOSE,
        'PRODUCT_TYPE_ID': row.PRODUCT_TYPE_ID,
        'PRODUCT_PROC_TYPE_ID': row.PRODUCT_PROC_TYPE_ID,
        'PACKAGE_TYPE_ID': row.PACKAGE_TYPE_ID,
        'EFFECT_DATE': row.EFFECT_DATE,
        'BATTERY_MODEL': row.BATTERY_MODEL,
        'BATTERY_TYPE': row.BATTERY_TYPE,
        'BATTERY_LAYERS': row.BATTERY_LAYERS,
        'BATTERY_QTY': row.BATTERY_QTY,
        'BATTERY_PARTNO': row.BATTERY_PARTNO,
        'PROJECT_CODE': row.PROJECT_CODE,
        'CUSTOMER_CODE': row.CUSTOMER_CODE,
        'PURPOSE': row.PURPOSE,
        'ORDER_TYPE': row.ORDER_TYPE,
        'SO_NO': row.SO_NO,
        'IS_URGENT': row.IS_URGENT,
        'OUTPUT_TARGET_DATE': row.OUTPUT_TARGET_DATE,
        'REASON_FORURGENT': row.REASON_FORURGENT,
        'PREPARED_BY': row.PREPARED_BY,
        'PREPARED_DATE': row.PREPARED_DATE,
        'APPROVE_FLOW_ID': row.APPROVE_FLOW_ID,
        'UPDATE_USER': row.UPDATE_USER,
        'UPDATE_DATE': row.UPDATE_DATE,
        'VALID_FLAG': row.VALID_FLAG,
        'DELETE_FLAG': row.DELETE_FLAG,
        'STATUS': row.STATUS,
        'PRODUCT_CHANGE_HL': row.PRODUCT_CHANGE_HL,
        'PROCESS_CHANGE_HL': row.PROCESS_CHANGE_HL,
        'MATERIAL_CHANGE_HL': row.MATERIAL_CHANGE_HL,
        'OTHER_CHANGE_HL': row.OTHER_CHANGE_HL
    };
    $.ajax({
        type: 'post',
        url: '/api/PACKAGE_BASE_INFO/PostEdit',
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
function Delete_PACKAGE_BASE_INFO(row) {
    var j = {
        'PACKAGE_NO': row.PACKAGE_NO,
        'FACTORY_ID': row.FACTORY_ID,
        'VERSION_NO': row.VERSION_NO
    };
    $.ajax({
        type: 'post',
        url: '/api/PACKAGE_BASE_INFO/PostDelete',
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
                Init_Table_PACKAGE_BASE_INFO(1, pageSize, '');
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
function Copy_PACKAGE_BASE_INFO() {
    if (!Validate()) {
        return;
    }
    var x = $('#Table_PACKAGE_BASE_INFO').datagrid('getSelected');
    if (x == null) return;
    var j = {
        'PACKAGE_NO': $('#PACKAGE_NO').val().toUpperCase(),
        'FACTORY_ID': $('#FACTORY_ID').val(),
        'VERSION_NO': $('#VERSION_NO').val().toUpperCase(),
        'GROUPS_PURPOSE': $('#GROUPS_PURPOSE').val(),
        'PRODUCT_TYPE_ID': $('#PRODUCT_TYPE_ID').combobox('getValue'),
        'PRODUCT_PROC_TYPE_ID': $('#PRODUCT_PROC_TYPE_ID').combobox('getValue'),
        'PACKAGE_TYPE_ID': $('#PACKAGE_TYPE_ID').combobox('getValue'),
        'EFFECT_DATE': $('#EFFECT_DATE').val(),
        'BATTERY_MODEL': $('#BATTERY_MODEL').combobox('getValue'),
        'BATTERY_TYPE': $('#BATTERY_TYPE').val(),
        'BATTERY_LAYERS': $('#BATTERY_LAYERS').val(),
        'BATTERY_QTY': $('#BATTERY_QTY').val(),
        'BATTERY_PARTNO': $('#BATTERY_PARTNO').combobox('getValue').toUpperCase(),
        'PROJECT_CODE': $('#PROJECT_CODE').combobox('getValue').toUpperCase(),
        'CUSTOMER_CODE': $('#CUSTOMER_CODE').combobox('getValue'),
        'PURPOSE': $('#PURPOSE').val(),
        'ORDER_TYPE': $('#ORDER_TYPE').val(),
        'IS_URGENT': $('#IS_URGENT').val(),
        'OUTPUT_TARGET_DATE': $('#OUTPUT_TARGET_DATE').datebox('getValue'),
        'REASON_FORURGENT': $('#REASON_FORURGENT').val(),
        'PREPARED_BY': $('#PREPARED_BY').val(),
        'PREPARED_DATE': $('#PREPARED_DATE').val(),
        'APPROVE_FLOW_ID': $('#APPROVE_FLOW_ID').combobox('getValue'),
        'UPDATE_USER': $('#UPDATE_USER').val(),
        'UPDATE_DATE': $('#UPDATE_DATE').val(),
        'VALID_FLAG': $('#VALID_FLAG').val(),
        'DELETE_FLAG': $('#DELETE_FLAG').val(),
        'STATUS': $('#STATUS').val(),
        'ORA_PACKAGE_NO': x.PACKAGE_NO.toUpperCase(),
        'ORA_VERSION_NO': x.VERSION_NO.toUpperCase(),
        'PRODUCT_CHANGE_HL': $('#PRODUCT_CHANGE_HL').val(),
        'PROCESS_CHANGE_HL': $('#PROCESS_CHANGE_HL').val(),
        'MATERIAL_CHANGE_HL': $('#MATERIAL_CHANGE_HL').val(),
        'OTHER_CHANGE_HL': $('#OTHER_CHANGE_HL').val()
    };

    $.ajax({
        type: 'post',
        url: '/api/PACKAGE_BASE_INFO/PostCopy',
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
                Init_Table_PACKAGE_BASE_INFO(1, pageSize, '');
                $('#Dialog_PACKAGE_BASE_INFO').dialog('close');
            } else if (data == -1) {
                $.messager.show({
                    title: '消息',
                    msg: '无权限',
                    showType: 'show'
                });
            } else if (data == 0) {
                $.messager.show({
                    title: '消息',
                    msg: '重复',
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

//查询
function InitSearch() {
    $('#btnSearch').click(function () {
        InitQueryStr();
        Init_Table_PACKAGE_BASE_INFO(1, pageSize, queryStr);
    });
}
function InitQueryStr() {
    queryStr = " AND 1=1 ";
    var packageNo = $.trim($('#Search_PACKAGE_NO').val());
    var versionNo = $.trim($('#Search_VERSION_NO').val());
    var validFlag = $('#Search_VALID_FLAG').val();
    var deleteFlag = $('#Search_DELETE_FLAG').val();
    if (packageNo != "") {
        queryStr = queryStr + " AND UPPER(PACKAGE_NO) LIKE'%" + packageNo.toUpperCase() + "%'";
    }
    if (versionNo != "") {
        queryStr = queryStr + " AND UPPER(VERSION_NO) ='" + versionNo.toUpperCase() + "'";
    }
    queryStr += " AND VALID_FLAG LIKE '" + validFlag + "'";
    queryStr += " AND DELETE_FLAG LIKE '" + deleteFlag + "'";
}

//新增对话框初始化
function InitDialogItems_InitAdd() {
    Init_PACKAGE_TYPE_ID();
    Init_PRODUCT_TYPE_ID();
    Init_APPROVE_FLOW_ID();
    //Init_BATTERY_MODEL();
    //Init_PROJECT_CODE();
    //Init_CUSTOMER_CODE();
    $('#PRODUCT_PROC_TYPE_ID').combobox('readonly', false);
    $('#PACKAGE_NO').val('').attr('readonly', false);
    $('#FACTORY_ID').val($('#USERS_FACTORY_ID').combobox('getValue')).attr('readonly', true);
    $('#VERSION_NO').val('').attr('readonly', true);
    $('#GROUPS_PURPOSE').attr('readonly', false);
    $('#PRODUCT_TYPE_ID').combobox('clear').combobox('readonly', false);
    $('#PRODUCT_PROC_TYPE_ID').combobox('clear').combobox('readonly', false);
    $('#PACKAGE_TYPE_ID').combobox('clear').combobox('readonly', false);
    $('#EFFECT_DATE').val('');
    $('#BATTERY_MODEL').combobox('clear');
    $('#BATTERY_TYPE').val('Li-Co');
    $('#BATTERY_LAYERS').val('');
    $('#BATTERY_QTY').val('').attr('readonly', true).css('border', 'none');
    $('#BATTERY_PARTNO').combobox('clear');
    $('#PROJECT_CODE').combobox('clear');
    $('#CUSTOMER_CODE').combobox('clear');
    $('#PURPOSE').val('');
    $('#ORDER_TYPE').val('');
    $('#SO_NO').val('');
    $('#IS_URGENT').val(0);
    $('#OUTPUT_TARGET_DATE').datebox('clear');
    $('#REASON_FORURGENT').val('');
    $('#PREPARED_BY').val($('#lbl_description').html()).attr('readonly', true).css('border', 'none');
    $('#PREPARED_DATE').val('').attr('readonly', true).css('border', 'none');
    $('#APPROVE_FLOW_ID').combobox('setValue', 'WF002');
    $('#UPDATE_USER').val('').attr('readonly', true).css('border', 'none');
    $('#UPDATE_DATE').val('').attr('readonly', true).css('border', 'none');
    $('#VALID_FLAG').val(0).attr('disabled', true);
    $('#DELETE_FLAG').val(0);
    $('#UPDATE_USER').attr('readonly', true);
    $('#UPDATE_DATE').attr('readonly', true);
    $('#STATUS').val('1').attr('disabled', true);
    $('#PRODUCT_CHANGE_HL').val('');
    $('#PROCESS_CHANGE_HL').val('');
    $('#MATERIAL_CHANGE_HL').val('');
    $('#OTHER_CHANGE_HL').val('');
    InitComboBox(true, true);

    $('#alert_BATTERY_MODEL').hide();
    $('#alert_BATTERY_PARTNO').hide();
    $('#alert_PROJECT_CODE').hide();
    $('#alert_CUSTOMER_CODE').hide();

}

//修改对话框初始化
function InitDialogItems_InitEdit(x) {
    Init_PACKAGE_TYPE_ID();
    $('#PACKAGE_TYPE_ID').combobox('setValue', x.PACKAGE_TYPE_ID).combobox('readonly', true);
    Init_PRODUCT_TYPE_ID();
    $('#PRODUCT_TYPE_ID').combobox('setValue', x.PRODUCT_TYPE_ID).combobox('readonly', true);
    Init_PRODUCT_PROC_TYPE_ID();
    $('#PRODUCT_PROC_TYPE_ID').combobox('setValue', x.PRODUCT_PROC_TYPE_ID).combobox('readonly', true);

    Init_BATTERY_MODEL();//factoryId,productTypeId,productProcTypeId
    Init_PROJECT_CODE();//factoryId,productTypeId,productProcTypeId
    Init_CUSTOMER_CODE();//factoryId,productTypeId,productProcTypeId
    Init_BATTERY_PARTNO();//factoryId,productTypeId,productProcTypeId,MATERIAL_CATEGORY_ID
    Init_APPROVE_FLOW_ID();//factoryId,productTypeId,productProcTypeId

    $('#PACKAGE_NO').val(x.PACKAGE_NO).attr('readonly', true);
    $('#FACTORY_ID').val(x.FACTORY_ID).attr('readonly', true);
    $('#VERSION_NO').val(x.VERSION_NO).attr('readonly', true);
    $('#GROUPS_PURPOSE').val(x.GROUPS_PURPOSE);
    $('#EFFECT_DATE').val(x.EFFECT_DATE);
    $('#BATTERY_MODEL').combobox('setValue', x.BATTERY_MODEL);
    $('#BATTERY_TYPE').val(x.BATTERY_TYPE);
    $('#BATTERY_LAYERS').val(x.BATTERY_LAYERS);
    $('#BATTERY_QTY').val(x.BATTERY_QTY).attr('readonly', true).css('border', 'none');
    $('#BATTERY_PARTNO').combobox('setValue', x.BATTERY_PARTNO);
    $('#PROJECT_CODE').combobox('setValue', x.PROJECT_CODE);
    $('#CUSTOMER_CODE').combobox('setValue', x.CUSTOMER_CODE);
    $('#PURPOSE').val(x.PURPOSE);
    $('#ORDER_TYPE').val(x.ORDER_TYPE);
    $('#SO_NO').val(x.SO_NO);
    $('#IS_URGENT').val(x.IS_URGENT);
    $('#OUTPUT_TARGET_DATE').datebox('setValue', dateFormat(x.OUTPUT_TARGET_DATE, "mm/dd/yyyy"));
    $('#REASON_FORURGENT').val(x.REASON_FORURGENT);
    $('#PREPARED_BY').val(x.PREPARED_BY).attr('readonly', true).css('border', 'none');
    $('#PREPARED_DATE').val(x.PREPARED_DATE).attr('readonly', true).css('border', 'none');
    $('#APPROVE_FLOW_ID').combobox('setValue', x.APPROVE_FLOW_ID);
    $('#UPDATE_USER').val(x.UPDATE_USER).attr('readonly', true).css('border', 'none');
    $('#UPDATE_DATE').val(x.UPDATE_DATE).attr('readonly', true).css('border', 'none');
    $('#VALID_FLAG').val(x.VALID_FLAG).attr('disabled', true);
    $('#DELETE_FLAG').val(x.DELETE_FLAG);
    $('#UPDATE_USER').attr('readonly', true);
    $('#UPDATE_DATE').attr('readonly', true);
    $('#STATUS').val(x.STATUS).attr('disabled', true);
    $('#PRODUCT_CHANGE_HL').val(x.PRODUCT_CHANGE_HL);
    $('#PROCESS_CHANGE_HL').val(x.PROCESS_CHANGE_HL);
    $('#MATERIAL_CHANGE_HL').val(x.MATERIAL_CHANGE_HL);
    $('#OTHER_CHANGE_HL').val(x.OTHER_CHANGE_HL);
    //InitGroupQty();
    InitComboBox(true, false);
    $('#alert_BATTERY_MODEL').hide();
    $('#alert_BATTERY_PARTNO').hide();
    $('#alert_PROJECT_CODE').hide();
    $('#alert_CUSTOMER_CODE').hide();
    InitWf();

    if ($('#ORDER_TYPE').val() == 'SO#') {
        $('#SO_NO').attr('readonly', false);
    } else {
        $('#SO_NO').val('').attr('readonly', true);
    }
}


//文件类型（下拉框）
function Init_PACKAGE_TYPE_ID() {
    var factoryId = $('#USERS_FACTORY_ID').combobox('getValue');
    if (factoryId == '') {
        return;
    }
    $('#PACKAGE_TYPE_ID').combobox({
        panelHeight: 100,
        valueField: 'PACKAGE_TYPE_ID',
        textField: 'PACKAGE_TYPE_ID',
        method: 'get',
        url: '/api/PACKAGE_TYPE_LIST/GetDataByFactoryId?FACTORY_ID=' + factoryId,
        editable: false,
        required: true,
        onSelect: function (record) {
            $('#PACKAGE_NO').val(record.PACKAGE_TYPE_ID + "-");
        }
    });
}
//产品类型（下拉框）
function Init_PRODUCT_TYPE_ID() {
    var factoryId = $('#USERS_FACTORY_ID').combobox('getValue');
    if (factoryId == '') {
        return;
    }
    $('#PRODUCT_TYPE_ID').combobox({
        panelHeight: 100,
        valueField: 'PRODUCT_TYPE_ID',
        textField: 'PRODUCT_TYPE_ID',
        method: 'get',
        url: '/api/PRODUCT_TYPE_LIST/GetDataByFactoryId?FACTORY_ID=' + factoryId,
        editable: false,
        required: true,
        onSelect: function (record) {
            Init_PRODUCT_PROC_TYPE_ID();
            InitComboBox(false, true);
        }
    });
}
//产品工艺（下拉框）
function Init_PRODUCT_PROC_TYPE_ID() {
    var factoryId = $('#USERS_FACTORY_ID').combobox('getValue');
    var productTypeId = $('#PRODUCT_TYPE_ID').combobox('getValue');
    if (factoryId == '' || productTypeId == '') {
        return;
    }
    $('#PRODUCT_PROC_TYPE_ID').combobox({
        panelHeight: 100,
        valueField: 'PRODUCT_PROC_TYPE_ID',
        textField: 'PRODUCT_PROC_TYPE_ID',
        method: 'get',
        url: '/api/PRODUCT_PROC_TYPE_LIST/GetDataByProductTypeId?FACTORY_ID=' + factoryId + '&PRODUCT_TYPE_ID=' + productTypeId,
        editable: false,
        required: true,
        onSelect: function (record) {
            Init_BATTERY_PARTNO();
            Init_PROJECT_CODE();
            Init_BATTERY_MODEL();
            Init_CUSTOMER_CODE();
            InitComboBox(false, false);
        }
    });
}
//品种（下拉框）
function Init_BATTERY_MODEL() {
    var factoryId = $('#USERS_FACTORY_ID').combobox('getValue');
    var productTypeId = $('#PRODUCT_TYPE_ID').combobox('getValue');
    var productProcTypeId = $('#PRODUCT_PROC_TYPE_ID').combobox('getValue');
    if (factoryId == '' || productTypeId == '' || productProcTypeId == '') {
        return;
    }
    $('#BATTERY_MODEL').combobox({
        panelHeight: 100,
        valueField: 'PRODUCT_MODEL_ID',
        textField: 'PRODUCT_MODEL_ID',
        method: 'get',
        url: '/api/PRODUCT_MODEL_LIST/GetDataByType?FACTORY_ID=' + factoryId + '&PRODUCT_TYPE_ID=' + productTypeId + '&PRODUCT_PROC_TYPE_ID=' + productProcTypeId,
        editable: true,
        filter: function (q, row) { // q是你输入的值，row是数据集合
            var opts = $(this).combobox('options');
            return row[opts.textField].toUpperCase().indexOf(q.toUpperCase()) == 0; // 同一转换成小写做比较，==0匹配首位，>=0匹配所有
        },
        onChange: function (newValue, oldValue) {
            if (newValue != null) {
                var x = $('#BATTERY_MODEL').combobox('getData');
                if (x == null) {
                    return;
                }
                var flag = false;
                $.each(x, function (i) {
                    if (x[i].PRODUCT_MODEL_ID.toUpperCase() == newValue.toUpperCase()) {
                        $('#BATTERY_MODEL').combobox('setValue', x[i].PRODUCT_MODEL_ID);
                        flag = true;
                    }
                });
                if (flag) {
                    $('#alert_BATTERY_MODEL').hide();
                } else {
                    $('#alert_BATTERY_MODEL').show();
                }
            }
        }
    });
}
function Valid_BATTERY_MODEL() {
    var productModelId = $('#BATTERY_MODEL').combobox('getValue');
    var factoryId = $('#USERS_FACTORY_ID').combobox('getValue');
    var productTypeId = $('#PRODUCT_TYPE_ID').combobox('getValue');
    var productProcTypeId = $('#PRODUCT_PROC_TYPE_ID').combobox('getValue');
    if (productModelId == '' || factoryId == '' || productTypeId == '' || productProcTypeId == '') {
        return false;
    }
    var j = {
        'PRODUCT_MODEL_ID': productModelId.toUpperCase(),
        'FACTORY_ID': factoryId,
        'PRODUCT_TYPE_ID': productTypeId,
        'PRODUCT_PROC_TYPE_ID': productProcTypeId
    };
    var r = false;
    $.ajax({
        type: 'get',
        url: '/api/PRODUCT_MODEL_LIST/GetDataValidateId',
        data: j,
        dataType: 'json',
        async: false,
        success: function (data) {
            if (data) {
                r = true;
            } else {
                r = false;
            }
        }
    });
    return r;
}
//电池料号
function Init_BATTERY_PARTNO() {
    var factoryId = $('#USERS_FACTORY_ID').combobox('getValue');
    var productTypeId = $('#PRODUCT_TYPE_ID').combobox('getValue');
    var productProcTypeId = $('#PRODUCT_PROC_TYPE_ID').combobox('getValue');
    if (factoryId == '' || productTypeId == '' || productProcTypeId == '') {
        return;
    }
    $('#BATTERY_PARTNO').combobox({
        panelHeight: 100,
        valueField: 'MATERIAL_PN_ID',
        textField: 'MATERIAL_PN_ID',
        method: 'get',
        url: '/api/MATERIAL_PN_LIST/GetDataByCategoryId?FACTORY_ID=' + factoryId + '&PRODUCT_TYPE_ID=' + productTypeId + '&PRODUCT_PROC_TYPE_ID=' + productProcTypeId + '&MATERIAL_CATEGORY_ID=GC-S',
        editable: true,
        filter: function (q, row) { // q是你输入的值，row是数据集合
            var opts = $(this).combobox('options');
            return row[opts.textField].toUpperCase().indexOf(q.toUpperCase()) == 0; // 同一转换成小写做比较，==0匹配首位，>=0匹配所有
        },
        onChange: function (newValue, oldValue) {
            if (newValue != null) {
                var x = $('#BATTERY_PARTNO').combobox('getData');
                if (x == null) {
                    return;
                }
                var flag = false;
                $.each(x, function (i) {
                    if (x[i].MATERIAL_PN_ID.toUpperCase() == newValue.toUpperCase()) {
                        $('#BATTERY_PARTNO').combobox('setValue', x[i].MATERIAL_PN_ID);
                        flag = true;
                    }
                });
                if (flag) {
                    $('#alert_BATTERY_PARTNO').hide();
                } else {
                    $('#alert_BATTERY_PARTNO').show();
                }
            }
        }
    });
}
function Valid_BATTERY_PARTNO() {
    var batteryPartNo = $('#BATTERY_PARTNO').combobox('getValue');
    var factoryId = $('#USERS_FACTORY_ID').combobox('getValue');
    var productTypeId = $('#PRODUCT_TYPE_ID').combobox('getValue');
    var productProcTypeId = $('#PRODUCT_PROC_TYPE_ID').combobox('getValue');
    if (batteryPartNo == '' || factoryId == '' || productTypeId == '' || productProcTypeId == '') {
        return false;
    }
    var j = {
        'MATERIAL_PN_ID': batteryPartNo.toUpperCase(),
        'FACTORY_ID': factoryId,
        'PRODUCT_TYPE_ID': productTypeId,
        'PRODUCT_PROC_TYPE_ID': productProcTypeId,
        'MATERIAL_CATEGORY_ID': 'GC-S'
    };
    var r = false;
    $.ajax({
        type: 'get',
        url: '/api/MATERIAL_PN_LIST/GetDataValidateId',
        data: j,
        dataType: 'json',
        async: false,
        success: function (data) {

            if (data) {
                r = true;
            } else {
                r = false;
            }

        }
    });
    return r;
}
//项目代码（下拉框）
function Init_PROJECT_CODE() {
    var factoryId = $('#USERS_FACTORY_ID').combobox('getValue');
    var productTypeId = $('#PRODUCT_TYPE_ID').combobox('getValue');
    var productProcTypeId = $('#PRODUCT_PROC_TYPE_ID').combobox('getValue');
    if (factoryId == '' || productTypeId == '' || productProcTypeId == '') {
        return;
    }
    $('#PROJECT_CODE').combobox({
        panelHeight: 100,
        valueField: 'PROJ_CODE_ID',
        textField: 'PROJ_CODE_ID',
        method: 'get',
        url: '/api/PROJ_CODE_LIST/GetDataByType?FACTORY_ID=' + factoryId + '&PRODUCT_TYPE_ID=' + productTypeId + '&PRODUCT_PROC_TYPE_ID=' + productProcTypeId,
        editable: true,
        filter: function (q, row) { // q是你输入的值，row是数据集合
            var opts = $(this).combobox('options');
            return row[opts.textField].toUpperCase().indexOf(q.toUpperCase()) == 0; // 同一转换成小写做比较，==0匹配首位，>=0匹配所有
        },
        onChange: function (newValue, oldValue) {
            if (newValue != null) {
                var x = $('#PROJECT_CODE').combobox('getData');
                if (x == null) {
                    return;
                }
                var flag = false;
                $.each(x, function (i) {
                    if (x[i].PROJ_CODE_ID.toUpperCase() == newValue.toUpperCase()) {
                        $('#PROJECT_CODE').combobox('setValue', x[i].PROJ_CODE_ID);
                        flag = true;
                    }
                });
                if (flag) {
                    $('#alert_PROJECT_CODE').hide();
                } else {
                    $('#alert_PROJECT_CODE').show();
                }
            }
        }
    });
}
function Valid_PROJECT_CODE() {
    var projCodeId = $('#PROJECT_CODE').combobox('getValue');
    var factoryId = $('#USERS_FACTORY_ID').combobox('getValue');
    var productTypeId = $('#PRODUCT_TYPE_ID').combobox('getValue');
    var productProcTypeId = $('#PRODUCT_PROC_TYPE_ID').combobox('getValue');
    if (projCodeId == '' || factoryId == '' || productTypeId == '' || productProcTypeId == '') {
        return false;
    }
    var j = {
        'PROJ_CODE_ID': projCodeId.toUpperCase(),
        'FACTORY_ID': factoryId,
        'PRODUCT_TYPE_ID': productTypeId,
        'PRODUCT_PROC_TYPE_ID': productProcTypeId
    };
    var r = false;
    $.ajax({
        type: 'get',
        url: '/api/PROJ_CODE_LIST/GetDataValidateId',
        data: j,
        dataType: 'json',
        async: false,
        success: function (data) {
            if (data) {
                r = true;
            } else {
                r = false;
            }

        }
    });
    return r;
}
//客户代码（下拉框）
function Init_CUSTOMER_CODE() {
    var factoryId = $('#USERS_FACTORY_ID').combobox('getValue');
    var productTypeId = $('#PRODUCT_TYPE_ID').combobox('getValue');
    var productProcTypeId = $('#PRODUCT_PROC_TYPE_ID').combobox('getValue');
    if (factoryId == '' || productTypeId == '' || productProcTypeId == '') {
        return;
    }
    $('#CUSTOMER_CODE').combobox({
        panelHeight: 100,
        valueField: 'CUSTOMER_CODE_ID',
        textField: 'CUSTOMER_CODE_ID',
        method: 'get',
        url: '/api/CUSTOMER_CODE_LIST/GetDataByType?FACTORY_ID=' + factoryId + '&PRODUCT_TYPE_ID=' + productTypeId + '&PRODUCT_PROC_TYPE_ID=' + productProcTypeId,
        editable: true,
        filter: function (q, row) { // q是你输入的值，row是数据集合
            var opts = $(this).combobox('options');
            return row[opts.textField].toUpperCase().indexOf(q.toUpperCase()) == 0; // 同一转换成小写做比较，==0匹配首位，>=0匹配所有
        },
        onChange: function (newValue, oldValue) {
            if (newValue != null) {
                var x = $('#CUSTOMER_CODE').combobox('getData');
                if (x == null) {
                    return;
                }
                var flag = false;
                $.each(x, function (i) {
                    if (x[i].CUSTOMER_CODE_ID.toUpperCase() == newValue.toUpperCase()) {
                        $('#CUSTOMER_CODE').combobox('setValue', x[i].CUSTOMER_CODE_ID);
                        flag = true;
                    }
                });
                if (flag) {
                    $('#alert_CUSTOMER_CODE').hide();
                } else {
                    $('#alert_CUSTOMER_CODE').show();
                }
            }
        }
    });
}
function Valid_CUSTOMER_CODE() {
    var customerCodeId = $('#CUSTOMER_CODE').combobox('getValue');
    var factoryId = $('#USERS_FACTORY_ID').combobox('getValue');
    var productTypeId = $('#PRODUCT_TYPE_ID').combobox('getValue');
    var productProcTypeId = $('#PRODUCT_PROC_TYPE_ID').combobox('getValue');
    if (customerCodeId == '' || factoryId == '' || productTypeId == '' || productProcTypeId == '') {
        return false;
    }
    var j = {
        'CUSTOMER_CODE_ID': customerCodeId.toUpperCase(),
        'FACTORY_ID': factoryId,
        'PRODUCT_TYPE_ID': productTypeId,
        'PRODUCT_PROC_TYPE_ID': productProcTypeId
    };
    var r = false;
    $.ajax({
        type: 'get',
        url: '/api/CUSTOMER_CODE_LIST/GetDataValidateId',
        data: j,
        dataType: 'json',
        async: false,
        success: function (data) {
            if (data) {
                r = true;
            } else {
                r = false;
            }

        }
    });
    return r;
}
//审批流程
function Init_APPROVE_FLOW_ID() {
    var factoryId = $('#USERS_FACTORY_ID').combobox('getValue');
    if (factoryId == '') {
        return;
    }
    $('#APPROVE_FLOW_ID').combobox({
        panelHeight: 100,
        valueField: 'WF_SET_NUM',
        textField: 'WF_SET_NAME',
        method: 'get',
        url: '/api/WF_SET/GetDataByFactoryId?FACTORY_ID=' + factoryId + '&queryStr=',
        editable: false
    });
}

function InitComboBox(typeCondition, othersCondition) {

    $('#PRODUCT_PROC_TYPE_ID').combobox('readonly', typeCondition);
    $('#BATTERY_MODEL').combobox('readonly', othersCondition);
    $('#BATTERY_PARTNO').combobox('readonly', othersCondition);
    $('#PROJECT_CODE').combobox('readonly', othersCondition);
    $('#CUSTOMER_CODE').combobox('readonly', othersCondition);
    //$('#APPROVE_FLOW_ID').combobox('readonly', othersCondition);

}

//自动生成版本号
function Init_VERSION_NO() {
    //$('#PACKAGE_NO').change(function () {
    //    var factoryId = $('#FACTORY_ID').val();
    //    if (factoryId == '') { return; }
    //    var num = $(this).val();
    //    $.ajax({
    //        type: 'get',
    //        url: '/api/PACKAGE_BASE_INFO/GenerateVersion',
    //        data: {
    //            'factoryId': factoryId,
    //            'packageNo': num.toUpperCase()
    //        },
    //        dataType: 'json',
    //        success: function (data) {
    //            $('#VERSION_NO').val(data);
    //        }
    //    });
    //});
    $('#btn_GenerateVersionNO').click(function () {
        var factoryId = $('#FACTORY_ID').val();
        if (factoryId == '') { return; }
        var num = $('#PACKAGE_NO').val();
        $.ajax({
            type: 'get',
            url: '/api/PACKAGE_BASE_INFO/GenerateVersion',
            data: {
                'factoryId': factoryId,
                'packageNo': num.toUpperCase()
            },
            dataType: 'json',
            success: function (data) {
                $('#VERSION_NO').val(data);
            }
        });
    })
}

function InitWf() {
    var j = {
        'PACKAGE_NO': $('#PACKAGE_NO').val(),
        'FACTORY_ID': $('#FACTORY_ID').val(),
        'VERSION_NO': $('#VERSION_NO').val()
    };
    $.ajax({
        type: 'get',
        url: '/api/Preview/HasBeginWf',
        data: j,
        dataType: 'json',
        success: function (data) {
            if (data) { $('#APPROVE_FLOW_ID').combobox('readonly', true); }
            else { $('#APPROVE_FLOW_ID').combobox('readonly', false); }
        }
    });
}