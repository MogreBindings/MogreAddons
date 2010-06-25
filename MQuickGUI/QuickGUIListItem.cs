using System;
using System.Collections.Generic;
using System.Text;
using Mogre;

namespace MQuickGUI
{
    public class ListItem : Label
    {
        protected Image mImage;
		protected NStateButton mButton;

	    public ListItem(string name, Vector4 dimensions, QGuiMetricsMode positionMode, QGuiMetricsMode sizeMode, OverlayContainer overlayContainer, Widget ParentWidget) : base(name,dimensions,positionMode, sizeMode, "",overlayContainer,ParentWidget)
	    {
		    mImage = null;
		    mButton = null;
		    mWidgetType = Widget.WidgetType.QGUI_TYPE_LISTITEM;
		    setCharacterHeight(1.0f);

            OnDeactivate += new DeactivateEventHandler(ListItem_OnDeactivate);
            OnMouseEnter += new MouseEnterEventHandler(ListItem_OnMouseEnter);
            OnMouseButtonUp += new MouseButtonUpEventHandler(ListItem_OnMouseButtonUp);
            OnMouseLeaves += new MouseLeavesEventHandler(ListItem_OnMouseLeaves);
	    }

        void ListItem_OnDeactivate(object source, EventArgs e)
        {
            if(!mEnabled)
                return;

		    if(getTargetWidget(MouseCursor.Singleton.getPixelPosition()) != null)
                return;

            if (mParentWidget != null)
                mParentWidget.Deactivate(e);
        }

        void ListItem_OnMouseLeaves(object source, MouseEventArgs e)
        {
            e.widget = this;
            List parentList = (List)mParentWidget;

            parentList.hideHighlight();
        }

        void ListItem_OnMouseButtonUp(object source, MouseEventArgs e)
        {

            //TODO Revisar que pasa con esto
            //onMouseButtonUp(e);
            // When the widget or any of its child widgets receive the mouse up event, 
            // hide the menu containing this list item.  This is performed in Menu::onMouseButtonUp
            e.handled = false;
            //if (mParentWidget != null)
            //    mParentWidget.MouseButtonUp(e);
        }

        void ListItem_OnMouseEnter(object source, MouseEventArgs e)
        {
            e.widget = this;
            List parentList = (List)mParentWidget;

            parentList.highlightListItem(this);
        }
    	
	    ~ListItem()
	    {
		    removeAndDestroyAllChildWidgets();

		    mImage = null;
		    mButton = null;
	    }

	    public NStateButton addNStateButton(Vector4 dimensions, QGuiMetricsMode positionMode, QGuiMetricsMode sizeMode)
	    {
		    if(mButton != null)
                return null;

		    mButton = new NStateButton(mInstanceName+".NStateButton",dimensions, positionMode, sizeMode, mChildrenContainer,this);
            //Reemplzado por Manejo de Eventos //GDZ
            //mButton->addEventHandler(Widget::QGUI_EVENT_DEACTIVATED,&ListItem::evtHndlr_hideMenus,dynamic_cast<ListItem*>(this));
		    //mButton->addEventHandler(Widget::QGUI_EVENT_MOUSE_BUTTON_UP,&ListItem::evtHndlr_hideMenus,dynamic_cast<ListItem*>(this));
            mButton.OnDeactivate += new DeactivateEventHandler(mButton_OnDeactivate);
            mButton.OnMouseButtonUp += new MouseButtonUpEventHandler(mButton_OnMouseButtonUp);
            mButton.setZOrderOffset(1, false);
		    if(!mVisible)
                mButton.hide();
		    _addChildWidget(mButton);

		    return mButton;
	    }

        void mButton_OnMouseButtonUp(object source, MouseEventArgs e)
        {
            Deactivate(e);
        }

        void mButton_OnDeactivate(object source, EventArgs e)
        {
            Deactivate(e);
        }

	    public Image addImage(Vector4 dimensions, string material)
	    {
		    if(mImage != null)
                return null;

		    mImage = new Image(mInstanceName+".Image",dimensions,QGuiMetricsMode.QGUI_GMM_RELATIVE, QGuiMetricsMode.QGUI_GMM_RELATIVE, material,mChildrenContainer,this);
            mImage.OnDeactivate += new DeactivateEventHandler(mImage_OnDeactivate);
            //Reemplazado por Manejo de Eventos //GDZ
		    //mImage->addEventHandler(Widget::QGUI_EVENT_DEACTIVATED,&ListItem::evtHndlr_hideMenus,dynamic_cast<ListItem*>(this));
		    mImage.setZOrderOffset(1,false);
		    if(!mVisible)
                mImage.hide();
		    _addChildWidget(mImage);

		    return mImage;
	    }

        void mImage_OnDeactivate(object source, EventArgs e)
        {
            Deactivate(e);
        }

	    NStateButton getNStateButton()
	    {
		    return mButton;
	    }

	    Image getImage()
	    {
		    return mImage;
	    }

        //new bool onMouseButtonUp(MouseEventArgs e)
        //{
        //    bool retVal = onMouseButtonUp(e);
        //    // When the widget or any of its child widgets receive the mouse up event, 
        //    // hide the menu containing this list item.  This is performed in Menu::onMouseButtonUp
        //    e.handled = false;
        //    if(mParentWidget != null)
        //        mParentWidget.onMouseButtonUp(e);
        //    return retVal;
        //}

        //new bool onMouseEnters(MouseEventArgs e)
        //{
        //    e.widget = this;
        //    List parentList = (List)mParentWidget;

        //    parentList.highlightListItem(this);

        //    return onMouseEnters(e);
        //}

        //new bool onMouseLeaves(MouseEventArgs e)
        //{
        //    e.widget = this;
        //    List parentList = (List)mParentWidget;

        //    parentList.hideHighlight();

        //    return onMouseLeaves(e);
        //}

	    void removeNStateButton()
	    {
		    if(mButton == null)
                return;

		    _removeChildWidget(mButton);
		    mButton = null;
	    }

	    void removeImage()
	    {
		    if(mImage == null)
                return;

		    _removeChildWidget(mImage);
		    mImage = null;
	    }

    }
}
