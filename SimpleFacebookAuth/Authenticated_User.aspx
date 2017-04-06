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
                <asp:Image ID="Image1" runat="server" Visible="False" />
            </p>   
        
            <p class="text-center">
                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
            </p>        
   
            <p class="text-center">
                <asp:Label ID="Label2" runat="server" Text="Label" Visible="False"></asp:Label>
            </p>   

             <p class="text-center">
                <asp:Label ID="Label4" runat="server" Text="Post something to your wall:" Visible="False" style="font-weight: 700"></asp:Label>
             </p>  

             <p class="text-center">
                <asp:TextBox ID="lbStatus" runat="server" Height="69px" Visible="False" Width="279px"></asp:TextBox>
             </p>

             <p class="text-center">                 
                 <asp:Label ID="Label5" runat="server" Text="Photo: " style="font-weight: 700" Visible="False"></asp:Label>
             </p>

            <div style="text-align: center; margin-left: auto; margin-right: auto">
                <asp:FileUpload ID="FileUpload1" runat="server" Visible="False" accept=".jpg, .jpeg"/>
            </div> 

             <p class="text-center">                 
             </p>
             
             <p class="text-center">
                 <asp:Button ID="PostBtn" runat="server" CssClass="btn btn-default" Text="Post" Visible="False" Width="100px" />
             </p> 
            
            <hr />  

            <p class="text-center">
                <asp:Button ID="LogoutBtn" runat="server" CssClass="btn btn-primary btn-lg" Text="Logout" Visible="False" />
            </p>
        </div>        
    </div>    
</asp:Content>
