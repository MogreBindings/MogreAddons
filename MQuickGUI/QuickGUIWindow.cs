using System;
using System.Collections.Generic;
using System.Text;
using Mogre;

namespace MQuickGUI
{
    public class Window : Panel
    {
		protected Overlay mOverlay;
		protected bool mTitleBarHidden;
		protected OverlayContainer mTitleBarContainer;
		protected TitleBar mTitleBar;

	    public Window(string name, Vector4 dimensions, QGuiMetricsMode positionMode, QGuiMetricsMode sizeMode, string material, Widget parentWidget) : base(name,dimensions, positionMode, sizeMode, material, null, parentWidget)
	    {
            mTitleBar = null;
		    mTitleBarHidden = false;


		    mWidgetType = Widget.WidgetType.QGUI_TYPE_WINDOW;
		    mOverlay = OverlayManager.Singleton.Create(mInstanceName+".Overlay");
		    mOverlay.ZOrder = 0;
		    mOverlay.Show();
		    mZOrderValues.Add(0);

		    // mChildrenContainer already created in Widget constructor
		    mTitleBarContainer = createOverlayContainer(mInstanceName+".TitleBarContainer","");
    		
		    mOverlay.Add2D(mOverlayContainer);
		    mChildrenContainer.AddChildImpl(mTitleBarContainer);
    		
		    mOverlayContainer.Show();
		    mChildrenContainer.Show();
		    mTitleBarContainer.Show();

		    // Create TitleBar - tradition titlebar dimensions: across the top of the window
		    Vector4 defaultTitleBarDimensions = new Vector4(0f,0f,1f,0.05f / getSize(QGuiMetricsMode.QGUI_GMM_ABSOLUTE).y);
		    mTitleBar = new TitleBar(mInstanceName+".Titlebar",defaultTitleBarDimensions, QGuiMetricsMode.QGUI_GMM_RELATIVE, QGuiMetricsMode.QGUI_GMM_RELATIVE, mWidgetMaterial+".titlebar",mTitleBarContainer,this);
		    mTitleBar.enableDragging(true);
		    mTitleBar.setDraggingWidget(this);
		    mTitleBar.getTextWidget().enableDragging(true);
		    mTitleBar.getTextWidget().setDraggingWidget(this);
		    mTitleBar.setZOrderOffset(1);
		    _addChildWidget(mTitleBar);

            // Now that mOverlayContainer has been created (via _init() function) we can create the borders
		    _createBorders();

            OnActivate += new ActivateEventHandler(Window_OnActivate);
	    }

        void Window_OnActivate(object source, EventArgs e)
        {
            getSheet().setActiveWindow(this);
        }

	    public override void DestroyWidget()
	    {
	    	_destroyBorders();
		    OverlayManager om = OverlayManager.Singleton;

		    // Delete all child widgets before deleting the window widget
		    removeAndDestroyAllChildWidgets();
    		
            //// destroy background overlay element
            mOverlayContainer.RemoveChild(mOverlayElement.Name);
            om.DestroyOverlayElement(mOverlayElement);
            mOverlayElement = null;

            // destroy TitleBar container
            mChildrenContainer.RemoveChild(mTitleBarContainer.Name);
            om.DestroyOverlayElement(mTitleBarContainer);
            mTitleBarContainer = null;
    	
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
//            base.DestroyWidget();
			
			unregisterZOrder(false);
			mParentWidget = null;

			GUIManager.Singleton.removeWidgetName(mInstanceName);
	    }

        public TitleBar getTitleBar()
	    {
		    return mTitleBar;
	    }

        protected void hideCloseButton()
	    {
		    if(mTitleBar!=null)
                mTitleBar.hideCloseButton();
	    }

	    public void hideTitlebar()
	    {
		    if(mTitleBar!=null) 
		    {
			    mTitleBar.hide();
			    mTitleBarHidden = true;
		    }
	    }

	    public override void setText(string text)
	    {
		    mText = text;
		    mTitleBar.setFont(mFont);
		    mTitleBar.setCharacterHeight(mCharacterHeight);
		    mTitleBar.setTextColor(mTextTopColor,mTextBotColor);
		    mTitleBar.setText(mText);
	    }

        public override void setTextColor(ColourValue topColor, ColourValue botColor)
	    {
		    mTextTopColor = topColor;
		    mTextBotColor = botColor;
		    mTitleBar.setFont(mFont);
		    mTitleBar.setCharacterHeight(mCharacterHeight);
		    mTitleBar.setTextColor(mTextTopColor,mTextBotColor);
		    mTitleBar.setText(mText);
	    }

	    public void setTitleBarHeight(float height)
	    {
		    if( height > mOverlayElement.Height )
                height = mOverlayElement.Height;

		    if(mTitleBar != null)
                mTitleBar.setHeight(height);
	    }

	    public void setZOrder(int zOrder)
	    {
		    // offset relative to parent sheet, which is using an overlay of zOrder 0.
		    int previousZOrderOffset = mZOrderOffset;
		    mZOrderOffset = zOrder;
    		
		    int difference = mZOrderOffset - previousZOrderOffset;
		    mOverlay.ZOrder = (ushort) mZOrderOffset;

		    // Update zOrder Value List
            //std::list<int>::iterator it;
            //for( it = mZOrderValues.begin(); it != mZOrderValues.end(); ++it )
            for (int j = 0; j < mZOrderValues.Count; j++)
            {
                mZOrderValues[j] += difference;
            }
	    }

	    public override void show()
	    {
		    base.show();

            if (mTitleBarHidden)
                hideTitlebar(); 
	    }

        public void showCloseButton()
	    {
		    if(mTitleBar != null)
                    mTitleBar.showCloseButton();
	    }

        public void showTitlebar()
	    {
            if (mTitleBar != null) 
		    {
			    mTitleBar.show();
			    mTitleBarHidden = false;
		    }
	    }
    }
}
