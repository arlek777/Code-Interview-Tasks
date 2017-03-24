using System.Collections.Generic;

namespace Tasks.DesignPatterns
{
    internal interface IProductsService
    {
        void ReturnProduct(uint position, double money);
    }
}