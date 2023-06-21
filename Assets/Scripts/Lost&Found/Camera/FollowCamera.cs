using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LostAndFound
{
    public class FollowCamera : MonoBehaviour {
    
        public GameObject target;
        public float damping = 1;
        Vector3 offset;
    
        void Start(){
            offset = target.transform.position - transform.position;
        }
    
    
        void LateUpdate(){
            if (damping > 0) 
            {
                float currentAngle = transform.eulerAngles.y;
                float desiredAngle = target.transform.eulerAngles.y;
                float angle = Mathf.LerpAngle (currentAngle, desiredAngle, Time.deltaTime * damping);
    
                Quaternion rotation = Quaternion.Euler (0, angle, 0);
                transform.position = target.transform.position - (rotation * offset);
    
                transform.LookAt (target.transform);
            }
            else
            {
                float desireAngle = target.transform.eulerAngles.y;
                Quaternion rotation = Quaternion.Euler (0, desireAngle, 0);
                transform.position = target.transform.position - (rotation * offset);
                transform.LookAt (target.transform);
            }
        }
    }


}
