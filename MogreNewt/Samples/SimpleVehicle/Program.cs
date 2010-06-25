using System;
using System.Collections.Generic;
using System.Text;
using Mogre;

namespace MogreNewt.Demo.SimpleVehicle
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                SimpleVehicleApp app = new SimpleVehicleApp();
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
