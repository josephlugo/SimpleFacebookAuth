# SimpleFacebookAuth
Facebook authentication with ASP.NET Web Forms-VB.Net using Facebook C# SDK

###Please read this carefully before using this solution:

Create a new app using https://developers.facebook.com/apps

Go to web.config and copy the APP ID and APP secret under the correspondent <appSettings> tag.
(You can also have these sensitive codes stored in other convenient locations, Ex. encrypted database columns, etc.)

* Once the new Facebook have been created,on your Facebook app dashboard, go to Settings:

Site URL: Type the base URL of your website (Ex. http://www.mywebsite.com)
App domains: Type the website domain (Ex. www.mywebsite.com)

* Add new Product, select Facebook Login.

Valid OAuth redirect URIs: Usually two URLs are put in here:
First one for the redirect login page, (Ex. http://www.mywebsite.com/Authenticated_User)
the second one for the redirect page once user logged off (Ex. http://www.mywebsite.com/)
