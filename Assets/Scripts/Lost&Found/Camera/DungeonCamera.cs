using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LostAndFound
{
    public class DungeonCamera : MonoBehaviour
    {
    
        public GameObject target;
        public float damping = 5;
        Vector3 offset;
    
        void Start() {
            offset = transform.position - target.transform.position;
        }
    
        void LateUpdate() {
            if (damping > 0) {
                Vector3 desiredPosition = target.transform.position + offset;
                Vector3 position = Vector3.Lerp (transform.position, desiredPosition, Time.deltaTime * damping);
                transform.position = position;
    
                transform.LookAt (target.transform.position);
            } else {
                Vector3 desiredPosition = target.transform.position + offset;
                transform.position = desiredPosition;
            }
        }
    }

}
