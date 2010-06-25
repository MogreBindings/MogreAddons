using Mogre;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using MOIS;

namespace MQuickGUI
{
	/**
	 * Useful for widgets horizontally aligning child widgets, for example a
	 * TitleBar aligning its label widget
	 */
	//public enum HorizontalAlignment
	//{
	//    QGUI_HA_NO_ALIGNMENT,
	//    QGUI_HA_LEFT,
	//    QGUI_HA_MID,
	//    QGUI_HA_RIGHT
	//};
	/**
	 * Useful for widgets vertically aligning child widgets, for example a
	 * TitleBar aligning its label widget
	 */
	//public enum VerticalAlignment
	//{
	//    QGUI_VA_NO_ALIGNMENT,
	//    QGUI_VA_TOP,
	//    QGUI_VA_MID,
	//    QGUI_VA_BOTTOM
	//};

	/**
	 * Allows positioning and setting size in 3 different modes.
	 * Absolute: [0.0,1.0], where 1.0 is the full width or height of screen.
	 * Relative: (-inf,+inf), where 1.0 is the full width or height of parent widget.
	 * Pixels: [0,x], where x is the width or height of the screen in pixels.
	 */
	public enum QGuiMetricsMode
	{
		QGUI_GMM_ABSOLUTE		=  0,
		QGUI_GMM_RELATIVE		=  1,
		QGUI_GMM_PIXELS         =  2
	};

	public class Widget
	{
		protected GUIManager mGUIManager;
		protected string mInstanceName;
		protected WidgetType mWidgetType;
		protected bool mVisible;

		protected bool mEnabled;
		protected ColourValue mDisabledColor;
		protected bool mGrabbed;
		protected bool mMovingEnabled;
		protected bool mDraggingEnabled;
		protected Widget mWidgetToDrag;
		// offset of zOrder in relation to parent widget.
		protected int mZOrderOffset;
		// true if parent panel has been notified of this widget's zOrder.
		protected bool mZOrderRegistered;

		protected string mFont;
		protected float mCharacterHeight;
		protected string mText;
		protected ColourValue mTextTopColor;
		protected ColourValue mTextBotColor;
		protected GuiVerticalAlignment mVerticalAlignment;
		protected GuiHorizontalAlignment mHorizontalAlignment;

		protected Widget mParentWidget;
		protected string mWidgetMaterial;
		// used for transparency picking
		protected Mogre.Image mWidgetImage;

		protected QuickGUIQuad mQuad;

		// Panel or TextArea Element, depending on the widget.
		protected OverlayElement mOverlayElement;
		protected OverlayContainer mOverlayContainer;

		protected PanelOverlayElement[] mBorders = new PanelOverlayElement[4];
		protected int[] mBorderSizeInPixels = new int[4];
		protected string[] mBorderMaterial = new string[4];
		protected bool mBordersHidden;

		// List of any child widgets this widget may have.
		protected List<Widget> mChildWidgets = new List<Widget>();
		// Each widget has an overlay container that children are attached to.
		protected OverlayContainer mChildrenContainer;

		// Everything is implemented in pixels under the hood
		protected Vector4 mPixelDimensions;
		// First two values represent the absolute positions - 0,0 to 1,1 represent top left to bottom right of render window
		// Second two values represent the absolute sizes
		protected Vector4 mAbsoluteDimensions;
		// First two values represent the relative positions - 0,0 to 1,1 represent top left to bottom right of parent widget
		// Second two values represent the relative sizes
		protected Vector4 mRelativeDimensions;

		protected QGuiMetricsMode mPositionMode;
		protected QGuiMetricsMode mSizeMode;

		/**
		 * Outlining Types of widgets in the library.
		 */
		public enum WidgetType
		{
			QGUI_TYPE_BUTTON,
			QGUI_TYPE_COMBOBOX,
			QGUI_TYPE_IMAGE,
			QGUI_TYPE_LABEL,
			QGUI_TYPE_LIST,
			QGUI_TYPE_LISTITEM,
			QGUI_TYPE_MENU,
			QGUI_TYPE_MENULIST,
			QGUI_TYPE_NSTATEBUTTON,
			QGUI_TYPE_PANEL,
			QGUI_TYPE_PROGRESSBAR,
			QGUI_TYPE_SHEET,
			QGUI_TYPE_TEXT,
			QGUI_TYPE_TEXTBOX,
			QGUI_TYPE_TEXTCURSOR,
			QGUI_TYPE_TITLEBAR,
			QGUI_TYPE_WINDOW
		};

		/**
		 * All widgets must support these events
		 */
		public enum Event
		{
			QGUI_EVENT_ACTIVATED = 0,
			QGUI_EVENT_CHARACTER_KEY = 1,
			QGUI_EVENT_DEACTIVATED = 2,
			QGUI_EVENT_KEY_DOWN = 3,
			QGUI_EVENT_KEY_UP = 4,
			QGUI_EVENT_MOUSE_BUTTON_DOWN = 5,
			QGUI_EVENT_MOUSE_BUTTON_UP = 6,
			QGUI_EVENT_MOUSE_CLICK = 7,
			QGUI_EVENT_MOUSE_ENTER = 8,
			QGUI_EVENT_MOUSE_LEAVE = 9,
			QGUI_EVENT_MOUSE_MOVE = 10,
			QGUI_EVENT_MOUSE_WHEEL = 11
		};
		/**
		 * Borders that every widget can use.
		 */
		public enum Border
		{
			QGUI_BORDER_TOP = 0,
			QGUI_BORDER_BOTTOM,
			QGUI_BORDER_LEFT,
			QGUI_BORDER_RIGHT
		};

		public Widget(string instanceName, Vector4 dimensions, QGuiMetricsMode positionMode, QGuiMetricsMode sizeMode, string WidgetMaterial, OverlayContainer overlayContainer, Widget ParentWidget)
		{
			mInstanceName = instanceName;
			mWidgetMaterial = WidgetMaterial;
			mParentWidget = ParentWidget;
			mOverlayContainer = overlayContainer;
			mAbsoluteDimensions = Vector4.ZERO;
			mVisible = true;
			mVerticalAlignment = GuiVerticalAlignment.GVA_TOP;
			mHorizontalAlignment = GuiHorizontalAlignment.GHA_LEFT;
			mGrabbed = false;
			mMovingEnabled = true;
			mBordersHidden = false;
			mZOrderOffset = 0;
			mDraggingEnabled = false;
			mEnabled = true;
			mDisabledColor = new ColourValue(0.4f, 0.4f, 0.4f);
			mWidgetImage = null;
			mZOrderRegistered = false;


			mGUIManager = GUIManager.Singleton;
			mWidgetToDrag = this;

			// calculate and store dimensions, from relative to absolute to pixel
			mPositionMode = positionMode;
			mSizeMode = sizeMode;
			mRelativeDimensions = _getRelativeDimensions(dimensions, positionMode, sizeMode);
			mAbsoluteDimensions = getAbsoluteDimensions(mRelativeDimensions);
			mPixelDimensions = absoluteToPixelDimensions(mAbsoluteDimensions);

			mQuad = new QuickGUIQuad(this);

			mChildrenContainer = createOverlayContainer(mInstanceName + ".ChildrenContainer", "");
			if (mOverlayContainer != null)
			{
				mOverlayContainer.AddChildImpl(mChildrenContainer);
			}
			//Ogre::LogManager::getSingleton().getLog("Ogre.log")->logMessage("Widget constructor - " + mInstanceName);

			// widget type has not been set yet.  If Parent is NULL,
			// we know the widget is a sheet.
			if (mParentWidget != null)
			{
				Sheet s = getSheet();
				mFont = s.getDefaultFont();
				mCharacterHeight = s.getDefaultCharacterHeight();
				mText = string.Empty;
				mTextTopColor = s.getDefaultTextColor();
				mTextBotColor = s.getDefaultTextColor();
			}

			// All widgets can create borders on initialization except the Window widget, since
			// mOverlayContainer is required. mOverlayContainer is created in the Window constructor, not
			// the widget constructor.
			_createBorders();
		}

		// Event Handler functions - should be overriden to supply default functionality,
		// For example, the button won't look like a button unless you apply the *.over and
		// *.down material during onMouseEnters, Leaves, ButtonDown, etc. events

		public virtual void DestroyWidget()
		{
			removeAndDestroyAllChildWidgets();

			_destroyBorders();

			OverlayManager om = OverlayManager.Singleton;

			if (mOverlayElement != null)
			{
				// destroy Children container
				
				//TODO: Revisar, no anda en el dispose
				mOverlayContainer.RemoveChild(mChildrenContainer.Name);
				om.DestroyOverlayElement(mChildrenContainer);
				mChildrenContainer = null;

				// destroy OverlayElement
				//TODO: Revisar, no anda en el dispose
				if (mOverlayElement.Parent != null)
					mOverlayElement.Parent.RemoveChild(mOverlayElement.Name);
				om.DestroyOverlayElement(mOverlayElement);
				mOverlayElement = null;
			}

			unregisterZOrder(false);
			mParentWidget = null;

			GUIManager.Singleton.removeWidgetName(mInstanceName);
		}

		public delegate void ActivateEventHandler(object source, EventArgs e);
		public event ActivateEventHandler OnActivate;

		public delegate void DeactivateEventHandler(object source, EventArgs e);
		public event DeactivateEventHandler OnDeactivate;

		public delegate void MouseEnterEventHandler(object source, MouseEventArgs e);
		public event MouseEnterEventHandler OnMouseEnter;

		public delegate void MouseClickedEventHandler(object source, MouseEventArgs e);
		public event MouseClickedEventHandler OnMouseClicked;

		public delegate void MouseMovedEventHandler(object source, MouseEventArgs e);
		public event MouseMovedEventHandler OnMouseMoved;

		public delegate void MouseLeavesEventHandler(object source, MouseEventArgs e);
		public event MouseLeavesEventHandler OnMouseLeaves;

		public delegate void MouseWheelEventHandler(object source, MouseEventArgs e);
		public event MouseWheelEventHandler OnMouseWheel;

		public delegate void MouseButtonUpEventHandler(object source, MouseEventArgs e);
		public event MouseButtonUpEventHandler OnMouseButtonUp;

		public delegate void MouseButtonDownEventHandler(object source, MouseEventArgs e);
		public event MouseButtonDownEventHandler OnMouseButtonDown;

		public delegate void KeyDownEventHandler(object source, KeyEventArgs e);
		public event KeyDownEventHandler OnKeyDown;

		public delegate void KeyUpEventHandler(object source, KeyEventArgs e);
		public event KeyUpEventHandler OnKeyUp;

		public delegate void CharacterEventHandler(object source, KeyEventArgs e);
		public event CharacterEventHandler OnCharacter;

		public delegate void DraggedEventHandler(object source, WidgetEventArgs e);
		public event DraggedEventHandler OnDragged;
		
		public delegate void TimeElapsedHandler(object source, TimeEventArgs e);
		public event TimeElapsedHandler OnTimeElapsed;

		// Function that allows widgets to add a child widget after creation.
		protected void _addChildWidget(Widget w)
		{
			w.setParentWidget(this);
			mChildWidgets.Add(w);
			w.registerZOrder();
		}

		// sets the widget position/size based on mPixelDimensions
		protected virtual void _applyDimensions()
		{
			mOverlayElement.HorizontalAlignment = mHorizontalAlignment;
			mOverlayElement.VerticalAlignment = mVerticalAlignment;
			mOverlayElement.SetPosition(mPixelDimensions.x, mPixelDimensions.y);
			mOverlayElement.SetDimensions(mPixelDimensions.z, mPixelDimensions.w);
		}

		// Internal method to create widget borders.
		protected void _createBorders()
		{
			mBorders[(int)Widget.Border.QGUI_BORDER_TOP] = null;
			mBorders[(int)Widget.Border.QGUI_BORDER_BOTTOM] = null;
			mBorders[(int)Widget.Border.QGUI_BORDER_LEFT] = null;
			mBorders[(int)Widget.Border.QGUI_BORDER_RIGHT] = null;
			mBorderSizeInPixels[(int)Widget.Border.QGUI_BORDER_TOP] = 0;
			mBorderSizeInPixels[(int)Widget.Border.QGUI_BORDER_BOTTOM] = 0;
			mBorderSizeInPixels[(int)Widget.Border.QGUI_BORDER_LEFT] = 0;
			mBorderSizeInPixels[(int)Widget.Border.QGUI_BORDER_RIGHT] = 0;

			if(mOverlayContainer == null)
				return;

			// Making the Borders - First step, get that border material information
			MaterialManager mm = MaterialManager.Singleton;
			// The material has a .border material to define all borders
			if( mm.ResourceExists(mWidgetMaterial+".border") )
			{
				string borderMaterial = mWidgetMaterial+".border";
				mBorderMaterial[(int)Widget.Border.QGUI_BORDER_TOP] = borderMaterial;
				mBorderMaterial[(int)Widget.Border.QGUI_BORDER_BOTTOM] = borderMaterial;
				mBorderMaterial[(int)Widget.Border.QGUI_BORDER_LEFT] = borderMaterial;
				mBorderMaterial[(int)Widget.Border.QGUI_BORDER_RIGHT] = borderMaterial;
			}
			else
			{
				if( mm.ResourceExists(mWidgetMaterial+".border.top") )
					mBorderMaterial[(int)Widget.Border.QGUI_BORDER_TOP] = mWidgetMaterial+".border.top";
				else
					mBorderMaterial[(int)Widget.Border.QGUI_BORDER_TOP] = string.Empty;

				if( mm.ResourceExists(mWidgetMaterial+".border.bottom") )
					mBorderMaterial[(int)Widget.Border.QGUI_BORDER_BOTTOM] = mWidgetMaterial+".border.bottom";
				else
					mBorderMaterial[(int)Widget.Border.QGUI_BORDER_BOTTOM] = string.Empty;

				if( mm.ResourceExists(mWidgetMaterial+".border.left") )
					mBorderMaterial[(int)Widget.Border.QGUI_BORDER_LEFT] = mWidgetMaterial+".border.left";
				else
					mBorderMaterial[(int)Widget.Border.QGUI_BORDER_LEFT] = string.Empty;

				if( mm.ResourceExists(mWidgetMaterial+".border.right") )
					mBorderMaterial[(int)Widget.Border.QGUI_BORDER_RIGHT] = mWidgetMaterial+".border.right";
				else
					mBorderMaterial[(int)Widget.Border.QGUI_BORDER_RIGHT] = string.Empty;
			}

			// Now border materials are stored.  Set Default Border thickness in pixels
			mBorderSizeInPixels[(int)Widget.Border.QGUI_BORDER_TOP] = 3;
			mBorderSizeInPixels[(int)Widget.Border.QGUI_BORDER_BOTTOM] = 3;
			mBorderSizeInPixels[(int)Widget.Border.QGUI_BORDER_LEFT] = 3;
			mBorderSizeInPixels[(int)Widget.Border.QGUI_BORDER_RIGHT] = 3;

			// Create the PanelOverlayElements, with a default of 3 pixel width
			Vector4 defaultTBorderDimensions = new Vector4 (
				mPixelDimensions.x - mBorderSizeInPixels[(int)Widget.Border.QGUI_BORDER_LEFT],
				mPixelDimensions.y - mBorderSizeInPixels[(int)Widget.Border.QGUI_BORDER_TOP],
				mPixelDimensions.z + mBorderSizeInPixels[(int)Widget.Border.QGUI_BORDER_LEFT] + mBorderSizeInPixels[(int)Widget.Border.QGUI_BORDER_RIGHT],
				mBorderSizeInPixels[(int)Widget.Border.QGUI_BORDER_TOP]);

			Vector4 defaultBBorderDimensions = new Vector4 (
				mPixelDimensions.x - mBorderSizeInPixels[(int)Widget.Border.QGUI_BORDER_LEFT],
				mPixelDimensions.y + mPixelDimensions.w,
				mPixelDimensions.z + mBorderSizeInPixels[(int)Widget.Border.QGUI_BORDER_LEFT] + mBorderSizeInPixels[(int)Widget.Border.QGUI_BORDER_RIGHT],
				mBorderSizeInPixels[(int)Widget.Border.QGUI_BORDER_BOTTOM]);

			int lWidth = mBorderSizeInPixels[(int)Widget.Border.QGUI_BORDER_LEFT];

			Vector4 defaultLBorderDimensions = new Vector4 (
				mPixelDimensions.x - mBorderSizeInPixels[(int)Widget.Border.QGUI_BORDER_LEFT],
				mPixelDimensions.y - mBorderSizeInPixels[(int)Widget.Border.QGUI_BORDER_TOP],
				mBorderSizeInPixels[(int)Widget.Border.QGUI_BORDER_LEFT],
				mPixelDimensions.w + mBorderSizeInPixels[(int)Widget.Border.QGUI_BORDER_TOP] + mBorderSizeInPixels[(int)Widget.Border.QGUI_BORDER_BOTTOM]);

			int rWidth = mBorderSizeInPixels[(int)Widget.Border.QGUI_BORDER_RIGHT];

			Vector4 defaultRBorderDimensions = new Vector4 (
				mPixelDimensions.x + mPixelDimensions.z,
				mPixelDimensions.y - mBorderSizeInPixels[(int)Widget.Border.QGUI_BORDER_TOP],
				mBorderSizeInPixels[(int)Widget.Border.QGUI_BORDER_RIGHT],
				mPixelDimensions.w + mBorderSizeInPixels[(int)Widget.Border.QGUI_BORDER_TOP] + mBorderSizeInPixels[(int)Widget.Border.QGUI_BORDER_BOTTOM]);

			mBorders[(int)Widget.Border.QGUI_BORDER_TOP] = createPanelOverlayElement(mInstanceName + ".border.top", defaultTBorderDimensions, mBorderMaterial[(int)Widget.Border.QGUI_BORDER_TOP]);
			mBorders[(int)Widget.Border.QGUI_BORDER_BOTTOM] = createPanelOverlayElement(mInstanceName + ".border.bottom", defaultBBorderDimensions, mBorderMaterial[(int)Widget.Border.QGUI_BORDER_BOTTOM]);
			mBorders[(int)Widget.Border.QGUI_BORDER_LEFT] = createPanelOverlayElement(mInstanceName + ".border.left", defaultLBorderDimensions, mBorderMaterial[(int)Widget.Border.QGUI_BORDER_LEFT]);
			mBorders[(int)Widget.Border.QGUI_BORDER_RIGHT] = createPanelOverlayElement(mInstanceName + ".border.right", defaultRBorderDimensions, mBorderMaterial[(int)Widget.Border.QGUI_BORDER_RIGHT]);

			// Attach panels and show
			mOverlayContainer.AddChild(mBorders[(int)Widget.Border.QGUI_BORDER_TOP]);
			mBorders[(int)Widget.Border.QGUI_BORDER_TOP].Show();
			mOverlayContainer.AddChild(mBorders[(int)Widget.Border.QGUI_BORDER_BOTTOM]);
			mBorders[(int)Widget.Border.QGUI_BORDER_BOTTOM].Show();
			mOverlayContainer.AddChild(mBorders[(int)Widget.Border.QGUI_BORDER_LEFT]);
			mBorders[(int)Widget.Border.QGUI_BORDER_LEFT].Show();
			mOverlayContainer.AddChild(mBorders[(int)Widget.Border.QGUI_BORDER_RIGHT]);
			mBorders[(int)Widget.Border.QGUI_BORDER_RIGHT].Show();
		}

		// Internal method to destroy widget borders.
		protected void _destroyBorders()
		{
			if (mOverlayContainer == null)
				return;

			OverlayManager om = OverlayManager.Singleton;

			int index;
			for (index = 0; index < 4; ++index)
			{
				if (mBorders[index] != null)
				{
					// TODO: Revisar. No funciona en el Dispose
					mOverlayContainer.RemoveChild(mBorders[index].Name);
					om.DestroyOverlayElement(mBorders[index]);
					mBorders[index] = null;
				}
			}
		}

		// Calculates relative dimensions.
		protected Vector4 _getRelativeDimensions(Vector4 dimensions, QGuiMetricsMode positionMode, QGuiMetricsMode sizeMode)
		{
			Vector2 relPos = _getRelativePosition(new Vector2(dimensions.x,dimensions.y),positionMode);
			Vector2 relSize = _getRelativeSize(new Vector2(dimensions.z,dimensions.w),sizeMode);

			return new Vector4(relPos.x,relPos.y,relSize.x,relSize.y);
		}

		// Calculates relative position.
		protected Vector2 _getRelativePosition(Vector2 position, QGuiMetricsMode mode)
		{
			Vector2 retVal = Vector2.ZERO;

			float windowWidth = mGUIManager.getRenderWindowWidth();
			float windowHeight = mGUIManager.getRenderWindowHeight();
			Vector2 parentAbsSize = new Vector2(1,1);
			Vector2 parentPixelSize = new Vector2(windowWidth,windowHeight);
			if(mParentWidget != null)
			{
				parentPixelSize = mParentWidget.getSize(QGuiMetricsMode.QGUI_GMM_PIXELS);
				parentAbsSize = mParentWidget.getSize(QGuiMetricsMode.QGUI_GMM_ABSOLUTE);
			}

			switch(mode)
			{
				case QGuiMetricsMode.QGUI_GMM_ABSOLUTE:
					retVal.x = position.x / parentAbsSize.x;
					retVal.y = position.y / parentAbsSize.y;
					break;
				case QGuiMetricsMode.QGUI_GMM_RELATIVE:
					retVal.x = position.x;
					retVal.y = position.y;
					break;
				case QGuiMetricsMode.QGUI_GMM_PIXELS:
					retVal.x = position.x / parentPixelSize.x;
					retVal.y = position.y / parentPixelSize.y;
					break;
			}

			return retVal;
		}

		// Calculates relative size.
		protected Vector2 _getRelativeSize(Vector2 size, QGuiMetricsMode mode)
		{
			Vector2 retVal = Vector2.ZERO;

			float windowWidth = mGUIManager.getRenderWindowWidth();
			float windowHeight = mGUIManager.getRenderWindowHeight();
			Vector2 parentAbsSize = new Vector2(1,1);
			Vector2 parentPixelSize = new Vector2(windowWidth,windowHeight);
			if(mParentWidget != null)
			{
				parentPixelSize = mParentWidget.getSize(QGuiMetricsMode.QGUI_GMM_PIXELS);
				parentAbsSize = mParentWidget.getSize(QGuiMetricsMode.QGUI_GMM_ABSOLUTE);
			}

			switch(mode)
			{
				case QGuiMetricsMode.QGUI_GMM_ABSOLUTE:
					retVal.x = size.x / parentAbsSize.x;
					retVal.y = size.y / parentAbsSize.y;
					break;
				case QGuiMetricsMode.QGUI_GMM_RELATIVE:
					retVal.x = size.x;
					retVal.y = size.y;
					break;
				case QGuiMetricsMode.QGUI_GMM_PIXELS:
					retVal.x = size.x / parentPixelSize.x;
					retVal.y = size.y / parentPixelSize.y;
					break;
			}

			return retVal;
		}

		// Recalculate the dimensions and reposition/resize the widget (calls _updateDimensions)
		internal virtual void _notifyDimensionsChanged()
		{
			// Update Widget dimensions - done first.
			_updateDimensions(mRelativeDimensions);
			_applyDimensions(); // Widget specific

			foreach(Widget i in mChildWidgets)
			{
				i._notifyDimensionsChanged();
			}

			// update Borders. Make sure this is done after dimensions are updated/applied.
			_updateBorderSize();

			Vector2 TBorderPos = new Vector2(mPixelDimensions.x - mBorderSizeInPixels[(int)Widget.Border.QGUI_BORDER_LEFT], mPixelDimensions.y - mBorderSizeInPixels[(int)Widget.Border.QGUI_BORDER_TOP]);
			Vector2 BBorderPos = new Vector2(mPixelDimensions.x - mBorderSizeInPixels[(int)Widget.Border.QGUI_BORDER_LEFT], mPixelDimensions.y + mPixelDimensions.w);
			Vector2 LBorderPos = new Vector2(mPixelDimensions.x - mBorderSizeInPixels[(int)Widget.Border.QGUI_BORDER_LEFT], mPixelDimensions.y - mBorderSizeInPixels[(int)Widget.Border.QGUI_BORDER_TOP]);
			Vector2 RBorderPos = new Vector2(mPixelDimensions.x + mPixelDimensions.z, mPixelDimensions.y - mBorderSizeInPixels[(int)Widget.Border.QGUI_BORDER_TOP]);

			if(mBorders[(int)Widget.Border.QGUI_BORDER_TOP] != null)
				mBorders[(int)Widget.Border.QGUI_BORDER_TOP].SetPosition(TBorderPos.x,TBorderPos.y);
			if (mBorders[(int)Widget.Border.QGUI_BORDER_BOTTOM] != null)
				mBorders[(int)Widget.Border.QGUI_BORDER_BOTTOM].SetPosition(BBorderPos.x, BBorderPos.y);
			if (mBorders[(int)Widget.Border.QGUI_BORDER_LEFT] != null)
				mBorders[(int)Widget.Border.QGUI_BORDER_LEFT].SetPosition(LBorderPos.x,LBorderPos.y);
			if (mBorders[(int)Widget.Border.QGUI_BORDER_RIGHT] != null)
				mBorders[(int)Widget.Border.QGUI_BORDER_RIGHT].SetPosition(RBorderPos.x, RBorderPos.y);
		}

		// Realign text, if the widget has a child label widget
		protected virtual void _notifyTextChanged()
		{
		}

		// Function that allows widgets to remove child widgets.
		protected void _removeChildWidget(Widget w)
		{
			//std::vector<Widget*>::iterator it;
			//for( it = mChildWidgets.begin(); it != mChildWidgets.end(); ++it )
			foreach(Widget i in mChildWidgets)
			{
				if( i.getInstanceName().Equals(w.getInstanceName()) )
				{
					i.unregisterZOrder();
					i.setParentWidget(null, false);
					mChildWidgets.Remove(i);
					break;
				}
			}
		}

		// Calculate Absolute and Pixel dimensions
		protected void _updateDimensions(Vector4 relativeDimensions)
		{
			mRelativeDimensions = relativeDimensions;
			mAbsoluteDimensions = getAbsoluteDimensions(mRelativeDimensions);
			mPixelDimensions = absoluteToPixelDimensions(mAbsoluteDimensions);
		}

		// Internal method to update border sizes
		void _updateBorderSize()
		{

			if (mBorders[(int)Widget.Border.QGUI_BORDER_TOP] != null)
			{
				mBorders[(int)Widget.Border.QGUI_BORDER_TOP].Height = mBorderSizeInPixels[(int)Widget.Border.QGUI_BORDER_TOP];
				// Adjust width to incorporate width's of left and right borders
				mBorders[(int)Widget.Border.QGUI_BORDER_TOP].Width = mPixelDimensions.z + mBorderSizeInPixels[(int)Widget.Border.QGUI_BORDER_LEFT] + mBorderSizeInPixels[(int)Widget.Border.QGUI_BORDER_RIGHT];
			}

			if (mBorders[(int)Widget.Border.QGUI_BORDER_BOTTOM] != null)
			{
				mBorders[(int)Widget.Border.QGUI_BORDER_BOTTOM].Height = mBorderSizeInPixels[(int)Widget.Border.QGUI_BORDER_BOTTOM];
				// Adjust width to incorporate width's of left and right borders
				mBorders[(int)Widget.Border.QGUI_BORDER_BOTTOM].Width = mPixelDimensions.z + mBorderSizeInPixels[(int)Widget.Border.QGUI_BORDER_LEFT] + mBorderSizeInPixels[(int)Widget.Border.QGUI_BORDER_RIGHT];
			}

			if (mBorders[(int)Widget.Border.QGUI_BORDER_LEFT] != null)
			{
				mBorders[(int)Widget.Border.QGUI_BORDER_LEFT].Width = mBorderSizeInPixels[(int)Widget.Border.QGUI_BORDER_LEFT];
				// Adjust width to incorporate width's of top and bottom borders
				mBorders[(int)Widget.Border.QGUI_BORDER_LEFT].Height = mPixelDimensions.w + mBorderSizeInPixels[(int)Widget.Border.QGUI_BORDER_TOP] + mBorderSizeInPixels[(int)Widget.Border.QGUI_BORDER_BOTTOM];
			}

			if (mBorders[(int)Widget.Border.QGUI_BORDER_RIGHT] != null)
			{
				mBorders[(int)Widget.Border.QGUI_BORDER_RIGHT].Width = mBorderSizeInPixels[(int)Widget.Border.QGUI_BORDER_RIGHT];
				// Adjust width to incorporate width's of top and bottom borders
				mBorders[(int)Widget.Border.QGUI_BORDER_RIGHT].Height = mPixelDimensions.w + mBorderSizeInPixels[(int)Widget.Border.QGUI_BORDER_TOP] + mBorderSizeInPixels[(int)Widget.Border.QGUI_BORDER_BOTTOM];
			}
		}
		
		// Reemplazado por Manejo de Eventos // GDZ
		//public void addEventHandler(Event EVENT, IMemberFunctionSlot handler)
		//{
		//    mUserEventHandlers[(int)EVENT].Add(handler);
		//}

		// COMMENT TAKEN DIRECTLY FROM OGRE
		/** A 2D element which contains other OverlayElement instances.
        @remarks
            This is a specialisation of OverlayElement for 2D elements that contain other
            elements. These are also the smallest elements that can be attached directly
            to an Overlay.
        @remarks
            OverlayContainers should be managed using OverlayManager. This class is responsible for
            instantiating / deleting elements, and also for accepting new types of element
            from plugins etc.
		 */
		protected OverlayContainer createOverlayContainer(string name, string material)
		{
			OverlayContainer newOverlayContainer = null;

			newOverlayContainer = (OverlayContainer)OverlayManager.Singleton.CreateOverlayElement("Panel", name);
			newOverlayContainer.MetricsMode = GuiMetricsMode.GMM_PIXELS;
			newOverlayContainer.SetPosition(0, 0);
			newOverlayContainer.SetDimensions(1.0f, 1.0f);
			newOverlayContainer.Colour = ColourValue.Red;
			if (!material.Equals(string.Empty))
				newOverlayContainer.MaterialName = material;

			return newOverlayContainer;
		}

		// COMMENT TAKEN DIRECTLY FROM OGRE
		/** OverlayElement representing a flat, single-material (or transparent) panel which can contain other elements.
        @remarks
            This class subclasses OverlayContainer because it can contain other elements. Like other
            containers, if hidden it's contents are also hidden, if moved it's contents also move etc.
            The panel itself is a 2D rectangle which is either completely transparent, or is rendered
            with a single material. The texture(s) on the panel can be tiled depending on your requirements.
        @par
            This component is suitable for backgrounds and grouping other elements. Note that because
            it has a single repeating material it cannot have a discrete border (unless the texture has one and
            the texture is tiled only once). For a bordered panel, see it's subclass BorderPanelOverlayElement.
        @par
            Note that the material can have all the usual effects applied to it like multiple texture
            layers, scrolling / animated textures etc. For multiple texture layers, you have to set
            the tiling level for each layer.
		 */
		protected PanelOverlayElement createPanelOverlayElement(string name, Vector4 dimensions, string material)
		{
			PanelOverlayElement newPanelOverlayElement = null;

			newPanelOverlayElement = (PanelOverlayElement)(OverlayManager.Singleton.CreateOverlayElement("Panel", name));
			newPanelOverlayElement.MetricsMode = GuiMetricsMode.GMM_PIXELS;
			newPanelOverlayElement.VerticalAlignment = mVerticalAlignment;
			newPanelOverlayElement.HorizontalAlignment = mHorizontalAlignment;
			newPanelOverlayElement.SetPosition(dimensions.x, dimensions.y);
			newPanelOverlayElement.SetDimensions(dimensions.z, dimensions.w);
			newPanelOverlayElement.Colour = ColourValue.Blue;
			if (!material.Equals(string.Empty))
				newPanelOverlayElement.MaterialName = material;

			return newPanelOverlayElement;
		}

		// COMMENT TAKEN FROM OGRE
		/** This class implements an overlay element which contains simple unformatted text.
		 */
		protected TextAreaOverlayElement createTextAreaOverlayElement(string name, Vector4 dimensions, string material)
		{
			TextAreaOverlayElement newTextAreaOverlayElement = null;

			newTextAreaOverlayElement = (TextAreaOverlayElement)OverlayManager.Singleton.CreateOverlayElement("TextArea",name);
			newTextAreaOverlayElement.MetricsMode = GuiMetricsMode.GMM_PIXELS;
			newTextAreaOverlayElement.VerticalAlignment = mVerticalAlignment;
			newTextAreaOverlayElement.HorizontalAlignment = mHorizontalAlignment;
			newTextAreaOverlayElement.SetPosition(dimensions.x, dimensions.y);
			newTextAreaOverlayElement.SetDimensions(dimensions.z, dimensions.w);
			newTextAreaOverlayElement.Colour = ColourValue.Green;
			if(!material.Equals(string.Empty))
				newTextAreaOverlayElement.MaterialName = material;

			return newTextAreaOverlayElement;
		}

		/**
		 * Internal method.  User works in relative coordinates, but internally everything is done in pixels.
		 * Used to calculate the pixel size acording to the dimensions provided.
		 */
		protected Vector4 absoluteToPixelDimensions(Vector4 dimensions)
		{
			float renderWindowWidth = mGUIManager.getRenderWindowWidth();
			float renderWindowHeight = mGUIManager.getRenderWindowHeight();

			return new Vector4(
				dimensions.x * renderWindowWidth,
				dimensions.y * renderWindowHeight,
				dimensions.z * renderWindowWidth,
				dimensions.w * renderWindowHeight);
		}

		/**
		 * Disable Widget, making it unresponsive to events.
		 */
		public void disable()
		{
			if((mWidgetType == Widget.WidgetType.QGUI_TYPE_TEXT) ||
			   (mWidgetType == Widget.WidgetType.QGUI_TYPE_TEXTCURSOR) ||
			   (mWidgetType == Widget.WidgetType.QGUI_TYPE_SHEET))
				return;

			mEnabled = false;
			MaterialPtr mp = MaterialManager.Singleton.GetByName(mWidgetMaterial);

			MaterialPtr disabledMaterial;
			if( MaterialManager.Singleton.ResourceExists(mWidgetMaterial+".disabled") )
				disabledMaterial = MaterialManager.Singleton.GetByName(mWidgetMaterial+".disabled");
			else
			{
				disabledMaterial = mp.Clone(mWidgetMaterial+".disabled");
				Pass p = disabledMaterial.GetTechnique(0).GetPass(0);

				if( (p != null) && (p.NumTextureUnitStates >= 1) )
				{
					TextureUnitState tus = p.CreateTextureUnitState();
					tus.SetColourOperationEx(LayerBlendOperationEx.LBX_MODULATE, LayerBlendSource.LBS_MANUAL, LayerBlendSource.LBS_CURRENT, mDisabledColor);
				}
			}

			setTextColor(mDisabledColor);
			mOverlayElement.MaterialName = disabledMaterial.Name;
		}

		/**
		 * Moves draggingWidget.  By default, dragging widget is this widget, but this can be changed.
		 * Allows dragging the titlebar or it's text to drag the window, for example.
		 */
		public void drag(float relativeXOffset, float relativeYOffset, QGuiMetricsMode mode)
		{
			if(!mDraggingEnabled)
				return;

			if (mWidgetToDrag!=null)
				mWidgetToDrag.move(relativeXOffset,relativeYOffset, mode);
			// fire onDragged Event.
			WidgetEventArgs args = new WidgetEventArgs(this);
			Dragged(args);
		}

		/**
		 * Returns true if the widget is able to be dragged, false otherwise.
		 */
		public bool draggingEnabled()
		{
			return mDraggingEnabled;
		}

		/**
		 * Enable Widget, allowing it to accept and handle events.
		 */
		public void enable()
		{
			if((mWidgetType == Widget.WidgetType.QGUI_TYPE_TEXT) ||
			   (mWidgetType == Widget.WidgetType.QGUI_TYPE_TEXTCURSOR) ||
			   (mWidgetType == Widget.WidgetType.QGUI_TYPE_SHEET))
				return;

			mEnabled = true;
			setTextColor(mTextTopColor, mTextBotColor);
			mOverlayElement.MaterialName = mWidgetMaterial;
		}

		/**
		 * Returns true is widget is enabled, false otherwise.
		 */
		public bool enabled()
		{
			return mEnabled;
		}

		/**
		 * Enable or Disable dragging.
		 */
		public void enableDragging(bool enable)
		{
			mDraggingEnabled = enable;
		}

		/**
		 * Sets focus to the widget by firing an activation event.
		 */
		public virtual void focus()
		{
			mGUIManager.setActiveWidget(this);
		}

		List<Widget> getChildWidgetList()
		{
			return mChildWidgets;
		}

		Widget getChildWidget(string name)
		{
			Widget w = null;
			if (string.Empty.Equals(name))
				return w;

			foreach (Widget i in mChildWidgets)
			{
				if (i.getChildWidget(name) != null)
				{
					w = i;
					break;
				}
			}

			if (w != null)
				return w;
			else if (mInstanceName == name)
				return this;
			else
				return null;
		}

		public Vector4 getDimensions(QGuiMetricsMode position, QGuiMetricsMode size)
		{
			Vector4 retVal = Vector4.ZERO;

			switch (position)
			{
				case QGuiMetricsMode.QGUI_GMM_ABSOLUTE:
					retVal.x = mAbsoluteDimensions.x;
					retVal.y = mAbsoluteDimensions.y;
					break;
				case QGuiMetricsMode.QGUI_GMM_RELATIVE:
					retVal.x = mRelativeDimensions.x;
					retVal.y = mRelativeDimensions.y;
					break;
				case QGuiMetricsMode.QGUI_GMM_PIXELS:
					retVal.x = mPixelDimensions.x;
					retVal.y = mPixelDimensions.y;
					break;
			}

			switch (size)
			{
				case QGuiMetricsMode.QGUI_GMM_ABSOLUTE:
					retVal.z = mAbsoluteDimensions.z;
					retVal.w = mAbsoluteDimensions.w;
					break;
				case QGuiMetricsMode.QGUI_GMM_RELATIVE:
					retVal.z = mRelativeDimensions.z;
					retVal.w = mRelativeDimensions.w;
					break;
				case QGuiMetricsMode.QGUI_GMM_PIXELS:
					retVal.z = mPixelDimensions.z;
					retVal.w = mPixelDimensions.w;
					break;
			}

			return retVal;
		}

		public Vector2 getPosition(QGuiMetricsMode mode)
		{
			Vector2 retVal = Vector2.ZERO;

			switch (mode)
			{
				case QGuiMetricsMode.QGUI_GMM_ABSOLUTE:
					retVal.x = mAbsoluteDimensions.x;
					retVal.y = mAbsoluteDimensions.y;
					break;
				case QGuiMetricsMode.QGUI_GMM_RELATIVE:
					retVal.x = mRelativeDimensions.x;
					retVal.y = mRelativeDimensions.y;
					break;
				case QGuiMetricsMode.QGUI_GMM_PIXELS:
					retVal.x = mPixelDimensions.x;
					retVal.y = mPixelDimensions.y;
					break;
			}

			return retVal;
		}


		public Vector2 getSize(QGuiMetricsMode mode)
		{
			Vector2 retVal = Vector2.ZERO;

			switch (mode)
			{
				case QGuiMetricsMode.QGUI_GMM_ABSOLUTE:
					retVal.x = mAbsoluteDimensions.z;
					retVal.y = mAbsoluteDimensions.w;
					break;
				case QGuiMetricsMode.QGUI_GMM_RELATIVE:
					retVal.x = mRelativeDimensions.z;
					retVal.y = mRelativeDimensions.w;
					break;
				case QGuiMetricsMode.QGUI_GMM_PIXELS:
					retVal.x = mPixelDimensions.z;
					retVal.y = mPixelDimensions.w;
					break;
			}

			return retVal;
		}




		/* Uses parent widget's absolute dimensions with given relative dimensions to produce absolute (screen) dimensions */
		public Vector4 getAbsoluteDimensions(Vector4 relativeDimensions)
		{
			Vector4 ParentAbsoluteDimensions;

			if (mParentWidget == null)
				ParentAbsoluteDimensions = new Vector4(0, 0, 1, 1);
			else
				ParentAbsoluteDimensions = mParentWidget.getDimensions(QGuiMetricsMode.QGUI_GMM_ABSOLUTE, QGuiMetricsMode.QGUI_GMM_ABSOLUTE);

			float x = ParentAbsoluteDimensions.x + (ParentAbsoluteDimensions.z * relativeDimensions.x);
			float y = ParentAbsoluteDimensions.y + (ParentAbsoluteDimensions.w * relativeDimensions.y);
			float z = ParentAbsoluteDimensions.z * relativeDimensions.z;
			float w = ParentAbsoluteDimensions.w * relativeDimensions.w;

			return new Vector4(x, y, z, w);
		}

		public float getCharacterHeight()
		{
			return mCharacterHeight;
		}

		public ColourValue getDisabledColor()
		{
			return mDisabledColor;
		}

		public string getFont()
		{
			return mFont;
		}

		public bool getGrabbed()
		{
			return mGrabbed;
		}

		public string getInstanceName()
		{
			return mInstanceName;
		}

		/**
		 * Returns true if window is able to be repositions, false otherwise.
		 */
		protected bool getMovingEnabled()
		{
			return mMovingEnabled;
		}

		/**
		 * Returns the Overlay Container is used to contain the Widget's OverlayElements
		 */
		protected OverlayContainer getOverlayContainer()
		{
			return mOverlayContainer;
		}

		/**
		 * Returns the Widget's Parent Widget.  Does not throw an exception is the Parent
		 * does not exist.  (Windows do not have parent widgets)
		 */
		protected Widget getParentWidget()
		{
			return mParentWidget;
		}
		
		/**
		 * Get Panel this widget belongs to.  If the widget is a Panel, it is returned.
		 * NOTE: Windows and Sheets are Panels.
		 */
		Panel getParentPanel()
		{
			if( mWidgetType == WidgetType.QGUI_TYPE_PANEL )
				return (Panel)this;

			// iterate through parents to find parent panel.
			Widget w = mParentWidget;
			while( w != null )
			{
				if( w.getWidgetType() == WidgetType.QGUI_TYPE_PANEL )
					return (Panel)w;

				w = w.getParentWidget();
			}

			return null;

		}
		/**
		 * Get Sheet this widget belongs to.  If the widget is a Sheet, it is returned.
		 */
		public Sheet getSheet()
		{
			if (mWidgetType == Widget.WidgetType.QGUI_TYPE_SHEET)
				return (Sheet)this;

			Widget w = mParentWidget;
			while (w != null)
			{
				if (w.getWidgetType() == Widget.WidgetType.QGUI_TYPE_SHEET)
					break;

				w = w.getParentWidget();
			}

			if (w != null)
				return (Sheet)w;
			else
				return null;
		}

		public Widget getTargetWidget(Vector2 p)
		{
			if( !mVisible || !mEnabled )
				return null;

			// iterate through child widgets..
			Widget w = null;
			// Get the widget with the highest zOrder
			int widgetZOrder = 0;
			foreach(Widget i in mChildWidgets)
			{
				Widget temp = i.getTargetWidget(p);
				if( (temp != null) && (temp.getZOrder() > widgetZOrder) )
				{
					w = temp;
					widgetZOrder = temp.getZOrder();
				}
			}

			if (w != null)
			{
				return w;
			}
			else if (isPointWithinBounds(p))
				return this;
			else
				return null;
		}

		public string getText()
		{
			return mText;
		}

		public ColourValue getTextColorTop()
		{
			return mTextTopColor;
		}

		public ColourValue getTextColorBot()
		{
			return mTextBotColor;
		}

		public Widget.WidgetType getWidgetType()
		{
			return mWidgetType;
		}

		public Window getWindow()
		{
			if (mWidgetType == Widget.WidgetType.QGUI_TYPE_WINDOW)
				return (Window)this;

			Widget w = mParentWidget;
			while (w != null)
			{
				if (w.getWidgetType() == Widget.WidgetType.QGUI_TYPE_WINDOW)
					break;

				w = w.getParentWidget();
			}

			if (w != null)
				return (Window)w;
			else
				return null;
		}

		public int getZOrder()
		{
			int zOrder = 0;
			if (mParentWidget != null)
				zOrder = mParentWidget.getZOrder();
			return zOrder + mZOrderOffset;
		}

		protected int getZOrderOffset()
		{
			return mZOrderOffset;
		}

		public void hide()
		{
			hide(true);
		}

		public void hide(bool forceHiding) // si forceHiding es true, poner no visible el widget; si es false ocultarlo pero no tocar su estado
		{
			if (forceHiding) {
				mVisible = false;
			}
			if (mBorders[(int)Widget.Border.QGUI_BORDER_TOP] != null)
				mBorders[(int)Widget.Border.QGUI_BORDER_TOP].Hide();

			if (mBorders[(int)Widget.Border.QGUI_BORDER_BOTTOM] != null)
				mBorders[(int)Widget.Border.QGUI_BORDER_BOTTOM].Hide();

			if (mBorders[(int)Widget.Border.QGUI_BORDER_LEFT] != null)
				mBorders[(int)Widget.Border.QGUI_BORDER_LEFT].Hide();

			if (mBorders[(int)Widget.Border.QGUI_BORDER_RIGHT] != null)
				mBorders[(int)Widget.Border.QGUI_BORDER_RIGHT].Hide();

			if (mOverlayElement != null)
				mOverlayElement.Hide();

			// hide children
			foreach (Widget i in mChildWidgets)
			{
				i.hide(forceHiding);
			}
		}

		protected void hideBorders()
		{
			mBordersHidden = true;

			if (mBorders[(int)Widget.Border.QGUI_BORDER_TOP] != null)
				mBorders[(int)Widget.Border.QGUI_BORDER_TOP].Hide();

			if (mBorders[(int)Widget.Border.QGUI_BORDER_BOTTOM] != null)
				mBorders[(int)Widget.Border.QGUI_BORDER_BOTTOM].Hide();

			if (mBorders[(int)Widget.Border.QGUI_BORDER_LEFT] != null)
				mBorders[(int)Widget.Border.QGUI_BORDER_LEFT].Hide();

			if (mBorders[(int)Widget.Border.QGUI_BORDER_RIGHT] != null)
				mBorders[(int)Widget.Border.QGUI_BORDER_RIGHT].Hide();
		}

		public bool isPointWithinBounds(Vector2 p)
		{
			if(!mVisible)
				return false;

			if( p.x < mPixelDimensions.x || p.x > (mPixelDimensions.x + mPixelDimensions.z) )
				return false;

			if( p.y < mPixelDimensions.y || p.y > (mPixelDimensions.y + mPixelDimensions.w) )
				return false;

			if (overTransparentPixel(p))
				return false;

			return true;
		}

		public bool isVisible()
		{
			return mVisible;
		}

		protected void move(float xVal, float yVal, QGuiMetricsMode mode)
		{
			if (!mMovingEnabled)
				return;

			Vector2 relPos = _getRelativePosition(new Vector2(xVal,yVal), mode);
			mRelativeDimensions.x += relPos.x;
			mRelativeDimensions.y += relPos.y;

			_notifyDimensionsChanged();
		}

		/**
		 * Determins if the mouse if over a transparent part of the image defining the widget.
		 * Used to determin if the mouse is *over* a widget. (non transparent parts)
		 */
		protected bool overTransparentPixel(Vector2 mousePosition)
		{
			if( mWidgetImage == null )
				return false;

			Vector2 pt = new Vector2(mousePosition.x - mPixelDimensions.x,mousePosition.y - mPixelDimensions.y);
			float relX = pt.x / mPixelDimensions.z;
			float relY = pt.y / mPixelDimensions.w;

			ColourValue c = mWidgetImage.GetColourAt((int)(relX * mWidgetImage.Width), (int)(relY * mWidgetImage.Height), 0);
			if( c.a <= 0.0 )
				return true;

			return false;
		}


		/**
		 * Notifies the parent panel of it's zOrder. If children is true,
		 * children will notify their parent panel also.
		 */
		void registerZOrder()
		{
			registerZOrder(true);
		}
		
		/**
		 * Notifies the parent panel of it's zOrder. If children is true,
		 * children will notify their parent panel also.
		 */
		void registerZOrder(bool children)
		{
			// if already registered, do nothing
			if(mZOrderRegistered)
				return;

			int zOrder = 0;
			if(mParentWidget != null)
				zOrder = mParentWidget.getZOrder();

			Panel p = getParentPanel();
			if( p != null )
			{
				p._addZOrderValue(zOrder + mZOrderOffset);
				mZOrderRegistered = true;

				if(children)
				{
					foreach(Widget w in mChildWidgets) {
						w.registerZOrder(true);
					}
				}
				return;
			}
		}

		/**
		 * Properly cleans up all child widgets.
		 */
		protected void removeAndDestroyAllChildWidgets()
		{
			//std::vector<Widget*>::iterator it;
			//for( it = mChildWidgets.begin(); it != mChildWidgets.end(); ++it )
			//    delete (*it);
			foreach (Widget i in mChildWidgets) // GDZ
			{
				i.DestroyWidget();
			}
			mChildWidgets.Clear();
		}

		/**
		 * Sets top/bottom/left/right border size in pixels.
		 */
		protected void setBorderWidth(int borderPixelHeight)
		{
			setBorderWidth(borderPixelHeight, borderPixelHeight, borderPixelHeight, borderPixelHeight);
		}

		/**
		 * Sets top/bottom, left/right border size in pixels.
		 */
		protected void setBorderWidth(int topBotBorderPixelHeight, int leftRightBorderPixelHeight)
		{
			setBorderWidth(topBotBorderPixelHeight, topBotBorderPixelHeight, leftRightBorderPixelHeight, leftRightBorderPixelHeight);
		}

		/**
		 * Sets top, bottom, left, right border size in pixels.
		 */
		protected void setBorderWidth(int topBorderPixelHeight, int botBorderPixelHeight, int leftBorderPixelHeight, int rightBorderPixelHeight)
		{
			// Update widths
			mBorderSizeInPixels[(int)Widget.Border.QGUI_BORDER_TOP] = topBorderPixelHeight;
			mBorderSizeInPixels[(int)Widget.Border.QGUI_BORDER_BOTTOM] = botBorderPixelHeight;
			mBorderSizeInPixels[(int)Widget.Border.QGUI_BORDER_LEFT] = leftBorderPixelHeight;
			mBorderSizeInPixels[(int)Widget.Border.QGUI_BORDER_RIGHT] = rightBorderPixelHeight;

			// This function repositions and resizes the borders
			_notifyDimensionsChanged();
		}

		/**
		 * Sets character height.  Widgets should override this, since text handling is widget specific,
		 * in terms of truncating and alignment.
		 */
		public virtual void setCharacterHeight(float relativeHeight)
		{
			mCharacterHeight = relativeHeight;
			_notifyTextChanged();
		}

		/**
		 * Manually set the Dimensions of the widget.
		 */
		public void setDimensions(Vector4 dimensions)
		{
			setDimensions(dimensions, QGuiMetricsMode.QGUI_GMM_RELATIVE, QGuiMetricsMode.QGUI_GMM_RELATIVE);
		}

		/**
		 * Manually set the Dimensions of the widget.
		 */
		public void setDimensions(Vector4 dimensions, QGuiMetricsMode positionMode, QGuiMetricsMode sizeMode)
		{
			mRelativeDimensions = _getRelativeDimensions(dimensions, mPositionMode, mSizeMode);
			
			_notifyDimensionsChanged();
		}

		/**
		 * Set the color used to blend in with a disabled widget.  Grey by default.
		 */
		protected void setDisabledColor(ColourValue c)
		{
			mDisabledColor = c;
		}

		/**
		 * This function specifies the widget that will be moved when the widget is *dragged*.
		 * By default, the Dragging Widget is the widget itself, but this allows for the possibility
		 * of moving a window by *dragging* the titlebar, or even the titlbar's text widget.
		 */
		public void setDraggingWidget(Widget w)
		{
			mWidgetToDrag = w;
		}

		/**
		 * Sets text font.  Widgets should override this, since text handling is widget specific,
		 * in terms of truncating and alignment.
		 */
		public virtual void setFont(string font)
		{
			mFont = font;
			_notifyTextChanged();
		}

		/**
		 * Manually set mGrabbed to true.
		 */
		protected void setGrabbed(bool grabbed)
		{
			mGrabbed = grabbed;
		}

		/**
		 * Sets only the height of the widget, relative to it's parent (or screen if parent does not exist)
		 * NOTE: all children widgets will be notified that dimensions have changed.
		 */
		public virtual void setHeight(float relativeHeight)
		{
			mRelativeDimensions.w = relativeHeight;
			_notifyDimensionsChanged();
		}

		/**
		 * If set to false, widget cannot be moved.
		 */
		protected void setMovingEnabled(bool enable)
		{
			mMovingEnabled = enable;
		}

		/**
		 * Notifies the widget of its parent.
		 */
		void setParentWidget(Widget newParent)
		{
			setParentWidget(newParent, true, true);
		}
		
		/**
		 * Notifies the widget of its parent.
		 */
		void setParentWidget(Widget newParent, bool removeFromOldParent)
		{
			setParentWidget(newParent, removeFromOldParent, true);
		}

		/**
		 * Notifies the widget of its parent.
		 */
		void setParentWidget(Widget newParent, bool removeFromOldParent, bool addToNewParent)
		{
			if( mParentWidget != null )
			{
				if( newParent == null )
				{
					if(removeFromOldParent)
						mParentWidget._removeChildWidget(this);
				}
				// If new parent is same as current parent, do nothing.
				else
					if( mParentWidget.getInstanceName().Equals(newParent.getInstanceName()) )
					return;
				else // current parent is different than new parent
				{
					if(removeFromOldParent)
						mParentWidget._removeChildWidget(this);
					if(addToNewParent)
						newParent._addChildWidget(this);
				}

				mParentWidget = newParent;
			}
			else // current parent is NULL.
			{
				// if new parent and current parent are NULL, do nothing.
				if( newParent == null )
					return;
				else
				{
					if(addToNewParent)
						newParent._addChildWidget(this);

					mParentWidget = newParent;
				}
			}
		}

		/**
		 * Manually set position of widget.
		 */
		public void setPosition(float xVal, float yVal)
		{
			setPosition(xVal, yVal, QGuiMetricsMode.QGUI_GMM_RELATIVE);
		}

		/**
		 * Manually set position of widget.
		 */
		public void setPosition(float xVal, float yVal, QGuiMetricsMode mode)
		{
			Vector2 parentPosition = new Vector2(0,0);
			Vector2 parentSize = new Vector2(1, 1);
			if(mParentWidget != null)
			{
				parentSize = mParentWidget.getSize(QGuiMetricsMode.QGUI_GMM_PIXELS);
				parentPosition = mParentWidget.getPosition(QGuiMetricsMode.QGUI_GMM_PIXELS);
			}

			switch(mode)
			{
				case QGuiMetricsMode.QGUI_GMM_ABSOLUTE:
					mRelativeDimensions.x = xVal / parentSize.x;
					mRelativeDimensions.y = yVal / parentSize.y;
					break;
				case QGuiMetricsMode.QGUI_GMM_RELATIVE:
					mRelativeDimensions.x = xVal;
					mRelativeDimensions.y = yVal;
					break;
				case QGuiMetricsMode.QGUI_GMM_PIXELS:
					float deltaX = (xVal) - parentPosition.x;
					float deltaY = (yVal) - parentPosition.y;

					mRelativeDimensions.x = (deltaX / parentSize.x);
					mRelativeDimensions.y = (deltaY / parentSize.y);
					break;
			}


			_notifyDimensionsChanged();
		}

		/**
		 * Sets text.  Widgets should override this, since text handling is widget specific,
		 * in terms of truncating and alignment.  Or in some cases where a widget does not use text,
		 * this will have no visual impact.
		 */
		public virtual void setText(string text)
		{
			mText = text;
			_notifyTextChanged();
		}

		/**
		 * Sets text color.  Widgets should override this, since text handling is widget specific,
		 * in terms of truncating and alignment.  Or in some cases where a widget does not use text,
		 * this will have no visual impact.
		 */
		public virtual void setTextColor(ColourValue color)
		{
			setTextColor(color, color);
		}

		/**
		 * Sets text color.  Widgets should override this, since text handling is widget specific,
		 * in terms of truncating and alignment.  Or in some cases where a widget does not use text,
		 * this will have no visual impact.
		 */
		public virtual void setTextColor(ColourValue topColor, ColourValue botColor)
		{
			mTextTopColor = topColor;
			mTextBotColor = botColor;
			_notifyTextChanged();
		}

		/**
		 * Sets the number of zOrders higher this widget is compared to its parent.
		 */
		public void setZOrderOffset(int offset) // GDZ
		{
			setZOrderOffset(offset, true);
		}

		/**
		 * Sets the number of zOrders higher this widget is compared to its parent.
		 */
		public void setZOrderOffset(int offset, bool updatePanelZList)
		{
			mZOrderOffset = offset;

			if (updatePanelZList)
			{
				int zOrder = 0;
				if (mParentWidget != null)
					zOrder = mParentWidget.getZOrder();

				registerZOrder(false);
			}
		}

		/**
		 * Sets mVisible to true.  Widgets should override this to implement how they handle
		 * showing.
		 */
		public virtual void show()
		{
			show(true);
		}
		
		/**
		 * Sets mVisible to true.  Widgets should override this to implement how they handle
		 * showing.
		 */
		public void show(bool forceVisible)
		{
			if (forceVisible) {
				mVisible = true;
			}
			if (mVisible) {
				if (!mBordersHidden)
				{
					if (mBorders[(int)Border.QGUI_BORDER_TOP] != null)
						mBorders[(int)Border.QGUI_BORDER_TOP].Show();
					
					if (mBorders[(int)Border.QGUI_BORDER_BOTTOM] != null)
						mBorders[(int)Border.QGUI_BORDER_BOTTOM].Show();
					
					if (mBorders[(int)Border.QGUI_BORDER_LEFT] != null)
						mBorders[(int)Border.QGUI_BORDER_LEFT].Show();
					
					if (mBorders[(int)Border.QGUI_BORDER_RIGHT] != null)
						mBorders[(int)Border.QGUI_BORDER_RIGHT].Show();
				}
				
				if (mOverlayElement != null)
					mOverlayElement.Show();

				// show children
				//std::vector<Widget*>::iterator it;
				//for( it = mChildWidgets.begin(); it != mChildWidgets.end(); ++it )
				//    (*it)->show();
				foreach (Widget i in mChildWidgets)
				{
					i.show(forceVisible);
				}
			}

		}

		/**
		 * Shows borders, if they exist.
		 */
		protected void showBorders()
		{
			mBordersHidden = false;

			if (mBorders[(int)Widget.Border.QGUI_BORDER_TOP] != null)
				mBorders[(int)Widget.Border.QGUI_BORDER_TOP].Show();

			if (mBorders[(int)Widget.Border.QGUI_BORDER_BOTTOM] != null)
				mBorders[(int)Widget.Border.QGUI_BORDER_BOTTOM].Show();

			if (mBorders[(int)Widget.Border.QGUI_BORDER_LEFT] != null)
				mBorders[(int)Widget.Border.QGUI_BORDER_LEFT].Show();

			if (mBorders[(int)Widget.Border.QGUI_BORDER_RIGHT] != null)
				mBorders[(int)Widget.Border.QGUI_BORDER_RIGHT].Show();
		}

		/**
		 * Applies the material, changing the widget's visual appearance.
		 */
		public void setMaterial(string material)
		{
			mWidgetMaterial = material;
			if( (string.Empty.Equals(mWidgetMaterial)) || (mOverlayElement == null) )
				return;

			mOverlayElement.MaterialName = mWidgetMaterial;

			MaterialManager mm = MaterialManager.Singleton;
			if( mm.ResourceExists(mWidgetMaterial) )
			{
				MaterialPtr mp = mm.GetByName(mWidgetMaterial);
				Pass p = mp.GetTechnique(0).GetPass(0);

				if( (p != null) && (p.NumTextureUnitStates >= 1) )
				{
					string name = p.GetTextureUnitState(0).TextureName;
					// exempt from transparent checks.
					if( name != "transparent.png" )
					{
						Mogre.Image i = new Mogre.Image();
						i.Load(name, ResourceGroupManager.DEFAULT_RESOURCE_GROUP_NAME);
						mWidgetImage = new Mogre.Image(i);
					}
				}
			}
		}

		/**
		 * Sets text vertical alignment.  Widgets should override this, since text handling is widget specific,
		 * in terms of truncating and alignment.  Or in some cases where a widget does not use text,
		 * this will have no visual impact.
		 */
		public void setVerticalAlignment(GuiVerticalAlignment va)
		{
			mVerticalAlignment = va;
		}

		/**
		 * Sets text horizontal alignment.  Widgets should override this, since text handling is widget specific,
		 * in terms of truncating and alignment.  Or in some cases where a widget does not use text,
		 * this will have no visual impact.
		 */
		public void setHorizontalAlignment(GuiHorizontalAlignment ha)
		{
			mHorizontalAlignment = ha;
		}

		/**
		 * Notifies and removes zOrder from parent panel.
		 * If children is true, child widgets will unregister also.
		 */
		void unregisterZOrder()
		{
			unregisterZOrder(true);
		}

		/**
		 * Notifies and removes zOrder from parent panel.
		 * If children is true, child widgets will unregister also.
		 */
		protected void unregisterZOrder(bool children)
		{
			// if already unregistered, do nothing.
			if(!mZOrderRegistered)
				return;

			int zOrder = 0;
			if(mParentWidget != null)
				zOrder = mParentWidget.getZOrder();

			Panel p = getParentPanel();
			if( p != null )
			{
				p._removeZOrderValue(zOrder + mZOrderOffset);
				mZOrderRegistered = false;

				if(children)
				{
					foreach(Widget w in mChildWidgets) {
						w.unregisterZOrder(true);
					}
				}
				return;
			}
		}

		/**
		 * Returns true if parent panel is aware of this child widgets zOrder, false otherwise.
		 */
		bool zOrderRegistered()
		{
			return mZOrderRegistered;
		}

		/**
		 * Implemented by GDZ
		 */
		//public bool IsChild(Widget w)
		//{
		//    return w != null && w.getParentWidget() != null && w.getParentWidget().getInstanceName().Equals(getInstanceName()) || (w != null && IsChild(w.getParentWidget()));
		//}

		#region EventHandlers

		public void Activate(EventArgs e)
		{
			if (!mEnabled)
				return;

			if (OnActivate != null)
				OnActivate(this, e);
		}

		public void Deactivate(EventArgs e)
		{
			if (!mEnabled)
				return;

			mGrabbed = false;

			if (OnDeactivate != null)
				OnDeactivate(this, e);
		}

		public void Dragged(WidgetEventArgs e)
		{
			if (!mEnabled)
				return;

			if (OnDragged != null)
				OnDragged(this, e);
		}

		internal void MouseEnters(MouseEventArgs e)
		{
			if (!mEnabled)
				return;

			if (OnMouseEnter != null)
				OnMouseEnter(this, e);
			else
				if (getParentWidget() != null) // if event not handled, pass it up to parent widget
				getParentWidget().MouseEnters(e);
		}

		internal void MouseLeaves(MouseEventArgs e)
		{
			if (!mEnabled)
				return;

			if (OnMouseLeaves != null)
				OnMouseLeaves(this, e);
			else
				if (getParentWidget() != null) // if event not handled, pass it up to parent widget
				getParentWidget().MouseLeaves(e);
		}

		internal void MouseMoved(MouseEventArgs e)
		{
			if (!mEnabled)
				return;

			if (OnMouseMoved != null)
				OnMouseMoved(this, e);
			else
				if (getParentWidget() != null) // if event not handled, pass it up to parent widget
				getParentWidget().MouseMoved(e);
		}

		internal void MouseWheel(MouseEventArgs e)
		{
			if (!mEnabled)
				return;

			if (OnMouseWheel != null)
				OnMouseWheel(this, e);
			else
				if (getParentWidget() != null) // if event not handled, pass it up to parent widget
				getParentWidget().MouseWheel(e);
		}

		internal void MouseButtonUp(MouseEventArgs e)
		{
			if (!mEnabled)
				return;

			mGrabbed = false;

			if (OnMouseButtonUp != null)
				OnMouseButtonUp(this, e);
			else
				if (getParentWidget() != null) // if event not handled, pass it up to parent widget
				getParentWidget().MouseButtonUp(e);
		}

		internal void MouseButtonDown(MouseEventArgs e)
		{
			if (!mEnabled)
				return;
			if ((e.button == MOIS.MouseButtonID.MB_Left) && (e.widget.getInstanceName().Equals(mInstanceName)))
			{
				mGrabbed = true;
			}

			if (OnMouseButtonDown != null)
				OnMouseButtonDown(this, e);
			else
				if (getParentWidget() != null) // if event not handled, pass it up to parent widget
				getParentWidget().MouseButtonDown(e);
		}

		internal virtual void MouseClicked(MouseEventArgs e)
		{
			if (!mEnabled)
				return;

			if (OnMouseClicked != null)
				OnMouseClicked(this, e);
			else
				if (getParentWidget() != null) // if event not handled, pass it up to parent widget
				getParentWidget().MouseClicked(e);
		}

		internal void KeyDown(KeyEventArgs e)
		{
			if (!mEnabled)
				return;

			if (OnKeyDown != null)
				OnKeyDown(this, e);
			else
				if (getParentWidget() != null) // if event not handled, pass it up to parent widget
				getParentWidget().KeyDown(e);
		}

		internal void KeyUp(KeyEventArgs e)
		{
			if (!mEnabled)
				return;

			if (OnKeyUp != null)
				OnKeyUp(this, e);
			else
				if (getParentWidget() != null) // if event not handled, pass it up to parent widget
				getParentWidget().KeyUp(e);
		}

		internal void Character(KeyEventArgs e)
		{
			if (!mEnabled)
				return;

			if (OnCharacter != null)
				OnCharacter(this, e);
			else
				if (getParentWidget() != null) // if event not handled, pass it up to parent widget
				getParentWidget().Character(e);
		}

		internal void TimeElapsed(float time)
		{
			if (!mEnabled)
				return;

			if (OnTimeElapsed != null)
				OnTimeElapsed(this, new TimeEventArgs(time));
		}
		#endregion


	}
}
