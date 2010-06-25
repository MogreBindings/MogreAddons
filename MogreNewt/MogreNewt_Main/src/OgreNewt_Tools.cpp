#include "OgreNewt_Tools.h"
#include "OgreNewt_World.h"
#include "OgreNewt_Body.h"
#include "OgreNewt_Collision.h"

namespace MogreNewt
{
	namespace NativeConverters
	{
	
	//! Take a Newton matrix and create a Quaternion + Position_vector
	void MatrixToQuatPos( const float* matrix, Ogre::Quaternion& quat, Ogre::Vector3 &pos )
	{
		// this takes a matrix returned by Newton, and creates a Quaternion
		// and position Vector3, which is more meaningful for Ogre.
		using namespace Ogre;
		quat = Quaternion( Matrix3(	matrix[0], matrix[4], matrix[8],
						matrix[1], matrix[5], matrix[9],
						matrix[2], matrix[6], matrix[10] ) );
	
		pos = Vector3( matrix[12], matrix[13], matrix[14] );
	}

	}

	//! Take a Newton matrix and create a Quaternion + Position_vector
	void Converter::MatrixToQuatPos( const float* matrix, Mogre::Quaternion% quat, Mogre::Vector3% pos )
	{
		// this takes a matrix returned by Newton, and creates a Quaternion
		// and position Vector3, which is more meaningful for Ogre.
		using namespace Mogre;
		quat = Quaternion( gcnew Matrix3(	matrix[0], matrix[4], matrix[8],
											matrix[1], matrix[5], matrix[9],
											matrix[2], matrix[6], matrix[10] ) );
	
		pos = Vector3( matrix[12], matrix[13], matrix[14] );
	}

	//! Take a Quaternion and Position Matrix and create a Newton-happy float matrix!
	void Converter::QuatPosToMatrix( Mogre::Quaternion quat, Mogre::Vector3 pos, float* matrix )
	{
		// this takes a Quaternion and a Vector3 and creates a float array
		// which is more meaningful to Newton.
		using namespace Mogre;
		Matrix3^ rot;
		Vector3 xcol, ycol, zcol;
		
		rot = quat.ToRotationMatrix();	// creates a 3x3 rotation matrix from the Quaternion.

		xcol = rot->GetColumn(0);
		ycol = rot->GetColumn(1);
		zcol = rot->GetColumn(2);
	
		// now fill the final matrix with the appropriate data:
		matrix[0] = xcol.x;
		matrix[1] = xcol.y;
		matrix[2] = xcol.z;
		matrix[3] = 0.0f;
	
		matrix[4] = ycol.x;
		matrix[5] = ycol.y;
		matrix[6] = ycol.z;
		matrix[7] = 0.0f;
	
		matrix[8] = zcol.x;
		matrix[9] = zcol.y;
		matrix[10] = zcol.z;
		matrix[11] = 0.0f;
	
		matrix[12] = pos.x;
		matrix[13] = pos.y;
		matrix[14] = pos.z;
		matrix[15] = 1.0;
	}

	void Converter::MatrixToMatrix4( const float* matrix_in, Mogre::Matrix4^ matrix_out )
	{
		// from Newton to Mogre::Matrix4
		matrix_out = gcnew Mogre::Matrix4( matrix_in[0], matrix_in[4], matrix_in[8], matrix_in[12],
			matrix_in[1], matrix_in[5], matrix_in[9], matrix_in[13],
			matrix_in[2], matrix_in[6], matrix_in[10], matrix_in[14],
			matrix_in[3], matrix_in[7], matrix_in[11], matrix_in[15] );
	}

	void Converter::Matrix4ToMatrix( Mogre::Matrix4^ matrix_in, float* matrix_out )
	{
		// from Mogre to Newton.
		matrix_out[0] = matrix_in->m00;
		matrix_out[1] = matrix_in->m10;
		matrix_out[2] = matrix_in->m20;
		matrix_out[3] = matrix_in->m30;

		matrix_out[4] = matrix_in->m01;
		matrix_out[5] = matrix_in->m11;
		matrix_out[6] = matrix_in->m21;
		matrix_out[7] = matrix_in->m31;

		matrix_out[8] = matrix_in->m02;
		matrix_out[9] = matrix_in->m12;
		matrix_out[10] = matrix_in->m22;
		matrix_out[11] = matrix_in->m32;

		matrix_out[12] = matrix_in->m03;
		matrix_out[13] = matrix_in->m13;
		matrix_out[14] = matrix_in->m23;
		matrix_out[15] = matrix_in->m33;
	}

	
		//! find the point on a collision primitive closest to a global point.
		
		int CollisionTool::CollisionPointDistance( MogreNewt::World^ world, const Mogre::Vector3 globalpt, 
									MogreNewt::Collision^ col, const Mogre::Quaternion colorient, const Mogre::Vector3 colpos, 
									Mogre::Vector3% retpt, Mogre::Vector3% retnormal,int threadindex )
		{
			float matrix[16];
			Converter::QuatPosToMatrix( colorient, colpos, matrix );

			pin_ptr<Mogre::Vector3> p_retpt = &retpt;
			pin_ptr<Mogre::Vector3> p_retnormal = &retnormal;
			return NewtonCollisionPointDistance( world->NewtonWorld, &globalpt.x, col->NewtonCollision, matrix, &p_retpt->x, &p_retnormal->x ,threadindex);
		}
		


		
		int CollisionTool::CollisionClosestPoint( MogreNewt::World^ world, MogreNewt::Collision^ colA, const Mogre::Quaternion colOrientA, const Mogre::Vector3 colPosA,
															MogreNewt::Collision^ colB, const Mogre::Quaternion colOrientB, const Mogre::Vector3 colPosB,
															Mogre::Vector3% retPosA, Mogre::Vector3% retPosB, Mogre::Vector3% retNorm,int threadindex )
		{
			float matrixA[16];
			float matrixB[16];

			Converter::QuatPosToMatrix( colOrientA, colPosA, matrixA );
			Converter::QuatPosToMatrix( colOrientB, colPosB, matrixB );

			pin_ptr<Mogre::Vector3> p_retPosA = &retPosA;
			pin_ptr<Mogre::Vector3> p_retPosB = &retPosB;
			pin_ptr<Mogre::Vector3> p_retNorm = &retNorm;
			return NewtonCollisionClosestPoint( world->NewtonWorld, colA->NewtonCollision, matrixA, colB->NewtonCollision, matrixB,
												&p_retPosA->x, &p_retPosB->x, &p_retNorm->x ,threadindex);
		}


		int CollisionTool::CollisionCollide(  MogreNewt::World^ world, int maxSize, 
			MogreNewt::Collision^ colA, const Mogre::Quaternion colOrientA, const Mogre::Vector3 colPosA,
			MogreNewt::Collision^ colB, const Mogre::Quaternion colOrientB, const Mogre::Vector3 colPosB,
			array<Mogre::Vector3>^% retContactPts, array<Mogre::Vector3>^% retNormals, array<Mogre::Real>^% retPenetrations,int threadindex )
		{
			float matrixA[16];
			float matrixB[16];

			Converter::QuatPosToMatrix( colOrientA, colPosA, matrixA );
			Converter::QuatPosToMatrix( colOrientB, colPosB, matrixB );

			retContactPts = gcnew array<Mogre::Vector3>( maxSize );
			retNormals = gcnew array<Mogre::Vector3>( maxSize );
			retPenetrations = gcnew array<Mogre::Real>( maxSize );

			pin_ptr<Mogre::Vector3>	p_contactPts =		&retContactPts[0];
			pin_ptr<Mogre::Vector3>	p_normals =			&retNormals[0];
			pin_ptr<Mogre::Real>		p_penetrations =	&retPenetrations[0];

			int num = NewtonCollisionCollide( world->NewtonWorld, maxSize, colA->NewtonCollision, matrixA,
						colB->NewtonCollision, matrixB, &p_contactPts->x, &p_normals->x, p_penetrations ,threadindex);

			Array::Resize<Mogre::Vector3>( retContactPts, num );			
			Array::Resize<Mogre::Vector3>( retNormals, num );			
			Array::Resize<Mogre::Real>( retPenetrations, num );

			return num;
		}


		int CollisionTool::CollisionCollideContinue( MogreNewt::World^ world, int maxSize, Mogre::Real timeStep,
			MogreNewt::Collision^ colA, const Mogre::Quaternion colOrientA, const Mogre::Vector3 colPosA, const Mogre::Vector3 colVelA, const Mogre::Vector3 colOmegaA,
			MogreNewt::Collision^ colB, const Mogre::Quaternion colOrientB, const Mogre::Vector3 colPosB, const Mogre::Vector3 colVelB, const Mogre::Vector3 colOmegaB,
			Mogre::Real% retTimeOfImpact, array<Mogre::Vector3>^% retContactPts, array<Mogre::Vector3>^% retNormals, array<Mogre::Real>^% retPenetrations,int threadindex )
		{
			float matrixA[16];
			float matrixB[16];

			Converter::QuatPosToMatrix( colOrientA, colPosA, matrixA );
			Converter::QuatPosToMatrix( colOrientB, colPosB, matrixB );

			retContactPts = gcnew array<Mogre::Vector3>( maxSize );
			retNormals = gcnew array<Mogre::Vector3>( maxSize );
			retPenetrations = gcnew array<Mogre::Real>( maxSize );

			pin_ptr<Mogre::Real>		p_timeOfImpact =	&retTimeOfImpact;
			pin_ptr<Mogre::Vector3>	p_contactPts =		&retContactPts[0];
			pin_ptr<Mogre::Vector3>	p_normals =			&retNormals[0];
			pin_ptr<Mogre::Real>		p_penetrations =	&retPenetrations[0];

			int num = NewtonCollisionCollideContinue( world->NewtonWorld, maxSize, timeStep,
				colA->NewtonCollision, matrixA, &colVelA.x, &colOmegaA.x, 
				colB->NewtonCollision, matrixB, &colVelB.x, &colOmegaB.x,
				p_timeOfImpact, &p_contactPts->x, &p_normals->x, p_penetrations, threadindex );

			Array::Resize<Mogre::Vector3>( retContactPts, num );			
			Array::Resize<Mogre::Vector3>( retNormals, num );			
			Array::Resize<Mogre::Real>( retPenetrations, num );

			return num;
		}


		Mogre::Real CollisionTool::CollisionRayCast( MogreNewt::Collision^ col, const Mogre::Vector3 startPt, const Mogre::Vector3 endPt, 
			Mogre::Vector3% retNorm, int% retColID )
		{
			pin_ptr<Mogre::Vector3> p_retNorm = &retNorm;
			pin_ptr<int> p_retColID = &retColID;
			return NewtonCollisionRayCast( col->NewtonCollision, &startPt.x, &endPt.x, &p_retNorm->x, p_retColID );
		}

		Mogre::AxisAlignedBox^ CollisionTool::CollisionCalculateAABB( MogreNewt::Collision^ col, const Mogre::Quaternion orient, const Mogre::Vector3 pos )
		{
			float matrix[16];
			Converter::QuatPosToMatrix( orient, pos, matrix );
			Mogre::Vector3 min, max;

			NewtonCollisionCalculateAABB( col->NewtonCollision, matrix, &min.x, &max.x );

			return gcnew Mogre::AxisAlignedBox( min, max );
		}

		namespace OgreAddons
        {
            /**
             * File: MovableText.cpp
             *
             * description: This create create a billboarding object that display a text.
             * 
             * @author  2003 by cTh see gavocanov@rambler.ru
             * @update  2006 by barraq see nospam@barraquand.com
            */


            using namespace Ogre;

            const unsigned short POS_TEX_BINDING = 0;
            const unsigned short COLOUR_BINDING = 1;

            MovableText::MovableText(const Ogre::String &name, const Ogre::String &caption, const Ogre::String &fontName, Ogre::Real charHeight, const Ogre::ColourValue &color)
                : mpCam(NULL)
                  , mpWin(NULL)
                  , mpFont(NULL)
                  , mName(name)
                  , mCaption(caption)
                  , mFontName(fontName)
                  , mCharHeight(charHeight)
                  , mColor(color)
                  , mType("MovableText")
                  , mTimeUntilNextToggle(0)
                  , mSpaceWidth(0)
                  , mUpdateColors(true)
                  , mOnTop(false)
                  , mHorizontalAlignment(H_LEFT)
                  , mVerticalAlignment(V_BELOW)
                  , mGlobalTranslation(0.0)
                  , mLocalTranslation(0.0)
            {
                if (name == "")
                    throw Ogre::Exception(Ogre::Exception::ERR_INVALIDPARAMS, "Trying to create MovableText without name", "MovableText::MovableText");

                if (caption == "")
                    throw Ogre::Exception(Ogre::Exception::ERR_INVALIDPARAMS, "Trying to create MovableText without caption", "MovableText::MovableText");

                mRenderOp.vertexData = NULL;
                this->setFontName(mFontName);
                this->_setupGeometry();
            }

            MovableText::~MovableText()
            {
                if (mRenderOp.vertexData)
                    delete mRenderOp.vertexData;
                // May cause crashing... check this and comment if it does
                if (!mpMaterial.isNull())
                    MaterialManager::getSingletonPtr()->remove(mpMaterial->getName());
            }

            void MovableText::setFontName(const Ogre::String &fontName)
            {
                if((Ogre::MaterialManager::getSingletonPtr()->resourceExists(mName + "Material"))) 
                { 
                    Ogre::MaterialManager::getSingleton().remove(mName + "Material"); 
                }

                if (mFontName != fontName || mpMaterial.isNull() || !mpFont)
                {
                    mFontName = fontName;
					mpFont = (Ogre::Font *)Ogre::FontManager::getSingleton().getByName(mFontName).getPointer();
                    if (!mpFont)
                        throw Ogre::Exception(Ogre::Exception::ERR_ITEM_NOT_FOUND, "Could not find font " + fontName, "MovableText::setFontName");

                    mpFont->load();
                    if (!mpMaterial.isNull())
                    {
                        Ogre::MaterialManager::getSingletonPtr()->remove(mpMaterial->getName());
                        mpMaterial.setNull();
                    }

                    mpMaterial = mpFont->getMaterial()->clone(mName + "Material");
                    if (!mpMaterial->isLoaded())
                        mpMaterial->load();

                    mpMaterial->setDepthCheckEnabled(!mOnTop);
                    mpMaterial->setDepthBias(1.0,1.0);
                    mpMaterial->setDepthWriteEnabled(mOnTop);
                    mpMaterial->setLightingEnabled(false);
                    mNeedUpdate = true;
                }
            }

            void MovableText::setCaption(const Ogre::String &caption)
            {
                if (caption != mCaption)
                {
                    mCaption = caption;
                    mNeedUpdate = true;
                }
            }

            void MovableText::setColor(const Ogre::ColourValue &color)
            {
                if (color != mColor)
                {
                    mColor = color;
                    mUpdateColors = true;
                }
            }

            void MovableText::setCharacterHeight(Ogre::Real height)
            {
                if (height != mCharHeight)
                {
                    mCharHeight = height;
                    mNeedUpdate = true;
                }
            }

            void MovableText::setSpaceWidth(Ogre::Real width)
            {
                if (width != mSpaceWidth)
                {
                    mSpaceWidth = width;
                    mNeedUpdate = true;
                }
            }

            void MovableText::setTextAlignment(const HorizontalAlignment& horizontalAlignment, const VerticalAlignment& verticalAlignment)
            {
                if(mHorizontalAlignment != horizontalAlignment)
                {
                    mHorizontalAlignment = horizontalAlignment;
                    mNeedUpdate = true;
                }
                if(mVerticalAlignment != verticalAlignment)
                {
                    mVerticalAlignment = verticalAlignment;
                    mNeedUpdate = true;
                }
            }

            void MovableText::setGlobalTranslation( Ogre::Vector3 trans )
            {
                mGlobalTranslation = trans;
            }

            void MovableText::setLocalTranslation( Ogre::Vector3 trans )
            {
                mLocalTranslation = trans;
            }

            void MovableText::showOnTop(bool show)
            {
                if( mOnTop != show && !mpMaterial.isNull() )
                {
                    mOnTop = show;
                    mpMaterial->setDepthBias(1.0,1.0);
                    mpMaterial->setDepthCheckEnabled(!mOnTop);
                    mpMaterial->setDepthWriteEnabled(mOnTop);
                }
            }

            void MovableText::_setupGeometry()
            {
                assert(mpFont);
                assert(!mpMaterial.isNull());

                unsigned int vertexCount = static_cast<unsigned int>(mCaption.size() * 6);

                if (mRenderOp.vertexData)
                {
                    // Removed this test as it causes problems when replacing a caption
                    // of the same size: replacing "Hello" with "hello"
                    // as well as when changing the text alignment
                    //if (mRenderOp.vertexData->vertexCount != vertexCount)
                    {
                        delete mRenderOp.vertexData;
                        mRenderOp.vertexData = NULL;
                        mUpdateColors = true;
                    }
                }

                if (!mRenderOp.vertexData)
                    mRenderOp.vertexData = new VertexData();

                mRenderOp.indexData = 0;
                mRenderOp.vertexData->vertexStart = 0;
                mRenderOp.vertexData->vertexCount = vertexCount;
                mRenderOp.operationType = RenderOperation::OT_TRIANGLE_LIST; 
                mRenderOp.useIndexes = false; 

                Ogre::VertexDeclaration  *decl = mRenderOp.vertexData->vertexDeclaration;
                Ogre::VertexBufferBinding   *bind = mRenderOp.vertexData->vertexBufferBinding;
                size_t offset = 0;

                // create/bind positions/tex.ccord. buffer
				if (!decl->findElementBySemantic(Ogre::VES_POSITION))
					decl->addElement(POS_TEX_BINDING, offset, Ogre::VET_FLOAT3, Ogre::VES_POSITION);

                offset += Ogre::VertexElement::getTypeSize(VET_FLOAT3);

                if (!decl->findElementBySemantic(VES_TEXTURE_COORDINATES))
					decl->addElement(POS_TEX_BINDING, offset, Ogre::VET_FLOAT2, Ogre::VES_TEXTURE_COORDINATES, 0);

                Ogre::HardwareVertexBufferSharedPtr ptbuf = Ogre::HardwareBufferManager::getSingleton().createVertexBuffer(decl->getVertexSize(POS_TEX_BINDING),
                        mRenderOp.vertexData->vertexCount,
                        Ogre::HardwareBuffer::HBU_DYNAMIC_WRITE_ONLY);
                bind->setBinding(POS_TEX_BINDING, ptbuf);

                // Colours - store these in a separate buffer because they change less often
                if (!decl->findElementBySemantic(Ogre::VES_DIFFUSE))
                    decl->addElement(COLOUR_BINDING, 0, Ogre::VET_COLOUR, Ogre::VES_DIFFUSE);

                HardwareVertexBufferSharedPtr cbuf = HardwareBufferManager::getSingleton().createVertexBuffer(decl->getVertexSize(COLOUR_BINDING),
                        mRenderOp.vertexData->vertexCount,
                        Ogre::HardwareBuffer::HBU_DYNAMIC_WRITE_ONLY);
                bind->setBinding(COLOUR_BINDING, cbuf);

                size_t charlen = mCaption.size();
                float *pPCBuff = static_cast<float*>(ptbuf->lock(Ogre::HardwareBuffer::HBL_DISCARD));

                float largestWidth = 0;
                float left = 0 * 2.0 - 1.0;
                float top = -((0 * 2.0) - 1.0);

                Ogre::Real spaceWidth = mSpaceWidth;
                // Derive space width from a capital A
                if (spaceWidth == 0)
                    spaceWidth = mpFont->getGlyphAspectRatio('A') * mCharHeight * 2.0;

                // for calculation of AABB
                Ogre::Vector3 min, max, currPos;
                Ogre::Real maxSquaredRadius;
                bool first = true;

                // Use iterator
                Ogre::String::iterator i, iend;
                iend = mCaption.end();
                bool newLine = true;
                Real len = 0.0f;

                Ogre::Real verticalOffset = 0;
                switch (mVerticalAlignment)
                {
                    case MovableText::V_ABOVE:
                        verticalOffset = mCharHeight;
                        break;
                    case MovableText::V_CENTER:
                        verticalOffset = 0.5*mCharHeight;
                        break;
                    case MovableText::V_BELOW:
                        verticalOffset = 0;
                        break;
                }
                // Raise the first line of the caption
                top += verticalOffset;
                for (i = mCaption.begin(); i != iend; ++i)
                {
                    if (*i == '\n')
                        top += verticalOffset * 2.0;
                }

                for (i = mCaption.begin(); i != iend; ++i)
                {
                    if (newLine)
                    {
                        len = 0.0f;
						for (Ogre::String::iterator j = i; j != iend && *j != '\n'; j++)
                        {
                            if (*j == ' ')
                                len += spaceWidth;
                            else 
                                len += mpFont->getGlyphAspectRatio(*j) * mCharHeight * 2.0;
                        }
                        newLine = false;
                    }

                    if (*i == '\n')
                    {
                        left = 0 * 2.0 - 1.0;
                        top -= mCharHeight * 2.0;
                        newLine = true;
                        continue;
                    }

                    if (*i == ' ')
                    {
                        // Just leave a gap, no tris
                        left += spaceWidth;
                        // Also reduce tri count
                        mRenderOp.vertexData->vertexCount -= 6;
                        continue;
                    }

                    Ogre::Real horiz_height = mpFont->getGlyphAspectRatio(*i);
                    Ogre::Real u1, u2, v1, v2; 
                    Ogre::Font::UVRect utmp;
                    utmp = mpFont->getGlyphTexCoords(*i);
                    u1 = utmp.left;
                    u2 = utmp.right;
                    v1 = utmp.top;
                    v2 = utmp.bottom;

                    // each vert is (x, y, z, u, v)
                    //-------------------------------------------------------------------------------------
                    // First tri
                    //
                    // Upper left
                    if(mHorizontalAlignment == MovableText::H_LEFT)
                        *pPCBuff++ = left;
                    else
                        *pPCBuff++ = left - (len / 2);
                    *pPCBuff++ = top;
                    *pPCBuff++ = -1.0;
                    *pPCBuff++ = u1;
                    *pPCBuff++ = v1;

                    // Deal with bounds
                    if(mHorizontalAlignment == MovableText::H_LEFT)
                        currPos = Ogre::Vector3(left, top, -1.0);
                    else
                        currPos = Ogre::Vector3(left - (len / 2), top, -1.0);
                    if (first)
                    {
                        min = max = currPos;
                        maxSquaredRadius = currPos.squaredLength();
                        first = false;
                    }
                    else
                    {
                        min.makeFloor(currPos);
                        max.makeCeil(currPos);
                        maxSquaredRadius = std::max(maxSquaredRadius, currPos.squaredLength());
                    }

                    top -= mCharHeight * 2.0;

                    // Bottom left
                    if(mHorizontalAlignment == MovableText::H_LEFT)
                        *pPCBuff++ = left;
                    else
                        *pPCBuff++ = left - (len / 2);
                    *pPCBuff++ = top;
                    *pPCBuff++ = -1.0;
                    *pPCBuff++ = u1;
                    *pPCBuff++ = v2;

                    // Deal with bounds
                    if(mHorizontalAlignment == MovableText::H_LEFT)
                        currPos = Ogre::Vector3(left, top, -1.0);
                    else
                        currPos = Ogre::Vector3(left - (len / 2), top, -1.0);
                    min.makeFloor(currPos);
                    max.makeCeil(currPos);
                    maxSquaredRadius = std::max(maxSquaredRadius, currPos.squaredLength());

                    top += mCharHeight * 2.0;
                    left += horiz_height * mCharHeight * 2.0;

                    // Top right
                    if(mHorizontalAlignment == MovableText::H_LEFT)
                        *pPCBuff++ = left;
                    else
                        *pPCBuff++ = left - (len / 2);
                    *pPCBuff++ = top;
                    *pPCBuff++ = -1.0;
                    *pPCBuff++ = u2;
                    *pPCBuff++ = v1;
                    //-------------------------------------------------------------------------------------

                    // Deal with bounds
                    if(mHorizontalAlignment == MovableText::H_LEFT)
                        currPos = Ogre::Vector3(left, top, -1.0);
                    else
                        currPos = Ogre::Vector3(left - (len / 2), top, -1.0);
                    min.makeFloor(currPos);
                    max.makeCeil(currPos);
                    maxSquaredRadius = std::max(maxSquaredRadius, currPos.squaredLength());

                    //-------------------------------------------------------------------------------------
                    // Second tri
                    //
                    // Top right (again)
                    if(mHorizontalAlignment == MovableText::H_LEFT)
                        *pPCBuff++ = left;
                    else
                        *pPCBuff++ = left - (len / 2);
                    *pPCBuff++ = top;
                    *pPCBuff++ = -1.0;
                    *pPCBuff++ = u2;
                    *pPCBuff++ = v1;

                    currPos = Ogre::Vector3(left, top, -1.0);
                    min.makeFloor(currPos);
                    max.makeCeil(currPos);
                    maxSquaredRadius = std::max(maxSquaredRadius, currPos.squaredLength());

                    top -= mCharHeight * 2.0;
                    left -= horiz_height  * mCharHeight * 2.0;

                    // Bottom left (again)
                    if(mHorizontalAlignment == MovableText::H_LEFT)
                        *pPCBuff++ = left;
                    else
                        *pPCBuff++ = left - (len / 2);
                    *pPCBuff++ = top;
                    *pPCBuff++ = -1.0;
                    *pPCBuff++ = u1;
                    *pPCBuff++ = v2;

                    currPos = Ogre::Vector3(left, top, -1.0);
                    min.makeFloor(currPos);
                    max.makeCeil(currPos);
                    maxSquaredRadius = std::max(maxSquaredRadius, currPos.squaredLength());

                    left += horiz_height  * mCharHeight * 2.0;

                    // Bottom right
                    if(mHorizontalAlignment == MovableText::H_LEFT)
                        *pPCBuff++ = left;
                    else
                        *pPCBuff++ = left - (len / 2);
                    *pPCBuff++ = top;
                    *pPCBuff++ = -1.0;
                    *pPCBuff++ = u2;
                    *pPCBuff++ = v2;
                    //-------------------------------------------------------------------------------------

                    currPos = Ogre::Vector3(left, top, -1.0);
                    min.makeFloor(currPos);
                    max.makeCeil(currPos);
                    maxSquaredRadius = std::max(maxSquaredRadius, currPos.squaredLength());

                    // Go back up with top
                    top += mCharHeight * 2.0;

                    float currentWidth = (left + 1)/2 - 0;
                    if (currentWidth > largestWidth)
                        largestWidth = currentWidth;
                }

                // Unlock vertex buffer
                ptbuf->unlock();

                // update AABB/Sphere radius
                mAABB = Ogre::AxisAlignedBox(min, max);
                mRadius = Ogre::Math::Sqrt(maxSquaredRadius);

                if (mUpdateColors)
                    this->_updateColors();

                mNeedUpdate = false;
            }

            void MovableText::_updateColors(void)
            {
                assert(mpFont);
                assert(!mpMaterial.isNull());

                // Convert to system-specific
                RGBA color;
                Root::getSingleton().convertColourValue(mColor, &color);
                Ogre::HardwareVertexBufferSharedPtr vbuf = mRenderOp.vertexData->vertexBufferBinding->getBuffer(COLOUR_BINDING);
                RGBA *pDest = static_cast<RGBA*>(vbuf->lock(Ogre::HardwareBuffer::HBL_DISCARD));
                for (int i = 0; i < (int)mRenderOp.vertexData->vertexCount; ++i)
                    *pDest++ = color;
                vbuf->unlock();
                mUpdateColors = false;
            }

            const Ogre::Quaternion& MovableText::getWorldOrientation(void) const
            {
                assert(mpCam);
                return const_cast<Quaternion&>(mpCam->getDerivedOrientation());
            }

            const Ogre::Vector3& MovableText::getWorldPosition(void) const
            {
                assert(mParentNode);
                return mParentNode->_getDerivedPosition();
            }

            void MovableText::getWorldTransforms(Ogre::Matrix4 *xform) const 
            {
                if (this->isVisible() && mpCam)
                {
                    Ogre::Matrix3 rot3x3, scale3x3 = Ogre::Matrix3::IDENTITY;

                    // store rotation in a matrix
                    mpCam->getDerivedOrientation().ToRotationMatrix(rot3x3);

                    // parent node position
                    Ogre::Vector3 ppos = mParentNode->_getDerivedPosition() + Ogre::Vector3::UNIT_Y*mGlobalTranslation;
                    ppos += rot3x3*mLocalTranslation;

                    // apply scale
                    scale3x3[0][0] = mParentNode->_getDerivedScale().x / 2;
                    scale3x3[1][1] = mParentNode->_getDerivedScale().y / 2;
                    scale3x3[2][2] = mParentNode->_getDerivedScale().z / 2;

                    // apply all transforms to xform       
                    *xform = (rot3x3 * scale3x3);
                    xform->setTrans(ppos);
                }
            }

            void MovableText::getRenderOperation(Ogre::RenderOperation &op)
            {
                if (this->isVisible())
                {
                    if (mNeedUpdate)
                        this->_setupGeometry();
                    if (mUpdateColors)
                        this->_updateColors();
                    op = mRenderOp;
                }
            }

            void MovableText::_notifyCurrentCamera(Ogre::Camera *cam)
            {
                mpCam = cam;
            }

            void MovableText::_updateRenderQueue(Ogre::RenderQueue* queue)
            {
                if (this->isVisible())
                {
                    if (mNeedUpdate)
                        this->_setupGeometry();
                    if (mUpdateColors)
                        this->_updateColors();

                    queue->addRenderable(this, mRenderQueueID, OGRE_RENDERABLE_DEFAULT_PRIORITY);
                    //queue->addRenderable(this, mRenderQueueID, RENDER_QUEUE_SKIES_LATE);
                }
            }

            void MovableText::visitRenderables(Ogre::Renderable::Visitor* visitor, bool debugRenderables)
            {
                visitor->visit(this, 0, debugRenderables);
            }
        }
		

}


