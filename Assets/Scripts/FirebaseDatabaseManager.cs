using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wrld.Space;
using Firebase;

namespace Invisit
{
    public class FirebaseDatabaseManager : MonoBehaviour
    {

        DBTable<SerializableData.LatLonS> database;
        public GameObject geoTransform;

        // Use this for initialization
        void Start()
        {
            //Creates database manager and loads gameObjects into gameworld

            database = new DBTable<SerializableData.LatLonS>("BuildingLocations", CommonData.app);
            CommonData.firebaseDatabaseManager = this.gameObject;
            
            foreach (KeyValuePair<string, DBObj<SerializableData.LatLonS>> pair in database.data)
            {
                
                AddBuildingObject(SerializableData.DeserializeLatLon(database.data[pair.Key].data));
                
            }
            
        }

        // Update is called once per frame
        void Update()
        {
            //Checks for new buildings in the local DB cache, that havent been synced yet. Adds them to the gameworld, thens syncs them with the database. Should probably switch the order for future proofing.

            foreach (KeyValuePair<string, SerializableData.LatLonS> pair in database.newData)
            {
                if (!database.data.ContainsKey(pair.Key))
                {
                    AddBuildingObject(SerializableData.DeserializeLatLon(database.newData[pair.Key]));
                }
            }
            
            database.ApplyRemoteChanges();
            database.PushData();
        }

        public void AddBuildingLocation(LatLong loc) //Adds LatLong to database cache, then builds it in the gameworld
        {
            database.Add(database.GetUniqueKey(), SerializableData.SerializeLatLon(loc));
            AddBuildingObject(loc);
        }

        void AddBuildingObject(LatLong loc) //builds building into the gameworld
        {
            GameObject obj = Instantiate(geoTransform);
            obj.GetComponent<GeographicTransform>().SetPosition(loc);
        }
    }
}