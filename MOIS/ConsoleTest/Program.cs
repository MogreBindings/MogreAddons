using System;
using System.Collections.Generic;
using System.Text;
using MOIS;

namespace ConsoleTest
{
    class Program
    {
        static string[] g_DeviceType = {"OISUnknown", "OISKeyboard", "OISMouse", "OISJoyStick",
							 "OISTablet", "OISOther"};
        static InputManager g_InputManager;
        static Keyboard g_kb;
        static Mouse g_m;
        static JoyStick[] g_joys;
        static bool appRunning = true;	//Global Exit Flag

        static void DoStartup()
        {
            ParamList pl = new ParamList();
            Form1 form = new Form1();
            form.Show();
            pl.Insert("WINDOW", form.Handle.ToString());

            //Default mode is foreground exclusive..but, we want to show mouse - so nonexclusive
            pl.Insert("w32_mouse", "DISCL_FOREGROUND");
            pl.Insert("w32_mouse", "DISCL_NONEXCLUSIVE");

            //This never returns null.. it will raise an exception on errors
            g_InputManager = InputManager.CreateInputSystem(pl);

            uint v = InputManager.VersionNumber;
            Console.WriteLine("OIS Version: " + (v >> 16) + "." + ((v >> 8) & 0x000000FF) + "." + (v & 0x000000FF)
                + "\n\tRelease Name: " //+ InputManager.VersionName
                + "\n\tPlatform: " + g_InputManager.InputSystemName()
                + "\n\tNumber of Mice: " + g_InputManager.GetNumberOfDevices(MOIS.Type.OISMouse)
                + "\n\tNumber of Keyboards: " + g_InputManager.GetNumberOfDevices(MOIS.Type.OISKeyboard)
                + "\n\tNumber of Joys/Pads = " + g_InputManager.GetNumberOfDevices(MOIS.Type.OISJoyStick));

            //List all devices
            DeviceList list = g_InputManager.ListFreeDevices();
            foreach (KeyValuePair<MOIS.Type, string> pair in list)
                Console.WriteLine("\n\tDevice: " + g_DeviceType[(int)pair.Key] + " Vendor: " + pair.Value);

            g_kb = (Keyboard)g_InputManager.CreateInputObject(MOIS.Type.OISKeyboard, true);
            g_kb.KeyPressed += new KeyListener.KeyPressedHandler(KeyPressed);
            g_kb.KeyReleased += new KeyListener.KeyReleasedHandler(KeyReleased);

            g_m = (Mouse)g_InputManager.CreateInputObject(MOIS.Type.OISMouse, true);
            g_m.MouseMoved += new MouseListener.MouseMovedHandler(MouseMoved);
            g_m.MousePressed += new MouseListener.MousePressedHandler(MousePressed);
            g_m.MouseReleased += new MouseListener.MouseReleasedHandler(MouseReleased);

            MouseState_NativePtr ms = g_m.MouseState;
            ms.width = form.Width;
            ms.height = form.Height;

            //This demo only uses at max 4 joys
            int numSticks = g_InputManager.GetNumberOfDevices(MOIS.Type.OISJoyStick);
            if (numSticks > 4) numSticks = 4;

            g_joys = new JoyStick[numSticks];

            for (int i = 0; i < numSticks; ++i)
            {
                g_joys[i] = (JoyStick)g_InputManager.CreateInputObject(MOIS.Type.OISJoyStick, true);
                g_joys[i].AxisMoved += new JoyStickListener.AxisMovedHandler(AxisMoved);
                g_joys[i].ButtonPressed += new JoyStickListener.ButtonPressedHandler(JoyButtonPressed);
                g_joys[i].ButtonReleased += new JoyStickListener.ButtonReleasedHandler(JoyButtonReleased);
                g_joys[i].PovMoved += new JoyStickListener.PovMovedHandler(PovMoved);
                g_joys[i].Vector3Moved += new JoyStickListener.Vector3MovedHandler(Vector3Moved);
            }
        }

        static bool Vector3Moved(JoyStickEvent arg, int index)
        {
            Vector3 vec = arg.state.GetVector(index);
            Console.Write("\n" + arg.device.Vendor() + ". Orientation # " + index
                + " X Value: " + vec.x
                + " Y Value: " + vec.y
                + " Z Value: " + vec.z);
            return true;
        }

        static bool PovMoved(JoyStickEvent arg, int pov)
        {
            Console.Write("\n" + arg.device.Vendor() + ". POV" + pov + " ");

            if ((arg.state.get_mPOV(pov).direction & Pov_NativePtr.North) != 0) //Going up
                Console.Write("North");
            else if ((arg.state.get_mPOV(pov).direction & Pov_NativePtr.South) != 0) //Going down
                Console.Write("South");

            if ((arg.state.get_mPOV(pov).direction & Pov_NativePtr.East) != 0) //Going right
                Console.Write("East");
            else if ((arg.state.get_mPOV(pov).direction & Pov_NativePtr.West) != 0) //Going left
                Console.Write("West");

            if (arg.state.get_mPOV(pov).direction == Pov_NativePtr.Centered) //stopped/centered out
                Console.Write("Centered");
            return true;
        }

        static bool JoyButtonReleased(JoyStickEvent arg, int button)
        {
            Console.Write("\n" + arg.device.Vendor() + ". Button Released # " + button);
            return true;
        }

        static bool JoyButtonPressed(JoyStickEvent arg, int button)
        {
            Console.Write("\n" + arg.device.Vendor() + ". Button Pressed # " + button);
            return true;
        }

        static bool AxisMoved(JoyStickEvent arg, int axis)
        {
            //Provide a little dead zone
            Axis_NativePtr axiscls = arg.state.GetAxis(axis);
            if (axiscls.abs > 2500 || axiscls.abs < -2500)
                Console.Write("\n" + arg.device.Vendor() + ". Axis # " + axis + " Value: " + axiscls.abs);
            return true;
        }

        static bool MouseReleased(MouseEvent arg, MouseButtonID id)
        {
            MouseState_NativePtr s = arg.state;
            Console.Write("\nMouse button #" + id + " released. Abs("
                      + s.X.abs + ", " + s.Y.abs + ", " + s.Z.abs + ") Rel("
                      + s.X.rel + ", " + s.Y.rel + ", " + s.Z.rel + ")");
            return true;
        }

        static bool MousePressed(MouseEvent arg, MouseButtonID id)
        {
            MouseState_NativePtr s = arg.state;
            Console.Write("\nMouse button #" + id + " pressed. Abs("
                      + s.X.abs + ", " + s.Y.abs + ", " + s.Z.abs + ") Rel("
                      + s.X.rel + ", " + s.Y.rel + ", " + s.Z.rel + ")");
            return true;
        }

        static bool MouseMoved(MouseEvent arg)
        {
            MouseState_NativePtr s = arg.state;
            Console.Write("\nMouseMoved: Abs("
                      + s.X.abs + ", " + s.Y.abs + ", " + s.Z.abs + ") Rel("
                      + s.X.rel + ", " + s.Y.rel + ", " + s.Z.rel + ")");
            return true;
        }

        static bool KeyReleased(KeyEvent arg)
        {
            if (arg.key == KeyCode.KC_ESCAPE || arg.key == KeyCode.KC_Q)
                appRunning = false;
            return true;
        }

        static bool KeyPressed(KeyEvent arg)
        {
            Console.WriteLine("\nKeyPressed {" + arg.key
                + ", " + ((Keyboard)(arg.device)).GetAsString(arg.key)
                + "} || Character (" + (char)arg.text + ")");
            return true;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("\n\n*** OIS Console Demo App is starting up... ***");
            try
            {
                DoStartup();
                Console.WriteLine("\nStartup done... Hit 'q' or ESC to exit (or joy button 1)\n");

                while (appRunning)
                {
                    System.Threading.Thread.Sleep(30);
                    System.Windows.Forms.Application.DoEvents();

                    if (g_kb != null)
                    {
                        g_kb.Capture();
                        if (!g_kb.Buffered())
                            handleNonBufferedKeys();
                    }

                    if (g_m != null)
                    {
                        g_m.Capture();
                        if (!g_m.Buffered())
                            handleNonBufferedMouse();
                    }

                    for (int i = 0; i < g_joys.Length; ++i)
                    {
                        if (g_joys[i] != null)
                        {
                            g_joys[i].Capture();
                            if (!g_joys[i].Buffered())
                                handleNonBufferedJoy(g_joys[i]);
                        }
                    }
                }
            }
            catch (System.Runtime.InteropServices.SEHException)
            {
                if (MOIS.OISException.LastException != null)
                    System.Windows.Forms.MessageBox.Show(MOIS.OISException.LastException.eText, "An exception has occured!", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                else
                    throw;
            }

            if (g_InputManager != null)
            {
                g_InputManager.DestroyInputObject(g_kb);
                g_InputManager.DestroyInputObject(g_m);

                for (int i = 0; i < g_joys.Length; ++i)
                    g_InputManager.DestroyInputObject(g_joys[i]);

                InputManager.DestroyInputSystem(g_InputManager);
            }

            Console.WriteLine("\n\nGoodbye\n");
        }


        static void handleNonBufferedKeys()
        {
            if (g_kb.IsKeyDown(KeyCode.KC_ESCAPE) || g_kb.IsKeyDown(KeyCode.KC_Q))
                appRunning = false;

            if (g_kb.IsModifierDown(Keyboard.Modifier.Shift))
                Console.WriteLine("Shift is down..");
            if (g_kb.IsModifierDown(Keyboard.Modifier.Alt))
                Console.WriteLine("Alt is down..");
            if (g_kb.IsModifierDown(Keyboard.Modifier.Ctrl))
                Console.WriteLine("Ctrl is down..");
        }

        static void handleNonBufferedMouse()
        {
            //Just dump the current mouse state
            MouseState_NativePtr ms = g_m.MouseState;
            Console.Write("\nMouse: Abs(" + ms.X.abs + " " + ms.Y.abs + " " + ms.Z.abs
                + ") B: " + ms.buttons + " Rel(" + ms.X.rel + " " + ms.Y.rel + " " + ms.Z.rel + ")");
        }

        static void handleNonBufferedJoy(JoyStick js)
        {
            //Just dump the current joy state
            JoyStickState_NativePtr joy = js.JoyStickState;
            for (int i = 0; i < joy.AxisCount; ++i)
                Console.Write("\nAxis " + i + " X: " + joy.GetAxis(i).abs);
        }
    }
}
