<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ILLUSTRATION_LIST.aspx.cs" Inherits="Web.BaseInfo.ILLUSTRATION_LIST" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/BaseInfo/ILLUSTRATION_LIST.js"></script>
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
                <table id="Table_ILLUSTRATION_LIST"></table>
                <div style="height: 2px; overflow: hidden;"></div>
                <table id="Table_ILLUSTRATION_PARAM_INFO"></table>
                <div style="height: 2px; overflow: hidden;"></div>
                <table id="Table_PARAMETER_LIST"></table>                
                <div class="easyui-panel" style="width: 600px; height: 248px;" title="显示图片">

                    <div id="showimg"></div>

                </div>

            </td>
        </tr>
    </table>

    <div id="Dialog_ILLUSTRATION_LIST" class="easyui-dialog" data-options="title:'对话框',modal:false,width:500,height:'auto',closed:true" style="padding: 10px;">
        <table cellspacing="0" cellspadding="0">
            <tr style="display: none;">
                <td style="text-align: right;">工序</td>
                <td>
                    <input type="text" id="PROCESS_ID" /></td>
                <td style="text-align: right;">厂别<b style="color: red; font-size: 15px;">*</b></td>
                <td>
                    <input type="text" id="FACTORY_ID" /></td>
            </tr>
            <tr style="display: none;">
                <td style="text-align: right;">产品类型<b style="color: red; font-size: 15px;">*</b></td>
                <td>
                    <input type="text" id="PRODUCT_TYPE_ID" /></td>
                <td style="text-align: right;">工艺类型<b style="color: red; font-size: 15px;">*</b></td>
                <td>
                    <input type="text" id="PRODUCT_PROC_TYPE_ID" /></td>
            </tr>
            <tr>
                <td style="text-align: right;">图片编号<b style="color: red; font-size: 15px;">*</b></td>
                <td colspan="3">
                    <input type="text" id="ILLUSTRATION_ID" class="easyui-validatebox" data-options="required:true,validType:['maxLength[25]']" />(少于25个字) </td>
            </tr>
            <tr>
                <td style="text-align: right;">图片描述<b style="color: red; font-size: 15px;">*</b></td>
                <td colspan="3">
                    <input type="text" id="ILLUSTRATION_DESC" class="easyui-validatebox" data-options="required:true,validType:['maxLength[25]']" style="width: 300px;" />(少于25个字) </td>
            </tr>
            <tr>
                <td style="text-align: right;">行数<b style="color: red; font-size: 15px;">*</b></td>
                <td colspan="3">
                    <input type="text" id="IMG_LENGTH" class="easyui-validatebox" data-options="required:true,validType:'number'" />
                </td>
            </tr>
            <tr>
                <td style="text-align:right;">上传图片
                </td>
                <td>
                    <a id="ILLUSTRATION_DATA" class="easyui-linkbutton">上传</a>
                </td>
            </tr>            
            <tr>
                <td style="text-align: right;">启用</td>
                <td colspan="3">
                    <select id="VALID_FLAG">
                        <option value="1">启用</option>
                        <option value="0">停用</option>
                    </select></td>
            </tr>
            <tr>
                <td style="text-align: right;">最后更新者</td>
                <td>
                    <input type="text" id="UPDATE_USER" /></td>
                <td style="text-align: right;">最后更新时间</td>
                <td>
                    <input type="text" id="UPDATE_DATE" /></td>
            </tr>
        </table>
    </div>

    <div id="Dialog_ILLUSTRATION_LIST_UploadImg" class="easyui-dialog" data-options="title:'对话框',modal:true,width:400,height:'auto',closed:true" style="padding: 10px;">
        <input type="text" id="url" value="" />
        <a class="easyui-linkbutton" id="image">选择图片</a>（本地上传）
    </div>

    <div id="Dialog_ILLUSTRATION_PARAM_INFO" class="easyui-dialog" data-options="title:'对话框',modal:false,width:400,height:'auto',closed:true" style="padding: 10px;">
        <table cellspacing="0" cellspadding="0">            
            <tr>
                <td style="text-align: right;">参数<b style="color: red; font-size: 15px;">*</b></td>
                <td>
                    <input type="text" id="PARAMETER_ID" />(少于15个字) </td>
            </tr>
            <tr>
                <td style="text-align: right;">序号</td>
                <td>
                    <input type="text" id="PARAM_ORDER_NO"  class="easyui-validatebox" data-options="required:false,validType:'number'"  /></td>
            </tr>
            <tr>
                <td style="text-align: right;">参数值</td>
                <td>
                    <input type="text" id="TARGET"  class="easyui-validatebox" data-options="required:false,validType:'number'" /></td>
            </tr>
            <tr>
                <td style="text-align: right;">上限</td>
                <td>
                    <input type="text" id="USL"  class="easyui-validatebox" data-options="required:false,validType:'number'" /></td>
            </tr>
            <tr>
                <td style="text-align: right;">下限</td>
                <td>
                    <input type="text" id="LSL"  class="easyui-validatebox" data-options="required:false,validType:'number'" /></td>
            </tr>
            <tr>
                <td style="text-align: right;">最后更新者</td>
                <td>
                    <input type="text" id="UPDATE_USER_ILLUSTRATION_PARAM_INFO" /></td>
            </tr>
            <tr>
                <td style="text-align: right;">最后更新日期</td>
                <td>
                    <input type="text" id="UPDATE_DATE_ILLUSTRATION_PARAM_INFO" /></td>
            </tr>
        </table>
    </div>
</asp:Content>
