
namespace Lesson4
{
    internal class Subscriber
    {
        /// <summary>
        /// Создал поле "Номер телефона" как строковую с возможностью только считывать   
        /// </summary>
        private readonly string NumberPhone;


        /// <summary>
        /// Создал поле "Имя" как строковую с возможностью только считывать   
        /// </summary>
        private readonly string NamePhone;


        /// <summary>
        /// Создал свойство "Subscriber", чтобы совершать с ним логические операции
        /// </summary>
        /// <param name="Numberphone"></param>
        /// <param name="Namephone"></param>
        public Subscriber(string Numberphone, string Namephone)
        {
            this.NumberPhone = Numberphone;
            this.NamePhone = Namephone;
        }
        
    }
}
