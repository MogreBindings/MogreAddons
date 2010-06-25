using System;
using System.Collections.Generic;
using System.Text;
using Mogre;

namespace MQuickGUI
{
    /** Represents a Widget Container.
        @remarks
        The Panel class has the ability to create the majority of defined Widgets.
        The Sheet and Window Widgets derive from this widget (Panel), giving them the
        same abilities.
        @note
        Panels cannot create the TitleBar, Window, or Sheet widget.
        @note
        Panels are meant to be created via the Window and Sheet widget.
    */
    public class Panel : Widget
    {
		protected List<int>					mZOrderValues = new List<int>();

		protected int						mAutoNameWidgetCounter;

		// Recording the number of widgets created
		protected int						mNumButtons;
		protected int						mNumComboBoxes;
		protected int						mNumImages;
		protected int						mNumLabels;
		protected int						mNumLists;		
		protected int						mNumMenus;
		protected int						mNumNStateButtons;
		protected int						mNumPanels;
		protected int						mNumProgressBars;
		protected int						mNumTextBoxes;
        protected int                       mNumTrackBars;

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
                parentWidget parent widget which created this widget.
        */
        public Panel(string name, Vector4 dimensions, QGuiMetricsMode positionMode, QGuiMetricsMode sizeMode, string material, OverlayContainer overlayContainer, Widget parentWidget)
            : base(name, dimensions, positionMode, sizeMode, material, overlayContainer, parentWidget)
	    {
		    mAutoNameWidgetCounter = 0;
		    mNumButtons = 0;
		    mNumComboBoxes = 0;
		    mNumImages = 0;
		    mNumLabels = 0;
		    mNumLists = 0;
		    mNumMenus = 0;
		    mNumNStateButtons = 0;
		    mNumPanels = 0;
		    mNumProgressBars = 0;
		    mNumTextBoxes = 0;
            mNumTrackBars = 0;

            mWidgetType = WidgetType.QGUI_TYPE_PANEL;
		    mZOrderValues.Clear();

		    if( overlayContainer == null )
		    {
			    mOverlayContainer = createOverlayContainer(mInstanceName+".OverlayContainer","");
			    mOverlayContainer.AddChildImpl(mChildrenContainer);

			    mOverlayContainer.Show();
			    mChildrenContainer.Show();
		    }

		    mOverlayElement = createPanelOverlayElement(mInstanceName+".Background",mPixelDimensions,"");
		    mOverlayContainer.AddChild(mOverlayElement);
		    mOverlayElement.Show();
            setMaterial(mWidgetMaterial);

            OnActivate += new ActivateEventHandler(Panel_OnActivate);
            OnDeactivate += new DeactivateEventHandler(Panel_OnDeactivate);
	    }
        
        

        public void _addZOrderValue(int zOrder)
	    {
		    mZOrderValues.Add(zOrder);  //REPLACED: ->push_back(zOrder)
		    mZOrderValues.Sort();
	    }

        public void _removeZOrderValue(int zOrder)
        {
            //std::list<int>::iterator it;
            //for( it = mZOrderValues.begin(); it != mZOrderValues.end(); ++it )
            //{
            //    if( *it == zOrder ) mZOrderValues.erase(it);
            //    return;
            //}
            int item;
            for (int index = mZOrderValues.Count; index > 0; index--)
            {
                item = mZOrderValues[index - 1];
                if (item == zOrder)
                    mZOrderValues.Remove(item);
            }

        }

        /** 
        * Private functions preventing users from setting the Widget Instance Name.  Names
        * can be given to Windows using the "setReferenceName()" function.
        */
        Button _createButton(string name, Vector4 dimensions, QGuiMetricsMode positionMode, QGuiMetricsMode sizeMode, string material)
        {
            Button newButton = new Button(name,dimensions, positionMode, sizeMode, material,mChildrenContainer,this);
            newButton.setZOrderOffset(1);
            if(!mVisible)
                newButton.hide();
            _addChildWidget(newButton);
            // update count
            ++mNumButtons;
    		
            return newButton;
        }

        ComboBox _createComboBox(string name, Vector4 dimensions, QGuiMetricsMode positionMode, QGuiMetricsMode sizeMode, string material)
        {
            ComboBox newComboBox = new ComboBox(name,dimensions, positionMode, sizeMode, material,mChildrenContainer,this);
            newComboBox.setZOrderOffset(1);
            if(!mVisible)
                newComboBox.hide();
            _addChildWidget(newComboBox);
            // update count
            ++mNumComboBoxes;
    		
            return newComboBox;
        }

       	Image _createImage(string name, Vector4 dimensions, QGuiMetricsMode positionMode, QGuiMetricsMode sizeMode, string material, bool texture)
	    {
		    string defaultMaterial = getSheet().getDefaultSkin() + ".image";
		    Image newImage = new Image(name,dimensions,positionMode,sizeMode,defaultMaterial,mChildrenContainer,this);
		    if (texture)
                newImage.setMaterial(material,true);
		    else
                newImage.setMaterial(material);

		    newImage.setZOrderOffset(1);
		    if(!mVisible)
                newImage.hide();

		    _addChildWidget(newImage);
		    // update count
		    ++mNumImages;

		    return newImage;
	    }

	    Label _createLabel(string name, Vector4 dimensions, QGuiMetricsMode positionMode, QGuiMetricsMode sizeMode, string material)
	    {
		    Label newLabel = new Label(name,dimensions,positionMode,sizeMode,material,mChildrenContainer,this);
		    newLabel.setZOrderOffset(1);
		    if(!mVisible)
                newLabel.hide();
		    _addChildWidget(newLabel);
		    // update count
		    ++mNumLabels;

		    return newLabel;
	    }

	    List _createList(string name, Vector3 dimensions, QGuiMetricsMode positionMode, QGuiMetricsMode sizeMode, string material)
	    {
		    List newList = new List(name,dimensions,positionMode,sizeMode,material,mChildrenContainer,this);
		    newList.setZOrderOffset(1);
		    if(!mVisible)
                newList.hide();
		    _addChildWidget(newList);
		    // update count
		    ++mNumLists;

		    return newList;
	    }

	    List _createList(string name, Vector3 dimensions, Label itemTemplate, QGuiMetricsMode positionMode, QGuiMetricsMode sizeMode, string material)
	    {
		    List newList = new List(name,dimensions,itemTemplate,positionMode,sizeMode,material,mChildrenContainer,this);
		    newList.setZOrderOffset(1);
		    if(!mVisible)
                newList.hide();
		    _addChildWidget(newList);
		    // update count
		    ++mNumLists;

		    return newList;
	    }

	    Menu _createMenu(string name, Vector4 dimensions, QGuiMetricsMode positionMode, QGuiMetricsMode sizeMode, string material)
	    {
		    Menu newMenu = new Menu(name,dimensions,positionMode,sizeMode,material,mChildrenContainer,this);
		    newMenu.setZOrderOffset(1);
		    if(!mVisible)
                newMenu.hide();
		    _addChildWidget(newMenu);
		    // update count
		    ++mNumMenus;

		    return newMenu;
	    }
    
	    NStateButton _createNStateButton(string name, Vector4 dimensions, QGuiMetricsMode positionMode, QGuiMetricsMode sizeMode)
	    {
		    NStateButton newNStateButton = new NStateButton(name,dimensions,positionMode,sizeMode,mChildrenContainer,this);
		    newNStateButton.setZOrderOffset(1);
		    if(!mVisible)
                newNStateButton.hide();
		    _addChildWidget(newNStateButton);
		    // update count
		    ++mNumNStateButtons;

		    return newNStateButton;
	    }

		Panel _createPanel(string name, Vector4 dimensions, QGuiMetricsMode positionMode, QGuiMetricsMode sizeMode, string material)
		{
			Panel newPanel = new Panel(name,dimensions,positionMode,sizeMode,material,mChildrenContainer,this);
			newPanel.setZOrderOffset(1);
			if(!mVisible) {
				newPanel.hide();
			}
			_addChildWidget(newPanel);
			// update count
			++mNumPanels;
	
			return newPanel;
		}

	    
        TextBox _createTextBox(string name, Vector4 dimensions, QGuiMetricsMode positionMode, QGuiMetricsMode sizeMode, string material)
	    {
		    TextBox newTextBox = new TextBox(name,dimensions,positionMode,sizeMode,material,mChildrenContainer,this);
		    newTextBox.setZOrderOffset(1);
		    if(!mVisible)
                newTextBox.hide();
		    _addChildWidget(newTextBox);
		    // update count
		    ++mNumTextBoxes;

		    return newTextBox;
	    }


	    bool _destroyWidget(WidgetType type, int index)
	    {
		    int counter = -1;
            //std::vector<Widget*>::iterator it;
            //for( it = mChildWidgets.begin(); it != mChildWidgets.end(); ++it )
            //{
            //    if( (*it)->getWidgetType() == type )
            //    {
            //        ++counter;
            //        if( counter == index )
            //        {
            //            Widget* w = (*it);
            //            mChildWidgets.erase(it);
            //            delete w;
            //            return true;
            //        }
            //    }
            //}

            // TODO: Verificar que puedo hacer el remove de mChildWidgets sin romper el foreach // GDZ
            foreach(Widget w in mChildWidgets) {
                if (w.getWidgetType() == type)
                {
                    ++counter;
                    if (counter == index) {
                        mChildWidgets.Remove(w);
                        return true;
                    }
                }
            }

		    return false;
	    }

	    bool _destroyWidget(string name)
	    {
            //std::vector<Widget*>::iterator it;
            //for( it = mChildWidgets.begin(); it != mChildWidgets.end(); ++it )
            //{
            //    if( (*it)->getInstanceName() == name )
            //    {
            //        Widget* w = (*it);
            //        mChildWidgets.erase(it);
            //        delete w;
            //        return true;
            //    }
            //}
            // TODO: Verificar que puedo hacer el remove de mChildWidgets sin romper el foreach // GDZ
            foreach(Widget w in mChildWidgets) {
                if (w.getInstanceName().Equals(name))
                {
                	w.DestroyWidget();
                    mChildWidgets.Remove(w);
                    return true;
                }
            }
		    return false;
	    }

   	    Widget _getWidget(WidgetType type, int index)
	    {
		    int counter = -1;
            foreach(Widget w in mChildWidgets) {
                if (w.getWidgetType() == type) {
                    ++counter;
                    if (counter == index) {
                        return w;
                    }
                }
            }
		    return null;
	    }

	    Widget _getWidget(string name)
	    {
            foreach(Widget w in mChildWidgets) {
                if (w.getInstanceName() == name) {
                    return w;
                }
            }
		    return null;
	    }

        void Panel_OnActivate(object source, EventArgs e)
        {
   		    if(!mEnabled)
                return;

            foreach(Widget w in mChildWidgets) {
                w.Activate(e);
            }
        }
    
        void Panel_OnDeactivate(object source, EventArgs e)
        {
		    if(!mEnabled)
                return;

            foreach(Widget w in mChildWidgets) {
                w.Deactivate(e);
            }
        }


        public Button createButton(string name, Vector4 dimensions, QGuiMetricsMode positionMode, QGuiMetricsMode sizeMode, string material)
        {
            if (!(GUIManager.Singleton.validWidgetName(name)))
                return null;

            return _createButton(name, dimensions, positionMode, sizeMode, material);
        }

        public Button createButton(Vector4 dimensions, QGuiMetricsMode positionMode, QGuiMetricsMode sizeMode, string material)
        {
            string name = mInstanceName + ".ChildWidget" + mAutoNameWidgetCounter;
            ++mAutoNameWidgetCounter;

            return _createButton(name, dimensions, positionMode, sizeMode, material);
        }

        public Button createButton(string name, Vector4 dimensions, QGuiMetricsMode positionMode, QGuiMetricsMode sizeMode)
        {
            if (!(GUIManager.Singleton.validWidgetName(name)))
                return null;

            string material = getSheet().getDefaultSkin() + ".button";

            return _createButton(name, dimensions, positionMode, sizeMode, material);
        }

        public Button createButton(Vector4 dimensions, QGuiMetricsMode positionMode, QGuiMetricsMode sizeMode)
        {
            string name = mInstanceName + ".ChildWidget" + mAutoNameWidgetCounter;
            ++mAutoNameWidgetCounter;

            string material = getSheet().getDefaultSkin() + ".button";

            return _createButton(name, dimensions, positionMode, sizeMode, material);
        }

        public ComboBox createComboBox(string name, Vector4 dimensions, QGuiMetricsMode positionMode, QGuiMetricsMode sizeMode, string material)
        {
            if (!(GUIManager.Singleton.validWidgetName(name)))
                return null;

            return _createComboBox(name, dimensions, positionMode, sizeMode, material);
        }

        public ComboBox createComboBox(Vector4 dimensions, QGuiMetricsMode positionMode, QGuiMetricsMode sizeMode, string material)
        {
            string name = mInstanceName + ".ChildWidget" + mAutoNameWidgetCounter;
            ++mAutoNameWidgetCounter;

            return _createComboBox(name, dimensions, positionMode, sizeMode, material);
        }

        public ComboBox createComboBox(string name, Vector4 dimensions, QGuiMetricsMode positionMode, QGuiMetricsMode sizeMode)
        {
            if (!(GUIManager.Singleton.validWidgetName(name)))
                return null;

            string material = getSheet().getDefaultSkin() + ".combobox";

            return _createComboBox(name, dimensions, positionMode, sizeMode, material);
        }

        public ComboBox createComboBox(Vector4 dimensions, QGuiMetricsMode positionMode, QGuiMetricsMode sizeMode)
        {
            string name = mInstanceName + ".ChildWidget" + mAutoNameWidgetCounter;
            ++mAutoNameWidgetCounter;

            String material = getSheet().getDefaultSkin() + ".combobox";

            return _createComboBox(name, dimensions, positionMode, sizeMode, material);
        }

        public Image createImage(string name, Vector4 dimensions, string material, bool texture, QGuiMetricsMode positionMode, QGuiMetricsMode sizeMode)
	    {
		    if( !(GUIManager.Singleton.validWidgetName(name)) )
                return null;

		    return _createImage(name,dimensions,positionMode,sizeMode,material,texture);
	    }

        public Image createImage(Vector4 dimensions, string material, bool texture, QGuiMetricsMode positionMode, QGuiMetricsMode sizeMode)
	    {
		    string name = mInstanceName + ".ChildWidget" + mAutoNameWidgetCounter;
		    ++mAutoNameWidgetCounter;

		    return _createImage(name,dimensions,positionMode,sizeMode,material,texture);
	    }

        public Image createImage(string name, Vector4 dimensions, bool texture, QGuiMetricsMode positionMode, QGuiMetricsMode sizeMode)
	    {
		    if( !(GUIManager.Singleton.validWidgetName(name)) )
                return null;

		    string material = getSheet().getDefaultSkin() + ".image";

		    return _createImage(name,dimensions,positionMode,sizeMode,material,texture);
	    }

        public Image createImage(Vector4 dimensions, bool texture, QGuiMetricsMode positionMode, QGuiMetricsMode sizeMode)
	    {
		    string name = mInstanceName + ".ChildWidget" + mAutoNameWidgetCounter;
		    ++mAutoNameWidgetCounter;

		    string material = getSheet().getDefaultSkin() + ".image";

		    return _createImage(name,dimensions,positionMode,sizeMode,material,texture);
	    }

        public Label createLabel(string name, Vector4 dimensions, string material, QGuiMetricsMode positionMode, QGuiMetricsMode sizeMode)
	    {
		    if( !(GUIManager.Singleton.validWidgetName(name)) )
                return null;

		    return _createLabel(name,dimensions,positionMode,sizeMode,material);
	    }


        public Label createLabel(Vector4 dimensions, string material, QGuiMetricsMode positionMode, QGuiMetricsMode sizeMode)
	    {
		    string name = mInstanceName + ".ChildWidget" + mAutoNameWidgetCounter;
		    ++mAutoNameWidgetCounter;

		    return _createLabel(name,dimensions,positionMode,sizeMode,material);
	    }

        public Label createLabel(string name, Vector4 dimensions, QGuiMetricsMode positionMode, QGuiMetricsMode sizeMode)
	    {
		    if( !(GUIManager.Singleton.validWidgetName(name)) )
                return null;

		    string material = getSheet().getDefaultSkin() + ".label";

		    return _createLabel(name,dimensions,positionMode,sizeMode,material);
	    }

        public Label createLabel(Vector4 dimensions, QGuiMetricsMode positionMode, QGuiMetricsMode sizeMode)
	    {
		    string name = mInstanceName + ".ChildWidget" + mAutoNameWidgetCounter;
		    ++mAutoNameWidgetCounter;

		    string material = getSheet().getDefaultSkin() + ".label";

		    return _createLabel(name,dimensions,positionMode,sizeMode,material);
	    }

        public List createList(string name, Vector3 dimensions, Label itemTemplate, string material, QGuiMetricsMode positionMode, QGuiMetricsMode sizeMode)
	    {
		    if( !(GUIManager.Singleton.validWidgetName(name)) )
                return null;

		    return _createList(name,dimensions,itemTemplate,positionMode,sizeMode,material);
	    }


        public List createList(Vector3 dimensions, Label itemTemplate, string material, QGuiMetricsMode positionMode, QGuiMetricsMode sizeMode)
	    {
		    string name = mInstanceName + ".ChildWidget" + mAutoNameWidgetCounter;
		    ++mAutoNameWidgetCounter;

		    return _createList(name,dimensions,itemTemplate,positionMode,sizeMode,material);
	    }

        public List createList(string name, Vector3 dimensions, Label itemTemplate, QGuiMetricsMode positionMode, QGuiMetricsMode sizeMode)
	    {
		    if( !(GUIManager.Singleton.validWidgetName(name)) )
                return null;

		    string material = getSheet().getDefaultSkin() + ".list";

		    return _createList(name,dimensions,itemTemplate,positionMode,sizeMode,material);
	    }

        public List createList(Vector3 dimensions, Label itemTemplate, QGuiMetricsMode positionMode, QGuiMetricsMode sizeMode)
	    {
		    string name = mInstanceName + ".ChildWidget" + mAutoNameWidgetCounter;
		    ++mAutoNameWidgetCounter;

		    string material = getSheet().getDefaultSkin() + ".list";

		    return _createList(name,dimensions,itemTemplate,positionMode,sizeMode,material);
	    }

        public Menu createMenu(string name, Vector4 dimensions, string material, QGuiMetricsMode positionMode, QGuiMetricsMode sizeMode)
	    {
		    if( !(GUIManager.Singleton.validWidgetName(name)) )
                return null;

		    return _createMenu(name,dimensions,positionMode,sizeMode,material);
	    }

        public Menu createMenu(Vector4 dimensions, string material, QGuiMetricsMode positionMode, QGuiMetricsMode sizeMode)
	    {
		    string name = mInstanceName + ".ChildWidget" + mAutoNameWidgetCounter;
		    ++mAutoNameWidgetCounter;

		    return _createMenu(name,dimensions,positionMode,sizeMode,material);
	    }

        public Menu createMenu(string name, Vector4 dimensions, QGuiMetricsMode positionMode, QGuiMetricsMode sizeMode)
	    {
		    if( !(GUIManager.Singleton.validWidgetName(name)) )
                return null;

		    string material = getSheet().getDefaultSkin() + ".menu";

		    return _createMenu(name,dimensions,positionMode,sizeMode,material);
	    }

        public Menu createMenu(Vector4 dimensions, QGuiMetricsMode positionMode, QGuiMetricsMode sizeMode)
	    {
		    string name = mInstanceName + ".ChildWidget" + mAutoNameWidgetCounter;
		    ++mAutoNameWidgetCounter;

		    string material = getSheet().getDefaultSkin() + ".menu";

		    return _createMenu(name,dimensions,positionMode,sizeMode,material);
	    }

        public NStateButton createNStateButton(string name, Vector4 dimensions, QGuiMetricsMode positionMode, QGuiMetricsMode sizeMode)
	    {
		    if( !(GUIManager.Singleton.validWidgetName(name)) )
                return null;

		    return _createNStateButton(name,dimensions,positionMode,sizeMode);
	    }

        public NStateButton createNStateButton(Vector4 dimensions, QGuiMetricsMode positionMode, QGuiMetricsMode sizeMode)
	    {
		    string name = mInstanceName + ".ChildWidget" + mAutoNameWidgetCounter;
		    ++mAutoNameWidgetCounter;

		    return _createNStateButton(name,dimensions,positionMode,sizeMode);
	    }

	        
		public Panel createPanel(string name, Vector4 dimensions, string material, QGuiMetricsMode positionMode, QGuiMetricsMode sizeMode)
		{
		    if( !(GUIManager.Singleton.validWidgetName(name)) )
                return null;
	
			return _createPanel(name,dimensions,positionMode,sizeMode,material);
		}
	
		public Panel createPanel(Vector4 dimensions, string material, QGuiMetricsMode positionMode, QGuiMetricsMode sizeMode)
		{
			string name = mInstanceName + ".ChildWidget" + mAutoNameWidgetCounter;
			++mAutoNameWidgetCounter;
	
			return _createPanel(name,dimensions,positionMode,sizeMode,material);
		}
	
		public Panel createPanel(string name, Vector4 dimensions, QGuiMetricsMode positionMode, QGuiMetricsMode sizeMode)
		{
		    if( !(GUIManager.Singleton.validWidgetName(name)) )
                return null;
	
		    string material = getSheet().getDefaultSkin() + ".panel";
	
			return _createPanel(name,dimensions,positionMode,sizeMode,material);
		}
	
		public Panel createPanel(Vector4 dimensions, QGuiMetricsMode positionMode, QGuiMetricsMode sizeMode)
		{
			string name = mInstanceName + ".ChildWidget" + mAutoNameWidgetCounter;
			++mAutoNameWidgetCounter;
	
		    string material = getSheet().getDefaultSkin() + ".panel";
	
			return _createPanel(name,dimensions,positionMode,sizeMode,material);
		}

        
	    public TextBox createTextBox(string name, Vector4 dimensions, string material, QGuiMetricsMode positionMode, QGuiMetricsMode sizeMode)
	    {
		    if( !(GUIManager.Singleton.validWidgetName(name)) )
                return null;

		    return _createTextBox(name,dimensions,positionMode,sizeMode,material);
	    }

        public TextBox createTextBox(Vector4 dimensions, string material, QGuiMetricsMode positionMode, QGuiMetricsMode sizeMode)
	    {
		    string name = mInstanceName + ".ChildWidget" + mAutoNameWidgetCounter;
		    ++mAutoNameWidgetCounter;

		    return _createTextBox(name,dimensions,positionMode,sizeMode,material);
	    }

        public TextBox createTextBox(string name, Vector4 dimensions, QGuiMetricsMode positionMode, QGuiMetricsMode sizeMode)
	    {
		    if( !(GUIManager.Singleton.validWidgetName(name)) )
                return null;

		    string material = getSheet().getDefaultSkin() + ".textbox";

		    return _createTextBox(name,dimensions,positionMode,sizeMode,material);
	    }

        public TextBox createTextBox(Vector4 dimensions, QGuiMetricsMode positionMode, QGuiMetricsMode sizeMode)
	    {
		    string name = mInstanceName + ".ChildWidget" + mAutoNameWidgetCounter;
		    ++mAutoNameWidgetCounter;

		    string material = getSheet().getDefaultSkin() + ".textbox";

		    return _createTextBox(name,dimensions,positionMode,sizeMode,material);
	    }

	    public void destroyButton(int index)
	    {
		    if( index >= mNumButtons )
                return;

		    if(_destroyWidget(WidgetType.QGUI_TYPE_BUTTON, index))
			    --mNumButtons;
	    }

        public void destroyButton(string name)
	    {
		    if(_destroyWidget(name))
			    --mNumButtons;
	    }

        public void destroyButton(Button b)
	    {
		    if(_destroyWidget(b.getInstanceName()))
			    --mNumButtons;
	    }

        public void destroyComboBox(int index)
	    {
		    if( index >= mNumComboBoxes ) return;

		    if(_destroyWidget(WidgetType.QGUI_TYPE_COMBOBOX, index))
			    --mNumComboBoxes;
	    }

        public void destroyComboBox(string name)
	    {
		    if(_destroyWidget(name))
			    --mNumComboBoxes;
	    }

        public void destroyComboBox(ComboBox b)
	    {
		    if(_destroyWidget(b.getInstanceName()))
			    --mNumComboBoxes;;
	    }

        public void destroyImage(int index)
	    {
		    if( index >= mNumImages )
                return;

		    if(_destroyWidget(WidgetType.QGUI_TYPE_IMAGE,index))
			    --mNumImages;
	    }

        public void destroyImage(string name)
	    {
		    if(_destroyWidget(name))
			    --mNumImages;
	    }

        public void destroyImage(Image i)
	    {
		    if(_destroyWidget(i.getInstanceName()))
			    --mNumImages;
	    }

        public void destroyLabel(int index)
	    {
		    if( index >= mNumLabels )
                return;

		    if(_destroyWidget(WidgetType.QGUI_TYPE_LABEL,index))
			    --mNumLabels;
	    }

        public void destroyLabel(string name)
	    {
		    if(_destroyWidget(name))
			    --mNumLabels;
	    }

        public void destroyLabel(Label l)
	    {
		    if(_destroyWidget(l.getInstanceName()))
			    --mNumLabels;
	    }

	    void destroyTextBox(int index)
	    {
		    if( index >= mNumTextBoxes )
                return;

		    if(_destroyWidget(WidgetType.QGUI_TYPE_TEXTBOX,index))
			    --mNumTextBoxes;
	    }

	    void destroyTextBox(string name)
	    {
		    if(_destroyWidget(name))
			    --mNumTextBoxes;
	    }

	    void destroyTextBox(TextBox b)
	    {
		    if(_destroyWidget(b.getInstanceName()))
			    --mNumTextBoxes;
	    }

        public Button getButton(int index)
	    {
		    if( index >= mNumButtons )
                return null;

		    Widget w = _getWidget(WidgetType.QGUI_TYPE_BUTTON, index);
		    if( w != null)
                return (Button)w;
            return null;
	    }

        public Button getButton(string name)
	    {
		    Widget w = _getWidget(name);
		    if( w != null )
                return (Button)w;
		    return null;
	    }

        public ComboBox getComboBox(int index)
	    {
		    if( index >= mNumComboBoxes )
                return null;

		    Widget w = _getWidget(WidgetType.QGUI_TYPE_COMBOBOX, index);
		    if( w != null)
                return (ComboBox)w;
            return null;
	    }

        public ComboBox getComboBox(string name)
	    {
		    Widget w = _getWidget(name);
		    if( w != null )
                return (ComboBox)w;
		    return null;
	    }

        public Image getImage(int index)
	    {
		    if( index >= mNumImages )
                return null;

		    Widget w = _getWidget(WidgetType.QGUI_TYPE_IMAGE,index);
		    if( w != null )
                return (Image)w;
		    else
                return null;
	    }

        public Image getImage(string name)
	    {
		    Widget w = _getWidget(name);
		    if( w != null )
                return (Image)w;
		    else
                return null;
	    }

        public Label getLabel(int index)
	    {
		    if( index >= mNumLabels )
                return null;

		    Widget w = _getWidget(WidgetType.QGUI_TYPE_LABEL,index);
		    if( w != null )
                return (Label)w;
		    else
                return null;
	    }

        public Label getLabel(string name)
	    {
		    Widget w = _getWidget(name);
		    if( w != null )
                return (Label)w;
		    else
                return null;
	    }

	   public TextBox getTextBox(int index)
	    {
		    if( index >= mNumTextBoxes )
                return null;

		    Widget w = _getWidget(WidgetType.QGUI_TYPE_TEXTBOX,index);
		    if( w != null )
                return (TextBox)w;
		    else
                return null;
	    }

	    public TextBox getTextBox(string name)
	    {
		    Widget w = _getWidget(name);
		    if( w != null )
                return (TextBox)w;
		    else
                return null;
	    }

        public string getMaterial()
	    {
		    return mWidgetMaterial;
	    }

        internal int getMaxZOrderValue()
        {
            return mZOrderValues[mZOrderValues.Count - 1];
        }

    }
}
