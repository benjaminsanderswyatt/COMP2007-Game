using UnityEngine;

public class KeyController : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField]
    private GameObject keyObj;
    [SerializeField]
    private GameObject lightObj;

    private KeySystem keySystem;

    private void Start()
    {
        keyObj.SetActive(true);
        lightObj.SetActive(true);

        keySystem = FindObjectOfType<KeySystem>();
    }


    public void TakeKey()
    {
        keyObj.SetActive(false);
        lightObj.SetActive(false);
        keySystem.AddKey();
    }
}
