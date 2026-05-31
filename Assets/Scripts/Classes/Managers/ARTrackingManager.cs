using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ARTrackingManager : Singleton<ARTrackingManager>
{
    public IARTrackingMode CurrentMode => _currentTrackingMode;

    [SerializeField]
    private List<MonoBehaviour> _trackingReferences; 

    // Collection of all available tracking modes
    private readonly Dictionary<ARTrackingMode, IARTrackingMode> _trackingModes = new();

    // What is the current IARTrackingMode active
    private IARTrackingMode _currentTrackingMode;

    [SerializeField] PlaneTrackingMode _planeTrackingMode;

    protected override void Awake()
    {
        base.Awake();
        RegisterModes();
    }

    private void Update()
    {
        // Drive the active mode every frame so it can detect touches and other input
        _planeTrackingMode.UpdateMode(); //Brute force method

    }


    public void SetMode(ARTrackingMode mode)
    {
        // If our current mode is already the targetMode, no need to switch to same mode
        if (_currentTrackingMode != null && _currentTrackingMode.Mode == mode)
        {
            return;
        }

        // This code will only be reached if target mode is a different mode
        // Disable the current mode
        _currentTrackingMode?.DisableMode();

        // Enable the target mode
        if (_trackingModes.TryGetValue(mode, out var newMode))
        {
            _currentTrackingMode = newMode;
            _currentTrackingMode.EnableMode();
        }
    }

    private void RegisterModes()
    {
        foreach (var trackingReference in _trackingReferences)
        {
            // since _trackingReferences are MonoBehaviours we only need IARTrackingMode
            if (trackingReference is not IARTrackingMode trackingMode)
            {
                // Ignore others
                continue;
            }

            // Register the mode 
            _trackingModes.TryAdd(trackingMode.Mode, trackingMode);
            Debug.Log($"Registered {trackingMode.Mode}");
        }
    }
    private void OnPlaneTrackingSelected()
    {
        Debug.Log("[UI] Plane Tracking button pressed");
        ARTrackingManager.Instance.SetMode(ARTrackingMode.PlaneTracking);
    }

}
