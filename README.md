# SimpleFacebookAuth
Facebook authentication with ASP.NET Web Forms-VB.Net using Facebook C# SDK

#Please read this before using this solution:

1- Create a new app using https://developers.facebook.com/apps
2- Go to web.config and copy the APP ID and APP secret under the correspondent <appSettings> tag.
   (You can also have these important codes stored in other convenient locations, Ex. encrypted database columns, etc.)
3- Once created,on your Facebook app dashboard, go to Settings:
   3.1- Site URL: Type the base URL of your website (Ex. http://www.mywebsite.com)
   3.2- App domains: Type the website domain (Ex. www.mywebsite.com)
4- Add new Product, select Facebook Login.
   4.1- Valid OAuth redirect URIs: Usually two URLs are put in here: 
        First one for the redirect login page, (Ex. http://www.mywebsite.com/Authenticated_User)
        the second one for the redirect page once user logged off (Ex. http://www.mywebsite.com/)
