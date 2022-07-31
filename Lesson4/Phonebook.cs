using System;
using System.Collections.Generic;
using System.IO;


namespace Lesson4
{
    internal class Phonebook
    {

        /// <summary>
        /// Method of reading a text file and getting a list of contacts
        /// </summary>
        static void ReadPhonebook()
        {
            const string FileName = "phonebook.txt";
            List<string> Contacts = new List<string>();

            string FilePath = Path.Combine(Environment.CurrentDirectory, FileName);
            string[] lines = File.ReadAllLines(FilePath);

            for (int i = 0; i < lines.Length; i++)
            {
                Contacts.Add(lines[i]);
            }
        }

        /// <summary>
        /// Method of adding a contact
        /// </summary>
        /// <param name="Contacts"></param>
        public void AddContact(List<string> Contacts, string NumberPhone, string Name)
        {
            Subscriber NewContact = new Subscriber();
            NewContact.NumberPhone = NumberPhone;
            NewContact.Name = Name;

            int index = Contacts.FindIndex(x => x.Contains(NewContact.Name));
            if (index != -1) { Console.WriteLine("Имя <" + NewContact.Name + "> уже есть в списке!"); }
            else { Contacts.Add(NewContact.NumberPhone + "\t" + NewContact.Name); }
        }

        /// <summary>
        /// Method of correcting a contact
        /// </summary>
        /// <param name="Contacts"></param>
        public void CorrectContact(List<string> Contacts, string Name)
        {
            Subscriber CorrectContact = new Subscriber();
            CorrectContact.Name = Name;
            
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
        public void DelContact(List<string> Contacts, string Name)
        {
            Subscriber DelContact = new Subscriber();
            DelContact.Name = Name;

            int index = Contacts.FindIndex(x => x.Contains(DelContact.Name));
            if (index == -1) { Console.WriteLine("Имя <" + DelContact.Name + "> отсутствует в списке!"); }
            else { Contacts.RemoveAt(index); }
        }
    }
}
