<%@ Page Title="Welcome" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.vb" Inherits="SimpleFacebookAuth._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1 style="font-size: xx-large"><strong>Simple Facebook Authentication</strong></h1>
        <p class="lead" style="font-size: medium">A template application for Facebook authentication flow. Once logged in, you can see your name, email and profile picture.</p>
        <p class="lead" style="font-size: medium">You can also post your status/pictures to your wall!</p>        
        <p>
            <asp:ImageButton ID="LoginBtn1" runat="server" ImageUrl="~/Content/Login.png" />
        </p>            
        <p>
            <asp:ImageButton ID="LogoutBtn1" runat="server" ImageUrl="~/Content/Logout.png" Visible="False" />
        </p> 
    </div>   
    
    <div class="fb-like" data-href="http://d348070.mdc.edu/eauth/" data-layout="standard" data-action="like" data-size="small" data-show-faces="true" data-share="true"></div>     
    
</asp:Content>
