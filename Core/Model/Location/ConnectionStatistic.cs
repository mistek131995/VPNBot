namespace Core.Model.Location
{
    public class ConnectionStatistic
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Count { get; set; }

        public ConnectionStatistic(int id, DateTime date, int count)
        {
            Id = id;
            Date = date;
            Count = count;
        }
    }
}
