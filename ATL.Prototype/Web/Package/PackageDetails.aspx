<%@ Page Title="基本信息" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PackageDetails.aspx.cs" Inherits="Web.Package.PackageDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Package/PackageDetails.js"></script>
    <script src="/Package/Tabs.js"></script>
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
        <div title="基本信息" style="padding: 2px;" data-options="selected:true">
            <a id="update" class="easyui-linkbutton">修改</a>
            <table class="TableDetail" cellspacing="0" cellpadding="8" style="width: 836px;">
                <tr>
                    <td style="background-color: #F7F7F7; width: 130px;">厂别
                    </td>
                    <td style="width: 130px;">
                        <span data-bind="text: FACTORY_ID"></span>
                    </td>
                    <td style="background-color: #F7F7F7; width: 130px;">文件类型
                    </td>
                    <td style="width: 130px;">
                        <span data-bind="text: PACKAGE_TYPE_ID"></span>
                    </td>
                    <td style="background-color: #F7F7F7; width: 130px;">生效日期
                    </td>
                    <td style="width: 130px;">
                        <span data-bind="text: EFFECT_DATE"></span>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F7F7F7">文件编号
                    </td>
                    <td>
                        <span data-bind="text: PACKAGE_NO"></span>
                    </td>
                    <td style="background-color: #F7F7F7">版本
                    </td>
                    <td>
                        <span data-bind="text: VERSION_NO"></span>
                    </td>
                    <td style="background-color: #F7F7F7">订单类型
                    </td>
                    <td>
                        <span data-bind="text: ORDER_TYPE"></span><span data-bind="    text: SO_NO"></span>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F7F7F7">分组数量
                    </td>
                    <td>
                        <span data-bind="text: GROUPS"></span>
                    </td>
                    <td style="background-color: #F7F7F7">分组编号
                    </td>
                    <td>
                        <span data-bind="text: GROUP_NO_LIST"></span>
                    </td>
                    <td style="background-color: #F7F7F7">分组数量
                    </td>
                    <td>
                        <span data-bind="text: GROUP_QTY_LIST"></span>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F7F7F7">目的
                    </td>
                    <td colspan="5">
                        <span data-bind="text: PURPOSE"></span>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F7F7F7">分组说明
                    </td>
                    <td colspan="5">
                        <span data-bind="text: GROUPS_PURPOSE"></span>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F7F7F7">产品类型
                    </td>
                    <td>
                        <span data-bind="text: PRODUCT_TYPE_ID"></span>
                    </td>
                    <td style="background-color: #F7F7F7">工艺类型
                    </td>
                    <td>
                        <span data-bind="text: PRODUCT_PROC_TYPE_ID"></span>
                    </td>
                    <td style="background-color: #F7F7F7">电池类型
                    </td>
                    <td>
                        <span data-bind="text: BATTERY_TYPE"></span>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F7F7F7">品种
                    </td>
                    <td>
                        <span data-bind="text: BATTERY_MODEL"></span>
                    </td>

                    <td style="background-color: #F7F7F7">层数
                    </td>
                    <td>
                        <span data-bind="text: BATTERY_LAYERS"></span>
                    </td>
                    <td style="background-color: #F7F7F7">数量
                    </td>
                    <td>
                        <span data-bind="text: BATTERY_QTY"></span>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F7F7F7">项目代码
                    </td>
                    <td>
                        <span data-bind="text: PROJECT_CODE"></span>
                    </td>
                    <td style="background-color: #F7F7F7">电池编号
                    </td>
                    <td>
                        <span data-bind="text: BATTERY_PARTNO"></span>
                    </td>

                    <td style="background-color: #F7F7F7">客户代码
                    </td>
                    <td>
                        <span data-bind="text: CUSTOMER_CODE"></span>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F7F7F7">出货日期
                    </td>
                    <td>
                        <span data-bind="text: OUTPUT_TARGET_DATE"></span>
                    </td>
                    <td style="background-color: #F7F7F7">是否紧急
                    </td>
                    <td colspan="3">
                        <span data-bind="text: IS_URGENT"></span>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F7F7F7">紧急原因
                    </td>
                    <td colspan="5">
                        <span data-bind="text: REASON_FORURGENT"></span>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F7F7F7">制作人
                    </td>
                    <td>
                        <span data-bind="text: PREPARED_BY"></span>
                    </td>
                    <td style="background-color: #F7F7F7">制作日期
                    </td>
                    <td>
                        <span data-bind="text: PREPARED_DATE"></span>
                    </td>
                    <td style="background-color: #F7F7F7">流程
                    </td>
                    <td>
                        <span data-bind="text: APPROVE_FLOW_ID"></span>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F7F7F7">产品变更
                    </td>
                    <td colspan="5">
                        <span data-bind="text: PRODUCT_CHANGE_HL"></span>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F7F7F7">工艺变更
                    </td>
                    <td colspan="5">
                        <span data-bind="text: PROCESS_CHANGE_HL"></span>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F7F7F7">物料变更
                    </td>
                    <td colspan="5">
                        <span data-bind="text: MATERIAL_CHANGE_HL"></span>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F7F7F7">其它变更
                    </td>
                    <td colspan="5">
                        <span data-bind="text: OTHER_CHANGE_HL"></span>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F7F7F7">最后更新人
                    </td>
                    <td>
                        <span data-bind="text: UPDATE_USER"></span>
                    </td>
                    <td style="background-color: #F7F7F7">最后更新日期
                    </td>
                    <td colspan="3">
                        <span data-bind="text: UPDATE_DATE"></span>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #F7F7F7">开启/关闭
                    </td>
                    <td>
                        <span data-bind="text: VALID_FLAG"></span>
                    </td>
                    <td style="background-color: #F7F7F7">是否删除
                    </td>
                    <td colspan="3">
                        <span data-bind="text: DELETE_FLAG"></span>
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
        <div title="分组信息"></div>
        <div title="设计信息"></div>
        <div title="工序信息"></div>
        <div title="工序明细"></div>
    </div>




</asp:Content>
