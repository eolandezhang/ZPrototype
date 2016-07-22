<%@ Page Title="设计信息" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PACKAGE_DESIGN_INFO.aspx.cs" Inherits="Web.Package.PACKAGE_DESIGN_INFO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Package/PACKAGE_DESIGN_INFO.js"></script>
    <script src="/Package/Tabs.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="width: 944px; margin-bottom: 2px; padding: 5px 10px; border: 1px solid #95B8E7; background-color: #fff;">
        <a class="easyui-linkbutton" data-options="iconCls:'icon-backhome'" href="/Package/PACKAGE_BASE_INFO.aspx?mid=24">返回列表</a>&nbsp;&nbsp;&nbsp;&nbsp;
        <span style="color: blue;">文件编号:</span><span data-bind="text: PACKAGE_NO"></span>&nbsp;&nbsp;<span style="color: blue;">版本:</span><span data-bind="    text: VERSION_NO"></span>
        &nbsp;&nbsp;<span style="color: blue;">厂别:</span><span data-bind="text: FACTORY_ID"></span>&nbsp;&nbsp;<span style="color: blue;">产品类型:</span><span data-bind="    text: PRODUCT_TYPE_ID"></span>&nbsp;&nbsp;<span style="color: blue;">工艺类型:</span><span data-bind="    text: PRODUCT_PROC_TYPE_ID"></span>
    </div>
    <div id="mytab" style="border-style: none none solid none;"><a class="easyui-linkbutton" id="wf" data-options="iconCls:'icon-ok'">审批流程</a></div>
    <div id="tt" class="easyui-tabs" style="width: 966px;" data-options="border:false,tools:'#mytab'">
        <div title="基本信息"></div>
        <div title="分组信息"></div>
        <div title="设计信息" style="padding: 2px 0;" data-options="selected:true">
            <div style="width: 944px; margin-bottom: 2px; padding: 10px; border: 1px solid #CCCCCC; background-color: #FFFFCC;">
                分组:<input type="text" id="Search_GROUP_NO" />&nbsp;
                <a id="btnSearch" style="cursor: pointer" class="easyui-linkbutton">查询</a>
            </div>
            <table id="Table_PACKAGE_DESIGN_INFO"></table>
            <div id="Dialog_PACKAGE_DESIGN_INFO" class="easyui-dialog" data-options="title:'对话框',modal:true,width:800,height:'450',closed:true" style="padding: 10px;">
                <div style="width: 620px; margin-bottom: 2px; padding: 5px; border: 1px solid #CCCCCC; background-color: #FFFFCC;">
                    <b>选择初始化:</b>文件编号
                            <input type="text" id="PACKAGE_NO_forInit" />
                    版本
                            <input type="text" id="VERSION_NO_forInit" class="easyui-combobox" style="width: 50px;" />
                    组别
                            <input type="text" id="GROUP_NO_forInit" class="easyui-combobox" />

                </div>
                <table cellspacing="0" cellspadding="0">
                    <tr>
                        <td></td>
                        <td style="text-align: left;">组别<b style="color: red; font-size: 15px;">*</b></td>
                        <td colspan="4">
                            <input type="text" id="GROUP_NO_PACKAGE_DESIGN_INFO" /></td>
                    </tr>
                    <tr>
                        <td>
                            <input type="checkbox" class="checkInfo" id="check_CELL_CAP" /></td>
                        <td style="text-align: left;">电池容量
                        </td>
                        <td colspan="4">
                            <input type="text" id="CELL_CAP" class="easyui-validatebox" data-options="required:false,validType:'number'" style="width: 80px;" /><span id="region_CELL_CAP">±<input type="text" style="width: 50px; background-color: #FFFFCC;" id="CELL_CAP_tolerance" class="easyui-validatebox" data-options="required:false,validType:'number'" />
                            </span>mAh
                            
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <input type="checkbox" class="checkInfo" id="check_BEG_VOL" /></td>
                        <td style="text-align: left;">起始电压
                           
                        </td>
                        <td>
                            <input type="text" id="BEG_VOL" class="easyui-validatebox" data-options="required:false,validType:'number'" style="width: 80px;" /><span id="region_BEG_VOL">±<input type="text" style="width: 50px; background-color: #FFFFCC;" id="BEG_VOL_tolerance" class="easyui-validatebox" data-options="required:false,validType:'number'" />
                            </span>V</td>
                        <td>
                            <input type="checkbox" class="checkInfo" id="check_END_VOL" /></td>
                        <td style="text-align: left;">截至电压</td>
                        <td>
                            <input type="text" id="END_VOL" class="easyui-validatebox" data-options="required:false,validType:'number'" style="width: 80px;" /><span id="region_END_VOL">±<input type="text" style="width: 50px; background-color: #FFFFCC;" id="END_VOL_tolerance" class="easyui-validatebox" data-options="required:false,validType:'number'" />
                            </span>V</td>
                    </tr>
                    <tr>
                        <td>
                            <input type="checkbox" class="checkInfo" id="check_ANODE_STUFF_ID" /></td>
                        <td style="text-align: left;">阳极材料<b id="alert_ANODE_STUFF_ID" style="color: red;">*</b></td>
                        <td>
                            <input type="text" id="ANODE_STUFF_ID" />
                            <!--查询对话框-->
                            <a class="easyui-linkbutton" id="btn_ANODE_STUFF_ID">选择</a>
                            <div id="Dialog_MATERIAL_PN_ID_" class="easyui-dialog" data-options="title:'对话框',modal:false,width:360,height:460,closed:true" style="padding: 10px;">
                                <br />
                                物料类型&nbsp;<input id="MATERIAL_TYPE_ID_" /><br />
                                物料编号&nbsp;<input id="Search_MATERIAL_PN_ID_" /><br />
                                英文名称&nbsp;<input id="Search_MATERIAL_PN_NAME_" /><br />
                                中文名称&nbsp;<input id="Search_MATERIAL_PN_DESC_" />
                                <a class="easyui-linkbutton" id="btn_Search">查询</a>
                                <div style="height: 4px; overflow: hidden;"></div>
                                <table id="Table_MATERIAL_PN_ID_"></table>
                            </div>

                        </td>
                        <td>
                            <input type="checkbox" class="checkInfo" id="check_CATHODE_STUFF_ID" /></td>
                        <td style="text-align: left;">阴极材料<b id="alert_CATHODE_STUFF_ID" style="color: red;">*</b>
                        </td>
                        <td>
                            <input type="text" id="CATHODE_STUFF_ID" />

                            <!--查询对话框-->
                            <a class="easyui-linkbutton" id="btn_CATHODE_STUFF_ID">选择</a>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <input type="checkbox" class="checkInfo" id="check_ANODE_FORMULA_ID" /></td>
                        <td style="text-align: left;">阳极配方<b id="alert_ANODE_FORMULA_ID" style="color: red;">*</b>
                        </td>
                        <td>
                            <input type="text" id="ANODE_FORMULA_ID" />
                            <a class="easyui-linkbutton" id="btn_ANODE_FORMULA_ID">选择</a>
                            <div id="Dialog_RECIPE_ID" class="easyui-dialog" data-options="title:'对话框',modal:false,width:360,height:420,closed:true" style="padding: 10px;">
                                <br />
                                配方编号&nbsp;<input id="Search_RECIPE_ID" /><br />
                                英文名称&nbsp;<input id="Search_RECIPE_NAME" /><br />
                                中文名称&nbsp;<input id="Search_RECIPE_DESC" />
                                <a class="easyui-linkbutton" id="btn_Search_RECIPE_ID">查询</a>
                                <div style="height: 4px; overflow: hidden;"></div>
                                <table id="Table_RECIPE_ID"></table>
                            </div>
                        </td>
                        <td>
                            <input type="checkbox" class="checkInfo" id="check_CATHODE_FORMULA_ID" /></td>
                        <td style="text-align: left;">阴极配方<b id="alert_CATHODE_FORMULA_ID" style="color: red;">*</b>
                        </td>
                        <td>
                            <input type="text" id="CATHODE_FORMULA_ID" />
                            <a class="easyui-linkbutton" id="btn_CATHODE_FORMULA_ID">选择</a>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <input type="checkbox" class="checkInfo" id="check_ANODE_COATING_WEIGHT" /></td>
                        <td style="text-align: left;">阳极涂布重量
                           
                        </td>
                        <td>
                            <input type="text" id="ANODE_COATING_WEIGHT" class="easyui-validatebox" data-options="required:false,validType:'number'" style="width: 80px;" /><span id="region_ANODE_COATING_WEIGHT">±<input type="text" style="width: 50px; background-color: #FFFFCC;" id="ANODE_COATING_WEIGHT_tolerance" class="easyui-validatebox" data-options="required:false,validType:'number'" />
                            </span>g/1540.25mm²</td>
                        <td>
                            <input type="checkbox" class="checkInfo" id="check_CATHODE_COATING_WEIGHT" /></td>
                        <td style="text-align: left;">阴极涂布重量
                           
                        </td>
                        <td>
                            <input type="text" id="CATHODE_COATING_WEIGHT" class="easyui-validatebox" data-options="required:false,validType:'number'" style="width: 80px;" /><span id="region_CATHODE_COATING_WEIGHT">±<input type="text" style="width: 50px; background-color: #FFFFCC;" id="CATHODE_COATING_WEIGHT_tolerance" class="easyui-validatebox" data-options="required:false,validType:'number'" />
                            </span>g/1540.25mm²</td>
                    </tr>
                    <tr>
                        <td>
                            <input type="checkbox" class="checkInfo" id="check_ANODE_DENSITY" /></td>
                        <td style="text-align: left;">阳极压实密度
                            
                        </td>
                        <td>
                            <input type="text" id="ANODE_DENSITY" class="easyui-validatebox" data-options="required:false,validType:'number'" style="width: 80px;" /><span id="region_ANODE_DENSITY">±<input type="text" style="width: 50px; background-color: #FFFFCC;" id="ANODE_DENSITY_tolerance" class="easyui-validatebox" data-options="required:false,validType:'number'" />
                            </span>g/cm³</td>
                        <td>
                            <input type="checkbox" class="checkInfo" id="check_CATHODE_DENSITY" /></td>
                        <td style="text-align: left;">阴极压实密度
                           
                        </td>
                        <td>
                            <input type="text" id="CATHODE_DENSITY" class="easyui-validatebox" data-options="required:false,validType:'number'" style="width: 80px;" /><span id="region_CATHODE_DENSITY">±<input type="text" style="width: 50px; background-color: #FFFFCC;" id="CATHODE_DENSITY_tolerance" class="easyui-validatebox" data-options="required:false,validType:'number'" />
                            </span>g/cm³</td>
                    </tr>
                    <tr>
                        <td>
                            <input type="checkbox" class="checkInfo" id="check_ANODE_FOIL_ID" /></td>
                        <td style="text-align: left;">阳极集流体材料<b id="alert_ANODE_FOIL_ID" style="color: red;">*</b>

                        </td>
                        <td>
                            <input type="text" id="ANODE_FOIL_ID" />
                            <!--查询对话框-->
                            <a class="easyui-linkbutton" id="btn_ANODE_FOIL_ID">选择</a>
                        </td>
                        <td>
                            <input type="checkbox" class="checkInfo" id="check_CATHODE_FOIL_ID" /></td>
                        <td style="text-align: left;">阴极集流体材料<b id="alert_CATHODE_FOIL_ID" style="color: red;">*</b>

                        </td>
                        <td>
                            <input type="text" id="CATHODE_FOIL_ID" />
                            <!--查询对话框-->
                            <a class="easyui-linkbutton" id="btn_CATHODE_FOIL_ID">选择</a>

                        </td>
                    </tr>
                    <tr>
                        <td>
                            <input type="checkbox" class="checkInfo" id="check_ANODE_THICKNESS" /></td>
                        <td style="text-align: left;">阳极集流体厚度
                           
                        </td>
                        <td>
                            <input type="text" id="ANODE_THICKNESS" class="easyui-validatebox" data-options="required:false,validType:'number'" style="width: 80px;" /><span id="region_ANODE_THICKNESS">±<input type="text" style="width: 50px; background-color: #FFFFCC;" id="ANODE_THICKNESS_tolerance" class="easyui-validatebox" data-options="required:false,validType:'number'" />
                            </span>mm</td>
                        <td>
                            <input type="checkbox" class="checkInfo" id="check_CATHODE_THICKNESS" /></td>
                        <td style="text-align: left;">阴极集流体厚度
                            
                        </td>
                        <td>
                            <input type="text" id="CATHODE_THICKNESS" class="easyui-validatebox" data-options="required:false,validType:'number'" style="width: 80px;" /><span id="region_CATHODE_THICKNESS">±<input type="text" style="width: 50px; background-color: #FFFFCC;" id="CATHODE_THICKNESS_tolerance" class="easyui-validatebox" data-options="required:false,validType:'number'" />
                            </span>mm</td>
                    </tr>
                    <tr>
                        <td>
                            <input type="checkbox" class="checkInfo" id="check_SEPARATOR_ID" /></td>
                        <td style="text-align: left;">隔离膜材料<b id="alert_SEPARATOR_ID" style="color: red;">*</b>

                        </td>
                        <td>
                            <input type="text" id="SEPARATOR_ID" />
                            <!--查询对话框-->
                            <a class="easyui-linkbutton" id="btn_SEPARATOR_ID">选择</a>
                        </td>
                        <td>
                            <input type="checkbox" class="checkInfo" id="check_ELECTROLYTE_ID" /></td>
                        <td style="text-align: left;">电解液配方<b id="alert_ELECTROLYTE_ID" style="color: red;">*</b>

                        </td>
                        <td>
                            <input type="text" id="ELECTROLYTE_ID" />
                            <!--查询对话框-->
                            <a class="easyui-linkbutton" id="btn_ELECTROLYTE_ID">选择</a>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <input type="checkbox" class="checkInfo" id="check_INJECTION_QTY" /></td>
                        <td style="text-align: left;">注液量                           
                        </td>
                        <td>
                            <input type="text" id="INJECTION_QTY" class="easyui-validatebox" data-options="required:false,validType:'number'" style="width: 80px;" /><span id="region_INJECTION_QTY">±<input type="text" style="width: 50px; background-color: #FFFFCC;" id="INJECTION_QTY_tolerance" class="easyui-validatebox" data-options="required:false,validType:'number'" />
                            </span>g</td>
                        <td>
                            <input type="checkbox" class="checkInfo" id="check_LIQUID_PER" /></td>
                        <td style="text-align: left;">保液系数                            
                        </td>
                        <td>
                            <input type="text" id="LIQUID_PER" class="easyui-validatebox" data-options="required:false,validType:'number'" style="width: 80px;" /><span id="region_LIQUID_PER">±<input type="text" style="width: 50px; background-color: #FFFFCC;" id="LIQUID_PER_tolerance" class="easyui-validatebox" data-options="required:false,validType:'number'" />
                            </span></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td style="text-align: left;">补充说明
                        </td>
                        <td colspan="4">
                            <textarea id="MODEL_DESC" style="width: 460px;"></textarea>
                            (少于25个字) </td>
                    </tr>
                    <tr>
                        <td colspan="6">
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td style="text-align: left;">设计日期</td>
                        <td colspan="4">
                            <input class="easyui-datebox" id="DESIGN_DATE" /></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td style="text-align: left;">最后修改人</td>
                        <td>
                            <input type="text" id="UPDATE_USER_PACKAGE_DESIGN_INFO" />
                        </td>
                        <td></td>
                        <td style="text-align: left;">最后修改日期</td>
                        <td>
                            <input type="text" id="UPDATE_DATE_PACKAGE_DESIGN_INFO" /></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td style="text-align: left;">启用/停用</td>
                        <td colspan="4">
                            <select id="VALID_FLAG_PACKAGE_DESIGN_INFO">
                                <option value="1" selected="selected">启用</option>
                                <option value="0">停用</option>
                            </select>(少于1个字)
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">
                            <hr />
                            <table id="Table_PACKAGE_DESIGN_INFO_Search"></table>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div title="工序信息"></div>
        <div title="工序明细"></div>
    </div>
</asp:Content>
