using System.Collections.Generic;
using System.IO.Compression;
using Vending_Machine.Models;
using Vending_Machine.Models.Products;

namespace Vending_Machine.Seller
{
    public interface IBasketHandler
    {
        void AddMoney(Basket basket, Money money);
        void AddProducts(Basket basket, Product product);
        double GetTotalCost(Basket basket);
        bool IsCorrectPayment(Basket basket);
    }
    
    
    
    
}