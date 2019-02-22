using System;
using System.Collections.Generic;

namespace Vending_Machine.Models.Storage
{
    /// <summary>
    /// Хранилище объектов
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Storage<T>
    {
        //public ICollection<StorageCell<T>> Items { get; set; }

        public bool IncreaseItem(T item, int count) => true;

        public bool DecreaseItem(T item, int count) => true;

        public StorageCell<T> GetAll() => new StorageCell<T>();

        public int GetCount(T item) => 0;
        
        public T GetItem(int id) => throw new Exception();
    }
}