using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MParticleUniverse
{
    /// <summary>
    /// Abstract (pure virtual) alias class
    ///<remarks>
    ///	The IAlias class acts as an interface and is used to represent all other classes that are a child of the
    ///	IAlias class. This typically concerns entities that are used in the Particle Universe scripts and for
    ///	which it is possible to define an alias (i.e. ParticleTechnique, ParticleEmitter and ParticleAffector).
    ///	</remarks>
    /// </summary>
    public interface IAlias
    {
        IntPtr NativePointer {get;}

        String AliasName { get; set; }

        AliasType AliasType { get; set; }

    }

    public enum AliasType
    {
        AT_UNDEFINED = 0,
        AT_TECHNIQUE = 1,
        AT_RENDERER = 2,
        AT_EMITTER = 3,
        AT_AFFECTOR = 4,
        AT_OBSERVER = 5,
        AT_EXTERN = 6,
        AT_HANDLER = 7,
        AT_BEHAVIOUR = 8
    };

    //#region IAlias Implementation
    //public AliasType AliasType
    //{
    //    get { return IAlias_GetAliasType(NativePointer); }
    //    set { IAlias_SetAliasType(NativePointer, value); }
    //}
    //public String AliasName
    //{
    //    get { return IAlias_GetAliasName(NativePointer); }
    //    set { IAlias_SetAliasName(NativePointer, value); }
    //}
    //#endregion

    //#region PInvoke
    //[DllImport("ParticleUniverse.dll", EntryPoint = "IAlias_GetAliasType", CallingConvention = CallingConvention.Cdecl)]
    //internal static extern AliasType IAlias_GetAliasType(IntPtr ptr);

    //[DllImport("ParticleUniverse.dll", EntryPoint = "IAlias_SetAliasType", CallingConvention = CallingConvention.Cdecl)]
    //internal static extern void IAlias_SetAliasType(IntPtr ptr, AliasType aliasType);


    //[DllImport("ParticleUniverse.dll", EntryPoint = "IAlias_GetAliasName", CallingConvention = CallingConvention.Cdecl)]
    //internal static extern IntPtr IAlias_GetAliasName(IntPtr ptr);

    //[DllImport("ParticleUniverse.dll", EntryPoint = "IAlias_SetAliasName", CallingConvention = CallingConvention.Cdecl)]
    //internal static extern void IAlias_SetAliasName(IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)]string aliasName);

    //#endregion

}
