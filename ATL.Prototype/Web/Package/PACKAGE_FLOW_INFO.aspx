<%@ Page Title="工序信息" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PACKAGE_FLOW_INFO.aspx.cs" Inherits="Web.Package.PACKAGE_FLOW_INFO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Package/PACKAGE_FLOW_INFO.js"></script>
    <script src="/Package/Tabs.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="width: 944px; margin-bottom: 2px; padding: 5px 10px; border: 1px solid #95B8E7; background-color: #fff;">
        <a class="easyui-linkbutton" data-options="iconCls:'icon-backhome'" href="/Package/PACKAGE_BASE_INFO.aspx?mid=24">返回列表</a>&nbsp;&nbsp;&nbsp;&nbsp;
        <span style="color: blue;">文件编号:</span><span data-bind="text: PACKAGE_NO"></span>&nbsp;&nbsp;<span style="color: blue;">版本:</span><span data-bind="    text: VERSION_NO"></span>
        &nbsp;&nbsp;<span style="color: blue;">厂别:</span><span data-bind="text: FACTORY_ID"></span>&nbsp;&nbsp;<span style="color: blue;">产品类型:</span><span data-bind="    text: PRODUCT_TYPE_ID"></span>&nbsp;&nbsp;<span style="color: blue;">工艺类型:</span><span data-bind="    text: PRODUCT_PROC_TYPE_ID"></span>
    </div>
    <div id="mytab" style="border-style: none none solid none;"><a class="easyui-linkbutton" id="wf" data-options="iconCls:'icon-ok'">审批流程</a></div>
    <div id="tt" class="easyui-tabs" style="width: 966px;" data-options="border:false,tools:'#mytab'">
        <div title="基本信息"></div>
        <div title="分组信息"></div>
        <div title="设计信息"></div>
        <div title="工序信息" style="padding: 2px 0px;" data-options="selected:true">
            <table cellspacing="0" cellpadding="0">
                <tr>
                    <td style="vertical-align: top; padding-right: 2px;">
                        <table id="Table_PACKAGE_GROUPS"></table>
                    </td>
                    <td style="vertical-align: top;">
                        <table id="Table_PACKAGE_FLOW_INFO"></table>
                    </td>
                </tr>
            </table>

            <div id="Dialog_PACKAGE_FLOW_INFO" class="easyui-dialog" data-options="title:'对话框',modal:false,width:500,height:'auto',closed:true" style="padding: 10px;">
                <div id="grp" style="width: 450px; margin-bottom: 2px; padding: 5px; border: 1px solid #CCCCCC; background-color: #FFFFCC;">
                    分组(多选)<input id="GROUP_NO_BATCHEDIT" />
                </div>
                <table cellspacing="0" cellspadding="0">
                    <tr>
                        <td style="text-align: right;">工序<b style="color: red; font-size: 15px;">*</b></td>
                        <td>
                            <input type="text" id="PROCESS_ID" /></td>
                        <td style="text-align: right;">序号</td>
                        <td>
                            <input type="text" id="PROC_SEQUENCE_NO" class="easyui-validatebox" data-options="required:false,validType:'number'" /></td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">前一工序</td>
                        <td>
                            <input type="text" id="PREVIOUS_PROCESS_ID" /></td>
                        <td style="text-align: right;">后一工序</td>
                        <td>
                            <input type="text" id="NEXT_PROCESS_ID" /></td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">工序说明</td>
                        <td colspan="3">
                            <textarea id="PKG_PROC_DESC" style="width: 380px;" class="easyui-validatebox" data-options="required:false,validType:['maxLength[100]']"></textarea></td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">分组归类</td>
                        <td colspan="3">
                            <input id="SUB_GROUP_NO" /></td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">最后修改者</td>
                        <td>
                            <input type="text" id="UPDATE_USER" /></td>
                        <td style="text-align: right;">最后修改时间</td>
                        <td>
                            <input type="text" id="UPDATE_DATE" /></td>
                    </tr>
                </table>
            </div>

            <div id="Dialog_PACKAGE_FLOW_INFO_BatchAdd" class="easyui-dialog" data-options="title:'批量新增',modal:false,width:300,height:'auto',closed:true" style="padding: 10px;">
                <table cellspacing="0" cellspadding="0">
                    <tr>
                        <td>分组(多选)
                        </td>
                        <td>
                            <input id="GROUP_NO_BATCHADD" />
                        </td>
                    </tr>
                    <tr>
                        <td>工序(多选)</td>
                        <td>
                            <input id="PROCESS_ID_BATCHADD" />
                        </td>
                    </tr>
                </table>
            </div>

            <div id="Dialog_PACKAGE_FLOW_INFO_BatchDel" class="easyui-dialog" data-options="title:'批量删除',modal:false,width:300,height:'400',closed:true" style="padding: 2px;">
                <div style="padding: 4px;">
                    分组（多选）<input id="GROUP_NO_BatchDel" />
                </div>

                <table id="Table_PACKAGE_FLOW_INFO_BatchDel"></table>
            </div>


        </div>
        <%--<div title="工序信息(v2)">
            <table cellspacing="0" cellspadding="0">
                <tr>
                    <td style="vertical-align: top;"></td>
                    <td></td>
                </tr>
            </table>
        </div>--%>
        <div title="工序明细">
        </div>
    </div>



</asp:Content>
