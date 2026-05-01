using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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

    [Header("플레이어 체력")]
    public int hp = 20;
    int maxHp = 20;
    public Slider hpSlider;
    public GameObject hitEffect;
    public Animator lowLifeAnim;

    Animator anim;

    private void Start()
    {
        cc = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (GameManager.gm.gState != GameManager.GameState.Run)
            return;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, 0, v);

        dir = dir.normalized;
        anim.SetFloat("MoveMotion", dir.magnitude);

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
        hpSlider.value = (float)hp / (float)maxHp;

        CheckLowLife();

        if (hp > 0)
        {
            StartCoroutine(PlayHitEffect());
        }
    }

    void CheckLowLife()
    {
        if (GameManager.gm.gState == GameManager.GameState.GameOver)
        {
            lowLifeAnim.SetBool("lowLife", false);
            return;
        }

        if ((float)hp / maxHp <= 0.15f)
            lowLifeAnim.SetBool("lowLife", true);
        else
            lowLifeAnim.SetBool("lowLife", false);
    }

    IEnumerator PlayHitEffect()
    {
        hitEffect.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        hitEffect.SetActive(false);
    }
}
