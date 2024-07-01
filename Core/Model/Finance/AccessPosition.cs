using Core.Common;
using Service.ControllerService.Common;
using System.Diagnostics;

namespace Core.Model.Finance
{
    public class AccessPosition : IAggregate
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public int MonthCount { get; private set; }
        public string Description { get; private set; }
        public int Price { get; private set; }
        public string GooglePlayIdentifier { get; private set; }

        public AccessPosition(int id, string name, int monthCount, string description, int price, string googlePlayIdentifier)
        {
            Id = id;
            Name = name;
            MonthCount = monthCount;
            Description = description;
            Price = price;
            GooglePlayIdentifier = googlePlayIdentifier;
        }

        public AccessPosition(string name, int monthCount, string description, int price, string googlePlayIdentifier)
        {
            Name = name;
            MonthCount = monthCount;
            Description = description;
            Price = price;
            GooglePlayIdentifier = googlePlayIdentifier;
        }

        public void UpdateName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new HandledException("Имя не может быть пустым");

            Name = name;
        }

        public void UpdateDescription(string description)
        {
            if (string.IsNullOrEmpty(description))
                throw new HandledException("Описание не может быть пустым");

            Description = description;
        }

        public void UpdatePrice(int price)
        {
            if(price < 0)
                throw new HandledException("Цена не может быть меньше 0");

            Price = price;
        }

        public void UpdateMonthCount(int monthCount)
        {
            if (monthCount < 0)
                throw new HandledException("Срок действия не может быть меньше 0");

            MonthCount = monthCount;
        }
    }
}
