using UnityEngine;

public class DataTransferToEndScreen : MonoBehaviour
{
    public float time;
    public bool state; //false = gameover   true = win 

    public void SetData(bool state, float time)
    {
        this.state = state;
        this.time = time;
    }

}
