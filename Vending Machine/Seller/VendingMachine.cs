using System.Drawing.Printing;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Vending_Machine.Exceptions;
using Vending_Machine.Models;
using Vending_Machine.Models.Product;
using Vending_Machine.Storage;

namespace Vending_Machine.Seller
{
    public class VendingMachine: ISeller
    {
        private readonly Storage<Product> _productStorage;
        private readonly Storage<Money> _moneyStorage;
        private readonly Basket _basket;
        
        public VendingMachine(Storage<Product> productStorage, Storage<Money> moneyStorage)
        {
            _productStorage = productStorage;
            _moneyStorage = moneyStorage;
            _basket = new Basket();
        }

        public void AddMoneyToBasket(int idMoney, int count = 1)
        {
            var money = _moneyStorage.GetItem(idMoney);
            if (money != null)
            {
                if (money.Enable)
                {
                    _basket.AddMoney(money, count);    
                }
                else
                {
                    throw new BlockedMoneyException("Данный вид денег заблокирован");
                }
            }
            else
            {
                throw new NotFoundException("Данный вид денег не найден");
            }
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
                throw new NotFoundException("Добавляемый товар не найден");
            }
        }
        
        /// <summary>
        /// Купить текущее содержимое корзины
        /// </summary>
        /// <returns></returns>
        public double Sell()
        {
            var oddMoney = 0.0;
            if (_basket.IsCorrectPayment())
            {
                foreach (var position in _basket.Products)
                {
                    position.Deconstruct(out var product, out var count);
                    _productStorage.DecreaseItem(product.Id, count);
                }

                foreach (var position in _basket.Moneys)
                {
                    position.Deconstruct(out var money, out var count);
                    _moneyStorage.IncreaseItem(money.Id, count);
                }

                oddMoney = _basket.OddMoney;
            }

            return oddMoney;
        }
    }
}