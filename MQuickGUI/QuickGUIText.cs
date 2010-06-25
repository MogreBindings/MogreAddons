using System;
using System.Collections.Generic;
using System.Text;
using Mogre;

namespace MQuickGUI
{
    public class Text : Widget
    {
		private TruncateMode mTruncateMode;
		private string mFeedbackString;

		private TextAreaOverlayElement mTextAreaOverlayElement;

		// Stores pixel offsets of where text characters begin and end.
		// Useful for placing a text cursor widget.
		private List<int> mCursorPositions = new List<int>();

		private TextCursor mTextCursor;
		private int mCursorIndex;
		// Offset applied to cursor index position to appear in between character;
		private int mCursorOffset;
		private bool mTextCursorHidden;

        public enum TruncateMode
		{
			LEFT	,
			RIGHT   ,
			NONE
		};
        
        public Text(string name, Vector3 dimensions, QGuiMetricsMode positionMode, QGuiMetricsMode sizeMode, OverlayContainer overlayContainer, Widget ParentWidget) : base(name,new Vector4(dimensions.x,dimensions.y,0,dimensions.z), positionMode, sizeMode,"",overlayContainer,ParentWidget)
	    {
		    mTextCursorHidden = true;
		    mCursorIndex = 0;
		    mCursorOffset = -3;
                        
            mWidgetType = Widget.WidgetType.QGUI_TYPE_TEXT;
		    mTruncateMode = TruncateMode.RIGHT;
		    mFeedbackString = "...";
		    mCursorPositions.Add(-5);
    		
		    mTextAreaOverlayElement = createTextAreaOverlayElement(mInstanceName+".Caption",mPixelDimensions,"");
		    mOverlayElement = mTextAreaOverlayElement;
		    mOverlayContainer.AddChild(mOverlayElement);
		    mTextAreaOverlayElement.Show();
		    mTextAreaOverlayElement.FontName = mFont;
		    mTextAreaOverlayElement.CharHeight = mPixelDimensions.w;

		    string tcMaterial = getSheet().getDefaultSkin() + ".textcursor";
		    mTextCursor = new TextCursor(mInstanceName+".TextCursor",new Vector4(0,0,0,1), QGuiMetricsMode.QGUI_GMM_RELATIVE, QGuiMetricsMode.QGUI_GMM_RELATIVE, tcMaterial,mChildrenContainer,this);
		    mTextCursor.setZOrderOffset(1,false);
		    mTextCursor.hide();
	    }

	    ~Text()
	    {
		    mTextCursor = null;
		    mTextAreaOverlayElement = null;
	    }
	    
	    public override void DestroyWidget()
		{
	    	if (mTextCursor!=null) {
	    		mTextCursor.DestroyWidget();
	    	}
			base.DestroyWidget();
		}

	    string _adjustWidth()
	    {
		    string displayText = mText;
		    // Adding a 5 pixel buffer, which helps textBoxes which have borders
		    int parentWidth = (int)mParentWidget.getSize(QGuiMetricsMode.QGUI_GMM_PIXELS).x - 5;


		    if( getTextWidth(displayText) > parentWidth )
		    {
			    // calling getTextWidth indexes the text, so cursor can be placed within text.  Only reason
			    // we don't break out early with truncate mode set to NONE.
			    if(mTruncateMode != TruncateMode.NONE)
			    {
				    // get width of feedback string
				    float feedbackWidth = getTextWidth(mFeedbackString);
				    float allotedWidth = parentWidth - feedbackWidth;
                    while ((getTextWidth(displayText) > allotedWidth) && !string.Empty.Equals(displayText))
				    {
					    if (mTruncateMode == TruncateMode.RIGHT)
                            displayText = displayText.Substring(0,displayText.Length-1); // GDZ Converted
					    else if(mTruncateMode == TruncateMode.LEFT)
                            displayText = displayText.Substring(1); // GDZ Converted
				    }
				    // concatenate
                    if ((mTruncateMode == TruncateMode.RIGHT) && !string.Empty.Equals(mFeedbackString))
                        displayText =  displayText + mFeedbackString; // GDZ Converted
				    else if( (mTruncateMode == TruncateMode.LEFT) && !string.Empty.Equals(mFeedbackString) )
                        displayText = mFeedbackString + displayText; // GDZ Converted
			    }
		    }

		    return displayText;
	    }

        protected override void _applyDimensions()
	    {
		    mTextAreaOverlayElement.SetPosition(mPixelDimensions.x,mPixelDimensions.y);
		    mTextAreaOverlayElement.SetDimensions(mPixelDimensions.z,mPixelDimensions.w);
		    mTextAreaOverlayElement.CharHeight = mPixelDimensions.w;
	    }

        internal override void _notifyDimensionsChanged()
        {
            _updateDimensions(mRelativeDimensions);
            _applyDimensions();
        }

        protected override void _notifyTextChanged()
	    {
		    // adjust width to match the width of text
		    mRelativeDimensions.z = (getTextWidth() / mParentWidget.getSize(QGuiMetricsMode.QGUI_GMM_PIXELS).x);
		    _notifyDimensionsChanged();
	    }

	    private float convertPosToIndex(Vector2 p)
	    {
            int numIndices = mCursorPositions.Count;
            float xPos = p.x;

            if (numIndices == 1) // If there is only 1 index, we return that index.
                return 0;
            else
                if (xPos < mCursorPositions[0] + mPixelDimensions.x) // If position is to the left of the left most index, return first index.
                    return 0; 
                else
                    if (xPos > mCursorPositions[numIndices - 1] + mPixelDimensions.x) // If position is to the right of the right most index, return last index.
                        return (numIndices - 1);

            // If we make it here we know that the mouse position is between the beginning and end of our index positions.

		    int index;
            for (index = 1; index < numIndices; ++index)
            {
                if (xPos < mCursorPositions[index] + mPixelDimensions.x)
                {
                    // determine if point is closer to the left or right index
                    int range = mCursorPositions[index] - mCursorPositions[index - 1];
                    if ((mCursorPositions[index] - xPos) >= (range / 2))
                        return index;
                    else
                        return index - 1;
                }
            }

            // Not supposed to be able to reach this point.
            return 0;
        }

        //new protected void Deactivate(EventArgs e)
        //{
        //    mParentWidget.Deactivate(e);
        //}

	    internal void decrementCursorIndex()
	    {
		    if( mCursorIndex > 0 ) --mCursorIndex;
		    mTextCursor.show();
            mTextCursor.setPosition(getCursorPosition(mCursorIndex), mPixelDimensions.y, QGuiMetricsMode.QGUI_GMM_PIXELS);

	    }

	    float getCursorPosition(int index)
	    {
            int numCursorPositions = mCursorPositions.Count;

            // check bounds
            if (index >= numCursorPositions)
                return (mPixelDimensions.x + mCursorPositions[numCursorPositions - 1] + mCursorOffset);
            else
                if (index < 0)
                    return (mPixelDimensions.x + mCursorPositions[0] + mCursorOffset);
            
            return (mPixelDimensions.x + mCursorPositions[index] + mCursorOffset);
	    }

	    internal int getCursorIndex()
	    {
		    return mCursorIndex;
	    }

	    float getTextWidth()
	    {
		    return getTextWidth(mText);
	    }

	    float getTextWidth(string text)
	    {
		    mCursorPositions.Clear();
		    // Store first cursor position
		    mCursorPositions.Add(0);

            if (string.Empty.Equals(text))
                return 0.0f;

		    FontManager fm = FontManager.Singleton;
            FontPtr f = (FontPtr)fm.GetByName(mFont);
		    float width = 0.0f;
		    for( int index = 0; index < text.Length; ++index )
		    {
			    if( text[index] == ' ' ) width += (f.GetGlyphAspectRatio('0') * mPixelDimensions.w);
			    else width += (f.GetGlyphAspectRatio(text[index]) * mPixelDimensions.w);
			    mCursorPositions.Add((int)width);
		    }

		    // now we know the text width in pixels, and have index positions at the start/end of each character.

		    return width;
	    }

	    internal void hideTextCursor()
	    {
		    mTextCursor.hide();
		    mTextCursorHidden = true;
	    }

	    internal void incrementCursorIndex()
	    {
		    ++mCursorIndex;
		    if( mCursorIndex >= mCursorPositions.Count )
                --mCursorIndex;
		    mTextCursor.show();
            mTextCursor.setPosition(getCursorPosition(mCursorIndex), mPixelDimensions.y, QGuiMetricsMode.QGUI_GMM_PIXELS);
        }

	    public override void setCharacterHeight(float relativeHeight)
	    {
		    // Enforce the Text Widget's dimensions to match the Actual Text Dimensions,
		    // as Text is not bounded to it's overlay element size

		    setHeight(relativeHeight);
	    }

        internal void setTruncationFeedback(string visualFeedback)
	    {
		    mFeedbackString = visualFeedback;
		    setText(mText);
	    }

	    public override void setFont(string font)
	    {
		    mFont = font;
		    _notifyTextChanged();
	    }

        public override void setHeight(float relativeHeight)
	    {
		    mRelativeDimensions.w = relativeHeight;
		    _notifyTextChanged();
	    }

        public override void setText(string text)
	    {
		    mText = text;

		    mTextAreaOverlayElement.FontName = mFont;
		    mTextAreaOverlayElement.CharHeight = mPixelDimensions.w;
		    mTextAreaOverlayElement.ColourTop = mTextTopColor;
		    mTextAreaOverlayElement.ColourBottom = mTextBotColor;
		    mTextAreaOverlayElement.Caption = _adjustWidth();
    		
		    _notifyTextChanged();
	    }

        internal void setTextCursorPosition(Vector2 p)
	    {
		    mCursorIndex = (int)convertPosToIndex(p);
            //mTextCursor.setPosition(getCursorPosition(mCursorIndex),mPixelDimensions.y,QGUI_GMM_PIXELS);
            mTextCursor.setPixelPosition(getCursorPosition(mCursorIndex), mPixelDimensions.y);
	    }

        internal void setTruncateMode(TruncateMode MODE)
	    {
		    mTruncateMode = MODE;
		    setText(mText);
	    }

        public override void show()
	    {
		    if(!mTextCursorHidden)
                mTextCursor.show();
		    base.show();
	    }

        protected void showTextCursor()
	    {
		    mTextCursor.show();
		    mTextCursorHidden = false;
	    }

	    internal void toggleTextCursorVisibility()
	    {
		    mTextCursor.toggleVisibility();
		    mTextCursorHidden = !mTextCursorHidden;
	    }

    }
}
