
namespace Lesson4
{
    internal class Subscriber
    {
        /// <summary>
        /// Field "Номер телефона" for read   
        /// </summary>
        private readonly string NumberPhone;


        /// <summary>
        /// Field "Имя" for read   
        /// </summary>
        private readonly string Name;


        /// <summary>
        /// Property "Subscriber" for logical operations
        /// </summary>
        /// <param name="Numberphone"></param>
        /// <param name="Name"></param>
        public Subscriber(string numberphone, string name)
        {
            this.NumberPhone = numberphone;
            this.Name = name;
        }
        
    }
}
