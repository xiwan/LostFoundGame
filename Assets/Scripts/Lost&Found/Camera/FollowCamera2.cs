using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera2 : MonoBehaviour
{
    public Transform playerTransform;//跟踪的对象
    public float smoothing = 5f;
    private Vector3 offset;

	// Use this for initialization
	void Start () {
        //         当前位置              物体的位置
        offset = transform.position - playerTransform.position;//计算相对距离
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 position = playerTransform.position + offset; //保持相对距离
        transform.position = Vector3.Lerp(transform.position, position, smoothing * Time.deltaTime);
    }

}
