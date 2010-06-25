using System;
using System.Collections.Generic;
using System.Text;
using Mogre;

namespace MQuickGUI
{
    public class Image : Widget
    {
        protected MaterialPtr mMaterialPtr;

        public Image(string name, Vector4 dimensions, QGuiMetricsMode positionMode, QGuiMetricsMode sizeMode, string material, OverlayContainer overlayContainer, Widget ParentWidget) : base(name,dimensions,positionMode, sizeMode, material,overlayContainer,ParentWidget)
	    {
		    mWidgetType = Widget.WidgetType.QGUI_TYPE_IMAGE;
		    mMaterialPtr = null;

		    mOverlayElement = createPanelOverlayElement(mInstanceName+"_Background",mPixelDimensions,"");
		    mOverlayContainer.AddChild(mOverlayElement);
		    mOverlayElement.Show();
            setMaterial(mWidgetMaterial);
	    }

	    ~Image()
	    {
		    // If a material was created (for rendertexture use), destroy and unload it
            if (mMaterialPtr != null)
		    {
			    string name = mMaterialPtr.Name;
			    MaterialManager.Singleton.Remove(name);
		    }
	    }

        public void setMaterial(RenderTexture texture)
	    {
		    setMaterial(texture.Name,true);
	    }

        public void setMaterial(string name, bool texture)
	    {
		    if(!texture)
		    {
                base.setMaterial(name);
		    }
		    else
		    {
                // If material already exists, use it!
                if (mMaterialPtr == null) // GDZ, codigo original: 			if(mMaterialPtr.isNull())
			    {
                    mMaterialPtr = MaterialManager.Singleton.Create(mInstanceName+"_RenderTextureMaterial",ResourceGroupManager.DEFAULT_RESOURCE_GROUP_NAME);
				    TextureUnitState t = mMaterialPtr.GetTechnique(0).GetPass(0).CreateTextureUnitState(name);
                }
			    mOverlayElement.MaterialName = mMaterialPtr.Name;
		    }
	    }

	    void setUV(float u1, float v1, float u2, float v2)
	    {
		    ((PanelOverlayElement)mOverlayElement).SetUV(u1,v1,u2,v2);
	    }
    }
}
