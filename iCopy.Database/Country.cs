namespace iCopy.Database
{
    public class Country : BaseEntity<int>
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string PhoneNumberCode { get; set; }
        public string PhoneNumberRegex { get; set; }
    }
}
