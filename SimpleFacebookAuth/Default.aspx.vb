Imports System.Dynamic
Imports Facebook

'Please read this before using this solution:
'1- Create a new app using https://developers.facebook.com/apps
'2- Go to web.config and copy the APP ID and APP secret under the correspondent <appSettings> tag.
'   (You can also have these important codes stored in other convenient locations, Ex. encrypted database columns, etc.)
'3- Once created,on your Facebook app dashboard, go to Settings:
'   3.1- Site URL: Type the base URL of your website (Ex. http://www.mywebsite.com)
'   3.2- App domains: Type the website domain (Ex. www.mywebsite.com)
'4- Add new Product, select Facebook Login.
'   4.1- Valid OAuth redirect URIs: Usually two URLs are put in here: 
'        First one for the redirect login page, (Ex. http://www.mywebsite.com/Authenticated_User)
'        the second one for the redirect page once user logged off (Ex. http://www.mywebsite.com/)

Public Class _Default
    Inherits Page

    Protected ReadOnly _fb As New FacebookClient

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        'Testing if User gained access from previous login attempt
        If Not Session("Authenticated") Is Nothing Then
            'Set logging off button visible
            Button2.Visible = CType(Session.Item("Authenticated"), Boolean)
        End If

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim facebookAppId = ConfigurationManager.AppSettings("FacebookAppId").ToString()
        Dim extendedPermissions = ConfigurationManager.AppSettings("FacebookScope").ToString()

        Session("redirect_Uri") = Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath + "/Authenticated_User.aspx"

        'Response_type: an access token (token), an authorization code (code), or both (code token).
        'List of additional display modes can be found at http://developers.facebook.com/docs/reference/dialogs/#display

        Dim uri = _fb.GetLoginUrl(New With {
            Key .client_id = facebookAppId,
            Key .display = "popup",
            Key .response_type = "code",
            Key .scope = extendedPermissions,
            Key .redirect_uri = Session.Item("redirect_Uri")
        })

        Response.Redirect(uri.ToString())

    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Dim logout_uri = Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath
        Dim logoutUrl = _fb.GetLogoutUrl(New With {
            Key .access_token = Session("user_Access_Token"),
            Key .[next] = logout_uri.ToString()
        })

        Session("Authenticated") = False

        Response.Redirect(logoutUrl.ToString())

    End Sub
End Class