Imports System.Dynamic
Imports System.IO
Imports Facebook

'Please read carefully before using the solution:
'1- Create a new app using https://developers.facebook.com/apps
'2- Go to web.config and copy the APP ID and APP secret under the correspondent <appSettings> tag.
'   (You can also have these sensitive codes stored in other convenient locations, Ex. encrypted database columns, etc.)
'3- Once the new Facebook app has been created, on your Facebook app dashboard, go to Settings:
'   3.1- Site URL: Type the base URL of your website (Ex. http://www.mywebsite.com)
'   3.2- App domains: Type the website domain (Ex. www.mywebsite.com)
'4- Add new Product, select Facebook Login.
'   4.1- Valid OAuth redirect URIs: Usually two URLs are put in here: 
'        First one for the redirect login page, (Ex. http://www.mywebsite.com/Authenticated_User)
'        the second one for the redirect page once user logged off (Ex. http://www.mywebsite.com/)

'<a rel = "license" href="http://creativecommons.org/licenses/by-nc-sa/4.0/"><img alt="Creative Commons License" style="border-width:0" src="https://i.creativecommons.org/l/by-nc-sa/4.0/80x15.png" /></a><br /><span xmlns:dct = "http://purl.org/dc/terms/" Property="dct:title">Simple Facebook Authentication</span> by <a xmlns:cc = "http://creativecommons.org/ns#" href="https://www.linkedin.com/in/jalugo/" Property="cc:attributionName" rel="cc:attributionURL">Jose Alejandro Lugo Garcia</a> Is licensed under a <a rel="license" href="http://creativecommons.org/licenses/by-nc-sa/4.0/">Creative Commons Attribution-NonCommercial-ShareAlike 4.0 International License</a>.<br />Based On a work at <a xmlns:dct = "http://purl.org/dc/terms/" href="https://github.com/jlugooi/SimpleFacebookAuth" rel="dct:source">https://github.com/jlugooi/SimpleFacebookAuth</a>.

Public Class _Default
    Inherits Page

    Protected ReadOnly _fb As New FacebookClient

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        'Testing if User gained access from previous login attempt
        If Not Session("Authenticated") Is Nothing Then
            'Set logging off button visible
            LogoutBtn1.Visible = CType(Session.Item("Authenticated"), Boolean)
        End If

    End Sub

    Protected Sub LoginBtn1_Click(sender As Object, e As ImageClickEventArgs) Handles LoginBtn1.Click
        Dim facebookAppId = ConfigurationManager.AppSettings("FacebookAppId").ToString()
        Dim extendedPermissions = ConfigurationManager.AppSettings("FacebookScope").ToString()

        'Use this line when publish to IIS
        Session("redirect_Uri") = Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath + "/Authenticated_User.aspx"

        'Use this line when test locally with IIS express
        'Session("redirect_Uri") = Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath + "Authenticated_User.aspx"

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

    Protected Sub LogoutBtn1_Click(sender As Object, e As ImageClickEventArgs) Handles LogoutBtn1.Click
        'Cleaning up temporary uploaded pictures from server.
        If (Directory.GetFiles(Server.MapPath("~/Content/Pictures")).Length > 0) Then
            Array.ForEach(Directory.GetFiles(Server.MapPath("~/Content/Pictures")), Sub(x) File.Delete(x))
        End If

        Dim logout_uri = Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath
        Dim logoutUrl = _fb.GetLogoutUrl(New With {
            Key .access_token = Session("user_Access_Token"),
            Key .[next] = logout_uri.ToString()
        })

        Session("Authenticated") = False

        Response.Redirect(logoutUrl.ToString())
    End Sub
End Class