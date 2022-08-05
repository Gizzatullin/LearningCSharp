using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Lesson4
{
    internal class Phonebook
    {
        const string FileName = "phonebook.txt";
        string FilePath = Path.Combine(Environment.CurrentDirectory, FileName);
        private List<Subscriber> Contacts = new List<Subscriber>();

        /// <summary>
        /// Method of reading a text file and getting a list of contacts
        /// </summary>
        public void ReadPhonebook()
        {
            if (File.Exists(FilePath) == false)
            {
                Console.WriteLine("Файл с телефонным справочником не обнаружен, создан новый файл");
                FileStream fileStream = File.Open(FilePath, FileMode.Create); // открываем файл (стираем содержимое файла)
                StreamWriter output = new StreamWriter(fileStream); // получаем поток
                output.Write(""); // записываем текст в поток
                output.Close();// закрываем поток
            }

            string[] lines = File.ReadAllLines(FilePath);
                       
            for (int i = 0; i < lines.Length; i++)
            {
                Subscriber Spisok = new Subscriber();
                string[] linesSplit = lines[i].Split('\t');
                Spisok.NumberPhone = linesSplit[0];
                Spisok.Name = linesSplit[1];
                this.Contacts.Add(Spisok);
            }

            PhonebookInput(this.Contacts);

        }

        /// <summary>
        /// Method of writing a contacts in txt-file
        /// </summary>
        /// <param name="phonebookInstance"></param>
        public void WritePhonebook(Phonebook phonebookInstance)
        {
            string lineNumperPhone, lineName, l;
            string[] lines = new string[this.Contacts.Count];
            
            for (int i = 0; i < this.Contacts.Count; i++)
            {
                lines[i] = this.Contacts.ElementAt(i).NumberPhone + "\t" + this.Contacts.ElementAt(i).Name;
                               
            }

            File.WriteAllLines(FilePath, lines);
        }

        /// <summary>
        /// Method of input Phonebook to screen
        /// </summary>
        /// <param name="Contacts"></param>
        private void PhonebookInput(List<Subscriber> Contacts)
        {
            Console.WriteLine("\n-Телефонная книжка- ");
            foreach (Subscriber Contact in this.Contacts)
            {
                Console.WriteLine(Contact.NumberPhone + '\t' + Contact.Name);
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Method of adding a contact
        /// </summary>
        /// <param name="phonebookInstance"></param>
        /// <param name="NumberPhone"></param>
        /// <param name="Name"></param>
        public void AddContact(Phonebook phonebookInstance, string NumberPhone, string Name)
        {
            Subscriber NewContact = new Subscriber();
            NewContact.NumberPhone = NumberPhone;
            NewContact.Name = Name;

            int index = this.Contacts.FindIndex(s => string.Equals(s.Name, NewContact.Name, StringComparison.CurrentCultureIgnoreCase));
            if (index != -1) { Console.WriteLine("Имя <" + NewContact.Name + "> уже есть в списке!"); }
            else { this.Contacts.Add(NewContact); }

            PhonebookInput(this.Contacts);
        }

        /// <summary>
        /// Method of correcting a contact
        /// </summary>
        /// <param name="phonebookInstance"></param>
        /// <param name="Name"></param>
        public void CorrectContact(Phonebook phonebookInstance, string Name)
        {
            Subscriber CorrectContact = new Subscriber();
            CorrectContact.Name = Name;

            int index = this.Contacts.FindIndex(s => string.Equals(s.Name, CorrectContact.Name, StringComparison.CurrentCultureIgnoreCase));
            if (index == -1) { Console.WriteLine("Имя <" + CorrectContact.Name + "> отсутствует в списке!"); }
            else
            {   Console.Write("Введите телефонный номер:");
                CorrectContact.NumberPhone = Console.ReadLine();
                this.Contacts.RemoveAt(index);
                this.Contacts.Insert(index, CorrectContact);
            }

            PhonebookInput(this.Contacts);
        }

        /// <summary>
        /// Contact removal method
        /// </summary>
        /// <param name="phonebookInstance"></param>
        /// <param name="Name"></param>
        public void DelContact(Phonebook phonebookInstance, string Name)
        {
            Subscriber DelContact = new Subscriber();
            DelContact.Name = Name;

            int index = this.Contacts.FindIndex(s => string.Equals(s.Name, DelContact.Name, StringComparison.CurrentCultureIgnoreCase));
            if (index == -1) { Console.WriteLine("Имя <" + DelContact.Name + "> отсутствует в списке!"); }
            else { this.Contacts.RemoveAt(index); }

            PhonebookInput(this.Contacts);
        }
    }
}
