#ifndef CUSTOM_UNITYCOMMON_INCLUDED
#define CUSTOM_UNITYCOMMON_INCLUDED

#include "UnityInput.hlsl"
#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"

CBUFFER_START(UnityPerDraw) 
    float4x4 unity_ObjectToWorld;
    float4x4 unity_WorldToObject;
  
    half4 unity_WorldTransformParams;
    float4 unity_LODFade;
CBUFFER_END

    float4x4 unity_MatrixV;
    float4x4 glstate_matrix_projection;
    float4x4 unity_MatrixVP;

//float3 TransformObjectToWorld(float3 positionOS)
//{
 // return mul(unity_ObjectToWorld,float4(positionOS,1.0)).xyz;
//}

//float4 TransformWorldToHClip(float3 positionW)
//{
 //   return mul(unity_MatrixVP,float4(positionW,1.0));
//}

#define UNITY_MATRIX_M unity_ObjectToWorld
#define UNITY_MATRIX_I_M unity_WorldToObject
#define UNITY_MATRIX_V unity_MatrixV
#define UNITY_MATRIX_VP unity_MatrixVP
#define UNITY_MATRIX_P glstate_matrix_projection

#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/UnityInstancing.hlsl"
#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/SpaceTransforms.hlsl"

#endif