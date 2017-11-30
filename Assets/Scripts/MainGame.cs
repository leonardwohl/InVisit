using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Unity.Editor;

namespace Invisit
{
    public class MainGame : MonoBehaviour
    {
        public States.StateManager stateManager = new States.StateManager();
        private float currentFrameTime, lastFrameTime;

        // Use this for initialization
        void Start()
        {
            Screen.SetResolution(Screen.width / 2, Screen.height / 2, true);
            InitializeFirebaseAndStart();
        }

        void InitializeFirebaseAndStart()
        {
            Firebase.DependencyStatus dependencyStatus = Firebase.FirebaseApp.CheckDependencies();

            if (dependencyStatus != Firebase.DependencyStatus.Available)
            {
                Firebase.FirebaseApp.FixDependenciesAsync().ContinueWith(task =>
                {
                    dependencyStatus = Firebase.FirebaseApp.CheckDependencies();
                    if (dependencyStatus == Firebase.DependencyStatus.Available)
                    {
                        StartGame();
                    }
                    else
                    {
                        Debug.LogError(
                            "Could not resolve all Firebase dependencies: " + dependencyStatus);
                        Application.Quit();
                    }
                });
            }
            else
            {
                StartGame();
            }
        }

        void StartGame()
        {

            Debug.Log("Starting Game");
            CommonData.mainGame = this;
            CommonData.mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            CommonData.map = GameObject.FindGameObjectWithTag("WRLDMap");
            CommonData.app = Firebase.FirebaseApp.Create();
            CommonData.app.SetEditorDatabaseUrl("https://invisit-gamescience.firebaseio.com/");
            CommonData.firebaseDatabase = Firebase.Database.FirebaseDatabase.GetInstance(CommonData.app);
            stateManager.PushState(new States.MapView(stateManager, GameObject.FindGameObjectWithTag("GUI")));
            
        }

        // Update is called once per frame
        void Update()
        {
            lastFrameTime = currentFrameTime;
            currentFrameTime = Time.realtimeSinceStartup;
            stateManager.Update();

        }


    }
}
