using System;
using System.Collections.Generic;
using System.Text;
using Mogre;
using MOIS;

namespace MQuickGUI
{
    /** Represents a button with several states.
        @remarks
        A state is defined as an image and text. For a small
        example, a checkbox would be the same as a 2 state button.
        For each state, you need to define its original, mouse
        over, and mouse down state, and any text.
        @note
        A ButtonStateEvent was created for this widget.
        @note
        NStateButton must be created by a Window widget.
    */
    public class NStateButton : Button
    {
		protected List<State> mStates;

		protected State mCurrentState;

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
                overlayContainer associates the internal OverlayElement with a specified zOrder.
            @param
                ParentWidget parent widget which created this widget.
        */
        public NStateButton(string name, Vector4 dimensions, QGuiMetricsMode positionMode, QGuiMetricsMode sizeMode, OverlayContainer overlayContainer, Widget ParentWidget)
            : base(name, dimensions, positionMode, sizeMode, "", overlayContainer, ParentWidget)
	    {
		    mWidgetType = Widget.WidgetType.QGUI_TYPE_NSTATEBUTTON;

            mStates = new List<State>();

            this.OnMouseButtonUp += new MouseButtonUpEventHandler(NStateButton_OnMouseButtonUp);
	    }

	    ~NStateButton()
	    {
		    clearStates();
	    }

        public void addState(string name, string material)
        {
            addState(name, material, "");
        }

        /**
        * Adds (and creates) a state to the button.  If it is the first state, the state will be applied.
        */
        public void addState(string name, string material, string text)
	    {
		    State s = new State(name,material,text);
		    mStates.Add(s);

		    // The widget has an image when the first state gets added
		    if(mStates.Count == 1)
		    {
			    setCurrentState(mStates[0]);
		    }
	    }

        /**
        * Removes (and destroys) all states.  Widget has no appearance after this, since no states are defined.
        */
        public void clearStates()
	    {
		    //std::vector<State*>::iterator it;
		    //for( it = mStates.begin(); it != mStates.end(); ++it )
            //foreach(State i in mStates)
		    //{
			    //delete (*it);
		    //}
		    mStates.Clear();
	    }

        /**
        * Returns the current state of the widget, if defined.
        */
        public State getCurrentState()
	    {
		    return mCurrentState;
	    }

        /**
        * Returns the index of the state provided.
        */
        public int getIndexOfState(State s)
	    {
            int index = -1;
            //std::vector<State*>::iterator it;
            //for( it = mStates.begin(); it != mStates.end(); ++it )
            //{
            //    if( (*it)->getName() == s->getName() ) return index;
            //    ++index;
            //}
            try
            {
                index = mStates.IndexOf(s); // GDZ
            }
            catch  {
            }

		    return index;
	    }

        /**
        * Returns the next state in the list of defined states.
        */
        public State getNextState()
	    {
		    int index = getIndexOfState(mCurrentState);
    		
		    int nextStateIndex = index + 1;
    		
		    if( nextStateIndex >= mStates.Count )
                nextStateIndex =  0;

		    return mStates[nextStateIndex];
	    }

        /**
        * Returns the previous state in the list of defined states.
        */
        public State getPreviousState()
	    {
		    int index = getIndexOfState(mCurrentState);

		    int prevStateIndex = index - 1;
    		
		    if( prevStateIndex < 0 )
                prevStateIndex = mStates.Count - 1;

		    return mStates[prevStateIndex];
	    }

        /**
        * Returns the state corresponding to the index in the list of defined states.
        * No exception is thrown if index is out of bounds.
        */
        public State getState(int index)
	    {
		    if( mStates.Count < index )
                return null;
		    return mStates[index];
	    }

        /**
        * Returns the state with the name provided.
        * No exception is state does not exist.
        */
        public State getState(string name)
	    {
		    //std::vector<State*>::iterator it;
		    //for( it = mStates.begin(); it != mStates.end(); ++it )
            foreach(State i in mStates)
		    {
			    if( i.getName().Equals(name) )
                    return i;
		    }

		    return null;
	    }

        public delegate void StateChangedDelegate(object source, WidgetEventArgs e);
        public event StateChangedDelegate OnStateChanged;

        internal void StateChanged(WidgetEventArgs e)
        {
            if (!mEnabled)
                return;

            if (OnStateChanged != null)
            {
                OnStateChanged(this, e);
            }
        }

        /**
        * Manually setting the current State of the widget. onStateChanged will be called.
        */
	    public void setCurrentState(State s)
	    {
		    // Make sure s is a state within this State Button
		    if(getState(s.getName()) == null)
                return;

		    mCurrentState = s;
            setMaterial(mCurrentState.getMaterial());
		    setText(mCurrentState.getText());

            MaterialManager mm = MaterialManager.Singleton;
            mOverMaterialExists = mm.ResourceExists(mWidgetMaterial + ".over");
            mDownMaterialExists = mm.ResourceExists(mWidgetMaterial+".down");

		    WidgetEventArgs e = new WidgetEventArgs(this);
		    e.handled = false;
		    StateChanged(e);
	    }

	    void setCurrentState(int index)
	    {
		    State s = getState(index);
		    if( s == null)
                return;
		    setCurrentState(s);
	    }

	    void setCurrentState(string name)
	    {
		    State s = getState(name);
		    if( s == null ) return;
		    setCurrentState(s);
	    }

        /**
        * Advancing the state of the Widget.(circular) onStateChanged will be called.
        */
        void toggleNextState()
	    {
		    int index = getIndexOfState(mCurrentState);
    		
		    int nextStateIndex = index + 1;
    		
		    if( nextStateIndex >= mStates.Count )
                nextStateIndex =  0;

		    setCurrentState(mStates[nextStateIndex]);
	    }

        /**
        * Setting State to Previously defined State.(circular) onStateChanged will be called.
        */
        void togglePreviousState()
	    {
		    int index = getIndexOfState(mCurrentState);

		    int prevStateIndex = index - 1;
    		
		    if( prevStateIndex < 0 )
                prevStateIndex = mStates.Count - 1;

		    setCurrentState(mStates[prevStateIndex]);
	    }

        void NStateButton_OnMouseButtonUp(object source, MouseEventArgs e)
        {
            if (!mEnabled)
                return;

            e.widget = this;

            if (e.button == MouseButtonID.MB_Left)
                toggleNextState();
            else if (e.button == MouseButtonID.MB_Right)
                togglePreviousState();
        }


        /** A Particular Image and Text representing a Logical State.
            @note
            A State must be created by a NStateButton widget.
        */
        public class State
		{

		    public State(string name, string material, string text)
			{
			  mName = name;
			  mMaterial = material;
			  mText = text;
            }

            public string getMaterial() { return mMaterial; }
			public string getName() { return mName; }
            public string getText() { return mText; }
			
		    protected string mName;
			protected string mMaterial;
			protected string mText;
		};
    }
}
