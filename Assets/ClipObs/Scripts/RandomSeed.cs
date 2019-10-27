using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyTrepass
{
    public class RandomSeed 
    {
        static int se=0;

        public static int GetSeed()
        {
            if (se == 0)
            {
                se = System.DateTime.Now.Second;
            }
            se+=1;

            return se;
        }


    }
}