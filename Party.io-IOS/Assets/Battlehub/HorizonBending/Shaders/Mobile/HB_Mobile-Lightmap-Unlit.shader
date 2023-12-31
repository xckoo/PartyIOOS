// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Unlit shader. Simplest possible textured shader.
// - SUPPORTS lightmap
// - no lighting
// - no per-material color

Shader "Battlehub/Mobile/HB_Unlit (Supports Lightmap)" {
Properties {
	_MainTex ("Base (RGB)", 2D) = "white" {}
}

SubShader {
	Tags { "RenderType"="Opaque" }
	LOD 100
	
	Pass{
		Tags{ "LightMode" = "ForwardBase" }
		// pass for ambient light and first light source
		CGPROGRAM

		
		#pragma vertex vert  
		#pragma fragment frag

		#include "UnityCG.cginc" 
		#include "../CGIncludes/HB_Core.cginc" 
	
		// User-specified properties
		uniform sampler2D _MainTex;


		struct vertexInput {
			float4 vertex : POSITION;
			float4 texcoord : TEXCOORD0;
		};
		struct vertexOutput
		{
			float4 pos : SV_POSITION;
			float4 tex : TEXCOORD0;	
		};

		vertexOutput vert(vertexInput v)
		{
			vertexOutput output;

			
			HB(v.vertex)

			output.tex = v.texcoord;
			output.pos = UnityObjectToClipPos(v.vertex);
			return output;
		}

		float4 frag(vertexOutput v) : COLOR
		{
			return tex2D(_MainTex, v.tex.xy);
		}

		ENDCG
	}
}
}



