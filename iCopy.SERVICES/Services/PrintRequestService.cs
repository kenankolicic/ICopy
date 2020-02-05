using AutoMapper;
using iCopy.Database.Context;
using iCopy.Model.Request;
using iCopy.SERVICES.Extensions;
using iCopy.SERVICES.IServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCopy.SERVICES.Services
{
    class PrintRequestService : CRUDService<Database.PrintRequest, Model.Request.PrintRequest, Model.Request.PrintRequest, Model.Response.PrintRequest, Model.Request.PrintRequestSearch, int>, IPrintRequestService
    {
        private readonly AuthContext auth;
        private readonly IUserService UserService;
        private readonly IProfilePhotoService ProfilePhotoService;

        public PrintRequestService(
           DBContext ctx,
           IMapper mapper,
           AuthContext auth,
           IUserService UserService,
           IProfilePhotoService ProfilePhotoService) : base(ctx, mapper)
        {
            this.auth = auth;
            this.UserService = UserService;
            this.ProfilePhotoService = ProfilePhotoService;
        }

        public override async Task<Model.Response.PrintRequest> GetByIdAsync(int id)
        {
            Model.Response.PrintRequest printRequest = mapper.Map<Model.Response.PrintRequest>(await ctx.Requests.Include(x => x.Client).Include(x => x.Copier).FirstOrDefaultAsync(x => x.ID == id));           
            return printRequest;
        }

        public override async Task<List<Model.Response.PrintRequest>> TakeRecordsByNumberAsync(int take = 15)
        {
            List<Model.Response.PrintRequest> items = mapper.Map<List<Model.Response.PrintRequest>>(await ctx.Requests.Include(x => x.Copier).Include(x => x.Client).ToListAsync());
            return items;
        }

        public override async Task<Tuple<List<Model.Response.PrintRequest>, int>> GetByParametersAsync(PrintRequestSearch search, string order, string nameOfColumnOrder, int start, int length)
        {
            var query = ctx.Requests.Include(x => x.Copier).Include(x => x.Client).AsQueryable();
            if (search.ClientId != null)
                query = query.Where(x => x.ClientId == search.ClientId);
            if (search.CopierId != null)
                query = query.Where(x => x.CopierId == search.CopierId);


            if (nameOfColumnOrder == nameof(Database.PrintRequest.ID))
                query = query.OrderByAscDesc(x => x.ID, order);
            else if (nameOfColumnOrder == nameof(Database.Client.Person.FirstName))
                query = query.OrderByAscDesc(x => x.Client.Person.FirstName, order);
            else if (nameOfColumnOrder == nameof(Database.Client.Person.LastName))
                query = query.OrderByAscDesc(x => x.Client.Person.LastName, order);

            var data = mapper.Map<List<Model.Response.PrintRequest>>(await query.Skip(start).Take(length).ToListAsync());
            return new Tuple<List<Model.Response.PrintRequest>, int>(data, await query.CountAsync());
        }

        public override async Task<Model.Response.PrintRequest> DeleteAsync(int id)
        {
            Database.PrintRequest printRequest = await ctx.Requests.FindAsync(id);
                ctx.Requests.Remove(printRequest);
                await ctx.SaveChangesAsync();
                // TODO: Dodati log operaciju
            return mapper.Map<Model.Response.PrintRequest>(printRequest);
        }

        public override async Task<Model.Response.PrintRequest> UpdateAsync(int id, Model.Request.PrintRequest entity)
        {
            try
            {
                Model.Response.PrintRequest request = await base.UpdateAsync(id, entity);
                // TODO: Dodati Log
                return request;
            }
            catch (Exception e)
            {
                // TODO: Dodati log
                throw e;
            }
        }

        public override Task<Model.Response.PrintRequest> InsertAsync(Model.Request.PrintRequest entity)
        {
            entity.Status = Database.Status.OnHold;
            return base.InsertAsync(entity);
        }
    }
}
