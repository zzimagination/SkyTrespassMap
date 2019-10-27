using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace SkyTrepass.Map
{

    public class BuildingsTrigger : MonoBehaviour
    {
        public UnityEvent EnterIn;
        public UnityEvent EnterOut;
        public UnityEvent ExitIn;
        public UnityEvent ExitOut;
        Vector3 targetPos;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Vector3 dir = transform.position - other.transform.position;
                if (Vector3.Dot(dir, transform.forward) > 0)
                {
                    EnterIn.Invoke();
                }
                else
                {
                    EnterOut.Invoke();
                }
                //var move = other.GetComponent<MoveObj>();
                //move?.UpdataPath();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Vector3 dir = other.transform.position - transform.position;
                if (Vector3.Dot(dir, transform.forward) > 0)
                {
                    ExitIn.Invoke();
                }
                else
                {
                    ExitOut.Invoke();
                }
            }
        }
    }

}