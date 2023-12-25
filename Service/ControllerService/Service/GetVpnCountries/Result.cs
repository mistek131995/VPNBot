namespace Service.ControllerService.Service.GetVpnCountries
{
    public class Result
    {
        public List<Country> Countries { get; set; }

        public class Country
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
