<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FACTORY_LIST.aspx.cs" Inherits="Web.BaseInfo.FACTORY_LIST" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/BaseInfo/FACTORY_LIST.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table id="Table_FACTORY_LIST"></table>
    <div id="Dialog_FACTORY_LIST" class="easyui-dialog" data-options="title:'对话框',modal:false,width:400,height:'auto',closed:true" style="padding: 10px;">
        <table cellspacing="0" cellspadding="0">
            <tr>
                <td style="text-align: right;">FACTORY_ID<b style="color: red; font-size: 15px;">*</b></td>
                <td>
                    <input type="text" id="FACTORY_ID" />(少于5个字) </td>
            </tr>
            <tr>
                <td style="text-align: right;">UPDATE_USER</td>
                <td>
                    <input type="text" id="UPDATE_USER" />(少于10个字) </td>
            </tr>
            <tr>
                <td style="text-align: right;">UPDATE_DATE</td>
                <td>
                    <input type="text" id="UPDATE_DATE" /></td>
            </tr>
            <tr>
                <td style="text-align: right;">FACTORY_NAME</td>
                <td>
                    <input type="text" id="FACTORY_NAME" />(少于15个字) </td>
            </tr>
            <tr>
                <td style="text-align: right;">FACTORY_DESC</td>
                <td>
                    <input type="text" id="FACTORY_DESC" />(少于20个字) </td>
            </tr>
        </table>
    </div>


</asp:Content>
