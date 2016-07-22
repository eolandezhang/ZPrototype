<%@ Page Title="文件列表" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PACKAGE_BASE_INFO.aspx.cs" Inherits="Web.Package.PACKAGE_BASE_INFO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Package/PACKAGE_BASE_INFO.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="width: 944px; margin-bottom: 2px; padding: 10px; border: 1px solid #95B8E7; background-color: #fff;">
        文件编号：<input type="text" id="Search_PACKAGE_NO" />&nbsp;
版本号：<input type="text" id="Search_VERSION_NO" />&nbsp;
        状态：<select id="Search_VALID_FLAG">
            <option value="%" selected="selected">全部</option>
            <option value="1">开启</option>
            <option value="0">关闭</option>
        </select>
        <select id="Search_DELETE_FLAG">
            <option value="%">全部</option>
            <option value="1">已删除</option>
            <option value="0" selected="selected">未删除</option>
        </select>
        <a id="btnSearch" style="cursor: pointer" class="easyui-linkbutton">查询</a>
    </div>
    <table id="Table_PACKAGE_BASE_INFO"></table>
    <p>导出：制作人可以导出【自己制作的】【“草稿/已退回”状态】的文件。</p>
    <div id="Dialog_PACKAGE_BASE_INFO" class="easyui-dialog" data-options="title:'对话框',modal:false,height:'auto',closed:true">
        <div id="Tab_Dialog_PACKAGE_BASE_INFO" class="easyui-tabs" data-options="border:false">
            <div title="基本信息" style="padding: 5px;" data-options="selected:true">
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
                            <input type="text" id="PACKAGE_NO" class="easyui-validatebox" data-options="required:true,validType:['maxLength[20]']" />(少于20个字) </td>
                        <td style="text-align: right;">版本号<b style="color: red; font-size: 15px;">*</b></td>
                        <td>
                            <input type="text" id="VERSION_NO" class="easyui-validatebox" data-options="required:false,validType:['maxLength[1]','alpha']" />
                            <a id="btn_GenerateVersionNO" class="easyui-linkbutton">生成版本号</a>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">产品类型<b style="color: red; font-size: 15px;">*</b></td>
                        <td>
                            <input type="text" id="PRODUCT_TYPE_ID" class="easyui-combobox" /></td>
                        <td style="text-align: right;">产品工艺<b style="color: red; font-size: 15px;">*</b></td>
                        <td>
                            <input type="text" id="PRODUCT_PROC_TYPE_ID" class="easyui-combobox" /></td>
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
                        <td style="text-align: right;">数量</td>
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
                        <td style="text-align: right;">目的</td>
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
                            <input id="OUTPUT_TARGET_DATE" class="easyui-datebox" data-options="editable:false,required:true" /></td>
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
                            <select id="VALID_FLAG">
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
                            <input type="text" id="UPDATE_USER" /></td>
                        <td style="text-align: right;">最后更新日期</td>
                        <td>
                            <input type="text" id="UPDATE_DATE" /></td>
                    </tr>
                </table>
            </div>
        </div>

    </div>




</asp:Content>
