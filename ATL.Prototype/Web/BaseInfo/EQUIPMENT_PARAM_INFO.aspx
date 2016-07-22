<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EQUIPMENT_PARAM_INFO.aspx.cs" Inherits="Web.BaseInfo.EQUIPMENT_PARAM_INFO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/BaseInfo/EQUIPMENT_PARAM_INFO.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellspacing="0">
        <tr>
            <td style="vertical-align: top;">
                <input id="FACTORY_ID_SEARCH" class="easyui-combobox" style="width: 240px;" /><br />
                <div style="height: 2px; overflow: hidden;"></div>
                <input id="PRODUCT_TYPE_ID_SEARCH" class="easyui-combobox" style="width: 240px;" /><br />
                <div style="height: 2px; overflow: hidden;"></div>
                <input id="PRODUCT_PROC_TYPE_ID_SEARCH" class="easyui-combobox" style="width: 240px;" /><br />
                <div style="height: 2px; overflow: hidden;"></div>
                <table id="Table_EQUIPMENT_TYPE_LIST"></table>
                <div style="height: 2px; overflow: hidden;"></div>
                <table id="Table_PROCESS_LIST"></table>

            </td>
            <td style="vertical-align: top;">
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="vertical-align: top;">
                            <table id="Table_EQUIPMENT_PROC_INFO"></table>
                        </td>
                        <td style="vertical-align: top; padding-left: 2px;">
                            <table id="Table_EQUIPMENT_PARAM_INFO"></table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="padding-top:2px;">
                            <table id="Table_PARAMETER_LIST"></table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>


    <div id="Dialog_EQUIPMENT_PARAM_INFO" class="easyui-dialog" data-options="title:'对话框',modal:false,width:400,height:'auto',closed:true" style="padding: 10px;">
        <table cellspacing="0" cellspadding="0">                        
            <tr>
                <td style="text-align: right;">PARAMETER_ID<b style="color: red; font-size: 15px;">*</b></td>
                <td>
                    <input type="text" id="PARAMETER_ID" />(少于25个字) </td>
            </tr>
            <tr>
                <td style="text-align: right;">DISP_ORDER_NO</td>
                <td>
                    <input type="text" id="DISP_ORDER_NO" /></td>
            </tr>
            <tr>
                <td style="text-align: right;">DISP_ORDER_IN_TASK</td>
                <td>
                    <input type="text" id="DISP_ORDER_IN_TASK" /></td>
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
                <td style="text-align: right;">UPDATE_USER</td>
                <td>
                    <input type="text" id="UPDATE_USER" />(少于10个字) </td>
            </tr>
            <tr>
                <td style="text-align: right;">UPDATE_DATE</td>
                <td>
                    <input type="text" id="UPDATE_DATE" /></td>
            </tr>
        </table>
    </div>

    <div id="Dialog_EQUIPMENT_PROC_INFO" class="easyui-dialog" data-options="title:'对话框',modal:false,width:500,height:'auto',closed:true" style="padding: 10px;">
        <table cellspacing="0" cellpadding="0">
            <tr>
                <td style="text-align: right;">设备编号<b style="color: red; font-size: 15px;">*</b></td>
                <td>
                    <input type="text" id="EQUIPMENT_ID" />
                    <b style="color:red; display: none;" id="alert_EQUIPMENT_ID">不存在</b>

                </td>
            </tr>
            <tr>
                <td style="text-align: right;">备注</td>
                <td>
                    <input type="text" id="REMARK" class="easyui-validatebox" data-options="required:false,validType:['maxLength[25]']" />(少于25个字) </td>
            </tr>
            <tr>
                <td style="text-align: right;">最后更新者</td>
                <td>
                    <input type="text" id="UPDATE_USER_EQUIPMENT_PROC_INFO" class="easyui-validatebox" data-options="required:false,validType:['maxLength[10]']" />(少于10个字) </td>
            </tr>
            <tr>
                <td style="text-align: right;">最后更新日期</td>
                <td>
                    <input type="text" id="UPDATE_DATE_EQUIPMENT_PROC_INFO" /></td>
            </tr>
        </table>
    </div>

</asp:Content>
