#pragma once

// When I try to use anything from the Mogre::Math class it throws:
// error C2860: 'void' cannot be an argument type, except for '(void)'
// WTF????

// Recreate some methods of Mogre::Math so we can use them..

namespace MogreNewt
{
	private ref class Math
	{
	public:

		static const Mogre::Real PI = Mogre::Real( System::Math::PI );
        static const Mogre::Real HALF_PI = Mogre::Real( 0.5 * PI );

		static Mogre::Radian ACos (Mogre::Real fValue)
		{
			if ( -1.0 < fValue )
			{
				if ( fValue < 1.0 )
					return Mogre::Radian(System::Math::Acos(fValue));
				else
					return Mogre::Radian(0.0);
			}
			else
			{
				return Mogre::Radian(PI);
			}
		}

		static Mogre::Radian ASin (Mogre::Real fValue)
		{
			if ( -1.0 < fValue )
			{
				if ( fValue < 1.0 )
					return Mogre::Radian(System::Math::Asin(fValue));
				else
					return Mogre::Radian(HALF_PI);
			}
			else
			{
				return Mogre::Radian(-HALF_PI);
			}
		}
	};
}
