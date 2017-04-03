namespace Tasks.DesignPatterns
{
    internal class ProductContainer
    {
        public ProductContainer(string name, double price, uint position, uint quantity)
        {
            Position = position;
            Name = name;
            Price = price;
            Quantity = quantity;
        }

        public uint Position { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public uint Quantity { get; set; }
    }
}