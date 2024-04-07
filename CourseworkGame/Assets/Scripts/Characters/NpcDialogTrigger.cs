using UnityEngine;

public class NpcDialogTrigger : MonoBehaviour
{
    public bool IsTalkingToPlayer = false;

    public NpcDialog dialog;

    public NpcDialog dialogAfterCondition;

    private KeySystem keySystem;
    private DoorController doorController;

    public void Start()
    {
        keySystem = GameObject.FindObjectOfType<KeySystem>();
        doorController = GameObject.FindObjectOfType<DoorController>();
    }

    public void Interact()
    {
        var sendDialog = dialog;

        if (keySystem.hasKey && dialog.name == "Gage")
        {
            sendDialog = dialogAfterCondition;
            doorController.keyDelivered = true;
        }

        FindObjectOfType<DialogManager>().StartDialog(sendDialog);
        IsTalkingToPlayer = true;
    }

    private void Update()
    {
        if (IsTalkingToPlayer && !DialogManager.inDialog)
        {
            IsTalkingToPlayer = false;
        }
    }


}
