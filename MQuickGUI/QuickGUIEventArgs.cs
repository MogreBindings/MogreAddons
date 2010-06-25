using System;
using System.Collections.Generic;
using System.Text;
using Mogre;
using MOIS;

namespace MQuickGUI
{
/** All supported Events used in QuickGUI.
	@remarks
	Every widget supports functions to fire the events listed below.  
	These Events will go to the widget's corresponding event handlers, 
	and call user defined event handlers if they have been created.
	Events iterate through their child or parent widgets, until it
	is handled, or there are no more children/parents.
	@note
	Events are fired via the GUIManager, which interacts with injected
	inputs.
    */
	
    /** Basic EventArgs
	*/
	public class EventArgs
	{

        // handlers should set this to true if they handled the event.
        public bool handled;

        public EventArgs()
        {
        }
	
	}

	/** Widget EventArgs
	*/
	public class WidgetEventArgs : EventArgs
	{
		//pointer to a Widget object of relevance to the event.
		public Widget widget;		
        
        public WidgetEventArgs(Widget w)
        {
            widget = w;
        }
	}

	/** Mouse EventArgs
	*/
	public class MouseEventArgs : WidgetEventArgs
	{
	    public MouseEventArgs(Widget w) : base(w) {}

		// holds current mouse position. (pixels)
        public Vector2 position;
		// holds variation of mouse position from last mouse input
        public Vector2 moveDelta;		
		// holds the mouse button that was down for the given event
		public MouseButtonID	button;
		// holds the amount the scroll wheel has changed.
        public float wheelChange;	
	}

	/** Key EventArgs
	*/
	public class KeyEventArgs : WidgetEventArgs
	{
	    public KeyEventArgs(Widget w) : base(w) {}

		// char codepoint for the key (only used for Character inputs).
		public char		codepoint;		
		// Scan code of key that caused event (only used for key up & down inputs.
        public KeyCode scancode;		
	}

	/** ButtonState EventArgs
	*/
	public class ButtonStateEventArgs : WidgetEventArgs
	{
	    public ButtonStateEventArgs(Widget w) : base(w) {}

		// index used to retreive the NStateButton's current state
		int	currentState = 0;
	}

    /** Time EventArgs
    */
    public class TimeEventArgs : EventArgs
    {
        //time elapsed.
        public float time;

        public TimeEventArgs(float f)
        {
            time = f;
        }
    }
}
