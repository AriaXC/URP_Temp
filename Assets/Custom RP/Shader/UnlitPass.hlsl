#ifndef CUSTOM_UNLIT_PASS_INCLUDED
#define CUSTOM_UNLIT_PASS_INCLUDED

#include "../ShaderLabrary/Common.hlsl"

//缓冲区

CBUFFER_START(UnityPerMaterial) 
    float4 _BaseColor;
CBUFFER_END

float4 vert(float3 positionOS : POSITION):SV_POSITION
{
  // return float4(positionOS,1.0);

    float3 pos =  TransformObjectToWorld(positionOS.xyz);
   return TransformWorldToHClip(pos);

    //return float4(pos,1.0);
}
float4 frag():SV_TARGET
{
    return _BaseColor;
  // return float4(1.0,1.0,0.0,1.0);
}

#endif