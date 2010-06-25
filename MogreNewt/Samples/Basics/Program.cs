using System;
using System.Collections.Generic;
using System.Text;
using Mogre;

namespace MogreNewt.Demo.Basics
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Basics app = new Basics();
                app.Go();
            }
            catch
            {
                // Check if it's an Ogre Exception
                if (OgreException.IsThrown)
                    Mogre.Demo.ExampleApplication.Example.ShowOgreException();
                else
                    throw;
            }
        }
    }
}
