using System;
using System.Collections.Generic;
using System.Text;
using Mogre;

namespace MQuickGUI
{
    public class MouseCursor
    {
        private static MouseCursor instance = null;

        public static MouseCursor Singleton {
            get {
                return instance;
            }
        }

		// Default material.
		protected string	mMaterial;
		// Width and Height in pixels
		protected Vector2	mPixelCursorDimensions;
		protected Vector2	mRelativeCursorDimensions;
        protected int mRenderWidthInPixels;
        protected int mRenderHeightInPixels;
		
		// Specifies the area (in screen pixels) that the mouse can move around in.
        protected Vector2 mConstraints;
        protected bool mOnTopBorder;
        protected bool mOnBotBorder;
        protected bool mOnLeftBorder;
        protected bool mOnRightBorder;

        protected bool mHideWhenOffScreen;
        protected bool mVisible;
        protected Vector2 mPosition;

		// Mouse Pointer Overlay
        protected Overlay mMouseOverlay;
        protected OverlayContainer mMousePointerContainer;			



       	internal MouseCursor(Vector2 dimensions, string material, int RenderWidthInPixels, int RenderHeightInPixels)
	    {
            instance = this;
		    mMaterial = material;
		    mRelativeCursorDimensions = dimensions;
		    mRenderWidthInPixels = RenderWidthInPixels;
		    mRenderHeightInPixels = RenderHeightInPixels;
		    mVisible = true;
		    mHideWhenOffScreen = true;
		    mOnTopBorder = false;
		    mOnBotBorder = false;
		    mOnLeftBorder = false;
		    mOnRightBorder = false;

            mPixelCursorDimensions.x = mRelativeCursorDimensions.x;// *RenderWidthInPixels; // GDZ: Prefiero al cursor en pixels
            mPixelCursorDimensions.y = mRelativeCursorDimensions.y;// *RenderHeightInPixels; // GDZ: Prefiero al cursor en pixels
		    mConstraints.x = RenderWidthInPixels;
		    mConstraints.y = RenderHeightInPixels;
		    mPosition.x = mConstraints.x/2;
		    mPosition.y = mConstraints.y/2;

		    // Create Mouse Overlay
		    mMouseOverlay = OverlayManager.Singleton.Create("simpleGUI_Mouse_Overlay");
		    mMouseOverlay.ZOrder = 649;
		    mMouseOverlay.Show();

		    // Create Mouse Overlay Container
		    mMousePointerContainer = (OverlayContainer)(OverlayManager.Singleton.CreateOverlayElement("Panel","simpleGUI_Mouse_Container"));
            mMousePointerContainer.MetricsMode = GuiMetricsMode.GMM_PIXELS;
		    mMousePointerContainer.SetPosition(mPosition.x,mPosition.y);
		    mMousePointerContainer.SetDimensions(Mogre.Math.Ceil(mPixelCursorDimensions.x), Mogre.Math.Ceil(mPixelCursorDimensions.y));
		    mMousePointerContainer.MaterialName = mMaterial;

		    mMouseOverlay.Add2D(mMousePointerContainer);
		    mMousePointerContainer.Show();
	    }
    	
	    ~MouseCursor()
	    {
            if (mMouseOverlay != null)
		    {
			    OverlayManager om = OverlayManager.Singleton;

			    if(mMousePointerContainer != null)
			    {
                    //TODO: Revisar esto, no anda en el dispose
//                    mMouseOverlay.Remove2D(mMousePointerContainer);
//                    om.DestroyOverlayElement(mMousePointerContainer);
//                    mMousePointerContainer = null;
			    }
                // En MOGRE esto nobe ser así / GDZ
//                om.Destroy(mMouseOverlay);
//                mMouseOverlay = null;
		    }
	    }

	    public void _updateWindowDimensions( int RenderWidthInPixels, int RenderHeightInPixels)
	    {
		    mRenderWidthInPixels = RenderWidthInPixels;
		    mRenderHeightInPixels = RenderHeightInPixels;

            mPixelCursorDimensions.x = mRelativeCursorDimensions.x;// *mRenderWidthInPixels;// GDZ: Prefiero al cursor en pixels
            mPixelCursorDimensions.y = mRelativeCursorDimensions.y;// *mRenderHeightInPixels;// GDZ: Prefiero al cursor en pixels
		    mMousePointerContainer.SetDimensions(Mogre.Math.Ceil(mPixelCursorDimensions.x), Mogre.Math.Ceil(mPixelCursorDimensions.y));

		    mConstraints.x = RenderWidthInPixels;
		    mConstraints.y = RenderHeightInPixels;
		    mPosition.x = mConstraints.x/2;
		    mPosition.y = mConstraints.y/2;

		    constrainPosition();
	    }

	    void constrainPosition()
	    {
		    bool offScreen = false;
		    mOnRightBorder = false;
		    mOnBotBorder = false;
		    mOnTopBorder = false;
		    mOnLeftBorder = false;

		    if (mPosition.x >= (mConstraints.x - 1))
		    {
			    mPosition.x = mConstraints.x - 1;
			    mOnRightBorder = true;
			    offScreen = true;
		    }

		    if (mPosition.y >= (mConstraints.y - 1))
		    {
			    mPosition.y = mConstraints.y - 1;
			    mOnBotBorder = true;
			    offScreen = true;
		    }

		    if (mPosition.y <= 0)
		    {
			    mPosition.y = 0;
			    mOnTopBorder = true;
			    offScreen = true;
		    }

		    if (mPosition.x <= 0)
		    {
			    mPosition.x = 0;
			    mOnLeftBorder = true;
			    offScreen = true;
		    }

		    if (offScreen)
                GUIManager.Singleton.injectMouseLeaves();
		    // For example, if the user wants the mouse hidden, we shouldn't show it
		    // even if its within bounds.
		    else 
		    {
			    if(mVisible)
                    show();
			    else
                    hide();
		    }

		    // Perform the actual moving of the mouse overlay
		    mMousePointerContainer.SetPosition(mPosition.x,mPosition.y);
	    }

	    public bool getHideWhenOffScreen()
	    {
		    return mHideWhenOffScreen;
	    }

	    public Vector2 getPixelPosition()
	    {
		    return mPosition;
	    }

	    Vector2 getRelativePosition()
	    {
		    return mRelativeCursorDimensions;
	    }

	    Vector2 getRelativeSize()
	    {
		    return mRelativeCursorDimensions;
	    }

	    public void hide()
	    {
		    if(mMouseOverlay!=null) 
		    {
			    mMouseOverlay.Hide();
			    mVisible = false;
		    }
	    }

	    internal void _hide()
	    {
		    if(mMouseOverlay!=null) 
		    {
			    mMouseOverlay.Hide();
		    }
	    }

        public bool isVisible()
	    {
		    return mVisible;
	    }

	    public bool mouseOnBotBorder()
	    {
		    return mOnBotBorder;
	    }

	    public bool mouseOnLeftBorder()
	    {
		    return mOnLeftBorder;
	    }

	    public bool mouseOnRightBorder()
	    {
		    return mOnRightBorder;
	    }

	    public bool mouseOnTopBorder()
	    {
		    return mOnTopBorder;
	    }

	    public void offsetPosition(int x_offset_in_pixels, int y_offset_in_pixels)
	    {
		    mPosition.x += x_offset_in_pixels;
		    mPosition.y += y_offset_in_pixels;
		    constrainPosition();
	    }

	    void setMaterial(string material)
	    {
		    mMaterial = material;
		    mMousePointerContainer.MaterialName = mMaterial;
	    }

	    void setHideCursorWhenOSCursorOffscreen(bool hide)
	    {
		    mHideWhenOffScreen = hide;
	    }

	    void setPosition(int xPixelPosition, int yPixelPosition)
	    {
		    mPosition.x = xPixelPosition;
		    mPosition.y = yPixelPosition;
		    constrainPosition();
	    }

	    void setSize(int widthInPixels, int heightInPixels)
	    {
		    mMousePointerContainer.SetDimensions(widthInPixels,heightInPixels);
	    }

	    public void show()
	    {
		    if(mMouseOverlay != null) 
		    {
			    mMouseOverlay.Show();
			    mVisible = true;
		    }
	    }


    }
}
