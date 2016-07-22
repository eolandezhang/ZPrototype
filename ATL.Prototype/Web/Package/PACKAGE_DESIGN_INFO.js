var editIndex_PACKAGE_DESIGN_INFO = undefined;
var addOrEdit_PACKAGE_DESIGN_INFO = null;
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
    Init_Table_PACKAGE_DESIGN_INFO(factoryId, packageNo, versionNo, queryStr);
    Dialog_PACKAGE_DESIGN_INFO();
    InitQueryStr();
    InitTabs("设计信息", packageNo, factoryId, versionNo, productTypeId, produceProcTypeId);
    //Init_tt(factoryId, packageNo, versionNo);

    MATERIAL_TYPE_ID_();

    Init_btn_ANODE_STUFF_ID('ANODE_STUFF_ID', 'A-001', '阳极材料');
    Init_btn_ANODE_STUFF_ID('CATHODE_STUFF_ID', 'C-001', '阴极材料');
    Init_btn_ANODE_STUFF_ID('ANODE_FOIL_ID', 'A-002', '阳极集流体材料');
    Init_btn_ANODE_STUFF_ID('CATHODE_FOIL_ID', 'C-002', '阴极集流体材料');
    Init_btn_ANODE_STUFF_ID('SEPARATOR_ID', 'S-001', '隔离膜材料');
    Init_btn_ANODE_STUFF_ID('ELECTROLYTE_ID', 'E-001', '电解液配方');
    Init_btn_ANODE_FORMULA_ID('ANODE_FORMULA_ID', 'AMIX', '阳极配方');
    Init_btn_ANODE_FORMULA_ID('CATHODE_FORMULA_ID', 'CMIX', '阴极配方');
    $('.checkInfo').attr('checked', false);
    Init_AllCheckbox();
});
function Table_PACKAGE_DESIGN_INFO(data) {
    $('#Table_PACKAGE_DESIGN_INFO').datagrid({
        title: '',
        singleSelect: true, //只能选择单行
        width: '966',
        height: '460',
        fitColumns: false,
        autoRowHeight: false,
        toolbar: [{
            text: '新增',
            iconCls: 'icon-add',
            handler: function () {
                addOrEdit_PACKAGE_DESIGN_INFO = 'add';
                $('.checkInfo').attr('disabled', true);
                GROUP_NO_PACKAGE_DESIGN_INFO();
                $('#GROUP_NO_PACKAGE_DESIGN_INFO').combobox('readonly', false);
                Init_MATERIAL_PN_ID('ANODE_STUFF_ID', 'A-001');
                Init_MATERIAL_PN_ID('CATHODE_STUFF_ID', 'C-001');
                Init_MATERIAL_PN_ID('ANODE_FOIL_ID', 'A-002');
                Init_MATERIAL_PN_ID('CATHODE_FOIL_ID', 'C-002');
                Init_MATERIAL_PN_ID('SEPARATOR_ID', 'S-001');
                Init_MATERIAL_PN_ID('ELECTROLYTE_ID', 'E-001');
                Init_RECIPE_ID('ANODE_FORMULA_ID', 'AMIX');
                Init_RECIPE_ID('CATHODE_FORMULA_ID', 'CMIX');                
                $('#CELL_CAP').val('').attr('readonly', false);
                $('#BEG_VOL').val('').attr('readonly', false);
                $('#END_VOL').val('').attr('readonly', false);                
                $('#ANODE_COATING_WEIGHT').val('').attr('readonly', false);
                $('#ANODE_DENSITY').val('').attr('readonly', false);               
                $('#CATHODE_STUFF_ID').val('').attr('readonly', true);                
                $('#CATHODE_COATING_WEIGHT').val('').attr('readonly', false);
                $('#CATHODE_DENSITY').val('').attr('readonly', false);                
                $('#INJECTION_QTY').val('').attr('readonly', false);
                $('#LIQUID_PER').val('').attr('readonly', false);
                $('#MODEL_DESC').val('').attr('readonly', false);
                $('#VALID_FLAG_PACKAGE_DESIGN_INFO').val(1);
                $('#DESIGN_DATE').datebox('setValue', dateFormat(new Date(), "mm/dd/yyyy"));
                $('#UPDATE_USER_PACKAGE_DESIGN_INFO').val('').attr('readonly', false);
                $('#UPDATE_DATE_PACKAGE_DESIGN_INFO').val('').attr('readonly', false);
                $('#ANODE_THICKNESS').val('').attr('readonly', false);
                $('#CATHODE_THICKNESS').val('').attr('readonly', false);
                $('#UPDATE_USER_PACKAGE_DESIGN_INFO').attr('readonly', true).css('border', 'none');
                $('#UPDATE_DATE_PACKAGE_DESIGN_INFO').attr('readonly', true).css('border', 'none');
                $('#Dialog_PACKAGE_DESIGN_INFO').dialog('open');
                PACKAGE_NO_forInit();
            }
        }, {
            text: '修改',
            iconCls: 'icon-edit',
            handler: function () {
                addOrEdit_PACKAGE_DESIGN_INFO = 'edit';
                $('.checkInfo').attr('disabled', false);
                $('.checkInfo').attr('checked', false);
                var x = $('#Table_PACKAGE_DESIGN_INFO').datagrid('getSelected');
                if (x == null) return;
                GROUP_NO_PACKAGE_DESIGN_INFO();
                Init_MATERIAL_PN_ID('ANODE_STUFF_ID', 'A-001');
                Init_MATERIAL_PN_ID('CATHODE_STUFF_ID', 'C-001');
                Init_MATERIAL_PN_ID('ANODE_FOIL_ID', 'A-002');
                Init_MATERIAL_PN_ID('CATHODE_FOIL_ID', 'C-002');
                Init_MATERIAL_PN_ID('SEPARATOR_ID', 'S-001');
                Init_MATERIAL_PN_ID('ELECTROLYTE_ID', 'E-001');
                Init_RECIPE_ID('ANODE_FORMULA_ID', 'AMIX');
                Init_RECIPE_ID('CATHODE_FORMULA_ID', 'CMIX');                
                $('#GROUP_NO_PACKAGE_DESIGN_INFO').combobox('setValue', x.GROUP_NO);
                $('#GROUP_NO_PACKAGE_DESIGN_INFO').combobox('readonly', true);
                $('#CELL_CAP').val(x.CELL_CAP);
                $('#BEG_VOL').val(x.BEG_VOL);
                $('#END_VOL').val(x.END_VOL);
                $('#ANODE_STUFF_ID').combobox('setValue', x.ANODE_STUFF_ID);
                $('#ANODE_FORMULA_ID').combobox('setValue', x.ANODE_FORMULA_ID);
                $('#ANODE_COATING_WEIGHT').val(x.ANODE_COATING_WEIGHT);
                $('#ANODE_DENSITY').val(x.ANODE_DENSITY);
                $('#ANODE_FOIL_ID').combobox('setValue', x.ANODE_FOIL_ID);
                $('#CATHODE_STUFF_ID').combobox('setValue', x.CATHODE_STUFF_ID);
                $('#CATHODE_FORMULA_ID').combobox('setValue', x.CATHODE_FORMULA_ID);
                $('#CATHODE_COATING_WEIGHT').val(x.CATHODE_COATING_WEIGHT);
                $('#CATHODE_DENSITY').val(x.CATHODE_DENSITY);
                $('#CATHODE_FOIL_ID').combobox('setValue', x.CATHODE_FOIL_ID);
                $('#SEPARATOR_ID').combobox('setValue', x.SEPARATOR_ID);
                $('#ELECTROLYTE_ID').combobox('setValue', x.ELECTROLYTE_ID);
                $('#INJECTION_QTY').val(x.INJECTION_QTY);
                $('#LIQUID_PER').val(x.LIQUID_PER);
                $('#MODEL_DESC').val(x.MODEL_DESC);
                $('#VALID_FLAG_PACKAGE_DESIGN_INFO').val(x.VALID_FLAG);
                $('#DESIGN_DATE').datebox('setValue', dateFormat(x.DESIGN_DATE, "mm/dd/yyyy"));
                $('#UPDATE_USER_PACKAGE_DESIGN_INFO').val(x.UPDATE_USER);
                $('#UPDATE_DATE_PACKAGE_DESIGN_INFO').val(x.UPDATE_DATE);
                $('#ANODE_THICKNESS').val(x.ANODE_THICKNESS);
                $('#CATHODE_THICKNESS').val(x.CATHODE_THICKNESS);
                $('#UPDATE_USER_PACKAGE_DESIGN_INFO').attr('readonly', true).css('border', 'none');
                $('#UPDATE_DATE_PACKAGE_DESIGN_INFO').attr('readonly', true).css('border', 'none');
                $('#Dialog_PACKAGE_DESIGN_INFO').dialog('open');
                PACKAGE_NO_forInit();
            }
        }, {
            text: '删除',
            iconCls: 'icon-cancel',
            handler: function () {
                Deleterow_PACKAGE_DESIGN_INFO();
            }
        }, {
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                $('#Table_PACKAGE_DESIGN_INFO').datagrid('endEdit', editIndex_PACKAGE_DESIGN_INFO);
                var changedRow = $('#Table_PACKAGE_DESIGN_INFO').datagrid('getChanges');
                if (changedRow.length > 0) {
                    for (var i = 0; i < changedRow.length; i++) {
                        Edit_Cell_PACKAGE_DESIGN_INFO(changedRow[i]);
                    }
                }
                editIndex_PACKAGE_DESIGN_INFO = undefined;
                $('#Table_PACKAGE_DESIGN_INFO').datagrid('acceptChanges');
            }
        }, {
            text: '取消',
            iconCls: 'icon-undo',
            handler: function () {
                $('#Table_PACKAGE_DESIGN_INFO').datagrid('rejectChanges');
            }
        }],
        frozenColumns: [[
            {
                field: 'GROUP_NO', title: '组别', align: 'left', width: 30,
                styler: function (value, row, index) { return 'color:blue'; }
            }
        ]],
        columns: [[
            {
                field: 'CELL_CAP', title: '电池容量', align: 'left', width: 55,
                editor: {
                    type: 'validatebox',
                    options: {
                        required: false,
                        validType: 'number'
                    }
                }
            },
            {
                field: 'BEG_VOL', title: '起始电压', align: 'left', width: 55,
                editor: {
                    type: 'validatebox',
                    options: {
                        required: false,
                        validType: 'number'
                    }
                }
            },
            {
                field: 'END_VOL', title: '截至电压', align: 'left', width: 55,
                editor: {
                    type: 'validatebox',
                    options: {
                        required: false,
                        validType: 'number'
                    }
                }
            },
            {
                field: 'ANODE_STUFF_ID', title: '阳极材料', align: 'left', width: 130,
                editor: {
                    type: 'combobox',
                    options: {
                        valueField: 'MATERIAL_PN_ID',
                        textField: 'MATERIAL_PN_ID',
                        url: '/api/MATERIAL_PN_LIST/GetDataQuery?MATERIAL_TYPE_GRP_NUM=A-001&PRODUCT_TYPE_ID=' + productTypeId + '&PRODUCT_PROC_TYPE_ID=' + produceProcTypeId + '&FACTORY_ID=' + factoryId + '&MATERIAL_TYPE_ID=&MATERIAL_PN_ID=&MATERIAL_PN_NAME=&MATERIAL_PN_DESC=&queryStr=',
                        method: 'get',
                        required: false
                    }
                }
            },
            {
                field: 'ANODE_FORMULA_ID', title: '阳极配方', align: 'left', width: 130,
                editor: {
                    type: 'combobox',
                    options: {
                        valueField: 'RECIPE_ID',
                        textField: 'RECIPE_ID',
                        url: '/api/RECIPE_LIST/GetDataQuery?RECIPE_TYPE_ID=AMIX&PRODUCT_TYPE_ID=' + productTypeId + '&PRODUCT_PROC_TYPE_ID=' + produceProcTypeId + '&FACTORY_ID=' + factoryId + '&RECIPE_ID=&RECIPE_NAME=&RECIPE_DESC=&queryStr=',
                        method: 'get',
                        required: false
                    }
                }
            },
            {
                field: 'ANODE_COATING_WEIGHT', title: '阳极涂布重量', align: 'left', width: 80,
                editor: {
                    type: 'validatebox',
                    options: {
                        required: false,
                        validType: 'number'
                    }
                }
            },
            {
                field: 'ANODE_DENSITY', title: '阳极压实密度', align: 'left', width: 80,
                editor: {
                    type: 'validatebox',
                    options: {
                        required: false,
                        validType: 'number'
                    }
                }
            },
            {
                field: 'ANODE_FOIL_ID', title: '阳极集流体材料', align: 'left', width: 130,
                editor: {
                    type: 'combobox',
                    options: {
                        valueField: 'MATERIAL_PN_ID',
                        textField: 'MATERIAL_PN_ID',
                        url: '/api/MATERIAL_PN_LIST/GetDataQuery?MATERIAL_TYPE_GRP_NUM=A-002&PRODUCT_TYPE_ID=' + productTypeId + '&PRODUCT_PROC_TYPE_ID=' + produceProcTypeId + '&FACTORY_ID=' + factoryId + '&MATERIAL_TYPE_ID=&MATERIAL_PN_ID=&MATERIAL_PN_NAME=&MATERIAL_PN_DESC=&queryStr=',
                        method: 'get',
                        required: false
                    }
                }
            },
            {
                field: 'ANODE_THICKNESS', title: '阳极集流体厚度', align: 'left', width: 90,
                editor: {
                    type: 'validatebox',
                    options: {
                        required: false,
                        validType: 'number'
                    }
                }
            },
            {
                field: 'CATHODE_STUFF_ID', title: '阴极材料', align: 'left', width: 130,
                editor: {
                    type: 'combobox',
                    options: {
                        valueField: 'MATERIAL_PN_ID',
                        textField: 'MATERIAL_PN_ID',
                        url: '/api/MATERIAL_PN_LIST/GetDataQuery?MATERIAL_TYPE_GRP_NUM=C-001&PRODUCT_TYPE_ID=' + productTypeId + '&PRODUCT_PROC_TYPE_ID=' + produceProcTypeId + '&FACTORY_ID=' + factoryId + '&MATERIAL_TYPE_ID=&MATERIAL_PN_ID=&MATERIAL_PN_NAME=&MATERIAL_PN_DESC=&queryStr=',
                        method: 'get',
                        required: false
                    }
                }
            }, {
                field: 'CATHODE_FORMULA_ID', title: '阴极配方', align: 'left', width: 130,
                editor: {
                    type: 'combobox',
                    options: {
                        valueField: 'RECIPE_ID',
                        textField: 'RECIPE_ID',
                        url: '/api/RECIPE_LIST/GetDataQuery?RECIPE_TYPE_ID=CMIX&PRODUCT_TYPE_ID=' + productTypeId + '&PRODUCT_PROC_TYPE_ID=' + produceProcTypeId + '&FACTORY_ID=' + factoryId + '&RECIPE_ID=&RECIPE_NAME=&RECIPE_DESC=&queryStr=',
                        method: 'get',
                        required: false
                    }
                }
            },
            {
                field: 'CATHODE_COATING_WEIGHT', title: '阴极涂布重量', align: 'left', width: 90,
                editor: {
                    type: 'validatebox',
                    options: {
                        required: false,
                        validType: 'number'
                    }
                }
            },
            {
                field: 'CATHODE_DENSITY', title: '阴极压实密度', align: 'left', width: 90,
                editor: {
                    type: 'validatebox',
                    options: {
                        required: false,
                        validType: 'number'
                    }
                }
            },
            {
                field: 'CATHODE_FOIL_ID', title: '阴极集流体材料', align: 'left', width: 130,
                editor: {
                    type: 'combobox',
                    options: {
                        valueField: 'MATERIAL_PN_ID',
                        textField: 'MATERIAL_PN_ID',
                        url: '/api/MATERIAL_PN_LIST/GetDataQuery?MATERIAL_TYPE_GRP_NUM=C-002&PRODUCT_TYPE_ID=' + productTypeId + '&PRODUCT_PROC_TYPE_ID=' + produceProcTypeId + '&FACTORY_ID=' + factoryId + '&MATERIAL_TYPE_ID=&MATERIAL_PN_ID=&MATERIAL_PN_NAME=&MATERIAL_PN_DESC=&queryStr=',
                        method: 'get',
                        required: false
                    }
                }
            },
            {
                field: 'CATHODE_THICKNESS', title: '阴极集流体厚度', align: 'left', width: 90,
                editor: {
                    type: 'validatebox',
                    options: {
                        required: false,
                        validType: 'number'
                    }
                }
            },
            {
                field: 'SEPARATOR_ID', title: '隔离膜材料', align: 'left', width: 130,
                editor: {
                    type: 'combobox',
                    options: {
                        valueField: 'MATERIAL_PN_ID',
                        textField: 'MATERIAL_PN_ID',
                        url: '/api/MATERIAL_PN_LIST/GetDataQuery?MATERIAL_TYPE_GRP_NUM=S-001&PRODUCT_TYPE_ID=' + productTypeId + '&PRODUCT_PROC_TYPE_ID=' + produceProcTypeId + '&FACTORY_ID=' + factoryId + '&MATERIAL_TYPE_ID=&MATERIAL_PN_ID=&MATERIAL_PN_NAME=&MATERIAL_PN_DESC=&queryStr=',
                        method: 'get',
                        required: false
                    }
                }
            },
            {
                field: 'ELECTROLYTE_ID', title: '电解液配方', align: 'left', width: 130,
                editor: {
                    type: 'combobox',
                    options: {
                        valueField: 'MATERIAL_PN_ID',
                        textField: 'MATERIAL_PN_ID',
                        url: '/api/MATERIAL_PN_LIST/GetDataQuery?MATERIAL_TYPE_GRP_NUM=E-001&PRODUCT_TYPE_ID=' + productTypeId + '&PRODUCT_PROC_TYPE_ID=' + produceProcTypeId + '&FACTORY_ID=' + factoryId + '&MATERIAL_TYPE_ID=&MATERIAL_PN_ID=&MATERIAL_PN_NAME=&MATERIAL_PN_DESC=&queryStr=',
                        method: 'get',
                        required: false
                    }
                }
            },
            {
                field: 'INJECTION_QTY', title: '注液量', align: 'left', width: 70,
                editor: {
                    type: 'validatebox',
                    options: {
                        required: false,
                        validType: 'number'
                    }
                }
            },
            {
                field: 'LIQUID_PER', title: '保液系数', align: 'left', width: 70,
                editor: {
                    type: 'validatebox',
                    options: {
                        required: false,
                        validType: 'number'
                    }
                }
            },
            {
                field: 'MODEL_DESC', title: '补充说明', align: 'left', width: 70,
                editor: {
                    type: 'validatebox',
                    options: {
                        required: false,
                        validType: ['maxLength[25]']
                    }
                }
            },
            { field: 'DESIGN_DATE', title: '设计日期', align: 'left', width: 100, editor: 'datebox' },
            { field: 'UPDATE_USER', title: '最后修改人', align: 'left', width: 70 },
            { field: 'UPDATE_DATE', title: '最后修改日期', align: 'left', width: 120 },
            { field: 'VALID_FLAG', title: '启用', align: 'left', width: 40, editor: { type: 'checkbox', options: { on: '1', off: '0' } } }
        ]],
        onClickRow: function (rowIndex, rowData) {
            editrow_PACKAGE_DESIGN_INFO(rowIndex);

            var editors = $('#Table_PACKAGE_DESIGN_INFO').datagrid('getEditors', rowIndex);
            $(editors[0].target)[0].focus(); $(editors[0].target).select();

            //电池容量0
            editors[0].target
                .bind('click', function () { $(editors[0].target).select(); })
                .bind('focus', function () { $(this).css('background', 'yellow'); })
                .bind('blur', function () { $(this).css('background', 'white'); });
            //起始电压1
            editors[1].target
                .bind('click', function () { $(editors[1].target).select(); })
                .bind('focus', function () { $(this).css('background', 'yellow'); })
                .bind('blur', function () { $(this).css('background', 'white'); });
            //截至电压2
            editors[2].target
                .bind('click', function () { $(editors[2].target).select(); })
                .bind('focus', function () { $(this).css('background', 'yellow'); })
                .bind('blur', function () { $(this).css('background', 'white'); });
            //阳极涂布重量5
            editors[5].target
                .bind('click', function () { $(editors[5].target).select(); })
                .bind('focus', function () { $(this).css('background', 'yellow'); })
                .bind('blur', function () { $(this).css('background', 'white'); });
            //阳极压实密度6
            editors[6].target
                .bind('click', function () { $(editors[6].target).select(); })
                .bind('focus', function () { $(this).css('background', 'yellow'); })
                .bind('blur', function () { $(this).css('background', 'white'); });
            //阳极集流体厚度8
            editors[8].target
                .bind('click', function () { $(editors[8].target).select(); })
                .bind('focus', function () { $(this).css('background', 'yellow'); })
                .bind('blur', function () { $(this).css('background', 'white'); });
            //阴极涂布重量11
            editors[11].target
                .bind('click', function () { $(editors[11].target).select(); })
                .bind('focus', function () { $(this).css('background', 'yellow'); })
                .bind('blur', function () { $(this).css('background', 'white'); });
            //阴极压实密度12
            editors[12].target
                .bind('click', function () { $(editors[12].target).select(); })
                .bind('focus', function () { $(this).css('background', 'yellow'); })
                .bind('blur', function () { $(this).css('background', 'white'); });
            //阴极集流体厚度14
            editors[14].target
                .bind('click', function () { $(editors[14].target).select(); })
                .bind('focus', function () { $(this).css('background', 'yellow'); })
                .bind('blur', function () { $(this).css('background', 'white'); });
            //注液量17
            editors[17].target
                .bind('click', function () { $(editors[17].target).select(); })
                .bind('focus', function () { $(this).css('background', 'yellow'); })
                .bind('blur', function () { $(this).css('background', 'white'); });
            //保液系数18
            editors[18].target
                .bind('click', function () { $(editors[18].target).select(); })
                .bind('focus', function () { $(this).css('background', 'yellow'); })
                .bind('blur', function () { $(this).css('background', 'white'); });
            //补充说明 19
            editors[19].target
                .bind('click', function () { $(editors[19].target).select(); })
                .bind('focus', function () { $(this).css('background', 'yellow'); })
                .bind('blur', function () { $(this).css('background', 'white'); });
            ////阳极材料
            //editors[3].target
            //    .bind('click', function () { Row_Init_btn_ANODE_STUFF_ID($(this), 4, rowIndex, 'A-001', '阳极材料'); })
            //    .bind('focus', function () { $(this).css('background', 'yellow'); })
            //    .bind('blur', function () { $(this).css('background', 'white'); })
            //    .bind('keyup', function () { $(this).val('') });
            ////阴极材料
            //editors[9].target
            //    .bind('click', function () { Row_Init_btn_ANODE_STUFF_ID($(this), 10, rowIndex, 'C-001', '阴极材料'); })
            //    .bind('focus', function () { $(this).css('background', 'yellow'); })
            //    .bind('blur', function () { $(this).css('background', 'white'); })
            //    .bind('keyup', function () { $(this).val('') });
            ////.attr('readonly', true);
            ////阳极集流体材料
            //editors[7].target
            //    .bind('click', function () { Row_Init_btn_ANODE_STUFF_ID($(this), 8, rowIndex, 'A-002', '阳极集流体材料'); })
            //    .bind('focus', function () { $(this).css('background', 'yellow'); })
            //    .bind('blur', function () { $(this).css('background', 'white'); })
            //    .bind('keyup', function () { $(this).val('') });
            ////.attr('readonly', true);
            ////阴极集流体材料
            //editors[13].target
            //    .bind('click', function () { Row_Init_btn_ANODE_STUFF_ID($(this), 14, rowIndex, 'C-002', '阴极集流体材料'); })
            //    .bind('focus', function () { $(this).css('background', 'yellow'); })
            //    .bind('blur', function () { $(this).css('background', 'white'); })
            //    .bind('keyup', function () { $(this).val('') });
            ////.attr('readonly', true);
            ////隔离膜材料
            //editors[15].target
            //    .bind('click', function () { Row_Init_btn_ANODE_STUFF_ID($(this), 16, rowIndex, 'S-001', '隔离膜材料'); })
            //    .bind('focus', function () { $(this).css('background', 'yellow'); })
            //    .bind('blur', function () { $(this).css('background', 'white'); })
            //    .bind('keyup', function () { $(this).val('') });
            ////.attr('readonly', true);
            ////电解液配方
            //editors[16].target
            //    .bind('click', function () { Row_Init_btn_ANODE_STUFF_ID($(this), 17, rowIndex, 'E-001', '电解液配方'); })
            //    .bind('focus', function () { $(this).css('background', 'yellow'); })
            //    .bind('blur', function () { $(this).css('background', 'white'); })
            //    .bind('keyup', function () { $(this).val('') });
            ////.attr('readonly', true);
            ////阳极配方
            //editors[4].target
            //    .bind('click', function () { Row_Init_btn_ANODE_FORMULA_ID($(this), 5, rowIndex, 'AMIX', '阳极配方'); })
            //    .bind('focus', function () { $(this).css('background', 'yellow'); })
            //    .bind('blur', function () { $(this).css('background', 'white'); })
            //    .bind('keyup', function () { $(this).val('') });
            ////.attr('readonly', true);
            ////阴极配方
            //editors[10].target
            //    .bind('click', function () { Row_Init_btn_ANODE_FORMULA_ID($(this), 11, rowIndex, 'CMIX', '阴极配方'); })
            //    .bind('focus', function () { $(this).css('background', 'yellow'); })
            //    .bind('blur', function () { $(this).css('background', 'white'); })
            //    .bind('keyup', function () { $(this).val('') });
            ////.attr('readonly', true);
        },
        onDblClickRow: function (rowIndex, rowData) { },
        rowStyler: function (index, row) { },
        onBeforeEdit: function (index, row) {
            editIndex_PACKAGE_DESIGN_INFO = index;
            row.editing = true;
            $('#Table_PACKAGE_DESIGN_INFO').datagrid('refreshRow', index);
        },
        onAfterEdit: function (index, row) {
            row.editing = false;
            $('#Table_PACKAGE_DESIGN_INFO').datagrid('refreshRow', index);
        },
        onCancelEdit: function (index, row) {
            row.editing = false;
            $('#Table_PACKAGE_DESIGN_INFO').datagrid('refreshRow', index);
        }
    });

    $('#Table_PACKAGE_DESIGN_INFO').datagrid("loadData", data).datagrid('acceptChanges');


}
function editrow_PACKAGE_DESIGN_INFO(index) {
    if (editIndex_PACKAGE_DESIGN_INFO != undefined)
        $('#Table_PACKAGE_DESIGN_INFO').datagrid('endEdit', editIndex_PACKAGE_DESIGN_INFO);
    $('#Table_PACKAGE_DESIGN_INFO').datagrid('beginEdit', index).datagrid('selectRow', index);
}
function Deleterow_PACKAGE_DESIGN_INFO() {
    var row = $('#Table_PACKAGE_DESIGN_INFO').datagrid('getSelected');
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
            Delete_PACKAGE_DESIGN_INFO(row);
        }
    });
}
function Init_Table_PACKAGE_DESIGN_INFO(factoryId, packageNo, versionNo, queryStr) {
    $.ajax({
        type: 'get',
        url: '/api/PACKAGE_DESIGN_INFO/GetData',
        data: {
            'factoryId': factoryId,
            'packageNo': packageNo,
            'versionNo': versionNo,
            'queryStr': queryStr
        },
        dataType: 'json',
        success: function (data) {
            Table_PACKAGE_DESIGN_INFO(data);
        }
    });
}
function Dialog_PACKAGE_DESIGN_INFO() {
    $('#Dialog_PACKAGE_DESIGN_INFO').dialog({
        toolbar: [{
            text: '保存',
            iconCls: 'icon-save',
            handler: function () {
                if (addOrEdit_PACKAGE_DESIGN_INFO == 'add') {
                    Add_PACKAGE_DESIGN_INFO();
                }
                else if (addOrEdit_PACKAGE_DESIGN_INFO == 'edit') {
                    Edit_PACKAGE_DESIGN_INFO();
                }
            }
        }, {
            text: '开始查询',
            iconCls: 'icon-search',
            handler: function () {
                Init_Table_PACKAGE_DESIGN_INFO_Search();
            }
        }]
    });
}
function Validate_PACKAGE_DESIGN_INFO() {
    if (!
        ($('#CELL_CAP').validatebox('isValid') &&
        $('#BEG_VOL').validatebox('isValid') &&
        $('#END_VOL').validatebox('isValid') &&
        $('#ANODE_COATING_WEIGHT').validatebox('isValid') &&
        $('#CATHODE_COATING_WEIGHT').validatebox('isValid') &&
        $('#ANODE_DENSITY').validatebox('isValid') &&
        $('#CATHODE_DENSITY').validatebox('isValid') &&
        $('#ANODE_THICKNESS').validatebox('isValid') &&
        $('#CATHODE_THICKNESS').validatebox('isValid') &&
        $('#INJECTION_QTY').validatebox('isValid') &&
        $('#LIQUID_PER').validatebox('isValid')

        )) {
        $.messager.show({
            title: '消息',
            msg: '输入不正确',
            showType: 'show'
        });
        return false;
    }
    if (
        $('#GROUP_NO_PACKAGE_DESIGN_INFO').combobox('getText') == '' ||        
        $('#ANODE_STUFF_ID').combobox('getText') == '' ||
        $('#CATHODE_STUFF_ID').combobox('getText') == '' ||
        $('#ANODE_FORMULA_ID').combobox('getText') == '' ||
        $('#CATHODE_FORMULA_ID').combobox('getText') == '' ||
        $('#ANODE_FOIL_ID').combobox('getText') == '' ||
        $('#CATHODE_FOIL_ID').combobox('getText') == '' ||
        $('#SEPARATOR_ID').combobox('getText') == '' ||
        $('#ELECTROLYTE_ID').combobox('getText') == ''
        ) {
        $.messager.show({
            title: '消息',
            msg: '请填写',
            showType: 'show'
        });
        return false;
    }
    var groupNo = $('#GROUP_NO_PACKAGE_DESIGN_INFO').combobox('getValues') + '';
    if (groupNo.length == 0) {
        $.messager.show({
            title: '消息',
            msg: '请选择分组',
            showType: 'show'
        });
        return false;
    }
    return true;
}
function Add_PACKAGE_DESIGN_INFO() {
    var groupNo = $('#GROUP_NO_PACKAGE_DESIGN_INFO').combobox('getValues') + '';
    if (!Validate_PACKAGE_DESIGN_INFO()) return;
    var j = {
        'PACKAGE_NO': packageNo,        
        'VERSION_NO': versionNo,
        'FACTORY_ID': factoryId,
        'GROUP_NO': groupNo,
        'CELL_CAP': $('#CELL_CAP').val(),
        'BEG_VOL': $('#BEG_VOL').val(),
        'END_VOL': $('#END_VOL').val(),
        'ANODE_STUFF_ID': $('#ANODE_STUFF_ID').combobox('getValue'),
        'ANODE_FORMULA_ID': $('#ANODE_FORMULA_ID').combobox('getValue'),
        'ANODE_COATING_WEIGHT': $('#ANODE_COATING_WEIGHT').val(),
        'ANODE_DENSITY': $('#ANODE_DENSITY').val(),
        'ANODE_FOIL_ID': $('#ANODE_FOIL_ID').combobox('getValue'),
        'CATHODE_STUFF_ID': $('#CATHODE_STUFF_ID').combobox('getValue'),
        'CATHODE_FORMULA_ID': $('#CATHODE_FORMULA_ID').combobox('getValue'),
        'CATHODE_COATING_WEIGHT': $('#CATHODE_COATING_WEIGHT').val(),
        'CATHODE_DENSITY': $('#CATHODE_DENSITY').val(),
        'CATHODE_FOIL_ID': $('#CATHODE_FOIL_ID').combobox('getValue'),
        'SEPARATOR_ID': $('#SEPARATOR_ID').combobox('getValue'),
        'ELECTROLYTE_ID': $('#ELECTROLYTE_ID').combobox('getValue'),
        'INJECTION_QTY': $('#INJECTION_QTY').val(),
        'LIQUID_PER': $('#LIQUID_PER').val(),
        'MODEL_DESC': $('#MODEL_DESC').val(),
        'VALID_FLAG': $('#VALID_FLAG_PACKAGE_DESIGN_INFO').val(),
        'DESIGN_DATE': $('#DESIGN_DATE').val(),
        'UPDATE_USER': $('#UPDATE_USER_PACKAGE_DESIGN_INFO').val(),
        'UPDATE_DATE': $('#UPDATE_DATE_PACKAGE_DESIGN_INFO').val(),
        'ANODE_THICKNESS': $('#ANODE_THICKNESS').val(),
        'CATHODE_THICKNESS': $('#CATHODE_THICKNESS').val()
    };
    $.ajax({
        type: 'post',
        url: '/api/PACKAGE_DESIGN_INFO/PostBatchAdd',
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
                Init_Table_PACKAGE_DESIGN_INFO(factoryId, packageNo, versionNo, queryStr);
                $('#Dialog_PACKAGE_DESIGN_INFO').dialog('close');
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
function Edit_PACKAGE_DESIGN_INFO() {
    var groupNo = $('#GROUP_NO_PACKAGE_DESIGN_INFO').combobox('getValue');
    if (groupNo == '') {
        return;
    }
    if (!Validate_PACKAGE_DESIGN_INFO()) return;
    var j = {
        'PACKAGE_NO': packageNo,
        'VERSION_NO': versionNo,
        'FACTORY_ID': factoryId,
        'GROUP_NO': $('#GROUP_NO_PACKAGE_DESIGN_INFO').combobox('getValue'),
        'CELL_CAP': $('#CELL_CAP').val(),
        'BEG_VOL': $('#BEG_VOL').val(),
        'END_VOL': $('#END_VOL').val(),
        'ANODE_STUFF_ID': $('#ANODE_STUFF_ID').combobox('getValue'),
        'ANODE_FORMULA_ID': $('#ANODE_FORMULA_ID').combobox('getValue'),
        'ANODE_COATING_WEIGHT': $('#ANODE_COATING_WEIGHT').val(),
        'ANODE_DENSITY': $('#ANODE_DENSITY').val(),
        'ANODE_FOIL_ID': $('#ANODE_FOIL_ID').combobox('getValue'),
        'CATHODE_STUFF_ID': $('#CATHODE_STUFF_ID').combobox('getValue'),
        'CATHODE_FORMULA_ID': $('#CATHODE_FORMULA_ID').combobox('getValue'),
        'CATHODE_COATING_WEIGHT': $('#CATHODE_COATING_WEIGHT').val(),
        'CATHODE_DENSITY': $('#CATHODE_DENSITY').val(),
        'CATHODE_FOIL_ID': $('#CATHODE_FOIL_ID').combobox('getValue'),
        'SEPARATOR_ID': $('#SEPARATOR_ID').combobox('getValue'),
        'ELECTROLYTE_ID': $('#ELECTROLYTE_ID').combobox('getValue'),
        'INJECTION_QTY': $('#INJECTION_QTY').val(),
        'LIQUID_PER': $('#LIQUID_PER').val(),
        'MODEL_DESC': $('#MODEL_DESC').val(),
        'VALID_FLAG': $('#VALID_FLAG_PACKAGE_DESIGN_INFO').val(),
        'DESIGN_DATE': $('#DESIGN_DATE').datebox('getValue'),
        'UPDATE_USER': $('#UPDATE_USER_PACKAGE_DESIGN_INFO').val(),
        'UPDATE_DATE': $('#UPDATE_DATE_PACKAGE_DESIGN_INFO').val(),
        'ANODE_THICKNESS': $('#ANODE_THICKNESS').val(),
        'CATHODE_THICKNESS': $('#CATHODE_THICKNESS').val(),
        'PRODUCT_TYPE_ID': productTypeId,
        'PRODUCT_PROC_TYPE_ID': produceProcTypeId
    };

    $.ajax({
        type: 'post',
        url: '/api/PACKAGE_DESIGN_INFO/PostEdit',
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
                Init_Table_PACKAGE_DESIGN_INFO(factoryId, packageNo, versionNo, queryStr);
                $('#Dialog_PACKAGE_DESIGN_INFO').dialog('close');
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
function Edit_Cell_PACKAGE_DESIGN_INFO(row) {
    var j = {
        'PACKAGE_NO': row.PACKAGE_NO,
        'GROUP_NO': row.GROUP_NO,
        'VERSION_NO': row.VERSION_NO,
        'FACTORY_ID': row.FACTORY_ID,
        'CELL_CAP': row.CELL_CAP,
        'BEG_VOL': row.BEG_VOL,
        'END_VOL': row.END_VOL,
        'ANODE_STUFF_ID': row.ANODE_STUFF_ID,
        'ANODE_FORMULA_ID': row.ANODE_FORMULA_ID,
        'ANODE_COATING_WEIGHT': row.ANODE_COATING_WEIGHT,
        'ANODE_DENSITY': row.ANODE_DENSITY,
        'ANODE_FOIL_ID': row.ANODE_FOIL_ID,
        'CATHODE_STUFF_ID': row.CATHODE_STUFF_ID,
        'CATHODE_FORMULA_ID': row.CATHODE_FORMULA_ID,
        'CATHODE_COATING_WEIGHT': row.CATHODE_COATING_WEIGHT,
        'CATHODE_DENSITY': row.CATHODE_DENSITY,
        'CATHODE_FOIL_ID': row.CATHODE_FOIL_ID,
        'SEPARATOR_ID': row.SEPARATOR_ID,
        'ELECTROLYTE_ID': row.ELECTROLYTE_ID,
        'INJECTION_QTY': row.INJECTION_QTY,
        'LIQUID_PER': row.LIQUID_PER,
        'MODEL_DESC': row.MODEL_DESC,
        'VALID_FLAG': row.VALID_FLAG,
        'DESIGN_DATE': dateFormat(row.DESIGN_DATE, "mm/dd/yyyy"),
        'UPDATE_USER': row.UPDATE_USER,
        'UPDATE_DATE': row.UPDATE_DATE,
        'ANODE_THICKNESS': row.ANODE_THICKNESS,
        'CATHODE_THICKNESS': row.CATHODE_THICKNESS,
        'PRODUCT_TYPE_ID': productTypeId,
        'PRODUCT_PROC_TYPE_ID': produceProcTypeId
    };
    $.ajax({
        type: 'post',
        url: '/api/PACKAGE_DESIGN_INFO/PostEdit',
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
function Delete_PACKAGE_DESIGN_INFO(row) {
    var j = {
        'PACKAGE_NO': row.PACKAGE_NO,
        'GROUP_NO': row.GROUP_NO,
        'VERSION_NO': row.VERSION_NO,
        'FACTORY_ID': row.FACTORY_ID
    };
    $.ajax({
        type: 'post',
        url: '/api/PACKAGE_DESIGN_INFO/PostDelete',
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
                Init_Table_PACKAGE_DESIGN_INFO(factoryId, packageNo, versionNo, queryStr);                
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
//物料编号下拉框
function Init_MATERIAL_PN_ID(controlID, grpNum) {
    var j = {
        'MATERIAL_TYPE_GRP_NUM': grpNum,
        'PRODUCT_TYPE_ID': productTypeId,
        'PRODUCT_PROC_TYPE_ID': produceProcTypeId,
        'FACTORY_ID': factoryId,
        'MATERIAL_TYPE_ID': "",
        'MATERIAL_PN_ID': "",
        'MATERIAL_PN_NAME': "",
        'MATERIAL_PN_DESC': "",
        'queryStr': ''
    };

    $.ajax({
        type: 'get',
        url: '/api/MATERIAL_PN_LIST/GetDataQuery',
        data: j,
        dataType: 'json',
        async: false,
        success: function (data) {
            $('#' + controlID).combobox({
                panelHeight: 100,
                valueField: 'MATERIAL_PN_ID',
                textField: 'MATERIAL_PN_ID',
                data: data,
                editable: true,
                required: true,
                filter: function (q, row) { // q是你输入的值，row是数据集合
                    var opts = $(this).combobox('options');
                    return row[opts.textField].toUpperCase().indexOf(q.toUpperCase()) == 0; // 同一转换成小写做比较，==0匹配首位，>=0匹配所有
                },
                onChange: function (newValue, oldValue) {
                    if (newValue != null) {
                        var x = $('#' + controlID).combobox('getData');
                        if (x == null) {
                            return;
                        }
                        var flag = false;
                        $.each(x, function (i) {
                            if (x[i].MATERIAL_PN_ID.toUpperCase() == newValue.toUpperCase()) {
                                $('#' + controlID).combobox('setValue', x[i].MATERIAL_PN_ID);
                                flag = true;
                            }
                        });
                        if (flag) {
                            $('#alert_' + controlID).hide();
                        } else {
                            $('#alert_' + controlID).show();
                        }
                    }
                }
            });

        }
    });
}
//物料编号查询框
function MATERIAL_TYPE_ID_() {
    $('#MATERIAL_TYPE_ID_').combogrid({
        idField: 'MATERIAL_TYPE_ID',
        textField: 'MATERIAL_TYPE_DESC',
        editable: false,
        required: false,
        multiple: false,
        panelWidth: 400,
        panelHeight: 250,
        columns: [[
            { field: 'MATERIAL_TYPE_ID', title: '类型', width: 80 },
            { field: 'MATERIAL_TYPE_NAME', title: '英文名', width: 100 },
            { field: 'MATERIAL_TYPE_DESC', title: '中文名', width: 200 }
        ]]
    });
}
function Table_MATERIAL_PN_ID_(controlID) {
    $('#Table_MATERIAL_PN_ID_').datagrid({
        title: '',
        singleSelect: true,
        width: '300',
        height: '236',
        columns: [[
            { field: 'MATERIAL_PN_ID', title: '物料PN', width: 260, align: 'left' }
        ]],
        onClickRow: function (rowIndex, rowData) {
            $('#' + controlID).combobox('setValue', rowData.MATERIAL_PN_ID);
            $('#Dialog_MATERIAL_PN_ID_').dialog('close');
        }
    });
}
function Init_btn_ANODE_STUFF_ID(controlID, grpNum, title) {
    $('#btn_' + controlID).click(function () {
        Init_MATERIAL_TYPE_ID_(grpNum);
        Table_MATERIAL_PN_ID_(controlID);
        $('#Table_MATERIAL_PN_ID_').datagrid('loadData', []);
        $('#MATERIAL_TYPE_ID_').combogrid('clear');
        $('#Search_MATERIAL_PN_ID_').val('');
        $('#Search_MATERIAL_PN_NAME_').val('');
        $('#Search_MATERIAL_PN_DESC_').val('');
        $('#btn_Search').click(function () {
            var j = {
                'MATERIAL_TYPE_GRP_NUM': grpNum,
                'PRODUCT_TYPE_ID': productTypeId,
                'PRODUCT_PROC_TYPE_ID': produceProcTypeId,
                'FACTORY_ID': factoryId,
                'MATERIAL_TYPE_ID': $('#MATERIAL_TYPE_ID_').combogrid('getValue'),
                'MATERIAL_PN_ID': $('#Search_MATERIAL_PN_ID_').val(),
                'MATERIAL_PN_NAME': $('#Search_MATERIAL_PN_NAME_').val(),
                'MATERIAL_PN_DESC': $('#Search_MATERIAL_PN_DESC_').val(),
                'queryStr': ''
            };

            $.ajax({
                type: 'get',
                url: '/api/MATERIAL_PN_LIST/GetDataQuery',
                data: j,
                dataType: 'json',
                cache: false,
                success: function (data) {
                    Table_MATERIAL_PN_ID_(controlID);
                    $('#Table_MATERIAL_PN_ID_').datagrid('loadData', data);
                }
            });
        });
        $('#Dialog_MATERIAL_PN_ID_').dialog({ 'title': title }).dialog('open');
    });
}
function Init_MATERIAL_TYPE_ID_(grpNum) {
    var j = {
        'MATERIAL_TYPE_GRP_NUM': grpNum,
        'PRODUCT_TYPE_ID': productTypeId,
        'PRODUCT_PROC_TYPE_ID': produceProcTypeId,
        'FACTORY_ID': factoryId,
        'queryStr': ''
    };
    $.ajax({
        type: 'get',
        url: '/api/MATERIAL_TYPE_GRP_LIST/GetDataByGrpId',
        data: j,
        dataType: 'json',
        success: function (data) {
            $('#MATERIAL_TYPE_ID_').combogrid('grid').datagrid('loadData', data);
        }
    });
}
//配方编号下拉框
function Init_RECIPE_ID(controlID, typeID) {
    var j = {
        'RECIPE_TYPE_ID': typeID,
        'PRODUCT_TYPE_ID': productTypeId,
        'PRODUCT_PROC_TYPE_ID': produceProcTypeId,
        'FACTORY_ID': factoryId,
        'RECIPE_ID': "",
        'RECIPE_NAME': "",
        'RECIPE_DESC': "",
        'queryStr': ''
    };

    $.ajax({
        type: 'get',
        url: '/api/RECIPE_LIST/GetDataQuery',
        data: j,
        dataType: 'json',
        async: false,
        success: function (data) {
            $('#' + controlID).combobox({
                panelHeight: 100,
                valueField: 'RECIPE_ID',
                textField: 'RECIPE_ID',
                data: data,
                editable: true,
                required: true,
                filter: function (q, row) { // q是你输入的值，row是数据集合
                    var opts = $(this).combobox('options');
                    return row[opts.textField].toUpperCase().indexOf(q.toUpperCase()) == 0; // 同一转换成小写做比较，==0匹配首位，>=0匹配所有
                },
                onChange: function (newValue, oldValue) {
                    if (newValue != null) {
                        var x = $('#' + controlID).combobox('getData');
                        if (x == null) {
                            return;
                        }
                        var flag = false;
                        $.each(x, function (i) {
                            if (x[i].RECIPE_ID.toUpperCase() == newValue.toUpperCase()) {
                                $('#' + controlID).combobox('setValue', x[i].RECIPE_ID);
                                flag = true;
                            }
                        });
                        if (flag) {
                            $('#alert_' + controlID).hide();
                        } else {
                            $('#alert_' + controlID).show();
                        }
                    }
                }
            });
        }
    });
}
//配方编号查询框
function Table_RECIPE_ID(controlID) {
    $('#Table_RECIPE_ID').datagrid({
        title: '',
        singleSelect: true,
        width: '300',
        height: '236',
        columns: [[
            { field: 'RECIPE_ID', title: '编号', width: 260, align: 'left' }
        ]],
        onClickRow: function (rowIndex, rowData) {
            $('#' + controlID).combobox('setValue', rowData.RECIPE_ID);
            $('#Dialog_RECIPE_ID').dialog('close');
        }
    });
}
function Init_btn_ANODE_FORMULA_ID(controlID, typeID, title) {
    $('#btn_' + controlID).click(function () {
        Table_RECIPE_ID(controlID);
        $('#Table_RECIPE_ID').datagrid('loadData', []);
        $('#Search_RECIPE_ID').val('');
        $('#Search_RECIPE_NAME').val('');
        $('#Search_RECIPE_DESC').val('');
        $('#btn_Search_RECIPE_ID').click(function () {
            var j = {
                'RECIPE_TYPE_ID': typeID,
                'PRODUCT_TYPE_ID': productTypeId,
                'PRODUCT_PROC_TYPE_ID': produceProcTypeId,
                'FACTORY_ID': factoryId,
                'RECIPE_ID': $('#Search_RECIPE_ID').val(),
                'RECIPE_NAME': $('#Search_RECIPE_NAME').val(),
                'RECIPE_DESC': $('#Search_RECIPE_DESC').val(),
                'queryStr': ''
            };

            $.ajax({
                type: 'get',
                url: '/api/RECIPE_LIST/GetDataQuery',
                data: j,
                dataType: 'json',
                cache: false,
                success: function (data) {
                    Table_RECIPE_ID(controlID);
                    $('#Table_RECIPE_ID').datagrid('loadData', data);
                }
            });
        });
        $('#Dialog_RECIPE_ID').dialog({ 'title': title }).dialog('open');
    });
}
//查询
function InitQueryStr() {
    Search_GROUP_NO();
    Init_btnSearch();
}
function Search_GROUP_NO() {
    $('#Search_GROUP_NO').combobox({
        panelHeight: 100,
        valueField: 'GROUP_NO',
        textField: 'GROUP_NO',
        multiple: true,
        method: 'get',
        url: '/api/PACKAGE_GROUPS/GetData?factoryId=' + factoryId + '&packageNo=' + packageNo + '&versionNo=' + versionNo + '&queryStr=',
        editable: true
    });
}
function Init_btnSearch() {
    $('#btnSearch').click(function () {
        var qs = $('#Search_GROUP_NO').combobox('getValues');
        queryStr = '';
        if (qs.length != 0) {
            var strqs = [];
            for (var i = 0; i < qs.length; i++) {
                strqs[i] = "'" + qs[i] + "'";
            }
            queryStr = "AND GROUP_NO IN (" + strqs + ")";
        }
        Init_Table_PACKAGE_DESIGN_INFO(factoryId, packageNo, versionNo, queryStr);
    });
}
//初始化下拉框
function GROUP_NO_PACKAGE_DESIGN_INFO() {    
    $('#GROUP_NO_PACKAGE_DESIGN_INFO').combobox({
        panelHeight: 100,
        valueField: 'GROUP_NO',
        textField: 'GROUP_NO',
        multiple: false,
        method: 'get',
        url: '/api/PACKAGE_GROUPS/GetGroupsNotInDesignInfo?factoryId=' + factoryId + '&packageNo=' + packageNo + '&versionNo=' + versionNo,
        editable: false
    });
}
//选择Package来初始化设计信息
function PACKAGE_NO_forInit() {
    $('#PACKAGE_NO_forInit').combobox({
        panelHeight: 100,
        valueField: 'PACKAGE_NO',
        textField: 'PACKAGE_NO',
        multiple: false,
        method: 'get',
        url: '/api/PACKAGE_BASE_INFO/GetDataByFactoryId?factoryId=' + factoryId,
        editable: true,
        onSelect: function (record) {
            VERSION_NO_forInit(record.PACKAGE_NO);
        },
        filter: function (q, row) { // q是你输入的值，row是数据集合
            var opts = $(this).combobox('options');
            return row[opts.textField].toUpperCase().indexOf(q.toUpperCase()) == 0; // 同一转换成小写做比较，==0匹配首位，>=0匹配所有
        },
        onChange: function (newValue, oldValue) {
            if (newValue != '') {
                var x = $('#PACKAGE_NO_forInit').combobox('getData');
                $.each(x, function (i) {
                    if (x[i].PACKAGE_NO.toUpperCase() == newValue.toUpperCase()) {
                        $('#PACKAGE_NO_forInit').combobox('setValue', x[i].PACKAGE_NO);
                        VERSION_NO_forInit(x[i].PACKAGE_NO);
                        return;
                    }
                })
            }
        }
    });
}
//选择版本
function VERSION_NO_forInit(packageNoForInit) {
    $('#VERSION_NO_forInit').combobox({
        panelHeight: 100,
        valueField: 'VERSION_NO',
        textField: 'VERSION_NO',
        multiple: false,
        method: 'get',
        url: '/api/PACKAGE_BASE_INFO/GetDataByPackageNo?factoryId=' + factoryId + '&PackageNo=' + packageNoForInit,
        editable: true,
        onSelect: function (record) {
            GROUP_NO_forInit(packageNoForInit, record.VERSION_NO);
        },
        filter: function (q, row) { // q是你输入的值，row是数据集合
            var opts = $(this).combobox('options');
            return row[opts.textField].toUpperCase().indexOf(q.toUpperCase()) == 0; // 同一转换成小写做比较，==0匹配首位，>=0匹配所有
        },
        onChange: function (newValue, oldValue) {
            if (newValue != '') {
                var x = $('#VERSION_NO_forInit').combobox('getData');
                $.each(x, function (i) {
                    if (x[i].VERSION_NO.toUpperCase() == newValue.toUpperCase()) {
                        $('#VERSION_NO_forInit').combobox('setValue', x[i].VERSION_NO);
                        GROUP_NO_forInit(packageNoForInit, x[i].VERSION_NO);
                        return;
                    }
                })
            }
        }
    });
}
//选择分组来初始化设计信息
function GROUP_NO_forInit(packageNoForInit, versionNoForInit) {
    $('#GROUP_NO_forInit').combobox({
        panelHeight: 100,
        valueField: 'GROUP_NO',
        textField: 'GROUP_NO',
        multiple: false,
        method: 'get',
        url: '/api/PACKAGE_GROUPS/GetData?factoryId=' + factoryId + '&packageNo=' + packageNoForInit + '&versionNo=' + versionNoForInit + '&queryStr=',
        editable: true,
        onSelect: function (record) {
            InitDesignInfo(packageNoForInit, versionNoForInit, record.GROUP_NO);
        },
        filter: function (q, row) { // q是你输入的值，row是数据集合
            var opts = $(this).combobox('options');
            return row[opts.textField].toUpperCase().indexOf(q.toUpperCase()) == 0; // 同一转换成小写做比较，==0匹配首位，>=0匹配所有
        },
        onChange: function (newValue, oldValue) {
            if (newValue != '') {
                var x = $('#GROUP_NO_forInit').combobox('getData');
                $.each(x, function (i) {
                    if (x[i].GROUP_NO.toUpperCase() == newValue.toUpperCase()) {
                        $('#GROUP_NO_forInit').combobox('setValue', x[i].GROUP_NO);
                        InitDesignInfo(packageNoForInit, versionNoForInit, x[i].GROUP_NO);
                        return;
                    }
                })
            }
        }
    });

}
function InitDesignInfo(packageNoForInit, versionNoForInit, groupNoForInit) {
    $.ajax({
        type: 'get',
        url: '/api/PACKAGE_DESIGN_INFO/GetDataById',
        data: {
            'groupNo': groupNoForInit,
            'factoryId': factoryId,
            'packageNo': packageNoForInit,
            'versionNo': versionNoForInit
        },
        dataType: 'json',
        success: function (data) {
            if (data.length == 0) {
                $('#CELL_CAP').val('');
                $('#BEG_VOL').val('');
                $('#END_VOL').val('');
                $('#ANODE_STUFF_ID').combobox('clear');
                $('#CATHODE_STUFF_ID').combobox('clear');
                $('#ANODE_FORMULA_ID').combobox('clear');
                $('#CATHODE_FORMULA_ID').combobox('clear');
                $('#ANODE_COATING_WEIGHT').val('');
                $('#CATHODE_COATING_WEIGHT').val('');
                $('#ANODE_DENSITY').val('');
                $('#CATHODE_DENSITY').val('');
                $('#ANODE_FOIL_ID').combobox('clear');
                $('#CATHODE_FOIL_ID').combobox('clear');
                $('#ANODE_THICKNESS').val('');
                $('#CATHODE_THICKNESS').val('');
                $('#SEPARATOR_ID').combobox('clear');
                $('#ELECTROLYTE_ID').combobox('clear');
                $('#INJECTION_QTY').val('');
                $('#LIQUID_PER').val('');
                $('#MODEL_DESC').val('');
                $('#DESIGN_DATE').datebox('clear');
                $('#UPDATE_USER_PACKAGE_DESIGN_INFO').val('');
                $('#UPDATE_DATE_PACKAGE_DESIGN_INFO').val('');
                $('#VALID_FLAG_PACKAGE_DESIGN_INFO').val(1);
                return;
            }
            var d = data[0];
            //$('#GROUP_NO').combobox('setValue', d.GROUP_NO);
            $('#CELL_CAP').val(d.CELL_CAP);
            $('#BEG_VOL').val(d.BEG_VOL);
            $('#END_VOL').val(d.END_VOL);
            $('#ANODE_STUFF_ID').combobox('setValue', d.ANODE_STUFF_ID);
            $('#CATHODE_STUFF_ID').combobox('setValue', d.CATHODE_STUFF_ID);
            $('#ANODE_FORMULA_ID').combobox('setValue', d.ANODE_FORMULA_ID);
            $('#CATHODE_FORMULA_ID').combobox('setValue', d.CATHODE_FORMULA_ID);
            $('#ANODE_COATING_WEIGHT').val(d.ANODE_COATING_WEIGHT);
            $('#CATHODE_COATING_WEIGHT').val(d.CATHODE_COATING_WEIGHT);
            $('#ANODE_DENSITY').val(d.ANODE_DENSITY);
            $('#CATHODE_DENSITY').val(d.CATHODE_DENSITY);
            $('#ANODE_FOIL_ID').combobox('setValue', d.ANODE_FOIL_ID);
            $('#CATHODE_FOIL_ID').combobox('setValue', d.CATHODE_FOIL_ID);
            $('#ANODE_THICKNESS').val(d.ANODE_THICKNESS);
            $('#CATHODE_THICKNESS').val(d.CATHODE_THICKNESS);
            $('#SEPARATOR_ID').combobox('setValue', d.SEPARATOR_ID);
            $('#ELECTROLYTE_ID').combobox('setValue', d.ELECTROLYTE_ID);
            $('#INJECTION_QTY').val(d.INJECTION_QTY);
            $('#LIQUID_PER').val(d.LIQUID_PER);
            $('#MODEL_DESC').val(d.MODEL_DESC);
            $('#DESIGN_DATE').datebox('setValue', dateFormat(d.DESIGN_DATE, "mm/dd/yyyy"));
        }
    });
}
//查询项目
function Init_AllCheckbox() {
    Init_CheckBox('CELL_CAP');
    Init_CheckBox('BEG_VOL');
    Init_CheckBox('END_VOL');
    Init_CheckBox('ANODE_COATING_WEIGHT');
    Init_CheckBox('CATHODE_COATING_WEIGHT');
    Init_CheckBox('ANODE_DENSITY');
    Init_CheckBox('CATHODE_DENSITY');
    Init_CheckBox('ANODE_THICKNESS');
    Init_CheckBox('CATHODE_THICKNESS');
    Init_CheckBox('INJECTION_QTY');
    Init_CheckBox('LIQUID_PER');
}
function Init_CheckBox(id) {
    $('#check_' + id).attr('checked', false);
    $('#region_' + id).hide();
    $('#check_' + id).change(function () {
        var x = $('#check_' + id).is(':checked');
        if (x) { $('#region_' + id).show(); $('#' + id + '_tolerance').val(''); }
        else $('#region_' + id).hide();
    })
}
//查询结果
function Table_PACKAGE_DESIGN_INFO_Search(data) {
    $('#Table_PACKAGE_DESIGN_INFO_Search').datagrid({
        title: '',
        singleSelect: true, //只能选择单行
        width: '730',
        height: '200',
        fitColumns: false,
        autoRowHeight: false,
        frozenColumns: [[
            {
                field: 'ACTION', width: 35,
                formatter: function (value, row, index) {
                    var openstr = '<a style=\"cursor:pointer;color:blue;\"  onclick="opendetail_PACKAGE_BASE_INFO(' + index + ')">打开</a>';
                    return openstr;
                }
            },
            { field: 'PACKAGE_NO', title: '文件编号', align: 'left', width: 120 },
            { field: 'VERSION_NO', title: '版本号', align: 'left', width: 40 },
            { field: 'GROUP_NO', title: '组别', align: 'left', width: 30 }
        ]],
        columns: [[
            { field: 'CELL_CAP', title: '电池容量', align: 'left', width: 55 },
            { field: 'BEG_VOL', title: '起始电压', align: 'left', width: 55 },
            { field: 'END_VOL', title: '截至电压', align: 'left', width: 55 },
            { field: 'ANODE_STUFF_ID', title: '阳极材料', align: 'left', width: 130 },
            { field: 'ANODE_FORMULA_ID', title: '阳极配方', align: 'left', width: 130 },
            { field: 'ANODE_COATING_WEIGHT', title: '阳极涂布重量', align: 'left', width: 80 },
            { field: 'ANODE_DENSITY', title: '阳极压实密度', align: 'left', width: 80 },
            { field: 'ANODE_FOIL_ID', title: '阳极集流体材料', align: 'left', width: 130 },
            { field: 'ANODE_THICKNESS', title: '阳极集流体厚度', align: 'left', width: 90 },
            { field: 'CATHODE_STUFF_ID', title: '阴极材料', align: 'left', width: 130 },
            { field: 'CATHODE_FORMULA_ID', title: '阴极配方', align: 'left', width: 130 },
            { field: 'CATHODE_COATING_WEIGHT', title: '阴极涂布重量', align: 'left', width: 90 },
            { field: 'CATHODE_DENSITY', title: '阴极压实密度', align: 'left', width: 90 },
            { field: 'CATHODE_FOIL_ID', title: '阴极集流体材料', align: 'left', width: 130 },
            { field: 'CATHODE_THICKNESS', title: '阴极集流体厚度', align: 'left', width: 90 },
            { field: 'SEPARATOR_ID', title: '隔离膜材料', align: 'left', width: 130 },
            { field: 'ELECTROLYTE_ID', title: '电解液配方', align: 'left', width: 130 },
            { field: 'INJECTION_QTY', title: '注液量', align: 'left', width: 70 },
            { field: 'LIQUID_PER', title: '保液系数', align: 'left', width: 70 },
            { field: 'MODEL_DESC', title: '补充说明', align: 'left', width: 70 },
            { field: 'DESIGN_DATE', title: '设计日期', align: 'left', width: 100, editor: 'datebox' },
            { field: 'UPDATE_USER', title: '最后修改人', align: 'left', width: 70 },
            { field: 'UPDATE_DATE', title: '最后修改日期', align: 'left', width: 120 },
            { field: 'VALID_FLAG', title: '启用', align: 'left', width: 40, editor: { type: 'checkbox', options: { on: '1', off: '0' } } }
        ]]
    })
}
function Init_Table_PACKAGE_DESIGN_INFO_Search() {
    var x = $('#Table_PACKAGE_DESIGN_INFO').datagrid('getSelected');
    if (x == null) return;
    if (!
        ($('#CELL_CAP_tolerance').validatebox('isValid') &&
        $('#BEG_VOL_tolerance').validatebox('isValid') &&
        $('#END_VOL_tolerance').validatebox('isValid') &&
        $('#ANODE_COATING_WEIGHT_tolerance').validatebox('isValid') &&
        $('#CATHODE_COATING_WEIGHT_tolerance').validatebox('isValid') &&
        $('#ANODE_DENSITY_tolerance').validatebox('isValid') &&
        $('#CATHODE_DENSITY_tolerance').validatebox('isValid') &&
        $('#ANODE_THICKNESS_tolerance').validatebox('isValid') &&
        $('#CATHODE_THICKNESS_tolerance').validatebox('isValid') &&
        $('#INJECTION_QTY_tolerance').validatebox('isValid') &&
        $('#LIQUID_PER_tolerance').validatebox('isValid')
        )) {
        $.messager.show({
            title: '消息',
            msg: '输入不正确',
            showType: 'show'
        });
        return;
    }
    var j = {
        'PACKAGE_NO': packageNo,
        'GROUP_NO': $('#GROUP_NO_PACKAGE_DESIGN_INFO').combobox('getValue'),
        'VERSION_NO': versionNo,
        'FACTORY_ID': factoryId,
        'CELL_CAP': $('#CELL_CAP').val(),
        'BEG_VOL': $('#BEG_VOL').val(),
        'END_VOL': $('#END_VOL').val(),
        'ANODE_STUFF_ID': $('#ANODE_STUFF_ID').combobox('getValue'),
        'ANODE_FORMULA_ID': $('#ANODE_FORMULA_ID').combobox('getValue'),
        'ANODE_COATING_WEIGHT': $('#ANODE_COATING_WEIGHT').val(),
        'ANODE_DENSITY': $('#ANODE_DENSITY').val(),
        'ANODE_FOIL_ID': $('#ANODE_FOIL_ID').combobox('getValue'),
        'CATHODE_STUFF_ID': $('#CATHODE_STUFF_ID').combobox('getValue'),
        'CATHODE_FORMULA_ID': $('#CATHODE_FORMULA_ID').combobox('getValue'),
        'CATHODE_COATING_WEIGHT': $('#CATHODE_COATING_WEIGHT').val(),
        'CATHODE_DENSITY': $('#CATHODE_DENSITY').val(),
        'CATHODE_FOIL_ID': $('#CATHODE_FOIL_ID').combobox('getValue'),
        'SEPARATOR_ID': $('#SEPARATOR_ID').combobox('getValue'),
        'ELECTROLYTE_ID': $('#ELECTROLYTE_ID').combobox('getValue'),
        'INJECTION_QTY': $('#INJECTION_QTY').val(),
        'LIQUID_PER': $('#LIQUID_PER').val(),
        'MODEL_DESC': $('#MODEL_DESC').val(),
        'VALID_FLAG': $('#VALID_FLAG_PACKAGE_DESIGN_INFO').val(),
        'DESIGN_DATE': $('#DESIGN_DATE').datebox('getValue'),
        'UPDATE_USER': $('#UPDATE_USER_PACKAGE_DESIGN_INFO').val(),
        'UPDATE_DATE': $('#UPDATE_DATE_PACKAGE_DESIGN_INFO').val(),
        'ANODE_THICKNESS': $('#ANODE_THICKNESS').val(),
        'CATHODE_THICKNESS': $('#CATHODE_THICKNESS').val(),
        'PRODUCT_TYPE_ID': productTypeId,
        'PRODUCT_PROC_TYPE_ID': produceProcTypeId,
        'CELL_CAP_tolerance': $('#CELL_CAP_tolerance').val(),
        'BEG_VOL_tolerance': $('#BEG_VOL_tolerance').val(),
        'END_VOL_tolerance': $('#END_VOL_tolerance').val(),
        'ANODE_COATING_WEIGHT_tolerance': $('#ANODE_COATING_WEIGHT_tolerance').val(),
        'ANODE_DENSITY_tolerance': $('#ANODE_DENSITY_tolerance').val(),
        'CATHODE_COATING_WEIGHT_tolerance': $('#CATHODE_COATING_WEIGHT_tolerance').val(),
        'CATHODE_DENSITY_tolerance': $('#CATHODE_DENSITY_tolerance').val(),
        'INJECTION_QTY_tolerance': $('#INJECTION_QTY_tolerance').val(),
        'LIQUID_PER_tolerance': $('#LIQUID_PER_tolerance').val(),
        'ANODE_THICKNESS_tolerance': $('#ANODE_THICKNESS_tolerance').val(),
        'CATHODE_THICKNESS_tolerance': $('#CATHODE_THICKNESS_tolerance').val(),
        'check_CELL_CAP': $('#check_CELL_CAP').is(':checked'),
        'check_BEG_VOL': $('#check_BEG_VOL').is(':checked'),
        'check_END_VOL': $('#check_END_VOL').is(':checked'),
        'check_ANODE_STUFF_ID': $('#check_ANODE_STUFF_ID').is(':checked'),
        'check_ANODE_FORMULA_ID': $('#check_ANODE_FORMULA_ID').is(':checked'),
        'check_ANODE_COATING_WEIGHT': $('#check_ANODE_COATING_WEIGHT').is(':checked'),
        'check_ANODE_DENSITY': $('#check_ANODE_DENSITY').is(':checked'),
        'check_ANODE_FOIL_ID': $('#check_ANODE_FOIL_ID').is(':checked'),
        'check_CATHODE_STUFF_ID': $('#check_CATHODE_STUFF_ID').is(':checked'),
        'check_CATHODE_FORMULA_ID': $('#check_CATHODE_FORMULA_ID').is(':checked'),
        'check_CATHODE_COATING_WEIGHT': $('#check_CATHODE_COATING_WEIGHT').is(':checked'),
        'check_CATHODE_DENSITY': $('#check_CATHODE_DENSITY').is(':checked'),
        'check_CATHODE_FOIL_ID': $('#check_CATHODE_FOIL_ID').is(':checked'),
        'check_SEPARATOR_ID': $('#check_SEPARATOR_ID').is(':checked'),
        'check_ELECTROLYTE_ID': $('#check_ELECTROLYTE_ID').is(':checked'),
        'check_INJECTION_QTY': $('#check_INJECTION_QTY').is(':checked'),
        'check_LIQUID_PER': $('#check_LIQUID_PER').is(':checked'),
        'check_ANODE_THICKNESS': $('#check_ANODE_THICKNESS').is(':checked'),
        'check_CATHODE_THICKNESS': $('#check_CATHODE_THICKNESS').is(':checked')
    };
    $.ajax({
        type: 'post',
        url: '/api/PACKAGE_DESIGN_INFO/PostDataQuery',
        data: JSON.stringify(j),
        dataType: 'json',
        contentType: 'application/json',
        success: function (data) {
            Table_PACKAGE_DESIGN_INFO_Search(data);
            $('#Table_PACKAGE_DESIGN_INFO_Search').datagrid('loadData', data);
        }
    });
}
function opendetail_PACKAGE_BASE_INFO(index) {
    $('#Table_PACKAGE_DESIGN_INFO_Search').datagrid('selectRow', index);
    var row = $('#Table_PACKAGE_DESIGN_INFO_Search').datagrid('getSelected');
    window.open('/Package/PackageDetails.aspx?packageNo=' + row.PACKAGE_NO + '&factoryId=' + row.FACTORY_ID + '&versionNo=' + row.VERSION_NO + '&productTypeId=' + row.PRODUCT_TYPE_ID + '&produceProcTypeId=' + row.PRODUCT_PROC_TYPE_ID, '_blank');
}




//初始化工序明细标签
function Init_tt(factoryId, packageNo, versionNo) {
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