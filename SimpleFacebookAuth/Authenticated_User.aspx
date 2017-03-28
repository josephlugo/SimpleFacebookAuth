<%@ Page Title="Authenticated User" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Authenticated_User.aspx.vb" Inherits="SimpleFacebookAuth.Authenticated_User" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">      
    <div class="row">        
        <div class="form-group col-md-4 col-md-offset-4">
            <p>
        
            </p> 
            <p class="text-center">
                &nbsp;<asp:Label ID="Label3" runat="server" Text="Label" Visible="False"></asp:Label>
            </p>
    
            <p class="text-center">
                <asp:Image ID="Image1" runat="server" />
            </p>   
        
            <p class="text-center">
                <asp:Label ID="Label1" runat="server" Text="Label" Visible="False"></asp:Label>
            </p>        
   
            <p class="text-center">
                <asp:Label ID="Label2" runat="server" Text="Label" Visible="False"></asp:Label>
            </p>   

            <p class="text-center">
                <asp:Button ID="LogoutBtn" runat="server" CssClass="btn btn-primary btn-lg" Text="Logout" Visible="False" />
            </p>
        </div>        
    </div>    
</asp:Content>
