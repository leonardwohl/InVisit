using UnityEngine;
using System;
using System.Collections;

namespace Invisit.States
{
    public class State
    {

        //State class for managing game states

        public StateManager manager;
        protected GameObject gui;

        public virtual void Initialize() { }

        public virtual ExitValue Cleanup() { return null; }

        public virtual void Suspend() { }

        public virtual void Resume(ExitValue result) { }
       
        public virtual void Update() { }

        public virtual void FixedUpdate() { }

        public virtual void OnGUI() { }

        public virtual void HandleUIEvents(GameObject source, object eventData) { }
       
    }

    public class ExitValue
    {
        public ExitValue(Type sourceState, object data = null)
        {
            this.data = data;
            this.sourceState = sourceState;
        }

        public Type sourceState;
        public object data;
    }
}