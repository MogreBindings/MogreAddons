using System;
using System.Collections.Generic;
using System.Text;
using Mogre;

namespace MQuickGUI
{
	/** A Cursor marking a position in Text.
		@note
		Every Text Widget has a TextCursor.
	*/
    public class TextCursor : Widget
    {
        /** Constructor
            @param
                name The name to be given to the widget (must be unique).
            @param
                dimensions The relative x Position, y Position, width and height of the widget.
            @param
                material Ogre material defining the widget image.
            @param
                overlayContainer associates the internal OverlayElement with a specified zOrder.
            @param
                ParentWidget parent widget which created this widget.
        */

		public TextCursor(string name, Vector4 dimensions, QGuiMetricsMode positionMode, QGuiMetricsMode sizeMode, string material, OverlayContainer overlayContainer, Widget ParentWidget) : base(name,dimensions, positionMode, sizeMode ,material,overlayContainer,ParentWidget)
        {
	        mWidgetType = Widget.WidgetType.QGUI_TYPE_TEXTCURSOR;

	        float pixelWidth = GUIManager.Singleton.getRenderWindowWidth() * 0.01f;
            mPixelDimensions.z = Mogre.Math.Ceil(pixelWidth);

	        mOverlayElement = createPanelOverlayElement(mInstanceName+"_Background",mPixelDimensions,"");
	        mOverlayContainer.AddChild(mOverlayElement);
	        mOverlayElement.Show();
            setMaterial(mWidgetMaterial);
        }
		
		/**
		* Internal method that sets the pixel location and size of the widget.
		*/
        new void _applyDimensions()
        {
		    float pixelWidth = GUIManager.Singleton.getRenderWindowWidth() * 0.01f;
		    mPixelDimensions.z = Mogre.Math.Ceil(pixelWidth);

		    mOverlayElement.SetPosition(mPixelDimensions.x,mPixelDimensions.y);
		    mOverlayElement.SetDimensions(mPixelDimensions.z,mPixelDimensions.w);
        }

		/**
		* Sets the pixel x and y position of the widget.
		*/
        public void setPixelPosition(float xPixelPos, float yPixelPos)
        {
            mPixelDimensions.x = xPixelPos;
            mPixelDimensions.y = yPixelPos;
            mOverlayElement.SetPosition(mPixelDimensions.x, mPixelDimensions.y);
        }

        public void toggleVisibility()
        {
            if (mVisible)
                mOverlayElement.Hide();
            else
                mOverlayElement.Show();

            mVisible = !mVisible;
        }

    }
}
