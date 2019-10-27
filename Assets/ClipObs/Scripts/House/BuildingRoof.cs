using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyTrepass.Map
{
    public class BuildingRoof : HiddenObstacle
    {
        public BuildingsInstance buildingsInstance;

        Renderer _renderer;
        Collider _collider;

        private void Awake()
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

        public override bool CompareIsOn()
        {
            if (isOn)
            {
                buildingsInstance.ApearBuilding();
                _renderer.enabled = true;
                isOn = true;
                return true;
            }
            else
            {
                buildingsInstance.DispearBuilding();
                _renderer.enabled = false;
                isOn = true;
                return false;
            }
        }

        public override void JudgeOn()
        {
            isOn = false;
        }
    }
}