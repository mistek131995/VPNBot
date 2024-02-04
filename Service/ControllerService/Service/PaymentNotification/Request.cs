using Application.ControllerService.Common;

namespace Service.ControllerService.Service.PaymentNotification
{
    public class Request : IRequest<string>
    {
        /// <summary>
        /// ID Вашего магазина
        /// </summary>
        public int MERCHANT_ID { get; set; }
        /// <summary>
        /// Сумма платежа
        /// </summary>
        public int AMOUNT { get; set; }
        /// <summary>
        /// Номер операции Free-Kassa
        /// </summary>
        public int intid { get; set; }
        /// <summary>
        /// Ваш номер заказа
        /// </summary>
        public int MERCHANT_ORDER_ID { get; set; }
        /// <summary>
        /// Email плательщика
        /// </summary>
        public string? P_EMAIL { get; set; }
        /// <summary>
        /// Телефон плательщика (если указан)
        /// </summary>
        public string? P_PHONE { get; set; }
        /// <summary>
        /// ID электронной валюты
        /// </summary>
        public int CUR_ID { get; set; }
        /// <summary>
        /// Подпись запроса
        /// </summary>
        public string? SIGN { get; set; }
        /// <summary>
        /// Номер счета/карты плательщика
        /// </summary>
        public string? payer_account { get; set; }
        /// <summary>
        /// Сумма комиссии в валюте платежа
        /// </summary>
        public decimal commission { get; set; }
        /// <summary>
        /// Id позиции
        /// </summary>
        public int us_position_id { get; set; }
        /// <summary>
        /// Id позиции
        /// </summary>
        public decimal us_sale { get; set; }
    }
}
