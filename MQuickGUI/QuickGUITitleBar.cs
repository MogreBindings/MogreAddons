using System;
using System.Collections.Generic;
using System.Text;
using Mogre;

namespace MQuickGUI
{
    public class TitleBar : Label
    {
        protected Button mCloseButton;

        public TitleBar(string name, Vector4 dimensions, QGuiMetricsMode positionMode, QGuiMetricsMode sizeMode, string material, OverlayContainer overlayContainer, Widget ParentWidget) : base(name,dimensions,positionMode,sizeMode,material,overlayContainer,ParentWidget)
	    {
		    mWidgetType = Widget.WidgetType.QGUI_TYPE_TITLEBAR;

		    setCharacterHeight(0.8f);
		    alignText(GuiHorizontalAlignment.GHA_LEFT, GuiVerticalAlignment.GVA_CENTER);

		    // Create CloseButton
		    // Button has same height as width - Make the button slightly smaller that the titlebar height
		    float buttonHeight = 0.8f;
		    float buttonPixelHeight = 0.8f * mPixelDimensions.w;
            float verticalPixelSpace = 0.1f * mPixelDimensions.w;
            float buttonWidth = buttonPixelHeight / mPixelDimensions.z;
            float horizontalPixelSpace = verticalPixelSpace / mPixelDimensions.z;

		    Vector4 cbDimensions = new Vector4((1 - (buttonWidth + horizontalPixelSpace)),0.1f,buttonWidth,buttonHeight);
		    mCloseButton = new Button(mInstanceName+".CloseButton",cbDimensions, positionMode, sizeMode, mWidgetMaterial+".button",mChildrenContainer,this);
            //Reemplazo por Manejo de Eventos //GDZ
            mCloseButton.OnMouseClicked += new MouseClickedEventHandler(mCloseButton_OnMouseClicked);
            mCloseButton.OnMouseButtonUp += new MouseButtonUpEventHandler(mCloseButton_OnMouseButtonUp);
		    mCloseButton.setZOrderOffset(1);
		    _addChildWidget(mCloseButton);
	    }

        void mCloseButton_OnMouseButtonUp(object source, MouseEventArgs e)
        {
            mParentWidget.hide();
        }

        void mCloseButton_OnMouseClicked(object source, MouseEventArgs e)
        {
            mParentWidget.hide();
        }

	    Button getCloseButton()
	    {
		    return mCloseButton;
	    }

	    public void hideCloseButton()
	    {
		    mCloseButton.hide();
	    }

        public void showCloseButton()
	    {
		    mCloseButton.show();
	    }



    }
}
