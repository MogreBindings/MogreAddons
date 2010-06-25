/* 
	OgreNewt Library

	Ogre implementation of Newton Game Dynamics SDK

	OgreNewt basically has no license, you may use any or all of the library however you desire... I hope it can help you in any way.

		by Walaber

*/
#ifndef _INCLUDE_OGRENEWT_TOOLS
#define _INCLUDE_OGRENEWT_TOOLS


#include <Ogre.h>
#include <Newton.h>
#include "OgreNewt_Prerequisites.h"
/*#include "OgreNewt_World.h"
#include "OgreNewt_Collision.h"*/

namespace MogreNewt
{
	ref class World;
    ref class Collision;

	namespace NativeConverters
	{
		void MatrixToQuatPos( const float* matrix, Ogre::Quaternion& quat, Ogre::Vector3 &pos );
	}

	//! set of handy convertors.
	public ref class Converter
	{
	public:

		//! Take a Newton matrix and create a Quaternion + Position_vector
		/*!
			\param matrix input newton matrix (float[16])
			\param quat returned quaternion
			\param pos returned position vector
		*/
		static void MatrixToQuatPos( const float* matrix, [Out] Mogre::Quaternion% quat, [Out] Mogre::Vector3% pos );
	

		//! Take a Quaternion and Position Matrix and create a Newton-happy float matrix!
		/*!
			\param quat input quaternion
			\param pos input position vector
			\param matrix returned matrix (float[16])
		*/
		static void QuatPosToMatrix( Mogre::Quaternion quat, Mogre::Vector3 pos, float* matrix );

		//! Take a Newton matrix and make it into an Ogre::Matrix4.
		/*!
			\param matrix_in input matrix from Newton (float[16])
			\param matrix_out output Ogre::Matrix4 object.
		*/
		static void MatrixToMatrix4( const float* matrix_in, Mogre::Matrix4^ matrix_out );

		//! Take an Ogre::Matrix4 and make it into a Newton-happy matrix.
		/*!
			\param matrix_in Ogre::Matrix4 to be converted.
			\param matrix_out Newton-happy output matrix (float[16])
		*/
		static void Matrix4ToMatrix( Mogre::Matrix4^ matrix_in, float* matrix_out );
	};

	//! low-level collision commands.
	public ref class CollisionTool
	{
	public:

		//! find the point on a collision primitive closest to a global point.
		/*!
			\param world pointer to world
			\param globalpt point to check distance from.
			\param col collision primitive pointer
			\param colorient orientation of the collision primitive
			\param colpos position of the collision primitive
			\param retpos returned position on the collision primitive in global space
			\param retnormal returned normal on the collision primitive in global space
		*/
		static int CollisionPointDistance( MogreNewt::World^ world, const Mogre::Vector3 globalpt, 
									MogreNewt::Collision^ col, const Mogre::Quaternion colorient, const Mogre::Vector3 colpos, 
									[Out] Mogre::Vector3% retpt, [Out] Mogre::Vector3% retnormal ,int threadindex);
									


		//! find the nearest 2 points between 2 collision shapes.
		/*!
			\param world pointer to world
			\param colA pointer to collision primitive A
			\param colOrientA orientation of collision primitive A
			\param colPosA position of collision primitive A
			\param colB pointer to collision primitive B
			\param colOrientB orientation of collision primitive B
			\param colPosB position of collision primitive B
			\param retPosA returned position on collision primitive A
			\param retPosB returned position on collision primitive B
			\param retNorm returned collision normal
		*/
		static int CollisionClosestPoint( MogreNewt::World^ world, MogreNewt::Collision^ colA, const Mogre::Quaternion colOrientA, const Mogre::Vector3 colPosA,
															MogreNewt::Collision^ colB, const Mogre::Quaternion colOrientB, const Mogre::Vector3 colPosB,
															[Out] Mogre::Vector3% retPosA, [Out] Mogre::Vector3% retPosB, [Out] Mogre::Vector3% retNorm ,int threadindex);


		//! manual collision between collision primitives.
		/*!
			This function allows you to perform a manual collision check on 2 collision primitives.
			the function returns the number of contacts found.
			\param world pointer to world
			\param maxSize max number of contact points you can receive
			\param colA pointer to Collision primitive A
			\param colOrientA orientation of collision primitive A
			\param colPosA position of collision primitive A
			\param colB pointer to collision primitive B
			\param colOrientB orientation of collision orimitive B
			\param colPosB position of collision primitive B
			\param retContactPts returned array of contact points
			\param retNormals returned normals for each contact
			\param retPenetrations returned penetrations for each contact.
		*/
		static int CollisionCollide(  MogreNewt::World^ world, int maxSize, 
			MogreNewt::Collision^ colA, const Mogre::Quaternion colOrientA, const Mogre::Vector3 colPosA,
			MogreNewt::Collision^ colB, const Mogre::Quaternion colOrientB, const Mogre::Vector3 colPosB,
			[Out] array<Mogre::Vector3>^% retContactPts, [Out] array<Mogre::Vector3>^% retNormals, [Out] array<Mogre::Real>^% retPenetrations ,int threadindex);


		//! manual collision between moving primitives.
		/*!
			This is the most advanced collision function, that takes 2 primitives with velocities and omegas,
			and calculates if the will contact with each other over a set timestep.
			\param world pointer to world
			\param maxSize max number of contact points you can receive
			\param colA pointer to Collision primitive A
			\param colOrientA orientation of collision primitive A
			\param colPosA position of collision primitive A
			\param colVelA velocity of collision primitive A
			\param colOmegaA omega of collision primitive A
			\param colB pointer to collision primitive B
			\param colOrientB orientation of collision orimitive B
			\param colPosB position of collision primitive B
			\param colVelB velocity of collision primitive B
			\param colOmegaB omega of collision primitive B
			\param retTimeOfImpact time of collision
			\param retContactPts returned array of contact points
			\param retNormals returned normals for each contact
			\param retPenetrations returned penetrations for each contact.
		*/
		static int CollisionCollideContinue( MogreNewt::World^ world, int maxSize, Mogre::Real timeStep,
			MogreNewt::Collision^ colA, const Mogre::Quaternion colOrientA, const Mogre::Vector3 colPosA, const Mogre::Vector3 colVelA, const Mogre::Vector3 colOmegaA,
			MogreNewt::Collision^ colB, const Mogre::Quaternion colOrientB, const Mogre::Vector3 colPosB, const Mogre::Vector3 colVelB, const Mogre::Vector3 colOmegaB,
			[Out] Mogre::Real% retTimeOfImpact, [Out] array<Mogre::Vector3>^% retContactPts, [Out] array<Mogre::Vector3>^% retNormals, [Out] array<Mogre::Real>^% retPenetrations ,int threadindex);


		//! local raycast on a collision object.
		/*!
			This function performs a local raycast on a single collision primitive shape.  the function returns the distance to the
			point of intersection as a scalar between [0,1].
			\param col pointer to collision shape on which to cast the ray
			\param startPt starting point of the ray, in local space of the collision shape
			\param endPt end point of the ray, in local space of the collision shape
			\param normal returned normal where the ray hit the collision.
			\param colID returned ID of the collision primitive hit.
		*/
		static Mogre::Real CollisionRayCast( MogreNewt::Collision^ col, const Mogre::Vector3 startPt, const Mogre::Vector3 endPt, 
														[Out] Mogre::Vector3% retNorm, [Out] int% retColID );
				

		//! calculate the AABB of a collision primitive in an arbitrary orientation
		/*!
			Calculates the global AABB of a collision primitive in world space, given an orientation and position.
			\param col collision shape to calculate.
			\param orient world orientation of the collision.
			\param pos world position of the collision.
		*/
		static Mogre::AxisAlignedBox^ CollisionCalculateAABB( MogreNewt::Collision^ col, const Mogre::Quaternion orient, const Mogre::Vector3 pos );

	};




	 namespace OgreAddons
        {
            /**
            * File: MovableText.h
            *
            * description: This create create a billboarding object that display a text.
            * 
            * @author  2003 by cTh see gavocanov@rambler.ru
            * @update  2006 by barraq see nospam@barraquand.com
            */


            using namespace Ogre;

            class MovableText : public MovableObject, public Renderable
            {
                /******************************** MovableText data ****************************/
            public:
                enum HorizontalAlignment    {H_LEFT, H_CENTER};
                enum VerticalAlignment      {V_BELOW, V_ABOVE, V_CENTER};
            
            protected:
                Ogre::String          mFontName;
                Ogre::String          mType;
                Ogre::String          mName;
                Ogre::String          mCaption;
                HorizontalAlignment mHorizontalAlignment;
                VerticalAlignment   mVerticalAlignment;
            
                Ogre::ColourValue     mColor;
                Ogre::RenderOperation mRenderOp;
                Ogre::AxisAlignedBox  mAABB;
                Ogre::LightList       mLList;
            
				Ogre::Real            mCharHeight;
                Ogre::Real            mSpaceWidth;
            
                bool            mNeedUpdate;
                bool            mUpdateColors;
                bool            mOnTop;
            
                Ogre::Real            mTimeUntilNextToggle;
                Ogre::Real            mRadius;
            
                Ogre::Vector3            mGlobalTranslation;
                Ogre::Vector3            mLocalTranslation;
            
                Ogre::Camera          *mpCam;
                Ogre::RenderWindow    *mpWin;
                Ogre::Font            *mpFont;
                Ogre::MaterialPtr     mpMaterial;
                Ogre::MaterialPtr     mpBackgroundMaterial;
            
                /******************************** public methods ******************************/
            public:
                MovableText(const Ogre::String &name, const Ogre::String &caption, const Ogre::String &fontName, Real charHeight, const ColourValue &color = ColourValue::White);
                virtual ~MovableText();
            
                // Set settings
                void    setFontName(const Ogre::String &fontName);
                void    setCaption(const Ogre::String &caption);
                void    setColor(const Ogre::ColourValue &color);
                void    setCharacterHeight(Ogre::Real height);
                void    setSpaceWidth(Ogre::Real width);
                void    setTextAlignment(const HorizontalAlignment& horizontalAlignment, const VerticalAlignment& verticalAlignment);
                void    setGlobalTranslation( Ogre::Vector3 trans );
                void    setLocalTranslation( Ogre::Vector3 trans );
                void    showOnTop(bool show=true);
            
                // Get settings
                const   Ogre::String          &getFontName() const {return mFontName;}
                const   Ogre::String          &getCaption() const {return mCaption;}
                const   Ogre::ColourValue     &getColor() const {return mColor;}
                
                Ogre::Real    getCharacterHeight() const {return mCharHeight;}
                Ogre::Real    getSpaceWidth() const {return mSpaceWidth;}
                Ogre::Vector3    getGlobalTranslation() const {return mGlobalTranslation;}
                Ogre::Vector3    getLocalTranslation() const {return mLocalTranslation;}
                bool    getShowOnTop() const {return mOnTop;}
                Ogre::AxisAlignedBox          GetAABB(void) { return mAABB; }
            
                /******************************** protected methods and overload **************/
            protected:
            
                // from MovableText, create the object
                void    _setupGeometry();
                void    _updateColors();
            
                // from MovableObject
                void    getWorldTransforms(Ogre::Matrix4 *xform) const;
                Ogre::Real    getBoundingRadius(void) const {return mRadius;};
                Ogre::Real    getSquaredViewDepth(const Ogre::Camera *cam) const {return 0;};
                void visitRenderables(Ogre::Renderable::Visitor* visitor,  bool debugRenderables = false);
                const   Quaternion        &getWorldOrientation(void) const;
                const   Vector3           &getWorldPosition(void) const;
                const   AxisAlignedBox    &getBoundingBox(void) const {return mAABB;};
                const   Ogre::String            &getName(void) const {return mName;};
                const   Ogre::String            &getMovableType(void) const {static Ogre::String movType = "MovableText"; return movType;};
            
                void    _notifyCurrentCamera(Ogre::Camera *cam);
                void    _updateRenderQueue(Ogre::RenderQueue* queue);
            
                // from renderable
                void    getRenderOperation(Ogre::RenderOperation &op);
                const   Ogre::MaterialPtr       &getMaterial(void) const {assert(!mpMaterial.isNull());return mpMaterial;};
                const   Ogre::LightList         &getLights(void) const {return mLList;};
            };

        }

}	// end namespace MogreNewt

#endif

