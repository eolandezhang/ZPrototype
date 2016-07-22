<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="APPROVE_FLOW_LIST.aspx.cs" Inherits="Web.BaseInfo.APPROVE_FLOW_LIST" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/BaseInfo/APPROVE_FLOW_LIST.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table id="Table_APPROVE_FLOW_LIST"></table>
<div id="Dialog_APPROVE_FLOW_LIST"  class="easyui-dialog" data-options="title:'对话框',modal:false,width:500,height:400,closed:true" style="padding: 10px;">
    <table cellspacing="0" cellspadding="0">
        <tr>
            <td style="text-align: right;">APPROVE_FLOW_ID<b style="color: red; font-size: 15px;">*</b></td>
            <td><input type="text" id="APPROVE_FLOW_ID" />(少于10个字) </td>
        </tr>
        <tr>
            <td style="text-align: right;">FACTORY_ID<b style="color: red; font-size: 15px;">*</b></td>
            <td><input type="text" id="FACTORY_ID" />(少于5个字) </td>
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
            <td style="text-align: right;">VALID_FLAG</td>
            <td><input type="text" id="VALID_FLAG" />(少于1个字) </td>
        </tr>
        <tr>
            <td style="text-align: right;">APPROVE_FLOW_DESC</td>
            <td><input type="text" id="APPROVE_FLOW_DESC" />(少于25个字) </td>
        </tr>
        <tr>
            <td style="text-align: right;">OWNER_APPROVE1</td>
            <td><input type="text" id="OWNER_APPROVE1" />(少于10个字) </td>
        </tr>
        <tr>
            <td style="text-align: right;">OWNER_APPROVE2</td>
            <td><input type="text" id="OWNER_APPROVE2" />(少于10个字) </td>
        </tr>
        <tr>
            <td style="text-align: right;">PROTO_APPROVE1</td>
            <td><input type="text" id="PROTO_APPROVE1" />(少于10个字) </td>
        </tr>
        <tr>
            <td style="text-align: right;">PROTO_APPROVE2</td>
            <td><input type="text" id="PROTO_APPROVE2" />(少于10个字) </td>
        </tr>
    </table>
</div>


</asp:Content>
