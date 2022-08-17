using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Lesson4
{   
    /// <summary>
    /// Реализация функционала телефонной книжки в части чтения/записи данных
    /// в файл, добавление/корректировка/удаление абонента.
    /// </summary>
    internal class Phonebook
    {
        /// <summary>
        /// Определение события добавления абонента.
        /// </summary>
        public event Action AddAbonent;

        /// <summary>
        /// Определение события удаления абонента.
        /// </summary>
        public event Action RemoveAbonent;

        /// <summary>
        /// Определение события чтения информации из файла.
        /// </summary>
        public event Action ReadAbonentFromFile;

        /// <summary>
        /// Определение события записи информации в файл.
        /// </summary>
        public event Action WriteAbonentToFile;
        
        private const string phonebookFilename = "phonebook.txt";
        
        string filePath = Path.Combine(Environment.CurrentDirectory, phonebookFilename);
        
        private List<Subscriber> Contacts = new List<Subscriber>();

        /// <summary>
        /// Чтение информации из файла и формирование списка абонентов.
        /// </summary>
        public void ReadPhonebook()
        {
            if (File.Exists(filePath) == false)
            {
                Console.WriteLine("Файл с телефонным справочником не обнаружен, создан новый файл");
                FileStream fileStream = File.Open(filePath, FileMode.Create); // открываем файл (стираем содержимое файла)
                StreamWriter output = new StreamWriter(fileStream); // получаем поток
                output.Write(""); // записываем текст в поток
                output.Close();// закрываем поток
            }

            string[] lines = File.ReadAllLines(filePath);
                       
            for (int i = 0; i < lines.Length; i++)
            {
                Subscriber Spisok = new Subscriber();
                string[] linesSplit = lines[i].Split('\t');
                Spisok.NumberPhone = linesSplit[0];
                Spisok.Name = linesSplit[1];
                this.Contacts.Add(Spisok);
            }
           
            PhonebookInput(this.Contacts);
            
            this.ReadAbonentFromFile?.Invoke();
        }

        /// <summary>
        /// Запись информации со списком абонентов в txt-файл.
        /// </summary>
        /// <param name="phonebookInstance"></param>
        public void WritePhonebook(Phonebook phonebookInstance)
        {
            string[] lines = new string[this.Contacts.Count];
            
            for (int i = 0; i < this.Contacts.Count; i++)
            {
                lines[i] = this.Contacts.ElementAt(i).NumberPhone + "\t" + this.Contacts.ElementAt(i).Name;
                               
            }

            File.WriteAllLines(filePath, lines);

            this.WriteAbonentToFile?.Invoke();
            Console.ReadKey();
        }

        /// <summary>
        /// Вывод на экран информации со списком абонентов.
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
        /// Добавление абонента в список.
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

            this.AddAbonent?.Invoke();
        }

        /// <summary>
        /// Корректировка телефона абонента в списке.
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
        /// Удаление абонента из списка.
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
            
            this.RemoveAbonent?.Invoke();
        }
    }
}
