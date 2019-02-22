using System.Drawing.Printing;
using Vending_Machine.Exceptions;
using Vending_Machine.Models;
using Vending_Machine.Models.Product;
using Vending_Machine.Models.Storage;

namespace Vending_Machine.Seller
{
    public class VendingMachine: ISeller
    {
        private readonly Storage<Product> _productStorage;
        private readonly Storage<Money> _moneyStorage;
        private readonly Basket _basket;
        
        public VendingMachine()
        {
            _productStorage = new Storage<Product>();
            _moneyStorage = new Storage<Money>();
            _basket = new Basket();
        }

        public void AddProductToBasket(int idProduct, int count = 1)
        {
            var product = _productStorage.GetItem(idProduct);
            if (product != null)
            {
                _basket.AddProducts(product, count); 
            }
            else
            {
                throw new NotFoundProductException("Добавляемый товар не найден");
            }
        }
        
        public double Sell()
        {
            var oddMoney = 0.0;
            if (_basket.IsCorrectPayment())
            {
                foreach (var position in _basket.Products)
                {
                    position.Deconstruct(out var product, out var count);
                    _productStorage.DecreaseItem(product, count);
                }

                foreach (var position in _basket.Moneys)
                {
                    position.Deconstruct(out var money, out var count);
                    _moneyStorage.IncreaseItem(money, count);
                }

                oddMoney = _basket.OddMoney;
            }

            return oddMoney;
        }

        public bool AddProduct(Product item, int count) => true;
        public bool RemoveProduct(Product item, int count) => true;
        public bool AddMoney(Money money, int count) => true;
        public bool RemoveMoney(Money money, int count) => true;
        public bool SetEnableMoney(Money money, bool enable) => true;
    }
}