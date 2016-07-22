<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WF_SET.aspx.cs" Inherits="Web.Settings.WF_SET" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Settings/WF_SET.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <input id="FACTORY_ID" class="easyui-combobox" style="width: 240px;" /><br />
    <table cellspacing="0" cellpadding="0">
        <tr>
            <td style="vertical-align: top;">
                <table id="Table_WF_SET"></table>
                <div id="Dialog_WF_SET" class="easyui-dialog" data-options="title:'对话框',modal:false,width:500,height:250,closed:true" style="padding: 10px;">
                    <table cellspacing="0" cellpadding="0">
                        <tr>
                            <td style="text-align: right;">编号<b style="color: red; font-size: 15px;">*</b></td>
                            <td>
                                <input type="text" id="WF_SET_NUM" class="easyui-validatebox" data-options="required:false,validType:['maxLength[20]']" />(少于20个字) </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">名称</td>
                            <td>
                                <input type="text" id="WF_SET_NAME" class="easyui-validatebox" data-options="required:false,validType:['maxLength[30]']" />(少于30个字) </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">最后更新者</td>
                            <td>
                                <input type="text" id="UPDATE_USER" class="easyui-validatebox" data-options="required:false,validType:['maxLength[10]']" />(少于10个字) </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">最后更新日期</td>
                            <td>
                                <input type="text" id="UPDATE_DATE" /></td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">状态</td>
                            <td>
                                <select id="VALID_FLAG">
                                    <option value="1" selected="selected">启用</option>
                                    <option value="0">停用</option>
                                </select>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
            <td style="vertical-align: top; padding: 0 0 0 2px;">
                <table id="Table_WF_SET_STEP"></table>
                <div id="Dialog_WF_SET_STEP" class="easyui-dialog" data-options="title:'对话框',modal:false,width:500,height:400,closed:true" style="padding: 10px;">
                    <table cellspacing="1" cellpadding="0">
                        <tr>
                            <td style="text-align: right;">编号<b style="color: red; font-size: 15px;">*</b></td>
                            <td>
                                <input type="text" id="WF_SET_STEP_ID" class="easyui-validatebox" data-options="required:false,validType:['maxLength[20]']" />(少于20个字) </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">序号</td>
                            <td>
                                <input type="text" id="ORDER_NUM" class="easyui-validatebox" data-options="required:false,validType:'number'" /></td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">名称<b style="color: red; font-size: 15px;">*</b></td>
                            <td>
                                <input type="text" id="WF_SET_STEP_NAME" class="easyui-validatebox" data-options="required:false,validType:['maxLength[20]']" />(少于20个字) </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">转交下一步</td>
                            <td>
                                <input type="text" id="AGREE_STEP_ID" class="easyui-validatebox" data-options="required:false,validType:['maxLength[20]']" />(少于20个字) </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">退回上一步</td>
                            <td>
                                <input type="text" id="DISAGREE_STEP_ID" class="easyui-validatebox" data-options="required:false,validType:['maxLength[20]']" />(少于20个字) </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">签审用户组</td>
                            <td>
                                <input type="text" id="PMES_USER_GROUP_ID" class="easyui-validatebox" data-options="required:false,validType:['maxLength[20]']" />(少于20个字) </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">标识</td>
                            <td>
                                <select id="STEP_FLAG">
                                    <option value="MDL" selected="selected">中间步骤</option>
                                    <option value="FST">第一步</option>
                                    <option value="LST">最后一步</option>
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">最后更新者</td>
                            <td>
                                <input type="text" id="UPDATE_USER_WF_SET_STEP" class="easyui-validatebox" data-options="required:false,validType:['maxLength[10]']" />(少于10个字) </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">最后更新日期</td>
                            <td>
                                <input type="text" id="UPDATE_DATE_WF_SET_STEP" /></td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">状态</td>
                            <td>
                                <select id="VALID_FLAG_WF_SET_STEP">
                                    <option value="1" selected="selected">启用</option>
                                    <option value="0">停用</option>
                                </select>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
