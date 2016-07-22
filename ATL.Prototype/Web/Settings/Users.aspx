<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="Web.Settings.Users" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="/Settings/Users.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    用户名：<input id="USERNAME_SEARCH" type="text" style="width: 200px;" />
    <a id="btn_search" class="easyui-linkbutton">查询</a>
    <br /><br />
    
    <table id="Table_Users"></table>

    <div id="Dialog_Users" class="dialog" style="padding: 10px;">
        <table style="text-align: left;">
            <tr>
                <td style="text-align: right;">用户名<b style="color: red; font-size: 15px;">*</b>
                </td>
                <td>
                    <input type="text" id="USERNAME" />
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">工号<b style="color: red; font-size: 15px;">*</b>
                </td>
                <td>
                    <input type="text" id="DESCRIPTION" />
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">姓名<b style="color: red; font-size: 15px;">*</b>
                </td>
                <td>
                    <input type="text" id="CNNAME" />
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">部门<b style="color: red; font-size: 15px;">*</b>
                </td>
                <td>
                    <input type="text" id="DEPARTMENT" />
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">职位<b style="color: red; font-size: 15px;">*</b>
                </td>
                <td>
                    <input type="text" id="TITLE" />
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">邮箱<b style="color: red; font-size: 15px;">*</b>
                </td>
                <td>
                    <input type="text" id="MAIL" style="width: 200px;" />
                </td>
            </tr>

        </table>
    </div>
</asp:Content>
