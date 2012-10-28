using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MParticleUniverse
{
    /// <summary>
    /// Abstract (pure virtual) dependency class
	/// <remarks>
	///	In some cases, an attribute of a ParticleSystem or its underlying components (ParticleEmitter, ...) may 
	///	depend on another value that changes over time. The ´changing value´ is wrapped into a IDependency class
	///	and the attribute is ´continuesly´ changed by the IDependency subclass.
    ///	</remarks>
    /// </summary>
    public interface IDependency : IElement
    {
    }
}
