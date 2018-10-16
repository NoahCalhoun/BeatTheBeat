﻿Shader "BB/AlphaBlend"
{
	Properties
	{
		_MainTex("Texture", 2D) = "" {}
		_Color("Color", Color) = (1, 1, 1, 1)
	}
		SubShader
	{
		Tags{ "RenderType" = "Transparent" }
		ZWrite On
		ZTest LEqual
		Blend SrcAlpha OneMinusSrcAlpha
		//ColorMaterial AmbientAndDiffuse
		LOD 100

		Pass
	{
		CGPROGRAM
#pragma vertex vert
#pragma fragment frag
		// make fog work
		//#pragma multi_compile_fog

#include "UnityCG.cginc"

		struct appdata
	{
		float4 vertex : POSITION;
		float2 uv : TEXCOORD0;
		fixed4 color : COLOR;
	};

	struct v2f
	{
		float2 uv : TEXCOORD0;
		//UNITY_FOG_COORDS(1)
		float4 vertex : SV_POSITION;
		fixed4 color : COLOR;
	};

	sampler2D _MainTex;
	float4 _MainTex_ST;
	fixed4 _Color;

	v2f vert(appdata v)
	{
		v2f o;
		o.vertex = UnityObjectToClipPos(v.vertex);
		o.uv = TRANSFORM_TEX(v.uv, _MainTex);
		//UNITY_TRANSFER_FOG(o,o.vertex);
		o.color = v.color;
		return o;
	}

	fixed4 frag(v2f i) : SV_Target
	{
		// sample the texture
		fixed4 col = tex2D(_MainTex, i.uv) * _Color;
	//if (col.r + col.b + col.g > 0.5)
	//	col.a = 1;
	//else
	//	col.a = 0;
	// apply fog
	//UNITY_APPLY_FOG(i.fogCoord, col);
	return col;
	}
		ENDCG
	}
	}
}
