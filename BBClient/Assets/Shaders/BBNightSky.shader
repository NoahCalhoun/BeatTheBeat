Shader "BB/NightSky"
{
	Properties
	{
		_MainTex("Texture", 2D) = "" {}
		_ColorLeft("ColorLeft", Color) = (1, 0, 0, 1)
		_ColorRight("ColorRight", Color) = (0, 0, 1, 1)
	}
		SubShader
	{
		Tags{ "RenderType" = "Transparent" "Queue" = "Background" }
		ZWrite On
		Blend SrcAlpha OneMinusSrcAlpha
		ColorMaterial AmbientAndDiffuse
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
	fixed4 _ColorLeft;
	fixed4 _ColorRight;

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
		fixed4 col = tex2D(_MainTex, i.uv) * i.color;
		float f = i.uv.x - 0.5;
		if (f > 0)
		{
			//col.a = 1.1 - f * 0.5;
			float a = min(max(1.1 - f * 0.5, 0), 1);
			col.rgb = _ColorRight.rgb * (1 - a) + col.rgb * a;
			col.a = 1;
		}
		else
		{
			//col.a = 1.1 + f * 0.5;
			float a = min(max(1.1 + f * 0.5, 0), 1);
			col.rgb = _ColorLeft.rgb * (1 - a) + col.rgb * a;
			col.a = 1;
		}
		//col.a = 1.1 - abs(i.uv.x - 0.5) * 0.5;
	return col;
	}
		ENDCG
	}
	}
}
