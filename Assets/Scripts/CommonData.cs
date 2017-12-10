using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;

namespace Invisit
{
    public class CommonData
    {

        //Common data for game controllers and scripts to use. Most are probably not needed, will trim down/add more as nessesary. 

        //public static PrefabList prefabs;
        //public static GameWorld gameWorld;
        public static GameObject mainCamera;
        public static Firebase.FirebaseApp app;
        public static Firebase.Database.FirebaseDatabase firebaseDatabase;
        public static MainGame mainGame;
        public static GameObject map;
        public static Wrld.Api wrld;
        public static GameObject firebaseDatabaseManager;
        //public static DBStruct<UserData> currentUser;

        /*
        public const string DBMapTablePath = "MapList/";
        public const string DBBonusMapTablePath = "BonusMaps/";
        public const string DBUserTablePath = "DB_Users/";
        */
    }
}
