<%@ Page Title="组别信息" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PACKAGE_GROUPS.aspx.cs" Inherits="Web.Package.PACKAGE_GROUPS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Package/PACKAGE_GROUPS.js"></script>
    <script src="/Package/Tabs.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="width: 944px; margin-bottom: 2px; padding: 5px 10px; border: 1px solid #95B8E7; background-color: #fff;">
        <a class="easyui-linkbutton" data-options="iconCls:'icon-backhome'" href="/Package/PACKAGE_BASE_INFO.aspx?mid=24">返回列表</a>&nbsp;&nbsp;&nbsp;&nbsp;
        <span style="color: blue;">文件编号:</span><span data-bind="text: PACKAGE_NO"></span>&nbsp;&nbsp;<span style="color: blue;">版本:</span><span data-bind="    text: VERSION_NO"></span>
        &nbsp;&nbsp;<span style="color: blue;">厂别:</span><span data-bind="text: FACTORY_ID"></span>&nbsp;&nbsp;<span style="color: blue;">产品类型:</span><span data-bind="    text: PRODUCT_TYPE_ID"></span>&nbsp;&nbsp;<span style="color: blue;">工艺类型:</span><span data-bind="    text: PRODUCT_PROC_TYPE_ID"></span>
    </div>
    <div id="mytab" style="border-style:none none solid none;"><a class="easyui-linkbutton" id="wf" data-options="iconCls:'icon-ok'">审批流程</a></div>
    <div id="tt" class="easyui-tabs" style="width: 966px;" data-options="border:false,tools:'#mytab'">
        <div title="基本信息"></div>
        <div title="分组信息" style="padding: 2px 0px;" data-options="selected:true">
            <table id="Table_PACKAGE_GROUPS"></table>
            <div id="Dialog_PACKAGE_GROUPS" class="easyui-dialog" data-options="title:'对话框',modal:false,width:400,height:'auto',closed:true" style="padding: 10px;">
                <table cellspacing="0" cellspadding="0">                    
                    <tr>
                        <td style="text-align: right;">组别<b style="color: red; font-size: 15px;">*</b></td>
                        <td>
                            <input type="text" id="GROUP_NO"  class="easyui-validatebox" data-options="required:true,validType:['maxLength[2]','alpha']" />(少于2个字) </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">数量</td>
                        <td>
                            <input type="text" id="GROUP_QTY"  class="easyui-validatebox" data-options="required:true,validType:'number'" /></td>
                    </tr>
                </table>
            </div>
        </div>
        <div title="设计信息"></div>
        <div title="工序信息">
        </div>
        <div title="工序明细">
        </div>
    </div>
</asp:Content>
