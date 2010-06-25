using System;
using System.Collections.Generic;
using System.Text;
using Mogre;

namespace MQuickGUI
{
    public class MenuList : Button
    {
        protected List mList;

	    public MenuList(string name, Vector4 dimensions, QGuiMetricsMode positionMode, QGuiMetricsMode sizeMode, string material, OverlayContainer overlayContainer, Widget ParentWidget) : base(name,dimensions,positionMode, sizeMode,material+".button",overlayContainer,ParentWidget)
	    {
		    mWidgetType = Widget.WidgetType.QGUI_TYPE_MENULIST;
		    setCharacterHeight(0.8f);

		    // create list
		    mList = new List(mInstanceName+".List",new Vector3(0f,1.0f,1.0f), QGuiMetricsMode.QGUI_GMM_RELATIVE, QGuiMetricsMode.QGUI_GMM_RELATIVE, material+".list",getSheet().getMenuContainer(),this);
		    mList.setCharacterHeight(mCharacterHeight);
		    mList.setFont(mFont);
		    mList.setTextColor(mTextTopColor,mTextBotColor);
		    mList.hide();
		    // give the list the same zOrder as the button representing this menulist.
		    mList.setZOrderOffset(mZOrderOffset,false);
		    _addChildWidget(mList);

            OnDeactivate += new DeactivateEventHandler(MenuList_OnDeactivate);
	    }

        void MenuList_OnDeactivate(object source, EventArgs e)
        {
        	if(!mEnabled)
                return;

		    // If the Mouse has clicked on any of the menu's List or ListItems, the widget should not *deactivate*.
		    // As for hiding the list, this will be taken care of in the onMouseButtonDown handler.  The list needs
		    // to remain visible so that ListItem picking works correctly. (If list is hidden, you can't click the ListItem..)
		    if(getTargetWidget(MouseCursor.Singleton.getPixelPosition()) != null)
                return;
        }

	    ~MenuList()
	    {
		    removeAndDestroyAllChildWidgets();
	    }

	    public ListItem addListItem(string text)
	    {
		    return mList.addListItem(text);
	    }

	    public ListItem addListItem(string name, string text)
	    {
		    return mList.addListItem(name,text);
	    }

        void alignListItemText(GuiHorizontalAlignment ha, GuiVerticalAlignment va)
	    {
		    mList.alignListItemText(ha,va);
	    }

	    void clearList()
	    {
		    mList.clearList();
	    }

	    ListItem getListItem(int index)
	    {
		    return mList.getListItem(index);
	    }

	    ListItem getListItem(string name)
	    {
		    return mList.getListItem(name);
	    }

	    int getNumberOfListItems()
	    {
		    return mList.getNumberOfListItems();
	    }

	    void hideHighlight()
	    {
		    mList.hideHighlight();
	    }

	    void highlightListItem(ListItem i)
	    {
		    mList.highlightListItem(i);
	    }

	    public void hideList()
	    {
		    mList.hide();
	    }

	    public bool isListVisible()
	    {
		    return mList.isVisible();
	    }

	    public bool isMouseOverButton()
	    {
		    return isPointWithinBounds(MouseCursor.Singleton.getPixelPosition());
	    }

        public void removeListItem(int index)
	    {
		    mList.removeListItem(index);
	    }

        public void setListCharacterHeight(float relativeHeight)
	    {
		    mList.setCharacterHeight(relativeHeight);
	    }

        public void setListHighlightMaterial(string material)
	    {
		    mList.setHighlightMaterial(material);
	    }

	    public void setListWidth(float relativeWidth)
	    {
		    // make it seem like the width is relative to the menu, and not the menu list
            float numPixels = relativeWidth * mParentWidget.getSize(QGuiMetricsMode.QGUI_GMM_PIXELS).x; 
		    mList.setWidth(numPixels / mPixelDimensions.z);
	    }

	    public void showList()
	    {
		    mList.show();
	    }
    }
}
