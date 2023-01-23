using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARPlacement : MonoBehaviour
{
    public GameObject chesterPrefab;
    public GameObject headPhonePrefab;
    public GameObject legoPrefab;
    public GameObject placementAim;
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
            UpdatePlacementAim();  
            
            if (Products.GetInstance().GetProduct() == null && placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                ARPlaceObject(GetPrefabProductSelected(productSelected));
            } 
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

    void UpdatePlacementAim()
    {
        if (Products.GetInstance().GetProduct() == null && placementPoseIsValid)
        {
            var cameraForward = aRCamera.transform.forward;
            var cameraBearing =
                new Vector3(cameraForward.x, 0, cameraForward.z).normalized;

            placementPose.rotation = Quaternion.LookRotation(cameraBearing);
            placementAim.SetActive(true);
            placementAim
                .transform
                .SetPositionAndRotation(placementPose.position,
                placementPose.rotation);
        }
        else
        {
            placementAim.SetActive(false);
        }
    }

    void ARPlaceObject(GameObject prefab)
    {
        Products.GetInstance().SetProduct(Instantiate(prefab,
            placementPose.position,
            placementPose.rotation));
    }

    private GameObject GetPrefabProductSelected(string _productSelected)
    {
        switch(_productSelected) 
        {
            case "Chester":
                prefabProductSelected = chesterPrefab;
                break;
            case "Headphone":
                prefabProductSelected = headPhonePrefab;
                break;
            case "Lego":
                prefabProductSelected = legoPrefab;
            break;
            default:
                prefabProductSelected = null;
            break;
        }
        
        return prefabProductSelected;
    }
}
