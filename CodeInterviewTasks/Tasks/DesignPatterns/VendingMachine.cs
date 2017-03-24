using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.DesignPatterns
{
    internal abstract class State
    {
        public double CurrentSum { get; set; }
        public uint ProductPosition { get; set; }
        public string DisplayMessage { get; set; }

        protected readonly VendingMachine VendingMachine;

        protected State(VendingMachine vendingMachine)
        {
            VendingMachine = vendingMachine;
        }

        public abstract void InsertMoney(Money money);
        public abstract void SelectProduct(uint position);
        public abstract void GetChange();
        public abstract void ReturnProduct();

    }

    internal class OutOfOrderState : State
    {
        public OutOfOrderState(VendingMachine vendingMachine, State state) : base(vendingMachine)
        {
            DisplayMessage = "Out of Order.";
        }

        public override void InsertMoney(Money money)
        {
            throw new ApplicationException("Out of order.");
        }

        public override void SelectProduct(uint position)
        {
            throw new ApplicationException("Out of order.");
        }

        public override void GetChange()
        {
            throw new ApplicationException("Out of order.");
        }

        public override void ReturnProduct()
        {
            throw new ApplicationException("Out of order.");
        }
    }

    internal class FreeState : State
    {
        private readonly IMoneyService _moneyService;
        private readonly IProductsService _productsService;

        public FreeState(VendingMachine vendingMachine, IMoneyService moneyService, IProductsService productsService) : base(vendingMachine)
        {
            CurrentSum = 0.0;
            ProductPosition = 0;
            _moneyService = moneyService;
            _productsService = productsService;
            DisplayMessage = "Welcome, please insert money...";
        }

        public override void InsertMoney(Money money)
        {
            _moneyService.AcceptMoney(money);
            CurrentSum += money.Value;
            DisplayMessage = "Sum: " + CurrentSum;
        }

        public override void SelectProduct(uint position)
        {
            if (CurrentSum != 0)
            {
                ProductPosition = position;
                base.VendingMachine.State = new SelectProductState(base.VendingMachine, this, _moneyService, _productsService);
            }
        }

        public override void GetChange()
        {
            if (CurrentSum != 0)
            {
                _moneyService.GetChange(CurrentSum);
                DisplayMessage = "Welcome, please insert money...";
            }
        }

        public override void ReturnProduct()
        {
        }
    }

    internal class SelectProductState : State
    {
        private readonly IMoneyService _moneyService;
        private readonly IProductsService _productsService;

        public SelectProductState(VendingMachine vendingMachine, State state, IMoneyService moneyService, IProductsService productsService) : base(vendingMachine)
        {
            _moneyService = moneyService;
            _productsService = productsService;
            CurrentSum = state.CurrentSum;
            ProductPosition = state.ProductPosition;
        }

        public override void InsertMoney(Money money)
        {
        }

        public override void SelectProduct(uint position)
        {
        }

        public override void GetChange()
        {
            if (CurrentSum != 0)
            {
                _moneyService.GetChange(CurrentSum);
                base.VendingMachine.State = new FreeState(base.VendingMachine, _moneyService, _productsService);
            }
        }

        public override void ReturnProduct()
        {
            try
            {
                _productsService.ReturnProduct(ProductPosition, CurrentSum);
                CurrentSum -= 10; // minus real product sum
                base.VendingMachine.State = new ReturnProductState(base.VendingMachine, this, _moneyService, _productsService);
            }
            catch
            {
                DisplayMessage = "Error";
            }
        }
    }

    internal class VendingMachine
    {
        public State State { get; set; }

        public VendingMachine()
        {
            State = new FreeState();
        }

        public void InsertMoney()
        {
            State.InsertMoney(new Money());
        }

        public void SelectProduct()
        {
            State.SelectProduct(1);
        }

        public void GetChange()
        {
            State.GetChange();
        }

        public void ReturnProduct()
        {
            State.ReturnProduct();
        }
    }
}
