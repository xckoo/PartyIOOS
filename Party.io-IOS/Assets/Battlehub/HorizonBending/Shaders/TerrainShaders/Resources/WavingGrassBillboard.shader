// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Hidden/TerrainEngine/Details/BillboardWavingDoublePass" {
	Properties{
		_WavingTint("Fade Color", Color) = (.7,.6,.5, 0)
		_MainTex("Base (RGB) Alpha (A)", 2D) = "white" {}
		_WaveAndDistance("Wave and distance", Vector) = (12, 3.6, 1, 1)
		_Cutoff("Cutoff", float) = 0.5
	}

		CGINCLUDE
		#include "UnityCG.cginc"
		#include "../../CGIncludes/HB_Core.cginc"
		#include "../../CGIncludes/HB_TerrainEngine.cginc"

	struct v2f {
		float4 pos : SV_POSITION;
		fixed4 color : COLOR;
		float4 uv : TEXCOORD0;
	};
	v2f BillboardVert(appdata_full v) {
		v2f o;
		WavingGrassBillboardVert(v);
		o.color = v.color;

		o.color.rgb *= ShadeVertexLights(v.vertex, v.normal);

		o.pos = UnityObjectToClipPos(v.vertex);
		o.uv = v.texcoord;
		o.color = fixed4(0, 0, 0, 1);
		return o;
	}
	ENDCG

		SubShader{
		Tags{
		"Queue" = "Geometry+200"
		"IgnoreProjector" = "True"
		"RenderType" = "GrassBillboard"
		"DisableBatching" = "True"
	}
		Cull Off
		LOD 200
		ColorMask RGB

		CGPROGRAM
		
		#pragma surface surf Lambert vertex:WavingGrassBillboardVert addshadow exclude_path:deferred

		sampler2D _MainTex;
	fixed _Cutoff;

	struct Input {
		float2 uv_MainTex;
		fixed4 color : COLOR;
	};

	void surf(Input IN, inout SurfaceOutput o) {
		fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * IN.color;
		o.Albedo = c.rgb;
		o.Alpha = c.a;
		clip(o.Alpha - _Cutoff);
		o.Alpha *= IN.color.a;
	}

	ENDCG
	}

		Fallback Off
}
