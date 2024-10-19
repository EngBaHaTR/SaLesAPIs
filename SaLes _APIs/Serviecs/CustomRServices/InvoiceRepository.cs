using BahaDev.EX.UoW;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaLes__APIs.Entity;


namespace SaLes__APIs.Serviecs.RepositoryServices
{
    public class InvoiceRepository : RServieces<Invoice , SelasContext>
    {
        private readonly SelasContext _context;
        public InvoiceRepository(SelasContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Invoice>> GetByCustomer(Guid id) 
        {
            Customer? customer = await _context .Customer .FindAsync(id);
            if (customer == null) { return null; }
            else {
                List<Invoice> invo = await _context.Invoice.Include(c => c.CustomerNavigation).Where(o => o.Customer == id).ToListAsync();
                return invo;
            }


        }
    }
}
