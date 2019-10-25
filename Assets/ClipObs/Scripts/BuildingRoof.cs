using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingRoof : MonoBehaviour,IHiddenObstacle
{
    public BuildingsInstance buildingsInstance;

    bool isOn;
    Renderer _renderer;
    Collider _collider;


    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _collider = GetComponent<Collider>();
    }
    public void Disappear()
    {
        _collider.enabled = false;
        _renderer.enabled = false;
    }

    public void Appear()
    {
        _collider.enabled = true;
        _renderer.enabled = true;
    }

    public void CompareIsOn()
    {
        isOn = true;
        StartCoroutine(EndFrame());
    }

    public void JudgeOn()
    {
        isOn = false;
    }

    IEnumerator EndFrame()
    {
        yield return new WaitForEndOfFrame();
        if (isOn)
        {
            buildingsInstance.ApearBuilding();
            _renderer.enabled = true;
        }
        else
        {
            buildingsInstance.DispearBuilding();
            _renderer.enabled = false;
        }
    }
}
