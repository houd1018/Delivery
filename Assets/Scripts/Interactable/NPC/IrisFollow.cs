using Cysharp.Threading.Tasks;
using Isekai.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IrisFollow : MonoBehaviour
{
    public float Speed;
    public Transform Player;
    public GameObject HolySpell;
    public Transform Destination;
    private Vector3 playerLastPoint;

    // Start is called before the first frame update
    void Start()
    {
        GameModel.Instance.TeleportTime = 3;
        recordPlayerLastPos().Forget();
        checkTeleport().Forget();
    }
    void trackPlayer()
    {
        if (Vector3.Distance(transform.position, playerLastPoint) > 1f)
        {
            transform.Translate((playerLastPoint - transform.position).normalized* Speed * Time.deltaTime);
        }
    }
    async UniTaskVoid recordPlayerLastPos()
    {
        while (true)
        {
            await UniTask.Delay(100, false, PlayerLoopTiming.Update, this.GetCancellationTokenOnDestroy());
            await UniTask.SwitchToMainThread();
            playerLastPoint = Player.position;
        }
    }
    async UniTaskVoid checkTeleport()
    {
        while (true)
        {
            
            if (GameModel.Instance.TeleportTime <= 0)
            {
                break;
            }
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                Player.gameObject.SetActive(false);
                if (Game.Instance != null)
                    Game.Instance.PauseScroll();
                await destroyVFX(Instantiate(HolySpell, Player.transform.position, Quaternion.identity));

                while (!chooseDestination())
                {
                    await UniTask.Yield(this.GetCancellationTokenOnDestroy());
                }
                Destination.gameObject.SetActive(false);
                if (Game.Instance != null)
                    Game.Instance.ResumeScroll();

                GameModel.Instance.TeleportTime--;
            }
            await UniTask.Yield(this.GetCancellationTokenOnDestroy());
        }


    }
    
    bool chooseDestination()
    {
        var target = Physics2D.OverlapCircle(Camera.main.ScreenToWorldPoint(Input.mousePosition), 0.5f, LayerMask.GetMask("Ground"));
        if (target != null)
        {
            Destination.gameObject.SetActive(true);
            Destination.position = target.transform.position+Vector3.up*0.55f;
        }
        else
        {
            Destination.gameObject.SetActive(false);
            return false;
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Player.gameObject.SetActive(true);
            Player.transform.position = target.transform.position + Vector3.up * 0.55f;
            destroyVFX(Instantiate(HolySpell, Player.transform.position, Quaternion.identity)).Forget();
            return true;
        }

        return false;
    }
    async UniTask destroyVFX(GameObject go)
    {
        await UniTask.Delay(1000, false, PlayerLoopTiming.Update, this.GetCancellationTokenOnDestroy());
        Destroy(go);
    }

    // Update is called once per frame
    void Update()
    {
        trackPlayer();
    }
}
