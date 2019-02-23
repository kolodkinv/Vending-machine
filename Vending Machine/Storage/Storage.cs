using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Vending_Machine.Exceptions;
using Vending_Machine.Repositories;

namespace Vending_Machine.Storage
{
    /// <summary>
    /// Хранилище объектов
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Storage<T> where T : class, ICount
    {
        private readonly IRepository<T> _repository;
        
        public Storage(IRepository<T> repository)
        {
            _repository = repository;
        }

        public void PutItem(T item)
        {
            item.Count = 1;
            _repository.Create(item);
        }

        public void RemoveItem(int id)
        {
            _repository.Delete(id);
        }
        
        public void IncreaseItem(int id, int count)
        {
            if (count > 0)
            {
                var item = GetItem(id);
                item.Count += count;
                _repository.Update(item);    
            }
            else
            {
                throw new ArgumentException("Количество добавляемого товара должно быть больше 0");
            }
        }

        public void DecreaseItem(int id, int count)
        {
            if (count > 0)
            {
                var item = GetItem(id);
                item.Count -= count;
                if (item.Count >= 0)
                {
                    _repository.Update(item);    
                }
                else
                {
                    throw new CountException($"Не хватает объектов типа {item.GetType()}");
                }
            }
            else
            {
                throw new ArgumentException("Количество удаляемого товара должно быть больше 0");
            }
        }

        public IEnumerable<T> GetAll()
        {
            return _repository.GetAll();
        }

        public int GetCount(int id)
        {
            var item = GetItem(id);
            return item.Count;
        }

        public T GetItem(int id)
        {
            var item = _repository.Get(id);
            if (item != null)
            {
                return item;
            }

            throw new NotFoundException($"Объект типа {typeof(T)} не найден");
        }
    }
}