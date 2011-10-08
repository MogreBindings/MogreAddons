using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace MogreFreeSL
{
    /// <summary>
    /// Sound rendering systems available for use with MogreFreeSL.
    /// </summary>
    public enum FSL_SOUND_SYSTEM
    {
        /// <summary>
        /// EAX 2.0 (Direct Sound 3D)
        /// </summary>
        FSL_SS_EAX2,

        /// <summary>
        /// Direct Sound 3D
        /// </summary>
        FSL_SS_DIRECTSOUND3D,

        /// <summary>
        /// Direct Sound
        /// </summary>
        FSL_SS_DIRECTSOUND,

        /// <summary>
        /// nVidia nForce 2
        /// </summary>
        FSL_SS_NVIDIA_NFORCE_2,

        /// <summary>
        /// Creative Audigy 2
        /// </summary>
        FSL_SS_CREATIVE_AUDIGY_2,

        /// <summary>
        /// Microsoft
        /// </summary>
        FSL_SS_MMSYSTEM,

        /// <summary>
        /// ALUT (OpenAL Utility Toolkit)
        /// </summary>
        FSL_SS_ALUT,

        /// <summary>
        /// No sound system
        /// </summary>
        FSL_SS_NOSYSTEM
    };

    /// <summary>
    /// Distance models available for use with MogreFreeSL.
    /// </summary>
    public enum AL_DISTANCE_MODEL
    {
        /// <summary>
        /// Inverse distance rolloff model, which is equivalent to the IASIG I3DL2 model with the exception that referenceDistance does not imply any clamping.
        /// <code>gain = referenceDistance / (referenceDistance + rolloffFactor * (distance - referenceDistance))</code>
        /// </summary>
        AL_INVERSE_DISTANCE = 90,

        /// <summary>
        /// Inverse Distance clamped model, which is essentially the inverse distance rolloff model, extended to guarantee that for distances below referenceDistance, gain is clamped. This mode is equivalent to the IASIG I3DL2 distance model.
        /// </summary>
        AL_INVERSE_DISTANCE_CLAMPED,

        /// <summary>
        /// Linear distance rolloff model, modeling a linear dropoff in gain as distance increases between the source and listener.
        /// <code>gain = (1 - rolloffFactor * (distance - referenceDistance) / (maxDistance - referenceDistance))</code>
        /// </summary>
        AL_LINEAR_DISTANCE,

        /// <summary>
        /// Linear Distance clamped model, which is the linear model, extended to guarantee that for distances below referenceDistance, gain is clamped.
        /// </summary>
        AL_LINEAR_DISTANCE_CLAMPED,

        /// <summary>
        /// Exponential distance rolloff model, modeling an exponential dropoff in gain as distance increases between the source and listener.
        /// <code>gain = (distance / referenceDistance) ** (- rolloffFactor)</code>
        /// </summary>
        AL_EXPONENT_DISTANCE,

        /// <summary>
        /// Exponential Distance clamped model, which is the exponential model, extended to guarantee that for distances below referenceDistance, gain is clamped.
        /// </summary>
        AL_EXPONENT_DISTANCE_CLAMPED,
    };

    /// <summary>
    /// Listener environments available for use with MogreFreeSL. Define your own with FSL_EAX_LISTENER_PROPERTIES.
    /// </summary>
    public enum FSL_LISTENER_ENVIRONMENT : int
    {
        /// <summary>
        /// Generic listener environment
        /// </summary>
        FSL_ENVIRONMENT_GENERIC,

        /// <summary>
        /// Listener environment simulating a padded cell
        /// </summary>
        FSL_ENVIRONMENT_PADDEDCELL,

        /// <summary>
        /// Listener environment simulating a normal room
        /// </summary>
        FSL_ENVIRONMENT_ROOM,

        /// <summary>
        /// Listener environment simulating a bathroom
        /// </summary>
        FSL_ENVIRONMENT_BATHROOM,

        /// <summary>
        /// Listener environment simulating a living room
        /// </summary>
        FSL_ENVIRONMENT_LIVINGROOM,

        /// <summary>
        /// Listener environment simulating a stone room
        /// </summary>
        FSL_ENVIRONMENT_STONEROOM,

        /// <summary>
        /// Listener environment simulating an auditorium
        /// </summary>
        FSL_ENVIRONMENT_AUDITORIUM,

        /// <summary>
        /// Listener environment simulating a concert hall
        /// </summary>
        FSL_ENVIRONMENT_CONCERTHALL,

        /// <summary>
        /// Listener environment simulating a cave
        /// </summary>
        FSL_ENVIRONMENT_CAVE,

        /// <summary>
        /// Listener environment simulating an arena
        /// </summary>
        FSL_ENVIRONMENT_ARENA,

        /// <summary>
        /// Listener environment simulating a hangar
        /// </summary>
        FSL_ENVIRONMENT_HANGAR,

        /// <summary>
        /// Listener environment simulating a carpeted hallway
        /// </summary>
        FSL_ENVIRONMENT_CARPETEDHALLWAY,

        /// <summary>
        /// Listener environment simulating a hallway
        /// </summary>
        FSL_ENVIRONMENT_HALLWAY,

        /// <summary>
        /// Listener environment simulating a stone hallway
        /// </summary>
        FSL_ENVIRONMENT_STONECORRIDOR,

        /// <summary>
        /// Listener environment simulating a small alley
        /// </summary>
        FSL_ENVIRONMENT_ALLEY,

        /// <summary>
        /// Listener environment simulating a forest
        /// </summary>
        FSL_ENVIRONMENT_FOREST,

        /// <summary>
        /// Listener environment simulating a city
        /// </summary>
        FSL_ENVIRONMENT_CITY,

        /// <summary>
        /// Listener environment simulating mountainous terrain
        /// </summary>
        FSL_ENVIRONMENT_MOUNTAINS,

        /// <summary>
        /// Listener environment simulating a quarry
        /// </summary>
        FSL_ENVIRONMENT_QUARRY,

        /// <summary>
        /// Listener environment simulating some plains
        /// </summary>
        FSL_ENVIRONMENT_PLAIN,

        /// <summary>
        /// Listener environment simulating a parkinglot
        /// </summary>
        FSL_ENVIRONMENT_PARKINGLOT,

        /// <summary>
        /// Listener environment simulating a sewer
        /// </summary>
        FSL_ENVIRONMENT_SEWERPIPE,

        /// <summary>
        /// Listener environment simulating an underwater environment
        /// </summary>
        FSL_ENVIRONMENT_UNDERWATER,

        /// <summary>
        /// Listener environment simulating the effect of being drugged
        /// </summary>
        FSL_ENVIRONMENT_DRUGGED,

        /// <summary>
        /// Listener environment simulating the effect of being dizzy
        /// </summary>
        FSL_ENVIRONMENT_DIZZY,

        /// <summary>
        /// Listener environment simulating the effect of being psychotic
        /// </summary>
        FSL_ENVIRONMENT_PSYCHOTIC,

        FSL_ENVIRONMENT_COUNT   //I have no idea what this does. Therefore, no XML documentation.
    };

    /// <summary>
    /// A struct which may be used to define a custom listener environment. Presets are available through FSL_LISTENER_ENVIRONMENT.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct FSL_EAX_LISTENER_PROPERTIES
    {
        /// <summary>
        /// Room effect level at low frequencies
        /// </summary>
        public long lRoom;

        /// <summary>
        /// Room effect high-frequency level re. low frequency level
        /// </summary>
        public long lRoomHF;

        /// <summary>
        /// Like DS3D flRolloffFactor but for room effect
        /// </summary>
        public float flRoomRolloffFactor;

        /// <summary>
        /// Reverberation decay time at low frequencies
        /// </summary>
        public float flDecayTime;

        /// <summary>
        /// High-frequency to low-frequency decay time ratio
        /// </summary>
        public float flDecayHFRatio;

        /// <summary>
        /// Early reflections level relative to room effect
        /// </summary>
        public long lReflections;

        /// <summary>
        /// Initial reflection delay time
        /// </summary>
        public float flReflectionsDelay;

        /// <summary>
        /// Late reverberation level relative to room effect
        /// </summary>
        public long lReverb;

        /// <summary>
        /// Late reverberation delay time relative to initial reflection
        /// </summary>
        public float flReverbDelay;

        /// <summary>
        /// Sets all listener properties
        /// </summary>
        public ulong dwEnvironment;

        /// <summary>
        /// Environment size in meters
        /// </summary>
        public float flEnvironmentSize;

        /// <summary>
        /// Environment diffusion
        /// </summary>
        public float flEnvironmentDiffusion;

        /// <summary>
        /// Change in level per meter at 5 kHz
        /// </summary>
        public float flAirAbsorptionHF;

        /// <summary>
        /// Modifies the behavior of properties
        /// </summary>
        public ulong dwFlags;
    };

    internal sealed class FreeSL
    {
        [DllImport("FreeSL.dll", EntryPoint = "?fslGetSoundMemoryUsage@@YAKXZ", CallingConvention=CallingConvention.Cdecl)]
        internal static extern ulong fslGetSoundMemoryUsage();

        [DllImport("FreeSL.dll", EntryPoint = "?fslSoundPauseAllSounds@@YAXXZ", CallingConvention=CallingConvention.Cdecl)]
        internal static extern void fslSoundPauseAllSounds();

        [DllImport("FreeSL.dll", EntryPoint = "?fslSoundUnPauseAllSounds@@YAXXZ", CallingConvention=CallingConvention.Cdecl)]
        internal static extern void fslSoundUnPauseAllSounds();

        [DllImport("FreeSL.dll", EntryPoint = "?fslSoundStopAllSounds@@YAXXZ", CallingConvention=CallingConvention.Cdecl)]
        internal static extern void fslSoundStopAllSounds();

        [DllImport("FreeSL.dll", EntryPoint = "?fslSoundSetPitchAllSounds@@YAXM@Z", CallingConvention=CallingConvention.Cdecl)]
        internal static extern void fslSoundSetSpeedAllSounds(float pitch);

        [DllImport("FreeSL.dll", EntryPoint = "?fslSoundSetGainAllSounds@@YAXM@Z", CallingConvention=CallingConvention.Cdecl)]
        internal static extern void fslSoundSetGainAllSounds(float gain);

        [DllImport("FreeSL.dll", EntryPoint = "?fslSetDopplerParameters@@YAXMM@Z", CallingConvention=CallingConvention.Cdecl)]
        internal static extern void fslSetDopplerParameters(float factor, float velocity);

        [DllImport("FreeSL.dll", EntryPoint = "?fslSetSpeedOfSound@@YAXM@Z", CallingConvention=CallingConvention.Cdecl)]
        internal static extern void fslSetSpeedOfSound(float val);

        [DllImport("FreeSL.dll", EntryPoint = "?fslSetListenerDistanceModel@@YAXI@Z", CallingConvention=CallingConvention.Cdecl)]
        internal static extern void fslSetListenerDistanceModel(AL_DISTANCE_MODEL model);

        [DllImport("FreeSL.dll", EntryPoint = "?fslInit@@YA_NW4FSL_SOUND_SYSTEM@@@Z", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool fslInit(FSL_SOUND_SYSTEM val);

        [DllImport("FreeSL.dll", EntryPoint = "?fslShutDown@@YAXXZ", CallingConvention=CallingConvention.Cdecl)]
        internal static extern void fslShutDown();

        [DllImport("FreeSL.dll", EntryPoint = "?fslSetVolume@@YAXM@Z", CallingConvention=CallingConvention.Cdecl)]
        internal static extern void fslSetVolume(float gain_mult);

        [DllImport("FreeSL.dll", EntryPoint = "?fslFreeSound@@YAXI_N@Z", CallingConvention=CallingConvention.Cdecl)]
        internal static extern void fslFreeSound(uint obj, bool remove_buffer);

        [DllImport("FreeSL.dll", EntryPoint = "?fslLoadSound@@YAIPBD@Z", CallingConvention=CallingConvention.Cdecl)]
        internal static extern uint fslLoadSound(string strFile);

        [DllImport("FreeSL.dll", EntryPoint = "?fslStreamSound@@YAIPBD@Z", CallingConvention=CallingConvention.Cdecl)]
        internal static extern uint fslStreamSound(string strFile);

        [DllImport("FreeSL.dll", EntryPoint = "?fslLoadSoundFromZip@@YAIPBD0@Z", CallingConvention=CallingConvention.Cdecl)]
        internal static extern uint fslLoadSoundFromZip(string strPackage, string strFile);
        
        /// <summary>
        /// Enables or disables AutoUpdate, which allows streaming to work.
        /// </summary>
        /// <param name="auto">Whether to use AutoUpdate.</param>
        [DllImport("FreeSL.dll", EntryPoint = "?fslSetAutoUpdate@@YAX_N@Z", CallingConvention=CallingConvention.Cdecl)]
        internal static extern void fslSetAutoUpdate(bool auto);
        
        [DllImport("FreeSL.dll", EntryPoint = "?fslSoundPlay@@YAXI@Z", CallingConvention=CallingConvention.Cdecl)]
        internal static extern void fslSoundPlay(uint obj);

        [DllImport("FreeSL.dll", EntryPoint = "?fslSoundRewind@@YAXI@Z", CallingConvention=CallingConvention.Cdecl)]
        internal static extern void fslSoundRewind(uint obj);   //Unused

        [DllImport("FreeSL.dll", EntryPoint = "?fslSoundStop@@YAXI@Z", CallingConvention=CallingConvention.Cdecl)]
        internal static extern void fslSoundStop(uint obj);

        [DllImport("FreeSL.dll", EntryPoint = "?fslSoundPause@@YAXI@Z", CallingConvention=CallingConvention.Cdecl)]
        internal static extern void fslSoundPause(uint obj);

        [DllImport("FreeSL.dll", EntryPoint = "?fslSoundIsPlaying@@YA_NI@Z", CallingConvention=CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool fslSoundIsPlaying(uint obj);

        [DllImport("FreeSL.dll", EntryPoint = "?fslSoundIsPaused@@YA_NI@Z", CallingConvention=CallingConvention.Cdecl)]
        internal static extern bool fslSoundIsPaused(uint obj);

        [DllImport("FreeSL.dll", EntryPoint = "?fslSoundSetLooping@@YAXI_N@Z", CallingConvention=CallingConvention.Cdecl)]
        internal static extern void fslSoundSetLooping(uint obj, bool loop_sound);

        [DllImport("FreeSL.dll", EntryPoint = "?fslSoundIsLooping@@YA_NI@Z", CallingConvention=CallingConvention.Cdecl)]
        internal static extern bool fslSoundIsLooping(uint obj);

        [DllImport("FreeSL.dll", EntryPoint = "?fslSoundSetPitch@@YAXIM@Z", CallingConvention=CallingConvention.Cdecl)]
        internal static extern void fslSoundSetSpeed(uint obj, float pitch);
        
        [DllImport("FreeSL.dll", EntryPoint = "?fslSoundSetGain@@YAXIM@Z", CallingConvention=CallingConvention.Cdecl)]
        internal static extern void fslSoundSetGain(uint obj, float gain);

        [DllImport("FreeSL.dll", EntryPoint = "?fslSoundSetSourceRelative@@YAXI_N@Z", CallingConvention=CallingConvention.Cdecl)]
        internal static extern void fslSoundSetSourceRelative(uint obj, bool is_relative);

        [DllImport("FreeSL.dll", EntryPoint = "?fslSetListenerPosition@@YAXMMM@Z", CallingConvention=CallingConvention.Cdecl)]
        internal static extern void fslSetListenerPosition(float x, float y, float z);

        [DllImport("FreeSL.dll", EntryPoint = "?fslSetListenerOrientation@@YAXMMMMMM@Z", CallingConvention=CallingConvention.Cdecl)]
        internal static extern void fslSetListenerOrientation(float atx, float aty, float atz, float upx, float upy, float upz);

        [DllImport("FreeSL.dll", EntryPoint = "?fslSoundSetPosition@@YAXIMMM@Z", CallingConvention=CallingConvention.Cdecl)]
        internal static extern void fslSoundSetPosition(uint obj, float x, float y, float z);

        [DllImport("FreeSL.dll", EntryPoint = "?fslSoundSetMaxDistance@@YAXIM@Z", CallingConvention=CallingConvention.Cdecl)]
        internal static extern void fslSoundSetMaxDistance(uint obj, float max_distance);

        [DllImport("FreeSL.dll", EntryPoint = "?fslSoundSetReferenceDistance@@YAXIM@Z", CallingConvention=CallingConvention.Cdecl)]
        internal static extern void fslSoundSetReferenceDistance(uint obj, float ref_distance);

        [DllImport("FreeSL.dll", EntryPoint = "?fslSetListenerEnvironment@@YAXPAUFSL_EAX_LISTENER_PROPERTIES@@@Z", CallingConvention=CallingConvention.Cdecl)]
        internal static extern void fslSetListenerEnvironment(FSL_EAX_LISTENER_PROPERTIES lpData);

        [DllImport("FreeSL.dll", EntryPoint = "?fslSetListenerEnvironmentPreset@@YAXW4FSL_LISTENER_ENVIRONMENT@@@Z", CallingConvention=CallingConvention.Cdecl)]
        internal static extern void fslSetListenerEnvironmentPreset(FSL_LISTENER_ENVIRONMENT type);

        [DllImport("FreeSL.dll", EntryPoint = "?fslSetListenerDefaultEnvironment@@YAXXZ", CallingConvention=CallingConvention.Cdecl)]
        internal static extern void fslSetListenerDefaultEnvironment();

        [DllImport("FreeSL.dll", EntryPoint = "?fslGetCurrentListenerEnvironment@@YA?AUFSL_EAX_LISTENER_PROPERTIES@@XZ", CallingConvention=CallingConvention.Cdecl)]
        internal static extern FSL_EAX_LISTENER_PROPERTIES fslGetCurrentListenerEnvironment();

        [DllImport("FreeSL.dll", EntryPoint = "?fslLoadListenerEnvironment@@YA?AUFSL_EAX_LISTENER_PROPERTIES@@PBD@Z", CallingConvention=CallingConvention.Cdecl)]
        internal static extern FSL_EAX_LISTENER_PROPERTIES fslLoadListenerEnvironment(string strFile);

        [DllImport("FreeSL.dll", EntryPoint = "?fslLoadListenerEnvironmentFromZip@@YA?AUFSL_EAX_LISTENER_PROPERTIES@@PBD0@Z", CallingConvention=CallingConvention.Cdecl)]
        internal static extern FSL_EAX_LISTENER_PROPERTIES fslLoadListenerEnvironmentFromZip(string strPackage, string strFile);

        [DllImport("FreeSL.dll", EntryPoint = "?fslCreateListenerEnvironment@@YA?AUFSL_EAX_LISTENER_PROPERTIES@@PBDI@Z", CallingConvention=CallingConvention.Cdecl)]
        internal static extern FSL_EAX_LISTENER_PROPERTIES fslCreateListenerEnvironment(string strData, uint size);
    }
}
