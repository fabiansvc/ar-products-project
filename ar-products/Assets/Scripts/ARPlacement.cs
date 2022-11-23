using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARPlacement : MonoBehaviour
{
    public GameObject arObjectToSpawnMachine;

    public GameObject placementIndicator;

    public GameObject canvas;

    public Camera aRCamera;

    private GameObject product;

    private Pose placementPose;

    private ARSessionOrigin sessionOrigin;

    private ARRaycastManager aRRaycastManager;

    private List<ARRaycastHit> hits;

    private bool placementPoseIsValid = false;

    void Start()
    {
        sessionOrigin = GetComponent<ARSessionOrigin>();
        aRRaycastManager = FindObjectOfType<ARRaycastManager>();
    }

    // need to update placement indicator, placement pose and spawn

    void Update()
    {
        UpdatePlacementPose();
        UpdatePlacementIndicator();

        if (
            product == null &&
            placementPoseIsValid &&
            Input.touchCount > 0 &&
            Input.GetTouch(0).phase == TouchPhase.Began
        )
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
        if (product == null && placementPoseIsValid)
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
        product =
            Instantiate(arObjectToSpawnMachine,
            placementPose.position,
            placementPose.rotation);
    }
}
