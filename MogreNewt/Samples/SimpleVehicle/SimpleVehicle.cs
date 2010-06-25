using System;
using System.Collections.Generic;
using System.Text;
using Mogre;
using MogreNewt;

namespace MogreNewt.Demo.SimpleVehicle
{
    class SimpleVehicle : Vehicle
    {
        public class SimpleTire : Tire
        {
            private bool steeringTire;
            private SceneManager mSceneMgr;

            public bool IsSteeringTire
            {
                get { return steeringTire; }
            }

            public SimpleTire(SceneManager sceneMgr, Vector3 pin, float mass, float width, float radius, float susShock, float susSpring, float susLength, int colID, bool steer)
                : base(pin, mass, width, radius, susShock, susSpring, susLength, colID)
            {
                this.steeringTire = steer;
                this.mSceneMgr = sceneMgr;
            }

            protected override void OnAttached(Vehicle vehicle)
            {
                // first, load the visual mesh that represents the tire.
                Entity ent = mSceneMgr.CreateEntity("Tire" + (mEntityCount++), "wheel.mesh");
                // make a scene node for the tire.
                SceneNode node = mSceneMgr.RootSceneNode.CreateChildSceneNode();
                node.AttachObject(ent);
                node.SetScale(new Vector3(m_radius, m_radius, m_width));

                AttachToNode(node);

                base.OnAttached(vehicle);
            }
            protected override void OnDetached(Vehicle vehicle)
            {
                // destroy entity, and scene node.
                Entity ent = (Entity)m_node.GetAttachedObject(0);
                m_node.DetachAllObjects();
                m_node.Creator.DestroyEntity(ent);
                m_node.ParentSceneNode.RemoveAndDestroyChild(m_node.Name);

                base.OnDetached(vehicle);
            }
        }

        public SimpleVehicle(SceneManager mgr, World world, Vector3 position, Quaternion orient)
        {
            // save the scene manager
            mSceneMgr = mgr;
            mWorld = world;

            // first thing we need to do is create the rigid body for the main chassis.
            Vector3 size = new Vector3(5f, 1.2f, 2f);
            Body bod = MakeSimpleBox(size, position, orient);

            //now that we have defined the chassis, we can call the "init()" function.  this is a helper function that
            // simply sets up some internal wiring of the vehicle class that makes eveything work :)  it also calls the virtual
            // function "setup" to finish building the vehicle.
            // you pass this function the body to be used as the main chassis, and the up direction of the world (for suspension purposes).
            Init(bod, new Vector3(0, 1, 0));

            // the above function calls our "setup" function, which takes care of the rest of the vehicle setup.
        }

        protected override void OnDisposing()
        {
            // finally, destroy entity and node from chassis.
            if (m_chassis != null)
            {
                Entity ent = (Entity)(m_chassis.OgreNode as SceneNode).GetAttachedObject(0);
                (m_chassis.OgreNode as SceneNode).DetachAllObjects();
                (m_chassis.OgreNode as SceneNode).Creator.DestroyEntity(ent);
                (m_chassis.OgreNode as SceneNode).ParentSceneNode.RemoveAndDestroyChild(m_chassis.OgreNode.Name);

                Destroy();
            }

            base.OnDisposing();
        }

        // this is a function created specifically for this vehicle to allow control of it.
        public void setTorqueSteering(float torque, Degree steering) { mTorque = torque; mSteering = steering; }

        public override void Setup()
        {
            // okay, we have the main chassis all setup.  let's do a few things to it:
            m_chassis.IsGravityEnabled = true;
            // we don't want the vehicle to freeze, because we'll be unable to control it.
            m_chassis.AutoFreeze = false;


            // okay, let's add tires!
            // all offsets here are in local space of the vehicle.
            Vector3 offset = new Vector3(1.8f, -1.6f, 0.87f);

            for (int x = -1; x <= 1; x += 2)
            {
                for (int z = -1; z <= 1; z += 2)
                {

                    // okay, let's create the tire itself.  we'll use the OgreNewt::Vehicle::Tire class for this.  most of the
                    // parameters are self-explanatory... try changing some of them to see what happens.
                    Quaternion tireorient = new Quaternion(new Degree(0), Vector3.UNIT_Y);
                    Vector3 tirepos = offset * new Vector3(x, 0.5f, z);
                    Vector3 pin = new Vector3(0, 0, x);
                    float mass = 15.0f;
                    float width = 0.3f;
                    float radius = 0.5f;
                    float susShock = 30.0f;
                    float susSpring = 200.0f;
                    float susLength = 1.2f;
                    bool steering;


                    if (x > 0)
                        steering = true;
                    else
                        steering = false;

                    // create the actual tire!
                    SimpleTire tire = new SimpleTire(mSceneMgr, pin, mass, width, radius,
                        susShock, susSpring, susLength, 0, steering);

                    AttachTire(tire, tireorient, tirepos);

                }
            }
        }

        public override void UserCallback()
        {
            // loop through wheels, adding torque and steering, and updating their positions.
            foreach (SimpleTire tire in m_tires)
            {
                // set the torque and steering!  non-steering tires get the torque.

                // is this a steering tire?
                if (tire.IsSteeringTire)
                    tire.SetSteeringAngle(mSteering);
                else
                    tire.SetTorque(mTorque);

                // finally, this command updates the location of the visual mesh.
                tire.UpdateNode();
            }
        }

        static private int mEntityCount;

        SceneManager mSceneMgr;
        World mWorld;

        // for steering, etc.
        float mTorque;
        Degree mSteering;


        Body MakeSimpleBox(Vector3 size, Vector3 pos, Quaternion orient)
        {
            // base mass on the size of the object.
            float mass = size.x * size.y * size.z * 100.0f;

            // calculate the inertia based on box formula and mass
            Vector3 inertia = MogreNewt.MomentOfInertia.CalcBoxSolid(mass, size);


            Entity box1;
            SceneNode box1node;

            box1 = mSceneMgr.CreateEntity("Entity" + (mEntityCount++), "box.mesh");
            box1node = mSceneMgr.RootSceneNode.CreateChildSceneNode();
            box1node.AttachObject(box1);
            box1node.SetScale(size);
            box1.NormaliseNormals = true;

            MogreNewt.Collision col = new MogreNewt.CollisionPrimitives.Box(mWorld, size);
            MogreNewt.Body bod = new MogreNewt.Body(mWorld, col);
            col.Dispose();

            bod.AttachToNode(box1node);
            bod.SetMassMatrix(mass, inertia);
            bod.IsGravityEnabled = true;

            box1.SetMaterialName("Simple/BumpyMetal");


            bod.SetPositionOrientation(pos, orient);

            return bod;
        }
    }
}
