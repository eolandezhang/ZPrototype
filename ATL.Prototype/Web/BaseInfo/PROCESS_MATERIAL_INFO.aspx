<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PROCESS_MATERIAL_INFO.aspx.cs" Inherits="Web.BaseInfo.PROCESS_MATERIAL_INFO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/BaseInfo/PROCESS_MATERIAL_INFO.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellspacing="0" cellpadding="0">
        <tr>
            <td style="vertical-align: top;">
                <table cellspacing="0" cellpadding="0">
                    <tr>
                        <td style="vertical-align: top;">
                            <input id="FACTORY_ID_SEARCH" class="easyui-combobox" style="width: 240px;" /><br />
                            <div style="height: 2px; overflow: hidden;"></div>
                            <input id="PRODUCT_TYPE_ID_SEARCH" class="easyui-combobox" style="width: 240px;" /><br />
                            <div style="height: 2px; overflow: hidden;"></div>
                            <input id="PRODUCT_PROC_TYPE_ID_SEARCH" class="easyui-combobox" style="width: 240px;" /><br />
                            <div style="height: 2px; overflow: hidden;"></div>
                            <table id="Table_MATERIAL_CATEGORY_LIST"></table>
                            <div style="height: 2px; overflow: hidden;"></div>
                            <table id="Table_PROCESS_LIST"></table>
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                <table cellspacing="0" cellpadding="1">
                    <tr>
                        <td style="vertical-align: top;">
                            <table id="Table_MATERIAL_TYPE_LIST"></table>
                            <div style="height: 2px; overflow: hidden;"></div>
                            <table id="Table_MATERIAL_PARA_INFO"></table>
                        </td>
                        <td style="vertical-align: top;">
                            <table id="Table_MATERIAL_PN_LIST"></table>
                            <div style="height: 2px; overflow: hidden;"></div>
                            <table id="Table_MATERIAL_PN_PARA_INFO"></table>
                        </td>
                        <td style="vertical-align: top;" colspan="3">
                            <table id="Table_MATERIAL_TYPE_GRP"></table>
                            <div style="height: 2px; overflow: hidden;"></div>
                            <table id="Table_MATERIAL_TYPE_GRP_LIST"></table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="vertical-align: top;">
                            <table id="Table_PARAMETER_LIST"></table>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top;">
                            <table id="Table_PROCESS_MATERIAL_INFO"></table>
                        </td>
                        <td style="vertical-align: top;">
                            <table id="Table_PROCESS_MATERIAL_PN_INFO"></table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>


    <div id="Dialog_PROCESS_MATERIAL_INFO" class="easyui-dialog" data-options="title:'对话框',modal:false,width:500,height:'auto',closed:true" style="padding: 10px;">
        <table cellspacing="0" cellspadding="0">
            <tr>
                <td style="text-align: right;">最后更新人</td>
                <td>
                    <input type="text" id="UPDATE_USER" class="easyui-validatebox" data-options="required:false,validType:['maxLength[10]']" />(少于10个字) </td>
            </tr>
            <tr>
                <td style="text-align: right;">最后更新日期</td>
                <td>
                    <input type="text" id="UPDATE_DATE" /></td>
            </tr>
            <tr>
                <td style="text-align: right;">状态</td>
                <td>
                    <select id="VALID_FLAG">
                        <option value="1">启用</option>
                        <option value="0">停用</option>
                    </select>
                </td>
            </tr>
        </table>
    </div>

    <div id="Dialog_MATERIAL_TYPE_LIST" class="easyui-dialog" data-options="title:'对话框',modal:false,width:400,height:'auto',closed:true" style="padding: 10px;">
        <table cellspacing="0" cellspadding="0">
            <tr>
                <td style="text-align: right;">编号</td>
                <td>
                    <input type="text" id="MATERIAL_TYPE_ID" />(少于20个字) </td>
            </tr>
            <tr>
                <td style="text-align: right;">英文名</td>
                <td>
                    <input type="text" id="MATERIAL_TYPE_NAME" />(少于20个字) </td>
            </tr>
            <tr>
                <td style="text-align: right;">名称</td>
                <td>
                    <input type="text" id="MATERIAL_TYPE_DESC" />(少于20个字) </td>
            </tr>
            <tr>
                <td style="text-align: right;">最后更新者</td>
                <td>
                    <input type="text" id="UPDATE_USER_MATERIAL_TYPE_LIST" />(少于10个字) </td>
            </tr>
            <tr>
                <td style="text-align: right;">最后更新日期</td>
                <td>
                    <input type="text" id="UPDATE_DATE_MATERIAL_TYPE_LIST" /></td>
            </tr>
            <tr>
                <td style="text-align: right;">状态</td>
                <td>
                    <select id="VALID_FLAG_MATERIAL_TYPE_LIST">
                        <option value="1">启用</option>
                        <option value="0">停用</option>
                    </select>
                </td>
            </tr>
        </table>
    </div>

    <div id="Dialog_MATERIAL_PN_LIST" class="easyui-dialog" data-options="title:'对话框',modal:false,width:400,height:'auto',closed:true" style="padding: 10px;">
        <table cellspacing="0" cellspadding="0">
            <tr>
                <td style="text-align: right;">编号<b style="color: red; font-size: 15px;">*</b></td>
                <td>
                    <input type="text" id="MATERIAL_PN_ID" />(少于20个字) </td>
            </tr>
            <tr>
                <td style="text-align: right;">英文名</td>
                <td>
                    <input type="text" id="MATERIAL_PN_NAME" />(少于20个字) </td>
            </tr>
            <tr>
                <td style="text-align: right;">名称</td>
                <td>
                    <input type="text" id="MATERIAL_PN_DESC" />(少于20个字) </td>
            </tr>
            <tr>
                <td style="text-align: right;">最后更新人</td>
                <td>
                    <input type="text" id="UPDATE_USER_MATERIAL_PN_LIST" />(少于10个字) </td>
            </tr>
            <tr>
                <td style="text-align: right;">最后更新日期</td>
                <td>
                    <input type="text" id="UPDATE_DATE_MATERIAL_PN_LIST" /></td>
            </tr>
            <tr>
                <td style="text-align: right;">状态</td>
                <td>
                    <select id="VALID_FLAG_MATERIAL_PN_LIST">
                        <option value="1">启用</option>
                        <option value="0">停用</option>
                    </select>
                </td>
            </tr>
        </table>
    </div>

    <div id="Dialog_PROCESS_MATERIAL_PN_INFO" class="easyui-dialog" data-options="title:'对话框',modal:false,width:400,height:'auto',closed:true" style="padding: 10px;">
        <table cellspacing="0" cellspadding="0">
            <tr>
                <td style="text-align: right;">最后更新者</td>
                <td>
                    <input type="text" id="UPDATE_USER_PROCESS_MATERIAL_PN_INFO" />(少于10个字) </td>
            </tr>
            <tr>
                <td style="text-align: right;">最后更新日期</td>
                <td>
                    <input type="text" id="UPDATE_DATE_PROCESS_MATERIAL_PN_INFO" /></td>
            </tr>
        </table>
    </div>

    <div id="Dialog_MATERIAL_PARA_INFO" class="easyui-dialog" data-options="title:'对话框',modal:false,width:500,height:400,closed:true" style="padding: 10px;">
        <table cellspacing="0" cellpadding="0">
            <tr>
                <td style="text-align: right;">参数<b style="color: red; font-size: 15px;">*</b></td>
                <td>
                    <input type="text" id="PARAMETER_ID" /></td>
            </tr>
            <tr>
                <td style="text-align: right;">备注</td>
                <td>
                    <input type="text" id="REMARK" class="easyui-validatebox" data-options="required:false,validType:['maxLength[15]']" />(少于15个字) </td>
            </tr>
            <tr>
                <td style="text-align: right;">最后更新者</td>
                <td>
                    <input type="text" id="UPDATE_USER_MATERIAL_PARA_INFO" />
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">最后更新日期</td>
                <td>
                    <input type="text" id="UPDATE_DATE_MATERIAL_PARA_INFO" /></td>
            </tr>
        </table>
    </div>

    <div id="Dialog_MATERIAL_PN_PARA_INFO" class="easyui-dialog" data-options="title:'对话框',modal:false,width:500,height:250,closed:true" style="padding: 10px;">
        <table cellspacing="0" cellpadding="0">
            <tr>
                <td style="text-align: right;">目标值</td>
                <td>
                    <input type="text" id="TARGET" class="easyui-validatebox" data-options="required:false,validType:['maxLength[25]']" />(少于25个字) </td>
            </tr>
            <tr>
                <td style="text-align: right;">上限</td>
                <td>
                    <input type="text" id="USL" class="easyui-validatebox" data-options="required:false,validType:['maxLength[25]']" />(少于25个字) </td>
            </tr>
            <tr>
                <td style="text-align: right;">下限</td>
                <td>
                    <input type="text" id="LSL" class="easyui-validatebox" data-options="required:false,validType:['maxLength[25]']" />(少于25个字) </td>
            </tr>
            <tr>
                <td style="text-align: right;">备足</td>
                <td>
                    <input type="text" id="REMARK_PN_PARA_INFO" class="easyui-validatebox" data-options="required:false,validType:['maxLength[15]']" />(少于15个字) </td>
            </tr>
            <tr>
                <td style="text-align: right;">最后更新者</td>
                <td>
                    <input type="text" id="UPDATE_USER_MATERIAL_PN_PARA_INFO" class="easyui-validatebox" data-options="required:false,validType:['maxLength[10]']" />(少于10个字) </td>
            </tr>
            <tr>
                <td style="text-align: right;">最后更新日期</td>
                <td>
                    <input type="text" id="UPDATE_DATE_MATERIAL_PN_PARA_INFO" /></td>
            </tr>
        </table>
    </div>

    <div id="Dialog_MATERIAL_TYPE_GRP" class="easyui-dialog" data-options="title:'对话框',modal:false,width:500,height:400,closed:true" style="padding: 10px;">
        <table cellspacing="0" cellpadding="0">
            <tr>
                <td style="text-align: right;">组编号<b style="color: red; font-size: 15px;">*</b></td>
                <td>
                    <input type="text" id="MATERIAL_TYPE_GRP_NUM" class="easyui-validatebox" data-options="required:false,validType:['maxLength[20]']" />(少于20个字) </td>
            </tr>
            <tr>
                <td style="text-align: right;">描述</td>
                <td>
                    <input type="text" id="MATERIAL_TYPE_GRP_DESC" class="easyui-validatebox" data-options="required:false,validType:['maxLength[20]']" />(少于20个字) </td>
            </tr>
            <tr>
                <td style="text-align: right;">最后更新者</td>
                <td>
                    <input type="text" id="UPDATE_USER_MATERIAL_TYPE_GRP" class="easyui-validatebox" data-options="required:false,validType:['maxLength[10]']" />(少于10个字) </td>
            </tr>
            <tr>
                <td style="text-align: right;">最后更新日期</td>
                <td>
                    <input type="text" id="UPDATE_DATE_MATERIAL_TYPE_GRP" /></td>
            </tr>
        </table>
    </div>

    <div id="Dialog_MATERIAL_TYPE_GRP_LIST" class="easyui-dialog" data-options="title:'对话框',modal:false,width:500,height:200,closed:true" style="padding: 10px;">
        <table cellspacing="0" cellpadding="0">
            <tr>
                <td style="text-align: right;">最后更新者</td>
                <td>
                    <input type="text" id="UPDATE_USER_MATERIAL_TYPE_GRP_LIST" class="easyui-validatebox" data-options="required:false,validType:['maxLength[10]']" />(少于10个字) </td>
            </tr>
            <tr>
                <td style="text-align: right;">最后更新日期</td>
                <td>
                    <input type="text" id="UPDATE_DATE_MATERIAL_TYPE_GRP_LIST" /></td>
            </tr>
        </table>
    </div>
</asp:Content>
