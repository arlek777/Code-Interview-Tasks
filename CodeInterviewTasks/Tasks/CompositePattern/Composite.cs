using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tasks.CompositePattern
{
    /// <summary>
    /// Composite pattern is used to treat a tree structure of objects as a uniform object.
    /// </summary>
    public abstract class Figure
    {
        public abstract void Draw();
        public abstract void Drag();
    }

    public class PrimitiveFigure : Figure
    {
        private readonly string _name;

        public PrimitiveFigure(string name)
        {
            _name = name;
        }

        public override void Draw()
        {
            Console.WriteLine("Primitive is drawing..: " + _name);
        }

        public override void Drag()
        {
            Console.WriteLine("Primitive is draging..: " + _name);
        }
    }

    public class CompositeFigure : Figure
    {
        private readonly List<Figure> _figures = new List<Figure>();
        private readonly string _name;

        public CompositeFigure(string name)
        {
            _name = name;
        }

        public void AddFigure(Figure figure)
        {
            _figures.Add(figure);
        }

        public void RemoveFigure(Figure figure)
        {
            _figures.Remove(figure);
        }

        public override void Draw()
        {
            Console.WriteLine("Composite is drawing..: " + _name);

            foreach (var figure in _figures)
            {
                figure.Draw();
            }
        }

        public override void Drag()
        {
            Console.WriteLine("Composite is draging..: " + _name);

            foreach (var figure in _figures)
            {
                figure.Drag();
            }
        }
    }

    public static class Program
    {
        public static void Main(string[] args)
        {
            var figure = new CompositeFigure("Square");
            figure.AddFigure(new PrimitiveFigure("Triangle"));
            figure.AddFigure(new PrimitiveFigure("Circle"));

            var composite = new CompositeFigure("Triangle");
            composite.AddFigure(new PrimitiveFigure("Circle"));

            figure.AddFigure(composite);

            figure.Draw();
            figure.Drag();
        }
    }
}
