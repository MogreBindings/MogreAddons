using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Mogre;
using MParticleUniverse;

namespace ParticleUniverseTest
{
    public partial class FrmTest : Form
    {
        bool mProgramInitialized = false;

        Root mRoot;
        RenderWindow mWindow;
        SceneManager mgr;

        SceneNode mCamTargetNode;
        SceneNode mCamNode;
        Camera mCamera;

        MParticleUniverse.ParticleSystemManager puManager;
        MParticleUniverse.ParticleSystem puSystem;
        MParticleUniverse.ParticleSystem puLightningBolt;
        SceneNode puScene;
        SceneNode puLightningBoltScene;


        public FrmTest()
        {
            InitializeComponent();
            
            //this.Size = new Size(800, 600);
            
            this.Activated += new EventHandler(Form1_Activated);
            this.Disposed += new EventHandler(Form1_Disposed);
            this.Resize += new EventHandler(Form1_Resize);

        }

        public void Form1_Activated(object sender, EventArgs e)
        {
            if (!mProgramInitialized)
            {
                mProgramInitialized = true;
                
                init();
                go();
            }
        }

        private void start_pu()
        {
            //puSystem = new MParticleUniverse.ParticleSystem("RedStar", "Spatial/RedStar");
            //puSystem = MParticleUniverse.ParticleSystem.CreateParticleSystem("RedStar", "Spatial/RedStar");
            //puSystem.SetSceneManager(mgr);
            puSystem = puManager.CreateParticleSystem("RedStar", "Spatial/RedStar", mgr); //Flare/mp_sun
            puScene = mgr.RootSceneNode.CreateChildSceneNode("puScene");
            puScene.AttachObject(puSystem);
            puSystem.KeepLocal = true;
            Vector3 scale = new Vector3(0.5f, 0.5f, 0.5f);
            puSystem.Scale = scale;
            puSystem.Start();

            
            //MParticleUniverse.ParticleSystem puCenterSystem = puManager.CreateParticleSystem("RedStarLocal", "Spatial/RedStar", mgr); //Flare/mp_sun
            //SceneNode puCenterScene = mgr.RootSceneNode.CreateChildSceneNode("puCenterScene");
            //puCenterScene.AttachObject(puCenterSystem);
            //puCenterSystem.IsKeepLocal = true;
            //puCenterSystem.Start();

            MParticleUniverse.ParticleSystem pump_starfieldSystem = puManager.CreateParticleSystem("mp_starfield", "Flare/mp_starfield", mgr);
            SceneNode pump_starfieldScene = mgr.RootSceneNode.CreateChildSceneNode("pump_starfieldScene");
            pump_starfieldScene.AttachObject(pump_starfieldSystem);
            pump_starfieldSystem.Start();

        }

        public void Example30()
        {
            puLightningBolt = puManager.CreateParticleSystem("Example30LaserBeam", "example_030", mgr); //Flare/mp_sun
            puLightningBoltScene = mgr.RootSceneNode.CreateChildSceneNode("Example30LaserBeamScene");
            puLightningBoltScene.AttachObject(puLightningBolt);
            puLightningBolt.KeepLocal = false;
            puLightningBolt.Scale = (new Vector3(0.5f, 0.5f, 0.5f));
            puLightningBolt.Start();
        }

        public void Lesson1()
        {
            puLightningBolt = puManager.CreateParticleSystem("puLesson1", mgr);
            SceneNode scene = mgr.RootSceneNode.CreateChildSceneNode("puLesson1Scene");
            scene.AttachObject(puLightningBolt);

            ParticleTechnique puLightningBoltT = puLightningBolt.CreateTechnique();
            puLightningBoltT.MaterialName = "ParticleUniverse/Flare";
            MParticleUniverse.ParticleEmitters.PointEmitter pe = (MParticleUniverse.ParticleEmitters.PointEmitter)puLightningBoltT.CreateEmitter("Point");
        }

        public void Lesson2()
        {
            puLightningBolt = puManager.CreateParticleSystem("puLesson2", mgr);
            SceneNode scene = mgr.RootSceneNode.CreateChildSceneNode("puLesson2Scene");
            scene.AttachObject(puLightningBolt);

            //puLightningBolt.IsKeepLocal = true;
            ParticleTechnique puLightningBoltT = puLightningBolt.CreateTechnique();
            //ParticleTechnique puLightningBoltT = puLightningBolt.GetTechnique(0);
            puLightningBoltT.MaterialName = "ParticleUniverse/Flare";
            //puLightningBoltT.SetRenderer("Billboard");
            MParticleUniverse.ParticleEmitters.PointEmitter pe = (MParticleUniverse.ParticleEmitters.PointEmitter)puLightningBoltT.CreateEmitter("Point");
            //puLightningBolt.SetDebugDisplayEnabled(true);
            DynamicAttributeFixed emissionRate = new DynamicAttributeFixed();
            emissionRate.SetValue(60);
            pe.DynEmissionRate = emissionRate;

            DynamicAttributeRandom emissionVelocity = new DynamicAttributeRandom();
            emissionVelocity.Min = 50;
            emissionVelocity.Max = 300;
            pe.DynVelocity = emissionVelocity;

            DynamicAttributeRandom emissionDimentions = new DynamicAttributeRandom();
            emissionDimentions.Min = 50;
            emissionDimentions.Max = 150;
            pe.DynParticleAllDimensions = emissionDimentions;

            MParticleUniverse.ParticleAffectors.ColourAffector colAffector = (MParticleUniverse.ParticleAffectors.ColourAffector)puLightningBoltT.CreateAffector("Colour");
            colAffector.AddColour(0, ColourValue.Black);
            colAffector.AddColour(0, new ColourValue(0.929412f, 0.0196078f, 0.909804f, 1f));

            MParticleUniverse.ParticleAffectors.LinearForceAffector lforceAffector = (MParticleUniverse.ParticleAffectors.LinearForceAffector)puLightningBoltT.CreateAffector("LinearForce");
            lforceAffector.ForceVector = new Vector3(100, -100, 0);

        }

        /// <summary>
        /// This is the same lightning System as Flare/mp_lightning from the Editor.
        /// </summary>
        public void MakeLighting()
        {
            puLightningBolt = puManager.CreateParticleSystem("TestLightningBolt", mgr);
            puLightningBoltScene = mgr.RootSceneNode.CreateChildSceneNode("LightningBoltScene");
            puLightningBoltScene.AttachObject(puLightningBolt);

            
            ParticleTechnique puLightningBoltT = puLightningBolt.CreateTechnique();
            puLightningBoltT.VisualParticleQuota = 5;
            puLightningBoltT.MaterialName = "mp_flare_lightning_bolt_1x2";
            puLightningBoltT.DefaultWidth = 200;
            puLightningBoltT.DefaultHeight = 200;

            MParticleUniverse.ParticleRenderers.BillboardRenderer renderer = (MParticleUniverse.ParticleRenderers.BillboardRenderer)puLightningBoltT.GetRenderer();
            renderer.TextureCoordsColumns = 2;
            renderer.BillboardRotationType = BillboardRotationType.BBR_VERTEX;
            
            MParticleUniverse.ParticleEmitters.PointEmitter pe = (MParticleUniverse.ParticleEmitters.PointEmitter)puLightningBoltT.CreateEmitter("Point");
            DynamicAttributeFixed peRate = new DynamicAttributeFixed();
            peRate.SetValue(9);
            pe.DynEmissionRate = peRate;
            DynamicAttributeRandom petime_to_live = new DynamicAttributeRandom();
            petime_to_live.Min = 0.3f;
            petime_to_live.Max = 0.6f;
            pe.DynTotalTimeToLive = petime_to_live;
            DynamicAttributeFixed peVelocity = new DynamicAttributeFixed();
            peVelocity.SetValue(0);
            pe.DynVelocity = peVelocity;
            DynamicAttributeFixed peDuration = new DynamicAttributeFixed();
            peDuration.SetValue(0.7f);
            pe.DynDuration = peDuration;
            pe.DynDurationSet(true);
            pe.ParticleTextureCoordsRangeEnd = 1;

            MParticleUniverse.ParticleAffectors.ColourAffector ca = (MParticleUniverse.ParticleAffectors.ColourAffector)puLightningBoltT.CreateAffector("Colour");
            ca.AddColour(0, new Mogre.ColourValue(1, 1, 1, 1));
            ca.AddColour(0.1f, new Mogre.ColourValue(1, 1, 1, 1));
            ca.AddColour(1, new Mogre.ColourValue(0, 0, 0, 1));
            ca.ColourOperation = MParticleUniverse.ParticleAffectors.ColourAffector.ColourOperations.CAO_MULTIPLY;
            ////puLightningBoltT.AddAffector(ca);


            MParticleUniverse.ParticleObservers.OnClearObserver oco = (MParticleUniverse.ParticleObservers.OnClearObserver)puLightningBoltT.CreateObserver("OnClear");
            MParticleUniverse.ParticleEventHandlers.DoStopSystemEventHandler dsseh = (MParticleUniverse.ParticleEventHandlers.DoStopSystemEventHandler)oco.CreateEventHandler("DoStopSystem");
        }

        public void TestLaser()
        {
            puLightningBolt = puManager.CreateParticleSystem("TestLaser", mgr);
            puLightningBoltScene = mgr.RootSceneNode.CreateChildSceneNode("TestLaserScene");
            puLightningBoltScene.AttachObject(puLightningBolt);

            puLightningBolt.Scale = (new Vector3(10, 10, 10));
            //MParticleUniverse.ParticleRenderers.BillboardRenderer renderer = (MParticleUniverse.ParticleRenderers.BillboardRenderer)puLightningBoltT.GetRenderer();
            //MParticleUniverse.ParticleEmitters.PointEmitter pe = (MParticleUniverse.ParticleEmitters.PointEmitter)puLightningBoltT.CreateEmitter("Point");
            
ParticleTechnique tTarget1 = puLightningBolt.CreateTechnique();

        tTarget1.VisualParticleQuota = 2;
        tTarget1.EmittedEmitterQuota = 0;
        tTarget1.EmittedTechniqueQuota = 0;
        tTarget1.EmittedAffectorQuota = 0;
        tTarget1.EmittedSystemQuota = 0;
        tTarget1.MaterialName = "ParticleUniverse/Beam_1";
        tTarget1.DefaultWidth = 0.3f;
        tTarget1.SetRenderer("Beam");
        MParticleUniverse.ParticleRenderers.BeamRenderer tTarget1Renderer = (MParticleUniverse.ParticleRenderers.BeamRenderer)tTarget1.GetRenderer();
            tTarget1Renderer.MaxChainElements = 20;
            tTarget1Renderer.UpdateInterval = 0.04f;
            tTarget1Renderer.Deviation = 10;
            tTarget1Renderer.NumberOfSegments = 1;
        MParticleUniverse.ParticleEmitters.PointEmitter tTarget1Emitter = (MParticleUniverse.ParticleEmitters.PointEmitter)tTarget1.CreateEmitter("Point");
            DynamicAttributeFixed tTarget1EmitterTTL = new DynamicAttributeFixed();
            tTarget1EmitterTTL.SetValue(200);
            tTarget1Emitter.DynTotalTimeToLive = tTarget1EmitterTTL;
            tTarget1Emitter.Position = new Vector3(0, 0, 10);
            tTarget1Emitter.Direction = new Vector3(0, 0, 0);
            
    ParticleTechnique tTarget2 = puLightningBolt.CreateTechnique();
        tTarget2.VisualParticleQuota = 2;
        tTarget2.EmittedEmitterQuota = 0;
        tTarget2.EmittedTechniqueQuota = 0;
        tTarget2.EmittedAffectorQuota = 0;
        tTarget2.MaterialName = "ParticleUniverse/Beam_2";
        tTarget2.DefaultWidth = 0.1f;
        tTarget2.SetRenderer("Beam");
        MParticleUniverse.ParticleRenderers.BeamRenderer tTarget2Renderer = (MParticleUniverse.ParticleRenderers.BeamRenderer)tTarget2.GetRenderer();
            tTarget2Renderer.MaxChainElements = 20;
            tTarget2Renderer.UpdateInterval = 0.02f;
            tTarget2Renderer.Deviation = 1.5f;
            tTarget2Renderer.NumberOfSegments = 3;
            tTarget2Renderer.Jump = true;
        MParticleUniverse.ParticleEmitters.PointEmitter tTarget2Emitter = (MParticleUniverse.ParticleEmitters.PointEmitter)tTarget2.CreateEmitter("Point");
            DynamicAttributeFixed tTarget2EmitterTTL = new DynamicAttributeFixed();
            tTarget2EmitterTTL.SetValue(200);
            tTarget2Emitter.DynTotalTimeToLive = tTarget2EmitterTTL;
            tTarget2Emitter.Direction = new Vector3(0, 0, 0);
            tTarget2Emitter.Position = new Vector3(0, 0, 10);
    ParticleTechnique tTarget3 = puLightningBolt.CreateTechnique();
        tTarget3.MaterialName = "ParticleUniverse/Star";
        tTarget3.DefaultWidth = 1;
        tTarget3.DefaultHeight = 5;
        MParticleUniverse.ParticleRenderers.BillboardRenderer tTarget3Renderer = (MParticleUniverse.ParticleRenderers.BillboardRenderer)tTarget3.GetRenderer();
            tTarget3Renderer.BillboardType = MParticleUniverse.ParticleRenderers.BillboardRenderer.BillboardTypes.BBT_ORIENTED_SELF;
        MParticleUniverse.ParticleEmitters.PointEmitter tTarget3Emitter1 = (MParticleUniverse.ParticleEmitters.PointEmitter)tTarget3.CreateEmitter("Point");
            DynamicAttributeFixed tTarget3EmitterTTL1 = new DynamicAttributeFixed();
            tTarget3EmitterTTL1.SetValue(0.2f);
            tTarget3Emitter1.DynTotalTimeToLive = tTarget3EmitterTTL1;
            DynamicAttributeFixed tTarget3EmitterEmissionRate1 = new DynamicAttributeFixed();
            tTarget3EmitterEmissionRate1.SetValue(100);
            tTarget3Emitter1.DynEmissionRate = tTarget3EmitterEmissionRate1;                    
            DynamicAttributeFixed tTarget3EmitterAngle1 = new DynamicAttributeFixed();
            tTarget3EmitterAngle1.SetValue(90);
            tTarget3Emitter1.DynAngle = tTarget3EmitterAngle1;
            DynamicAttributeRandom tTarget3EmitterVelocity1 = new DynamicAttributeRandom();
            tTarget3EmitterVelocity1.Min = 1;
            tTarget3EmitterVelocity1.Max = 15;
            tTarget3Emitter1.DynVelocity = tTarget3EmitterVelocity1;
            tTarget3Emitter1.Name = "tTarget3Emitter1_Sparkles";

        MParticleUniverse.ParticleEmitters.PointEmitter tTarget3Emitter2 = (MParticleUniverse.ParticleEmitters.PointEmitter)tTarget3.CreateEmitter("Point");
            DynamicAttributeFixed tTarget3EmitterEmissionRate2 = new DynamicAttributeFixed();
            tTarget3EmitterEmissionRate2.SetValue(1);
            tTarget3Emitter2.DynEmissionRate = tTarget3EmitterEmissionRate2;                    
            DynamicAttributeFixed tTarget3EmitterTTL2 = new DynamicAttributeFixed();
            tTarget3EmitterTTL2.SetValue(200);
            tTarget3Emitter2.DynTotalTimeToLive = tTarget3EmitterTTL2;
            tTarget3Emitter2.Position = new Vector3(0, 0, 10);
            tTarget3Emitter2.Direction = new Vector3(0, 0, 0);
            tTarget3Emitter2.EmitsName = tTarget3Emitter1.Name;
            tTarget3Emitter2.EmitsType = MParticleUniverse.Particle.ParticleType.PT_EMITTER;
            tTarget3Emitter2.ForceEmission = true;
            tTarget3Emitter2.Name = "tTarget3Emitter2_Base";
            
        MParticleUniverse.ParticleAffectors.LinearForceAffector tTarget3LinearForceAffector = (MParticleUniverse.ParticleAffectors.LinearForceAffector)tTarget3.CreateAffector("LinearForce");
            tTarget3LinearForceAffector.AddEmitterToExclude(tTarget3Emitter2.Name);
            tTarget3LinearForceAffector.ForceVector = new Vector3(0, -30, 0);
            
    ParticleTechnique tTarget4 = puLightningBolt.CreateTechnique();
        tTarget4.MaterialName = "BaseWhite";
        tTarget1.SetRenderer("Sphere");
        //MParticleUniverse.ParticleRenderers.SphereRenderer tTarget4Renderer = (MParticleUniverse.ParticleRenderers.SphereRenderer)tTarget1.GetRenderer();
        MParticleUniverse.ParticleEmitters.BoxEmitter tTarget4Emitter = (MParticleUniverse.ParticleEmitters.BoxEmitter)tTarget4.CreateEmitter("Box");
            tTarget4Emitter.Position = new Vector3(0, 0, 0);
            DynamicAttributeFixed tTarget4EmitterTTL = new DynamicAttributeFixed();
            tTarget4EmitterTTL.SetValue(10);
            tTarget4Emitter.DynTotalTimeToLive = tTarget4EmitterTTL;
            DynamicAttributeFixed tTarget4EmitterVelocity = new DynamicAttributeFixed();
            tTarget4EmitterVelocity.SetValue(1);
            tTarget4Emitter.DynVelocity = tTarget4EmitterVelocity;
            DynamicAttributeFixed tTarget4EmitterDim = new DynamicAttributeFixed();
            tTarget4EmitterDim.SetValue(0.5f);
            tTarget4Emitter.DynParticleAllDimensions = tTarget4EmitterDim;
            tTarget4Emitter.Width = 10;
            tTarget4Emitter.Height = 10;
            tTarget4Emitter.Depth = 10;
            
        //extern                                  PhysXActor
        //    physx_shape                         Sphere
        //        shape_group                     2

        }

        public void go()
        {
            Show();

            puManager = MParticleUniverse.ParticleSystemManager.Singleton;
            start_pu();
            //MakeLighting();
            //TestLaser();
            Example30();
            
            //Lesson2();
            ParticleTechnique puLightningBoltTechnique0 = puLightningBolt.GetTechnique(0);
            ParticleTechnique puLightningBoltTechnique1 = puLightningBolt.GetTechnique(1);
            ParticleTechnique puLightningBoltTechnique2 = puLightningBolt.GetTechnique(2);
            //ParticleTechnique puLightningBoltTechnique3 = puLightningBolt.GetTechnique(3);

                puLightningBoltScene.InheritOrientation = (false);
                puLightningBoltScene.Position = new Vector3(150, 300, 0);

            puLightningBolt.Start();

            
            while (mRoot != null && mRoot.RenderOneFrame())
            {
                lblInfo.Text = puLightningBolt.Name + "\nNumber Of Emitted Particles: " + puLightningBolt.GetNumberOfEmittedParticles() +
                    "\nPosition: \n" + puLightningBolt.Position +
                    "\nLatestPosition: \n" + puLightningBolt.LatestPosition;


                //The Following part Shows how something like a laser can move between source and target.
                {
                    Vector3 targetPosition = puScene.Position; // = new Vector3(150, 200, 0);

                    ParticlePool pool = puLightningBoltTechnique0._getParticlePool();
                    MParticleUniverse.Particle particle = pool.GetFirst();
                    while (particle != null)
                    {
                        particle.Position = targetPosition;
                        particle = pool.GetNext();
                    }

                    pool = puLightningBoltTechnique1._getParticlePool();
                    particle = pool.GetFirst();
                    while (particle != null)
                    {
                        particle.Position = targetPosition;
                        particle = pool.GetNext();
                    }

                    pool = puLightningBoltTechnique2._getParticlePool();
                    particle = pool.GetFirst();
                    while (particle != null)
                    {
                        particle.Position = targetPosition;
                        particle = pool.GetNext();
                    }

                    //pool = puLightningBoltTechnique0._getParticlePool();
                    //particle = pool.GetFirst();
                    //while (particle != null)
                    //{
                    //    particle.Position = targetPosition;
                    //    particle = pool.GetNext();
                    //}
                }

                
                Application.DoEvents();
            }
        }

        public void init()
        {
            mRoot = new Root();
            mRoot.AddResourceLocation(Application.StartupPath + "\\media", "FileSystem", "General", false);
            //mRoot.AddResourceLocation(Application.StartupPath + "\\media\\background", "FileSystem", "General", false);
            //mRoot.AddResourceLocation(Application.StartupPath + "\\media\\spatial", "FileSystem", "General", false);
            //mRoot.AddResourceLocation(Application.StartupPath + "\\media\\Asteroids", "FileSystem", "General", false);
            mRoot.AddResourceLocation(Application.StartupPath + "\\media\\pu", "FileSystem", "General", false);

            //            mRoot.AddResourceLocation(Application.StartupPath + "\\media\\models", "FileSystem", "General", false);
            //mRoot.AddResourceLocation(Application.StartupPath + "\\media\\fonts", "FileSystem", "General", false);
            //mRoot.AddResourceLocation(Application.StartupPath + "\\media\\materials", "FileSystem", "General", false);
            //mRoot.AddResourceLocation(Application.StartupPath + "\\media\\ParticleUniverse\\core", "FileSystem", "General", false);
            //mRoot.AddResourceLocation(Application.StartupPath + "\\media\\ParticleUniverse\\examples\\materials", "FileSystem", "General", false);
            //mRoot.AddResourceLocation(Application.StartupPath + "\\media\\ParticleUniverse\\examples\\models", "FileSystem", "General", false);
            //mRoot.AddResourceLocation(Application.StartupPath + "\\media\\ParticleUniverse\\examples\\scripts", "FileSystem", "General", false);
            //mRoot.AddResourceLocation(Application.StartupPath + "\\media\\ParticleUniverse\\examples\\textures", "FileSystem", "General", false);
            //mRoot.AddResourceLocation(Application.StartupPath + "\\media\\ParticleUniverse\\examples\\ogre", "FileSystem", "General", false);
            //mRoot.AddResourceLocation(Application.StartupPath + "\\media\\ParticleUniverse\\mediapack\\materials", "FileSystem", "General", false);
            //mRoot.AddResourceLocation(Application.StartupPath + "\\media\\ParticleUniverse\\mediapack\\models", "FileSystem", "General", false);
            //mRoot.AddResourceLocation(Application.StartupPath + "\\media\\ParticleUniverse\\mediapack\\scripts", "FileSystem", "General", false);
            //mRoot.AddResourceLocation(Application.StartupPath + "\\media\\ParticleUniverse\\mediapack\\textures", "FileSystem", "General", false);


            //mRoot.
            // Setup RenderSystem
            RenderSystem rs = mRoot.GetRenderSystemByName("Direct3D9 Rendering Subsystem"); //("OpenGL Rendering Subsystem"); // 
            // or use "OpenGL Rendering Subsystem"
            mRoot.RenderSystem = rs;
            rs.SetConfigOption("Full Screen", "No");
            rs.SetConfigOption("Video Mode", "500 x 500 @ 32-bit colour");

            // Create Render Window
            mRoot.Initialise(false, "Main Ogre Window");
            NameValuePairList misc = new NameValuePairList();
            misc["externalWindowHandle"] = picRenderBox.Handle.ToString();
            misc["vsync"] = "true";
            mWindow = mRoot.CreateRenderWindow("Main RenderWindow", 500, 500, false, misc);

            mRoot.FrameStarted += new FrameListener.FrameStartedHandler(FrameStarted);


            // Create a Simple Scene
            mgr = mRoot.CreateSceneManager(SceneType.ST_GENERIC, "RootSceneManager");
            mCamera = mgr.CreateCamera("Camera");
            mCamera.AutoAspectRatio = true;
            mWindow.AddViewport(mCamera);

            mgr.AmbientLight = ColourValue.White;
            mgr.ShadowTechnique = ShadowTechnique.SHADOWTYPE_STENCIL_ADDITIVE;

            ResourceGroupManager.Singleton.InitialiseAllResourceGroups();

            //Entity ent1 = mgr.CreateEntity("ninja", "ninja.mesh");
            //SceneNode node1 = mgr.RootSceneNode.CreateChildSceneNode("ninjaNode");
            //node1.AttachObject(ent1);

            Vector3 targetPosition = new Mogre.Vector3(0, 0, 0);
            mCamTargetNode = mgr.RootSceneNode.CreateChildSceneNode("CamTargetNode", targetPosition);
            mCamNode = mCamTargetNode.CreateChildSceneNode("CameraNode", new Mogre.Vector3(100, 100, 2000)); //100, 800, 3000
            mCamNode.AttachObject(mCamera);
            mCamera.LookAt(targetPosition);
            mCamera.QueryFlags = 0;

        }

        void Form1_Disposed(object sender, EventArgs e)
        {
            try
            {
                //Call this before disposing of Mogre to avoid Access Violation Errors when Mogre Shuts down.
                puManager.DestroyAllParticleSystems(mgr);
                mRoot.Dispose();
            }
            catch (Exception) { }
            mRoot = null;
        }
        void Form1_Resize(object sender, EventArgs e)
        {
            mWindow.WindowMovedOrResized();
        }

        private bool FrameStarted(FrameEvent evt)
        {
            //timeSinceLastFrame = evt.timeSinceLastFrame;
            UpdateOrbit();
            return true;
        }

        #region Orbiting Star

        float currentOrbitalDegree = 0;
        float orbitalSpeed = 0.1f;
        Vector3 orbitingPosistion;
        Vector3 orbitingTarget = new Vector3(0, 0, 0);

        Mogre.Vector3 OrbitMajorAxis = new Vector3(675, 0, 0);
        public Mogre.Vector3 OrbitMinorAxis = new Vector3(0, 375, 0);

        public void UpdateOrbit()
        {
            if (orbitalSpeed == 0)
                return;

            currentOrbitalDegree += orbitalSpeed; // 0.01f;
            if (currentOrbitalDegree > 360)
                currentOrbitalDegree = 0;
            if (currentOrbitalDegree < 0)
                currentOrbitalDegree = 360;

            //if (orbitingObject != null)
            //    orbitingTarget = orbitingObject.Position;

            //orbitingPosistion = new Vector3(orbitRadius * Mogre.Math.Cos(currentOrbitalDegree) + orbitingTarget.x, 0 + orbitingTarget.y, orbitRadius * Mogre.Math.Sin(currentOrbitalDegree) + orbitingTarget.z);
            orbitingPosistion = PositionAtDegree(currentOrbitalDegree);
            puScene.Position = orbitingPosistion;
            puSystem.Position = orbitingPosistion;
            //orbitPathScene.Position = orbitingTarget;

            
            //particleMan.UpdatePosition(name + "SelectionHalo", orbitingPosistion);
        }

        Mogre.Vector3 PositionAtDegree(float degree)
        {
            float alpha = degree * (Mogre.Math.PI / 180);
            float alphaCos = Mogre.Math.Cos(alpha);
            float alphaSin = Mogre.Math.Sin(alpha);
            float X = orbitingTarget.x + alphaCos * OrbitMajorAxis.x + alphaSin * OrbitMinorAxis.x;
            float Y = orbitingTarget.y + alphaCos * OrbitMajorAxis.y + alphaSin * OrbitMinorAxis.y;
            float Z = orbitingTarget.z + alphaCos * OrbitMajorAxis.z + alphaSin * OrbitMinorAxis.z;
            return (new Mogre.Vector3(X, Y, Z)); //, orbitingPosistion);
        }

        #endregion 
    }
}
