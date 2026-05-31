using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

public class PlaneTrackingMode : MonoBehaviour, IARTrackingMode
{
    [SerializeField]
    private ARPlaneManager _planeManager;

    [SerializeField]
    private GameObject _prefabToSpawnFromPlane;

    [SerializeField]
    private ARRaycastManager _raycastManager;

    // This stores the data of what string-prefab pair to instantiate

    private readonly List<ARRaycastHit> _raycastHits = new();

    public ARTrackingMode Mode => ARTrackingMode.PlaneTracking;

    private GameObject _selectedObject;

    private int objectLayer;
    private void Start()
    {
        objectLayer = LayerMask.NameToLayer("Object");
    }
    public void Initialize()
    {
        //throw new System.NotImplementedException();
        DisableMode();

    }

    public void EnableMode()
    {
        EnhancedTouchSupport.Enable();
        _planeManager.enabled = true;
        Debug.Log("_planeManager.enabled = true;");
    }

    public void DisableMode()
    {
        EnhancedTouchSupport.Disable();
        _planeManager.enabled = false;
        Debug.Log("_planeManager.enabled = false;");

    }


    public void UpdateMode()
    {
        // Check for any active touches in the screen
        if (Touch.activeTouches.Count == 0)
        {
            // Do an early exit this frame because there's no interaction happening requiring touches
            return;
        }

        // Store the information of the first active touch in the screen
        var touch = Touch.activeTouches[0];

        if (touch.phase != TouchPhase.Began)
        {
            // We only need to detect the first point of contact
            return;
        }
        Debug.Log("[AR] Touch detected");


        if (EventSystem.current.IsPointerOverGameObject(touch.touchId))
        {
            Debug.Log("[AR] Touch blocked by UI");
            return;
        }

        if (TrySelectObject(touch.screenPosition))
        {
            Debug.Log("TrySelectObject(touch.screenPosition)");
            return; // tapped an object — don't spawn anything
        }


        // We will check if the point in the screen that we touched actually has an ARPlane
        if (_raycastManager.Raycast(
            touch.screenPosition, // cast a ray from the position of the touch
            _raycastHits, // store the data of whatever ARRaycastHit information we got
            TrackableType.PlaneWithinPolygon)) // filter whatever trackable type we want
        {
            Debug.Log("[AR] Trying to spawn object");
            // If we hit a plane, spawn it where we clicked
            var spawnedObject = Instantiate(
                _prefabToSpawnFromPlane,
                _raycastHits[0].pose.position,
                _raycastHits[0].pose.rotation);
        }
        else
        {
            Debug.Log("[AR] Raycast did not hit any plane");
        }
    }
    private bool TrySelectObject(Vector2 screenPosition)
    {
        // Cast a ray from the camera through the screen position into 3D space
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);

        if (Physics.Raycast(ray, out RaycastHit target))
        {
            GameObject tappedObject = target.collider.gameObject;

            // Ignore AR planes
            if (tappedObject.layer != objectLayer) return false;

            // Check if we tapped the currently selected object
            if (_selectedObject == tappedObject && tappedObject.layer == objectLayer)
            {
                // Tapping the selected object again deletes it
                Destroy(_selectedObject);
                _selectedObject = null;
                Debug.Log($"Deleting selected object {_selectedObject.name}");
            }
            else if(tappedObject.layer == objectLayer)
            {
                // Deselect the previous object
                DeselectObject(_selectedObject);
                // Select the new object
                _selectedObject = tappedObject;
                SelectObject(_selectedObject);
                Debug.Log($"Object selected {_selectedObject.name}");
            }
            return true;
        }
        // Tapped empty space — deselect current object
        DeselectObject(_selectedObject);
        return false;
    }
    private void SelectObject(GameObject targetObject)
    {
        Debug.Log($"SelectObject(GameObject targetObject) is TAPPED");

        float newColorIntensity = 2.5f;
        if (targetObject.TryGetComponent<EmissionColorController>(out EmissionColorController targetObjectColor))
        {
            Debug.Log($"SelectObject(GameObject targetObject) is changing color");
            targetObjectColor.intensity = newColorIntensity;
            targetObjectColor.changerColor = Color.green;
        }
        else
        {
            Debug.Log($" EmissionColorController is Null : {targetObject.name}");
            return; 
        }
    }
    private void DeselectObject(GameObject targetObject)
    {
        if (_selectedObject == null) return;

        _selectedObject.TryGetComponent<EmissionColorController>(out EmissionColorController targetObjectColor);
        if (_selectedObject == null) return;           
        else
        {
            targetObjectColor.changerColor *= 0.5f; // restore default highlight
            targetObjectColor.changerColor = Color.red;
        }

        _selectedObject = null;
    }

}
