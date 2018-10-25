Shader "BB/RGBSeparate"
{
	Properties
	{
		_MainTex("Base (RGB)", 2D) = "white" {}
		fSampleDist("fSampleDist", float) = 0.1
	}

	SubShader
	{

		Pass
		{
			ZTest Always Cull Off ZWrite Off
			Fog{ Mode off }

			CGPROGRAM
			#pragma target 3.0 
			#pragma vertex vert 
			#pragma fragment frag 
			#pragma fragmentoption ARB_precision_hint_fastest

			#include "UnityCG.cginc"

			uniform sampler2D _MainTex;
			uniform float fSampleDist;
			uniform float4 _MainTex_ST;
			uniform float4 _MainTex_TexelSize;

			struct v2f
			{
				float4 pos : POSITION;
				float2 uv : TEXCOORD0;
			};

			v2f vert(appdata_img v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = v.texcoord.xy;
				return o;
			}

			float4 frag(v2f i) : COLOR
			{
				float2 texCo = i.uv;

				float r = min(texCo.x + fSampleDist, 1);
				float g = texCo.x;
				float b = max(texCo.x - fSampleDist, 0);

				return float4(tex2D(_MainTex, float2(r, texCo.y)).r, tex2D(_MainTex, float2(g, texCo.y)).g,
					tex2D(_MainTex, float2(b, texCo.y)).b, 1);
			}
		ENDCG
		}
	}
	Fallback off
}
