using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyTrepass.Map
{
    public class HiddenObstacle : MonoBehaviour, IHiddenObstacle
    {
        protected Renderer renders;
        protected bool isOn;

        public virtual void JudgeOn()
        {
            if (!renders)
            {
                renders = GetComponent<Renderer>();
            }

            isOn = false;
        }

        public virtual bool CompareIsOn()
        {
            if (isOn)
            {
                renders.enabled = true;
                return true;
            }
            else
            {
                renders.enabled = false;
                isOn = true;
                return false;
            }

        }
    }
}