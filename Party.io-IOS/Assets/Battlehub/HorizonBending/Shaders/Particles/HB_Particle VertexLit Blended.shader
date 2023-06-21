// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Battlehub/Particles/HB_VertexLit Blended" {
	Properties{
		_EmisColor("Emissive Color", Color) = (.2,.2,.2,0)
		_MainTex("Particle Texture", 2D) = "white" {}

	}

		SubShader{
			Tags { "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
			Tags { "LightMode" = "Vertex" }
			Cull Off
			Lighting Off
			//Material { Emission[_EmisColor] }
			//ColorMaterial AmbientAndDiffuse
			ZWrite Off
			ColorMask RGB
			Blend SrcAlpha OneMinusSrcAlpha

	Pass
	{
			CGPROGRAM
			
			
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fog
			#include "UnityCG.cginc"
			#include "../CGIncludes/HB_Core.cginc"

			struct v2f {
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				fixed4 diff : COLOR0;
				float4 pos : SV_POSITION;
			};

			uniform fixed4 _EmisColor;
			uniform float4 _MainTex_ST;

			v2f vert(appdata_base v)
			{
				v2f o;
				
				HB(v.vertex)
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.texcoord,_MainTex);
				float4 diffuse = float4(ShadeVertexLightsFull(v.vertex, v.normal, 4, true) * 0.5, 1.0f);
				o.diff = diffuse + _EmisColor;
				UNITY_TRANSFER_FOG(o, o.pos);
				return o;
			}

			uniform sampler2D _MainTex;

			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 temp = tex2D(_MainTex, i.uv);
				fixed4 c;
				c.xyz = temp.xyz * i.diff.xyz;
				c.w = temp.w * i.diff.w;
				UNITY_APPLY_FOG(i.fogCoord, c);
				
				return c;
			}
			ENDCG
	}
}

Fallback "Particles/VertexLit Blended" 
}