using System.Drawing.Printing;
using Vending_Machine.Models;
using Vending_Machine.Models.Product;
using Vending_Machine.Models.Storage;

namespace Vending_Machine.Seller
{
    public class BeverageMachine: ISeller
    {
        private readonly Storage<Drink> _drinkStorage;
        private readonly Storage<Money> _moneyStorage;

        public BeverageMachine()
        {
            _drinkStorage = new Storage<Drink>();
            _moneyStorage = new Storage<Money>();
        }
        
        public bool Sell()
        {
            throw new System.NotImplementedException();
        }
    }
}