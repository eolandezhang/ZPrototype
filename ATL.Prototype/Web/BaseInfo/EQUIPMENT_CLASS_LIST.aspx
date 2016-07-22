<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EQUIPMENT_CLASS_LIST.aspx.cs" Inherits="Web.BaseInfo.EQUIPMENT_CLASS_LIST" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/BaseInfo/EQUIPMENT_CLASS_LIST.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table id="Table_EQUIPMENT_CLASS_LIST"></table>
<div id="Dialog_EQUIPMENT_CLASS_LIST"  class="easyui-dialog" data-options="title:'对话框',modal:false,width:500,height:400,closed:true" style="padding: 10px;">
    <table cellspacing="0" cellspadding="0">
        <tr>
            <td style="text-align: right;">EQUIPMENT_CLASS_ID<b style="color: red; font-size: 15px;">*</b></td>
            <td><input type="text" id="EQUIPMENT_CLASS_ID"   class="easyui-validatebox" data-options="required:false,validType:['maxLength[20]']"  />(少于20个字) </td>
        </tr>
        <tr>
            <td style="text-align: right;">FACTORY_ID<b style="color: red; font-size: 15px;">*</b></td>
            <td><input type="text" id="FACTORY_ID"   class="easyui-validatebox" data-options="required:false,validType:['maxLength[5]']"  />(少于5个字) </td>
        </tr>
        <tr>
            <td style="text-align: right;">UPDATE_USER</td>
            <td><input type="text" id="UPDATE_USER"   class="easyui-validatebox" data-options="required:false,validType:['maxLength[10]']"  />(少于10个字) </td>
        </tr>
        <tr>
            <td style="text-align: right;">UPDATE_DATE</td>
            <td><input type="text" id="UPDATE_DATE"  /></td>
        </tr>
        <tr>
            <td style="text-align: right;">EQUIPMENT_CLASS_NAME</td>
            <td><input type="text" id="EQUIPMENT_CLASS_NAME"   class="easyui-validatebox" data-options="required:false,validType:['maxLength[20]']"  />(少于20个字) </td>
        </tr>
        <tr>
            <td style="text-align: right;">EQUIPMENT_CLASS_DESC</td>
            <td><input type="text" id="EQUIPMENT_CLASS_DESC"   class="easyui-validatebox" data-options="required:false,validType:['maxLength[25]']"  />(少于25个字) </td>
        </tr>
        <tr>
            <td style="text-align: right;">CLASSIFIED_BY</td>
            <td><input type="text" id="CLASSIFIED_BY"   class="easyui-validatebox" data-options="required:false,validType:['maxLength[50]']"  />(少于50个字) </td>
        </tr>
        <tr>
            <td style="text-align: right;">VALID_FLAG</td>
            <td><input type="text" id="VALID_FLAG"   class="easyui-validatebox" data-options="required:false,validType:['maxLength[1]']"  />(少于1个字) </td>
        </tr>
    </table>
</div>

</asp:Content>
