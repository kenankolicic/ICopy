namespace iCopy.Model.Response
{
    public class City
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public int PostalCode { get; set; }
        public Country Country { get; set; }
        public int CountryID { get; set; }
        public bool Active { get; set; }
    }
}
