// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

Shader "Custom/Particles/VertexLit Blended" {
Properties {
    _EmisColor ("Emissive Color", Color) = (.2,.2,.2,0)
    _MainTex ("Particle Texture", 2D) = "white" {}
}

SubShader {
    Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "PreviewType"="Plane" }
    Tags { "LightMode" = "Vertex" }

	Stencil
	{
		// Write 1 to the Stencil Buffer
		Ref 1
		// If the value in this stencil buffer is different in the previous added stencil buffer
		Comp notequal //notequal
		// Keep the pixel that belongs to this mesh
		Pass keep
	}

    Cull Off
    Lighting On
    Material { Emission [_EmisColor] }
    ColorMaterial AmbientAndDiffuse
    ZWrite Off
    ColorMask RGB
    Blend SrcAlpha OneMinusSrcAlpha
    Pass {
        SetTexture [_MainTex] { combine primary * texture }
    }
}
}
