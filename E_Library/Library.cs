﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace E_Library
{
    /// <summary>
    /// Реализация функций библиотеки: считывание данных, просмотр по фильтру,
    /// добавление книги, редактирование параметров книги, скачивание файла.
    /// </summary>
    internal class Library
    {
        const string fileName = "BookLibraryCollection.txt";
        string filePath = Path.Combine(Environment.CurrentDirectory, fileName);
         
        
        /// <summary>
        /// Сохранение данных в файл формата json.
        /// </summary>
        public void SavetoFile(Book book, bool FlagCorrect)
        {
            List<Book> allCurrentBooks = ReadfromFile();

            int lastId = allCurrentBooks.Count == 0 ? 0 : allCurrentBooks.Last().Id;
            if (FlagCorrect == false) book.SetId(lastId + 1);
                        
            allCurrentBooks.Add(book);

            string serializedBooks = JsonConvert.SerializeObject(allCurrentBooks);
            File.WriteAllText(filePath, serializedBooks);
        }

        /// <summary>
        /// перегрузка для сохранения данных в файл формата json.
        /// </summary>
        public void SavetoFile(List<Book> book)
        {
            string serializedBooks = JsonConvert.SerializeObject(book);
            File.WriteAllText(filePath, serializedBooks);
        }


        /// <summary>
        /// Удаление данных о книге из файла формата json по её ID.
        /// </summary>
        public bool DeletefromFile(int id)
        {
            List<Book> allCurrentBooks = ReadfromFile();
            Book bookForDeletion = allCurrentBooks.FirstOrDefault(u => u.Id == id);
           
            bool result = false;
            
            if (bookForDeletion != null)
            {
                allCurrentBooks.Remove(bookForDeletion);
                
                for (int i = 0; i < allCurrentBooks.Count; i++)
                {
                    if (allCurrentBooks[i].Id > i + 1) allCurrentBooks[i].Id = i + 1;
                }

                SavetoFile(allCurrentBooks);
                result = true;
            }

            return result;
        }

        /// <summary>
        /// Корректировка данных о книге по её ID.
        /// </summary>
        public bool CorrectBookInfo(int id, string title, string author, string description, string genre, string filenamebook)
        {
            List<Book> allCurrentBooks = ReadfromFile();
            Book bookForCorrect = allCurrentBooks.FirstOrDefault(u => u.Id == id);

            bool result = false;

            if (bookForCorrect != null)
            {
                bookForCorrect.Id = id;
                bookForCorrect.Title = title;
                bookForCorrect.Author = author;
                bookForCorrect.Description = description;
                bookForCorrect.Genre = genre;
                bookForCorrect.FileNameBook = filenamebook;

                SavetoFile(allCurrentBooks);
                result = true;
            }

            return result;
        }











        /// <summary>
        /// Чтение данных из файла формата json.
        /// </summary>
        public List<Book> ReadfromFile()
        {
            if (File.Exists(filePath) == false)
            {
                var file = File.Create(filePath);
                file.Close();
            }           
            string json = File.ReadAllText(filePath);
            List<Book> currentBooks = JsonConvert.DeserializeObject<List<Book>>(json);
            return currentBooks ?? new List<Book>();
        }


        /// <summary>
        /// Вывод информации пользователю о содержании библиотеки.
        /// </summary>
        public void OutputContentLibrary()
        {

        }


        /// <summary>
        /// Добавление книги в библиотеку.
        /// </summary>
        public void AddBookInLibrary()
        {

        }


        /// <summary>
        /// Коррекция информации о книге.
        /// </summary>
        public void CorrectBookInfo()
        {

        }


        /// <summary>
        /// Загрузка файла книги.
        /// </summary>
        public void DownloadBookFile()
        {

        }

    }
}
