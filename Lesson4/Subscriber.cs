namespace Lesson4
{   
    /// <summary>
    /// Опредение параметров абонента.
    /// </summary>
    internal class Subscriber
    {
        /// <summary>
        /// Поле "Номер телефона".   
        /// </summary>
        public string NumberPhone;


        /// <summary>
        /// Поле "Имя".   
        /// </summary>
        public string Name;

        /// <summary>
        /// Конструктор "Subscriber" по умолчанию.
        /// </summary>
        public Subscriber()
        {
        }

        /// <summary>
        /// Конструктор "Subscriber" для логических операций с переменными.
        /// </summary>
        /// <param name="NumberPhone"></param>
        /// <param name="Name"></param>
        public Subscriber(string numberphone, string name)
        {
            this.NumberPhone = numberphone;
            this.Name = name;
        }
    }
}
