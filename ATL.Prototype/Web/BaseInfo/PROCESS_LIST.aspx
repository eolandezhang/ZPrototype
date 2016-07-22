<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PROCESS_LIST.aspx.cs" Inherits="Web.BaseInfo.PROCESS_LIST" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/BaseInfo/PROCESS_LIST.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellspacing="0" cellspadding="0">
        <tr>
            <td style="vertical-align: top;">
                <input id="FACTORY_ID_SEARCH" class="easyui-combobox" style="width: 240px;" /><br />
                <div style="height: 2px; overflow: hidden;"></div>
                <input id="PRODUCT_TYPE_ID_SEARCH" class="easyui-combobox" style="width: 240px;" /><br />
                <div style="height: 2px; overflow: hidden;"></div>
                <input id="PRODUCT_PROC_TYPE_ID_SEARCH" class="easyui-combobox" style="width: 240px;" /><br />
                <div style="height: 2px; overflow: hidden;"></div>
                <table id="Table_PROCESS_GROUP_LIST"></table>
            </td>
            <td style="vertical-align: top;">
                <table id="Table_PROCESS_LIST"></table>
            </td>
        </tr>
    </table>

    <div id="Dialog_PROCESS_LIST" class="easyui-dialog" data-options="title:'对话框',modal:false,width:500,height:'auto',closed:true" style="padding: 10px;">
        <table cellspacing="0" cellspadding="0">
            <tr>
                <td style="text-align: right;">序号</td>
                <td>
                    <input type="text" id="SEQUENCE_NO" class="easyui-validatebox" data-options="required:false,validType:'number'" /></td>
            </tr>
            <tr>
                <td style="text-align: right;">编号<b style="color: red; font-size: 15px;">*</b></td>
                <td>
                    <input type="text" id="PROCESS_ID" class="easyui-validatebox" data-options="required:true,validType:['maxLength[20]']" />(少于20个字) </td>
            </tr>
            <tr>
                <td style="text-align: right;">中文名</td>
                <td>
                    <input type="text" id="PROCESS_DESC" class="easyui-validatebox" data-options="required:true,validType:['maxLength[25]']" />(少于25个字) </td>
            </tr>
            <tr>
                <td style="text-align: right;">英文名</td>
                <td>
                    <input type="text" id="PROCESS_NAME" class="easyui-validatebox" data-options="required:false,validType:['maxLength[20]']" />(少于20个字) </td>
            </tr>            
            
            <tr>
                <td style="text-align: right;">前一工序</td>
                <td>
                    <input type="text" id="PREVIOUS_PROCESS_ID" /></td>
            </tr>
            <tr>
                <td style="text-align: right;">后一工序</td>
                <td>
                    <input type="text" id="NEXT_PROCESS_ID" /></td>
            </tr>
            <tr>
                <td style="text-align: right;">停用/启用</td>
                <td>
                    <select id="VALID_FLAG">
                        <option value="1" selected="selected">启用</option>
                        <option value="0">停用</option>
                    </select></td>
            </tr>
            <tr>
                <td style="text-align: right;">WORKSTATION_ID</td>
                <td>
                    <input type="text" id="WORKSTATION_ID" class="easyui-validatebox" data-options="required:false,validType:['maxLength[20]']" />(少于20个字) </td>
            </tr>            
            <tr>
                <td style="text-align: right;">ORDER_IN_GROUP</td>
                <td>
                    <input type="text" id="ORDER_IN_GROUP" class="easyui-validatebox" data-options="required:false,validType:'number'" /></td>
            </tr>
            <tr>
                <td style="text-align: right;">IS_MULTI_TASK</td>
                <td>
                    <select id="IS_MULTI_TASK">
                        <option value="0" selected="selected">否</option>
                        <option value="1">是</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">最后更新者</td>
                <td>
                    <input type="text" id="UPDATE_USER" /></td>
            </tr>
            <tr>
                <td style="text-align: right;">最后更新日期</td>
                <td>
                    <input type="text" id="UPDATE_DATE" /></td>
            </tr>
        </table>
    </div>

    <div id="Dialog_PROCESS_GROUP_LIST" class="easyui-dialog" data-options="title:'对话框',modal:false,width:400,height:'auto',closed:true" style="padding: 10px;">
        <table cellspacing="0" cellspadding="0">
            <tr>
                <td style="text-align: right;">编号<b style="color: red; font-size: 15px;">*</b></td>
                <td>
                    <input type="text" id="PROCESS_GROUP_ID"  class="easyui-validatebox" data-options="required:true,validType:['maxLength[20]']"/>(少于20个字) </td>
            </tr>
            <tr>
                <td style="text-align: right;">中文名称</td>
                <td>
                    <input type="text" id="PROCESS_GROUP_DESC"  class="easyui-validatebox" data-options="required:true,validType:['maxLength[30]']"/>(少于30个字) </td>
            </tr>
            <tr>
                <td style="text-align: right;">英文名称</td>
                <td>
                    <input type="text" id="PROCESS_GROUP_NAME"  class="easyui-validatebox" data-options="required:false,validType:['maxLength[25]']"/>(少于25个字) </td>
            </tr>            
            <tr>
                <td style="text-align: right;">序号</td>
                <td>
                    <input type="text" id="SEQUENCE_NO_PROCESS_GROUP_LIST"  class="easyui-validatebox" data-options="required:false,validType:'number'" /></td>
            </tr>
            <tr>
                <td style="text-align: right;">启用/停用</td>
                <td>
                    <select id="VALID_FLAG_PROCESS_GROUP_LIST">
                        <option value="1" selected="selected">启用</option>
                        <option value="0">停用</option>
                    </select>
            </tr>
            <tr>
                <td style="text-align: right;">最后更新者</td>
                <td>
                    <input type="text" id="UPDATE_USER_PROCESS_GROUP_LIST" />(少于10个字) </td>
            </tr>
            <tr>
                <td style="text-align: right;">最后更新日期</td>
                <td>
                    <input type="text" id="UPDATE_DATE_PROCESS_GROUP_LIST" /></td>
            </tr>
        </table>
    </div>

</asp:Content>
