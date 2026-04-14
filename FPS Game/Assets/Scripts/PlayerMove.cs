using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("플레이어 이동 속도")]
    [Range(1f, 10f)]
    public float moveSpeed = 7f;

    CharacterController cc;

    float gravity = -20f;
    [SerializeField]
    float yVelocity = 0;

    [Header("플레이어 점프")]
    public float jumpPower = 10f;
    [Space(10f)]
    [HideInInspector]
    public bool isJumping = false;

    int hp = 100;

    private void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, 0, v);
        dir = dir.normalized;

        dir = Camera.main.transform.TransformDirection(dir);

        if (isJumping && cc.collisionFlags == CollisionFlags.Below)
        {
            isJumping = false;
            yVelocity = 0;
        }

        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            yVelocity = jumpPower;
            isJumping = true;
        }

        yVelocity += gravity * Time.deltaTime;
        dir.y = yVelocity;

        cc.Move(dir * moveSpeed * Time.deltaTime);
    }

    public void DamageAction(int damage)
    {
        hp -= damage;
    }
}
