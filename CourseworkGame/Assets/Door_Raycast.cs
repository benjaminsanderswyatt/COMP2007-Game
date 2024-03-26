using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door_Raycast : MonoBehaviour
{
    [SerializeField]
    private int rayLength = 5;
    [SerializeField]
    private LayerMask layerMaskInteract;
    [SerializeField]
    private string excludeLayerName = null;

    private MyChestController raycastedObj;

    [SerializeField] private KeyCode openChestKey = KeyCode.Mouse1;

    [SerializeField]
    private Image crosshair = null;
    private bool isCrosshairactive;
    private bool doOnce;

    private const string interactableTag = "InteractiveObject";

    private void Update()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        int mask = 1 << LayerMask.NameToLayer(excludeLayerName) | layerMaskInteract.value;

        if (Physics.Raycast(transform.position, fwd, out hit, rayLength, mask))
        {
            if (hit.collider.CompareTag(interactableTag))
            {
                if (!doOnce)
                {
                    raycastedObj = hit.collider.gameObject.GetComponent<MyChestController>();
                    CrosshairChange(true);
                }

                isCrosshairactive = true;
                doOnce = true;

                if (Input.GetKeyDown(openChestKey))
                {
                    raycastedObj.PlayAnimation();
                }
            }




        }
        else if (isCrosshairactive)
        {
            CrosshairChange(false);
            doOnce = false;
        }

    }

    void CrosshairChange(bool on)
    {
        if (on && !doOnce)
        {
            crosshair.color = Color.red;
        }
        else
        {
            crosshair.color = Color.white;
            isCrosshairactive = false;
        }
    }

}
