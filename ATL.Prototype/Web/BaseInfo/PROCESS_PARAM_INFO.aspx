<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PROCESS_PARAM_INFO.aspx.cs" Inherits="Web.BaseInfo.PROCESS_PARAM_INFO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/BaseInfo/PROCESS_PARAM_INFO.js"></script>
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
                <table id="Table_PROCESS_PARAM_INFO"></table>
                <div style="height: 2px; overflow: hidden;"></div>
                <table id="Table_PARAMETER_LIST"></table>
            </td>
        </tr>
    </table>

    <div id="Dialog_PROCESS_PARAM_INFO" class="easyui-dialog" data-options="title:'对话框',modal:false,width:500,height:'auto',closed:true" style="padding: 10px;">
        <table cellspacing="0" cellspadding="0">
            <tr>
                <td style="text-align: right;">序号</td>
                <td>
                    <input type="text" id="PARAM_ORDER_NO"  class="easyui-validatebox" data-options="required:false,validType:'number'" /></td>
            </tr>
            <tr>
                <td style="text-align: right;">参数<b style="color: red; font-size: 15px;">*</b></td>
                <td>
                    <input type="text" id="PARAMETER_ID" style="width: 300px;" /></td>
            </tr>
            <tr>
                <td style="text-align: right;">是图片参数</td>
                <td>
                    <select id="IS_ILLUSTRATION_PARAM">
                        <option value="0" selected="selected">否</option>
                        <option value="1">是</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">在规格牌中</td>
                <td>
                    <select id="IS_SC_PARAM">
                        <option value="0" selected="selected">否</option>
                        <option value="1">是</option>
                    </select>
                </td>
            </tr>            
            <tr>
                <td style="text-align: right;">在规格牌中的序号</td>
                <td>
                    <input type="text" id="DISP_ORDER_IN_SC"  class="easyui-validatebox" data-options="required:false,validType:'number'"/></td>
            </tr>
            <tr>
                <td style="text-align: right;">最后更新者</td>
                <td>
                    <input type="text" id="UPDATE_USER" /></td>
            </tr>
            <tr>
                <td style="text-align: right;">组后更新日期</td>
                <td>
                    <input type="text" id="UPDATE_DATE" /></td>
            </tr>
        </table>
    </div>
      
</asp:Content>
