<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="WebProject.Pages.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Register Page -->
    <!-- Note : after every elements in the register page we need to go down in line so it wont stack on each other  -->

    <!-- Title -->
    <h2> Sign up </h2> <br />
     
    <!-- Div to apply changes -->
    <!-- Struct for input

        _Text_ : 
        [input box / what type you chose]

        Note! : id and name for input element need to be the same to avoid issues
    -->
    <div >
        <!-- User name -->
        <label for="userName"> Username : </label> <br />
        <input type="text" id="userName" name="userName"/> <br />

        <!-- User name -->
        <label for="password"> password : </label> <br />
        <input type="password" id="password" name="password"/> <br />

        <!-- User name -->
        <label for="firstName"> Firstname : </label> <br />
        <input type="text" id="firstName" name="firstName"/> <br />

        <!-- User name -->
        <label for="lastName"> Lastname : </label> <br />
        <input type="text" id="lastName" name="lastName"/> <br />

        <!-- User name -->
        <label for="birthday"> Birthday : </label> <br />
        <input type="date" id="birthday" name="birthday"/> <br />

        <!-- User name -->
        <label for="city"> City : </label> <br />
        <input type="text" id="city" name="city"/> <br />
    </div>

    <!-- Break Line-->
    <br />

    <!-- Submit Button / Post Back Data -->
    <input type="submit" value="Sign Up" class="Submit"/>
</asp:Content>
