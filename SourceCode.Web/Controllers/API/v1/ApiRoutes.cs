namespace SourceCode.Web.Controllers.API.v1
{
    public static class ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "v1";
        public const string Base = Root + "/" + Version;

        public static class Clients
        {
            public const string GetAll = Base + "/clientapi";

            public const string Get = Base + "/clientapi/{id}";

            public const string Search = Base + "/clientapi/search/{search}";

            public const string Create = Base + "/clientapi";

            public const string Update = Base + "/clientapi/{id}";

            public const string Delete = Base + "/clientapi/{id}";
        }
    }
}
