using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenObstacle : MonoBehaviour,IHiddenObstacle
{
    Renderer renders;
    bool isOn;
    public void JudgeOn()
    {
        if(!renders)
        {
            renders = GetComponent<Renderer>();
        }
        isOn = false;
    }

    public void CompareIsOn()
    {

        isOn = true;
        StartCoroutine(EndFrame());
    }

    IEnumerator EndFrame()
    {
        yield return new WaitForEndOfFrame();
        if (isOn)
        {
            renders.enabled = true;
        }
        else
        {
            renders.enabled = false;
        }
    }
}
