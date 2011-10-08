using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Mogre;
using MogreFreeSL;

namespace MogreFreeSLDemo
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            OgreStartup ogre = new OgreStartup();
            ogre.Go();
        }
    }
    
    class OgreStartup
    {
        SoundManager smgr = SoundManager.Instance;
        SoundEntity soundEntity;
        AmbientSound ambientSound;

        #region Global Variables

        int position = -1;

        #region General

        bool running = true;
        float deltaTime;
        float totalTime;

        #endregion

        #region Rendering (MOGRE)

        Camera cam;
        Root root = null;
        SceneManager mgr;
        RenderWindow window;

        #endregion

        #region Input (MOIS)

        MOIS.InputManager inputManager;
        MOIS.Keyboard inputKeyboard;
        MOIS.Mouse inputMouse;

        #endregion

        #endregion

        #region Setup

        #region Set Up Mogre

        /// <summary>
        /// Runs the Mogre setup routine.
        /// </summary>
        public void Go()
        {
            CreateRoot();
            DefineResources();
            SetupRenderSystem();
            CreateRenderWindow();
            InitializeResourceGroups();
            CreateScene();
            CreateEventHandlers();
            StartRenderLoop();
        }

        /// <summary>
        /// Creates the root object.
        /// </summary>
        void CreateRoot()
        {
            root = new Root();
        }

        /// <summary>
        /// Defines resources needed for the game.
        /// </summary>
        void DefineResources()
        {
            ConfigFile configFile = new ConfigFile();
            configFile.Load("resources.cfg", "\t:=", true);
            ConfigFile.SectionIterator seci = configFile.GetSectionIterator();
            String secName, typeName, archName;

            while (seci.MoveNext())
            {
                secName = seci.CurrentKey;
                ConfigFile.SettingsMultiMap settings = seci.Current;
                foreach (KeyValuePair<string, string> pair in settings)
                {
                    typeName = pair.Key;
                    archName = pair.Value;
                    ResourceGroupManager.Singleton.AddResourceLocation(archName, typeName, secName);
                }
            }
        }

        /// <summary>
        /// Readies the rendering system.
        /// </summary>
        void SetupRenderSystem()
        {
            //RenderSystem renderSystem = root.GetRenderSystemByName("Direct3D9 Rendering Subsystem");
            RenderSystem renderSystem = root.GetRenderSystemByName("OpenGL Rendering Subsystem");
            root.RenderSystem = renderSystem;
            renderSystem.SetConfigOption("Full Screen", "No");
            renderSystem.SetConfigOption("Video Mode", "800 x 600 @ 32-bit colour");
        }

        /// <summary>
        /// Creates the render window.
        /// </summary>
        void CreateRenderWindow()
        {
            window = root.Initialise(true, "MogreFreeSL Demo");
        }

        /// <summary>
        /// Create the event handlers for keyboard & mouse input, sound updates.
        /// </summary>
        void CreateEventHandlers()
        {
            if (inputKeyboard != null)
            {
                inputKeyboard.KeyPressed += new MOIS.KeyListener.KeyPressedHandler(KeyPressed);
            }

            root.FrameStarted += new Mogre.FrameListener.FrameStartedHandler(smgr.FrameStarted);
        }

        /// <summary>
        /// Initializes all resource groups.
        /// </summary>
        void InitializeResourceGroups()
        {
            TextureManager.Singleton.DefaultNumMipmaps = 5;
            ResourceGroupManager.Singleton.InitialiseAllResourceGroups();
        }

        #endregion

        /// <summary>
        /// Sets up the view, the input, the newton debugger, loads the menuSys.paused background, etcetera.
        /// </summary>
        private void CreateScene()
        {
            SetUpView();
            SetUpInput();

            mgr.RootSceneNode.CreateChildSceneNode("Exit").AttachObject(mgr.CreateEntity("Assets\\Meshes\\Exit.mesh"));
            mgr.RootSceneNode.CreateChildSceneNode("Drum").AttachObject(mgr.CreateEntity("Assets\\Meshes\\Drum.mesh"));
            mgr.RootSceneNode.CreateChildSceneNode("Note").AttachObject(mgr.CreateEntity("Assets\\Meshes\\Note.mesh"));

            mgr.GetSceneNode("Exit").Position -= new Vector3(5f, 0, 0);
            mgr.GetSceneNode("Note").Position += new Vector3(5f, 0, 0);

            root.FrameStarted += new FrameListener.FrameStartedHandler(FrameStarted);
        }

        /// <summary>
        /// Sets up the keyboard/mouse for input.
        /// </summary>
        void SetUpInput()
        {
            MOIS.ParamList pl = new MOIS.ParamList();
            IntPtr windowHnd;
            window.GetCustomAttribute("WINDOW", out windowHnd); // window is your RenderWindow!
            pl.Insert("WINDOW", windowHnd.ToString());

            inputManager = MOIS.InputManager.CreateInputSystem(pl);

            // Create all devices (except joystick, as most people have Keyboard/Mouse) using unbuffered input
            inputKeyboard = (MOIS.Keyboard)inputManager.CreateInputObject(MOIS.Type.OISKeyboard, true);
            inputMouse = (MOIS.Mouse)inputManager.CreateInputObject(MOIS.Type.OISMouse, true);
        }

        /// <summary>
        /// Sets up the background, lights, etcetera.
        /// </summary>
        void SetUpView()
        {
            mgr = root.CreateSceneManager(SceneType.ST_GENERIC);
            mgr.AmbientLight = ColourValue.Black;
            //mgr.ShadowTechnique = ShadowTechnique.SHADOWTYPE_STENCIL_ADDITIVE;
            //mgr.ShadowTechnique = ShadowTechnique.SHADOWTYPE_STENCIL_MODULATIVE;
            mgr.ShadowTechnique = ShadowTechnique.SHADOWTYPE_NONE;

            cam = mgr.CreateCamera("Camera");
            cam.NearClipDistance = 0.01f;
            cam.FarClipDistance = 1000;
            cam.Position = new Vector3(position*5, 0, 5);

            mgr.CreateSceneNode("cam");
            mgr.GetSceneNode("cam").AttachObject(cam);

            smgr.InitializeSound(FSL_SOUND_SYSTEM.FSL_SS_EAX2, cam);

            smgr.SetLinearDistanceModel(AL_DISTANCE_MODEL.AL_EXPONENT_DISTANCE_CLAMPED);

            root.AutoCreatedWindow.AddViewport(cam);

            #region Set Up Lights

            mgr.CreateLight("light1");
            mgr.GetLight("light1").Type = Light.LightTypes.LT_POINT;
            mgr.GetLight("light1").Position = new Vector3(-60, 20, -40);
            mgr.GetLight("light1").PowerScale = 0.6f;

            mgr.CreateLight("light2");
            mgr.GetLight("light2").Type = Light.LightTypes.LT_POINT;
            mgr.GetLight("light2").Position = new Vector3(90, -40, -60);

            mgr.CreateLight("light3");
            mgr.GetLight("light3").Type = Light.LightTypes.LT_POINT;
            mgr.GetLight("light3").Position = new Vector3(100, 60, 40);

            mgr.CreateLight("light4");
            mgr.GetLight("light4").Type = Light.LightTypes.LT_POINT;
            mgr.GetLight("light4").Position = new Vector3(-50, -40, 40);
            mgr.GetLight("light4").PowerScale = 0.6f;

            #endregion
        }

        /// <summary>
        /// Runs the render loop. Called to start the rendering process.
        /// </summary>
        void StartRenderLoop()
        {
            while (!root.AutoCreatedWindow.IsClosed && root.RenderOneFrame())
            {
                Application.DoEvents();
            }

            smgr.Destroy();
            root.Dispose();

            Application.Exit();
        }

        #endregion

        /// <summary>
        /// Frame listener that updates the game world/state
        /// </summary>
        /// <param name="evt">Data passed by the event handler that calls the function.</param>
        /// <returns></returns>
        bool FrameStarted(FrameEvent evt)
        {
            inputKeyboard.Capture();
            inputMouse.Capture();

            deltaTime = evt.timeSinceLastFrame;
            totalTime += evt.timeSinceLastFrame;

            if (!Approx(cam.Position.x, position * 5))
            {
                if (cam.Position.x < position * 5)
                    cam.Position += new Vector3(5f, 0, 0) * deltaTime;
                if (cam.Position.x > position * 5)
                    cam.Position -= new Vector3(5f, 0, 0) * deltaTime;
            }

            return running;
        }

        /// <summary>
        /// Function that handles tasks that trigger when a key is pressed.
        /// </summary>
        /// <param name="e">Data passed by the event handler that calls the function.</param>
        /// <returns></returns>
        public bool KeyPressed(MOIS.KeyEvent e)
        {
            switch (e.key)
            {
                case MOIS.KeyCode.KC_LEFT:
                case MOIS.KeyCode.KC_A:
                    position--;
                    if (position < -1)
                        position = -1;
                    break;

                case MOIS.KeyCode.KC_RIGHT:
                case MOIS.KeyCode.KC_D:
                    position++;
                    if (position > 1)
                        position = 1;
                    break;

                case MOIS.KeyCode.KC_SPACE:
                    switch (position)
                    {
                        case -1:
                            running = false;
                            break;

                        case 0:
                            if (soundEntity != null)
                            {
                                if (soundEntity.IsPlaying())
                                    soundEntity.Stop();
                                else
                                    soundEntity.Play();
                                break;
                            }

                            soundEntity = smgr.CreateSoundEntity("Assets/Sounds/DrumMono.ogg", mgr.GetSceneNode("Drum"), "Drum", false, false);
                            soundEntity.SetReferenceDistance(.5f);
                            soundEntity.SetMaxDistance(5);
                            soundEntity.Play();
                            break;

                        case 1:
                            if (ambientSound != null)
                            {
                                if (ambientSound.IsPlaying())
                                    ambientSound.Stop();
                                else
                                    ambientSound.Play();
                                break;
                            }
                            ambientSound = smgr.CreateAmbientSound("Assets/Sounds/Tune.ogg", "Tune", false, false);
                            ambientSound.Play();
                            break;
                    }
                    break;

                case MOIS.KeyCode.KC_ESCAPE:
                    running = false;
                    break;
            }

            return true;
        }

        bool Approx(float value1, float value2)
        {
            return Approx(value1, value2, .01f);
        }

        bool Approx(float value1, float value2, float maxDifference)
        {
            if (Mogre.Math.Abs(value1 - value2) < maxDifference)
                return true;
            return false;
        }
    }
}