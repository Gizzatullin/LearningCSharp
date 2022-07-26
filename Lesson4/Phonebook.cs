using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson4
{
    internal class Phonebook
    {

        /// <summary>
        /// Method of adding a contact
        /// </summary>
        /// <param name="Contacts"></param>
        public void AddContact(List<string> Contacts)
        {
            Console.WriteLine("\n- Добавляем контакт -");

            Console.Write("Введите телефонный номер:");
            string Nomer = Console.ReadLine();
            Console.Write("Введите имя:");
            string Name = Console.ReadLine();

            Contacts.Add(Nomer + "\t" + Name);
        }

        public void CorrectContact()
        {

        }

        /// <summary>
        /// Contact removal method
        /// </summary>
        /// <param name="Contacts"></param>
        public void DelContact(List<string> Contacts)
        {
            Console.WriteLine("\n- Удаляем контакт -");

            Console.Write("Введите имя, контакт которого вы желаете удалить:");
            string Name = Console.ReadLine();

            int index = Contacts.FindIndex(x => x.Contains(Name));
            Contacts.RemoveAt(index);
        }











    }
}
