Imports System.Dynamic
Imports Facebook

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
                Dim MyName As String
                Dim MyEmail As String

                'Checking if the user got authenticated with the access token granted from Facebook
                If fb.TryParseOAuthCallbackUrl(New Uri("https://www.facebook.com/connect/login_success.html#access_token=" + CType(Session.Item("user_Access_Token"), String)), oauthResult) Then
                    If oauthResult.IsSuccess Then
                        Session("Authenticated") = True
                        'Setting Logout button visible
                        Button2.Visible = True

                        'Getting basic user info from Facebook
                        fb = New FacebookClient(CType(Session.Item("user_Access_Token"), String))
                        Dim details1 = fb.Get("me")
                        MyName = details1.name
                        Dim details2 = fb.Get("/me?fields=email")
                        MyEmail = details2.email

                        Label1.Visible = True
                        Label2.Visible = True
                        Label3.Visible = True
                        Label3.Text = "Congratulations. You are authenticated!"
                        Label1.Text = "Welcome: " + MyName + "!"
                        Label2.Text = "Your email: " + MyEmail

                    Else
                        'User was not authenticated
                        Session("Authenticated") = False
                        'Setting Logout button hidden
                        Button2.Visible = False
                    End If

                End If

            Else
                'If user don't had access to the page through a valid auth flow, trigger a page error access
                Label1.Visible = True
                Label1.Text = "ACCESS DENIED! You haven't followed the proper authentication flow."
            End If

        End If

    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Dim logout_uri = Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath
        Dim logoutUrl = fb.GetLogoutUrl(New With {
            Key .access_token = Session("user_Access_Token"),
            Key .[next] = logout_uri.ToString()
        })

        Session("Authenticated") = False

        Response.Redirect(logoutUrl.ToString())
    End Sub
End Class