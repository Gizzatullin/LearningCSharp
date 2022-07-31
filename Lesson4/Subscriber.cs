namespace Lesson4
{
    internal class Subscriber
    {
        /// <summary>
        /// Field "Номер телефона"   
        /// </summary>
        public string NumberPhone;


        /// <summary>
        /// Field "Имя" for read   
        /// </summary>
        public string Name;

        public Subscriber()
        {
        }

        /// <summary>
        /// Property "Subscriber" for logical operations
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
