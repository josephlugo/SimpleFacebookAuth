<%@ Page Title="Welcome" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.vb" Inherits="SimpleFacebookAuth._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1 style="font-size: xx-large"><strong>Simple Facebook Authentication</strong></h1>
        <p class="lead">A template application for Facebook authentication flow. Once logged in, the user can see its basic personal data such as name, email and profile picture.</p>
        <p>
            <asp:Button ID="LoginBtn" runat="server" CssClass="btn btn-primary btn-lg" Text="Login with Facebook" />
        </p>    
        <p>
            <asp:Button ID="LogoutBtn" runat="server" CssClass="btn btn-primary btn-lg" Text="Logout" Visible="False" />
        </p>    
    </div>    

</asp:Content>
