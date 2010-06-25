using System;
using System.Collections.Generic;
using System.Text;
using Mogre;
using MogreNewt;

namespace MogreNewt.Demo.SimpleVehicle
{
    // 'SPACE' throws a 'ball'
    // 'Q' Resets the vehicle
    // 'I','K','J','L' control the vehicle
    // 'F3' Newton debug lines

    class SimpleVehicleApp : Mogre.Demo.ExampleApplication.Example
    {
        const int NEWTON_FRAMERATE = 60;

        float m_update = 1.0f / (float)NEWTON_FRAMERATE;
        float m_elapsed = 0;

        int mEntityCount = 0;
        World m_World;

        float timer = 0;

        SimpleVehicle mCar;
        bool mR;

        SceneNode collisionNode;

        public override void CreateFrameListener()
        {
            base.CreateFrameListener();

            root.FrameStarted += new FrameListener.FrameStartedHandler(Scene_FrameStarted);
            root.FrameStarted += new FrameListener.FrameStartedHandler(Newton_FrameStarted);
        }

        bool Scene_FrameStarted(FrameEvent evt)
        {

            inputKeyboard.Capture();


            if (inputKeyboard.IsKeyDown(MOIS.KeyCode.KC_SPACE))
            {
                if (timer <= 0.0)
                {
                    Vector3 dir, vec;
                    Quaternion camorient = camera.Orientation;
                    vec = new Vector3(0, 0, -1);

                    dir = camorient * vec;

                    Entity ent;
                    SceneNode node;
                    String name;
                    Vector3 pos = camera.Position;

                    name = "Body " + (mEntityCount++);

                    ent = sceneMgr.CreateEntity(name, "ellipsoid.mesh");
                    node = sceneMgr.RootSceneNode.CreateChildSceneNode(name);
                    node.AttachObject(ent);

                    ent.SetMaterialName("Simple/dirt01");

                    MogreNewt.Collision col = new MogreNewt.CollisionPrimitives.Ellipsoid(m_World, new Vector3(1, 1, 1));
                    MogreNewt.Body body = new MogreNewt.Body(m_World, col);

                    Vector3 inertia = MogreNewt.MomentOfInertia.CalcSphereSolid(10.0f, 1.0f);
                    body.SetMassMatrix(10.0f, inertia);
                    body.AttachToNode(node);
                    body.IsGravityEnabled = true;
                    body.SetPositionOrientation(pos, camorient);
                    body.Velocity = dir * 15.0f;

                    timer = 0.2f;
                }
            }

            timer -= evt.timeSinceLastFrame;

            // ---------------------------------------------------------
            // -- VEHICLE CONTORLS
            // ---------------------------------------------------------
            float torque = 0.0f;
            Degree steering = new Degree(0);

            if (inputKeyboard.IsKeyDown(MOIS.KeyCode.KC_I))
                torque += 600.0f;

            if (inputKeyboard.IsKeyDown(MOIS.KeyCode.KC_K))
                torque -= 600.0f;

            if (inputKeyboard.IsKeyDown(MOIS.KeyCode.KC_J))
                steering += new Degree(30);

            if (inputKeyboard.IsKeyDown(MOIS.KeyCode.KC_L))
                steering -= new Degree(30);

            //update the vehicle!
            mCar.setTorqueSteering(torque, steering);

            if ((inputKeyboard.IsKeyDown(MOIS.KeyCode.KC_Q)) && (!mR))
            {
                mR = true;
                // rebuild the vehicle
                if (mCar != null)
                {
                    mCar.Dispose();
                    mCar = new SimpleVehicle(sceneMgr, m_World, new Vector3(0, (float)new Random().NextDouble() * 10.0f, 0), Quaternion.IDENTITY);
                }
            }
            if (!inputKeyboard.IsKeyDown(MOIS.KeyCode.KC_Q)) { mR = false; }

            return true;
        }


        // The basic MogreNewt framelistener with time slicing
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


            // floor object! this time we'll scale it slightly to make it more vehicle-friendly :P
            Vector3 size = new Vector3(2.0f, 0.5f, 2.0f);
            Entity floor;
            SceneNode floornode;
            floor = sceneMgr.CreateEntity("Floor", "simple_terrain.mesh");
            floornode = sceneMgr.RootSceneNode.CreateChildSceneNode("FloorNode");
            floornode.AttachObject(floor);
            floor.SetMaterialName("Simple/BeachStones");
            floornode.SetScale(size);

            collisionNode = floornode;

            floor.CastShadows = false;

            //Vector3 siz(100.0, 10.0, 100.0);
            MogreNewt.Collision col = new MogreNewt.CollisionPrimitives.TreeCollision(m_World, floornode, false);
            MogreNewt.Body bod = new MogreNewt.Body(m_World, col);
            col.Dispose();

            bod.AttachToNode(floornode);
            bod.SetPositionOrientation(new Vector3(0, -2.0f, 0.0f), Quaternion.IDENTITY);

            // here's where we make the simple vehicle.  everything is taken care of in the constuctor.
            mCar = new SimpleVehicle(sceneMgr, m_World, new Vector3(0, -0.5f, 0), Quaternion.IDENTITY);



            // position camera
            camera.SetPosition(0.0f, 1.0f, 20.0f);

            //make a light
            Light light;

            light = sceneMgr.CreateLight("Light1");
            light.Type = Light.LightTypes.LT_POINT;
            light.SetPosition(0.0f, 100.0f, 100.0f);

        }

        public override void DestroyScene()
        {
            mCar.Dispose();

            m_World.Dispose();
            m_World = null;

            MogreNewt.Debugger.Instance.DeInit();
        }
    }
}
