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

        public void AddBuildingLocation(LatLong loc)
        {
            database.Add(database.GetUniqueKey(), SerializableData.SerializeLatLon(loc));
            AddBuildingObject(loc);
        }

        void AddBuildingObject(LatLong loc)
        {
            GameObject obj = Instantiate(geoTransform);
            obj.GetComponent<GeographicTransform>().SetPosition(loc);
        }
    }
}