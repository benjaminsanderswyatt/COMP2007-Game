using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ChestRaycast : MonoBehaviour
{
    [SerializeField]
    private int rayLength = 5;
    [SerializeField]
    private LayerMask layerMaskInteract;

    private MyChestController raycastedChestObj;
    private KeyController raycastedKeyObj;

    [SerializeField] private KeyCode interactKey = KeyCode.Mouse1;

    [SerializeField] 
    private Image crosshair = null;
    private bool isCrosshairactive;
    private bool doOnce;

    private const string interactableTag = "InteractiveObject";

    private void Update()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, fwd, out hit, rayLength) && hit.collider.CompareTag(interactableTag))
        {
            if (hit.collider.gameObject.layer == 7) //chest
            {
                if (!doOnce)
                {
                    raycastedChestObj = hit.collider.gameObject.GetComponent<MyChestController>();
                    CrosshairChange(true);
                }

                isCrosshairactive = true;
                doOnce = true;

                if (Input.GetKeyDown(interactKey))
                {
                    raycastedChestObj.PlayAnimation();
                }
            }
            else if (hit.collider.CompareTag(interactableTag) && hit.collider.gameObject.layer == 8) //Key
            {
                if (!doOnce)
                {
                    raycastedKeyObj = hit.collider.gameObject.GetComponent<KeyController>();
                    CrosshairChange(true);
                }

                isCrosshairactive = true;
                doOnce = true;

                if (Input.GetKeyDown(interactKey))
                {
                    raycastedKeyObj.TakeKey();
                }
            }
            else
            {
                CrosshairChange(false);
                doOnce = false;
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
