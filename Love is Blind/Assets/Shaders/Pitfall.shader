Shader "Custom/Pitfall" {
	Properties {
		_MainTex ("Diffuse", 2D) = "white" {}
	}
	SubShader {
		// Before the common Geometry
		Tags { "RenderType"="Transparent" "Queue"="Geometry-1" }

		Blend SrcAlpha OneMinusSrcAlpha

		// Don't draw this mesh
		ColorMask 0
		// Don't save in ZBuffer
		ZWrite off

		Stencil
		{
			// Write 1 to the Stencil buffer
			Ref 1
			// For all pixels
			Comp always
			// Replace all pixels in the Stencil buffer (1)
			// Pass is a DrawCall
			Pass replace
		}
		
		CGPROGRAM
		#pragma surface surf Lambert


		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutput o) 
		{
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
