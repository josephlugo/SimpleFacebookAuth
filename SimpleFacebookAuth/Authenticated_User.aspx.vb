Imports System.Dynamic
Imports System.IO
Imports Facebook
Imports Newtonsoft.Json.Linq

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
                        LogoutBtn1.Visible = True

                        'Getting basic user info from Facebook
                        fb = New FacebookClient(CType(Session.Item("user_Access_Token"), String))
                        Dim details1 = fb.Get("me")
                        UserID = details1.id
                        MyName = details1.name
                        Dim details2 = fb.Get("/me?fields=email")
                        MyEmail = details2.email

                        ShowPageDetails()

                        Label3.Text = "Congratulations. You are authenticated!"
                        Label1.Text = "Welcome: " + MyName + "!"
                        Label2.Text = "Your email: " + MyEmail

                        'Getting profile picture
                        Dim profilePictureUri As Uri = New Uri(String.Format("https://graph.facebook.com/{0}/picture?type={1}&access_token={2}", UserID, "large", CType(Session.Item("user_Access_Token"), String)))

                        Image1.ImageUrl = profilePictureUri.ToString()

                        'Getting friend list 
                        'In order for a person to show up in one person's friend list, both people must
                        'have decided to share their list of friends with your app And Not disabled that
                        'permission during login. Also both friends must have been asked for user_friends
                        'during the login process.
                        'More info at: https://developers.facebook.com/docs/facebook-login/permissions#reference-user_friends

                        Dim friendListData = fb.Get("/me/friends")

                        Dim friendListJson As JObject = JObject.Parse(friendListData.ToString())
                        Dim fbUsers As New List(Of FbUser)()

                        For Each var In friendListJson("data").Children()
                            Dim fbUser As New FbUser()
                            fbUser.Id = var("id").ToString().Replace("""", "")
                            fbUser.Name = var("name").ToString().Replace("""", "")
                            fbUsers.Add(fbUser)
                        Next

                    Else
                        'User was not authenticated
                        Session("Authenticated") = False
                        'Setting Logout button hidden
                        LogoutBtn1.Visible = False
                        HidePageDetails()
                    End If

                End If

            Else
                'If the user don't have access to the page through a valid auth flow, trigger a page error access
                HidePageDetails()
                Label1.Text = "ACCESS DENIED! You haven't followed the proper authentication flow. Please, go back to the main page and log in."
            End If

        End If

    End Sub

    Private Sub ShowPageDetails()
        Label2.Visible = True
        Label3.Visible = True
        Label4.Visible = True
        Label5.Visible = True
        lbStatus.Visible = True
        PostBtn.Visible = True
        Image1.Visible = True
        FileUpload1.Visible = True
    End Sub

    Private Sub HidePageDetails()
        Label2.Visible = False
        Label3.Visible = False
        Label4.Visible = False
        Label5.Visible = False
        lbStatus.Visible = False
        PostBtn.Visible = False
        Image1.Visible = False
        FileUpload1.Visible = False
    End Sub

    Protected Sub PostBtn_Click(sender As Object, e As EventArgs) Handles PostBtn.Click

        Try

            If Not (String.IsNullOrEmpty(lbStatus.Text) And Not FileUpload1.HasFile) Then

                Dim photoList As New List(Of String)
                Dim index As Integer = 0

                If (FileUpload1.HasFile) Then

                    'Saving the photo(s) to upload temporarily in /Content/Pictures
                    For Each file As HttpPostedFile In FileUpload1.PostedFiles
                        photoList.Add(Path.Combine(Server.MapPath("~/Content/Pictures"), file.FileName))
                        file.SaveAs(photoList(index))
                        index += 1
                    Next

                End If

                'Check if user wants to publish both status and pictures
                If Not (String.IsNullOrEmpty(lbStatus.Text) Or photoList.Count = 0) Then
                    PostStatusWithPictures(CType(Session.Item("user_Access_Token"), String), lbStatus.Text, photoList)
                    'If user wants just to publish status
                ElseIf (photoList.Count = 0) Then
                    PostStatus(CType(Session.Item("user_Access_Token"), String), lbStatus.Text)
                    'If user wants just to publish pictures
                ElseIf (String.IsNullOrEmpty(lbStatus.Text)) Then
                    PostPictures(CType(Session.Item("user_Access_Token"), String), photoList)
                End If

                lbStatus.Text = Nothing

                Response.Write("<script>alert('Posted to your wall!');</script>")

            Else
                Response.Write("<script>alert('Hey, you are not posting something!');</script>")
            End If

        Catch ex As Exception
            Response.Write("<script>alert('" + ex.Message + "');</script>")
        End Try

    End Sub

    Private Sub PostStatusWithPictures(accessToken As String, status As String, photoList As List(Of String))

        fb = New FacebookClient(accessToken)

        For Each photoPath In photoList

            Using stream = File.OpenRead(photoPath)
                fb.Post("me/photos", New With {
                    Key .message = status,
                    Key .file = New FacebookMediaStream() With {
                        .ContentType = "image/jpg",
                        .FileName = Path.GetFileName(photoPath)
                    }.SetValue(stream)
                })
            End Using

        Next

    End Sub

    Private Sub PostStatus(accessToken As String, status As String)

        fb = New FacebookClient(accessToken)

        Dim parameters As Object = New ExpandoObject()
        parameters.message = status
        Dim result As Object = fb.Post("me/feed", parameters)

    End Sub


    Private Sub PostPictures(accessToken As String, photoList As List(Of String))

        fb = New FacebookClient(accessToken)

        For Each photoPath In photoList

            Using stream = File.OpenRead(photoPath)
                fb.Post("me/photos", New With {
                Key .file = New FacebookMediaStream() With {
                    .ContentType = "image/jpg",
                    .FileName = Path.GetFileName(photoPath)
                }.SetValue(stream)
            })
            End Using
        Next

    End Sub

    Protected Sub LogoutBtn1_Click(sender As Object, e As ImageClickEventArgs) Handles LogoutBtn1.Click
        'Cleaning up temporary uploaded pictures from server.
        If (Directory.GetFiles(Server.MapPath("~/Content/Pictures")).Length > 0) Then
            Array.ForEach(Directory.GetFiles(Server.MapPath("~/Content/Pictures")), Sub(x) File.Delete(x))
        End If

        Dim logout_uri = Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath
        Dim logoutUrl = fb.GetLogoutUrl(New With {
            Key .access_token = Session("user_Access_Token"),
            Key .[next] = logout_uri.ToString()
        })

        Session("Authenticated") = False

        Response.Redirect(logoutUrl.ToString())
    End Sub
End Class