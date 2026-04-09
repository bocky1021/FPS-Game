using UnityEngine;

public class DestroyEffect : MonoBehaviour
{
    public float destoryTime = 1.5f;

    private void Start()
    {
        Destroy(gameObject, destoryTime);   
    }
}
