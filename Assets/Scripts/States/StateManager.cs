using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Invisit.States
{
    public class StateManager
    {
        Stack<State> states;

        public StateManager()
        {
            states = new Stack<State>();
            states.Push(new State());
        }

        public State GetCurrentState()
        {
            return states.Peek();
        }

        public void PushState(State pState)
        {
            pState.manager = this;
            GetCurrentState().Suspend();
            states.Push(pState);
            pState.Initialize();
        
        }

        public void PopState()
        {
            ExitValue exitVal = GetCurrentState().Cleanup();
            states.Pop();
            GetCurrentState().Resume(exitVal);
        }

        public void ClearStack(State pState)
        {
            while (states.Count > 1)
            {
                PopState();
            }
            SwapState(pState);
        }

        public void SwapState(State pState)
        {
            pState.manager = this;
            GetCurrentState().Cleanup();
            states.Pop();
            states.Push(pState);
            GetCurrentState().Initialize();
        }

        public void Update()
        {
            GetCurrentState().Update();
        }
        public void FixedUpdate()
        {
            GetCurrentState().FixedUpdate();
        }
        public void OnGUI()
        {
            GetCurrentState().OnGUI();
        }
        public void HandleUIEvent(GameObject source, object eventData)
        {
            GetCurrentState().HandleUIEvents(source, eventData);
        }
    }
}
