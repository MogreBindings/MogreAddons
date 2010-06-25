using System;
using System.Collections.Generic;
using System.Text;
using Mogre;

namespace MQuickGUI
{
    public class Label : Widget
    {
		// Default Label material, displayed in its original state.
        protected string    mMaterial;
        protected Text      mTextWidget;

        public Label(string name, Vector4 dimensions, QGuiMetricsMode positionMode, QGuiMetricsMode sizeMode, string material, OverlayContainer overlayContainer, Widget ParentWidget)
            : base(name, dimensions, positionMode, sizeMode, material, overlayContainer, ParentWidget)
	    {
		    mWidgetType = Widget.WidgetType.QGUI_TYPE_LABEL;

		    mOverlayElement = createPanelOverlayElement(mInstanceName+".Background",mPixelDimensions,"");
		    mOverlayContainer.AddChild(mOverlayElement);
		    mOverlayElement.Show();
            setMaterial(mWidgetMaterial);

		    mCharacterHeight = 0.8f;
		    mTextWidget = new Text(mInstanceName+".Text", new Vector3(0,0,mCharacterHeight), QGuiMetricsMode.QGUI_GMM_RELATIVE, QGuiMetricsMode.QGUI_GMM_RELATIVE, mChildrenContainer, this);
		    mTextWidget.setZOrderOffset(1,false);
		    _addChildWidget(mTextWidget);

		    mHorizontalAlignment = GuiHorizontalAlignment.GHA_CENTER;
		    mVerticalAlignment = GuiVerticalAlignment.GVA_CENTER;

            OnActivate += new ActivateEventHandler(Label_OnActivate);
	    }

        void Label_OnActivate(object source, EventArgs e)
        {
            if (!mEnabled)
                return;

            mTextWidget.Activate(e);
        }

        protected override void _notifyTextChanged()
	    {
		    mTextWidget.setFont(mFont);
		    mTextWidget.setCharacterHeight(mCharacterHeight);
		    mTextWidget.setTextColor(mTextTopColor,mTextBotColor);
		    mTextWidget.setText(mText);

		    alignText(mHorizontalAlignment,mVerticalAlignment);
	    }

        public void alignText(GuiHorizontalAlignment ha, GuiVerticalAlignment va)
	    {
		    mHorizontalAlignment = ha;
		    mVerticalAlignment = va;

            Vector2 relativeLabelPos = mTextWidget.getPosition(QGuiMetricsMode.QGUI_GMM_RELATIVE);
            Vector2 labelPos = mTextWidget.getPosition(QGuiMetricsMode.QGUI_GMM_ABSOLUTE);
            Vector2 labelSize = mTextWidget.getSize(QGuiMetricsMode.QGUI_GMM_ABSOLUTE);

		    if( mHorizontalAlignment == GuiHorizontalAlignment.GHA_LEFT) 
		    {
			    // We should add a 5 pixel buffer
			    float buffer = 5.0f / mPixelDimensions.z;
			    mTextWidget.setPosition(buffer, relativeLabelPos.y);
		    }
		    else if( mHorizontalAlignment == GuiHorizontalAlignment.GHA_CENTER )
		    {
			    mTextWidget.setPosition(((mAbsoluteDimensions.z / 2) - (labelSize.x / 2)) / mAbsoluteDimensions.z, relativeLabelPos.y);
		    }
		    else if( mHorizontalAlignment == GuiHorizontalAlignment.GHA_RIGHT )
		    {
			    // We should add a 5 pixel buffer
			    float buffer = 5.0f / mPixelDimensions.z;
			    mTextWidget.setPosition(((mAbsoluteDimensions.z) - (labelSize.x) - buffer) / mAbsoluteDimensions.z, relativeLabelPos.y);
		    }

		    // Make sure to update the position, in case alignment has moved it
            relativeLabelPos = mTextWidget.getPosition(QGuiMetricsMode.QGUI_GMM_RELATIVE);

		    if( mVerticalAlignment == GuiVerticalAlignment.GVA_TOP )
                mTextWidget.setPosition(relativeLabelPos.x,0.0f);
		    else if( mVerticalAlignment == GuiVerticalAlignment.GVA_CENTER )
		    {
                mTextWidget.setPosition(relativeLabelPos.x,((mAbsoluteDimensions.w / 2) - (labelSize.y / 2)) / mAbsoluteDimensions.w);
		    }
		    else if( mVerticalAlignment == GuiVerticalAlignment.GVA_BOTTOM )
		    {
			    mTextWidget.setPosition(relativeLabelPos.x,((mAbsoluteDimensions.w) - (labelSize.y)) / mAbsoluteDimensions.w);
		    }
	    }

	    public Text getTextWidget()
	    {
		    return mTextWidget;
	    }
    }
}
