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
                         
            Subscriber NewContact = new Subscriber();       
                                                                        
            Console.Write("Введите телефонный номер:");      
            NewContact.NumberPhone  = Console.ReadLine();          
                                                             
            Console.Write("Введите имя:");                   
            NewContact.Name = Console.ReadLine();

            int index = Contacts.FindIndex(x => x.Contains(NewContact.Name));
            if (index != -1) { Console.WriteLine("Имя <" + NewContact.Name + "> уже есть в списке!"); }
            else { Contacts.Add(NewContact.NumberPhone + "\t" + NewContact.Name); }
        }

        /// <summary>
        /// Method of correcting a contact
        /// </summary>
        /// <param name="Contacts"></param>
        public void CorrectContact(List<string> Contacts)
        {
            Console.WriteLine("\n- Обновляем запись -");

            Subscriber CorrectContact = new Subscriber();

            Console.Write("Введите имя:");
            CorrectContact.Name = Console.ReadLine();
            int index = Contacts.FindIndex(x => x.Contains(CorrectContact.Name));
            if (index == -1) { Console.WriteLine("Имя <" + CorrectContact.Name + "> отсутствует в списке!"); }
            else
            {   Console.Write("Введите телефонный номер:");
                CorrectContact.NumberPhone = Console.ReadLine();
                Contacts.RemoveAt(index);
                Contacts.Insert(index, CorrectContact.NumberPhone + "\t" + CorrectContact.Name);
            }
        }

        /// <summary>
        /// Contact removal method
        /// </summary>
        /// <param name="Contacts"></param>
        public void DelContact(List<string> Contacts)
        {
            Console.WriteLine("\n- Удаляем контакт -");

            Subscriber DelContact = new Subscriber();

            Console.Write("Введите имя, контакт которого вы желаете удалить:");
            DelContact.Name = Console.ReadLine();

            int index = Contacts.FindIndex(x => x.Contains(DelContact.Name));
            if (index == -1) { Console.WriteLine("Имя <" + DelContact.Name + "> отсутствует в списке!"); }
            else { Contacts.RemoveAt(index); }
        }
    }
}
