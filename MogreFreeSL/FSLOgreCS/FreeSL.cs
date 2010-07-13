using System;
using System.Collections.Generic;
using System.Text;

namespace FSLOgreCS
{
    using System;
    using System.Runtime.InteropServices;

    public sealed class FreeSL
    {
        // Sound System
        public enum FSL_SOUND_SYSTEM
        {
            FSL_SS_EAX2,							// EAX 2.0 (Direct Sound 3D)
            FSL_SS_DIRECTSOUND3D,					// Direct Sound 3D
            FSL_SS_DIRECTSOUND,						// Direct Sound
            FSL_SS_NVIDIA_NFORCE_2,					// nVidia nForce 2
            FSL_SS_CREATIVE_AUDIGY_2,				// Creative Audigy 2
            FSL_SS_MMSYSTEM,						// Microsoft
            FSL_SS_ALUT,							// ALUT

            FSL_SS_NOSYSTEM							// no sound system
        };

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


        [DllImport("FreeSL.dll", EntryPoint = "?fslInit@@YA_NW4FSL_SOUND_SYSTEM@@@Z")]
        internal static extern bool fslInit(FSL_SOUND_SYSTEM val);

        /// <summary>
        /// Gets the current memory usage of all non-streaming sounds. This method does not seem to work properly.
        /// </summary>
        /// <returns></returns>
        [DllImport("FreeSL.dll", EntryPoint = "?fslGetSoundMemoryUsage@@YAKXZ")]
        public static extern ulong fslGetSoundMemoryUsage();

        /// <summary>
        /// Pauses all sounds currently playing.
        /// </summary>
        [DllImport("FreeSL.dll", EntryPoint = "?fslSoundPauseAllSounds@@YAXXZ")]
        public static extern void fslSoundPauseAllSounds();

        /// <summary>
        /// Unpauses all sounds currently paused.
        /// </summary>
        [DllImport("FreeSL.dll", EntryPoint = "?fslSoundUnPauseAllSounds@@YAXXZ")]
        public static extern void fslSoundUnPauseAllSounds();

        /// <summary>
        /// Stops all sounds currently playing.
        /// </summary>
        [DllImport("FreeSL.dll", EntryPoint = "?fslSoundStopAllSounds@@YAXXZ")]
        public static extern void fslSoundStopAllSounds();

        /// <summary>
        /// Sets the speed of all sounds.
        /// </summary>
        /// <param name="pitch">Speed between 0 and 1 at which to play the sound.</param>
        [DllImport("FreeSL.dll", EntryPoint = "?fslSoundSetPitchAllSounds@@YAXM@Z")]
        public static extern void fslSoundSetSpeedAllSounds(float pitch);

        /// <summary>
        /// Sets the gain of all sounds.
        /// </summary>
        /// <param name="gain">Positive or negative gain to apply to all sounds.</param>
        [DllImport("FreeSL.dll", EntryPoint = "?fslSoundSetGainAllSounds@@YAXM@Z")]
        public static extern void fslSoundSetGainAllSounds(float gain);

        /// <summary>
        /// Sets parameters for the doppler effect.
        /// </summary>
        /// <param name="factor">Factor by which to scale the Doppler effect. 0 = off, 1 = normal.</param>
        /// <param name="velocity">A leftover input variable from OpenAL 1.0. Default value is 1.</param>
        [DllImport("FreeSL.dll", EntryPoint = "?fslSetDopplerParameters@@YAXMM@Z")]
        public static extern void fslSetDopplerParameters(float factor, float velocity);

        /// <summary>
        /// Sets speed of sound for use by the doppler effect.
        /// </summary>
        /// <param name="val">Speed of sound to assign. The default value is 343.3.</param>
        [DllImport("FreeSL.dll", EntryPoint = "?fslSetSpeedOfSound@@YAXM@Z")]
        public static extern void fslSetSpeedOfSound(float val);

        /// <summary>
        /// Sets the distance-based sound attenuation model. Controls how the gain of sound sources is affected by distance from the listener.
        /// </summary>
        /// <param name="model">Model to apply as the distance sound attenuation model. The default attenuation model is AL_INVERSE_DISTANCE_CLAMPED.</param>
        [DllImport("FreeSL.dll", EntryPoint = "?fslSetListenerDistanceModel@@YAXI@Z")]
        public static extern void fslSetListenerDistanceModel(AL_DISTANCE_MODEL model);

        [DllImport("FreeSL.dll", EntryPoint = "?fslShutDown@@YAXXZ")]
        internal static extern void fslShutDown();

        [DllImport("FreeSL.dll", EntryPoint = "?fslSetVolume@@YAXM@Z")]
        public static extern void fslSetVolume(float gain_mult);

        [DllImport("FreeSL.dll", EntryPoint = "?fslFreeSound@@YAXI_N@Z")]
        internal static extern void fslFreeSound(uint obj, bool remove_buffer);

        [DllImport("FreeSL.dll", EntryPoint = "?fslLoadSound@@YAIPBD@Z")]
        internal static extern uint fslLoadSound(string strFile);

        [DllImport("FreeSL.dll", EntryPoint = "?fslStreamSound@@YAIPBD@Z")]
        internal static extern uint fslStreamSound(string strFile);

        [DllImport("FreeSL.dll", EntryPoint = "?fslLoadSoundFromZip@@YAIPBD0@Z")]
        internal static extern uint fslLoadSoundFromZip(string strPackage, string strFile);
        
        /// <summary>
        /// Enables or disables AutoUpdate, which allows streaming to work.
        /// </summary>
        /// <param name="auto">Whether to use AutoUpdate.</param>
        [DllImport("FreeSL.dll", EntryPoint = "?fslSetAutoUpdate@@YAX_N@Z")]
        public static extern void fslSetAutoUpdate(bool auto);
        
        [DllImport("FreeSL.dll", EntryPoint = "?fslSoundPlay@@YAXI@Z")]
        internal static extern void fslSoundPlay(uint obj);

        [DllImport("FreeSL.dll", EntryPoint = "?fslSoundRewind@@YAXI@Z")]
        internal static extern void fslSoundRewind(uint obj);

        [DllImport("FreeSL.dll", EntryPoint = "?fslSoundStop@@YAXI@Z")]
        internal static extern void fslSoundStop(uint obj);

        [DllImport("FreeSL.dll", EntryPoint = "?fslSoundPause@@YAXI@Z")]
        internal static extern void fslSoundPause(uint obj);

        [DllImport("FreeSL.dll", EntryPoint = "?fslSoundIsPlaying@@YA_NI@Z")]
        internal static extern bool fslSoundIsPlaying(uint obj);

        [DllImport("FreeSL.dll", EntryPoint = "?fslSoundIsPaused@@YA_NI@Z")]
        internal static extern bool fslSoundIsPaused(uint obj);

        [DllImport("FreeSL.dll", EntryPoint = "?fslSoundSetLooping@@YAXI_N@Z")]
        internal static extern void fslSoundSetLooping(uint obj, bool loop_sound);

        [DllImport("FreeSL.dll", EntryPoint = "?fslSoundIsLooping@@YA_NI@Z")]
        internal static extern bool fslSoundIsLooping(uint obj);

        [DllImport("FreeSL.dll", EntryPoint = "?fslSoundSetPitch@@YAXIM@Z")]
        internal static extern void fslSoundSetSpeed(uint obj, float pitch);
        
        [DllImport("FreeSL.dll", EntryPoint = "?fslSoundSetGain@@YAXIM@Z")]
        internal static extern void fslSoundSetGain(uint obj, float gain);

        [DllImport("FreeSL.dll", EntryPoint = "?fslSoundSetSourceRelative@@YAXI_N@Z")]
        internal static extern void fslSoundSetSourceRelative(uint obj, bool is_relative);

        [DllImport("FreeSL.dll", EntryPoint = "?fslSetListenerPosition@@YAXMMM@Z")]
        internal static extern void fslSetListenerPosition(float x, float y, float z);

        [DllImport("FreeSL.dll", EntryPoint = "?fslSetListenerOrientation@@YAXMMMMMM@Z")]
        internal static extern void fslSetListenerOrientation(float atx, float aty, float atz, float upx, float upy, float upz);

        [DllImport("FreeSL.dll", EntryPoint = "?fslSoundSetPosition@@YAXIMMM@Z")]
        internal static extern void fslSoundSetPosition(uint obj, float x, float y, float z);

        [DllImport("FreeSL.dll", EntryPoint = "?fslSoundSetMaxDistance@@YAXIM@Z")]
        internal static extern void fslSoundSetMaxDistance(uint obj, float max_distance);

        [DllImport("FreeSL.dll", EntryPoint = "?fslSoundSetReferenceDistance@@YAXIM@Z")]
        internal static extern void fslSoundSetReferenceDistance(uint obj, float ref_distance);


        // Listener Environments
        public enum FSL_LISTENER_ENVIRONMENT : int
        {
            FSL_ENVIRONMENT_GENERIC,
            FSL_ENVIRONMENT_PADDEDCELL,
            FSL_ENVIRONMENT_ROOM,
            FSL_ENVIRONMENT_BATHROOM,
            FSL_ENVIRONMENT_LIVINGROOM,
            FSL_ENVIRONMENT_STONEROOM,
            FSL_ENVIRONMENT_AUDITORIUM,
            FSL_ENVIRONMENT_CONCERTHALL,
            FSL_ENVIRONMENT_CAVE,
            FSL_ENVIRONMENT_ARENA,
            FSL_ENVIRONMENT_HANGAR,
            FSL_ENVIRONMENT_CARPETEDHALLWAY,
            FSL_ENVIRONMENT_HALLWAY,
            FSL_ENVIRONMENT_STONECORRIDOR,
            FSL_ENVIRONMENT_ALLEY,
            FSL_ENVIRONMENT_FOREST,
            FSL_ENVIRONMENT_CITY,
            FSL_ENVIRONMENT_MOUNTAINS,
            FSL_ENVIRONMENT_QUARRY,
            FSL_ENVIRONMENT_PLAIN,
            FSL_ENVIRONMENT_PARKINGLOT,
            FSL_ENVIRONMENT_SEWERPIPE,
            FSL_ENVIRONMENT_UNDERWATER,
            FSL_ENVIRONMENT_DRUGGED,
            FSL_ENVIRONMENT_DIZZY,
            FSL_ENVIRONMENT_PSYCHOTIC,

            FSL_ENVIRONMENT_COUNT
        };

        // Structs
        [StructLayout(LayoutKind.Sequential)]
        public struct FSL_EAX_LISTENER_PROPERTIES
        {
            public long lRoom;						// room effect level at low frequencies
            public long lRoomHF;					// room effect high-frequency level re. low frequency level
            public float flRoomRolloffFactor;		// like DS3D flRolloffFactor but for room effect
            public float flDecayTime;				// reverberation decay time at low frequencies
            public float flDecayHFRatio;			// high-frequency to low-frequency decay time ratio
            public long lReflections;				// early reflections level relative to room effect
            public float flReflectionsDelay;		// initial reflection delay time
            public long lReverb;					// late reverberation level relative to room effect
            public float flReverbDelay;			// late reverberation delay time relative to initial reflection
            public ulong dwEnvironment;	// sets all listener properties
            public float flEnvironmentSize;		// environment size in meters
            public float flEnvironmentDiffusion;	// environment diffusion
            public float flAirAbsorptionHF;		// change in level per meter at 5 kHz
            public ulong dwFlags;			// modifies the behavior of properties
        };

        [DllImport("FreeSL.dll", EntryPoint = "?fslSetListenerEnvironment@@YAXPAUFSL_EAX_LISTENER_PROPERTIES@@@Z")]
        public static extern void fslSetListenerEnvironment(FSL_EAX_LISTENER_PROPERTIES lpData);

        [DllImport("FreeSL.dll", EntryPoint = "?fslSetListenerEnvironmentPreset@@YAXW4FSL_LISTENER_ENVIRONMENT@@@Z")]
        public static extern void fslSetListenerEnvironmentPreset(FSL_LISTENER_ENVIRONMENT type);

        [DllImport("FreeSL.dll", EntryPoint = "?fslSetListenerDefaultEnvironment@@YAXXZ")]
        public static extern void fslSetListenerDefaultEnvironment();

        [DllImport("FreeSL.dll", EntryPoint = "?fslGetCurrentListenerEnvironment@@YA?AUFSL_EAX_LISTENER_PROPERTIES@@XZ")]
        public static extern FSL_EAX_LISTENER_PROPERTIES fslGetCurrentListenerEnvironment();

        [DllImport("FreeSL.dll", EntryPoint = "?fslLoadListenerEnvironment@@YA?AUFSL_EAX_LISTENER_PROPERTIES@@PBD@Z")]
        public static extern FSL_EAX_LISTENER_PROPERTIES fslLoadListenerEnvironment(string strFile);

        [DllImport("FreeSL.dll", EntryPoint = "?fslLoadListenerEnvironmentFromZip@@YA?AUFSL_EAX_LISTENER_PROPERTIES@@PBD0@Z")]
        public static extern FSL_EAX_LISTENER_PROPERTIES fslLoadListenerEnvironmentFromZip(string strPackage, string strFile);

        [DllImport("FreeSL.dll", EntryPoint = "?fslCreateListenerEnvironment@@YA?AUFSL_EAX_LISTENER_PROPERTIES@@PBDI@Z")]
        public static extern FSL_EAX_LISTENER_PROPERTIES fslCreateListenerEnvironment(string strData, uint size);
    }
}
