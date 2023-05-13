<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Delete.aspx.cs" Inherits="WebProject.Pages.Delete" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Delete user from SQL Table -->

    <a href="/Pages/Admin">Back</a>
    
    <% Response.Write(String.Format("<h3> Are you sure you want to delete user id {0} ? </h3>", UserId));%>

    <br />
    <h3> User Data </h3>
    <% Response.Write(String.Format("<h4> First Name : {0} </h4>", Firstname));
       Response.Write(String.Format("<h4> Last Name : {0} </h4>", Lastname));
    %>
    <br />

    <input type="submit" value="Remove"/>
    
</asp:Content>
