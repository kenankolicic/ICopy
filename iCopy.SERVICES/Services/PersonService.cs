using AutoMapper;
using iCopy.Database.Context;
using iCopy.SERVICES.IServices;
using System;
using System.Threading.Tasks;

namespace iCopy.SERVICES.Services
{
    public class PersonService : CRUDService<Database.Person, Model.Request.Person, Model.Request.Person, Model.Response.Person, object, int>, IPersonService
    {
        public PersonService(DBContext ctx, IMapper mapper) : base(ctx, mapper)
        {
        }
        public override async Task<Model.Response.Person> UpdateAsync(int id, Model.Request.Person entity)
        {
            try
            {
                //Model.Response.Person person = await base.UpdateAsync(id, entity);
                //if (entity != null)
                //{
                //    entity.ProfilePhoto.ApplicationUserId = copier.ApplicationUserId;
                //    await ProfilePhotoService.InsertAsync(entity.ProfilePhoto);
                //}
                // TODO: Dodati Log
                // return copier;
                return null;
            }
            catch (Exception e)
            {
                // TODO: Dodati log
                throw e;
            }
        }
    }
}

