using System;
using System.Collections.Generic;
using System.Text;
using Mogre;
using MOIS;

namespace MQuickGUI
{
    /** Represents a traditional ComboBox.
    @remarks
    The ComboBox class requires at least 3 materials to define it's image:
    Normal State, Mouse Over, and Mouse Down.  For example, if you pass
    the constructor "sample.combobox" as its arguement for the material,
    the class will expect "sample.combobox.over" and "sample.combobox.down"
    to exist.  The ComboBox supplies a list of items from which the user
    can choose.
    @note
    In order to get the most use out of ComboBox, you will need to add
    ListItems.
    @note
    ComboBoxes are meant to be created via the Window widget.
    */
    public class ComboBox : Label
    {
   		// ComboBox Label component, usually displays the text of the ListItem
		// that has been selected.
		protected Label mLabel;
		// Button that shows the drop down list.
		protected Button mButton;
		// Drop down list.
		protected List mList;

		GuiHorizontalAlignment mListItemHorizontalAlignment;
		GuiVerticalAlignment mListItemVerticalAlignment;

		// User defined event handlers that are called when a Selection is made.
        // Reemplazado por Manejo de Eventos // GDZ
		//List<MemberFunctionSlot> mOnSelectUserEventHandlers = new List<MemberFunctionSlot>();

        
        internal ComboBox(string name, Vector4 dimensions, QGuiMetricsMode positionMode, QGuiMetricsMode sizeMode, string material, OverlayContainer overlayContainer, Widget ParentWidget) : base(name,dimensions,positionMode,sizeMode,material,overlayContainer,ParentWidget)
	    {
            mListItemHorizontalAlignment = GuiHorizontalAlignment.GHA_CENTER;
            mListItemVerticalAlignment = GuiVerticalAlignment.GVA_CENTER;
		    mWidgetType = Widget.WidgetType.QGUI_TYPE_COMBOBOX;

		    // Create CloseButton - remember to position it relative to it's parent (TitleBar)
		    // Height of the Title Bar
		    float height = (mAbsoluteDimensions.w / mAbsoluteDimensions.z);
		    // Button has same height as width - Make the button slightly smaller that the titlebar height
		    float buttonHeight = 0.8f;
		    float buttonWidth = (height * buttonHeight);
		    // Make a 5 pixel buffer
		    float buffer = 5.0f / mPixelDimensions.z;
            Vector4 bDimensions = new Vector4((1 - (buttonWidth + buffer)), 0.1f, buttonWidth, buttonHeight);
		    mButton = new Button(mInstanceName+".button",bDimensions, QGuiMetricsMode.QGUI_GMM_RELATIVE, QGuiMetricsMode.QGUI_GMM_RELATIVE, mWidgetMaterial+".button",mChildrenContainer,this);
		    mButton.setZOrderOffset(1);
		    _addChildWidget(mButton);

		    // Make a 15 pixel buffer for the Label
		    float bigBuffer = 15.0f / mPixelDimensions.z;
		    mLabel = new Label(mInstanceName+".Label",new Vector4(bigBuffer,0,(1 - (buttonWidth + bigBuffer)),1), QGuiMetricsMode.QGUI_GMM_RELATIVE, QGuiMetricsMode.QGUI_GMM_RELATIVE ,"",mChildrenContainer,this);
            mLabel.OnDeactivate += new DeactivateEventHandler(ComboBox_OnDeactivate);
		    //Reemplazado por Manejo de Eventos //GDZ
            //mLabel.addEventHandler((int)Widget.Event.QGUI_EVENT_DEACTIVATED, this);
		    mLabel.setZOrderOffset(1);
		    _addChildWidget(mLabel);

		    mList = new List(mInstanceName+".List",new Mogre.Vector3(0,1.0f,1.0f), QGuiMetricsMode.QGUI_GMM_RELATIVE, QGuiMetricsMode.QGUI_GMM_RELATIVE, mWidgetMaterial+".list",getSheet().getMenuContainer(),this);
		    mList.setCharacterHeight(mCharacterHeight);
		    mList.setFont(mFont);
		    mList.setTextColor(mTextTopColor,mTextBotColor);
		    mList.hide();
		    int derivedZOrder = getSheet().getMenuOverlayZOrder() + 1;
            Window w = getWindow();
            if (w != null)
                mList.setZOrderOffset(derivedZOrder - getWindow().getZOrder(), false);
            else
                mList.setZOrderOffset(derivedZOrder, false);
            _addChildWidget(mList);

            OnDeactivate += new DeactivateEventHandler(ComboBox_OnDeactivate);
            OnMouseEnter += new MouseEnterEventHandler(ComboBox_OnMouseEnter);
            OnMouseLeaves += new MouseLeavesEventHandler(ComboBox_OnMouseLeaves);
            OnMouseButtonDown += new MouseButtonDownEventHandler(ComboBox_OnMouseButtonDown);
            OnMouseButtonUp += new MouseButtonUpEventHandler(ComboBox_OnMouseButtonUp);
	    }

        void ComboBox_OnDeactivate(object source, EventArgs e)
        {
            if (!mEnabled)
                return;

            //Restore default material
            mOverlayElement.MaterialName = mWidgetMaterial;
            mButton.applyDefaultMaterial();
            //If the Mouse has clicked on the ComboBox's List or ListItems, the widget should not *deactivate*.
            //As for hiding the list, this will be taken care of in the onMouseButtonDown handler.  The list needs
            //to remain visible so that ListItem picking works correctly. (If list is hidden, you can't click the ListItem..)
            Vector2 p = MouseCursor.Singleton.getPixelPosition();
            if (getTargetWidget(p) != null)
                return;

            mList.hide();
        }

        void ComboBox_OnMouseEnter(object source, MouseEventArgs e)
        {
            if (!mEnabled)
                return;

            if (!mList.isVisible())
                mOverlayElement.MaterialName = mWidgetMaterial + ".over";

            if (mList.isVisible())
                mButton.applyButtonDownMaterial();
        }

        void ComboBox_OnMouseLeaves(object source, MouseEventArgs e)
        {
            if (!mEnabled)
                return;

            if (!mList.isVisible())
                mOverlayElement.MaterialName = mWidgetMaterial;
        }

        void ComboBox_OnMouseButtonDown(object source, MouseEventArgs e)
        {
            if (!mEnabled)
                return;

            if (e.button == MouseButtonID.MB_Left)
            {
                Widget w = mList.getTargetWidget(e.position);
                if (w != null)
                {
                    e.widget = w;
                    Selection(e);
                    return;
                }
                else
                {
                    if (!mList.isVisible())
                    {
                        // apply button ".down" material
                        mOverlayElement.MaterialName = mWidgetMaterial + ".down";
                        mButton.applyButtonDownMaterial();
                        mList.show();
                    }
                    else
                    {
                        mOverlayElement.MaterialName = mWidgetMaterial + ".over";
                        mButton.applyDefaultMaterial();
                        mList.hide();
                    }
                }
            }
        }

        void ComboBox_OnMouseButtonUp(object source, MouseEventArgs e)
        {
            if (!mEnabled)
                return; 
            
            if (mList.isVisible())
                mButton.applyButtonDownMaterial();
        }

	    ~ComboBox()
	    {
		    removeAndDestroyAllChildWidgets();
	    }

        public void addListItem(string text)
	    {
		    ListItem newListItem = mList.addListItem(text);
	    }

        public void addListItem(string name, string text)
	    {
		    ListItem newListItem = mList.addListItem(name,text);
	    }

        public void alignListItemText(GuiHorizontalAlignment ha, GuiVerticalAlignment va)
	    {
		    mListItemHorizontalAlignment = ha;
		    mListItemVerticalAlignment = va;

		    mList.alignListItemText(mListItemHorizontalAlignment,mListItemVerticalAlignment);
	    }

	    void clearList()
	    {
		    mList.clearList();
	    }

	    int getNumberOfListItems()
	    {
		    return mList.getNumberOfListItems();
	    }

	    void removeListItem(int index)
	    {
		    mList.removeListItem(index);
	    }

	    new void setCharacterHeight(float relativeHeight)
	    {
		    base.setCharacterHeight(relativeHeight);
		    mList.setCharacterHeight(relativeHeight);
	    }

	    new void setText(string text)
	    {
		    // If text is bigger than combobox width, append "..." to a fitting portion of the text
		    mText = text;	// store original text so we can retrieve it.

		    mLabel.setFont(mFont);
		    mLabel.setCharacterHeight(mCharacterHeight);
		    mLabel.setTextColor(mTextTopColor,mTextBotColor);
		    mLabel.setText(mText);
		    mLabel.alignText(mHorizontalAlignment,mVerticalAlignment);
	    }

	    void setDropListHighlightMaterial(string material)
	    {
		    mList.setHighlightMaterial(material);
	    }

	    void setDropListWidth(float relativeWidth)
	    {
		    mList.setWidth(relativeWidth);
	    }

	    public override void show()
	    {
		    base.show();
		    mList.hide();
	    }


        public delegate void SelectionEventHandler(object source, WidgetEventArgs e);
        public event SelectionEventHandler OnSelection;

        void Selection(WidgetEventArgs e)
        {
            if (!mEnabled)
                return;

            SetSelectedItem(e.widget.getText());
        }
        
        public void SetSelectedItem(string itemText){
        	
        	Widget selectedItem=null;
        	
        	selectedItem = mList.getListItem(itemText);
        	
        	if (selectedItem!=null) {
        		setText(selectedItem.getText());
	            mOverlayElement.MaterialName = mWidgetMaterial;
	            mButton.applyDefaultMaterial();
	            mList.hide();
	            if (OnSelection!=null){
		            WidgetEventArgs wea = new WidgetEventArgs(selectedItem);
	                OnSelection(this, wea);
	            }
        	}
        }


    }
}
