using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Wrld;
using Wrld.Space;
using Wrld.Resources.Buildings;

namespace Invisit {

    public class AddBuilding : MonoBehaviour {

        public GameObject geoTransform;

        public Vector3 mouseDownPosition;
        

        public GameObject GUI;


        void OnEnable()
        {
            
        }

        void Update()
        {
            //Get mopuse input

            if (Input.GetMouseButtonDown(0))
            {
                mouseDownPosition = Input.mousePosition;
            }
            if (Input.GetMouseButtonUp(0) && Vector3.Distance(mouseDownPosition, Input.mousePosition) < 5.0f) //check if its a click or drag
            {
                Debug.Log("Mouse Up");
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                RaycastHit hit;

                if (Physics.Raycast(ray, out hit)) //raycast uses physics colliders, so terrain physics must be enabled
                {
                    //places building at coords from mouse click
                    var viewportPoint = Camera.main.WorldToViewportPoint(hit.point);
                    var latLongAlt = Api.Instance.CameraApi.ViewportToGeographicPoint(viewportPoint, Camera.main);
                    double lat = latLongAlt.GetLatLong().GetLatitude();
                    double lon = latLongAlt.GetLatLong().GetLongitude();
                    GUI.GetComponent<Text>().text = lat.ToString() + " " + lon.ToString();
                    PlaceBuilding(latLongAlt);


                }
            }
        }

        void PlaceBuilding(LatLongAltitude location) //adds building to db cache
        {
            CommonData.firebaseDatabaseManager.GetComponent<FirebaseDatabaseManager>().AddBuildingLocation(location.GetLatLong());
            return;
        }

    }
}