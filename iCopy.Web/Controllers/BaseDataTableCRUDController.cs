using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using iCopy.SERVICES.IServices;
using iCopy.Web.Models;
using iCopy.Web.Resources;
using Microsoft.AspNetCore.Mvc;

namespace iCopy.Web.Controllers
{
    public class BaseDataTableCRUDController<TInsert, TUpdate, TResult, TSearch, TPk> : BaseCRUDController<TInsert, TUpdate, TResult, TSearch, TPk> where TSearch : class where TInsert : class, new()
    {
        public BaseDataTableCRUDController(ICRUDService<TInsert, TUpdate, TResult, TSearch, TPk> crudService, SharedResource _localizer, IMapper mapper) 
            : base(crudService, _localizer, mapper)
        {
        }
        [HttpGet]
        public override async Task<IActionResult> Index()
        {
            int numberOfRecords = await crudService.GetNumberOfRecordsAsync();
            DataTable<TResult> model = new DataTable<TResult>()
            {
                recordsFiltered = numberOfRecords,
                recordsTotal = numberOfRecords,
                data = await crudService.TakeRecordsByNumberAsync()
            };
            return View(model);
        }

        [HttpPost, IgnoreAntiforgeryToken]
        public virtual async Task<DataTable<TResult>> GetData([FromForm]TSearch Search, [FromForm]DataTableRequest Request)
        {
            var filteredData = await crudService.GetByParametersAsync(Search, Request.order[0].dir, Request.columns[Request.order[0].column].name, Request.start, Request.length);
            return new DataTable<TResult>
            {
                draw = Request.draw,
                recordsTotal = filteredData.Item2,
                recordsFiltered = filteredData.Item2,
                data = filteredData.Item1
            };
        }
    }
}
