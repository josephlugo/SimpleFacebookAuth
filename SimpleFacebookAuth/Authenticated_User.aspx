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
                <asp:Image ID="Image1" runat="server" Visible="False" TabIndex="1" />
            </p>   
        
            <p class="text-center">
                <asp:Label ID="Label1" runat="server" Text="Label" TabIndex="2"></asp:Label>
            </p>        
   
            <p class="text-center">
                <asp:Label ID="Label2" runat="server" Text="Label" Visible="False" TabIndex="3"></asp:Label>
            </p>   

             <p class="text-center">
                <asp:Label ID="Label4" runat="server" Text="Post something to your wall:" Visible="False" style="font-weight: 700" TabIndex="4" AssociatedControlID="lbStatus"></asp:Label>
             </p>  

             <p class="text-center">
                <asp:TextBox ID="lbStatus" runat="server" Height="69px" Visible="False" Width="279px" TextMode="MultiLine" TabIndex="5"></asp:TextBox>
             </p>

             <p class="text-center">                 
                 <asp:Label ID="Label5" runat="server" Text="Photo: (8 Mb max)" style="font-weight: 700" Visible="False" TabIndex="6"></asp:Label>                                                                    
             </p>
              
             <p class="text-center">                 
                 
             </p>  

             <p class="text-center">                                  
                 <asp:FileUpload ID="FileUpload1" runat="server" CssClass="btn btn-default fileUploadMargin" AllowMultiple="True" TabIndex="7"/>
                 <asp:RegularExpressionValidator ID="regexValidator" runat="server"
                    ControlToValidate="FileUpload1"
                    ErrorMessage="Only JPEG images allowed." 
                    ValidationExpression="(.*\.([Jj][Pp][Gg])|.*\.([Jj][Pp][Ee][Gg])$)" ForeColor="#CC3300" TabIndex="8"></asp:RegularExpressionValidator>
             </p>  
           

            <p class="text-center">
                                 
             </p> 

             <p class="text-center">
                 <asp:Button ID="PostBtn" runat="server" CssClass="btn btn-default" Text="Post" Visible="False" Width="100px" TabIndex="9" />
             </p> 
            
            <hr />  

            <p class="text-center">
                <asp:ImageButton ID="LogoutBtn1" runat="server" ImageUrl="~/Content/Logout.png" Visible="False" AlternateText="Logout from the web application button" TabIndex="10" />
            </p>
        </div>        
    </div>        
       
</asp:Content>

