<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Web.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>
        .linkbtn {
            border: 4px solid white;
            background-color: #007fff;
            color: white;
            font-size: 30px;
            padding: 20px;
            text-align: center;
            font-weight: bold;
            font-family: 黑体;
            width: 200px;
            display: block;
            text-decoration: none;
            float: left;
        }

            .linkbtn:hover {
                background-color: #007fff;
                text-decoration: none;
                border: 4px solid #0062c4;
            }

        .linkbtn1 {
            border: 4px solid white;
            background-color:#FFD700;
            color: black;
            font-size: 30px;
            padding: 20px;
            text-align: center;
            font-weight: bold;
            font-family: 黑体;
            width: 200px;
            display: block;
            text-decoration: none;
            float: left;
        }

            .linkbtn1:hover {
                background-color: #FFD700;
                text-decoration: none;
                border: 4px solid #0062c4;
            }
            .linkbtn2 {
            border: 4px solid white;
            background-color: #DC143C;
            color: white;
            font-size: 30px;
            padding: 20px;
            text-align: center;
            font-weight: bold;
            font-family: 黑体;
            width: 200px;
            display: block;
            text-decoration: none;
            float: left;
        }

            .linkbtn2:hover {
                background-color: #DC143C;
                text-decoration: none;
                border: 4px solid #0062c4;
            }
    </style>
    <script src="/Package/Tasks.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ul style="color: slategrey;">
        <li>
            <%--<img src="/Images/chrome.png" />--%><a href="/attached/谷歌浏览器 稳定版_29.0.1547.66.exe" style="height: 16px; cursor: pointer; font-family: Arial; font-size: 12px; margin-top: 2px">请点我下载后,安装.</a>

            (或<a href="/attached/GoogleChromePortable.zip" style="height: 16px; cursor: pointer; font-family: Arial; font-size: 12px; margin-top: 2px">下载免安装版</a>,下载后解压，运行ChromePortable.exe)
        </li>
        <li>当出现“是否作为默认浏览器”的提示，请您选择“否”，不将其设定为默认浏览器。
        </li>
        <li>在地址栏输入网址，访问网站.</li>
    </ul>
    <hr />

    <a class="linkbtn" href="/Package/PACKAGE_BASE_INFO.aspx?mid=24">Package文件</a>
    <a class="linkbtn1" href="/Documents/Instruction/index.html" target="_blank">教程</a>
    <a class="linkbtn2" href="/Package/Tasks.aspx">签审任务</a>
    
    <hr style="clear:both;" />
    <table id="Table_Tasks"></table>
</asp:Content>
