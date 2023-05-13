<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="WebProject.Pages.Edit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <a href="/Pages/AdminPanel">Back</a>

    <h2> User-Edit </h2>

    <br />
    <div class="InputLoad">
        <label for="userName"> Username : </label> <br />
        <input type="text" id="userName" name="userName" value="<%=UserName %>" /> <br />

        <label for="password"> password : </label> <br />
        <input type="password" id="password" name="password" value="<%=Password %>" /> <br />

        <label for="firstName"> Firstname : </label> <br />
        <input type="text" id="firstName" name="firstName" value="<%=FirstName %>" /> <br />

        <label for="lastName"> Lastname : </label> <br />
        <input type="text" id="lastName" name="lastName" value="<%=LastName %>" /> <br />

        <label for="birthday"> Birthday : </label> <br />
        <input type="date" id="birthday" name="birthday" value="<%=BirthDay %>" /> <br />

        <label for="city"> City : </label> <br />
        <input type="text" id="city" name="city" value="<%=City %>" /> <br />
    </div>

    <input type="submit" value="Update" />

</asp:Content>
