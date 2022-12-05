using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARPlacement : MonoBehaviour
{
    public GameObject chairPrefab;
    public GameObject placementIndicator;
    public GameObject canvas;
    public Camera aRCamera;
    private Pose placementPose;
    private ARSessionOrigin sessionOrigin;
    private ARRaycastManager aRRaycastManager;
    private List<ARRaycastHit> hits;
    private string productSelected;
    private bool placementPoseIsValid;
    private bool isButtonSelectProduct;
    private GameObject prefabProductSelected;

    public void Start()
    {
        sessionOrigin = GetComponent<ARSessionOrigin>();
        aRRaycastManager = FindObjectOfType<ARRaycastManager>();
        placementPoseIsValid = false;
    }

    public void Update()
    {  
        productSelected = Products.GetInstance().GetProductSelected();

        if(productSelected != "None")
        {
            UpdatePlacementPose();
            UpdatePlacementIndicator();   
        }
        
        if (Products.GetInstance().GetProduct() == null && placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            ARPlaceObject();
        }
    }

    void UpdatePlacementPose()
    {
        var screenCenter =
            aRCamera.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        aRRaycastManager
            .Raycast(screenCenter, hits, TrackableType.PlaneWithinPolygon);

        placementPoseIsValid = hits.Count > 0;

        if (placementPoseIsValid)
        {
            placementPose = hits[0].pose;
        }
    }

    void UpdatePlacementIndicator()
    {
        if (Products.GetInstance().GetProduct() == null && placementPoseIsValid)
        {
            var cameraForward = aRCamera.transform.forward;
            var cameraBearing =
                new Vector3(cameraForward.x, 0, cameraForward.z).normalized;

            placementPose.rotation = Quaternion.LookRotation(cameraBearing);
            placementIndicator.SetActive(true);
            placementIndicator
                .transform
                .SetPositionAndRotation(placementPose.position,
                placementPose.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }

    void ARPlaceObject()
    {
        setInstantiatePrefab(GetPrefabProductSelected());
    }

    private void setInstantiatePrefab(GameObject prefab)
    {
        Products.GetInstance().SetProduct(Instantiate(prefab,
            placementPose.position,
            placementPose.rotation));
    }

    private GameObject GetPrefabProductSelected()
    {
        switch(productSelected) 
        {
            case "Chair":
                prefabProductSelected = chairPrefab;
                break;
            case "Table":
                prefabProductSelected = chairPrefab;
                break;
            case "Watch":
                prefabProductSelected = chairPrefab;
            break;
        }
        
        return prefabProductSelected;
    }
}
