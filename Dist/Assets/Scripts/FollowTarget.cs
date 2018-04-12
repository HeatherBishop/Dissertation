using System;
using UnityEngine;


    public class FollowTarget : MonoBehaviour
    {
        public Transform target;
        public Vector3 offset = new Vector3(7f, 0f, 0f);


        private void LateUpdate()
        {
        transform.position = new Vector3(target.position.x, target.position.y, 0) + offset; 
        }
    }

