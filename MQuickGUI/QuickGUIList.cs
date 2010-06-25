using System;
using System.Collections.Generic;
using System.Text;
using Mogre;

namespace MQuickGUI
{
    public class List : Label
    {
        protected int mAutoNameListItemCount;
		protected int mAutoNameListCount;
		protected List<ListItem> mItems;

        protected GuiHorizontalAlignment mListItemHorizontalAlignment;
        protected GuiVerticalAlignment mListItemVerticalAlignment;

		protected OverlayContainer mHighlightContainer;
		protected PanelOverlayElement mHighlightPanel;
		protected string mHighlightMaterial;

		protected List<List> mChildLists;

		// This value is an absolute dimension
		protected float mDefaultListItemHeight;

		public List(string name, Vector3 dimensions, Label itemTemplate, QGuiMetricsMode positionMode, QGuiMetricsMode sizeMode, string material, OverlayContainer overlayContainer, Widget ParentWidget) : base(name,new Vector4(dimensions.x,dimensions.y,dimensions.z,0), positionMode, sizeMode, material,overlayContainer,ParentWidget)
	    {
		    mAutoNameListItemCount = 0;
		    mAutoNameListCount = 0;
		    mListItemHorizontalAlignment = GuiHorizontalAlignment.GHA_CENTER;
            mListItemVerticalAlignment = GuiVerticalAlignment.GVA_CENTER;

            mWidgetType = Widget.WidgetType.QGUI_TYPE_LIST;

            mDefaultListItemHeight = itemTemplate.getDimensions(QGuiMetricsMode.QGUI_GMM_RELATIVE, QGuiMetricsMode.QGUI_GMM_RELATIVE).w;
            
		    mHighlightMaterial = getSheet().getDefaultSkin() + ".list.highlight";

		    // create highlight container for the list
		    mHighlightContainer = createOverlayContainer(mInstanceName+".HighlightContainer","");
		    mChildrenContainer.AddChildImpl(mHighlightContainer);

		    mHighlightPanel = createPanelOverlayElement(mInstanceName+".HighlightPanel",mPixelDimensions,mHighlightMaterial);
		    mHighlightContainer.AddChild(mHighlightPanel);
		    mHighlightPanel.Show();

		    mItems = new List<ListItem>();
		    mChildLists = new List<List>();
            OnDeactivate += new DeactivateEventHandler(List_OnDeactivate);
            
            
            setCharacterHeight(itemTemplate.getCharacterHeight());
            setFont(itemTemplate.getFont());
            setTextColor(itemTemplate.getTextColorTop());

	    }
			
		
	    public List(string name, Vector3 dimensions, QGuiMetricsMode positionMode, QGuiMetricsMode sizeMode, string material, OverlayContainer overlayContainer, Widget ParentWidget) : base(name,new Vector4(dimensions.x,dimensions.y,dimensions.z,0), positionMode, sizeMode, material,overlayContainer,ParentWidget)
	    {
		    mAutoNameListItemCount = 0;
		    mAutoNameListCount = 0;
		    mListItemHorizontalAlignment = GuiHorizontalAlignment.GHA_CENTER;
            mListItemVerticalAlignment = GuiVerticalAlignment.GVA_CENTER;

            mWidgetType = Widget.WidgetType.QGUI_TYPE_LIST;

		    mDefaultListItemHeight = mParentWidget.getSize(QGuiMetricsMode.QGUI_GMM_ABSOLUTE).y;
		    mHighlightMaterial = getSheet().getDefaultSkin() + ".list.highlight";

		    // create highlight container for the list
		    mHighlightContainer = createOverlayContainer(mInstanceName+".HighlightContainer","");
		    mChildrenContainer.AddChildImpl(mHighlightContainer);

		    mHighlightPanel = createPanelOverlayElement(mInstanceName+".HighlightPanel",mPixelDimensions,mHighlightMaterial);
		    mHighlightContainer.AddChild(mHighlightPanel);
		    mHighlightPanel.Show();

		    mItems = new List<ListItem>();
		    mChildLists = new List<List>();
            OnDeactivate += new DeactivateEventHandler(List_OnDeactivate);
	    }

        void List_OnDeactivate(object source, EventArgs e)
        {
       		if(!mEnabled)
                return;

	        if(getTargetWidget(MouseCursor.Singleton.getPixelPosition()) != null)
                return;

// TODO: Revisar si esto realmente no va; causa un Stack Overflow, pero sin esto no se advierte un mal comportamiento     
//	        if(mParentWidget != null)
//                mParentWidget.Deactivate(e);
        }

	    public override void DestroyWidget()
	    {
		    removeAndDestroyAllChildWidgets();
		    mItems.Clear();
		    mChildLists.Clear();


            // TODO: Ver si esto está bien
            OverlayManager om = OverlayManager.Singleton;

            mHighlightContainer.RemoveChild(mHighlightPanel.Name);
            om.DestroyOverlayElement(mHighlightPanel);
            mHighlightPanel = null;

            mChildrenContainer.RemoveChild(mHighlightContainer.Name);
            om.DestroyOverlayElement(mHighlightContainer);
            mHighlightContainer = null;
            base.DestroyWidget();
	    }

	    public ListItem addListItem(string text)
	    {
		    string name = mInstanceName + ".ListItem" + mAutoNameListItemCount;
		    ++mAutoNameListItemCount;

		    return addListItem(name,text);
	    }

	    public ListItem addListItem(string name, string text)
	    {
		    Vector4 OriginalAbsoluteDimensions = mAbsoluteDimensions;

		    // Calculate the current absolute height of the list
		    float currentListHeight = 0.0f;
            foreach(ListItem i in mItems)
		    {
			    currentListHeight = currentListHeight + i.getSize(QGuiMetricsMode.QGUI_GMM_ABSOLUTE).y;
		    }

		    // Grow the list to accept the new incoming list item
		    float calculatedListHeight = currentListHeight + mDefaultListItemHeight;
		    // funny, this should almost always be more than 1.0, which defies relative coordinates
		    // IMPORTANT! Do not call _notifyDimensionsChanged, otherwise all child ListItems will have their dimensions auto adjusted.
		    // We need to keep them as is, and later use the unchanged values to calculate new values, as the list height has changed.
		    // Essentially, we want to maintain the ListItems' height while adjusting the List's height.
		    mRelativeDimensions.w = calculatedListHeight / (mParentWidget.getSize(QGuiMetricsMode.QGUI_GMM_ABSOLUTE).y);
		    _updateDimensions(mRelativeDimensions);
    		
		    // Shrink the previous list Items back to the correct size
		    //for( it = mItems.begin(); it != mItems.end(); ++it )
            foreach(ListItem i in mItems)
		    {
                Vector2 pos = i.getPosition(QGuiMetricsMode.QGUI_GMM_ABSOLUTE);
                Vector2 size = i.getSize(QGuiMetricsMode.QGUI_GMM_ABSOLUTE);

			    i.setDimensions(
				    new Vector4(
					    (pos.x - mAbsoluteDimensions.x) / mAbsoluteDimensions.z,
					    (pos.y - mAbsoluteDimensions.y) / mAbsoluteDimensions.w,
					    size.x / mAbsoluteDimensions.z,
					    size.y / mAbsoluteDimensions.w
					    )
				    );
		    }
    		
		    // Add the List Item
		    ListItem newListItem = new ListItem(name,new Vector4(0f,currentListHeight / calculatedListHeight,1.0f,mDefaultListItemHeight / mAbsoluteDimensions.w),QGuiMetricsMode.QGUI_GMM_RELATIVE, QGuiMetricsMode.QGUI_GMM_RELATIVE, mChildrenContainer,this);
		    newListItem.setCharacterHeight(mCharacterHeight);
		    newListItem.setFont(mFont);
		    newListItem.setTextColor(mTextTopColor,mTextBotColor);
		    newListItem.setText(text);
		    newListItem.alignText(mListItemHorizontalAlignment,mListItemVerticalAlignment);
		    newListItem.setZOrderOffset(1,false);
		    mItems.Add(newListItem);
		    _addChildWidget(newListItem);
		    if(!mVisible)
                newListItem.hide();

		    // Important!  Even though we have set the dimensions, we need to update the overlay elements (of the list, specifically)
		    _notifyDimensionsChanged();
    		
		    return newListItem;
	    }

        public void alignListItemText(GuiHorizontalAlignment ha, GuiVerticalAlignment va)
	    {
		    mListItemHorizontalAlignment = ha;
		    mListItemVerticalAlignment = va;

		    //std::vector<ListItem*>::iterator it;
		    //for( it = mItems.begin(); it != mItems.end(); ++it )
            foreach(ListItem i in mItems)
		    {
			    i.alignText(mListItemHorizontalAlignment,mListItemVerticalAlignment);
		    }
	    }

	    public void clearList()
	    {
		    //std::vector<ListItem*>::iterator it;
		    //for( it = mItems.begin(); it != mItems.end(); ++it )
            foreach(ListItem i in mItems)
		    {
			    _removeChildWidget(i);
			    //delete *it; // GDZ
		    }
		    mItems.Clear();

		    mRelativeDimensions.w = 0;
		    _updateDimensions(mRelativeDimensions);
	    }

	    public ListItem getListItem(int index)
	    {
		    if( (mItems.Count - 1) < index )
                return null;
		    return mItems[index];
	    }

	    public ListItem getListItem(string name)
	    {
		    //std::vector<ListItem*>::iterator it;
		    //for( it = mItems.begin(); it != mItems.end(); ++it )
            foreach(ListItem i in mItems)
		    {
			    if( i.getInstanceName().Equals(name) )
                    return i;
		    }

		    return null;
	    }

	    public int getNumberOfListItems()
	    {
		    return mChildLists.Count;
	    }

	    new void hide()
	    {
		    mHighlightPanel.Hide();

		    base.hide();
	    }

	    public void hideHighlight()
	    {
		    mHighlightPanel.Hide();
	    }

	    public void highlightListItem(ListItem i)
	    {
            Vector4 liPixelDimensions = i.getDimensions(QGuiMetricsMode.QGUI_GMM_PIXELS, QGuiMetricsMode.QGUI_GMM_PIXELS);
		    mHighlightPanel.SetPosition(liPixelDimensions.x,liPixelDimensions.y);
		    mHighlightPanel.SetDimensions(liPixelDimensions.z,liPixelDimensions.w);
		    mHighlightPanel.Show();
	    }

	    void removeList(int index)
	    {
		    if( (mChildLists.Count - 1) < index )
                return;
    		
		    int counter = 0;
		    //std::vector<List*>::iterator it;
		    //for( it = mChildLists.begin(); it != mChildLists.end(); ++it )
            foreach(List i in mChildLists)
		    {
			    if( counter == index )
			    {
				    _removeChildWidget(i);
				    mChildLists.Remove(i);
				    return;
			    }
			    ++counter;
		    }
	    }

	    public void removeListItem(int index)
	    {
		    if( (mItems.Count == 0) || ((mItems.Count - 1) < index) ) return;

		    // Delete the List Item
		    ListItem li = null;
            float FreeHeight = 0f;
            int counter = 0;
		    //std::vector<ListItem*>::iterator it;
		    //for( it = mItems.begin(); it != mItems.end(); ++it )
            foreach(ListItem i in mItems)
		    {
			    if( counter == index )
			    {
				    _removeChildWidget(i);
				    li = i;
                    FreeHeight = li.getSize(QGuiMetricsMode.QGUI_GMM_ABSOLUTE).y;
				    mItems.Remove(i);
			    }

			    ++counter;
		    }

		    // IMPORTANT! Do not call _notifyDimensionsChanged, otherwise all child ListItems will have their dimensions auto adjusted.
		    // We need to keep them as is, and later use the unchanged values to calculate new values, as the list height has changed.
		    // Essentially, we want to maintain the ListItems' height while adjusting the List's height.
		    mRelativeDimensions.w = (mAbsoluteDimensions.w - FreeHeight) / (mParentWidget.getSize(QGuiMetricsMode.QGUI_GMM_ABSOLUTE).y);
		    _updateDimensions(mRelativeDimensions);

		    float newYPos = 0.0f;
		    //for( it = mItems.begin(); it != mItems.end(); ++it )
            foreach(ListItem i in mItems)
		    {
			    Vector2 pos = i.getPosition(QGuiMetricsMode.QGUI_GMM_ABSOLUTE);
			    pos.y = newYPos;
			    Vector2 size = i.getSize(QGuiMetricsMode.QGUI_GMM_ABSOLUTE);

			    i.setDimensions(
				    new Vector4(
					    (pos.x - mAbsoluteDimensions.x) / mAbsoluteDimensions.z,
					    (pos.y) / mAbsoluteDimensions.w,
					    size.x / mAbsoluteDimensions.z,
					    size.y / mAbsoluteDimensions.w
					    )
				    );

			    newYPos = newYPos + i.getSize(QGuiMetricsMode.QGUI_GMM_ABSOLUTE).y;
		    }
		    // Important!  Even though we have set the dimensions, we need to update the overlay elements (of the list, specifically)
            _notifyDimensionsChanged();
	    }

	    public override void setCharacterHeight(float relativeHeight)
	    {
		    base.setCharacterHeight(relativeHeight);
		    //std::vector<ListItem*>::iterator it;
		    //for( it = mItems.begin(); it != mItems.end(); ++it )
            foreach(ListItem i in mItems)
		    {
			    i.setCharacterHeight(relativeHeight);
		    }
	    }

	    public void setHighlightMaterial(string material)
	    {
		    mHighlightMaterial = material;
		    mHighlightPanel.MaterialName = mHighlightMaterial;
	    }

	    public void setWidth(float relativeWidth)
	    {
		    mRelativeDimensions.z = relativeWidth;
		    _notifyDimensionsChanged();
	    }

	    public override void show()
	    {
		    mHighlightPanel.Hide();

		    base.show();
	    }
	    
	    public ListItem getItem(string text) {
	    	foreach (ListItem li in mItems) {
	    		if (li.getText().Equals(text)) {
	    			return li;
	    		}
	    	}
	    	return null;
	    }
    }
}
