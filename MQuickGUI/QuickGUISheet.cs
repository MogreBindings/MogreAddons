using System;
using System.Collections.Generic;
using System.Text;
using Mogre;

namespace MQuickGUI
{
    /** Represents a transparent area for holding widgets.
        @remarks
        The Sheet class is derived from a Window, although it
        differs because it is the size of the screen.  
        In addition, the Sheet class can create Windows.
    */
    public class Sheet : Panel
    {
        protected Overlay mOverlay;

		protected int	mAutoNameWindowCounter;
		// List of windows according to z-order.  highest z-order in front of list
        protected List<Window> mWindows = new List<Window>();

		protected string mDefaultSkin;
		protected string mDefaultFont;
		protected ColourValue mDefaultTextColor;
		protected float mDefaultCharacterHeight;

		protected Overlay mMenuOverlay;
		protected OverlayContainer mMenuContainer;
		protected int mMenuOverlayZOrder;

        /** Constructor
            @param
                name The name to be given to the widget (must be unique).
            @note
                If you want a transparent background, pass "" as the material.
        */
        public Sheet(string name, string material)
            : base(name, new Vector4(0, 0, 1, 1), QGuiMetricsMode.QGUI_GMM_RELATIVE, QGuiMetricsMode.QGUI_GMM_RELATIVE, material, null, null)
	    {

            mDefaultCharacterHeight = 1;
		    mDefaultTextColor = ColourValue.White;
		    mDefaultSkin = "qgui";
		    mAutoNameWindowCounter = 0;
		    mMenuOverlayZOrder = 600;

            mVerticalAlignment = GuiVerticalAlignment.GVA_BOTTOM;
            
            mWidgetType = Widget.WidgetType.QGUI_TYPE_SHEET;
		    mWindows.Clear();
		    // Sheets are always at zOrder 0.
		    mOverlay = OverlayManager.Singleton.Create(mInstanceName+".Overlay");
		    mOverlay.ZOrder = 0;
		    mOverlay.Show();
		    mOverlay.Add2D(mOverlayContainer);

		    FontManager fm = FontManager.Singleton;
		    ResourceManager.ResourceMapIterator rmi = fm.GetResourceIterator();
		    if(rmi.MoveNext())
                mDefaultFont = rmi.Current.Name;
		    else
                mDefaultFont = string.Empty;

		    // Create menu overlay and container
		    mMenuOverlay = OverlayManager.Singleton.Create(mInstanceName+".MenuOverlay");
		    mMenuOverlay.Show();
		    mMenuContainer = createOverlayContainer(mInstanceName+".MenuContainer","");
		    mMenuOverlay.Add2D(mMenuContainer);
		    mMenuContainer.Show();

		    // Set zOrder very high, but leave room for containers and mouse cursor to show up correctly.
		    mMenuOverlay.ZOrder = (ushort)mMenuOverlayZOrder;
	    }

	    public override void DestroyWidget()
	    {
		    removeAndDestroyAllChildWidgets();
		    mWindows.Clear();

		    OverlayManager om = OverlayManager.Singleton;

		    //destroy menu container
            mMenuOverlay.Remove2D(mMenuContainer);
            om.DestroyOverlayElement(mMenuContainer);
            mMenuContainer = null;

            // destroy menu overlay
            om.Destroy(mMenuOverlay);
            mMenuOverlay = null;

            // destroy background overlay element
            mOverlayContainer.RemoveChild(mOverlayElement.Name);
            om.DestroyOverlayElement(mOverlayElement);
            mOverlayElement = null;
    	
            // destroy Children container
            mOverlayContainer.RemoveChild(mChildrenContainer.Name);
            om.DestroyOverlayElement(mChildrenContainer);
            mChildrenContainer = null;
    		
            // destroy default container
            mOverlay.Remove2D(mOverlayContainer);
            om.DestroyOverlayElement(mOverlayContainer);
            mOverlayContainer = null;

            // destroy overlay
            om.Destroy(mOverlay);
            
            base.DestroyWidget();
	    }

        /**
        * Internal method that sets the pixel location and size of the widget.
        */
        protected override void _applyDimensions()
	    {
		    // cannot set position/size of sheet widget..
	    }

        Menu _createMenu(string name, Vector4 dimensions, QGuiMetricsMode positionMode, QGuiMetricsMode sizeMode, string material)
        {
            Menu newMenu = new Menu(name,dimensions, positionMode, sizeMode, material,mMenuContainer,this);
            newMenu.setZOrderOffset(mMenuOverlayZOrder + 2);
            _addChildWidget(newMenu);
            // update count
            ++mNumMenus;

            return newMenu;
        }

        /** Create a Window with material, borders, and TitleBar
            @param
                name The name to be given to the Window (must be unique).
            @param
                dimensions The relative x Position, y Position, width, and height of the window.
            @param
                material Ogre material to define the Window image.
            @param
                toggle visibility.
            @note
                Private function preventing users from setting the Widget Instance Name.  Names
                can be given to Windows using the "setReferenceName()" function.
        */
        Window _createWindow(string name, Vector4 dimensions, QGuiMetricsMode positionMode, QGuiMetricsMode sizeMode, string material)
        {
            Window newWindow = new Window(name,dimensions, positionMode, sizeMode,material,this);

            int zOrder = 10;
            if( mWindows.Count != 0 )
                zOrder = mWindows[0].getMaxZOrderValue();

            // note that zOrder starts at 11.  0-10 are reserved for Sheet Widgets.
            // text widgets and text cursor widgets are usually not included in
            // a windows ZOrder List, so we add 3 to make up for these.
            newWindow.setZOrder(zOrder+3);
            mWindows.Add(newWindow);
            _addChildWidget(newWindow);

            return newWindow;
        }

        /** Create a Window with material, borders, and TitleBar
            @param
                dimensions The relative x Position, y Position, width, and height of the window.
            @param
                material Ogre material to define the Window image.
            @param
                toggle visibility.
            @note
                Name for window will be autogenerated. (Convenience method)
        */
        public Window createWindow(Vector4 dimensions, string material, QGuiMetricsMode positionMode, QGuiMetricsMode sizeMode)
        {
            // Many widgets can have no material, (label or empty window for example) but a regular window
            // must have a material!
            if(string.Empty.Equals(material))
                return null;

            string name = "DefaultWindow" + mAutoNameWindowCounter;
            ++mAutoNameWindowCounter;

            return _createWindow(name, dimensions, positionMode, sizeMode, material);
        }

        public Window createWindow(string name, Vector4 dimensions, string material, QGuiMetricsMode positionMode, QGuiMetricsMode sizeMode)
        {
            if( !(GUIManager.Singleton.validWidgetName(name)) )
                return null;

            // Many widgets can have no material, (label or empty window for example) but a regular window
            // must have a material!
            if(string.Empty.Equals(material))
                return null;

            return _createWindow(name, dimensions, positionMode, sizeMode, material);
        }

        /** Create a Window with material, borders, and TitleBar
            @param
                name Window name, must be unique.
            @param
                dimensions The relative x Position, y Position, width, and height of the window.
            @note
                Default Skin material for windows will be applied.
        */
        public Window createWindow(string name, Vector4 dimensions, QGuiMetricsMode positionMode, QGuiMetricsMode sizeMode)
        {
            if( !(GUIManager.Singleton.validWidgetName(name)) )
                return null;

            string material = mDefaultSkin + ".window";

            return _createWindow(name, dimensions, positionMode, sizeMode, material);
        }

        /** Create a Window with material, borders, and TitleBar
            @param
                dimensions The relative x Position, y Position, width, and height of the window.
            @note
                Name for window will be autogenerated. (Convenience method)
            @note
                Default Skin material for windows will be applied.
        */
        public Window createWindow(Vector4 dimensions, QGuiMetricsMode positionMode, QGuiMetricsMode sizeMode)
        {
            string name = "DefaultWindow" + mAutoNameWindowCounter;
            ++mAutoNameWindowCounter;

            string material = mDefaultSkin + ".window";

            return _createWindow(name, dimensions, positionMode, sizeMode, material);
        }

        void destroyWindow(string name)
        {
            if(string.Empty.Equals(name))
                return;

            //std::list<Window*>::iterator it;
            //for( it = mWindows.begin(); it != mWindows.end(); ++it )
            foreach(Window i in mWindows)
            {
                if( i.getInstanceName().Equals(name) )
                {
                    Window w = i;
                    _removeChildWidget(w);
                    mWindows.Remove(i);
                    w = null;
                    sortListByWindowZOrder();
                    return;
                }
            }
        }

        void destroyWindow(Window w)
        {
            destroyWindow(w.getInstanceName());
        }

        public float getDefaultCharacterHeight()
        {
            return mDefaultCharacterHeight;
        }

        public string getDefaultFont()
        {
            return mDefaultFont;
        }

        public ColourValue getDefaultTextColor()
        {
            return mDefaultTextColor;
        }

        public string getDefaultSkin()
        {
            return mDefaultSkin;
        }

        public OverlayContainer getMenuContainer()
        {
            return mMenuContainer;
        }

        public int getMenuOverlayZOrder()
        {
            return mMenuOverlayZOrder;
        }

        Window getWindow(string name)
        {
            if( string.Empty.Equals(name) )
                return null;

            //std::list<Window*>::iterator it;
            //for( it = mWindows.begin(); it != mWindows.end(); ++it )
            foreach(Window i in mWindows)
            {
                if( i.getInstanceName().Equals(name) ) 
                    return i;
            }

            return null;
        }

        /**
        * Sets the window to have the highest zOrder (shown on top of all other windows)
        */
        public void setActiveWindow(Window w)
        {
            if( mWindows.Count < 2 || mWindows[0].getInstanceName() == w.getInstanceName() )
                return;
    		
            // If we make it here, the list has at least 2 windows, and the passed in window is not the active window

            //std::list<Window*>::iterator it;
            //for( it = mWindows.begin(); it != mWindows.end(); ++it )
            foreach(Window i in mWindows)
            {
                if( w.getInstanceName() == i.getInstanceName() )
                {
                    mWindows.Remove(i);
                    break;
                }
            }

            mWindows.Add(w);

            sortListByWindowZOrder();
        }

        protected void setDefaultFont(string font)
        {
            mDefaultFont = font;
        }

        protected void setDefaultTextColor(ColourValue color)
        {
            mDefaultTextColor = color;
        }

        protected void setDefaultCharacterHeight(float height)
        {
            mDefaultCharacterHeight = height;
        }

        /**
        * Sets the default skin, used for widgets with no specified material.
        */
        protected void setDefaultSkin(string skin)
        {
            mDefaultSkin = skin;
        }

        /**
        * Iterates Window List, and adjusts zOrder of Windows.  Empty Windows are not adjusted.
        * This is to ensure that the most of the 0-650 zOrder range can be used, especially when
        * deleting and creating a lot of Windows.
        */
        protected void sortListByWindowZOrder()
        {
            // Re-organize all z-orders - min window zOrder is 10
            int zOrder = 10;

            //std::list<Window*>::reverse_iterator rIt;
            //for( rIt = mWindows.rbegin(); rIt != mWindows.rend(); ++rIt )
            for (int j = mWindows.Count-1; j >= 0; j--) // GDZ
            {
                // text widgets and text cursor widgets are usually not included in
                // a windows ZOrder List, so we add 3 to make up for these.
                mWindows[j].setZOrder(zOrder+3);
                zOrder += mWindows[j].getMaxZOrderValue();
            }
        }
    }
}
