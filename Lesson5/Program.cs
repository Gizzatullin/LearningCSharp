using System;

namespace Lesson5
{
    public class MyList<T>
    {
        private T[] _array = Array.Empty<T>();

       public T this[int index]
        {
            get { return _array[index]; }
            set { _array[index] = value; }
        }
        public int Count
        {   get
            { return _array.Length; }
        }

        /// <summary>
        /// Добавление элемента в список
        /// </summary>
        /// <param name="value"></param>
        public void Add(T value)
        {   var newArray = new T[this.Count + 1];
            for (int i = 0; i < this.Count; i++)
            {
                newArray[i] = _array[i];
            }
            newArray[this.Count] = value;

            _array = newArray;
        }

        /// <summary>
        /// Удаление элемента по индексу
        /// </summary>
        public void Delete(int iElement)
        {
            var newArray = new T[this.Count-1];
            for (int i = 0; i < newArray.Length; i++)
            {   
                if (i < iElement)
                {
                    newArray[i] = _array[i];
                }
                else
                {
                    newArray[i] = _array[i+1];
                }
            }
            _array = newArray;
        }               
    }

    /// <summary>
    /// Задание про динамический список
    /// </summary>
    internal class Program
    {   
        /// <summary>
        /// Метод вывода списка на экран
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="myList"></param>
        static void InputList<T>(MyList<T> myList)
        {
            Console.WriteLine("-Текущий список-");
            for (int i = 0; i < myList.Count; i++)
            {
                Console.WriteLine(myList[i]);
            }
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            MyList<int> myList = new MyList<int>();
            myList.Add(5);
            myList.Add(6);
            myList.Add(7);
            myList.Add(8);
            InputList(myList);
                                          
            Console.WriteLine("Удаляем 2-й элемент списка");
            myList.Delete(2);
            InputList(myList);
            
            Console.WriteLine("\nТекущая длина массива: " + myList.Count);
            Console.ReadKey();
        }
    }
}