
Shader "Unlit/Test"
{
Properties
{
[Gamma][Header(CubeMap)]_MainColor("MainColor",COLOR)=(0.5,0.5,0.5,1)
_Spec("Spec",Range(1,8))=1
[NoScaleOffset]_Tex("CubeMap",CUBE)="black"{}
[Header(Rotation)][Toggle(_ENABLEROTATION_ON)]_EnableRotation("Enable Rotation",Float)=0
[IntRange]_Rotation("Rotation",Range(0,360))=0
_RotationSpeed("RotationSpeed",float)=1
[Header(Fog)][Toggle(_ENABLEFOG_ON)]_EnableFog("Enable Fog",float)=0
_FogHeight("FogHeight",Range(0,1))=1
_FogSmooth("FogSmooth",Range(0.01,1))=0.01
_FogHill("FogHill",Range(0,1))=0.5
 
 
}
SubShader
{
Tags { "RenderType"="Background"  "Queue"="Background" "IgnoreProjector"="True" "ForceNoShadowCasting"="True"}
LOD 100
Cull Off
ZWrite Off
CGPROGRAM
#include "UnityShaderVariables.cginc"
#include "UnityCG.cginc"
#pragma target 3.0
#pragma shader_feature _ENABLEROTATION_ON
#pragma shader_feature _ENABLEFOG_ON
#pragma surface surf Lint keepalpha noshadow noambient novertexlights nolightmap nodynlightmap nodirlightmap nofog nometa noforwardadd vertex:vertexRo
 
 
struct Input
{
float3 worldPos;
float3 vertextofrag;
};
 
 
uniform float4 _MainColor;
uniform float _Spec;
uniform samplerCUBE _Tex;
uniform half _Rotation;
uniform half _RotationSpeed;
uniform half _FogHeight;
uniform half _FogSmooth;
uniform half _FogHill;
uniform half4 _Tex_HDR;
 
 
half3 decode_HDR(half4 data)
{
return DecodeHDR(data,_Tex_HDR);
}
void vertexRo(inout appdata_full v,out Input o)
{
UNITY_INITIALIZE_OUTPUT(Input,o);
float3 _worldPos=mul(unity_ObjectToWorld,v.vertex);
float _lerpResult=lerp(1.0,(unity_OrthoParams.y/unity_OrthoParams.x),unity_OrthoParams.w);
float3 _append=float3(_worldPos.x,_worldPos.y*_lerpResult,_worldPos.z);
float3 _nor_append=normalize(_append);
float _timeMove=_Time.y;
float3 _timeMove1=float3(cos(radians(_Rotation+_timeMove*_RotationSpeed)),0,sin(radians(_Rotation+_timeMove*_RotationSpeed))*-1);
float3 _timeMove2=float3(0,_lerpResult,0);
float3 _timeMove3=float3(sin(radians(_Rotation+_timeMove*_RotationSpeed)),0,cos(radians(_Rotation+_timeMove*_RotationSpeed)));
float3 _nor_worldPos=normalize(_worldPos);
#ifdef _ENABLEROTATION_ON 
o.vertextofrag=mul(float3x3(_timeMove1,_timeMove2,_timeMove3),_nor_worldPos);
#else
o.vertextofrag=_nor_append;
#endif
 
 
}
fixed4 LightingLint(SurfaceOutput s,float3 lightDir,float atten)
{
return fixed4(0,0,0,s.Alpha);
}
void surf(Input i,inout SurfaceOutput o)
{
half4 CUBEdata=texCUBE(_Tex,i.vertextofrag);
half3 CUBEdataHDR=decode_HDR(CUBEdata);
float4 CUBEColor=(float4(CUBEdataHDR,0))*_MainColor*_Spec*unity_ColorSpaceDouble;
float3 _nor_worldPos=normalize(i.worldPos);
float _lerpFog=lerp(saturate(pow(_nor_worldPos.y/_FogHeight,1-_FogSmooth)),0,_FogHill);
float4 FinalColor=lerp(unity_FogColor,CUBEColor,_lerpFog);
#ifdef _ENABLEFOG_ON
o.Emission=FinalColor.rgb;
#else
o.Emission=CUBEColor.rgb;
#endif
o.Alpha=1;
 
 
}
ENDCG
}
 
}