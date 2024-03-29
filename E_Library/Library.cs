﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace E_Library
{
    /// <summary>
    /// Реализация функций библиотеки: сохранение данных в json файл и добавление книги,
    /// удаление данных о книге из файла, корректировка данных о книге,
    /// считывание данных из файла, скачивание файла.
    /// </summary>
    public class Library
    {
        string filePath;
                 
        /// <summary>
        /// Сохранение данных в файл формата json и добавление книги.
        /// </summary>
        public void SavetoFile(Book book, bool FlagCorrect, string fileNameUser, string pathBook, string pdir)
        {
            List<Book> allCurrentBooks = ReadfromFile(fileNameUser, pathBook);

            int lastId = allCurrentBooks.Count == 0 ? 0 : allCurrentBooks.Last().Id;
            if (FlagCorrect == false) book.SetId(lastId + 1);
                        
            allCurrentBooks.Add(book);

            string serializedBooks = JsonConvert.SerializeObject(allCurrentBooks);
            filePath = Path.Combine(Environment.CurrentDirectory + $@"\BookFile\{pdir}\", fileNameUser);
            File.WriteAllText(filePath, serializedBooks);
        }

        /// <summary>
        /// перегрузка для сохранения данных в файл формата json.
        /// </summary>
        public void SavetoFile(List<Book> book, string fileNameUser, string pdir)
        {
            string serializedBooks = JsonConvert.SerializeObject(book);
            filePath = Path.Combine(Environment.CurrentDirectory + $@"\BookFile\{pdir}\", fileNameUser);
            File.WriteAllText(filePath, serializedBooks);
        }

        /// <summary>
        /// Удаление данных о книге из файла формата json по её ID.
        /// </summary>
        public bool DeletefromFile(int id, string fileNameUser, string pathBook, string pdir)
        {
            List<Book> allCurrentBooks = ReadfromFile(fileNameUser, pathBook);
            Book bookForDeletion = allCurrentBooks.FirstOrDefault(u => u.Id == id);
           
            bool result = false;
            
            if (bookForDeletion != null)
            {
                allCurrentBooks.Remove(bookForDeletion);
                
                for (int i = 0; i < allCurrentBooks.Count; i++)
                {
                    if (allCurrentBooks[i].Id > i + 1) allCurrentBooks[i].Id = i + 1;
                }

                SavetoFile(allCurrentBooks, fileNameUser, pdir);
                result = true;
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int IdFileForDownload(int id, string fileNameUser, string pathBook)
        {
            List<Book> allCurrentBooks = ReadfromFile(fileNameUser, pathBook);
            Book bookForDownload = allCurrentBooks.FirstOrDefault(u => u.Id == id);
            return bookForDownload.Id;
        }


        /// <summary>
        /// Корректировка данных о книге по её ID.
        /// </summary>
        public bool CorrectBookInfo(int id, string title, string author, string description, string genre, string filenamebook, string fileNameUser, string pathBook, string pdir)
        {
            List<Book> allCurrentBooks = ReadfromFile(fileNameUser, pathBook);
            Book bookForCorrect = allCurrentBooks.FirstOrDefault(u => u.Id == id);

            bool result = false;

            if (bookForCorrect != null)
            {
                bookForCorrect.Id = id;
                bookForCorrect.Title = title;
                bookForCorrect.Author = author;
                bookForCorrect.Description = description;
                bookForCorrect.Genre = genre;
                if (filenamebook != null) { bookForCorrect.FileNameBook = filenamebook; }

                SavetoFile(allCurrentBooks, fileNameUser, pdir);
                result = true;
            }

            return result;
        }

        /// <summary>
        /// Чтение данных из файла формата json.
        /// </summary>
        public List<Book> ReadfromFile(string fileNameUser, string pathBook)
        {
            filePath = Path.Combine(pathBook, fileNameUser);

            if (File.Exists(filePath) == false)
            {
                var file = File.Create(filePath);
                file.Close();
            }           
            string json = File.ReadAllText(filePath);
            List<Book> currentBooks = JsonConvert.DeserializeObject<List<Book>>(json);
            return currentBooks ?? new List<Book>();
        }
    }
}
