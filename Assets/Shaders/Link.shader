Shader "Custom/Link" {
	Properties {
		_MainTex ("Albedo", 2D) = "white" {}
		_Color1 ("Color1", Color) = (1,1,1,1)
		_Color2 ("Color2", Color) = (1,1,1,1)
		_Emit ("Emit", Float) = 1
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		uniform sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

		fixed _Emit;
		fixed4 _Color1;
		fixed4 _Color2;

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			float s = tex2D (_MainTex, float2(IN.uv_MainTex.y + _Time.y, IN.uv_MainTex.x)).x;
			fixed4 c = s*_Color1 + (1-s)*_Color2;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
			o.Emission = _Emit * c.rgb;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
