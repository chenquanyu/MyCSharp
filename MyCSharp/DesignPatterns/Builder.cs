using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCSharp.DesignPatterns
{
    class Builder
    {
        class Pizza
        {
            public string Dough { get; set; }
            public string Sauce { get; set; }
            public string Topping { get; set; }
        }

        interface IPizzaBuilder
        {
            void BuildDough();
            void BuildSauce();
            void BuildTopping();
            Pizza GetResult();
        }

        class HawaiianPizzaBuilder : IPizzaBuilder
        {
            public void BuildDough()
            {
                throw new NotImplementedException();
            }

            public void BuildSauce()
            {
                throw new NotImplementedException();
            }

            public void BuildTopping()
            {
                throw new NotImplementedException();
            }

            public Pizza GetResult()
            {
                throw new NotImplementedException();
            }
        }

        class SpicyPizzaBuilder : IPizzaBuilder
        {
            public void BuildDough()
            {
                throw new NotImplementedException();
            }

            public void BuildSauce()
            {
                throw new NotImplementedException();
            }

            public void BuildTopping()
            {
                throw new NotImplementedException();
            }

            public Pizza GetResult()
            {
                throw new NotImplementedException();
            }
        }



    }
}
