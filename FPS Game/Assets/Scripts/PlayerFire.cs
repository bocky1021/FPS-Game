using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject firePosition;
    
    public GameObject bombFactory;
    public float throwPower = 15f;

    public GameObject bulletEffect;
    ParticleSystem ps;

    public int weaponPower = 5;

    Animator anim;

    enum WeaponMode
    {
        Normal,
        Sniper
    }
    WeaponMode wMode;

    bool zoomMode = false;

    private void Start()
    {
        ps = bulletEffect.GetComponent<ParticleSystem>();
        anim = GetComponentInChildren<Animator>();

        wMode = WeaponMode.Normal;
    }

    private void Update()
    {
        if (GameManager.gm.gState != GameManager.GameState.Run)
            return;

        if (Input.GetMouseButtonDown(1))
        {
            switch (wMode)
            {
                case WeaponMode.Normal:
                    GameObject bomb = Instantiate(bombFactory);
                    bomb.transform.position = firePosition.transform.position;

                    Rigidbody rb = bomb.GetComponent<Rigidbody>();
                    rb.AddForce(Camera.main.transform.forward * throwPower, ForceMode.Impulse);
                    break;
                case WeaponMode.Sniper:
                    if (!zoomMode)
                    {
                        Camera.main.fieldOfView = 15f;
                        zoomMode = true;
                    }
                    else
                    {
                        Camera.main.fieldOfView = 60f;
                        zoomMode = false;
                    }
                    break;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (anim.GetFloat("MoveMotion") == 0)
            {
                anim.SetTrigger("Attack");
            }

            Transform camTr = Camera.main.transform;
            Ray ray = new Ray(camTr.position, camTr.forward);

            RaycastHit hitInfo = new RaycastHit();
            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    EnemyFSM eFSM = hitInfo.transform.GetComponent<EnemyFSM>();
                    eFSM.HitEnemy(weaponPower);
                }
                else
                {
                    bulletEffect.transform.position = hitInfo.point;
                    bulletEffect.transform.forward = hitInfo.normal;

                    ps.Play();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            wMode = WeaponMode.Normal;

            Camera.main.fieldOfView = 60f;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            wMode = WeaponMode.Sniper;
        }
    }
}
