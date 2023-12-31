// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'



Shader "Battlehub/Legacy Shaders/Self-Illumin/HB_VertexLit" {
Properties {
	_Color ("Main Color", Color) = (1,1,1,1)
	//_SpecColor ("Spec Color", Color) = (1,1,1,1)
	//_Shininess ("Shininess", Range (0.1, 1)) = 0.7
	_MainTex ("Base (RGB)", 2D) = "white" {}
	//_Illum ("Illumin (A)", 2D) = "white" {}
	//_Emission("Emission (Lightmapper)", Float) = 1.0

}

SubShader {
	LOD 100
	Tags { "RenderType"="Opaque" }
	
		Pass{
		Tags{ "LightMode" = "Vertex" }
		Lighting On
		CGPROGRAM
		#include "../../CGIncludes/HB_Core.cginc"
		
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
		//uniform float4 _Emission;

		v2f vert(appdata_base v)
		{
			v2f o;
			
			HB(v.vertex)
			o.pos = UnityObjectToClipPos(v.vertex);
			o.uv = TRANSFORM_TEX(v.texcoord,_MainTex);
			float4 diffuse = float4(ShadeVertexLightsFull(v.vertex, v.normal, 4, true) , 1.0f);
			o.diff = diffuse * _Color;// +_Emission;
			UNITY_TRANSFER_FOG(o,o.pos);
			return o;
		}

		uniform sampler2D _MainTex;

		fixed4 frag(v2f i) : SV_Target
		{
			fixed4 temp = tex2D(_MainTex, i.uv);
			fixed4 c = temp * i.diff;// +_Emission;
			UNITY_APPLY_FOG(i.fogCoord, c);
			UNITY_OPAQUE_ALPHA(c.a);
			return c;
		}
		ENDCG
	}
	//Pass {
	//	Name "BASE"
	//	Tags {"LightMode" = "Vertex"}
	//	Material {
	//		Diffuse [_Color]
	//		Shininess [_Shininess]
	//		Specular [_SpecColor]
	//	}
	//	SeparateSpecular On
	//	Lighting On
	//	SetTexture [_Illum] {
	//		constantColor [_Color]
	//		combine constant lerp (texture) previous
	//	}
	//	SetTexture [_MainTex] {
	//		constantColor (1,1,1,1)
	//		Combine texture * previous, constant // UNITY_OPAQUE_ALPHA_FFP
	//	}

	//	
	//}

	// Extracts information for lightmapping, GI (emission, albedo, ...)
	// This pass it not used during regular rendering.
	Pass
	{
		Name "META" 
		Tags { "LightMode" = "Meta" }
		CGPROGRAM
		#include "../../CGIncludes/HB_Core.cginc"
		
		
		#pragma vertex vert
		#pragma fragment frag
		#include "UnityCG.cginc"
		#include "UnityMetaPass.cginc"
	
		struct v2f
		{
			float4 pos : SV_POSITION;
			float2 uvMain : TEXCOORD0;
			float2 uvIllum : TEXCOORD1;
		};

		float4 _MainTex_ST;
		float4 _Illum_ST;

		v2f vert (appdata_full v)
		{
			v2f o;
			
			HB(v.vertex)
			o.pos = UnityMetaVertexPosition(v.vertex, v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST);
			o.uvMain = TRANSFORM_TEX(v.texcoord, _MainTex);
			o.uvIllum = TRANSFORM_TEX(v.texcoord, _Illum);
			return o;
		}

		sampler2D _MainTex;
		sampler2D _Illum;
		fixed4 _Color;
		fixed _Emission;

		half4 frag (v2f i) : SV_Target
		{
			UnityMetaInput metaIN;
			UNITY_INITIALIZE_OUTPUT(UnityMetaInput, metaIN);

			fixed4 tex = tex2D(_MainTex, i.uvMain);
			fixed4 c = tex * _Color;
			metaIN.Albedo = c.rgb;
			metaIN.Emission = c.rgb * tex2D(_Illum, i.uvIllum).a;
#if defined (UNITY_PASS_META)
			o.Emission *= _Emission.rrr;
#endif
			return UnityMetaFragment(metaIN);
		}
		ENDCG
	}
}

Fallback "Battlehub/Legacy Shaders/HB_VertexLit"
CustomEditor "LegacyIlluminShaderGUI"
}
