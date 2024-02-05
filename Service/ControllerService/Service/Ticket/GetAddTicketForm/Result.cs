namespace Service.ControllerService.Service.GetAddTicketForm
{
    public class Result
    {
        public List<Option> Categories { get; set; }

        public class Option
        {
            public string Value { get; set; }
            public string Label { get; set; }
        }
    }
}
