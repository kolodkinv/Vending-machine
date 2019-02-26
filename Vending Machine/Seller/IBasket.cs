using System.IO.Compression;
using Vending_Machine.Models;
using Vending_Machine.Models.Products;

namespace Vending_Machine.Seller
{
    public interface IBasket
    {
        void AddMoney(Money money);
        void AddProducts(Product product);
        double GetTotalCost();
        double GetOddMoney();
        bool IsCorrectPayment();
    }
    
    
    
    
}