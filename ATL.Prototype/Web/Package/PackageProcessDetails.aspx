<%@ Page Title="工序明细" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PackageProcessDetails.aspx.cs" Inherits="Web.Package.PackageProcessDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Package/PackageProcessDetails.js"></script>
    <%--<script src="/Package/Tabs.js"></script>--%>
    <style>
        .TableDetail {
            border-style: solid none none solid;
            border-color: #efefef;
            border-width: 1px;
            background-color: #fff;
        }

            .TableDetail td {
                border-style: none solid solid none;
                border-color: #efefef;
                border-width: 1px;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="width: 944px; margin-bottom: 2px; padding: 5px 10px; border: 1px solid #95B8E7; background-color: #fff;">
        <a class="easyui-linkbutton" data-options="iconCls:'icon-backhome'" href="/Package/PACKAGE_BASE_INFO.aspx?mid=24">返回列表</a>&nbsp;&nbsp;&nbsp;&nbsp;
        <span style="color: blue;">文件编号:</span><span data-bind="text: PACKAGE_NO"></span>&nbsp;&nbsp;<span style="color: blue;">版本:</span><span data-bind="    text: VERSION_NO"></span>
        &nbsp;&nbsp;<span style="color: blue;">厂别:</span><span data-bind="text: FACTORY_ID"></span>&nbsp;&nbsp;<span style="color: blue;">产品类型:</span><span data-bind="    text: PRODUCT_TYPE_ID"></span>&nbsp;&nbsp;<span style="color: blue;">工艺类型:</span><span data-bind="    text: PRODUCT_PROC_TYPE_ID"></span>
    </div>
    <div id="mytab" style="border-style: none none solid none;"><a class="easyui-linkbutton" id="wf" data-options="iconCls:'icon-ok'">审批流程</a></div>
    <div id="tt" class="easyui-tabs" style="width: 966px;" data-options="border:false,tools:'#mytab'">
        <div title="基本信息"  style="padding: 2px 0px;" data-options="selected:true" >
            <a id="update" class="easyui-linkbutton">修改</a>
            <table class="TableDetail" cellspacing="0" cellpadding="8" style="width: 836px;">
                <tr>
                    <td style="background-color: #F7F7F7; width: 130px;">厂别
                    </td>
                    <td style="width: 130px;">                        
                        <span id="txt_FACTORY_ID" ></span>
                    </td>
                    <td style="background-color: #F7F7F7; width: 130px;">文件类型
                    </td>
                    <td style="width: 130px;">                        
                        <span id="txt_PACKAGE_TYPE_ID" ></span>
                    </td>
                    <td style="background-color: #F7F7F7; width: 130px;">生效日期
                    </td>
                    <td style="width: 130px;">                       
                        <span id="txt_EFFECT_DATE" ></span>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F7F7F7">文件编号
                    </td>
                    <td>                        
                        <span id="txt_PACKAGE_NO" ></span>
                    </td>
                    <td style="background-color: #F7F7F7">版本
                    </td>
                    <td>                       
                        <span id="txt_VERSION_NO"></span>
                    </td>
                    <td style="background-color: #F7F7F7">订单类型
                    </td>
                    <td>                       
                        <span id="txt_ORDER_TYPE" ></span><span id="txt_SO_NO" ></span>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F7F7F7">分组数量
                    </td>
                    <td>                       
                        <span id="txt_GROUPS" ></span>
                    </td>
                    <td style="background-color: #F7F7F7">分组编号
                    </td>
                    <td>                        
                        <span id="txt_GROUP_NO_LIST" ></span>
                    </td>
                    <td style="background-color: #F7F7F7">分组数量
                    </td>
                    <td>                        
                        <span id="txt_GROUP_QTY_LIST" ></span>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F7F7F7">目的
                    </td>
                    <td colspan="5">                        
                        <span id="txt_PURPOSE" ></span>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F7F7F7">分组说明
                    </td>
                    <td colspan="5">                        
                        <span id="txt_GROUPS_PURPOSE" ></span>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F7F7F7">产品类型
                    </td>
                    <td>                        
                        <span id="txt_PRODUCT_TYPE_ID"></span>
                    </td>
                    <td style="background-color: #F7F7F7">工艺类型
                    </td>
                    <td>                        
                        <span id="txt_PRODUCT_PROC_TYPE_ID" ></span>
                    </td>
                    <td style="background-color: #F7F7F7">电池类型
                    </td>
                    <td>                        
                        <span id="txt_BATTERY_TYPE" ></span>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F7F7F7">品种
                    </td>
                    <td>                        
                        <span id="txt_BATTERY_MODEL" ></span>
                    </td>

                    <td style="background-color: #F7F7F7">层数
                    </td>
                    <td>
                        
                        <span id="txt_BATTERY_LAYERS" ></span>
                    </td>
                    <td style="background-color: #F7F7F7">数量
                    </td>
                    <td>
                       
                        <span id="txt_BATTERY_QTY" ></span>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F7F7F7">项目代码
                    </td>
                    <td>
                        
                        <span id="txt_PROJECT_CODE" ></span>
                    </td>
                    <td style="background-color: #F7F7F7">电池编号
                    </td>
                    <td>
                        
                        <span id="txt_BATTERY_PARTNO" ></span>
                    </td>

                    <td style="background-color: #F7F7F7">客户代码
                    </td>
                    <td>
                        
                         <span id="txt_CUSTOMER_CODE" ></span>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F7F7F7">出货日期
                    </td>
                    <td>
                       
                        <span id="txt_OUTPUT_TARGET_DATE"></span>
                    </td>
                    <td style="background-color: #F7F7F7">是否紧急
                    </td>
                    <td colspan="3">
                        
                        <span id="txt_IS_URGENT" ></span>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F7F7F7">紧急原因
                    </td>
                    <td colspan="5">
                       
                        <span id="txt_REASON_FORURGENT" ></span>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F7F7F7">制作人
                    </td>
                    <td>
                        
                        <span id="txt_PREPARED_BY" ></span>
                    </td>
                    <td style="background-color: #F7F7F7">制作日期
                    </td>
                    <td>
                       
                         <span id="txt_PREPARED_DATE" ></span>
                    </td>
                    <td style="background-color: #F7F7F7">流程
                    </td>
                    <td>
                        
                        <span id="txt_APPROVE_FLOW_ID" ></span>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F7F7F7">产品变更
                    </td>
                    <td colspan="5">
                       
                        <span id="txt_PRODUCT_CHANGE_HL" ></span>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F7F7F7">工艺变更
                    </td>
                    <td colspan="5">
                        
                        <span id="txt_PROCESS_CHANGE_HL" ></span>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F7F7F7">物料变更
                    </td>
                    <td colspan="5">
                        
                        <span id="txt_MATERIAL_CHANGE_HL" ></span>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F7F7F7">其它变更
                    </td>
                    <td colspan="5">
                      
                        <span id="txt_OTHER_CHANGE_HL" ></span>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F7F7F7">最后更新人
                    </td>
                    <td>
                        
                        <span id="txt_UPDATE_USER" ></span>
                    </td>
                    <td style="background-color: #F7F7F7">最后更新日期
                    </td>
                    <td colspan="3">
                        
                        <span id="txt_UPDATE_DATE"></span>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F7F7F7">开启/关闭
                    </td>
                    <td>
                        
                        <span id="txt_VALID_FLAG"></span>
                    </td>
                    <td style="background-color: #F7F7F7">是否删除
                    </td>
                    <td colspan="3">
                        
                        <span id="txt_DELETE_FLAG" ></span>
                    </td>
                </tr>
            </table>
            <div id="Dialog_PACKAGE_BASE_INFO" class="easyui-dialog" data-options="title:'对话框',modal:false,height:'auto',closed:true,width:700,height:430">
                <div id="Tab_Dialog_PACKAGE_BASE_INFO" class="easyui-tabs" data-options="border:false">
                    <div title="基本信息" style="padding: 5px;">
                        <table cellspacing="1" cellspadding="0">
                            <tr>
                                <td style="text-align: right;">文件类型</td>
                                <td>
                                    <input type="text" id="PACKAGE_TYPE_ID" /></td>
                                <td style="text-align: right;">厂别</td>
                                <td>
                                    <input type="text" id="FACTORY_ID" style="border: none;" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">文件编号<b style="color: red; font-size: 15px;">*</b></td>
                                <td>
                                    <input type="text" id="PACKAGE_NO" /></td>
                                <td style="text-align: right;">版本号<b style="color: red; font-size: 15px;">*</b></td>
                                <td>
                                    <input type="text" id="VERSION_NO" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">产品类型<b style="color: red; font-size: 15px;">*</b></td>
                                <td>
                                    <input type="text" id="PRODUCT_TYPE_ID" /></td>
                                <td style="text-align: right;">产品工艺<b style="color: red; font-size: 15px;">*</b></td>
                                <td>
                                    <input type="text" id="PRODUCT_PROC_TYPE_ID" /></td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">品种<b style="color: red; font-size: 15px;">*</b></td>
                                <td>
                                    <input type="text" id="BATTERY_MODEL" class="easyui-combobox" data-options="required:true" /><b id="alert_BATTERY_MODEL" style="color: red; display: none;">不存在!</b>
                                </td>
                                <td style="text-align: right;">电池类型</td>
                                <td>
                                    <select id="BATTERY_TYPE">
                                        <option value="Li-Co" selected="selected">Li-Co</option>
                                    </select>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">层数<b style="color: red; font-size: 15px;">*</b></td>
                                <td>
                                    <input type="text" id="BATTERY_LAYERS" class="easyui-validatebox" data-options="required:true,validType:'number'" />(数字)</td>
                                <td style="text-align: right;">数量<b style="color: red; font-size: 15px;">*</b></td>
                                <td>
                                    <input type="text" id="BATTERY_QTY" /></td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">电池料号</td>
                                <td>
                                    <input type="text" id="BATTERY_PARTNO" class="easyui-combobox" /><b id="alert_BATTERY_PARTNO" style="color: red; display: none;">不存在!</b> </td>

                                <td style="text-align: right;">项目代码<b style="color: red; font-size: 15px;">*</b></td>
                                <td>
                                    <input type="text" id="PROJECT_CODE" class="easyui-combobox" data-options="required:true" /><b id="alert_PROJECT_CODE" style="color: red; display: none;">不存在!</b>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">客户代码</td>
                                <td>
                                    <input type="text" id="CUSTOMER_CODE" class="easyui-combobox" /><b id="alert_CUSTOMER_CODE" style="color: red; display: none;">不存在!</b>
                                </td>
                                <td style="text-align: right;">订单类型</td>
                                <td>
                                    <select id="ORDER_TYPE" style="width: 60px;">
                                        <option value="SO#">SO#</option>
                                        <option value="FREE">FREE</option>
                                        <option value="PMO">PMO</option>
                                    </select>
                                    <input type="text" id="SO_NO" class="easyui-validatebox" data-options="required:false,validType:['maxLength[15]']" style="width: 100px;" />(少于15个字)</td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">生产用途</td>
                                <td colspan="3">
                                    <textarea id="PURPOSE" style="width: 500px;" class="easyui-validatebox" data-options="required:false,validType:['maxLength[50]']"></textarea>(少于50个字) </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">紧急需求</td>
                                <td colspan="3">
                                    <select id="IS_URGENT">
                                        <option value="0" selected="selected">否</option>
                                        <option value="1">是</option>
                                    </select>
                                    加急原因&nbsp;<input type="text" id="REASON_FORURGENT" style="width: 400px;" class="easyui-validatebox" data-options="required:false,validType:['maxLength[30]']" />(少于30个字) </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">要求出货日期</td>
                                <td>
                                    <input id="OUTPUT_TARGET_DATE" class="easyui-datebox" data-options="editable:false" /></td>
                                <td style="text-align: right;">文件生效日期</td>
                                <td>
                                    <input type="text" id="EFFECT_DATE" readonly="readonly" style="border: none;" /></td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">制作人</td>
                                <td>
                                    <input type="text" id="PREPARED_BY" /></td>
                                <td style="text-align: right;">制作日期</td>
                                <td>
                                    <input type="text" id="PREPARED_DATE" /></td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">审批流程ID</td>
                                <td>
                                    <input type="text" id="APPROVE_FLOW_ID" class="easyui-combobox" /></td>
                                <td></td>
                                <td></td>
                            </tr>
                        </table>
                    </div>
                    <div title="变更信息" style="padding: 5px;">
                        <table cellspacing="1" cellspadding="0">
                            <tr>
                                <td>产品变更(少于150个字,7行以内)<br />
                                    <textarea id="PRODUCT_CHANGE_HL" rows="7" style="width: 600px;" class="easyui-validatebox" data-options="required:false,validType:['maxLength[150]']"></textarea>
                                </td>
                            </tr>
                            <tr>
                                <td>工艺变更(少于150个字,7行以内)<br />
                                    <textarea id="PROCESS_CHANGE_HL" rows="7" style="width: 600px;" class="easyui-validatebox" data-options="required:false,validType:['maxLength[150]']"></textarea>
                                </td>
                            </tr>
                            <tr>
                                <td>物料变更(少于150个字,7行以内)<br />
                                    <textarea id="MATERIAL_CHANGE_HL" rows="7" style="width: 600px;" class="easyui-validatebox" data-options="required:false,validType:['maxLength[150]']"></textarea>
                                </td>
                            </tr>
                            <tr>
                                <td>其它变更(少于150个字,7行以内)<br />
                                    <textarea id="OTHER_CHANGE_HL" rows="7" style="width: 600px;" class="easyui-validatebox" data-options="required:false,validType:['maxLength[150]']"></textarea>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div title="分组信息" style="padding: 5px;">
                        <textarea id="GROUPS_PURPOSE" style="width: 400px;" class="easyui-validatebox" data-options="required:false,validType:['maxLength[100]']"></textarea>(少于100个字)  
                    </div>
                    <div title="其它信息" style="padding: 5px;">
                        <table cellspacing="1" cellspadding="0">
                            <tr>
                                <td style="text-align: right;">版本开启/关闭</td>
                                <td>
                                    <select id="VALID_FLAG_PACKAGE_BASE_INFO">
                                        <option value="1" selected="selected">开启</option>
                                        <option value="0">关闭</option>
                                    </select>
                                </td>
                                <td style="text-align: right;">删除</td>
                                <td>
                                    <select id="DELETE_FLAG">
                                        <option value="1" selected="selected">已删除</option>
                                        <option value="0">未删除</option>
                                    </select>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">文件状态
                                </td>
                                <td>
                                    <select id="STATUS">
                                        <option value="1" selected="selected">草稿</option>
                                        <option value="2">已送审</option>
                                        <option value="3">已退回</option>
                                        <option value="4">已签审</option>
                                        <option value="5">已发布</option>
                                    </select>
                                </td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">最后更新人</td>
                                <td>
                                    <input type="text" id="UPDATE_USER_PACKAGE_BASE_INFO" /></td>
                                <td style="text-align: right;">最后更新日期</td>
                                <td>
                                    <input type="text" id="UPDATE_DATE_PACKAGE_BASE_INFO" /></td>
                            </tr>
                        </table>
                    </div>
                </div>

            </div>
        </div>
        <div title="分组信息"  style="padding: 2px 0px;">
            <table id="Table_PACKAGE_GROUPS"></table>
            <div id="Dialog_PACKAGE_GROUPS" class="easyui-dialog" data-options="title:'对话框',modal:false,width:350,height:'auto',closed:true" style="padding: 10px;">
                <table cellspacing="0" cellspadding="0">                    
                    <tr>
                        <td style="text-align: right;">组别<b style="color: red; font-size: 15px;">*</b></td>
                        <td>
                            <input type="text" id="GROUP_NO"  class="easyui-validatebox" data-options="required:true,validType:['maxLength[2]','alpha']" />(少于2个字) </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">数量</td>
                        <td>
                            <input type="text" id="GROUP_QTY"  class="easyui-validatebox" data-options="required:true,validType:'number'" /></td>
                    </tr>
                </table>
            </div>
        </div>
        <div title="设计信息"  style="padding: 2px 0px;">
            <table id="Table_PACKAGE_DESIGN_INFO"></table>
            <div id="Dialog_PACKAGE_DESIGN_INFO" class="easyui-dialog" data-options="title:'对话框',modal:false,width:800,height:'450',closed:true" style="padding: 10px;">
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
        <div title="工序明细" style="padding: 2px 0px;">
            <table cellspacing="0" cellpadding="0">
                <tr>
                    <td style="vertical-align: top;">
                        <%--<table id="Table_PROCESS_LIST_PACKAGE"></table>--%>
                        <table id="Table_PROCESS_LIST"></table>
                    </td>
                    <td style="vertical-align: top; padding-left: 2px; padding-right: 2px;">
                        <%--<table id="Table_GROUP_NO_PACKAGE_PROCESS"></table>--%>
                        <table id="Table_PACKAGE_FLOW_INFO"></table>
                        <div id="Dialog_PACKAGE_FLOW_INFO" class="easyui-dialog" data-options="title:'对话框',modal:false,width:500,height:'auto',closed:true" style="padding: 10px;">
                            <div id="grp" style="width: 450px; margin-bottom: 2px; padding: 5px; border: 1px solid #CCCCCC; background-color: #FFFFCC;">
                                分组(多选)<input id="GROUP_NO_BATCHEDIT" />
                            </div>
                            <table cellspacing="0" cellspadding="0">
                                <tr>
                                    <td style="text-align: right;">工序<b style="color: red; font-size: 15px;">*</b></td>
                                    <td>
                                        <input type="text" id="PROCESS_ID" /></td>
                                    <td style="text-align: right;">序号</td>
                                    <td>
                                        <input type="text" id="PROC_SEQUENCE_NO" class="easyui-validatebox" data-options="required:false,validType:'number'" /></td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">前一工序</td>
                                    <td>
                                        <input type="text" id="PREVIOUS_PROCESS_ID" /></td>
                                    <td style="text-align: right;">后一工序</td>
                                    <td>
                                        <input type="text" id="NEXT_PROCESS_ID" /></td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">工序说明</td>
                                    <td colspan="3">
                                        <textarea id="PKG_PROC_DESC" style="width: 380px;" class="easyui-validatebox" data-options="required:false,validType:['maxLength[100]']"></textarea></td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">分组归类</td>
                                    <td colspan="3">
                                        <input id="SUB_GROUP_NO" /></td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">最后修改者</td>
                                    <td>
                                        <input type="text" id="UPDATE_USER_PACKAGE_FLOW_INFO" /></td>
                                    <td style="text-align: right;">最后修改时间</td>
                                    <td>
                                        <input type="text" id="UPDATE_DATE_PACKAGE_FLOW_INFO" /></td>
                                </tr>
                            </table>
                        </div>

                        <div id="Dialog_PACKAGE_FLOW_INFO_BatchAdd" class="easyui-dialog" data-options="title:'批量新增',modal:false,width:300,height:'auto',closed:true" style="padding: 10px;">
                            <table cellspacing="0" cellspadding="0">
                                <tr>
                                    <td>分组(多选)
                                    </td>
                                    <td>
                                        <input id="GROUP_NO_BATCHADD" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>工序(多选)</td>
                                    <td>
                                        <input id="PROCESS_ID_BATCHADD" />
                                    </td>
                                </tr>
                            </table>
                        </div>

                        <div id="Dialog_PACKAGE_FLOW_INFO_BatchDel" class="easyui-dialog" data-options="title:'批量删除',modal:false,width:300,height:'400',closed:true" style="padding: 2px;">
                            <div style="padding: 4px;">
                                分组（多选）<input id="GROUP_NO_BatchDel" />
                            </div>

                            <table id="Table_PACKAGE_FLOW_INFO_BatchDel"></table>
                        </div>
                    </td>
                    <td style="vertical-align: top;">
                        <table cellspacing="0" cellpadding="0">
                            <tr>
                                <td style="vertical-align: top;">
                                    <div id="tt1" class="easyui-tabs" data-options="border:false" style="width: 712px;">
                                        <div title="产品及工艺参数信息" data-options="selected:true" style="padding: 1px 0px;">
                                        </div>
                                        <div title="物料信息" style="padding: 2px 0;">
                                            <div class="easyui-panel" title="查询" style="width: 712px; margin-bottom: 2px; padding: 5px;">
                                                物料分类&nbsp;<input type="text" id="MATERIAL_CATEGORY_ID" class="easyui-combobox" style="width: 250px;" />
                                            </div>
                                            <table cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td style="padding-right: 2px;">
                                                        <!-------------物料类型-------------->
                                                        <table id="Table_PACKAGE_PROC_MATERIAL_INFO"></table>
                                                        <div id="Dialog_PACKAGE_PROC_MATERIAL_INFO" class="easyui-dialog" data-options="title:'对话框',modal:false,width:350,height:210,closed:true" style="padding: 10px;">
                                                            分组(多选)<input id="GROUP_NO_MATERIAL_INFO_BATCH" />
                                                            <hr />
                                                            <table cellspacing="2" cellpadding="0">
                                                                <tr>
                                                                    <td style="text-align: right;">物料类型<b style="color: red; font-size: 15px;">*</b></td>
                                                                    <td>
                                                                        <input type="text" id="MATERIAL_TYPE_ID" /><b id="alert_MATERIAL_TYPE_ID" style="color: red; display: none;">不存在!</b>
                                                                        <a id="btn_MATERIAL_TYPE_ID" class="easyui-linkbutton">选择</a>
                                                                        <div id="Dialog_MATERIAL_TYPE_ID" class="easyui-dialog" data-options="title:'对话框',modal:false,width:340,height:400,closed:true" style="padding: 10px;">
                                                                            物料分类&nbsp;<input id="MATERIAL_CATEGORY_ID_PACKAGE_PROC_MATERIAL_INFO" style="width: 250px;" /><br />
                                                                            类型编号&nbsp;<input type="text" id="Search_MATERIAL_TYPE_ID" />
                                                                            <br />
                                                                            英文名称&nbsp;<input type="text" id="Search_MATERIAL_TYPE_NAME" /><br />
                                                                            中文名称&nbsp;<input type="text" id="Search_MATERIAL_TYPE_DESC" />
                                                                            <a class="easyui-linkbutton" id="btn_Search_MATERIAL_TYPE_ID">查询</a>
                                                                            <div style="height: 4px; overflow: hidden;"></div>
                                                                            <table id="Table_MATERIAL_TYPE_ID" class="easyui-datagrid"></table>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="text-align: right;">最后更新者</td>
                                                                    <td>
                                                                        <input type="text" id="UPDATE_USER_PACKAGE_PROC_MATERIAL_INFO" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="text-align: right;">最后更新日期</td>
                                                                    <td>
                                                                        <input type="text" id="UPDATE_DATE_PACKAGE_PROC_MATERIAL_INFO" /></td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <!-------------物料PN-------------->
                                                        <table id="Table_PACKAGE_PROC_PN_INFO"></table>
                                                        <div id="Dialog_PACKAGE_PROC_PN_INFO" class="easyui-dialog" data-options="title:'对话框',modal:false,width:350,height:210,closed:true" style="padding: 10px;">
                                                            分组(多选)<input id="GROUP_NO_PN_INFO_BATCH" />
                                                            <hr />
                                                            <table cellspacing="2" cellpadding="0">
                                                                <tr>
                                                                    <td style="text-align: right;"></td>
                                                                    <td></td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="text-align: right;">物料编号<b style="color: red; font-size: 15px;">*</b></td>
                                                                    <td>
                                                                        <input type="text" id="MATERIAL_PN_ID" />
                                                                        <a class="easyui-linkbutton" id="btn_MATERIAL_PN_ID">选择</a>
                                                                        <div id="Dialog_MATERIAL_PN_ID" class="easyui-dialog" data-options="title:'对话框',modal:false,width:360,height:460,closed:true" style="padding: 10px;">
                                                                            <br />
                                                                            物料分类&nbsp;<input id="MATERIAL_CATEGORY_ID_PACKAGE_PROC_PN_INFO" style="width: 250px;" /><br />
                                                                            物料类型&nbsp;<input id="MATERIAL_TYPE_ID_PACKAGE_PROC_PN_INFO" /><br />
                                                                            物料编号&nbsp;<input id="Search_MATERIAL_PN_ID" /><br />
                                                                            英文名称&nbsp;<input id="Search_MATERIAL_PN_NAME" /><br />
                                                                            中文名称&nbsp;<input id="Search_MATERIAL_PN_DESC" />
                                                                            <a class="easyui-linkbutton" id="btn_Search_MATERIAL_PN_ID">查询</a>
                                                                            <div style="height: 4px; overflow: hidden;"></div>
                                                                            <table id="Table_MATERIAL_PN_ID"></table>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="text-align: right;">最后更新者</td>
                                                                    <td>
                                                                        <input type="text" id="UPDATE_USER_PACKAGE_PROC_PN_INFO" />(少于10个字) </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="text-align: right;">最后更新日期</td>
                                                                    <td>
                                                                        <input type="text" id="UPDATE_DATE_PACKAGE_PROC_PN_INFO" /></td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div title="设备信息" style="padding: 2px 0;">
                                            <!-------------设备类型-------------->
                                            <div class="easyui-panel" title="查询" style="width: 712px; margin-bottom: 2px; padding: 5px;">
                                                设备类型&nbsp;<input type="text" id="EQUIPMENT_TYPE_ID" class="easyui-combobox" />
                                            </div>
                                            <table cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td style="padding-right: 2px;">
                                                        <table id="Table_PACKAGE_PROC_EQUIP_CLASS_INFO"></table>
                                                        <div id="Dialog_PACKAGE_PROC_EQUIP_CLASS_INFO" class="easyui-dialog" data-options="title:'对话框',modal:false,width:500,height:300,closed:true" style="padding: 10px;">
                                                            分组(多选)<b style="color: red; font-size: 15px;">*</b><input id="GROUP_NO_EQUIP_CLASS_INFO_BATCH" />
                                                            <hr />
                                                            <table cellspacing="0" cellpadding="0">
                                                                <tr>
                                                                    <td style="text-align: right;">分类<b style="color: red; font-size: 15px;">*</b></td>
                                                                    <td>
                                                                        <input type="text" id="EQUIPMENT_CLASS_ID" />
                                                                        <a id="btn_EQUIPMENT_CLASS_ID" class="easyui-linkbutton">选择</a>
                                                                        <div id="Dialog_EQUIPMENT_CLASS_ID" class="easyui-dialog" data-options="title:'对话框',modal:false,width:340,height:400,closed:true" style="padding: 10px;">
                                                                            设备类型&nbsp;<input type="text" id="EQUIPMENT_TYPE_ID_EQUIP_CLASS_INFO" class="easyui-combobox" /><br />
                                                                            设备编号&nbsp;<input type="text" id="Search_EQUIPMENT_CLASS_ID" />
                                                                            <br />
                                                                            英文名称&nbsp;<input type="text" id="Search_EQUIPMENT_CLASS_NAME" /><br />
                                                                            中文名称&nbsp;<input type="text" id="Search_EQUIPMENT_CLASS_DESC" />
                                                                            <a class="easyui-linkbutton" id="btn_Search_EQUIPMENT_CLASS_ID">查询</a>
                                                                            <div style="height: 4px; overflow: hidden;"></div>
                                                                            <table id="Table_EQUIPMENT_CLASS_ID"></table>
                                                                        </div>
                                                                    </td>

                                                                </tr>
                                                                <tr>
                                                                    <td style="text-align: right;">备注</td>
                                                                    <td>
                                                                        <textarea id="REMARK" style="width: 300px;" class="easyui-validatebox" data-options="required:false,validType:['maxLength[25]']"></textarea>(少于25个字) </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="text-align: right;">最后更新者</td>
                                                                    <td>
                                                                        <input type="text" id="UPDATE_USER_PACKAGE_PROC_EQUIP_CLASS_INFO" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="text-align: right;">最后更新日期</td>
                                                                    <td>
                                                                        <input type="text" id="UPDATE_DATE_PACKAGE_PROC_EQUIP_CLASS_INFO" /></td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <!-------------设备PN-------------->
                                                        <table id="Table_PACKAGE_PROC_EQUIP_INFO"></table>
                                                        <div id="Dialog_PACKAGE_PROC_EQUIP_INFO" class="easyui-dialog" data-options="title:'对话框',modal:false,width:500,height:400,closed:true" style="padding: 10px;">
                                                            分组(多选)<input id="GROUP_NO_EQUIP_INFO_BATCH" />
                                                            <hr />
                                                            <table cellspacing="0" cellpadding="0">
                                                                <tr>
                                                                    <td style="text-align: right;">编号<b style="color: red; font-size: 15px;">*</b></td>
                                                                    <td>
                                                                        <input type="text" id="EQUIPMENT_ID" />
                                                                        <a id="btn_EQUIPMENT_ID" class="easyui-linkbutton">选择</a>
                                                                        <div id="Dialog_EQUIPMENT_ID" class="easyui-dialog" data-options="title:'对话框',modal:false,width:340,height:420,closed:true" style="padding: 10px;">
                                                                            设备类型&nbsp;<input type="text" id="EQUIPMENT_TYPE_ID_EQUIP_INFO" class="easyui-combobox" />
                                                                            <br />
                                                                            设备分类&nbsp;<input type="text" id="EQUIPMENT_CLASS_ID_EQUIP_INFO" class="easyui-combobox" />
                                                                            <br />
                                                                            设备编号&nbsp;<input type="text" id="Search_EQUIPMENT_ID" /><br />
                                                                            中文名称&nbsp;<input type="text" id="Search_EQUIPMENT_NAME" /><br />
                                                                            中文名称&nbsp;<input type="text" id="Search_EQUIPMENT_DESC" />
                                                                            <a class="easyui-linkbutton" id="btn_Search_EQUIPMENT_ID">查询</a>
                                                                            <div style="height: 4px; overflow: hidden;"></div>
                                                                            <table id="Table_EQUIPMENT_ID"></table>
                                                                        </div>

                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="text-align: right;">备注</td>
                                                                    <td>
                                                                        <textarea id="REMARK_PACKAGE_PROC_EQUIP_INFO" style="width: 300px;" class="easyui-validatebox" data-options="required:false,validType:['maxLength[25]']"></textarea>(少于25个字)</td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="text-align: right;">最后更新者</td>
                                                                    <td>
                                                                        <input type="text" id="UPDATE_USER_PACKAGE_PROC_EQUIP_INFO" />(少于10个字) </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="text-align: right;">最后更新日期</td>
                                                                    <td>
                                                                        <input type="text" id="UPDATE_DATE_PACKAGE_PROC_EQUIP_INFO" /></td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>



                                        </div>
                                        <div title="附图信息" style="padding: 2px 0;">
                                            <!-------------附图-------------->
                                            <table id="Table_PACKAGE_ILLUSTRATION_INFO"></table>
                                            <div id="Dialog_PACKAGE_ILLUSTRATION_INFO" class="easyui-dialog" data-options="title:'对话框',modal:false,width:600,height:'400',closed:true" style="padding: 10px;">
                                                分组(多选)<input id="GROUP_NO_ILLUSTRATION_INFO_BATCH" />
                                                <hr />
                                                <table cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td style="text-align: right;">图片类型</td>
                                                        <td>
                                                            <input type="text" id="ILLUSTRATION_ID_PACKAGE_ILLUSTRATION_INFO" style="width: 300px;" />

                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: right;">图片描述</td>
                                                        <td>
                                                            <textarea id="ILLUSTRATION_DESC" style="width: 300px;" class="easyui-validatebox" data-options="required:false,validType:['maxLength[25]']"></textarea>(少于25个字) </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: right;">附图</td>
                                                        <td>
                                                            <a class="easyui-linkbutton" id="ILLUSTRATION_DATA_Upload">上传图片</a>（不上传，则默认为系统图片）

                                                            <div id="Dialog_PACKAGE_ILLUSTRATION_INFO_UploadImg" class="easyui-dialog" data-options="title:'对话框',modal:false,width:300,height:'auto',closed:true" style="padding: 10px;">
                                                                <input type="text" id="url" value="" />
                                                                <a class="easyui-linkbutton" id="image">选择图片</a>（本地上传）
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: right;">启用/停用</td>
                                                        <td>
                                                            <select id="VALID_FLAG">
                                                                <option value="1" selected="selected">启用</option>
                                                                <option value="0">停用</option>
                                                            </select></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: right;">最后更新者</td>
                                                        <td>
                                                            <input type="text" id="UPDATE_USER_PACKAGE_ILLUSTRATION_INFO" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: right; width: 100px;">最后更新日期</td>
                                                        <td>
                                                            <input type="text" id="UPDATE_DATE_PACKAGE_ILLUSTRATION_INFO" /></td>
                                                    </tr>

                                                </table>
                                                <div id="dlg_showimg"></div>
                                            </div>
                                            <%-- <div id="msg"></div>        --%>
                                        </div>
                                        <div title="BOM信息" style="padding: 2px 0;">
                                            <table id="Table_PACKAGE_BOM_SPEC_INFO"></table>
                                            <div id="Dialog_PACKAGE_BOM_SPEC_INFO" class="easyui-dialog" data-options="title:'对话框',modal:false,width:500,height:400,closed:true" style="padding: 10px;">
                                                分组(多选)<input id="GROUP_NO_BOM_SPEC_INFO_BATCH" />
                                                <hr />
                                                <table cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td style="text-align: right;">父PN<b style="color: red; font-size: 15px;">*</b></td>
                                                        <td>
                                                            <input type="text" id="P_PART_ID" />
                                                            <a id="btn_P_PART_ID" class="easyui-linkbutton">选择</a>
                                                            <div id="Dialog_Search_P_PART_ID" class="easyui-dialog" data-options="title:'对话框',modal:false,width:340,height:320,closed:true" style="padding: 10px;">
                                                                <input id="Search_P_PART_ID" />
                                                                <a class="easyui-linkbutton" id="btn_Search_P_PART_ID">查询</a>
                                                                <div style="height: 4px; overflow: hidden;"></div>
                                                                <table id="Table_P_PART_ID"></table>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: right;">子PN<b style="color: red; font-size: 15px;">*</b></td>
                                                        <td>
                                                            <input type="text" id="C_PART_ID" />
                                                            <a id="btn_C_PART_ID" class="easyui-linkbutton">选择</a>
                                                            <div id="Dialog_Search_C_PART_ID" class="easyui-dialog" data-options="title:'对话框',modal:false,width:340,height:320,closed:true" style="padding: 10px;">
                                                                <input id="Search_C_PART_ID" />
                                                                <a class="easyui-linkbutton" id="btn_Search_C_PART_ID">查询</a>
                                                                <div style="height: 4px; overflow: hidden;"></div>
                                                                <table id="Table_C_PART_ID"></table>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: right;">父物料数量</td>
                                                        <td>
                                                            <input type="text" id="P_PART_QTY" class="easyui-validatebox" data-options="required:false,validType:'number'" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: right;">子物料数量</td>
                                                        <td>
                                                            <input type="text" id="C_PART_QTY" class="easyui-validatebox" data-options="required:false,validType:'number'" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: right;">虚拟件</td>
                                                        <td>
                                                            <select id="IS_VIRTUAL_PART">
                                                                <option value="0" selected="selected">否</option>
                                                                <option value="1">是</option>
                                                            </select>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: right;">IQC来料</td>
                                                        <td>
                                                            <select id="IS_IQC_MATERIAL">
                                                                <option value="0" selected="selected">否</option>
                                                                <option value="1">是</option>
                                                            </select>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: right;">替代件</td>
                                                        <td>
                                                            <select id="IS_SUBSTITUTE">
                                                                <option value="0" selected="selected">否</option>
                                                                <option value="1">是</option>
                                                            </select>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: right;">同步日期</td>
                                                        <td>
                                                            <input type="text" id="SYNC_DATE" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: right;">最后更新者</td>
                                                        <td>
                                                            <input type="text" id="UPDATE_USER_PACKAGE_BOM_SPEC_INFO" />(少于10个字) </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: right;">最后更新日期</td>
                                                        <td>
                                                            <input type="text" id="UPDATE_DATE_PACKAGE_BOM_SPEC_INFO" /></td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                        <div title="大分组" data-options="selected:false" style="padding: 2px 0;">
                                            <table id="Table_PACKAGE_PROC_GRP"></table>
                                            <div id="Dialog_PACKAGE_PROC_GRP" class="easyui-dialog" data-options="title:'对话框',modal:false,width:500,height:250,closed:true" style="padding: 10px;">
                                                <table cellspacing="0" cellpadding="0">
                                                    <tr id="grp">
                                                        <td>分组（多选）</td>
                                                        <td>
                                                            <input id="GROUP_NO_PACKAGE_PROC_GRP" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: right;">描述</td>
                                                        <td>
                                                            <input type="text" id="PROC_GRP_DESC" class="easyui-validatebox" data-options="required:false,validType:['maxLength[100]']" />(少于100个字) </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: right;">最后更新者</td>
                                                        <td>
                                                            <input type="text" id="UPDATE_USER_PACKAGE_PROC_GRP" class="easyui-validatebox" data-options="required:false,validType:['maxLength[10]']" />(少于10个字) </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: right;">最后更新日期</td>
                                                        <td>
                                                            <input type="text" id="UPDATE_DATE_PACKAGE_PROC_GRP" /></td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div style="overflow: hidden; height: 2px;"></div>
                                            <table id="Table_PACKAGE_PROC_GRP_LIST"></table>
                                            <div id="Dialog_PACKAGE_PROC_GRP_LIST" class="easyui-dialog" data-options="title:'对话框',modal:false,width:500,height:400,closed:true" style="padding: 10px;">
                                                <table cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td style="text-align: right;">组别<b style="color: red; font-size: 15px;">*</b></td>
                                                        <td>
                                                            <input id="GROUP_NO_PACKAGE_PROC_GRP_LIST" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: right;">最后更新者</td>
                                                        <td>
                                                            <input type="text" id="UPDATE_USER_PACKAGE_PROC_GRP_LIST" class="easyui-validatebox" data-options="required:false,validType:['maxLength[10]']" />(少于10个字) </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: right;">最后更新日期</td>
                                                        <td>
                                                            <input type="text" id="UPDATE_DATE_PACKAGE_PROC_GRP_LIST" /></td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="Param">
                                        <table id="Table_PACKAGE_PARAM_SETTING"></table>
                                        <div id="Dialog_PACKAGE_PARAM_SETTING" class="easyui-dialog" data-options="title:'对话框',modal:false,width:650,height:'auto',closed:true" style="padding: 10px;">
                                            分组(多选)<input id="GROUP_NO_BATCH" />
                                            <hr />
                                            <table cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td style="text-align: right;">参数<b style="color: red; font-size: 15px;">*</b></td>
                                                    <td colspan="3">
                                                        <input type="text" id="PARAMETER_ID_SETTING" />
                                                        输入/输出<select id="PARAM_IO_SETTING">
                                                            <option value="1" selected="selected">输入</option>
                                                            <option value="2">输出</option>
                                                            <option value="3">输入&输出</option>
                                                        </select>
                                                        是分组参数<select id="IS_GROUP_PARAM_SETTING">
                                                            <option value="0" selected="selected">否</option>
                                                            <option value="1">是</option>
                                                        </select>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right;">首件参数</td>
                                                    <td colspan="3">
                                                        <select id="IS_FIRST_CHECK_PARAM_SETTING">
                                                            <option value="0" selected="selected">否</option>
                                                            <option value="1">是</option>
                                                        </select>过程参数<select id="IS_PROC_MON_PARAM_SETTING">
                                                            <option value="0" selected="selected">否</option>
                                                            <option value="1">是</option>
                                                        </select>出货参数<select id="IS_OUTPUT_PARAM_SETTING">
                                                            <option value="0" selected="selected">否</option>
                                                            <option value="1">是</option>
                                                        </select>
                                                    </td>
                                                </tr>
                                                <tr style="display: none;">
                                                    <td style="text-align: right;">参数类型</td>
                                                    <td colspan="3">
                                                        <input type="text" id="PARAM_TYPE_ID_SETTING" />(少于15个字) </td>


                                                    <td style="text-align: right;">在规格牌中</td>
                                                    <td>
                                                        <select id="IS_SC_PARAM_SETTING">
                                                            <option value="0" selected="selected">否</option>
                                                            <option value="1">是</option>
                                                        </select>
                                                    </td>
                                                    <td style="text-align: right;">规格牌顺序</td>
                                                    <td>
                                                        <input type="text" id="DISP_ORDER_IN_SC_SETTING" class="easyui-validatebox" data-options="required:false,validType:'number'" /></td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right;">目标值</td>
                                                    <td>
                                                        <input type="text" id="TARGET_SETTING" class="easyui-validatebox" data-options="required:false,validType:['maxLength[25]']" />(少于25个字)
                                                    </td>
                                                    <td style="text-align: right;">参数单位</td>
                                                    <td>
                                                        <select id="PARAM_UNIT_SETTING">
                                                            <option value=""></option>
                                                            <option value="%">%</option>
                                                            <option value="ea">ea</option>
                                                            <option value="g/cm^3">g/cm^3</option>
                                                            <option value="H">H</option>
                                                            <option value="Hz">Hz</option>
                                                            <option value="J">J</option>
                                                            <option value="Kg">Kg</option>
                                                            <option value="Kpa">Kpa</option>
                                                            <option value="m">m</option>
                                                            <option value="m/min">m/min</option>
                                                            <option value="m/s">m/s</option>
                                                            <option value="mAh">mAh</option>
                                                            <option value="min">min</option>
                                                            <option value="mm">mm</option>
                                                            <option value="mm/s">mm/s</option>
                                                            <option value="mohm">mohm</option>
                                                            <option value="Mpa">Mpa</option>
                                                            <option value="mV">mV</option>
                                                            <option value="mV/h">mV/h</option>
                                                            <option value="MΩ">MΩ</option>
                                                            <option value="N">N</option>
                                                            <option value="pcs">pcs</option>
                                                            <option value="ppm">ppm</option>
                                                            <option value="RPM">RPM</option>
                                                            <option value="S">S</option>
                                                            <option value="T">T</option>
                                                            <option value="V">V</option>
                                                            <option value="V/℃">V/℃</option>
                                                            <option value="μm">μm</option>
                                                            <option value="℃">℃</option>
                                                            <option value="进制">进制</option>
                                                        </select>
                                                        数据类型<select id="PARAM_DATATYPE_SETTING">
                                                            <option value=""></option>
                                                            <option value="STRING">STRING</option>
                                                            <option value="NUMBER">NUMBER</option>
                                                        </select>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right;">上限值</td>
                                                    <td>
                                                        <input type="text" id="USL_SETTING" class="easyui-validatebox" data-options="required:false,validType:['maxLength[25]']" />(少于25个字) </td>
                                                    <td style="text-align: right;">下限值</td>
                                                    <td>
                                                        <input type="text" id="LSL_SETTING" class="easyui-validatebox" data-options="required:false,validType:['maxLength[25]']" />(少于8个字) </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right;">抽样频率</td>
                                                    <td>
                                                        <input type="text" id="SAMPLING_FREQUENCY_SETTING" class="easyui-validatebox" data-options="required:false,validType:['maxLength[50]']" />(少于50个字) </td>

                                                    <td style="text-align: right;">控制方法</td>
                                                    <td>
                                                        <input type="text" id="CONTROL_METHOD_SETTING" class="easyui-validatebox" data-options="required:false,validType:['maxLength[50]']" />(少于50个字) </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right;">最后更新者</td>
                                                    <td>
                                                        <input type="text" id="UPDATE_USER_SETTING" /></td>

                                                    <td style="text-align: right;">最后更新日期</td>
                                                    <td>
                                                        <input type="text" id="UPDATE_DATE_SETTING" /></td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div id="Dialog_PACKAGE_PARAM_SETTING_BatchAdd" class="easyui-dialog" data-options="title:'批量增加工序参数',modal:false,width:350,height:'auto',closed:true" style="padding: 10px;">
                                            <table cellspacing="0" cellpadding="1">
                                                <tr>
                                                    <td>组别</td>
                                                    <td>
                                                        <input id="GROUP_NO_Dialog_PACKAGE_PARAM_SETTING_BatchAdd" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>参数</td>
                                                    <td>
                                                        <input id="PARAMETER_ID_Dialog_PACKAGE_PARAM_SETTING_BatchAdd" style="width: 240px;" /></td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div style="height: 2px; overflow: hidden;"></div>
                                        <table id="Table_PACKAGE_PARAM_SPEC_INFO"></table>
                                        <div id="Dialog_PACKAGE_PARAM_SPEC_INFO" class="easyui-dialog" data-options="title:'对话框',modal:false,width:700,height:'auto',closed:true" style="padding: 10px;">
                                            <table cellspacing="0" cellpadding="0">
                                                <tr id="show_GROUP_NO_PACKAGE_PARAM_SPEC_INFO_BATCH">
                                                    <td style="text-align: right;">组别<b style="color: red; font-size: 15px;">*</b></td>
                                                    <td>
                                                        <input id="GROUP_NO_PACKAGE_PARAM_SPEC_INFO_BATCH" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right;">参数类型<b style="color: red; font-size: 15px;">*</b></td>
                                                    <td colspan="3">
                                                        <select id="SPEC_TYPE">
                                                            <option value=""></option>
                                                            <option value="FAI">首件</option>
                                                            <option value="PMI">过程</option>
                                                            <option value="OI">出货</option>
                                                        </select>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right;">单位</td>
                                                    <td>
                                                        <%--<input type="text" id="PARAM_UNIT" class="easyui-validatebox" data-options="required:false,validType:['maxLength[8]']" />--%>
                                                        <select id="PARAM_UNIT">
                                                            <option value=""></option>
                                                            <option value="%">%</option>
                                                            <option value="ea">ea</option>
                                                            <option value="g/cm^3">g/cm^3</option>
                                                            <option value="H">H</option>
                                                            <option value="Hz">Hz</option>
                                                            <option value="J">J</option>
                                                            <option value="Kg">Kg</option>
                                                            <option value="Kpa">Kpa</option>
                                                            <option value="m">m</option>
                                                            <option value="m/min">m/min</option>
                                                            <option value="m/s">m/s</option>
                                                            <option value="mAh">mAh</option>
                                                            <option value="min">min</option>
                                                            <option value="mm">mm</option>
                                                            <option value="mm/s">mm/s</option>
                                                            <option value="mohm">mohm</option>
                                                            <option value="Mpa">Mpa</option>
                                                            <option value="mV">mV</option>
                                                            <option value="mV/h">mV/h</option>
                                                            <option value="MΩ">MΩ</option>
                                                            <option value="N">N</option>
                                                            <option value="pcs">pcs</option>
                                                            <option value="ppm">ppm</option>
                                                            <option value="RPM">RPM</option>
                                                            <option value="S">S</option>
                                                            <option value="T">T</option>
                                                            <option value="V">V</option>
                                                            <option value="V/℃">V/℃</option>
                                                            <option value="μm">μm</option>
                                                            <option value="℃">℃</option>
                                                            <option value="进制">进制</option>
                                                        </select>


                                                    </td>
                                                    <td style="text-align: right;">目标值</td>
                                                    <td>
                                                        <input type="text" id="TARGET" class="easyui-validatebox" data-options="required:false,validType:['maxLength[25]']" />(少于25个字) </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right;">上限</td>
                                                    <td>
                                                        <input type="text" id="USL" class="easyui-validatebox" data-options="required:false,validType:['maxLength[25]']" />(少于25个字) </td>
                                                    <td style="text-align: right;">下限</td>
                                                    <td>
                                                        <input type="text" id="LSL" class="easyui-validatebox" data-options="required:false,validType:['maxLength[25]']" />(少于25个字) </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right;">最后更新者</td>
                                                    <td>
                                                        <input type="text" id="UPDATE_USER" /></td>
                                                    <td style="text-align: right;">最后更新日期</td>
                                                    <td>
                                                        <input type="text" id="UPDATE_DATE" /></td>
                                                </tr>
                                            </table>
                                        </div>
                                        <!-------------图片-------------->
                                        <div style="height: 2px; overflow: hidden;"></div>
                                        <div id="panel_showimg" class="easyui-panel" style="width: 712px; height: 248px;" title="显示图片" data-options="close:true">

                                            <div id="showimg"></div>

                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>

</asp:Content>
