using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyUtility.DesignPatterns
{
    public class AbstractFactory
    {
        public interface IButton { }
        public interface IBorder { }

        public class WinButton : IButton { }
        public class MacButton : IButton { }

        public class WinBorder : IBorder { }
        public class MacBorder : IBorder { }

        public interface IGUIFactory
        {
            IButton CreateButton();

            IBorder CreateBorder();
        }

        public class WinFactory : IGUIFactory
        {
            public IBorder CreateBorder()
            {
                return new WinBorder();
            }

            public IButton CreateButton()
            {
                return new WinButton();
            }
        }

        public class MacFactory : IGUIFactory
        {
            public IBorder CreateBorder()
            {
                return new MacBorder();
            }

            public IButton CreateButton()
            {
                return new MacButton();
            }
        }

        public class Application
        {
            public void Start(IGUIFactory factory)
            {
                //The objects and their operations are rely on the factory passed to application
                //Provide an interface for creating families of related or dependent objects without specifying their concrete classes
                var button = factory.CreateButton();
                var border = factory.CreateBorder();
            }

        }

    }
}
