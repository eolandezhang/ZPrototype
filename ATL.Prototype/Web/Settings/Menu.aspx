<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="Web.Settings.Menu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Settings/Menu.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 200px; vertical-align: top">
                <ul id="menu" class="ztree">
                </ul>
            </td>
            <td style="vertical-align: top">
                <div style="background: #fafafa; padding: 2px; border: 1px solid #ddd; width: 500px; margin-bottom: 2px; position: relative;">
                    <a id="add" class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true">新增</a>
                    <a id="save" class="easyui-linkbutton" data-options="iconCls:'icon-save',plain:true">保存</a>
                    <a id="del" class="easyui-linkbutton" data-options="iconCls:'icon-remove',plain:true">删除</a>
                </div>
                <input type="text" id="ID" style="border: none;" />
                <table id="Table_menu">
                    <tr>
                        <td style="text-align: right;">父目录<b style="color: red; font-size: 15px;">*</b>
                        </td>
                        <td>
                            <input id="PID" type="text" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">名称<b style="color: red; font-size: 15px;">*</b>
                        </td>
                        <td>
                            <input id="NAME" type="text" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">描述
                        </td>
                        <td>
                            <input id="TITLE" type="text" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">URL
                        </td>
                        <td>
                            <textarea id="URL" rows="2" style="width: 400px;"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">TARGET
                        </td>
                        <td>
                            <select id="TARGET">
                                <option value="_self" selected="selected">_self</option>
                                <option value="_blank">_blank</option>

                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">Open
                        </td>
                        <td>
                            <select id="OPEN">
                                <option value="true" selected="selected">打开</option>
                                <option value="false">关闭</option>
                            </select>

                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">序号
                        </td>
                        <td>
                            <input id="SORT" type="text" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div id="dialog_PID" class="dialog">
        <ul id="tree_Doc_MNG_PID" class="ztree">
        </ul>
    </div>
</asp:Content>
