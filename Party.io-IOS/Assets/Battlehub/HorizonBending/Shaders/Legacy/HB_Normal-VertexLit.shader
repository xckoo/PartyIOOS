// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Battlehub/Legacy Shaders/HB_VertexLit" {
Properties {
	_Color ("Main Color", Color) = (1,1,1,1)
	_Emission("Emissive Color", Color) = (0,0,0,0)
	_MainTex ("Base (RGB)", 2D) = "white" {}

}
Category{
	Tags{ "RenderType" = "Opaque" }
	LOD 150

	SubShader{
	// Vertex Lit, emulated in shaders (4 lights max, no specular)
	Pass{
		Tags{ "LightMode" = "Vertex" }
		Lighting On
		CGPROGRAM
		#include "../CGIncludes/HB_Core.cginc"
		
		#pragma vertex vert
		#pragma fragment frag
		#pragma multi_compile_fog
		#include "UnityCG.cginc"

		struct v2f {
			float2 uv : TEXCOORD0;
			UNITY_FOG_COORDS(1)
			fixed4 diff : COLOR0;
			float4 pos : SV_POSITION;
		};

		uniform float4 _MainTex_ST;
		uniform float4 _Color;
		uniform float4 _Emission;
		
		v2f vert(appdata_base v)
		{
			v2f o;
			HB(v.vertex)
			o.pos = UnityObjectToClipPos(v.vertex);
			o.uv = TRANSFORM_TEX(v.texcoord,_MainTex);
			float4 diffuse = float4(ShadeVertexLightsFull(v.vertex, v.normal, 4, true) , 1.0f);
			o.diff = diffuse * _Color + _Emission;
			UNITY_TRANSFER_FOG(o,o.pos);
			return o;
		}

		uniform sampler2D _MainTex;

		fixed4 frag(v2f i) : SV_Target
		{	
			fixed4 temp = tex2D(_MainTex, i.uv);
			fixed4 c = temp * i.diff + _Emission;
			UNITY_APPLY_FOG(i.fogCoord, c);
			UNITY_OPAQUE_ALPHA(c.a);
			return c;
		}
		ENDCG
	}

	// Lightmapped
	Pass{
		Tags{ "LightMode" = "VertexLM" }

		CGPROGRAM
		#include "../CGIncludes/HB_Core.cginc"
		
		#pragma vertex vert
		#pragma fragment frag
		#pragma multi_compile_fog

		#include "UnityCG.cginc"

		struct v2f {
			half2 uv : TEXCOORD0;
			half2 uv2 : TEXCOORD1;
			UNITY_FOG_COORDS(2)
			float4 pos : SV_POSITION;
		};

		uniform float4 _MainTex_ST;
	

		v2f vert(float4 vertex : POSITION, float2 uv : TEXCOORD0, float2 uv2 : TEXCOORD1)
		{
			v2f o;
			HB(vertex)
			o.pos = UnityObjectToClipPos(vertex);
			o.uv = TRANSFORM_TEX(uv,_MainTex);
			o.uv2 = uv2 * unity_LightmapST.xy + unity_LightmapST.zw;
			UNITY_TRANSFER_FOG(o,o.pos);
			return o;
		}

		uniform sampler2D _MainTex;
		uniform fixed4 _Color;


		fixed4 frag(v2f i) : SV_Target
		{
			fixed4 lm = UNITY_SAMPLE_TEX2D(unity_Lightmap, i.uv2) * _Color;
			fixed4 c = tex2D(_MainTex, i.uv);
			c.rgb *= lm.rgb * 2;
			UNITY_APPLY_FOG(i.fogCoord, c);
			UNITY_OPAQUE_ALPHA(c.a);
			return c;
		}
		ENDCG
	}

	// Lightmapped, encoded as RGBM
	Pass{
		Tags{ "LightMode" = "VertexLMRGBM" }
	
		CGPROGRAM
		#include "../CGIncludes/HB_Core.cginc"
		
		#pragma vertex vert
		#pragma fragment frag
		#pragma multi_compile_fog

		#include "UnityCG.cginc"

		struct v2f {
			half2 uv : TEXCOORD0;
			half2 uv2 : TEXCOORD1;
			UNITY_FOG_COORDS(2)
			float4 pos : SV_POSITION;
		};

		uniform float4 _MainTex_ST;

		v2f vert(float4 vertex : POSITION, float2 uv : TEXCOORD0, float2 uv2 : TEXCOORD1)
		{
			v2f o;
			HB(vertex)
			o.pos = UnityObjectToClipPos(vertex);
			o.uv = TRANSFORM_TEX(uv,_MainTex);
			o.uv2 = uv2 * unity_LightmapST.xy + unity_LightmapST.zw;
			UNITY_TRANSFER_FOG(o,o.pos);
			return o;
		}

		uniform sampler2D _MainTex;
		uniform fixed4 _Color;
		

		fixed4 frag(v2f i) : SV_Target
		{
			fixed4 lm = UNITY_SAMPLE_TEX2D(unity_Lightmap, i.uv2);
			lm *= lm.a * 2;
			lm *= _Color;
			fixed4 c = tex2D(_MainTex, i.uv);
			c.rgb *= lm.rgb * 4;
			UNITY_APPLY_FOG(i.fogCoord, c);
			UNITY_OPAQUE_ALPHA(c.a);
			return c;
		}
		ENDCG
	}

	// Pass to render object as a shadow caster
	Pass {
		Name "ShadowCaster"
		Tags { "LightMode" = "ShadowCaster" }
		
		CGPROGRAM
		
		#pragma vertex vert
		#pragma fragment frag
		#pragma multi_compile_shadowcaster
		#include "UnityCG.cginc"
		#include "../CGIncludes/HB_Core.cginc"

		struct v2f { 
			V2F_SHADOW_CASTER;
		};

		v2f vert( appdata_base v )
		{
			v2f o;
			HB(v.vertex)
			TRANSFER_SHADOW_CASTER_NORMALOFFSET(o)
			return o;
		}

		float4 frag( v2f i ) : SV_Target
		{
			SHADOW_CASTER_FRAGMENT(i)
		}
		ENDCG
		}
	}
}
Fallback "Legacy Shaders/VertexLit"
CustomEditor "HB_ShaderGUI"
}

/*
SubShader{
	Pass{
	Tags{ "LightMode" = "ForwardBase" }
	// pass for ambient light and first light source

	CGPROGRAM

#pragma shader_feature RELATIVE_TO_CAMERA
#pragma shader_feature HORIZON_BEND_X
#pragma shader_feature HORIZON_BEND_Z
#pragma vertex vert  
#pragma fragment frag

#include "UnityCG.cginc" 
#include "CGIncludes/HB_Core.cginc" 
	uniform float4 _LightColor0;
// color of light source (from "Lighting.cginc")

// User-specified properties
uniform sampler2D _MainTex;
uniform float4 _Color;
uniform float4 _Emission;
uniform float4 _SpecColor;
uniform float _Shininess;

struct vertexInput {
	float4 vertex : POSITION;
	float3 normal : NORMAL;
	float4 texcoord : TEXCOORD0;
};
struct vertexOutput
{
	float4 pos : SV_POSITION;
	float4 tex : TEXCOORD0;
	float3 color : TEXCOORD1;
};

vertexOutput vert(vertexInput v)
{
	vertexOutput output;

	POS_WORLD
		HORIZON_BEND(v, posWorld)

		float4x4 modelMatrix = _Object2World;
		float3 normalDirection = normalize(mul(float4(v.normal, 0.0), modelMatrix).xyz);
		float3 viewDirection = normalize(_WorldSpaceCameraPos - mul(modelMatrix, v.vertex).xyz);
		float3 lightDirection;
		float attenuation;
		if (0.0 == _WorldSpaceLightPos0.w)
		{
			attenuation = 1.0;
			lightDirection = normalize(_WorldSpaceLightPos0.xyz);
		}
		else
		{
			float3 vertexToLightSource = _WorldSpaceLightPos0.xyz - mul(modelMatrix, v.vertex).xyz;
			float distance = length(vertexToLightSource);
			attenuation = 1.0 / distance;
			lightDirection = normalize(vertexToLightSource);
		}


		float3 emission = _Emission.rgb;
		float3 color = _Color.rgb;
		float3 ambient = UNITY_LIGHTMODEL_AMBIENT.rgb * color;
		float3 diffuse = color * max(0.0, dot(normalDirection, lightDirection));
		float3 specular;
		if (dot(normalDirection, lightDirection) < 0.0)
		{
			specular = float3(0.0, 0.0, 0.0);
		}
		else
		{
			float3 h = normalize(lightDirection + viewDirection);
			float nh = saturate(dot(h, normalDirection));
			specular = (0.5f * _SpecColor.rgb) *  pow(nh, _Shininess * 128);
		}


		output.color = ambient + attenuation * _LightColor0.rgb  * (diffuse + specular) + emission * 1.8;
		output.tex = v.texcoord;
		output.pos = mul(UNITY_MATRIX_MVP, v.vertex);
		return output;
}

float4 frag(vertexOutput v) : COLOR
{
	return float4(v.color * tex2D(_MainTex, v.tex.xy), 1.0);
}

ENDCG
}
Pass{
	Tags{ "LightMode" = "ForwardAdd" }
	// pass for additional light sources
	Blend One One // additive blending 
	CGPROGRAM

#pragma shader_feature RELATIVE_TO_CAMERA
#pragma shader_feature HORIZON_BEND_X
#pragma shader_feature HORIZON_BEND_Z
#pragma vertex vert  
#pragma fragment frag 

#include "UnityCG.cginc" 
#include "CGIncludes/HB_Core.cginc" 
	uniform float4 _LightColor0;
// color of light source (from "Lighting.cginc")

// User-specified properties
uniform sampler2D _MainTex;
uniform float4 _Color;
uniform float4 _SpecColor;
uniform float _Shininess;

struct vertexInput {
	float4 vertex : POSITION;
	float3 normal : NORMAL;
	float4 texcoord : TEXCOORD0;
};
struct vertexOutput {
	float4 pos : SV_POSITION;
	float4 tex : TEXCOORD0;
	float3 color : TEXCOORD1;
};

vertexOutput vert(vertexInput v)
{
	vertexOutput output;

	POS_WORLD
		HORIZON_BEND(v, posWorld)

		float4x4 modelMatrix = _Object2World;
		float3 normalDirection = normalize(mul(float4(v.normal, 0.0), modelMatrix).xyz);
		float3 viewDirection = normalize(_WorldSpaceCameraPos - mul(modelMatrix, v.vertex).xyz);
		float3 lightDirection;
		float attenuation;
		if (0.0 == _WorldSpaceLightPos0.w)
		{
			attenuation = 1.0;
			lightDirection = normalize(_WorldSpaceLightPos0.xyz);
		}
		else
		{
			float3 vertexToLightSource = _WorldSpaceLightPos0.xyz - mul(modelMatrix, v.vertex).xyz;
			float distance = length(vertexToLightSource);
			attenuation = 1.0 / distance;
			lightDirection = normalize(vertexToLightSource);
		}

		float3 ambient = UNITY_LIGHTMODEL_AMBIENT.rgb * _Color.rgb;
		float3 diffuse = _Color.rgb * max(0.0, dot(normalDirection, lightDirection));
		float3 specular;
		if (dot(normalDirection, lightDirection) < 0.0)
		{
			specular = float3(0.0, 0.0, 0.0);
		}
		else
		{
			float3 h = normalize(lightDirection + viewDirection);
			float nh = saturate(dot(h, normalDirection));
			specular = 0.5f * _SpecColor.rgb *  pow(nh, _Shininess * 64);
		}

		output.color = attenuation * _LightColor0.rgb  * (diffuse + specular);
		output.tex = v.texcoord;
		output.pos = mul(UNITY_MATRIX_MVP, v.vertex);
		return output;
}

float4 frag(vertexOutput v) : COLOR
{
	return float4(v.color * tex2D(_MainTex, v.tex.xy), 1.0);
}

ENDCG
}
*/