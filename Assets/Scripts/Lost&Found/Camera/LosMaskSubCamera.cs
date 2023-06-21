using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LosMaskSubCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var t = new RenderTexture(Screen.width, Screen.height, 16);
        GetComponent<Camera>().targetTexture = t;
        Shader.SetGlobalTexture("_LOSMaskTexture", t);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
