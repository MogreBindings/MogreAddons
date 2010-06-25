using System;
using System.Collections.Generic;
using System.Text;
using Mogre;
using System.ComponentModel;

namespace MogreDesignSupport.ProxyClasses
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class SceneManagerProxy
    {
        public SceneManager SceneManager { get; protected set; }

        public SceneManagerProxy(SceneManager sceneManager)
        {
            this.SceneManager = sceneManager;
        }

        public FogMode FogMode
        {
            get { return SceneManager.FogMode; }
            set { SceneManager.SetFog(value, SceneManager.FogColour, SceneManager.FogDensity, SceneManager.FogStart, SceneManager.FogEnd); }
        }

        public ColourValue FogColour
        {
            get { return SceneManager.FogColour; }
            set { SceneManager.SetFog(SceneManager.FogMode, value, SceneManager.FogDensity, SceneManager.FogStart, SceneManager.FogEnd); }
        }

        public float FogDensity
        {
            get { return SceneManager.FogDensity; }
            set { SceneManager.SetFog(SceneManager.FogMode, SceneManager.FogColour, value, SceneManager.FogStart, SceneManager.FogEnd); }
        }

        public float FogStart
        {
            get { return SceneManager.FogStart; }
            set { SceneManager.SetFog(SceneManager.FogMode, SceneManager.FogColour, SceneManager.FogDensity, value, SceneManager.FogEnd); }
        }

        public float FogEnd
        {
            get { return SceneManager.FogEnd; }
            set { SceneManager.SetFog(SceneManager.FogMode, SceneManager.FogColour, SceneManager.FogDensity, SceneManager.FogStart, value); }
        }
    }
}
