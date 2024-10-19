using SaLes__APIs.Entity;
using SaLes__APIs.Serviecs.BaseClass;
using SaLes__APIs.Serviecs.RepositoryServices;

namespace SaLes__APIs.Serviecs.RouterClass
{
    public class RouterInvoice : BaseRouter
    {
        public RouterInvoice()
        {
            UrlFragment = "/api/Invoices";
            TagName = "Invoices";
        }
        public override void AddRoutes(WebApplication app)
        {
            // Get Invoices By Customer (Id)
            app.MapGet($"{UrlFragment}/GetByCustomer/{{id:Guid}}", (InvoiceRepository repo, Guid id) => GetByCustomer(repo, id)).WithTags(TagName)
                .Produces(200)
                .Produces<List<Invoice>>()
                .Produces(404)
                .Produces(500).RequireAuthorization();
            // Insert new Invoices
            app.MapPost($"{UrlFragment}/Post", (InvoiceRepository repo, Invoice entity) => Insert(repo,entity)).WithTags(TagName)
                .Produces(200)
                .Produces(500).RequireAuthorization();

        }
        protected virtual async Task<IResult> GetByCustomer(InvoiceRepository repo, Guid id)
        {
            InfoMass = "This Customer dont have any Invoices OR This Id Note Valid";
            IResult r;
            List<Invoice> list = await repo.GetByCustomer(id);
            try
            {
                if (list.Count == 0 || list == null) { r = Results.BadRequest(InfoMass); }
                else { r = Results.Ok(list); }
            }
            catch (Exception ex)
            {
                r = Results.Problem($"{InfoMass} : {ex.Message}");
            }
            return r;
        }
        protected virtual async Task<IResult> Insert(InvoiceRepository repo , Invoice entity) 
        {
            InfoMass = "Some thing is rong in enter data ";
            IResult r;
            try
            {
                entity = await repo.Insert(entity);
                if (entity == null) { r = Results.BadRequest(InfoMass); }
                else { r = Results.Ok(entity); }
                    
                
            }
            catch (Exception ex)
            {
                r = Results.Problem($"{InfoMass} : {ex.Message}");
            }
            return r;
        }
    }
}
