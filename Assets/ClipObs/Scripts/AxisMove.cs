using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SkyTrepass
{
    public class AxisMove : MonoBehaviour
    {
        public float moveSpeed;

        Rigidbody _rigidbody;
        Vector3 delt;
        // Start is called before the first frame update
        void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
        private void FixedUpdate()
        {
            if (!delt.Equals(Vector3.zero))
            {
                Vector3 pos = _rigidbody.position + delt * moveSpeed * Time.fixedDeltaTime;
                _rigidbody.MovePosition(pos);
                float angle = Vector3.Angle(new Vector3(0, 0, 1), delt);
                angle *= Vector3.Dot(new Vector3(1, 0, 0), delt) > 0 ? 1 : -1;
                Quaternion qua = Quaternion.AngleAxis(angle, new Vector3(0, 1, 0));

                _rigidbody.MoveRotation(qua);
            }
        }
        // Update is called once per frame
        void Update()
        {

        }

        void OnAxisMove(InputValue value)
        {
            Vector2 t = value.Get<Vector2>();
            delt = new Vector3(t.x, 0, t.y);
        }
    }
}