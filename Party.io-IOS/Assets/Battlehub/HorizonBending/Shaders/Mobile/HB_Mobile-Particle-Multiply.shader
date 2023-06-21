// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Simplified Multiply Particle shader. Differences from regular Multiply Particle one:
// - no Smooth particle support
// - no AlphaTest
// - no ColorMask

Shader "Battlehub/Mobile/Particles/HB_Multiply" {
Properties {
	_MainTex ("Particle Texture", 2D) = "white" {}

}

Category {
	Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
	Blend Zero SrcColor
	Cull Off Lighting Off ZWrite Off Fog { Color (1,1,1,1) }
	
	SubShader{
		Pass{

			CGPROGRAM
			
			
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_particles
			#pragma multi_compile_fog
	
			#include "UnityCG.cginc"
			#include "../CGIncludes/HB_Core.cginc"

			sampler2D _MainTex;
			fixed4 _TintColor;

			struct appdata_t {
				float4 vertex : POSITION;
				fixed4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f {
				float4 vertex : SV_POSITION;
				fixed4 color : COLOR;
				float2 texcoord : TEXCOORD0;
				UNITY_FOG_COORDS(1)
			};

			float4 _MainTex_ST;

			v2f vert(appdata_t v)
			{
				v2f o;
				
				HB(v.vertex)
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.color = v.color;
				o.texcoord = TRANSFORM_TEX(v.texcoord,_MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}

			

			fixed4 frag(v2f i) : SV_Target
			{
				half4 prev = i.color * tex2D(_MainTex, i.texcoord);
				fixed4 col = lerp(half4(1,1,1,1), prev, prev.a);
				//UNITY_APPLY_FOG_COLOR(i.fogCoord, col, fixed4(1,1,1,1)); // fog towards white due to our blend mode
				UNITY_APPLY_FOG(i.fogCoord, col); //replicate original mobile shader
				return col;
			}
			ENDCG
		}
	}
}
Fallback "Mobile/Particles/Multiply"
}
