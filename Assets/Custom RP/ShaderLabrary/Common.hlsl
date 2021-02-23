#ifndef CUSTOM_UNITYCOMMON_INCLUDED
#define CUSTOM_UNITYCOMMON_INCLUDED

#include "UnityInput.hlsl"

float4x4 unity_ObjectToWorld;
float4x4 unity_MatrixVp;


float3 TransformObjectToWorld(float3 positionOS)
{
  return mul(unity_ObjectToWorld,float4(positionOS,1.0)).xyz;
}

float4 TransformWorldToClip(float3 positionW)
{
    return mul(unity_MatrixVp,float4(positionW,1.0));
}

#endif