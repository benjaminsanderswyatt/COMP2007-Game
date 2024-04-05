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
    private NpcDialogTrigger raycastedNpcObj;

    [SerializeField] private KeyCode interactKey = KeyCode.Mouse1;

    [SerializeField] 
    private Image crosshair = null;
    private bool isCrosshairactive;
    private bool doOnce;

    private const string interactableTag = "InteractiveObject";

    private void Update()
    {
        //do when player is not in dialog
        if (!DialogManager.inDialog)
        {

            RaycastHit hit;
            Vector3 fwd = transform.TransformDirection(Vector3.forward);

            if (Physics.Raycast(transform.position, fwd, out hit, rayLength) && hit.collider.CompareTag(interactableTag))
            {
                if (hit.collider.gameObject.layer == 7) //chest
                {
                    raycastedChestObj = hit.collider.gameObject.GetComponent<MyChestController>();

                    if (!doOnce)
                    {
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
                    raycastedKeyObj = hit.collider.gameObject.GetComponent<KeyController>();

                    if (!doOnce)
                    {
                        CrosshairChange(true);
                    }

                    isCrosshairactive = true;
                    doOnce = true;

                    if (Input.GetKeyDown(interactKey))
                    {
                        raycastedKeyObj.TakeKey();
                    }
                }
                else if (hit.collider.CompareTag(interactableTag) && hit.collider.gameObject.layer == 9) //NPC
                {
                    raycastedNpcObj = hit.collider.gameObject.GetComponent<NpcDialogTrigger>();

                    if (!doOnce)
                    {
                        CrosshairChange(true);
                    }

                    isCrosshairactive = true;
                    doOnce = true;

                    if (Input.GetKeyDown(interactKey))
                    {
                        raycastedNpcObj.Interact();
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
