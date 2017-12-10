using UnityEngine;
using UnityEditor;

namespace Invisit.States
{
    public class MapView : State //State for holding the gameworld map
    {
        GameObject map;
        GameObject gameController;

        public MapView(StateManager manager, GameObject gui)
        {
            this.manager = manager;
            this.gui = gui;
        }

        public override ExitValue Cleanup()
        {
            map.GetComponent<WrldMap>().enabled = false;
            return base.Cleanup();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
        }

        public override void HandleUIEvents(GameObject source, object eventData)
        {
            base.HandleUIEvents(source, eventData);
        }

        public override void Initialize()
        {
            Debug.Log("Test");
            map = GameObject.FindGameObjectWithTag("WRLDMap");
            map.GetComponent<WrldMap>().enabled = true;
            gameController = GameObject.FindGameObjectWithTag("GameController");
            gameController.GetComponent<FirebaseDatabaseManager>().enabled = true;
            base.Initialize();
        }

        public override void OnGUI()
        {
            base.OnGUI();
        }

        public override void Resume(ExitValue result)
        {
            base.Resume(result);
        }

        public override void Suspend()
        {
            base.Suspend();
        }

        public override void Update()
        {
            base.Update();
        }
    }
}