using System;
using System.Collections.Generic;
using System.Text;
using Mogre;
using MogreNewt;

namespace MogreNewt.Demo.Basics
{
    // Pressing 'SPACE' shoots a box
    // 'F3' Newton debug lines

    class Basics : Mogre.Demo.ExampleApplication.Example
    {
        const int NEWTON_FRAMERATE = 60;

        float m_update = 1.0f / (float)NEWTON_FRAMERATE;
        float m_elapsed = 0;

        int mEntityCount = 0;
        World m_World;

        float timer = 0;

        public override void CreateFrameListener()
        {
            base.CreateFrameListener();

            root.FrameStarted += new FrameListener.FrameStartedHandler(Scene_FrameStarted);
            root.FrameStarted += new FrameListener.FrameStartedHandler(Newton_FrameStarted);
        }

        bool Scene_FrameStarted(FrameEvent evt)
        {
            // in this frame listener we allow the user to "shoot" boxes
            // by pressing the space bar.

            if (inputKeyboard.IsKeyDown(MOIS.KeyCode.KC_SPACE))
            {
                if (timer <= 0.0f)
                {
                    // now "shoot" an object!

                    // we get the position and direction from the camera...
                    Vector3 dir, vec;
                    Quaternion camorient = camera.Orientation;
                    vec = new Vector3(0, 0, -1);

                    dir = camorient * vec;

                    // then make the visual object (again a cylinder)
                    Vector3 pos = camera.Position;

                    Body body = MakeSimpleBox(new Vector3(2, 2, 2), pos, camorient);

                    // set the initial orientation and velocity!
                    body.Velocity = dir * 30.0f;

                    timer = 0.2f;
                }
            }

            timer -= evt.timeSinceLastFrame;

            return true;
        }


        // The basic OgreNewt framelistener with time slicing
        bool Newton_FrameStarted(FrameEvent evt)
        {
            m_elapsed += evt.timeSinceLastFrame;

            if ((m_elapsed > m_update) && (m_elapsed < (1.0f)))
            {
                while (m_elapsed > m_update)
                {
                    m_World.Update(m_update);
                    m_elapsed -= m_update;
                }
            }
            else
            {
                if (m_elapsed < (m_update))
                {
                    // not enough time has passed this loop, so ignore for now.
                }
                else
                {
                    m_World.Update(m_elapsed);
                    m_elapsed = 0.0f; // reset the elapsed time so we don't become "eternally behind".
                }
            }

            // For the debug lines
            if (inputKeyboard.IsKeyDown(MOIS.KeyCode.KC_F3))
            {
                MogreNewt.Debugger.Instance.ShowLines(m_World);
            }
            else
            {
                MogreNewt.Debugger.Instance.HideLines();
            }

            return true;
        }

        public override void CreateScene()
        {
            // Newton initialization
            m_World = new World();
            MogreNewt.Debugger.Instance.Init(sceneMgr);


            // sky box.
            sceneMgr.SetSkyBox(true, "Examples/CloudyNoonSkyBox");

            // shadows on!
            sceneMgr.ShadowTechnique = ShadowTechnique.SHADOWTYPE_STENCIL_ADDITIVE;

            // floor object!
            Entity floor;
            SceneNode floornode;
            floor = sceneMgr.CreateEntity("Floor", "simple_terrain.mesh");
            floornode = sceneMgr.RootSceneNode.CreateChildSceneNode("FloorNode");
            floornode.AttachObject(floor);
            floor.SetMaterialName("Simple/BeachStones");

            floor.CastShadows = false;

            //-------------------------------------------------------------
            // add some other objects.
            Entity floor2;
            SceneNode floornode2;
            floor2 = sceneMgr.CreateEntity("Floor2", "simple_terrain.mesh");
            floornode2 = floornode.CreateChildSceneNode("FloorNode2");
            floornode2.AttachObject(floor2);
            floor2.SetMaterialName("Simple/BeachStones");
            floor2.CastShadows = false;
            floornode2.SetPosition(80.0f, 0.0f, 0.0f);

            Entity floor3;
            SceneNode floornode3;
            floor3 = sceneMgr.CreateEntity("Floor3", "simple_terrain.mesh");
            floornode3 = floornode.CreateChildSceneNode("FloorNode3");
            floornode3.AttachObject(floor3);
            floor3.SetMaterialName("Simple/BeachStones");
            floor3.CastShadows = false;
            floornode3.SetPosition(-80.0f, -5.0f, 0.0f);
            floornode3.Orientation = new Quaternion(new Degree(15.0f), Vector3.UNIT_Z);
            //-------------------------------------------------------------

            // using the new "SceneParser" TreeCollision primitive.  this will automatically parse an entire tree of
            // SceneNodes (parsing all children), and add collision for all meshes in the tree.
            MogreNewt.CollisionPrimitives.TreeCollisionSceneParser stat_col = new MogreNewt.CollisionPrimitives.TreeCollisionSceneParser(m_World);
            stat_col.ParseScene(floornode, true);
            MogreNewt.Body bod = new MogreNewt.Body(m_World, stat_col);
            stat_col.Dispose();

            bod.AttachNode(floornode);
            bod.SetPositionOrientation(new Vector3(0.0f, -20.0f, 0.0f), Quaternion.IDENTITY);


            // position camera
            camera.SetPosition(0.0f, -3.0f, 20.0f);

            //make a light
            Light light;

            light = sceneMgr.CreateLight("Light1");
            light.Type = Light.LightTypes.LT_POINT;
            light.SetPosition(0.0f, 100.0f, 100.0f);
        }

        public override void DestroyScene()
        {
            // Not absolutely necessary, it can get cleaned up at the finalizer
            m_World.Dispose();
            m_World = null;

            MogreNewt.Debugger.Instance.DeInit();
        }

        Body MakeSimpleBox(Vector3 size, Vector3 pos, Quaternion orient)
        {
            // base mass on the size of the object.
            float mass = size.x * size.y * size.z * 2.5f;

            // calculate the inertia based on box formula and mass
            Vector3 inertia;
            Vector3 offset;


            Entity box1;
            SceneNode box1node;

            box1 = sceneMgr.CreateEntity("Entity" + (mEntityCount++), "box.mesh");
            box1node = sceneMgr.RootSceneNode.CreateChildSceneNode();
            box1node.AttachObject(box1);
            box1node.SetScale(size);
            box1.NormaliseNormals = true;

            MogreNewt.ConvexCollision col = new MogreNewt.CollisionPrimitives.Box(m_World, size);
            col.CalculateInertialMatrix(out inertia, out offset);
            inertia = inertia * mass;
            MogreNewt.Body bod = new MogreNewt.Body(m_World, col);
            col.Dispose();

            bod.AttachNode(box1node);
            bod.SetMassMatrix(mass, inertia);
            bod.IsGravityEnabled = true;

            box1.SetMaterialName("Examples/10PointBlock");


            bod.SetPositionOrientation(pos, orient);

            return bod;
        }
    }
}
