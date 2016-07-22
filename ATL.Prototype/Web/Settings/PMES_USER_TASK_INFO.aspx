<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PMES_USER_TASK_INFO.aspx.cs" Inherits="Web.Settings.PMES_USER_TASK_INFO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Settings/PMES_USER_TASK_INFO.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table id="Table_PMES_USER_TASK_INFO"></table>
    <div id="Dialog_PMES_USER_TASK_INFO" class="easyui-dialog" data-options="title:'对话框',modal:false,width:500,height:400,closed:true" style="padding: 10px;">
        <table cellspacing="0" cellpadding="0">
            <tr>
                <td style="text-align: right;">PMES_USER_ID<b style="color: red; font-size: 15px;">*</b></td>
                <td>
                    <input type="text" id="PMES_USER_ID" class="easyui-validatebox" data-options="required:false,validType:['maxLength[10]']" />(少于10个字) </td>
            </tr>
            <tr>
                <td style="text-align: right;">PMES_TASK_ID<b style="color: red; font-size: 15px;">*</b></td>
                <td>
                    <input type="text" id="PMES_TASK_ID" class="easyui-validatebox" data-options="required:false,validType:['maxLength[20]']" />(少于20个字) </td>
            </tr>
            <tr>
                <td style="text-align: right;">FACTORY_ID<b style="color: red; font-size: 15px;">*</b></td>
                <td>
                    <input type="text" id="FACTORY_ID" class="easyui-validatebox" data-options="required:false,validType:['maxLength[5]']" />(少于5个字) </td>
            </tr>
            <tr>
                <td style="text-align: right;">UPDATE_USER</td>
                <td>
                    <input type="text" id="UPDATE_USER" class="easyui-validatebox" data-options="required:false,validType:['maxLength[10]']" />(少于10个字) </td>
            </tr>
            <tr>
                <td style="text-align: right;">UPDATE_DATE</td>
                <td>
                    <input type="text" id="UPDATE_DATE" /></td>
            </tr>
            <tr>
                <td style="text-align: right;">VALID_FLAG</td>
                <td>
                    <input type="text" id="VALID_FLAG" class="easyui-validatebox" data-options="required:false,validType:['maxLength[1]']" />(少于1个字) </td>
            </tr>
        </table>
    </div>
</asp:Content>
