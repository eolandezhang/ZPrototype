<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PkgRight.aspx.cs" Inherits="Web.Documents.PkgRight" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .mytable {
            border-top: 1px solid silver;
            border-left: 1px solid silver;
            border-spacing: 0;
        }

            .mytable td {
                border-right: 1px solid silver;
                border-bottom: 1px solid silver;
                padding: 4px;
                text-align: center;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>用户分类</h3>
    <table class="mytable">
        <tr>
            <td style="width: 100px;">普通用户
            </td>
            <td style="width: 300px;">无权限</td>
        </tr>
        <tr>
            <td style="width: 100px;">查看者
            </td>
            <td style="width: 200px;">查看</td>
        </tr>
        <tr>
            <td style="width: 100px;">制作人
            </td>
            <td style="width: 200px;">查看，新增，修改，删除，复制，导出</td>
        </tr>        
        <tr>
            <td>文件管理员
            </td>
            <td>导出</td>
        </tr>
        <tr>
            <td>管理员
            </td>
            <td></td>
        </tr>
    </table>

    <h3>制作人权限</h3>
    <p>制作人只有操作自己创建的文件的权限。</p>
    <table class="mytable">
        <tr>
            <td style="width: 80px;"></td>
            <td style="width: 80px;">草稿</td>
            <td style="width: 80px;">已送审</td>
            <td style="width: 80px;">已退回</td>
            <td style="width: 80px;">已签审</td>
            <td style="width: 80px;">已发布</td>
        </tr>
        <tr>
            <td>修改</td>
            <td>√</td>
            <td></td>
            <td>√</td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td>删除</td>
            <td>√</td>
            <td></td>
            <td>√</td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td>导出</td>
            <td>√</td>
            <td></td>
            <td>√</td>
            <td></td>
            <td></td>
        </tr>
    </table>


    <h3>文件管理员</h3>
    
    <table class="mytable">
        <tr>
            <td style="width: 80px;"></td>
            <td style="width: 80px;">草稿</td>
            <td style="width: 80px;">已送审</td>
            <td style="width: 80px;">已退回</td>
            <td style="width: 80px;">已签审</td>
            <td style="width: 80px;">已发布</td>
        </tr>
        <tr>
            <td>导出</td>
            <td>√</td>
            <td>√</td>
            <td>√</td>
            <td>√</td>
            <td>√</td>
        </tr>
    </table>

    <h3>文件查看</h3>
    <table class="mytable">
        <tr>
            <td style="width: 80px;"></td>
            <td style="width: 80px;">全部的文件<br />
            </td>
            <td style="width: 80px;">自己的文件</td>
            <td style="width: 80px;">启用的文件</td>
        </tr>
        <tr>
            <td>普通用户</td>
            <td></td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td>查看人</td>
            <td></td>
            <td></td>
            <td>√</td>
        </tr>
        <tr>
            <td>制作人</td>
            <td></td>
            <td>√</td>
            <td>√</td>
        </tr>
        <tr>
            <td>管理员</td>
            <td>√</td>
            <td>√</td>
            <td>√</td>
        </tr>
    </table>

</asp:Content>
