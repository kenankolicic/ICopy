using AutoMapper;
using iCopy.Database;
using iCopy.Database.Context;
using iCopy.SERVICES.IServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrintRequestFile = iCopy.Model.Response.PrintRequestFile;

namespace iCopy.SERVICES.Services
{
    public class PrintRequestFileService : CRUDService<Database.PrintRequestFile, Model.Request.PrintRequestFile, Model.Request.PrintRequestFile, Model.Response.PrintRequestFile, object, int>, IPrintRequestFile
    {
        public PrintRequestFileService(DBContext ctx, IMapper mapper) : base(ctx, mapper)
        {
        }

        public override async Task<PrintRequestFile> InsertAsync(Model.Request.PrintRequestFile entity)
        {
            try
            {

                if (await ctx.ApplicationUserPrintRequestFile.AnyAsync(x => x.Active && x.ApplicationUserId == entity.ApplicationUserId))
                {
                    var activeApplicationUserPrintRequestFile = await ctx.ApplicationUserPrintRequestFile.SingleOrDefaultAsync(x => x.Active && x.ApplicationUserId == entity.ApplicationUserId);
                    activeApplicationUserPrintRequestFile.Active = false;
                    ctx.Entry<ApplicationUserPrintRequestFile>(activeApplicationUserPrintRequestFile).State = EntityState.Modified;
                    await ctx.SaveChangesAsync();
                }

                Model.Response.PrintRequestFile model = await base.InsertAsync(entity);

                var aplicationUserPrintRequestFile = new ApplicationUserPrintRequestFile()
                {
                    ApplicationUserId = entity.ApplicationUserId,
                    PrintRequestFileId = model.ID
                };
                ctx.ApplicationUserPrintRequestFile.Add(aplicationUserPrintRequestFile);
                await ctx.SaveChangesAsync();
                // TODO: Dodati log operaciju

                return model;
            }
            catch (Exception e)
            {

                // TODO: Dodati log operaciju

                throw e;
            }

        }

        public async Task<Model.Response.PrintRequestFile> GetByApplicationUserId(int applicationUserId)
        {
            return mapper.Map<Model.Response.PrintRequestFile>((await ctx.ApplicationUserPrintRequestFile.Include(x => x.PrintRequestFile).FirstOrDefaultAsync(x => x.ApplicationUserId == applicationUserId && x.Active))?.PrintRequestFile);
        }

        public async Task<bool> DeleteByApplicationUserIdAsync(int applicationUserId)
        {
            try
            {
                IEnumerable<Database.ApplicationUserPrintRequestFile> profile = await ctx.ApplicationUserPrintRequestFile.Where(x => x.ApplicationUserId == applicationUserId).ToListAsync();
                IEnumerable<Database.PrintRequestFile> printRequestFiles = await ctx.PrintRequestFiles.Where(x => profile.Any(y => y.PrintRequestFileId == x.ID)).ToListAsync();
                if (profile?.Any() ?? false)
                {
                    ctx.ApplicationUserPrintRequestFile.RemoveRange(profile);
                    await ctx.SaveChangesAsync();
                }

                if (printRequestFiles?.Any() ?? false)
                {
                    ctx.PrintRequestFiles.RemoveRange(printRequestFiles);
                    await ctx.SaveChangesAsync();
                }

                // TODO: Dodati log operaciju
                return true;
            }
            catch (Exception e)
            {
                //TODO: Dodati log operaciju
                throw e;
            }

            return false;
        }

        public override async Task<PrintRequestFile> UpdateAsync(int id, Model.Request.PrintRequestFile entity)
        {
            try
            {
               return await InsertAsync(entity);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
