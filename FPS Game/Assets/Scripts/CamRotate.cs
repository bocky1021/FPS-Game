using UnityEngine;

public class CamRotate : MonoBehaviour
{
    public float rotSpeed = 5f;

    float mx = 0;
    float my = 0;

    private void Update()
    {
        if (GameManager.gm.gState != GameManager.GameState.Run)
            return;

        if (Input.GetMouseButton(2))
        {
            float mouse_X = Input.GetAxisRaw("Mouse X");
            float mouse_Y = Input.GetAxisRaw("Mouse Y");

            mx += mouse_X * rotSpeed;
            my += mouse_Y * rotSpeed;

            my = Mathf.Clamp(my, -90f, 90f);

            transform.eulerAngles = new Vector3(-my, mx, 0);
        }
    }
}
