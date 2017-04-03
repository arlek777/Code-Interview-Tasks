namespace Tasks.DesignPatterns
{
    internal interface IMoneyService
    {
        double TotalSum { get; }
        void AcceptMoney(Money money);
        void GetChange(double requiredChange);
    }
}
