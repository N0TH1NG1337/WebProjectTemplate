<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LogIn.aspx.cs" Inherits="WebProject.Pages.LogIn" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Register Page -->
    <!-- Note : after every elements in the register page we need to go down in line so it wont stack on each other  -->
    <!-- Note! : we will use LogInMessege to Handle everything; Better to use PageLoad insted  -->

    <!-- Title -->
    <h2>Log In</h2> <br />

     <!-- Error Messege Handler -->
    <div runat="server">
       <%Response.Write(LogInMessage()); %>
    </div>

     <!-- Div -->
    <div>
        <!-- Username -->
        <label for="userName"> Username : </label> <br />
        <input type="text" id="userName" name="userName" /> <br />

        <!-- Password -->
        <label for="password"> Password : </label> <br />
        <input type="password" id="password" name="password"/> <br />

    </div>

    <!-- Break Line -->
    <br />

    <!-- Submit -->
    <input type="submit" value="log in"/>

</asp:Content>
