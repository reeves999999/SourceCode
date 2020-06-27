Login using reeves999999@gmail.com, Password: $ourceCode999

If required, please set CMD to wwwroot and run "npm install" and/or "npm run build" to regenerate JS and CSS.


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


Missed out, but would have added, given time:

-Something other than jQuery! - As Bootstrap uses this and I wanted Bootstrap I just stuck with it and its jQuery-based scripting
-Validation on page for create/update modals
-Fluent API
-Add unique index to client name
-CQRS
-Swagger
-Separate API,MVC and Service projects
-Separate Identity and Application DB contexts
-Hand crafted my own HTML - used Inspinia Bootstrap wrapper for headstart
-React for front end interaction
-Move connection string into Key Vault or similar
-Auth and roles
-JWT for APIs
-Centralised, sharable JS modules
-Polly (API retries)
-Unit tests
-JS testing using Jest
-Integration tests using in-memory DB
-Selenium
-Wider searching and filtering
-TypeScript
-Use reflection on object properties to better deduce sorting and filtering without magic strings
-Improve CSS responsiveness around tables
-Improve UI/UX - animated positive cues for successful updates/errors etc.
-Allow JS to update paging status (currently commented out) 
-Make modals reusable
-Possibly add AutoMapper (not a fan of its overuse and silent failures)
-CRUD for the Projects

ISSUES:

-Could not connect SMSS to Azure DB