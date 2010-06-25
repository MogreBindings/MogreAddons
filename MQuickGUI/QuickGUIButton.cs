using System;
using System.Collections.Generic;
using System.Text;
using Mogre;

namespace MQuickGUI
{
    public class Button : Label
    {
        protected bool mOverMaterialExists;
        protected bool mDownMaterialExists;

        public Button(string name, Vector4 dimensions, QGuiMetricsMode positionMode, QGuiMetricsMode sizeMode, string material, OverlayContainer overlayContainer, Widget ParentWidget) : base(name, dimensions, positionMode, sizeMode, material, overlayContainer, ParentWidget) {
		    mWidgetType = Widget.WidgetType.QGUI_TYPE_BUTTON;
		    setCharacterHeight(0.5f);

		    MaterialManager mm = MaterialManager.Singleton;

		    mOverMaterialExists = mm.ResourceExists(mWidgetMaterial+".over");
		    mDownMaterialExists = mm.ResourceExists(mWidgetMaterial+".down");

            OnMouseEnter += new MouseEnterEventHandler(Button_OnMouseEnter);
            OnMouseButtonDown += new MouseButtonDownEventHandler(Button_OnMouseButtonDown);
            OnMouseButtonUp += new MouseButtonUpEventHandler(Button_OnMouseButtonUp);
            OnMouseLeaves += new MouseLeavesEventHandler(Button_OnMouseLeaves);
        }

        public void applyButtonDownMaterial()
	    {
		    if(mDownMaterialExists) 
		    {
			    // apply button ".down" material
		        mOverlayElement.MaterialName = mWidgetMaterial+".down";
		    }
	    }

	    public void applyDefaultMaterial()
	    {
		    mOverlayElement.MaterialName = mWidgetMaterial;
	    }

        void Button_OnMouseButtonDown(object source, MouseEventArgs e)
        {
            if (!mEnabled)
                return;

            applyButtonDownMaterial();
        }

        void Button_OnMouseButtonUp(object source, MouseEventArgs e)
        {
            if (!mEnabled)
                return;
            if (mOverMaterialExists)
                mOverlayElement.MaterialName = mWidgetMaterial + ".over";
        }

        void Button_OnMouseEnter(object source, MouseEventArgs e)
        {
            if (!mEnabled)
                return;
            if (mGrabbed)
                applyButtonDownMaterial();
            else
                if (mOverMaterialExists)
                    mOverlayElement.MaterialName = mWidgetMaterial + ".over";
        }

        void Button_OnMouseLeaves(object source, MouseEventArgs e)
        {
       		if(!mEnabled)
                return;
		    mOverlayElement.MaterialName = mWidgetMaterial;
        }
    }
}
