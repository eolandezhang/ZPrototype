<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RECIPE_LIST.aspx.cs" Inherits="Web.BaseInfo.RECIPE_LIST" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/BaseInfo/RECIPE_LIST.js"></script>
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
                <table id="Table_RECIPE_TYPE_LIST"></table>

            </td>
            <td style="vertical-align: top;">
                <table id="Table_RECIPE_LIST"></table>
                <%--<table cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="vertical-align: top;">
                            
                        </td>                        
                    </tr>                    
                </table>--%>
            </td>
        </tr>
    </table>


    <div id="Dialog_RECIPE_LIST" class="easyui-dialog" data-options="title:'对话框',modal:false,width:500,height:400,closed:true" style="padding: 10px;">
        <table cellspacing="0" cellpadding="0">
            <tr>
                <td style="text-align: right;">编号<b style="color: red; font-size: 15px;">*</b></td>
                <td>
                    <input type="text" id="RECIPE_ID" class="easyui-validatebox" data-options="required:false,validType:['maxLength[15]']" />(少于15个字) </td>
            </tr>
            
            <tr>
                <td style="text-align: right;">中文名</td>
                <td>
                    <input type="text" id="RECIPE_NAME" class="easyui-validatebox" data-options="required:false,validType:['maxLength[15]']" />(少于15个字) </td>
            </tr>
            <tr>
                <td style="text-align: right;">英文名</td>
                <td>
                    <input type="text" id="RECIPE_DESC" class="easyui-validatebox" data-options="required:false,validType:['maxLength[50]']" />(少于50个字) </td>
            </tr>
            <tr>
                <td style="text-align: right;">最后更新者</td>
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
                        <option value="1" selected="selected">启用</option>
                        <option value="0">停用</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">SOLID_CONTENT</td>
                <td>
                    <input type="text" id="SOLID_CONTENT" class="easyui-validatebox" data-options="required:false,validType:'number'" /></td>
            </tr>
            <tr>
                <td style="text-align: right;">SCP_VAR</td>
                <td>
                    <input type="text" id="SCP_VAR" class="easyui-validatebox" data-options="required:false,validType:['maxLength[5]']" />(少于5个字) </td>
            </tr>
            <tr>
                <td style="text-align: right;">BASE_RECIPE</td>
                <td>
                    <input type="text" id="BASE_RECIPE" class="easyui-validatebox" data-options="required:false,validType:['maxLength[1]']" />(少于1个字) </td>
            </tr>
            <tr>
                <td style="text-align: right;">STAGE</td>
                <td>
                    <input type="text" id="STAGE" class="easyui-validatebox" data-options="required:false,validType:['maxLength[3]']" />(少于3个字) </td>
            </tr>
            <tr>
                <td style="text-align: right;">IS_HIGH_VISCOSITY</td>
                <td>
                    <input type="text" id="IS_HIGH_VISCOSITY" class="easyui-validatebox" data-options="required:false,validType:['maxLength[1]']" />(少于1个字) </td>
            </tr>
            <tr>
                <td style="text-align: right;">PROC_CONDITION</td>
                <td>
                    <input type="text" id="PROC_CONDITION" class="easyui-validatebox" data-options="required:false,validType:['maxLength[200]']" />(少于200个字) </td>
            </tr>
            <tr>
                <td style="text-align: right;">OTHER_CONDITION</td>
                <td>
                    <input type="text" id="OTHER_CONDITION" class="easyui-validatebox" data-options="required:false,validType:['maxLength[50]']" />(少于50个字) </td>
            </tr>
        </table>
    </div>

    <div id="Dialog_RECIPE_TYPE_LIST" class="easyui-dialog" data-options="title:'对话框',modal:false,width:500,height:250,closed:true" style="padding: 10px;">
        <table cellspacing="0" cellpadding="0">
            <tr>
                <td style="text-align: right;">编号<b style="color: red; font-size: 15px;">*</b></td>
                <td>
                    <input type="text" id="RECIPE_TYPE_ID" class="easyui-validatebox" data-options="required:false,validType:['maxLength[15]']" />(少于15个字) </td>
            </tr>
            <tr>
                <td style="text-align: right;">英文名</td>
                <td>
                    <input type="text" id="RECIPE_TYPE_NAME" class="easyui-validatebox" data-options="required:false,validType:['maxLength[15]']" />(少于15个字) </td>
            </tr>
            <tr>
                <td style="text-align: right;">中文名</td>
                <td>
                    <input type="text" id="RECIPE_TYPE_DESC" class="easyui-validatebox" data-options="required:false,validType:['maxLength[50]']" />(少于50个字) </td>
            </tr>
            <tr>
                <td style="text-align: right;">最后更新者</td>
                <td>
                    <input type="text" id="UPDATE_USER_RECIPE_TYPE_LIST" /></td>
            </tr>
            <tr>
                <td style="text-align: right;">最后更新日期</td>
                <td>
                    <input type="text" id="UPDATE_DATE_RECIPE_TYPE_LIST" /></td>
            </tr>
            <tr>
                <td style="text-align: right;">状态</td>
                <td>
                    <select id="VALID_FLAG_RECIPE_TYPE_LIST">
                        <option value="1" selected="selected">启用</option>
                        <option value="0">停用</option>
                    </select>
                </td>
            </tr>

        </table>
    </div>


</asp:Content>
