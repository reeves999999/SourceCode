Login using reeves999999@gmail.com, Password: $ourceCode999

If required, please set CMD to wwwroot and run "npm install" and/or "npm run build" to regenerate JS and CSS.

If you delete all clients, just restart the app and two will be regenerated.


Application features:

-MVC with CRUD, Paging, Sorting, and Searching of Client Name (via API)
-API with CRUD
-SASS
-webpack
-Identity (basic)
-Versioned API
-xUnit
-Bootstrap Wrapper HTML integrated
-Image upload (via MVC Details > Edit screen NOT inline editor)
-Child Projects (no CRUD, just display)
-Serilog


Missed out, but would have added, given time:

-Something other than jQuery! - As Bootstrap uses this and I wanted Bootstrap I just stuck with it and its jQuery-based scripting
-Fluent API for ClientProject
-CQRS
-Swagger
-Separate API,MVC, Domain and Service projects
-Separate Identity and Application DB contexts
-React for front end interaction
-Move connection string into Key Vault or similar
-Authentication and roles
-JWT for APIs
-Centralised, sharable JS modules
-Polly (API retries)
-More Unit tests
-JS testing using Jest
-Integration tests using in-memory DB
-Selenium
-Wider searching and filtering
-TypeScript
-Use reflection on object properties to better deduce sorting and filtering without magic strings
-Allow JS to update paging status (currently commented out) 
-Make modals reusable
-Possibly add AutoMapper (not a fan of its overuse and silent failures)
-CRUD for the Client Projects

ISSUES:

-Could not connect SMSS to Azure DB