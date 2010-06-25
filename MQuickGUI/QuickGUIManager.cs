using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Mogre;
using MOIS;

namespace MQuickGUI
{
    public class Timer
    {
        private const long ticks1970 = 621355968000000000;

        public static int time(Timer t)
        {
            return (int) ((DateTime.UtcNow.Ticks - ticks1970 ) / 10000000L);
        }
    }

   	/** Manages Windows, Mouse Cursor, and Input
		@remarks
		The most important class of QuickGUI, responsible for creating and
		destroying Windows, updating the Mouse Cursor, and handling input.
		GUIManager has a simple clearAll method, which cleans up all created
		widgets.  This supports multiple game states that have differing GUI
		Layouts.
		@note
		GUIManager is a Singleton, and frequently accessed by widgets, for
		information on the rendering window, default font, text color, character
		height, or setting focus to itself. (Window Widget does this)
		@note
		GUIManager allows 5 zOrder per window, and manages Window zOrdering so
		that windows and widgets are rendered properly on top of each other.
	*/
    public class GUIManager
    {

        private Regex objAlphaPattern = new Regex("[^a-zA-Z]");
        private Regex obDigitPattern = new Regex("[^0-9]");
        private Regex objPunctPattern = new Regex(@"[\!\#\$\*\(\)\-\:\;\,\.""\?]",
                    RegexOptions.IgnoreCase
                    | RegexOptions.Multiline
                    | RegexOptions.IgnorePatternWhitespace
                    | RegexOptions.Compiled);


        protected static GUIManager instance = null;

        protected int			mRenderWindowWidth;
		protected int			mRenderWindowHeight;

        protected QuickGUIRenderer mGUIRenderer;

		protected MouseCursor	mMouseCursor;

        protected List<string> mWidgetNames = new List<string>();

		protected Sheet		    mDefaultSheet;
		// Sheet currently being shown.
		protected Sheet		    mActiveSheet;
		// Includes the Default Sheet.
		protected List<Sheet>	mSheets = new List<Sheet>();
		protected int			mAutoNameSheetCounter;

		protected int           mClickTimeout;	// max number of seconds a click can be performed in
		protected Dictionary<MOIS.MouseButtonID, int>			mMouseButtonTimings = new Dictionary<MouseButtonID,int>();
		// Keep track of mouse button down/up and on what widget.  This prevents left mouse button down on button A,
		// moving the mouse to button B, and releasing the left mouse button, causing button B to be pressed. (example)
		protected Dictionary<MOIS.MouseButtonID, Widget>		mMouseButtonDown = new Dictionary<MouseButtonID,Widget>();

		protected Widget 		mWidgetContainingMouse;
		// Stores reference to last clicked Widget.
		protected Widget		mActiveWidget;


        /** Returns the Listener singleton object */
        public static GUIManager Singleton
        {
            get {
                if (instance == null)
                    instance = new GUIManager();
                return instance;
            }
        }
        public GUIManager() : this(0,0)
        {
        }

        /** Constructor
            @param
                RenderWindowWidth Width of the primary render window in Pixels.
            @param
                RenderWindowHeight Height of the primary render window in Pixels.
        */
        public GUIManager(uint RenderWindowWidth, uint RenderWindowHeight)
	    {

            mRenderWindowWidth = (int)RenderWindowWidth;
            mRenderWindowHeight = (int)RenderWindowHeight;
		    mMouseCursor = null;
		    mActiveSheet = null;
		    mWidgetContainingMouse = null;
		    mActiveWidget = null;
		    mClickTimeout = 1;
            mAutoNameSheetCounter = 0;

            mGUIRenderer = null;//QuickGUIRenderer.Singleton;
		    mWidgetNames.Clear();

            mMouseButtonDown[MouseButtonID.MB_Left] = null;
            mMouseButtonDown[MouseButtonID.MB_Middle] = null;
            mMouseButtonDown[MouseButtonID.MB_Right] = null;
            mMouseButtonDown[MouseButtonID.MB_Button3] = null;
            mMouseButtonDown[MouseButtonID.MB_Button4] = null;
            mMouseButtonDown[MouseButtonID.MB_Button5] = null;
            mMouseButtonDown[MouseButtonID.MB_Button6] = null;
            mMouseButtonDown[MouseButtonID.MB_Button7] = null;

		    mDefaultSheet = null;
		    mActiveSheet = mDefaultSheet;
            instance = this;
	    }

	    ~GUIManager()
	    {
		    clearAll();
	    }
	    

        /**
        * Updates MouseCursor class and all Windows that the window dimensions have changed.
        */
        public void _notifyWindowDimensions(int RenderWindowWidth, int RenderWindowHeight)
	    {
		    mRenderWindowWidth = RenderWindowWidth;
		    mRenderWindowHeight = RenderWindowHeight;
    		
		    if( mMouseCursor!=null )
                mMouseCursor._updateWindowDimensions(mRenderWindowWidth,mRenderWindowHeight);

		    //std::list<Sheet*>::iterator it;
		    //for( it = mSheets.begin(); it != mSheets.end(); ++it )
		    //	(*it)->_notifyDimensionsChanged();
            foreach(Sheet i in mSheets) {
                i._notifyDimensionsChanged();
            }
	    }

        /**
        * Iterates through Window List and destroys it, which properly destroys all child widgets.
        * NOTE: mouse cursor object is also destroyed.
        */
        void clearAll()
	    {
		    mWidgetNames.Clear();

            mMouseButtonDown[MouseButtonID.MB_Left] = null;
            mMouseButtonDown[MouseButtonID.MB_Middle] = null;
            mMouseButtonDown[MouseButtonID.MB_Right] = null;
            mMouseButtonDown[MouseButtonID.MB_Button3] = null;
            mMouseButtonDown[MouseButtonID.MB_Button4] = null;
            mMouseButtonDown[MouseButtonID.MB_Button5] = null;
            mMouseButtonDown[MouseButtonID.MB_Button6] = null;
            mMouseButtonDown[MouseButtonID.MB_Button7] = null;
		    mWidgetContainingMouse = null;
		    mActiveWidget = null;

		    //std::list<Sheet*>::iterator it;
		    //for( it = mSheets.begin(); it != mSheets.end(); ++it )
		    //	delete (*it); // GDZ
		    mSheets.Clear();

		    // reset counter
		    mAutoNameSheetCounter = 0;
		    // create default sheet
            mDefaultSheet = null;
		    mActiveSheet = mDefaultSheet;

		    mGUIRenderer = null ;

		    destroyMouseCursor();
	    }

        /** Create a Mouse Cursor representing the Mouse
            @param
                dimensions The relative x Position, y Position, width, and height of the cursor.
            @param
                material Ogre material to define the cursor image.
        */
        public MouseCursor createMouseCursor(Vector2 dimensions, string material)
	    {
		    mMouseCursor = new MouseCursor(dimensions,material,mRenderWindowWidth,mRenderWindowHeight);

		    return mMouseCursor;
	    }

        /** Create a Mouse Cursor representing the Mouse
            @param
                dimensions The relative x Position, y Position, width, and height of the cursor.
            @note
                Default Skin material for mouse cursor will be applied.
        */
        public MouseCursor createMouseCursor(Vector2 dimensions)
	    {
		    string material = "qgui.pointer";

		    return createMouseCursor(dimensions,material);
	    }

        Sheet _createSheet(string name, string material)
	    {
		    Sheet newSheet = new Sheet(name,material);

		    mSheets.Add(newSheet);

		    return newSheet;
	    }

        public Sheet createSheet(string name, string material)
	    {
		    if( !(GUIManager.Singleton.validWidgetName(name)) )
                return null;

		    return _createSheet(name,material);
	    }

        public Sheet createSheet(string name)
	    {
		    if( !(GUIManager.Singleton.validWidgetName(name)) )
                return null;

		    // the default skin for a sheet is transparency.
		    return _createSheet(name,"");
	    }

        public Sheet createSheet()
	    {
		    string name = "Sheet" + mAutoNameSheetCounter;
		    ++mAutoNameSheetCounter;

		    // the default skin for a sheet is transparency.
		    return _createSheet(name,"");
	    }

        /**
        * Destroys the Mouse Cursor - done in desctructor
        */
        void destroyMouseCursor()
	    {
		    //delete mMouseCursor;
		    mMouseCursor = null;
	    }

        /** Destroys a Window and all child widgets that exist
            @param
                name Name of the window to destroy.
            @note
                name can also be reference name given to the window.
            @note 
                no exception is thrown if window does not exist
        */
        public void destroySheet(string name)
	    {
		    if( name.Equals(string.Empty) )
                return;

		    //std::list<Sheet*>::iterator it;
		    //for( it = mSheets.begin(); it != mSheets.end(); ++it )
            foreach(Sheet i in mSheets)
		    {
			    if( i.getInstanceName().Equals(name) )
			    {
			    	i.DestroyWidget();
				    mSheets.Remove(i);
				    break;
			    }
		    }
	    }

        /** Destroys a Window and all child widgets that exist
            @param
                window Window to destroy.
        */
        public void destroySheet(Sheet sheet)
	    {
		    destroySheet(sheet.getInstanceName());
	    }

        /**
        * Returns the sheet currently being used, whether shown or hidden.
        */
        public Sheet getActiveSheet()
	    {
		    return mActiveSheet;
	    }

        /**
        * Returns the default sheet, automatically created with the GUI manager.
        */
        public Sheet getDefaultSheet()
	    {
            if (mDefaultSheet == null)
            {
                mDefaultSheet = createSheet("DefaultSheet", "");
            }
		    return mDefaultSheet;
	    }

        public MouseCursor getMouseCursor()
	    {
		    return mMouseCursor;
	    }

        public Widget getMouseOverWidget()
	    {
		    return mWidgetContainingMouse;
	    }

        QuickGUIRenderer getGUIRenderer()
	    {
		    return mGUIRenderer;
	    }

        /**
        * Width / Height.  The Width and Height are not gotten directly
        * from the Render Window, so updates to the window dimensions should notify
        * the manager, ie _notifyWindowDimensions(...).
        */
        public float getRenderWindowAspectRatio()
	    {
		    return (float)mRenderWindowWidth / (float)mRenderWindowHeight;
	    }

        /**
        * Get primary render window width in pixels
        */
        public int getRenderWindowWidth()
        {
            return mRenderWindowWidth;
        }

        /**
        * Get primary render window height in pixels
        */
        public int getRenderWindowHeight()
        {
            return mRenderWindowHeight;
        }

        /**
        * Iterates through sheet list and returns the Sheet with the
        * matching name.  Null if no match found.
        */
        public Sheet getSheet(string name)
	    {
            if (string.Empty.Equals(name))
                return null;

		    //std::list<Sheet*>::iterator it;
		    //for( it = mSheets.begin(); it != mSheets.end(); ++it )
            foreach(Sheet i in mSheets)
		    {
			    if( i.getInstanceName().Equals(name) ) 
				    return i;
		    }

		    return null;
	    }

        /**
        * Useful for Text Input Widgets, like the TextBox
        */
        public bool injectChar(char c)
	    {
		    KeyEventArgs args = new KeyEventArgs(null);
		    args.codepoint = c;
		    args.handled = false;

            if (isalpha(c))
            {
                System.Console.WriteLine("alfa");
            }

            if (isdigit(c))
            {
                System.Console.WriteLine("digit");
            }
            if (ispunct(c))
            {
                System.Console.WriteLine("punct");
            }


            if (isalpha(c) || isdigit(c) || ispunct(c) || c == ' ')
		    {
			    if(mActiveWidget != null) 
			    {
				    args.widget = mActiveWidget;
				    mActiveWidget.Character(args);
			    }
		    }

		    return args.handled;
	    }
        
        public bool injectKeyCode(KeyCode kc){
//        	KeyEventArgs args = new KeyEventArgs(null);
//		    args.codepoint = c;
//		    args.handled = false;
//
//            if (isalpha(c) || isdigit(c) || ispunct(c) || c == ' ')
//		    {
//			    if(mActiveWidget != null) 
//			    {
//				    args.widget = mActiveWidget;
//				    mActiveWidget.Character(args);
//				    mActiveWidget.
//			    }
//		    }
//
//		    return args.handled;
			return true ;
        }

        // Function To test for Alphabets.
        private bool isalpha(char c)
        {
            return !objAlphaPattern.IsMatch(""+c);
        }

        // Function To test for Digits.
        private bool isdigit(char c)
        {
            return !obDigitPattern.IsMatch("" + c);
        }

        // Function To test for Punctuation.
        private bool ispunct(char c)
        {
            return !objPunctPattern.IsMatch("" + c);
        }

        public bool injectKeyDown(KeyCode kc)
	    {
            KeyEventArgs args = new KeyEventArgs(null);
		    args.scancode = kc;
		    args.handled = false;

		    if(mActiveWidget != null) 
		    {
			    args.widget = mActiveWidget;
			    mActiveWidget.KeyDown(args);
		    }

		    return args.handled;
	    }

        public bool injectKeyUp(KeyCode kc)
	    {
		    KeyEventArgs args = new KeyEventArgs(null);
		    args.scancode = kc;
		    args.handled = false;

		    if(mActiveWidget != null) 
		    {
			    args.widget = mActiveWidget;
			    mActiveWidget.KeyUp(args);
		    }

		    return args.handled;
	    }

        public bool injectMouseButtonDown(MouseButtonID button)
	    {
		    if( (mMouseCursor == null) || (!mMouseCursor.isVisible()) )
                return false;

		    MouseEventArgs args = new MouseEventArgs(null);

		    args.position.x = mMouseCursor.getPixelPosition().x;
		    args.position.y = mMouseCursor.getPixelPosition().y;
		    args.button = button;
		    args.handled = false;

		    if(mWidgetContainingMouse != null) 
		    {
			    // mActiveWidget is the last widget the user clicked on, ie TextBox, ComboBox, etc.
			    if( (mActiveWidget != null) && (!mActiveWidget.getInstanceName().Equals(mWidgetContainingMouse.getInstanceName())) )
			    {
				    args.widget = mActiveWidget;
				    mActiveWidget.Deactivate(args);
				    // reset to false, becase within this context, the event (mouseButtonDown) has not been handled
				    args.handled = false;
				    mActiveWidget = null;
			    }

			    mActiveWidget = mWidgetContainingMouse;
    			
			    args.widget = mActiveWidget;
			    mWidgetContainingMouse.MouseButtonDown(args);
			    // Gaurds against the scenario where a handler changes game state (clears GUI)
			    if( mWidgetContainingMouse == null )
                    return args.handled;
    				
			    // When a window becomes active, activates all its child widgets (except textboxes)
			    Window w = mWidgetContainingMouse.getWindow();
			    if( w != null )
                    w.Activate(args);
			    else
                    mWidgetContainingMouse.getSheet().Activate(args);

			    // Only one textbox can be active at a time.  In the event that the clicked widget
			    // is a textbox, activate it!
			    if(mActiveWidget.getWidgetType() == Widget.WidgetType.QGUI_TYPE_TEXTBOX)
                    mActiveWidget.Activate(args);

			    // Record that the mouse button went down on this widget (non-window)
			    mMouseButtonDown[args.button] = mWidgetContainingMouse;
		    }
		    else
		    {
			    if( mActiveWidget != null )
			    {
				    args.widget = mActiveWidget;
				    mActiveWidget.Deactivate(args);
				    mActiveWidget = null;
			    }

			    mMouseButtonDown[args.button] = null;
		    }

		    mMouseButtonTimings[button] = Timer.time(null);
		    return args.handled;
	    }

        public bool injectMouseButtonUp(MouseButtonID button)
	    {
		    if( (mMouseCursor == null) || (!mMouseCursor.isVisible()) )
                return false;

		    MouseEventArgs args = new MouseEventArgs(null);

		    args.position.x = mMouseCursor.getPixelPosition().x;
		    args.position.y = mMouseCursor.getPixelPosition().y;
		    args.button = button;
		    args.handled = false;

		    // If the MouseButton was not pressed on this widget, do not register the button being released on the widget
		    // Obviously if the recorded widget is NULL, we know that nothing will be registered
            if (mMouseButtonDown[button] == null) 
		    {
			    if( mActiveWidget != null )
                    mActiveWidget.Deactivate(args);
			    return false;
		    }
		    if( mWidgetContainingMouse == null )
		    {
			    if( mActiveWidget != null )
                    mActiveWidget.Deactivate(args);
			    return false;
		    }
            if (!mMouseButtonDown[button].getInstanceName().Equals(mWidgetContainingMouse.getInstanceName()))
		    {
			    if( mActiveWidget != null )
                    mActiveWidget.Deactivate(args);
			    return false;
		    }

		    // after this point, we know that the user had mouse button down on this widget, and is now doing mouse button up

		    args.widget = mWidgetContainingMouse;

		    mWidgetContainingMouse.MouseButtonUp(args);
		    // Need to gaurd against the scenario where  mousebutton up destroys the UI
            if ((mWidgetContainingMouse != null) && (Timer.time(null) - mMouseButtonTimings[button] < mClickTimeout))
                mWidgetContainingMouse.MouseClicked(args);

		    return args.handled;
	    }


        /**
        * Injection when the mouse leaves the primary render window
        */
        public bool injectMouseLeaves()
	    {
		    if( (mMouseCursor == null) || (!mMouseCursor.isVisible()) )
                return false;

		    if(mMouseCursor.getHideWhenOffScreen())
                mMouseCursor._hide();

		    return true;
	    }

        public bool injectMouseMove(int xPixelOffset, int yPixelOffset)
	    {
		    if( mMouseCursor == null )
                return false;

		    MouseEventArgs args = new MouseEventArgs(null);
            
		    args.moveDelta.x = xPixelOffset;
		    args.moveDelta.y = yPixelOffset;
		    args.handled = false;

		    if( mMouseCursor.mouseOnTopBorder() && yPixelOffset < 0 )
                args.moveDelta.y = 0;
		    if( mMouseCursor.mouseOnBotBorder() && yPixelOffset > 0 )
                args.moveDelta.y = 0;
		    if( mMouseCursor.mouseOnLeftBorder() && xPixelOffset < 0 )
                args.moveDelta.x = 0;
		    if( mMouseCursor.mouseOnRightBorder() && xPixelOffset > 0 )
                args.moveDelta.x = 0;

		    // Update Mouse Cursor Position
		    mMouseCursor.offsetPosition(xPixelOffset,yPixelOffset);
		    args.position.x = mMouseCursor.getPixelPosition().x;
		    args.position.y = mMouseCursor.getPixelPosition().y;

            
		    if(!mMouseCursor.isVisible())
                return args.handled;

		    // Now that moving the cursor is handled, move onto widget event handling.
            if (mActiveSheet == null)
            {
                return args.handled;
            }

            // See if we should be dragging a widget.
		    if((mWidgetContainingMouse != null) && (mWidgetContainingMouse.getGrabbed()) && (mWidgetContainingMouse.draggingEnabled()) )
		    {
			    // If a widget is active, make in inactive
			    if( mActiveWidget != null && !mActiveWidget.getInstanceName().Equals(mWidgetContainingMouse.getInstanceName()) ) {
                    mActiveWidget.Deactivate(args);
                    mActiveWidget = null;
                    // restore arg values, since deactivate may have modified it.
                    args.position.x = mMouseCursor.getPixelPosition().x;
                    args.position.y = mMouseCursor.getPixelPosition().y;
                    args.moveDelta.x = xPixelOffset;
                    args.moveDelta.y = yPixelOffset;
                    args.handled = false;

                }

                // Dragging, which uses move function, works with pixel values (uninfluenced by parent dimensions!)
                mWidgetContainingMouse.drag(args.moveDelta.x, args.moveDelta.y, QGuiMetricsMode.QGUI_GMM_PIXELS);

			    return args.handled;
		    }

		    // Now get the widget the cursor is over.
		    Widget hitWidget = mActiveSheet.getTargetWidget(args.position);
            // NOTE: Widget is only detected if it is enabled.
            args.widget = hitWidget;

		    // Take care of properly firing MouseMoved, MouseEnters, and MouseLeaves events
		    if(hitWidget != null)
		    {
			    if( mWidgetContainingMouse != null )
			    {
				    if( ! mWidgetContainingMouse.getInstanceName().Equals(hitWidget.getInstanceName()) )
				    {
                        mWidgetContainingMouse.MouseLeaves(args);
					    mWidgetContainingMouse = hitWidget;
					    mWidgetContainingMouse.MouseEnters(args);
				    }
                    mWidgetContainingMouse.MouseMoved(args);
			    }
			    else
			    {
                    mWidgetContainingMouse = hitWidget;
                    mWidgetContainingMouse.MouseEnters(args);
                    mWidgetContainingMouse.MouseMoved(args);
			    }
		    }
		    else
		    {
			    if( mWidgetContainingMouse != null )
			    {
				    mWidgetContainingMouse.MouseLeaves(args);
			    }
			    mWidgetContainingMouse = null;
		    }

		    return args.handled;
	    }

        public bool injectMousePosition(int xPixelPosition, int yPixelPosition)
	    {
		    if( mMouseCursor == null )
                return false;

		    MouseEventArgs args = new MouseEventArgs(null);
		    args.handled = false;

		    Vector2 pos = mMouseCursor.getPixelPosition();

            injectMouseMove((int)(xPixelPosition - pos.x), (int)(yPixelPosition - pos.y));

		    return args.handled;
	    }

        public bool injectMouseWheelChange(float delta)
	    {
		    MouseEventArgs args = new MouseEventArgs(null);
		    args.handled = false;

		    args.wheelChange = delta;

		    if(mWidgetContainingMouse != null) 
		    {
			    args.widget = mWidgetContainingMouse;
			    mWidgetContainingMouse.MouseWheel(args);
		    }

		    return args.handled;
	    }

        public void injectTime(float time)
	    {
		    if( mActiveSheet != null )
                mActiveSheet.TimeElapsed(time);
	    }

        /**
        * Removes a name from the list of used Widget names. (if name in list)
        */
        public void removeWidgetName(string name)
	    {
		    if( name.Equals(String.Empty))
                return;

            mWidgetNames.Remove(name);
            /// REVISAR Si está bien; // GDZ
            //Ogre::StringVector::iterator it = std::remove(mWidgetNames.begin(),mWidgetNames.end(),name);

            //if( it != mWidgetNames.end() ) mWidgetNames.erase(it);
	    }


        /**
        * Hides all other sheets and shows this one.
        */
        public void setActiveSheet(Sheet s)
	    {
		    if( s == null )
                return;

		    //std::list<Sheet*>::iterator it;
		    //for( it = mSheets.begin(); it != mSheets.end(); ++it )
		    //	(*it)->hide();
            foreach (Sheet i in mSheets)
            {
                i.hide(false); // sin forzar a que los childWidget visibles pasen a estar invisibles, solo ocultarlos
            }

		    mActiveSheet = s;
		    mActiveSheet.show(false);  // sin forzar a que los childWidget no visibles pasen a estar visibles, solo mostrar los visibles

            // Update the active widget
            mActiveWidget = null;
            mWidgetContainingMouse = mActiveSheet;
            injectMouseMove(0, 0);
	    }

        /**
        * Activates the widget w, and deactivates the previously active widget. (if exists)
        */
        public void setActiveWidget(Widget w)
        {
       		if( (w.getInstanceName() == mActiveWidget.getInstanceName()) || (!w.enabled()) || (w == null) ) {
                return;
            }

		    if( mActiveWidget != null )
		    {
			    WidgetEventArgs e = new WidgetEventArgs(mActiveWidget);
			    e.handled = false;
			    mActiveWidget.Deactivate(e);
		    }

		    mActiveWidget = w;
		    WidgetEventArgs e2 = new WidgetEventArgs(mActiveWidget);
		    e2.handled = false;
		    mActiveWidget.Activate(e2);

        }

        /**
        * Checks if the desired widget name already exists.  If it already exists,
        * false is returned.  Otherwise, if addToList is true, the name is added to
        * list of used Widget names, and true is returned.
        */
        public bool validWidgetName(string name)
        {
            return validWidgetName(name, true);
        }

	    public bool validWidgetName(string name, bool addToList)
	    {
		    if( name.Equals(string.Empty) )
                return false;

		    //Ogre::StringVector::iterator it;
		    //for( it = mWidgetNames.begin(); it != mWidgetNames.end(); ++it )
		    //{
		    //	if( (*it) == name ) return false;
		    //}
            foreach(string i in mWidgetNames)
            {
                if (i.Equals(name))
                    return false;
            }

		    if(addToList)
                mWidgetNames.Add(name);

		    return true;
	    }

     }
}
