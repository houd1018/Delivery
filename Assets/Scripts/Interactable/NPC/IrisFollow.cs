using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IrisFollow : MonoBehaviour
{
    public float Speed;
    public Transform Player;
    private Vector3 playerLastPoint;
    // Start is called before the first frame update
    void Start()
    {
        recordPlayerLastPos().Forget();
    }
    void trackPlayer()
    {
        if (Vector3.Distance(transform.position, playerLastPoint) > 0.5f)
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
    // Update is called once per frame
    void Update()
    {
        trackPlayer();
    }
}
