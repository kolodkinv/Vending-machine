namespace Vending_Machine.Models.Storage
{
    // Ячейка хранилища
    public class StorageCell<T>
    {
        public T Item { get; set; }
        public int Count { get; set; }
    }
}