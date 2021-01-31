using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class BgmController : MonoBehaviour
{
    [SerializeField] private EmitterRef emitterRef;

    void Start()
    {
        StartCoroutine(CountDownControl());
        Game.Instance.gameEndEvent.AddListener(EndGame);
    }

    
    IEnumerator CountDownControl()
    {
        print("Intensity = 0");
        emitterRef.Target.SetParameter("Intensity", 0);
        yield return new WaitForSeconds(27);

        // 27

        print("Intensity = 1");
        emitterRef.Target.SetParameter("Intensity", 1);
        yield return new WaitForSeconds(27);

        // 54

        print("Intensity = 2");
        emitterRef.Target.SetParameter("Intensity", 2);
        yield return new WaitForSeconds(27);

        // 81 (1:21)

        print("Intensity = 3");
        emitterRef.Target.SetParameter("Intensity", 3);
        yield return new WaitForSeconds(27);

        // 108 (1:48)

        print("Transition = 1");
        emitterRef.Target.SetParameter("Transition", 1);
    }

    void EndGame()
    {
        if (Game.Instance.gameResult > 0)
            OnWin();
        else
            OnLose();
    }

    void OnWin()
    {
        emitterRef.Target.SetParameter("Win n Lose", 1);
    }
    void OnLose()
    {
        emitterRef.Target.SetParameter("Win n Lose", 2);
    }
}
