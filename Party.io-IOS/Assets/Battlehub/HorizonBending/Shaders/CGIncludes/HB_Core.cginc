// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

#ifndef HB_CORE_INCLUDED
#define HB_CORE_INCLUDED
#include "UnityCG.cginc"

uniform float3 _Curvature;
uniform float3 _HorizonOffset;
uniform float3 _Flatten;
uniform float4 _HBWorldSpaceCameraPos;

#define _HB_XZ_YUP_ZFWD 1

float4 HorizonBendXY_ZUP_YFWD(float4 posWorld)
{
	float4 offset = posWorld - _HBWorldSpaceCameraPos;

	float d1 = max(0, abs(_HorizonOffset.y - offset.y) - _Flatten.y);
	float d2 = max(0, abs(_HorizonOffset.x - offset.x) - _Flatten.x);

	offset = float4(d1 * d1 * -_Curvature.z, 0.0f, (d1 * d1 * -_Curvature.y + d2 * d2 * -_Curvature.x), 0.0f);
	return posWorld + offset;
}

float4 HorizonBendXY_ZUP_XFWD(float4 posWorld)
{
	float4 offset = posWorld - _HBWorldSpaceCameraPos;

	float d1 = max(0, abs(_HorizonOffset.y - offset.y) - _Flatten.y);
	float d2 = max(0, abs(_HorizonOffset.x - offset.x) - _Flatten.x);

	offset = float4(0.0f, d2 * d2 * -_Curvature.z, (d1 * d1 * -_Curvature.y + d2 * d2 * -_Curvature.x), 0.0f);
	return posWorld + offset;
}

float4 HorizonBendXZ_YUP_ZFWD(float4 posWorld)
{
	float4 offset = posWorld - _HBWorldSpaceCameraPos;

	float d1 = max(0, abs(_HorizonOffset.z - offset.z) - _Flatten.z);
	float d2 = max(0, abs(_HorizonOffset.x - offset.x) - _Flatten.x);

	offset = float4(d1 * d1 * -_Curvature.y, (d1 * d1 * -_Curvature.z + d2 * d2 * -_Curvature.x), 0.0f, 0.0f);
	return posWorld + offset;
}

float4 HorizonBendXZ_YUP_XFWD(float4 posWorld)
{
	float4 offset = posWorld - _HBWorldSpaceCameraPos;

	float d1 = max(0, abs(_HorizonOffset.z - offset.z) - _Flatten.z);
	float d2 = max(0, abs(_HorizonOffset.x - offset.x) - _Flatten.x);

	offset = float4(0.0f, (d1 * d1 * -_Curvature.z + d2 * d2 * -_Curvature.x), d2 * d2 * -_Curvature.y, 0.0f);
	return posWorld + offset;
}

float4 HorizonBendYZ_XUP_ZFWD(float4 posWorld)
{
	float4 offset = posWorld - _HBWorldSpaceCameraPos;

	float d1 = max(0, abs(_HorizonOffset - offset.z) - _Flatten.z);
	float d2 = max(0, abs(_HorizonOffset - offset.y) - _Flatten.y);

	offset = float4((d1 * d1 * -_Curvature.z + d2 * d2 * -_Curvature.y), d1 * d1 * -_Curvature.x, 0.0f, 0.0f);
	return posWorld + offset;
}

float4 HorizonBendYZ_XUP_YFWD(float4 posWorld)
{
	float4 offset = posWorld - _HBWorldSpaceCameraPos;

	float d1 = max(0, abs(_HorizonOffset - offset.z) - _Flatten.z);
	float d2 = max(0, abs(_HorizonOffset - offset.y) - _Flatten.y);

	offset = float4((d1 * d1 * -_Curvature.z + d2 * d2 * -_Curvature.y), 0.0f, d2 * d2 * -_Curvature.x, 0.0f);
	return posWorld + offset;
}

#if _HB_XZ_YUP_ZFWD
#define POS_WORLD(vertex) float4 posWorld = mul(unity_ObjectToWorld, vertex);
#define APPLY_OFFSET posWorld += float4(0.0f, _HorizonOffset.y, 0.0f, 0.0f);
#define HORIZON_BEND(vertex, posWorld) posWorld = HorizonBendXZ_YUP_ZFWD(posWorld); vertex = mul(unity_WorldToObject, posWorld);
#elif _HB_XZ_YUP_XFWD
#define POS_WORLD(vertex) float4 posWorld = mul(unity_ObjectToWorld, vertex);
#define APPLY_OFFSET posWorld += float4(0.0f, _HorizonOffset.y, 0.0f, 0.0f);
#define HORIZON_BEND(vertex, posWorld) posWorld = HorizonBendXZ_YUP_XFWD(posWorld); vertex = mul(unity_WorldToObject, posWorld);
#elif _HB_XY_ZUP_YFWD
#define POS_WORLD(vertex) float4 posWorld = mul(unity_ObjectToWorld, vertex);
#define APPLY_OFFSET posWorld += float4(0.0f,  0.0f, _HorizonOffset.z, 0.0f);
#define HORIZON_BEND(vertex, posWorld) posWorld = HorizonBendXY_ZUP_YFWD(posWorld); vertex = mul(unity_WorldToObject, posWorld);
#elif _HB_XY_ZUP_XFWD
#define POS_WORLD(vertex) float4 posWorld = mul(unity_ObjectToWorld, vertex);
#define APPLY_OFFSET posWorld += float4(0.0f,  0.0f, _HorizonOffset.z, 0.0f);
#define HORIZON_BEND(vertex, posWorld) posWorld = HorizonBendXY_ZUP_XFWD(posWorld); vertex = mul(unity_WorldToObject, posWorld);
#elif _HB_YZ_XUP_ZFWD
#define POS_WORLD(vertex) float4 posWorld = mul(unity_ObjectToWorld, vertex);
#define APPLY_OFFSET posWorld += float4(_HorizonOffset.x, 0.0f,  0.0f, 0.0f);
#define HORIZON_BEND(vertex, posWorld) posWorld = HorizonBendYZ_XUP_ZFWD(posWorld); vertex = mul(unity_WorldToObject, posWorld);
#elif _HB_YZ_XUP_YFWD
#define POS_WORLD(vertex) float4 posWorld = mul(unity_ObjectToWorld, vertex);
#define APPLY_OFFSET posWorld += float4(_HorizonOffset.x, 0.0f,  0.0f, 0.0f);
#define HORIZON_BEND(vertex, posWorld) posWorld = HorizonBendYZ_XUP_YFWD(posWorld); vertex = mul(unity_WorldToObject, posWorld);
#else
#define POS_WORLD(vertex)
#define APPLY_OFFSET 
#define HORIZON_BEND(vertex, posWorld)
#endif

#define HB(vertex) POS_WORLD(vertex) HORIZON_BEND(vertex, posWorld)

void hb_vert(inout appdata_full v)
{
	HB(v.vertex)
}

#endif


