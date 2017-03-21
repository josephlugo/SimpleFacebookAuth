Imports System.Dynamic
Imports Facebook

'<a rel = "license" href="http://creativecommons.org/licenses/by-nc-sa/4.0/"><img alt="Creative Commons License" style="border-width:0" src="https://i.creativecommons.org/l/by-nc-sa/4.0/80x15.png" /></a><br /><span xmlns:dct = "http://purl.org/dc/terms/" Property="dct:title">Simple Facebook Authentication</span> by <a xmlns:cc = "http://creativecommons.org/ns#" href="https://www.linkedin.com/in/jalugo/" Property="cc:attributionName" rel="cc:attributionURL">Jose Alejandro Lugo Garcia</a> Is licensed under a <a rel="license" href="http://creativecommons.org/licenses/by-nc-sa/4.0/">Creative Commons Attribution-NonCommercial-ShareAlike 4.0 International License</a>.<br />Based On a work at <a xmlns:dct = "http://purl.org/dc/terms/" href="https://github.com/jlugooi/SimpleFacebookAuth" rel="dct:source">https://github.com/jlugooi/SimpleFacebookAuth</a>.

Public Class Authenticated_User
    Inherits System.Web.UI.Page

    Private fb = New FacebookClient()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not (Page.IsPostBack) Then

            'To get app_Access_Token
            'Dim result1 = fb.Get("oauth/access_token", New With {
            '    Key .client_id = ConfigurationManager.AppSettings("FacebookAppId").ToString(),
            '    Key .client_secret = ConfigurationManager.AppSettings("FacebookSecret").ToString(),
            '    Key .grant_type = "client_credentials"
            '})

            'app_Access_Token = result1.access_token

            'If the user gained access putting the right credentials, then it should get an authorization code
            'Checking if the authorization code is on the URL:
            If Not (String.IsNullOrEmpty(Request.QueryString("code"))) Then

                'Using the authorization code to get the user access token
                Dim result2 = fb.Get("oauth/access_token", New With {
                Key .client_id = ConfigurationManager.AppSettings("FacebookAppId").ToString(),
                Key .client_secret = ConfigurationManager.AppSettings("FacebookSecret").ToString(),
                Key .redirect_uri = Session.Item("redirect_Uri"),
                Key .code = Request.QueryString("code")
                })

                Session("user_Access_Token") = result2.access_token

                Dim oauthResult As FacebookOAuthResult
                Dim UserID As String
                Dim MyName As String
                Dim MyEmail As String

                'Checking if the user got authenticated with the access token granted from Facebook
                If fb.TryParseOAuthCallbackUrl(New Uri("https://www.facebook.com/connect/login_success.html#access_token=" + CType(Session.Item("user_Access_Token"), String)), oauthResult) Then
                    If oauthResult.IsSuccess Then
                        Session("Authenticated") = True
                        'Setting Logout button visible
                        LogoutBtn.Visible = True

                        'Getting basic user info from Facebook
                        fb = New FacebookClient(CType(Session.Item("user_Access_Token"), String))
                        Dim details1 = fb.Get("me")
                        UserID = details1.id
                        MyName = details1.name
                        Dim details2 = fb.Get("/me?fields=email")
                        MyEmail = details2.email

                        Label1.Visible = True
                        Label2.Visible = True
                        Label3.Visible = True
                        Label3.Text = "Congratulations. You are authenticated!"
                        Label1.Text = "Welcome: " + MyName + "!"
                        Label2.Text = "Your email: " + MyEmail

                        Dim profilePictureUri As Uri = New Uri(String.Format("https://graph.facebook.com/{0}/picture?type={1}&access_token={2}", UserID, "large", CType(Session.Item("user_Access_Token"), String)))

                        Image1.ImageUrl = profilePictureUri.ToString()

                    Else
                        'User was not authenticated
                        Session("Authenticated") = False
                        'Setting Logout button hidden
                        LogoutBtn.Visible = False
                    End If

                End If

            Else
                'If user don't had access to the page through a valid auth flow, trigger a page error access
                Label1.Visible = True
                Label1.Text = "ACCESS DENIED! You haven't followed the proper authentication flow."
            End If

        End If

    End Sub

    Protected Sub LogoutBtn_Click(sender As Object, e As EventArgs) Handles LogoutBtn.Click

        Dim logout_uri = Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath
        Dim logoutUrl = fb.GetLogoutUrl(New With {
            Key .access_token = Session("user_Access_Token"),
            Key .[next] = logout_uri.ToString()
        })

        Session("Authenticated") = False

        Response.Redirect(logoutUrl.ToString())

    End Sub
End Class