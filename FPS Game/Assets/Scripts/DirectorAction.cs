using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Playables;

public class DirectorAction : MonoBehaviour
{
    PlayableDirector pd;

    public Camera targetCam;

    private void Start()
    {
        pd = GetComponent<PlayableDirector>();
        pd.Play();
    }

    private void Update()
    {
        if (pd.time >= pd.duration - 0.1f)
        {
            GameManager.gm.ReadyGo();

            if (Camera.main == targetCam)
                targetCam.GetComponent<CinemachineBrain>().enabled = false;
            else
                targetCam.gameObject.SetActive(false);

            gameObject.SetActive(false);
        }
    }
}
