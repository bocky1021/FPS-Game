using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    public float rotSpeed = 5f;
    float mx = 0;

    private void Update()
    {
        if (Input.GetMouseButton(2))
        {
            float mouse_X = Input.GetAxisRaw("Mouse X");
            mx += mouse_X * rotSpeed;
            transform.eulerAngles = new Vector3(0, mx, 0);
        }
    }
}
