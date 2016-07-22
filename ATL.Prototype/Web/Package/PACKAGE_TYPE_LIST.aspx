<%@ Page Title="文件类型" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PACKAGE_TYPE_LIST.aspx.cs" Inherits="Web.Package.PACKAGE_TYPE_LIST" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Package/PACKAGE_TYPE_LIST.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table id="Table_PACKAGE_TYPE_LIST"></table>
<div id="Dialog_PACKAGE_TYPE_LIST"  class="easyui-dialog" data-options="title:'对话框',modal:false,width:500,height:400,closed:true" style="padding: 10px;">
    <table cellspacing="0" cellspadding="0">
        <tr>
            <td style="text-align: right;">PACKAGE_TYPE_ID<b style="color: red; font-size: 15px;">*</b></td>
            <td><input type="text" id="PACKAGE_TYPE_ID" />(少于5个字) </td>
        </tr>
        <tr>
            <td style="text-align: right;">FACTORY_ID<b style="color: red; font-size: 15px;">*</b></td>
            <td><input type="text" id="FACTORY_ID" />(少于5个字) </td>
        </tr>
        <tr>
            <td style="text-align: right;">PACKAGE_TYPE_DESC</td>
            <td><input type="text" id="PACKAGE_TYPE_DESC" />(少于25个字) </td>
        </tr>
        <tr>
            <td style="text-align: right;">PACKAGE_CODE</td>
            <td><input type="text" id="PACKAGE_CODE" />(少于2个字) </td>
        </tr>
        <tr>
            <td style="text-align: right;">VALID_FLAG</td>
            <td><input type="text" id="VALID_FLAG" />(少于1个字) </td>
        </tr>
        <tr>
            <td style="text-align: right;">UPDATE_USER</td>
            <td><input type="text" id="UPDATE_USER" />(少于10个字) </td>
        </tr>
        <tr>
            <td style="text-align: right;">UPDATE_DATE</td>
            <td><input type="text" id="UPDATE_DATE" /></td>
        </tr>
        <tr>
            <td style="text-align: right;">GROUPS_LIMIT</td>
            <td><input type="text" id="GROUPS_LIMIT" /></td>
        </tr>
    </table>
</div>




</asp:Content>
