using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyTrepass.Map
{
    public interface IHiddenObstacle
    {
        void JudgeOn();
        bool CompareIsOn();
    }
}