var factoryId;
var packageNo;
var versionNo;
var productTypeId;
var produceProcTypeId;
var auditor = [];
var auditorName = [];
$(function () {
    packageNo = $.request.queryString["packageNo"];
    factoryId = $.request.queryString["factoryId"];
    versionNo = $.request.queryString["versionNo"];
    productTypeId = $.request.queryString["productTypeId"];
    produceProcTypeId = $.request.queryString["produceProcTypeId"];
    var ViewModel = function () {
        this.PACKAGE_NO = ko.observable(packageNo);
        this.VERSION_NO = ko.observable(versionNo);
        this.FACTORY_ID = ko.observable(factoryId);
        this.PRODUCT_TYPE_ID = ko.observable(productTypeId);
        this.PRODUCT_PROC_TYPE_ID = ko.observable(produceProcTypeId);
    }
    ko.applyBindings(new ViewModel());
    Dialog_NextStep();
    Table_PMES_USER_ID();
    Init_Table_PMES_USER_ID();
    $('#nextstep').click(function () {
        $('#Dialog_NextStep').dialog('open');
    });
    $('#modify').click(function () {
        window.location.href = '/Package/PackageDetails.aspx?packageNo=' + packageNo + '&factoryId=' + factoryId + '&versionNo=' + versionNo + '&productTypeId=' + productTypeId + '&produceProcTypeId=' + produceProcTypeId;
    });
    $('#myTab').tabs({
        onSelect: function (title) {
            switch (title) {
                case "签审记录":
                    Table_PACKAGE_WF_STEP_();
                    Init_Table_PACKAGE_WF_STEP_();
                    Table_PACKAGE_WF_STEP_AUDITOR_();
                    AuditHistory();
                    Init_CurrentWf();
                    break;
                case "文件预览":
                    break;
            }
        }
    });
    Dialog_Change();
});

function Dialog_NextStep() {
    $('#Dialog_NextStep').dialog({
        toolbar: [{
            text: '同意',
            iconCls: 'icon-ok',
            handler: function () {
                Agree();
            }
        }, {
            text: '不同意',
            iconCls: 'icon-cancel',
            handler: function () {
                Disagree();
            }
        }]
    });
}
function Table_PMES_USER_ID() {
    $('#Table_PMES_USER_ID').datagrid({
        title: '请选择下一步签审者',
        singleSelect: true,
        width: '412',
        height: '206',
        fitColumns: true,
        striped: true,
        nowrap: false,
        columns: [[
            { field: 'DEPARTMENT', title: '部门', align: 'left', width: 80 },
            { field: 'TITLE', title: '职位', align: 'left', width: 150 },
            { field: 'CNNAME', title: '姓名', align: 'left', width: 150 }
        ]],
        onClickRow: function (rowIndex, rowData) {
            if ($.inArray(rowData.PMES_USER_ID, auditor) >= 0) {
                auditor.splice($.inArray(rowData.PMES_USER_ID, auditor), 1);
                auditorName.splice($.inArray(rowData.CNNAME, auditorName), 1);
            } else {
                auditor = [];
                auditorName = [];
                auditor.push(rowData.PMES_USER_ID);
                auditorName.push(rowData.CNNAME);
            }
            var names = $.unique(auditorName).sort().join(',');
            $('#SelectedAuditor').val(names);

        }
    });
}
function Init_Table_PMES_USER_ID() {
    var j = {
        'PACKAGE_NO': packageNo,
        'VERSION_NO': versionNo,
        'FACTORY_ID': factoryId
    };
    $.ajax({
        type: 'post',
        url: '/api/Preview/PostAuditInfo',
        data: JSON.stringify(j),
        dataType: 'json',
        contentType: 'application/json',
        success: function (data) {
            auditor = [];
            auditorName = [];
            $('#CurrentStep').html('');
            $('#NextStep').html('');
            $('#AUDITOR_COMMENT').val('');
            $('#SelectedAuditor').val('');

            if (data.IS_AUDITOR == 1) {
                $('#nextstep').show();                
            } else {
                $('#nextstep').hide();
                $('#myTab').tabs('select', '签审记录');
                $('#myTab').tabs('disableTab', '文件预览');
            }
            if (data.STEP_FLAG != 'LST') {
                $('.nextAuditor').show();
            } else {
                $('.nextAuditor').hide();
            }

            if (data.CURRENT_STEP_FLAG == 'FST') {
                $('#modify').show();
            } else {
                $('#modify').hide();
            }
            $('#CurrentStep').html(data.CurrentStepName);
            $('#NextStep').html(data.NextStepName);
            if (data.Auditors != null) {
                $('#Table_PMES_USER_ID').datagrid('loadData', data.Auditors);
            }
        }
    });
}
function Agree() {
    $('#loading-mask').fadeIn();
    var user = '';
    if (auditor.length != 0) user = auditor[0];
    var j = {
        'PACKAGE_NO': packageNo,
        'VERSION_NO': versionNo,
        'FACTORY_ID': factoryId,
        'PMES_USER_ID': user,
        'AUDITOR_COMMENT': $('#AUDITOR_COMMENT').val()
    };

    $.ajax({
        type: 'get',
        url: '/api/Preview/Agree',
        data: j,
        dataType: 'json',
        success: function (data) {
            $('#loading-mask').fadeOut();
            if (data > 0) {
                $('#myTab').tabs('select', '签审记录');
                AuditHistory();
                $('#Dialog_NextStep').dialog('close');
                $.messager.show({
                    title: '消息',
                    msg: '转交完成',
                    showType: 'show'
                });
                Init_Table_PMES_USER_ID();
            } else if (data == -1) {
                $.messager.show({
                    title: '消息',
                    msg: '无权限',
                    showType: 'show'
                });
            } else if (data == -2) {
                $.messager.show({
                    title: '消息',
                    msg: '请选择签审人',
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
function Disagree() {
    var j = {
        'PACKAGE_NO': packageNo,
        'VERSION_NO': versionNo,
        'FACTORY_ID': factoryId,
        'AUDITOR_COMMENT': $('#AUDITOR_COMMENT').val()
    };
    $.ajax({
        type: 'get',
        url: '/api/Preview/Disagree',
        data: j,
        dataType: 'json',
        success: function (data) {
            if (data > 0) {
                //window.location.href = '/Package/PACKAGE_WF_STEP.aspx?packageNo=' + packageNo + '&factoryId=' + factoryId + '&versionNo=' + versionNo + '&productTypeId=' + productTypeId + '&produceProcTypeId=' + produceProcTypeId;
                $('#myTab').tabs('select', '签审记录');
                AuditHistory();
                $('#Dialog_NextStep').dialog('close');
                $.messager.show({
                    title: '消息',
                    msg: '转交完成',
                    showType: 'show'
                });
                Init_Table_PMES_USER_ID();
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

function Table_PACKAGE_WF_STEP_() {
    $('#Table_PACKAGE_WF_STEP').datagrid({
        title: '审批步骤',
        singleSelect: true,
        width: '298',
        height: '250',
        fitColumns: true,
        idField: 'PACKAGE_WF_STEP_ID',
        columns: [[
            //{ field: 'PACKAGE_WF_STEP_ID', title: '', align: 'left', width: 40 },
            { field: 'WF_SET_STEP_NAME', title: '审批步骤', align: 'left', width: 140 },
        { field: 'UPDATE_DATE', title: '时间', align: 'left', width: 120 }
        ]],
        onClickRow: function (rowIndex, rowData) {
            Init_Table_PACKAGE_WF_STEP_AUDITOR_();
        }
    });
}
function Init_Table_PACKAGE_WF_STEP_() {
    var j = {
        'PACKAGE_NO': packageNo,
        'VERSION_NO': versionNo,
        'FACTORY_ID': factoryId
    };
    $.ajax({
        type: 'get',
        url: '/api/PACKAGE_WF_STEP/GetDataByPkgId',
        data: j,
        dataType: 'json',
        async:false,
        success: function (data) {
            $('#Table_PACKAGE_WF_STEP').datagrid("loadData", data);
        }
    });
}
function Table_PACKAGE_WF_STEP_AUDITOR_() {
    $('#Table_PACKAGE_WF_STEP_AUDITOR').datagrid({
        title: '审批人',
        singleSelect: true,
        width: '964',
        height: '200',
        fitColumns: false,
        toolbar: [{
            text: '更换',
            iconCls: 'icon-add',
            handler: function () {
                var x = $('#Table_PACKAGE_WF_STEP_AUDITOR').datagrid('getSelected');
                if (x == null) return;
                Table_PMES_USER_ID_Change();
                GetWfSetAuditorByPkgWfAuditorId(x.AUDITOR_ID);
                $('#Dialog_Change').dialog('open');
            }
        }],
        columns: [[
            //{ field: 'AUDITOR_ID', title: '', align: 'left', width: 40 },
            { field: 'DEPARTMENT', title: '部门', align: 'center', width: 80 },
            { field: 'TITLE', title: '职位', align: 'left', width: 150 },
            { field: 'CNNAME', title: '姓名', align: 'left', width: 80 },
            {
                field: 'IS_AGREED', title: '审批意见', align: 'center', width: 60,
                formatter: function (value, row, index) {
                    switch (value) {
                        case '0':
                            return '待处理';
                        case '1':
                            return '同意';
                        case '2':
                            return '不同意';
                    }
                }
            },
            { field: 'AUDITOR_COMMENT', title: '备注', align: 'left', width: 200 },
            {
                field: 'AUDIT_AT', title: '签审日期', align: 'center', width: 140,
                formatter: function (value, row, index) {
                    if (row.IS_AGREED == '0') {
                        return '';
                    } else {
                        return value;
                    }
                }
            },
            {
                field: 'IS_CANCELED', title: '状态', align: 'left', width: 50,
                formatter: function (value, row, index) {
                    if (value == '1') {
                        return '已取消';
                    } else {
                        return '';
                    }
                }
            }
        ]]
    });
}
function Init_Table_PACKAGE_WF_STEP_AUDITOR_() {
    var x = $('#Table_PACKAGE_WF_STEP').datagrid('getSelected');
    if (x == null) return;
    var j = { 'PACKAGE_WF_STEP_ID': x.PACKAGE_WF_STEP_ID };
    $.ajax({
        type: 'get',
        url: '/api/PACKAGE_WF_STEP_AUDITOR/GetDataByPkgStepId',
        data: j,
        dataType: 'json',
        success: function (data) {
            $('#Table_PACKAGE_WF_STEP_AUDITOR').datagrid("loadData", data);
        }
    });
}
function Init_CurrentWf() {
    var j = {
        'PACKAGE_NO': packageNo,
        'VERSION_NO': versionNo,
        'FACTORY_ID': factoryId
    };
    $.ajax({
        type: 'get',
        url: '/api/Preview/GetCurrentPkgWfStep',
        data: j,
        dataType: 'json',
        success: function (data) {
            $('#Table_PACKAGE_WF_STEP').datagrid('selectRecord', data.PACKAGE_WF_STEP_ID);
            Init_Table_PACKAGE_WF_STEP_AUDITOR_();
        }
    });
}
function AuditHistory() {
    var j = {
        'PACKAGE_NO': packageNo,
        'VERSION_NO': versionNo,
        'FACTORY_ID': factoryId
    };
    $.ajax({
        type: 'get',
        url: '/api/Preview/AuditHistory',
        data: j,
        dataType: 'json',
        success: function (data) {
            $('#Audit_History').html(data);
        }
    });
}

function GetWfSetAuditorByPkgWfAuditorId(AUDITOR_ID) {
    var j = {
        'AUDITOR_ID': AUDITOR_ID,
        'FACTORY_ID': factoryId
    };
    $.ajax({
        type: 'get',
        url: '/api/Preview/GetWfSetAuditorByPkgWfAuditorId',
        data: j,
        dataType: 'json',
        success: function (data) {
            $('#Table_PMES_USER_ID_Change').datagrid('loadData', data);
        }
    });
}
function Table_PMES_USER_ID_Change() {
    $('#Table_PMES_USER_ID_Change').datagrid({
        title: '请选择签审者',
        singleSelect: true,
        width: '412',
        height: '306',
        fitColumns: true,
        striped: true,
        nowrap: false,
        columns: [[
            { field: 'DEPARTMENT', title: '部门', align: 'left', width: 80 },
            { field: 'TITLE', title: '职位', align: 'left', width: 150 },
            { field: 'CNNAME', title: '姓名', align: 'left', width: 150 }
        ]],
        onClickRow: function (rowIndex, rowData) {

        }
    });
}
function Dialog_Change() {
    $('#Dialog_Change').dialog({
        toolbar: [{
            text: '确认',
            iconCls: 'icon-ok',
            handler: function () {
                ChangeAuditor();
            }
        }]
    });
}
function ChangeAuditor() {
    $('#loading-mask').fadeIn();
    var x = $('#Table_PMES_USER_ID_Change').datagrid('getSelected');
    if (x == null) return;
    var y = $('#Table_PACKAGE_WF_STEP_AUDITOR').datagrid('getSelected');
    if (y == null) return;
    var j = {
        'AUDITOR_ID': y.AUDITOR_ID,
        'PMES_USER_ID': x.PMES_USER_ID
    };    
    $.ajax({
        type: 'get',
        url: '/api/Preview/ChangeAuditor',
        data: j,
        dataType: 'json',
        success: function (data) {
            $('#loading-mask').fadeOut();
            if (data > 0) {
                $.messager.show({
                    title: '消息',
                    msg: '完成',
                    showType: 'show'
                });
                Init_Table_PACKAGE_WF_STEP_AUDITOR_();
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