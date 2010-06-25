using System;
using System.Collections.Generic;
using System.Text;
using Mogre;
using MOIS;

namespace MQuickGUI
{
	/** Represents a TextBox.
		@remarks
		TextBoxes allow the user to input data on the screen,
		which can be used for other purposes.  The TextBox class 
		requires at least 2 materials to define it's image:
		Background Image, Border.  For example, if you pass
		the constructor "sample.textbox" as its arguement for the 
		material, the class will expect "sample.textbox.border"
		to exist.
		@note
		TextBoxes must be created by the Window widget.
	*/
    public class TextBox : Widget
    {
		protected bool		mMaskUserInput;
		protected string    mMaskSymbol;

		protected float     mBackSpaceTimer;
		protected bool		mBackSpaceDown;
		protected float 	mDeleteTimer;
		protected bool		mDeleteDown;
		protected float     mMoveCursorTimer;
		protected bool		mLeftArrowDown;
		protected bool		mRightArrowDown;
		protected float     mCursorVisibilityTimer;
		protected bool      mReadOnly;

		protected Text		mTextWidget;
		protected bool		mInputMode;

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
        public TextBox(string name, Vector4 dimensions, QGuiMetricsMode positionMode, QGuiMetricsMode sizeMode, string material, OverlayContainer overlayContainer, Widget ParentWidget)
            : base(name, dimensions, positionMode, sizeMode, material, overlayContainer, ParentWidget)
	    {
		    mWidgetType = Widget.WidgetType.QGUI_TYPE_TEXTBOX;

       		mMaskUserInput = false;
		    mBackSpaceDown = false;
		    mBackSpaceTimer = 0.0f;
		    mDeleteDown = false;
		    mDeleteTimer = 0.0f;
		    mLeftArrowDown = false;
		    mRightArrowDown = false;
		    mMoveCursorTimer = 0.0f;
		    mCursorVisibilityTimer = 0.0f;
		    mReadOnly = false;
		    mInputMode = false;
                

		    // Border Overlay gives us ability to assign material to TextBox border and Panel separately.
		    mOverlayElement = createPanelOverlayElement(mInstanceName+".Background",mPixelDimensions,"");
		    mOverlayContainer.AddChild(mOverlayElement);
		    mOverlayElement.Show();
            setMaterial(mWidgetMaterial);

            mCharacterHeight = 0.75f;
            Mogre.Vector3 textDimensions = new Mogre.Vector3(0, 0, mCharacterHeight);
		    // Label has no material, since it directly overlaps the textbox overlay element
		    mTextWidget = new Text(mInstanceName+".Text",textDimensions,QGuiMetricsMode.QGUI_GMM_RELATIVE,QGuiMetricsMode.QGUI_GMM_RELATIVE,mChildrenContainer,this);
		    mTextWidget.setTruncateMode(Text.TruncateMode.LEFT);
		    mTextWidget.setTruncationFeedback("");
		    mTextWidget.setZOrderOffset(1);
		    _addChildWidget(mTextWidget);

		    mHorizontalAlignment = GuiHorizontalAlignment.GHA_LEFT;
		    mVerticalAlignment = GuiVerticalAlignment.GVA_CENTER;

		    alignText(mHorizontalAlignment,mVerticalAlignment);

            OnDeactivate += new DeactivateEventHandler(TextBox_OnDeactivate);
            OnCharacter += new CharacterEventHandler(TextBox_OnCharacter);
            OnKeyDown += new KeyDownEventHandler(TextBox_OnKeyDown);
            OnKeyUp +=new KeyUpEventHandler(TextBox_OnKeyUp);
            OnMouseButtonDown +=new MouseButtonDownEventHandler(TextBox_OnMouseButtonDown);
            OnTimeElapsed +=new TimeElapsedHandler(TextBox_OnTimeElapsed);

	    }

		/**
		* When user has changed the font, character height, or text,
		* the label must be updated and aligned according to its parent.
		*/
		protected override void _notifyTextChanged()
        {
		    mTextWidget.setFont(mFont);
		    mTextWidget.setCharacterHeight(mCharacterHeight);
		    mTextWidget.setTextColor(mTextTopColor,mTextBotColor);
		    mTextWidget.setText(mTextWidget.getText());

		    alignText(mHorizontalAlignment,mVerticalAlignment);
        }

		/**
		* Adds a character to the textBox right before text cursor.
		*/
		void addCharacter(string c)
        {
		    if(mReadOnly)
                return;

		    // Insert a cter right before the text cursor.
		    int index = mTextWidget.getCursorIndex();

            mText = mText.Insert(index, c);

		    // Handle adding character to password box.
		    string s = string.Empty;
            if (mMaskUserInput)
            {
		    	StringBuilder sb = new StringBuilder();
		    	sb.Append(mMaskSymbol.ToCharArray()[0], mText.Length);
		    	s = sb.ToString();
            }
            else
            {
                s = mText;
            }

            


		    mTextWidget.setText(s);
		    _notifyTextChanged();
		    mTextWidget.incrementCursorIndex();
        }

		/**
		* Add user defined event that will be called when user presses Enter key with Textbox Activated.
		*/
        public delegate void EnterPressedEventHandler(object source, KeyEventArgs e);
        public event EnterPressedEventHandler OnEnterPressed;

        /**
		* Aligns the child Label widget horizontally and vertically.
		*/
		void alignText(GuiHorizontalAlignment ha, GuiVerticalAlignment va)
        {
		    mHorizontalAlignment = ha;
		    mVerticalAlignment = va;

            Vector2 relativeLabelPos = mTextWidget.getPosition(QGuiMetricsMode.QGUI_GMM_RELATIVE);
		    Vector2 labelPos = mTextWidget.getPosition(QGuiMetricsMode.QGUI_GMM_ABSOLUTE);
		    Vector2 labelSize = mTextWidget.getSize(QGuiMetricsMode.QGUI_GMM_ABSOLUTE);

		    if( mHorizontalAlignment == GuiHorizontalAlignment.GHA_LEFT ) 
		    {
			    float widthBuffer = 5.0f / mPixelDimensions.z;
			    mTextWidget.setPosition(widthBuffer,relativeLabelPos.y);
		    }
		    if( mHorizontalAlignment == GuiHorizontalAlignment.GHA_CENTER )
		    {
			    mTextWidget.setPosition(((mAbsoluteDimensions.z / 2) - (labelSize.x / 2)) / mAbsoluteDimensions.z,relativeLabelPos.y);
		    }
		    if( mHorizontalAlignment == GuiHorizontalAlignment.GHA_RIGHT )
		    {
			    float widthBuffer = 5.0f / mPixelDimensions.z;
			    mTextWidget.setPosition(((mAbsoluteDimensions.z) - (labelSize.x) - widthBuffer) / mAbsoluteDimensions.z,relativeLabelPos.y);
		    }

		    // Make sure to update the position, in case alignment has moved it
		    relativeLabelPos = mTextWidget.getPosition(QGuiMetricsMode.QGUI_GMM_RELATIVE);

		    if( mVerticalAlignment == GuiVerticalAlignment.GVA_TOP ) 
		    {
			    float heightBuffer = 3.0f / mPixelDimensions.w;
			    mTextWidget.setPosition(relativeLabelPos.x,heightBuffer);
		    }
		    if( mVerticalAlignment == GuiVerticalAlignment.GVA_CENTER )
		    {
			    mTextWidget.setPosition(relativeLabelPos.x,((mAbsoluteDimensions.w / 2) - (labelSize.y / 2)) / mAbsoluteDimensions.w);
		    }
		    if( mVerticalAlignment == GuiVerticalAlignment.GVA_BOTTOM )
		    {
			    float heightBuffer = 3.0f / mPixelDimensions.w;
			    mTextWidget.setPosition(relativeLabelPos.x,((mAbsoluteDimensions.w) - (labelSize.y) - heightBuffer) / mAbsoluteDimensions.w);
		    }

        }
		/**
		* Method to erase the character right before the text cursor.
		*/
		void backSpace()
        {
		    if( string.Empty.Equals(mText) )
                return;

		    // remove character from cursor position
		    int index = mTextWidget.getCursorIndex();
		    if( index == 0 )
                return;
            mText = mText.Remove(index - 1, 1);

		    // Handle removing character to password box.
		    string s = string.Empty;
		    if(mMaskUserInput) {
		    	StringBuilder sb = new StringBuilder();
		    	sb.Append(mMaskSymbol.ToCharArray()[0], mText.Length);
		    	s = sb.ToString();
		    }
		    else {
                s = mText;
		    }

		    mTextWidget.setText(s);
		    _notifyTextChanged();
		    mTextWidget.decrementCursorIndex();
        }

		/**
		* Method to erase the character right after the text cursor.
		*/
		void deleteCharacter()
        {
		    if( string.Empty.Equals(mText) )
                return;

		    // remove character in front of cursor position
		    int index = mTextWidget.getCursorIndex();
		    if ( index >= mText.Length ){
		    	return ;
		    }
            mText = mText.Remove(index, 1);

		    // Handle removing character to password box.
            string s = string.Empty;
            if (mMaskUserInput) {
		    	StringBuilder sb = new StringBuilder();
		    	sb.Append(mMaskSymbol.ToCharArray()[0], mText.Length);
		    	s = sb.ToString();
            }
            else {
                s = mText;
            }

		    mTextWidget.setText(s);
		    _notifyTextChanged();
        }

		/**
		* Sets focus to the widget, displaying the text cursor.
		*/
		public override void focus()
        {
		    if(!mEnabled)
                return;

		    mTextWidget.setTextCursorPosition(Vector2.ZERO);
		    mInputMode = true;

		    mGUIManager.setActiveWidget(this);
       }

		/**
		* Gets a handle to the Label widget used for this Widget.
		*/
		Text getTextWidget()
        {
            return mTextWidget;
        }

        bool getReadOnly()
        {
       		return mReadOnly;
        }

		/**
		* Hides the actual user input and writes the designated character
		* in its place.  Great for visible password protection.
		*/
		public void maskUserInput(string symbol)
        {
		    mMaskSymbol = symbol;

		    // if there was previously text, we now need to mask it.
		    if( !string.Empty.Equals(mText) && !mMaskUserInput )
                setText(mText);

		    mMaskUserInput = true;
        }

		/**
		* If set to true, cannot input text to textbox
		*/
		void setReadOnly(bool readOnly)
        {
    		mReadOnly = readOnly;
        }

		public override void setText(string text)
        {
            string t = text;
		    if(mMaskUserInput)
		    {
		    	StringBuilder sb = new StringBuilder();
		    	sb.Append(mMaskSymbol.ToCharArray()[0], text.Length);
		    	t = sb.ToString();
		    }
		    mTextWidget.setText(t);
            // TODO: Revisar que esto llame realmente al método de Widget
		    base.setText(text);
        }

        #region Event Handlers
        
        // Overridden Event Handling functions
		/**
		* Handler for the QGUI_EVENT_DEACTIVATED event, and deactivates all child widgets (if exist)
		*/
        void  TextBox_OnDeactivate(object source, EventArgs e)
        {
		    if(!mEnabled)
                return;

		    mTextWidget.hideTextCursor();
		    mInputMode = false;
		    mBackSpaceDown = false;
		    mLeftArrowDown = false;
		    mRightArrowDown = false;
        }

		/**
		* User defined event handler called when user presses Enter.
		* Note that this event is not passed to its parents, the event is specific to this widget.
		*/
        public void EnterPressed(KeyEventArgs e)
        {
            if (!mEnabled)
                return;

            if (OnEnterPressed != null)
                OnEnterPressed(this, e);
        }

		/**
		* Handler for the QGUI_EVENT_KEY_DOWN event.  If not handled, it will be passed
		* to the parent widget (if exists)
		*/
        void TextBox_OnKeyDown(object source, KeyEventArgs e)
        {
		    if(!mEnabled)
                return;

		    if( (e.scancode == KeyCode.KC_BACK) && !mReadOnly ) 
		    {
			    mBackSpaceDown = true;
			    mLeftArrowDown = false;
			    mRightArrowDown = false;
			    mBackSpaceTimer = 0.0f;
			    backSpace();
		    }
		    else if( e.scancode == KeyCode.KC_LEFT )
		    {
			    mMoveCursorTimer = 0.0f;
			    mLeftArrowDown = true;
			    mRightArrowDown = false;
			    mTextWidget.decrementCursorIndex();
		    }
		    else if( e.scancode == KeyCode.KC_RIGHT )
		    {
			    mMoveCursorTimer = 0.0f;
			    mRightArrowDown = true;
			    mLeftArrowDown = false;
			    mTextWidget.incrementCursorIndex();
		    }
		    else if( e.scancode == KeyCode.KC_DELETE )
		    {
			    mDeleteTimer = 0.0f;
			    mDeleteDown = true;
			    mBackSpaceDown = false;
			    mLeftArrowDown = false;
			    mRightArrowDown = false;
			    deleteCharacter();
		    }
        }

		/**
		* Handler for the QGUI_EVENT_KEY_UP event.  If not handled, it will be passed
		* to the parent widget (if exists)
		*/
        void TextBox_OnKeyUp(object source, KeyEventArgs e)
        {
		    if(!mEnabled)
                return;

		    if( (e.scancode == KeyCode.KC_BACK) && !mReadOnly )
			    mBackSpaceDown = false;
		    else if( e.scancode == KeyCode.KC_LEFT )
                mLeftArrowDown = false;
		    else if( e.scancode == KeyCode.KC_RIGHT )
                mRightArrowDown = false;
		    else if( e.scancode == KeyCode.KC_DELETE )
                mDeleteDown = false;
		    else if( e.scancode == KeyCode.KC_RETURN )
                EnterPressed(e);
        }

		/**
		* Handler for the QGUI_EVENT_CHARACTER_KEY event.  Appends character to the end of the Label's text.
		* If not handled, it will be passed to the parent widget (if exists)
		*/
        void TextBox_OnCharacter(object source, KeyEventArgs e)
        {
		    if(!mEnabled)
                return;

		    if(!mReadOnly) 
		    {
			    mBackSpaceDown = false;
			    mLeftArrowDown = false;
			    mRightArrowDown = false;

			    addCharacter(e.codepoint.ToString());
		    }
        }

		/**
		* Default Handler for the QGUI_EVENT_MOUSE_BUTTON_DOWN event.  If not handled, it will be passed
		* to the parent widget (if exists)
		*/
        void TextBox_OnMouseButtonDown(object source, MouseEventArgs e)
        {
		    if(!mEnabled)
                return;

		    if(e.button == MouseButtonID.MB_Left) 
		    {
			    mTextWidget.setTextCursorPosition(e.position);
			    mInputMode = true;
		    }
        }

		/**
		* Default Handler for injecting Time.
		*/
        void TextBox_OnTimeElapsed(object source, TimeEventArgs e)
        {

            if (!mEnabled)
                return;

            mBackSpaceTimer += e.time;
            mCursorVisibilityTimer += e.time;
            mMoveCursorTimer += e.time;
            mDeleteTimer += e.time;

            // Hard coding the time to allow repetitive operations to be every .5 seconds
            if (mBackSpaceTimer > 0.125)
            {
                if (mBackSpaceDown && !mReadOnly)
                    backSpace();
                mBackSpaceTimer = 0.0f;
            }

            if (mCursorVisibilityTimer > 0.5)
            {
                if (mInputMode && !mReadOnly && !(mLeftArrowDown || mRightArrowDown))
                    mTextWidget.toggleTextCursorVisibility();
                mCursorVisibilityTimer = 0.0f;
            }

            if (mMoveCursorTimer > 0.125)
            {
                if (mLeftArrowDown)
                    mTextWidget.decrementCursorIndex();
                else if (mRightArrowDown)
                    mTextWidget.incrementCursorIndex();
                mMoveCursorTimer = 0.0f;
            }

            if (mDeleteTimer > 0.125)
            {
                if (mDeleteDown && !mReadOnly)
                    deleteCharacter();
                mDeleteTimer = 0.0f;
            }

        }

        #endregion

    }
}
