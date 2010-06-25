#include "OgreNewt_RayCast.h"


namespace MogreNewt
{

	Raycast::Raycast()
	{
		m_NewtonWorldRayFilterCallbackDelegate = gcnew NewtonWorldRayFilterCallbackDelegate( this, &Raycast::NewtonRaycastFilter );
		m_funcptr_newtonRaycastFilter = (NewtonWorldRayFilterCallback) Marshal::GetFunctionPointerForDelegate( m_NewtonWorldRayFilterCallbackDelegate ).ToPointer();

		m_NewtonWorldRayPrefilterCallbackDelegate = gcnew NewtonWorldRayPrefilterCallbackDelegate( this, &Raycast::NewtonRaycastPreFilter );
		m_funcptr_newtonRaycastPreFilter = (NewtonWorldRayPrefilterCallback) Marshal::GetFunctionPointerForDelegate( m_NewtonWorldRayPrefilterCallbackDelegate ).ToPointer();
	}


	void Raycast::Go(MogreNewt::World^ world, const Mogre::Vector3 startpt, const Mogre::Vector3 endpt )
	{
		if(world->DebuggerInstance->IsRaycastRecording)
		{
			world->DebuggerInstance->AddRay(startpt,endpt);
		}


		m_lastbody = nullptr;
		// perform the raycast!
		NewtonWorldRayCast( world->NewtonWorld, (float*)&startpt, (float*)&endpt, m_funcptr_newtonRaycastFilter, NULL, m_funcptr_newtonRaycastPreFilter );

		m_lastbody = nullptr;
	}

	float Raycast::NewtonRaycastFilter(const NewtonBody* body, const float* hitNormal, int collisionID, void* userData, float intersectParam)
	{
		if(this->bodyalreadyadded)
			return intersectParam;

		Body^ bod = static_cast<BodyNativeInfo*>( NewtonBodyGetUserData( body ) )->managedBody;
		Mogre::Vector3 normal = Mogre::Vector3( hitNormal[0], hitNormal[1], hitNormal[2] );

		if (this->UserCallback( bod, intersectParam, normal, collisionID ))
			return intersectParam;
		else
			return 1.1;

	}

	unsigned Raycast::NewtonRaycastPreFilter(const NewtonBody *body, const NewtonCollision *collision, void* userData)
	{
		this->bodyalreadyadded = false;

		Body^ bod = static_cast<BodyNativeInfo*>( NewtonBodyGetUserData( body ) )->managedBody;

		this->m_lastbody = bod;

		if (this->UserPreFilterCallback( bod ))
			return 1;
		else
			return 0;
	}



	//--------------------------------
	BasicRaycast::BasicRaycastInfo::BasicRaycastInfo()
	{
		mBody = nullptr;
		mDistance = -1.0;
		mNormal = Mogre::Vector3::ZERO;
	}


	BasicRaycast::BasicRaycast() : Raycast()
	{
		mRayList = gcnew RaycastInfoList();
	}

	BasicRaycast::BasicRaycast(MogreNewt::World^ world, const Mogre::Vector3 startpt, const Mogre::Vector3 endpt ) : Raycast() 
	{
		mRayList = gcnew RaycastInfoList();
		Go( world, startpt, endpt );
	}


  int BasicRaycast::HitCount::get() { return mRayList->Count; }


  BasicRaycast::BasicRaycastInfo^ BasicRaycast::FirstHit::get()
	{
		//return the closest hit...
		BasicRaycast::BasicRaycastInfo^ ret;

		Mogre::Real dist = 10000.0;

		for (int i=0; i < mRayList->Count; i++)
		{
			BasicRaycastInfo^ info = mRayList[i];

			if (info->mDistance < dist)
			{
				ret = info;
			}
		}

		return ret;
	}


	BasicRaycast::BasicRaycastInfo^ BasicRaycast::GetInfoAt( int hitnum )
	{
		if ((hitnum < 0) || (hitnum > mRayList->Count))
			return nullptr;

		return mRayList[hitnum];
	}

	bool BasicRaycast::UserCallback( MogreNewt::Body^ body, Mogre::Real distance, const Mogre::Vector3 normal, int collisionID )
	{
		// create a new infor object.
		BasicRaycast::BasicRaycastInfo^ newinfo = gcnew BasicRaycastInfo;

		newinfo->mBody = body;
		newinfo->mDistance = distance;
		newinfo->mNormal = normal;
		newinfo->mCollisionID = collisionID;

		mRayList->Add( newinfo );

		return false;
	}





	ConvexCast::ConvexCast()
	{
		m_NewtonConvexcastPreFilterCallbackDelegate = gcnew NewtonConvexcastPreFilterCallbackDelegate(this,&ConvexCast::NewtonConvexcastPreFilter);
		m_funcptr_newtonConvexCastPreFilter = (NewtonWorldRayPrefilterCallback)Marshal::GetFunctionPointerForDelegate(m_NewtonConvexcastPreFilterCallbackDelegate).ToPointer();

	}

	void ConvexCast::Go(MogreNewt::World^ world,MogreNewt::ConvexCollision^ col,const Mogre::Vector3 startpt,const Mogre::Quaternion colOrientation,const Mogre::Vector3 endpt,int MaxContactsCount,int ThreadIndex)
	{
		if(world->DebuggerInstance->IsRaycastRecording)
		{
			world->DebuggerInstance->AddConvexRay(col,startpt,colOrientation, endpt);
		}

		 // reserve memory
                if( mReturnInfoListSize < MaxContactsCount )
                {
                    mReturnInfoListSize = 0;
                    if( mReturnInfoList )
                        delete[] mReturnInfoList;
                    mReturnInfoList = new NewtonWorldConvexCastReturnInfo[MaxContactsCount];
                    mReturnInfoListSize = MaxContactsCount;
                }

                memset(mReturnInfoList, 0, sizeof(mReturnInfoList[0])*mReturnInfoListSize);
        // perform the cast
                float matrix[16];
				MogreNewt::Converter::QuatPosToMatrix(colOrientation,startpt,&matrix[0]);
                mFirstContactDistance = -1;

				pin_ptr<Mogre::Real> pmFirstContactDistance = &mFirstContactDistance;

                mReturnInfoListLength = 
					NewtonWorldConvexCast( world->NewtonWorld, &matrix[0], (float*)&endpt, col->NewtonCollision,
                              pmFirstContactDistance, NULL, m_funcptr_newtonConvexCastPreFilter,
                              mReturnInfoList, mReturnInfoListSize, ThreadIndex);


				
            if( world->DebuggerInstance->IsRaycastRecording && world->DebuggerInstance->IsRaycastRecordingHitBodies )
            {
				MogreNewt::Body^ body;
                for(int i = 0; i < mReturnInfoListLength; i++)
                {
                    body = static_cast<BodyNativeInfo*>( NewtonBodyGetUserData( mReturnInfoList[i].m_hitBody ) )->managedBody;
					world->DebuggerInstance->AddHitBody(body);
                }
            }
	}


	unsigned ConvexCast::NewtonConvexcastPreFilter(const NewtonBody *body, const NewtonCollision *collision, void* userData)
	{
		Body^ bod = static_cast<BodyNativeInfo*>( NewtonBodyGetUserData( body ) )->managedBody;

		World^ world = bod->World;

		if (this->UserPreFilterCallback( bod ))
			return 1;
		else
		{
			if(world->DebuggerInstance->IsRaycastRecording && world->DebuggerInstance->IsRaycastRecordingHitBodies)
			{
				world->DebuggerInstance->AddDiscardedBody(bod);
			}

			return 0;
		}
	}

	BasicConvexCast::ConvexcastContactInfo::ConvexcastContactInfo()
	{
		mBody = nullptr;
	}

	BasicConvexCast::BasicConvexCast() : ConvexCast()
	{
	}

	BasicConvexCast::BasicConvexCast(MogreNewt::World ^world, MogreNewt::ConvexCollision ^col, const Mogre::Vector3 startpt, const Mogre::Quaternion colori, const Mogre::Vector3 endpt, int maxcontactscount, int threadIndex): ConvexCast()
	{
		Go(world,col,startpt,colori,endpt,maxcontactscount,threadIndex);
	}


	int BasicConvexCast::HitCount::get() 
	{
		 int count = 0;
        for( int i = 0; i < mReturnInfoListLength; i++ )
        {
            int j;
            for( j = 0; j < i; j++ )
                if( mReturnInfoList[i].m_hitBody == mReturnInfoList[j].m_hitBody )
                    break;

            if( j == i )
                count++;
        }

        return count;
	}

	int BasicConvexCast::ContactsCount::get()
	{
		return mReturnInfoListLength;
	}

	BasicConvexCast::ConvexcastContactInfo^ BasicConvexCast::GetInfoAt(int hitnum)
	{
		BasicConvexCast::ConvexcastContactInfo^ info = gcnew ConvexcastContactInfo();

            if( hitnum < 0 || hitnum >= mReturnInfoListLength )
                return info;

		info->mBody =  static_cast<BodyNativeInfo*>( NewtonBodyGetUserData( mReturnInfoList[hitnum].m_hitBody ) )->managedBody;
		info->mCollisionID = mReturnInfoList[hitnum].m_contactID;
		info->mContactNormal.x =   mReturnInfoList[hitnum].m_normal[0];
		info->mContactNormal.y =   mReturnInfoList[hitnum].m_normal[1];
		info->mContactNormal.z =   mReturnInfoList[hitnum].m_normal[2];
		info->mContactNormalOnHitPoint.x =   mReturnInfoList[hitnum].m_normalOnHitPoint[0];
		info->mContactNormalOnHitPoint.y =   mReturnInfoList[hitnum].m_normalOnHitPoint[1];
		info->mContactNormalOnHitPoint.z =   mReturnInfoList[hitnum].m_normalOnHitPoint[2];
		info->mContactPoint.x =   mReturnInfoList[hitnum].m_point[0];
		info->mContactPoint.y =   mReturnInfoList[hitnum].m_point[1];
		info->mContactPoint.z =   mReturnInfoList[hitnum].m_point[2];
		info->mContactPenetration = mReturnInfoList[hitnum].m_penetration;
        return info;
	}

	Mogre::Real BasicConvexCast::NormalizedDistanceToFirstHit::get()
	{
		return mFirstContactDistance;
	}

}	// end namespace MogreNewt


