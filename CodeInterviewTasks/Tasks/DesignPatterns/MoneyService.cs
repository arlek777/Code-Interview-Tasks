using System;
using System.Collections.Generic;

namespace Tasks.DesignPatterns
{
    internal class MoneyService : IMoneyService
    {
        private readonly List<Money> _bank;

        public double TotalSum
        {
            get
            {
                var totalSum = 0.0;
                _bank.ForEach(m => totalSum += m.Value);
                return totalSum;
            }
        }

        public MoneyService(List<Money> initBank)
        {
            _bank = initBank;
        }

        public void AcceptMoney(Money money)
        {
            // validate money etc.
            _bank.Add(money);
        }

        public void GetChange(double requiredChange)
        {
            if(requiredChange > TotalSum) throw new ApplicationException("Can't give change, not enough money.");
            // try to get change with different type of coins is possible and remove them from the list
        }
    }
}