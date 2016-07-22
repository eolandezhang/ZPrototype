<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PROCESS_GROUP_LIST.aspx.cs" Inherits="Web.BaseInfo.PROCESS_GROUP_LIST" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/BaseInfo/PROCESS_GROUP_LIST.js"></script>
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
                <table id="Table_PROCESS_LIST"></table>
            </td>
            <td style="vertical-align: top;">
                <table id="Table_PROCESS_GROUP_LIST"></table>
            </td>
        </tr>
    </table>

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
                    <input type="text" id="SEQUENCE_NO"  class="easyui-validatebox" data-options="required:false,validType:'number'" /></td>
            </tr>
            <tr>
                <td style="text-align: right;">启用/停用</td>
                <td>
                    <select id="VALID_FLAG">
                        <option value="1" selected="selected">启用</option>
                        <option value="0">停用</option>
                    </select>
            </tr>
            <tr>
                <td style="text-align: right;">最后更新者</td>
                <td>
                    <input type="text" id="UPDATE_USER" />(少于10个字) </td>
            </tr>
            <tr>
                <td style="text-align: right;">最后更新日期</td>
                <td>
                    <input type="text" id="UPDATE_DATE" /></td>
            </tr>
        </table>
    </div>


</asp:Content>
