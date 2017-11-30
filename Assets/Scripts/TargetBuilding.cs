using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Wrld;
using Wrld.Space;
using Wrld.Resources.Buildings;

public class TargetBuilding : MonoBehaviour {

    public Material highlightMaterial;
    public Vector3 mouseDownPosition;
    public WrldMap wrldMap;

    private bool isHighlighted = false;
    private Highlight currentHighlight;
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
                GUI.GetComponent<Text>().text = lat.ToString()+" "+lon.ToString();
                if (currentHighlight != null)
                {
                    ClearHighlight(currentHighlight);
                }
                Debug.Log("Requesting Highlight from API");
                Api.Instance.BuildingsApi.HighlightBuildingAtLocation(latLongAlt, highlightMaterial, OnHighlightReceived);
               
            }
        }
    }

    void OnHighlightReceived(bool success, Highlight highlight)
    {
        if (success)
        {
            Debug.Log("Highlight Successful");
            currentHighlight = highlight;
            isHighlighted = true;
        }
        Debug.Log("Highlight Unsuccessful");
    }

    void ClearHighlight(Highlight highlight)
    {
        Api.Instance.BuildingsApi.ClearHighlight(highlight);
        isHighlighted = false;
    }
}
