# Simple Facebook Authentication
A basic Facebook authentication app built on ASP.Net, Web Forms and VB.Net using Facebook C# SDK

### Please read carefully before using the solution:

Create a new app using https://developers.facebook.com/apps

Go to the web.config file under SimpleFacebookAuth folder. Copy the generated APP ID and APP secret in the correspondent <appSettings> tag.
(You can also have these sensitive codes stored in other convenient locations, Ex. encrypted database columns, etc.)

* Once the new Facebook app has been created, on your Facebook app dashboard, go to Settings:

Site URL: Type the base URL of your website (Ex. http://www.mywebsite.com)

App domains: Type the website domain (Ex. www.mywebsite.com)

* Add new Product, select Facebook Login.

Valid OAuth redirect URIs: Usually, two URLs must be provided:

First one for the redirect login page, (Ex. http://www.mywebsite.com/Authenticated_User)

The second one for the redirect page once user logged off (Ex. http://www.mywebsite.com/)
