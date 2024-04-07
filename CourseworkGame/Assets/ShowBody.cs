using UnityEngine;

public class ShowBody : MonoBehaviour
{
    public SkinnedMeshRenderer[] bodyParts;

    private bool isHidden = true;

    void Update()
    {
        if (SwitchCamera.isFirstPerson)
        {
            //hide
            if (!isHidden)
            {
                HideTheBody();
                isHidden = true;
            }
        }
        else
        {
            //show
            if (isHidden)
            {
                ShowTheBody();
                isHidden = false;
            }
        }
    }

    private void ShowTheBody()
    {
        foreach (var part in bodyParts)
        {
            part.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
            part.receiveShadows = true;
        }
    }

    private void HideTheBody()
    {
        foreach (var part in bodyParts)
        {
            part.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
            part.receiveShadows = false;
        }
    }


}
