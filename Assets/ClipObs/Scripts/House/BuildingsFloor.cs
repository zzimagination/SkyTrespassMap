using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SkyTrepass.Map
{
    public class BuildingsFloor : MonoBehaviour
    {
        public BuildingsFloor underFloor;
        public BuildingsFloor onFloor;

        Renderer[] renderers;

        FloorState state;
        // Start is called before the first frame update
        void Start()
        {
            state = FloorState.standby;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void ActiveFloor()
        {
            if (state == FloorState.active)
                return;
            gameObject.SetActive(true);
            state = FloorState.active;
        }

        public void DisableFloor()
        {
            if (state == FloorState.stop)
                return;
            gameObject.SetActive(false);
            state = FloorState.stop;
        }

        public void StandByFloor()
        {
            gameObject.SetActive(true);
            renderers = GetComponentsInChildren<Renderer>();
            state = FloorState.standby;
        }

        public void HiddenRender(bool open)
        {
            for (int i = 0; i < renderers.Length; i++)
            {
                renderers[i].enabled = open;
            }
        }

        public void UpEvent()
        {
            onFloor?.ActiveFloor();
        }
        public void DownEvent()
        {
            onFloor?.DisableFloor();
        }

        enum FloorState
        {
            standby,
            active,
            stop,
        }
    }
}