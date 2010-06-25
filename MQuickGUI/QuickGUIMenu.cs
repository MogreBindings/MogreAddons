using System;
using System.Collections.Generic;
using System.Text;
using Mogre;
using MOIS;

namespace MQuickGUI
{
    /** Represents a Menu.
        @remarks
        Menus are empty be default.  They do not become useful
        until adding MenuStrip widgets, which contain ListItem widgets.
        @note
        Menus must be created by the Window widget.
        @note
        Menus do not contain any text.
    */
    public class Menu : Label
    {
        protected List<MenuList> mMenuLists;
        
		// If user clicks on a menu, it drops down and sets this variable to true;
		// Mousing over other menu anchor buttons will switch to their menus. (Windows XP menu functionality)
		protected bool mShowMenus;

		protected int mMenuListCounter;

        /** Constructor
            @param
                name The name to be given to the widget (must be unique).
            @param
                dimensions The x Position, y Position, width, and height of the widget.
            @param
                positionMode The GuiMetricsMode for the values given for the position. (absolute/relative/pixel)
            @param
                sizeMode The GuiMetricsMode for the values given for the size. (absolute/relative/pixel)
            @param
                material Ogre material defining the widget image.
            @param
                overlayContainer associates the internal OverlayElement with a specified zOrder.
            @param
                ParentWidget parent widget which created this widget.
        */
        public Menu(string name, Vector4 dimensions, QGuiMetricsMode positionMode, QGuiMetricsMode sizeMode, string material, OverlayContainer overlayContainer, Widget ParentWidget)
            : base(name, dimensions, positionMode, sizeMode, material, overlayContainer, ParentWidget)
        {
	        mMenuListCounter = 0;
	        mShowMenus = false;
	        mWidgetType = Widget.WidgetType.QGUI_TYPE_MENU;
	        mMenuLists = new List<MenuList>();

            OnDeactivate += new DeactivateEventHandler(Menu_OnDeactivate);
            OnMouseButtonUp += new MouseButtonUpEventHandler(Menu_OnMouseButtonUp);
            OnMouseMoved += new MouseMovedEventHandler(Menu_OnMouseMoved);

        }

        ~Menu()
        {
	        clearAllMenuLists();
        }

        /**
        * Adds a menu list.
        */
        MenuList addMenuList(string name, string text, float relXPos, float relXSize, string material)
        {
	        MenuList newMenuList = new MenuList(name, new Vector4(relXPos,0,relXSize,1), QGuiMetricsMode.QGUI_GMM_RELATIVE, QGuiMetricsMode.QGUI_GMM_RELATIVE, material, mChildrenContainer, this);
	        newMenuList.setText(text);
            newMenuList.OnMouseButtonDown += new MouseButtonDownEventHandler(newMenuList_OnMouseButtonDown);
            newMenuList.OnDeactivate += new DeactivateEventHandler(newMenuList_OnDeactivate);
            newMenuList.setZOrderOffset(1);
	        mMenuLists.Add(newMenuList);
	        _addChildWidget(newMenuList);

	        return newMenuList;
        }

        /**
        * Adds a menu list.  Name is generated.
        */
        public MenuList addMenuList(string text, float relXPos, float relXSize, string material)
        {
	        string name = mInstanceName+".MenuList" + mMenuListCounter;
	        ++mMenuListCounter;

	        return addMenuList(name,text,relXPos,relXSize,material);
        }

        /**
        * Adds a menu list.  Name is generated. Default Material applied.
        */
        public MenuList addMenuList(string text, float relXPos, float relXSize)
        {
	        string name = mInstanceName + ".MenuList" + mMenuListCounter;
	        ++mMenuListCounter;

	        string material = getSheet().getDefaultSkin() + ".menulist";

	        return addMenuList(name,text,relXPos,relXSize,material);
        }

        /**
        * Removes and Destroys all Child Lists, and their corresponding Anchor Button.
        */
        void clearAllMenuLists()
        {
	        removeAndDestroyAllChildWidgets();
	        mMenuLists.Clear();
        }

        /**
        * Gets a List from the Menu.  No exception is thrown
        * if the index is out of bounds.
        */
        MenuList getMenuList(int index)
        {
	        if( (mMenuLists.Count - 1) < index )
                return null;
	        return mMenuLists[index];
        }

        MenuList getMenuList(string name)
        {
            foreach(MenuList i in mMenuLists) {
                if (i.getInstanceName().Equals(name))
                    return i;
            }

	        return null;
        }

        /**
        * Hides All Child Menus.
        */
        void hideMenus()
        {
            foreach(MenuList i in mMenuLists) {
                i.hideList();
            }

	        mShowMenus = false;
        }

        /**
        * Default Event Handler called when widget is deactivated.
        */
        void Menu_OnDeactivate(object source, EventArgs e)
        {
            if (!mEnabled)
                return;

            // If the Mouse has clicked on any of the menu's List or ListItems, the widget should not *deactivate*.
            // As for hiding the list, this will be taken care of in the onMouseButtonDown handler.  The list needs
            // to remain visible so that ListItem picking works correctly. (If list is hidden, you can't click the ListItem..)
            if (getTargetWidget(MouseCursor.Singleton.getPixelPosition()) != null)
                return;

            hideMenus();
        }

        /**
        * Default Handler for the QGUI_EVENT_MOUSE_BUTTON_UP event.
        */
        void Menu_OnMouseButtonUp(object source, MouseEventArgs e)
        {
            if (e.button == MouseButtonID.MB_Left)
            {
                foreach (MenuList i in mMenuLists)
                {
                    if (i.isMouseOverButton())
                        break;
                    else if (i.getTargetWidget(MouseCursor.Singleton.getPixelPosition()) != null)
                    {
                        i.hideList();
                        mShowMenus = false;
                        break;
                    }
                }
            }
        }

        /**
        * Default Handler for the QGUI_EVENT_MOUSE_MOVE event. 
        */
        void Menu_OnMouseMoved(object source, MouseEventArgs e)
        {
            if (!mEnabled)
                return;

            if (mShowMenus)
            {
                // get index of button clicked
                int index = 0;
                bool found = false;
                foreach (MenuList i in mMenuLists)
                {
                    if (i.isPointWithinBounds(MouseCursor.Singleton.getPixelPosition()))
                    {
                        found = true;
                        index = mMenuLists.IndexOf(i);
                        break;
                    }

                }

                if (found)
                {
                    if (!mMenuLists[index].isListVisible())
                    {
                        // Hide all menus and show just the desired one
                        hideMenus();
                        mMenuLists[index].showList();
                        mShowMenus = true;
                    }
                }
            }

        }

        /**
        * Event Handler used to hide all child Lists, when a ListItem is clicked.
        */
        void newMenuList_OnDeactivate(object source, EventArgs e)
        {
            // If the Mouse has clicked on any of the menu's List or ListItems, the widget should not *deactivate*.
            // As for hiding the list, this will be taken care of in the onMouseButtonDown handler.  The list needs
            // to remain visible so that ListItem picking works correctly. (If list is hidden, you can't click the ListItem..)
            if (getTargetWidget(MouseCursor.Singleton.getPixelPosition()) != null)
                return;

            hideMenus();

        }

        /**
        * Called whenever a child list creates a child list item.
        * Used to add MouseEnter and MouseLeave event handlers to List Item, 
        * for selection highlighting.
        */
        void newMenuList_OnMouseButtonDown(object source, MouseEventArgs e)
        {

            if (e.button == MouseButtonID.MB_Left)
            {
                // get index of button clicked
                bool buttonClicked = false;
                int index = 0;
                foreach (MenuList i in mMenuLists)
                {
                    if (i.isMouseOverButton())
                    {
                        buttonClicked = true;
                        break;
                    }

                    ++index;
                }
                if (!buttonClicked)
                {
                    return;
                }

                // get target list corresponding to the clicked anchor (button) and toggle visibility
                if (mMenuLists[index].isListVisible())
                {
                    mMenuLists[index].hideList();
                    mShowMenus = false;
                }
                else
                {
                    // Hide all menus and show just the desired one
                    hideMenus();
                    mMenuLists[index].showList();
                    mShowMenus = true;
                }
            }
        }
    }
}
