<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Authenticated_User.aspx.vb" Inherits="SimpleFacebookAuth.Authenticated_User" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">      
    <p>
        
    </p>   
    <p>
        &nbsp;<asp:Label ID="Label3" runat="server" Text="Label" Visible="False"></asp:Label>
    </p>
    
     <p>
         <asp:Image ID="Image1" runat="server" />
    </p>   
        
    <p>
        <asp:Label ID="Label1" runat="server" Text="Label" Visible="False"></asp:Label>
    </p>        
   
    <p>
        <asp:Label ID="Label2" runat="server" Text="Label" Visible="False"></asp:Label>
    </p>   

    <p>
        <asp:Button ID="LogoutBtn" runat="server" CssClass="btn btn-primary btn-lg" Text="Logout" Visible="False" />
    </p>
     

</asp:Content>
