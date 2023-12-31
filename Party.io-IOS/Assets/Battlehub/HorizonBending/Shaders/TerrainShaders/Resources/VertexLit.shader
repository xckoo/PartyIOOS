// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Hidden/TerrainEngine/Details/Vertexlit" {
Properties {
	_MainTex ("Main Texture", 2D) = "white" {  }

}
SubShader{
	Tags { "RenderType" = "Opaque" }
	LOD 200

CGPROGRAM

#pragma surface surf Lambert vertex:hb_vert
#include "UnityCG.cginc"
#include "../../CGIncludes/HB_Core.cginc"

sampler2D _MainTex;

struct Input {
	float2 uv_MainTex;
	fixed4 color : COLOR;
};

void surf (Input IN, inout SurfaceOutput o) {
	fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * IN.color;
	o.Albedo = c.rgb;
	o.Alpha = c.a;
}

ENDCG
}
	SubShader {
		Tags { "RenderType"="Opaque" }
		Pass {
			Tags { "LightMode" = "Vertex" }
			ColorMaterial AmbientAndDiffuse
			Lighting On
			CGPROGRAM
			
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fog
			#include "UnityCG.cginc"
			#include "../../CGIncludes/HB_Core.cginc"

			struct v2f {
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				fixed4 diff : COLOR;
				float4 pos : SV_POSITION;
			};

			uniform float4 _MainTex_ST;


			struct appdata_color {
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float4 texcoord : TEXCOORD0;
				fixed4 color : COLOR;
			};

			v2f vert(appdata_color v)
			{
				v2f o;
				
				HB(v.vertex)
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.texcoord,_MainTex);
				float4 diffuse = float4(ShadeVertexLightsFull(v.vertex, v.normal, 4, true), 1.0f);
				o.diff = diffuse * v.color;
				UNITY_TRANSFER_FOG(o, o.pos);
				return o;
			}

			uniform sampler2D _MainTex;

			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 temp = tex2D(_MainTex, i.uv);
				fixed4 c = temp * i.diff;
				UNITY_APPLY_FOG(i.fogCoord, c);
				UNITY_OPAQUE_ALPHA(c.a);
				return c;
			}
			ENDCG
	}
	Pass {
		Tags { "LightMode" = "VertexLMRGBM" }
		ColorMaterial AmbientAndDiffuse
		CGPROGRAM

		
		#pragma vertex vert
		#pragma fragment frag
		#pragma multi_compile_fog

		#include "UnityCG.cginc"
		#include "../../CGIncludes/HB_Core.cginc"

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

		fixed4 frag(v2f i) : SV_Target
		{
			fixed4 lm = UNITY_SAMPLE_TEX2D(unity_Lightmap, i.uv2);
			lm *= lm.a * 2;
			fixed4 c = tex2D(_MainTex, i.uv);
			c.rgb *= lm.rgb * 4;
			UNITY_APPLY_FOG(i.fogCoord, c);
			UNITY_OPAQUE_ALPHA(c.a);
			return c;
		}
		ENDCG
	}
}

Fallback "Battlehub/Legacy Shaders/HB_VertexLit"
}
