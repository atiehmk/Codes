<%@ Page Language="C#" MasterPageFile="~/GlacierPoint.Master" AutoEventWireup="true" CodeBehind="EmployeeDirectory.aspx.cs" Inherits="WebApplication6.EmployeeDirectory"
title="GlacierPoint Employee Directory" %>
<asp:Content ID="Content1"
ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<h1>Employee Directory</h1>
<asp:Repeater id="employeesRepeater" runat="server">
<ItemTemplate>
Employee ID:
<strong><%#Eval("EmployeeId")%></strong><br />
Name: <strong><%#Eval("Name")%></strong><br />
Username: <strong><%#Eval("Username")%></strong>
</ItemTemplate>
<SeparatorTemplate>
<hr />
</SeparatorTemplate>
</asp:Repeater>
</asp:Content>

