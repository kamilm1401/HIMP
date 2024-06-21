using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.IO;



// Class created to support the management class in handling the program menu through methods

namespace HIMP
{
    abstract class MethodsForTheManagementClass
    {
        //Method called when no ID has been assigned within the program yet
        protected static private void EmptyList()
        {
            Console.Clear();
            Console.WriteLine("your list is empty");
            Console.WriteLine("Return to the main menu");
            Thread.Sleep(2000);
        }

        //Method called when the user-entered ID could not be found
        protected static private void IdNotExist()
        {
            Console.Clear();
            Console.WriteLine("The provided ID does not exist.");
            Console.WriteLine("Return to the main menu");
            Thread.Sleep(2000);
        }

        //Method used after displaying the values the user is looking for. They remain visible until the user finishes its operation by pressing a button.
        protected private static void PressKey()
        {
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
            Console.Clear();
        }

        



    }
}









   
