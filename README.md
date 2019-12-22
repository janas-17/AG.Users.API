# AG.Users.API

Startup:

Change the SQL connection string in API appsettings.json to whatever works for you, added a migration for you to build easily
Could also use an in memory dbContext as found in the Tests solution if you want, just swap out the Startup services.AddDbContext lines :)

Added Swagger UI as launch URL, should be able to try out endpoints straight from there

/healthz will provide health checks response, or /show-health-ui for a UI

Assumptions:
  - operators and administrators have a common structure and can therefore be stored in a single structure using a discriminator field
  - 200 char limit on names is for both first name and last name combined
  - name lookup is searching standard user names, first name and last name (not gameName), case insensitive
  - since approved is binary in nature, defined it as such rather than a 'status'
  -- Additional comments can be found throughout solution in case I'm forgetting to mention something here
  
Didn't go over board with the unit or integration tests, just gave some of the general basics. Structure wise I wanted to demonstrate
some separation of concern and layering but didn't go crazy either. Used the inheritence and use of interfaces and generic repo's to
demonstrate some extras I guess.

Hope everything works!
