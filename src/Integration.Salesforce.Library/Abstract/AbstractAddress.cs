namespace Integration.Salesforce.Library.Abstract
{
    public abstract class AbstractAddress
    {
        public abstract string StreetAddress { get; set; }
        public abstract string City { get; set; }
        public abstract string State { get; set; }
        public abstract int Zip { get; set; }
    }
}