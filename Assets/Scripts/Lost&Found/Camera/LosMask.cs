using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LosMask : MonoBehaviour
{
    public Material LOSMaskMaterial;
    
    private void OnRenderImage(RenderTexture source, RenderTexture destination) 
    {
        Graphics.Blit(source, destination, LOSMaskMaterial);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
