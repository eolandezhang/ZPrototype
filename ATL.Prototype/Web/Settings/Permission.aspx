<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Permission.aspx.cs" Inherits="Web.Settings.Permission" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Settings/Permission.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <input id="FACTORY_ID_SEARCH" class="easyui-combobox" style="width: 240px;" /><br />
    <table cellpadding="0" cellspacing="0">
        <tr>
            <td style="vertical-align: top; padding: 0 2px 2px 0;">
                <table id="Table_PMES_USER_GROUP_LIST"></table>
                <div id="Dialog_PMES_USER_GROUP_LIST" class="easyui-dialog" data-options="title:'对话框',modal:false,width:500,height:400,closed:true" style="padding: 10px;">
                    <table cellspacing="0" cellpadding="0">
                        <tr>
                            <td style="text-align: right;">编号<b style="color: red; font-size: 15px;">*</b></td>
                            <td>
                                <input type="text" id="PMES_USER_GROUP_ID" class="easyui-validatebox" data-options="required:false,validType:['maxLength[20]']" />(少于20个字) </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">英文名称</td>
                            <td>
                                <input type="text" id="USER_GROUP_NAME" class="easyui-validatebox" data-options="required:false,validType:['maxLength[20]']" />(少于20个字) </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">中文名称</td>
                            <td>
                                <input type="text" id="USER_GROUP_DESC" class="easyui-validatebox" data-options="required:false,validType:['maxLength[25]']" />(少于25个字) </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">最后更新者</td>
                            <td>
                                <input type="text" id="UPDATE_USER_PMES_USER_GROUP_LIST" class="easyui-validatebox" data-options="required:false,validType:['maxLength[10]']" />(少于10个字) </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">最后更新日期</td>
                            <td>
                                <input type="text" id="UPDATE_DATE_PMES_USER_GROUP_LIST" /></td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">状态</td>
                            <td>
                                <select id="VALID_FLAG_PMES_USER_GROUP_LIST">
                                    <option value="1" selected="selected">启用</option>
                                    <option value="0">停用</option>
                                </select>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
            <td style="vertical-align: top;">
                <table id="Table_PMES_TASK_LIST"></table>
                <div id="Dialog_PMES_TASK_LIST" class="easyui-dialog" data-options="title:'对话框',modal:false,width:500,height:400,closed:true" style="padding: 10px;">
                    <table cellspacing="0" cellpadding="0">
                        <tr>
                            <td style="text-align: right;">编号<b style="color: red; font-size: 15px;">*</b></td>
                            <td>
                                <input type="text" id="PMES_TASK_ID" class="easyui-validatebox" data-options="required:false,validType:['maxLength[20]']" />(少于20个字) </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">英文名</td>
                            <td>
                                <input type="text" id="TASK_NAME" class="easyui-validatebox" data-options="required:false,validType:['maxLength[20]']" />(少于20个字) </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">中文名</td>
                            <td>
                                <input type="text" id="TASK_DESC" class="easyui-validatebox" data-options="required:false,validType:['maxLength[25]']" />(少于25个字) </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">应用</td>
                            <td>
                                <input type="text" id="PROGRAM_NAME" class="easyui-validatebox" data-options="required:false,validType:['maxLength[25]']" />(少于25个字) </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">模块</td>
                            <td>
                                <input type="text" id="MODULE_NAME" class="easyui-validatebox" data-options="required:false,validType:['maxLength[25]']" />(少于25个字) </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">方法</td>
                            <td>
                                <input type="text" id="FUNCTIONS" class="easyui-validatebox" data-options="required:false,validType:['maxLength[50]']" />(少于50个字) </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">菜单名</td>
                            <td>
                                <input type="text" id="MENU_NAME" class="easyui-validatebox" data-options="required:false,validType:['maxLength[25]']" />(少于25个字) </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">层</td>
                            <td>
                                <input type="text" id="MENU_LAYER" class="easyui-validatebox" data-options="required:false,validType:'number'" /></td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">父目录</td>
                            <td>
                                <input type="text" id="PARENT_MENU" class="easyui-validatebox" data-options="required:false,validType:['maxLength[25]']" />(少于25个字) </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">最后更新者</td>
                            <td>
                                <input type="text" id="UPDATE_USER_PMES_TASK_LIST" class="easyui-validatebox" data-options="required:false,validType:['maxLength[10]']" />(少于10个字) </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">最后更新日期</td>
                            <td>
                                <input type="text" id="UPDATE_DATE_PMES_TASK_LIST" /></td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">状态</td>
                            <td>
                                <select id="VALID_FLAG_PMES_TASK_LIST">
                                    <option value="1" selected="selected">启用</option>
                                    <option value="0">停用</option>
                                </select>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
            <td rowspan="2"  style="vertical-align: top; padding: 0 0px 0px 2px;">
                <table id="Table_PMES_USER_TASK_INFO"></table>
                <div id="Dialog_PMES_USER_TASK_INFO" class="easyui-dialog" data-options="title:'对话框',modal:false,width:500,height:400,closed:true" style="padding: 10px;">
                    <table cellspacing="0" cellpadding="0">
                        <tr>
                            <td style="text-align: right;">工号<b style="color: red; font-size: 15px;">*</b></td>
                            <td>
                                <input type="text" id="PMES_USER_ID_PMES_USER_TASK_INFO" readonly="readonly" />
                                <a id="btn_PMES_USER_ID_PMES_USER_TASK_INFO" class="easyui-linkbutton">选择</a>
                                <div id="Dialog_PMES_USER_ID_PMES_USER_TASK_INFO" class="easyui-dialog" data-options="title:'对话框',modal:false,width:400,height:400,closed:true" style="padding: 10px;">
                                    工号<input id="Search_PMES_USER_ID_PMES_USER_TASK_INFO" /><a id="btn_Search_PMES_USER_ID_PMES_USER_TASK_INFO" class="easyui-linkbutton">查询</a><br />
                                    <table id="Table_PMES_USER_ID_PMES_USER_TASK_INFO"></table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">最后更新者</td>
                            <td>
                                <input type="text" id="UPDATE_USER_PMES_USER_TASK_INFO" class="easyui-validatebox" data-options="required:false,validType:['maxLength[10]']" />(少于10个字) </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">最后更新日期</td>
                            <td>
                                <input type="text" id="UPDATE_DATE_PMES_USER_TASK_INFO" /></td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">状态</td>
                            <td>
                                <input type="text" id="VALID_FLAG_PMES_USER_TASK_INFO" class="easyui-validatebox" data-options="required:false,validType:['maxLength[1]']" />(少于1个字) </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top;">
                <table id="Table_PMES_USER_GRP_TASK_INFO"></table>
                <div id="Dialog_PMES_USER_GRP_TASK_INFO" class="easyui-dialog" data-options="title:'对话框',modal:false,width:500,height:200,closed:true" style="padding: 10px;">
                    <table cellspacing="0" cellpadding="0">

                        <tr>
                            <td style="text-align: right;">最后更新者</td>
                            <td>
                                <input type="text" id="UPDATE_USER_PMES_USER_GRP_TASK_INFO" class="easyui-validatebox" data-options="required:false,validType:['maxLength[10]']" />(少于10个字) </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">最后更新日期</td>
                            <td>
                                <input type="text" id="UPDATE_DATE_PMES_USER_GRP_TASK_INFO" /></td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">状态</td>
                            <td>
                                <select id="VALID_FLAG_PMES_USER_GRP_TASK_INFO">
                                    <option value="1" selected="selected">启用</option>
                                    <option value="0">停用</option>
                                </select>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
            <td style="vertical-align: top;">
                <table id="Table_PMES_USER_GROUP_INFO"></table>
                <div id="Dialog_PMES_USER_GROUP_INFO" class="easyui-dialog" data-options="title:'对话框',modal:false,width:500,height:250,closed:true" style="padding: 10px;">
                    <table cellspacing="0" cellpadding="0">
                        <tr>
                            <td style="text-align: right;">工号<b style="color: red; font-size: 15px;">*</b>
                            </td>
                            <td>
                                <input type="text" id="PMES_USER_ID" readonly="readonly" />
                                <a id="btn_PMES_USER_ID" class="easyui-linkbutton">选择</a>
                                <div id="Dialog_PMES_USER_ID" class="easyui-dialog" data-options="title:'对话框',modal:false,width:400,height:400,closed:true" style="padding: 10px;">
                                    工号<input id="Search_PMES_USER_ID" /><a id="btn_Search" class="easyui-linkbutton">查询</a><br />
                                    <table id="Table_PMES_USER_ID"></table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">备注</td>
                            <td>
                                <input type="text" id="REMARK" class="easyui-validatebox" data-options="required:false,validType:['maxLength[15]']" />(少于15个字) </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">状态</td>
                            <td>
                                <select id="VALID_FLAG_PMES_USER_GROUP_INFO">
                                    <option value="1" selected="selected">启用</option>
                                    <option value="0">停用</option>
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">最后更新者</td>
                            <td>
                                <input type="text" id="UPDATE_USER_PMES_USER_GROUP_INFO" class="easyui-validatebox" data-options="required:false,validType:['maxLength[10]']" />(少于10个字) </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">最后更新日期</td>
                            <td>
                                <input type="text" id="UPDATE_DATE_PMES_USER_GROUP_INFO" /></td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
