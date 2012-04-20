using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MParticleUniverse.ParticleRenderers
{
    public interface PrimitiveShapeSet // : Mogre.MovableObject, Mogre.Renderable
    {
        ///** Default constructor.
        //    */
        //    PrimitiveShapeSet(String name, uint poolSize = 20, bool externalData = false);

        //    /** Default constructor.
        //    */
        //    PrimitiveShapeSet();

			/** Set indication whether the 'Z' rotation is activated. Z rotation is a 2D rotation effect and
				for a primitive shapeset implemented as a texture rotation.
			*/
			//void setZRotated(bool zRotated);

			/** Returns true if the Z rotation has been set. This causes the textures to rotate.
			*/
			bool ZRotated{get; set;} //();

			/** Internal callback used by primitive shapes to notify their parent that they have been rotated 
				around the Z-axis.
			*/
			void _notifyZRotated();

			/** Sets the name of the material to be used for this primitive shapeset.
			*/
            String MaterialName { get; set; }

			/** Gets the name of the material to be used for this primitive shapeset.
			*/
			//String getMaterialName();

			/** Internal callback used by primitive shapes to notify their parent that they have been resized.
		    */
			void _notifyResized();

			/** Overridden from MovableObject
			@see
				MovableObject
			*/
			void _notifyCurrentCamera(Mogre.Camera cam);

			/** Overridden from MovableObject
			@see
				MovableObject
			*/
            Mogre.AxisAlignedBox BoundingBox{get;}

			/** Overridden from MovableObject
			@see
				MovableObject
			*/
            float BoundingRadius { get; }

			/** Overridden from MovableObject
			@see
				MovableObject
			*/
            Mogre.MaterialPtr Material { get; }

			/** Overridden from MovableObject
			@see
				MovableObject
			*/
            Mogre.Matrix4 WorldTransforms { get; }

			/** @copydoc Renderable::getWorldOrientation */
            Mogre.Quaternion WorldOrientation { get; }

			/** @copydoc Renderable::getWorldPosition */
            Mogre.Vector3 WorldPosition { get; }

			/** Returns whether or not primitive shapes in this primitive shapeset are tested individually for culling.
			*/
            bool CullIndividually { get; set; }

			/** Sets whether culling tests primitive shapes in this primitive shapeset individually as well as in a group.
			*/
			//void setCullIndividually(bool cullIndividual);

			/** Overridden, see Renderable
			*/
			float GetSquaredViewDepth(Mogre.Camera cam);

			/** @copydoc Renderable::getLights 
			*/
            Mogre.LightList Lights { get; }

			/** Override to return specific type flag
			*/
            uint TypeFlags { get; }

			/** Rotate Texture
		    */
			void RotateTexture(float speed);

            ///** @see MovableObject
            //*/
            //virtual void visitRenderables(Ogre::Renderable::Visitor* visitor,
            //    bool debugRenderables = false) {/* No implementation */};
    }
}
