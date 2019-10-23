Shader "Custom/VertextStretch"
{

	Properties{
		_Color("Color", Color) = (1,1,1,1)
		_Amount("Height Adjustment", Float) = 1.0
	}
		SubShader{
			Tags { "RenderType" = "Opaque" }
			CGPROGRAM
			#pragma surface surf Lambert vertex:vert
			struct Input {
				float4 Color : COLOR;
			};

		// Access the shaderlab properties
		float _Amount;
		fixed4 _Color;

		// Vertex modifier function
		void vert(inout appdata_full v) {
			// Do whatever you want with the "vertex" property of v here
			if (v.vertex.y > 0)
			{
				v.vertex.y -= _Amount;
			}
		}

		// Surface shader function
		void surf(Input IN, inout SurfaceOutput o) {
			o.Albedo = _Color;
		}
		ENDCG
		
	}
}
