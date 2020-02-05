using System.Collections.Generic;

namespace iCopy.Web.Models
{
    public class DataTable<TResult>
    {
        public List<TResult> data { get; set; }
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public string error { get; set; }
    }
}
