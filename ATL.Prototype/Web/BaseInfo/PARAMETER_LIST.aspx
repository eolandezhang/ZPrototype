<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PARAMETER_LIST.aspx.cs" Inherits="Web.BaseInfo.PARAMETER_LIST" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/BaseInfo/PARAMETER_LIST.js"></script>
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
                <table id="Table_PARAM_TYPE_LIST"></table>
            </td>
            <td style="vertical-align: top;">
                <table id="Table_PARAMETER_LIST"></table>
            </td>
        </tr>
    </table>

    <div id="Dialog_PARAMETER_LIST" class="easyui-dialog" data-options="title:'对话框',modal:false,width:700,height:'auto',closed:true" style="padding: 10px;">
        <table cellspacing="0" cellspadding="0">
            <tr>
                <td style="text-align: right;">编号<b style="color: red; font-size: 15px;">*</b></td>
                <td colspan="3">
                    <input type="text" id="PARAMETER_ID" class="easyui-validatebox" data-options="required:true,validType:['maxLength[25]']" />(少于25个字) </td>
            </tr>
            <tr>
                <td style="text-align: right;">中文名</td>
                <td>
                    <input type="text" id="PARAM_DESC" class="easyui-validatebox" data-options="required:true,validType:['maxLength[30]']" />(少于30个字) </td>
                <td style="text-align: right;">英文名</td>
                <td>
                    <input type="text" id="PARAM_NAME" class="easyui-validatebox" data-options="required:false,validType:['maxLength[20]']" />(少于20个字) </td>
            </tr>
            <tr>
                <td style="text-align: right;">输入/输出</td>
                <td>
                    <select id="PARAM_IO">
                        <option value="1" selected="selected">输入</option>
                        <option value="2">输出</option>
                    </select></td>

                <td style="text-align: right;">SOURCE</td>
                <td>
                    <select id="SOURCE">
                        <option value="0" selected="selected">否</option>
                        <option value="1">是</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">规格牌</td>
                <td>
                    <select id="IS_SPEC_PARAM">
                        <option value="0" selected="selected">否</option>
                        <option value="1">是</option>
                    </select>
                </td>

                <td style="text-align: right;">首件</td>
                <td>
                    <select id="IS_FIRST_CHECK_PARAM">
                        <option value="0" selected="selected">否</option>
                        <option value="1">是</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">量产</td>
                <td>
                    <select id="IS_PROC_MON_PARAM">
                        <option value="0" selected="selected">否</option>
                        <option value="1">是</option>
                    </select>
                </td>

                <td style="text-align: right;">出货</td>
                <td>
                    <select id="IS_OUTPUT_PARAM">
                        <option value="0" selected="selected">否</option>
                        <option value="1">是</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">版本</td>
                <td>
                    <select id="IS_VERSION_CTRL">
                        <option value="0" selected="selected">否</option>
                        <option value="1">是</option>
                    </select>
                </td>

                <td style="text-align: right;">MEASURE_METHOD</td>
                <td>
                    <select id="MEASURE_METHOD">
                        <option value="0" selected="selected">否</option>
                        <option value="1">是</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">分组参数</td>
                <td>
                    <select id="IS_GROUP_PARAM">
                        <option value="0" selected="selected">否</option>
                        <option value="1">是</option>
                    </select>
                </td>

                <td style="text-align: right;">数据类型</td>
                <td>
                    <select id="PARAM_DATATYPE">
                        <option value="STRING" selected="selected">字符串</option>
                        <option value="NUMBER">数字</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">单位</td>
                <td>
                    <input type="text" id="PARAM_UNIT" class="easyui-validatebox" data-options="required:false,validType:['maxLength[8]']" />(少于8个字) </td>

                <td style="text-align: right;">目标值</td>
                <td>
                    <input type="text" id="TARGET" class="easyui-validatebox" data-options="required:false,validType:['maxLength[25]']" />(少于25个字) </td>
            </tr>
            <tr>
                <td style="text-align: right;">上限</td>
                <td>
                    <input type="text" id="USL" class="easyui-validatebox" data-options="required:false,validType:['maxLength[25]']" />(少于25个字) </td>

                <td style="text-align: right;">下限</td>
                <td>
                    <input type="text" id="LSL" class="easyui-validatebox" data-options="required:false,validType:['maxLength[25]']" />(少于25个字) </td>
            </tr>            
            <tr>
                 <td style="text-align: right;">抽样频率</td>
                <td>
                    <input type="text" id="SAMPLING_FREQUENCY" class="easyui-validatebox" data-options="required:false,validType:['maxLength[50]']" />(少于50个字) </td>
                <td style="text-align: right;">控制方法</td>
                <td>
                    <input type="text" id="CONTROL_METHOD" class="easyui-validatebox" data-options="required:false,validType:['maxLength[50]']" />(少于50个字) </td> 
            </tr>
            <tr>
                <td style="text-align: right;">停用/启用</td>
                <td>
                    <select id="VALID_FLAG">
                        <option value="0" selected="selected">否</option>
                        <option value="1">是</option>
                    </select>
                </td>
                 <td></td>
                <td></td>
                
            </tr>
            <tr>
                <td style="text-align: right;">最后更新者</td>
                <td>
                    <input type="text" id="UPDATE_USER" />(少于10个字) </td>

                <td style="text-align: right;">最后更新日期</td>
                <td>
                    <input type="text" id="UPDATE_DATE" /></td>

            </tr>
        </table>
    </div>

    <div id="Dialog_PARAM_TYPE_LIST" class="easyui-dialog" data-options="title:'对话框',modal:false,width:400,height:'auto',closed:true" style="padding: 10px;">
        <table cellspacing="0" cellspadding="0">
            <tr>
                <td style="text-align: right;">编号<b style="color: red; font-size: 15px;">*</b></td>
                <td>
                    <input type="text" id="PARAM_TYPE_ID" />(少于15个字) </td>
            </tr>
            <tr>
                <td style="text-align: right;">名称</td>
                <td>
                    <input type="text" id="PARAM_TYPE_DESC" />(少于25个字) </td>
            </tr>
            <tr>
                <td style="text-align: right;">启用/停用</td>
                <td>
                    <select id="VALID_FLAG_PARAM_TYPE_LIST">
                        <option value="1" selected="selected">启用</option>
                        <option value="0">停用</option>
                    </select></td>
            </tr>
            <tr>
                <td style="text-align: right;">最后更新者</td>
                <td>
                    <input type="text" id="UPDATE_USER_PARAM_TYPE_LIST" /></td>
            </tr>
            <tr>
                <td style="text-align: right;">最后更新日期</td>
                <td>
                    <input type="text" id="UPDATE_DATE_PARAM_TYPE_LIST" /></td>
            </tr>
        </table>
    </div>

</asp:Content>
