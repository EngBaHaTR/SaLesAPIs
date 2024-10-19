namespace SaLes__APIs.Serviecs.BaseClass
{
    public abstract class BaseRouter
    {
        public BaseRouter()
        {
            UrlFragment = string.Empty;
            TagName = string.Empty;
            InfoMass = string.Empty;
        }
        public string UrlFragment { get; set; }
        public string TagName { get; set; }
        public string InfoMass { get; set; }
        public abstract void AddRoutes(WebApplication app);
    }
}
