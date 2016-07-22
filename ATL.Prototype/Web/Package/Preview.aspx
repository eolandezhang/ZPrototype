<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Preview.aspx.cs" Inherits="Web.Package.Preview" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Package/Preview.js"></script>

    <style type="text/css">
        .tbl {
            border-style: solid none none solid;
            border-color: gray;
            border-width: 1px;
        }

            .tbl td {
                border-style: none solid solid none;
                border-color: gray;
                padding: 0px;
                border-width: 1px;
                vertical-align: top;
            }


        .tbl_border_right td {
            border-style: none solid none none;
            border-color: gray;
            padding: 0px;
            border-width: 1px;
            vertical-align: top;
        }

        .tbl_border_bottom td {
            border-style: none none solid none;
            border-color: gray;
            padding: 0px;
            border-width: 1px;
            vertical-align: top;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="width: 944px; margin-bottom: 2px; padding: 5px 10px; border: 1px solid #95B8E7; background-color: #fff;">
        <a class="easyui-linkbutton" data-options="iconCls:'icon-backhome'" href="/Package/PACKAGE_BASE_INFO.aspx?mid=24">返回列表</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color: blue;">文件编号:</span><span data-bind="text: PACKAGE_NO"></span>&nbsp;&nbsp;<span style="color: blue;">版本:</span><span data-bind="    text: VERSION_NO"></span>
        &nbsp;&nbsp;<span style="color: blue;">厂别:</span><span data-bind="text: FACTORY_ID"></span>&nbsp;&nbsp;<span style="color: blue;">产品类型:</span><span data-bind="    text: PRODUCT_TYPE_ID"></span>&nbsp;&nbsp;<span style="color: blue;">工艺类型:</span><span data-bind="    text: PRODUCT_PROC_TYPE_ID"></span>
    </div>
    <div id="mytoolbar" style="border-style: none none solid none;">
        <a id="modify" class="easyui-linkbutton" data-options="iconCls:'icon-edit',plain:false">修改</a>
        <a id="nextstep" class="easyui-linkbutton" data-options="iconCls:'icon-nextstep',iconAlign:'right',plain:false">转交下一步</a>
    </div>
    <div id="myTab" class="easyui-tabs" style="width: 966px; height: 500px;" data-options="border:false,tools:'#mytoolbar'">
        <div title="签审记录" style="padding: 2px 0; overflow: hidden;">
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <table id="Table_PACKAGE_WF_STEP"></table>
                    </td>
                    <td style="padding-left: 2px;">
                        <div class="easyui-panel" title="签审历史" style="width: 664px; height: 250px; padding: 5px;">
                            <div id="Audit_History"></div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="padding-top: 2px;">
                        <table id="Table_PACKAGE_WF_STEP_AUDITOR"></table>
                        <div id="Dialog_Change" class="easyui-dialog" data-options="title:'转交下一步',modal:false,width:457,height:400,closed:true" style="padding: 8px;">
                            <table id="Table_PMES_USER_ID_Change"></table>
                        </div>
                    </td>
                </tr>
            </table>

        </div>
        <div title="文件预览" style="padding: 2px 0; overflow: hidden;" data-options="selected:true">
            <div id="tt" class="easyui-tabs" style="width: 964px; height: 468px;" data-options="border:false">
                <div title="Cover" style="padding: 2px;">
                    <div runat="server" id="cover"></div>
                </div>
                <div title="正文" style="padding: 2px;" data-options="selected:true">
                    <div runat="server" id="parameter"></div>
                </div>
                <div title="物料" style="padding: 2px;">
                    <div runat="server" id="material"></div>
                </div>
                <div title="设备" style="padding: 2px;">
                    <div runat="server" id="equipment"></div>
                </div>
                <div title="附图" style="padding: 2px;">
                    <div runat="server" id="illustration"></div>
                </div>
                <div title="BOM" style="padding: 2px;">
                    <div runat="server" id="bom"></div>
                </div>
            </div>
            <div id="Dialog_NextStep" class="easyui-dialog" data-options="title:'转交下一步',modal:false,width:457,height:450,closed:true" style="padding: 8px;">
                <table>
                    <tr>
                        <td style="width: 60px;">当前步骤：</td>
                        <td><span id="CurrentStep"></span></td>
                    </tr>
                    <tr>
                        <td style="color: blue;">下一步骤：</td>
                        <td><span id="NextStep" style="color: blue;"></span></td>
                    </tr>
                    <tr>
                        <td colspan="2">签审意见(少于30个字)：<br />
                            <textarea id="AUDITOR_COMMENT" rows="2" style="width: 408px;" class="easyui-validatebox" data-options="required:false,validType:['maxLength[30]']"></textarea><br />
                        </td>
                    </tr>
                    <tr class="nextAuditor">
                        <td colspan="2">请选择下一步签审人：<br />
                            <input id="SelectedAuditor" style="width: 400px; padding: 5px; background-color: #ffff99; border: 1px solid #000;" readonly="readonly" />
                        </td>
                    </tr>
                    <tr class="nextAuditor">
                        <td colspan="2">
                            <table id="Table_PMES_USER_ID"></table>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>

</asp:Content>
