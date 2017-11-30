using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Wrld;
using Wrld.Space;
using Wrld.Resources.Buildings;
using Invisit;
namespace Invisit {
    public class AddBuilding : MonoBehaviour {

        public GameObject geoTransform;

        public Vector3 mouseDownPosition;
        

        public GameObject GUI;


        void OnEnable()
        {
            // var cameraLocation = LatLong.FromDegrees(37.795641, -122.404173);
            // Api.Instance.CameraApi.MoveTo(cameraLocation, distanceFromInterest: 400, headingDegrees: 0, tiltDegrees: 45);
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                mouseDownPosition = Input.mousePosition;
            }
            if (Input.GetMouseButtonUp(0) && Vector3.Distance(mouseDownPosition, Input.mousePosition) < 5.0f)
            {
                Debug.Log("Mouse Up");
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    var viewportPoint = Camera.main.WorldToViewportPoint(hit.point);
                    var latLongAlt = Api.Instance.CameraApi.ViewportToGeographicPoint(viewportPoint, Camera.main);
                    double lat = latLongAlt.GetLatLong().GetLatitude();
                    double lon = latLongAlt.GetLatLong().GetLongitude();
                    GUI.GetComponent<Text>().text = lat.ToString() + " " + lon.ToString();
                    PlaceBuilding(latLongAlt);


                }
            }
        }

        void PlaceBuilding(LatLongAltitude location)
        {
            CommonData.firebaseDatabaseManager.GetComponent<FirebaseDatabaseManager>().AddBuildingLocation(location.GetLatLong());
            return;
        }

    }
}