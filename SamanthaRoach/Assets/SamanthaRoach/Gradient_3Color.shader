// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Gradient_3Color" {
	Properties{
		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
		_ColorA("_ColorA", Color) = (1,1,1,1)
		_ColorB("_ColorB", Color) = (1,1,1,1)
		_Middle("Middle", Range(0.001, 0.999)) = 1
	}

		SubShader{
			Tags {"Queue" = "Background"  "IgnoreProjector" = "True"}
			LOD 100

			ZWrite On

			Pass {
			CGPROGRAM
			#pragma vertex vert  
			#pragma fragment frag
			#include "UnityCG.cginc"

			fixed3 _ColorA;
			fixed3 _ColorB;
			float  _Middle;

			struct v2f {
				float4 pos : SV_POSITION;
				float4 texcoord : TEXCOORD0;
			};

			v2f vert(appdata_full v) {
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.texcoord = v.texcoord;
				return o;
			}

			fixed3 frag(v2f i) : COLOR {
				fixed3 c = lerp(_ColorA, _ColorB, (i.texcoord.y - _Middle)) ;
				//fixed4 c = lerp(_ColorB, _ColorA, (i.texcoord.y - _Middle) / (1 - _Middle)) * step(_Middle, i.texcoord.y);
				//c.a = 1;
				return c;
			}
			ENDCG
			}
		}
}