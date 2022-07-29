using System;
using System.Collections.Generic;


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

            /* 
            Subscriber NewContact = new Subscriber(numberphone, Name);       
                                                                        
            Console.Write("Введите телефонный номер:");      
            NewContact.NumberPhone  = Console.ReadLine();          
                                                             
            Console.Write("Введите имя:");                   
            NewContact.Name = Console.ReadLine();           
            */

            

            Console.Write("Введите телефонный номер:");
            string Nomer = Console.ReadLine();
              
            Console.Write("Введите имя:");
            string Name = Console.ReadLine();
                
            int index = Contacts.FindIndex(x => x.Contains(Name));
            if (index != -1) { Console.WriteLine("Имя <" + Name + "> уже есть в списке!"); }
            else { Contacts.Add(Nomer + "\t" + Name); }
        }

        /// <summary>
        /// Method of correcting a contact
        /// </summary>
        /// <param name="Contacts"></param>
        public void CorrectContact(List<string> Contacts)
        {
            Console.WriteLine("\n- Обновляем запись -");

            Console.Write("Введите имя:");
            string Name = Console.ReadLine();
            int index = Contacts.FindIndex(x => x.Contains(Name));
            if (index == -1) { Console.WriteLine("Имя <" + Name + "> отсутствует в списке!"); }
            else
            {   Console.Write("Введите телефонный номер:");
                string Nomer = Console.ReadLine();
                Contacts.RemoveAt(index);
                Contacts.Insert(index, Nomer + "\t" + Name);
            }
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
            if (index == -1) { Console.WriteLine("Имя <" + Name + "> отсутствует в списке!"); }
            else { Contacts.RemoveAt(index); }
        }
    }
}
