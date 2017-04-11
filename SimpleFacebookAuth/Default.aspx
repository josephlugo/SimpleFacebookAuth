﻿<%@ Page Title="Welcome" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.vb" Inherits="SimpleFacebookAuth._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1 style="font-size: xx-large"><strong>Simple Facebook Authentication</strong></h1>
        <p class="lead">A template application for Facebook authentication flow. Once logged in, the user can see its basic personal data such as name, email and profile picture.</p>        
        <p>
            <asp:ImageButton ID="LoginBtn1" runat="server" ImageUrl="~/Content/Login.png" />
        </p>            
        <p>
            <asp:ImageButton ID="LogoutBtn1" runat="server" ImageUrl="~/Content/Logout.png" Visible="False" />
        </p>    
    </div>    

</asp:Content>
