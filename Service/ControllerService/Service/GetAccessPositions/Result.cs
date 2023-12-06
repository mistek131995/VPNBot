namespace Service.ControllerService.Service.GetAccessPositions
{
    public class Result
    {
        public List<AccessPosition> AccessPositions { get; set; }

        public class AccessPosition
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public int Price { get; set; }
        }
    }
}
