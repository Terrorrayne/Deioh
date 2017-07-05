// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// todo: implement scrolling effect to move source image up/down along uv's

Shader "Deioh/PostProcess/DeathCameraFX" {
	Properties {
		_MainTex ("Input Image (RGB)", 2D) = "white" {}
	
		_RedShades ("Red Shades", Range(2,256)) = 60
		_GreenShades ("Green Shades", Range(2,256)) = 60
		_BlueShades ("Blue Shades", Range(2,256)) = 60
		
		_GammaCorrection ("Gamma Adjust", Range(0.1, 2.0)) = 0.8

		_Scroll ("Scroll", Range(-1.0, 1.0)) = 0.0

		_WarpIntensity ("WarpIntensity", Range(0.0, 1.5)) = 0.5

		_Stretching ("Stretching", Range(0.0, 5.0)) = 1.0
	}
	SubShader {
		Pass {
		CGPROGRAM

		#pragma vertex vert 
		#pragma fragment frag
		
		#include "UnityCG.cginc"
		
		sampler2D _MainTex;
		int _RedShades;
		int _GreenShades;
		int _BlueShades;
		float _GammaCorrection;
		float _Scroll;
		float _WarpIntensity;
		float _Stretching;
		
		struct v2f {
			float4 pos : SV_POSITION;
			float2 uv : TEXCOORD0;
		};
		
		float4 _MainTex_ST;

		v2f vert(appdata_base v) 
		{
			v2f o;
			o.pos = UnityObjectToClipPos(v.vertex);  
			o.uv = TRANSFORM_TEX (v.texcoord, _MainTex);
			return o;
		}

		fixed4 frag(v2f i) : SV_Target
		{
			// stretch it
			i.uv.y = i.uv.y *_Stretching * _Stretching;

			// scroll image
			i.uv.y = (i.uv.y + _Scroll * _Stretching + 1) % 1;

			// fish eye
			half2 coords = i.uv;
			coords = (coords - 0.5) * 2.0;		
		
			half2 realCoordOffs;
			realCoordOffs.x = (1-coords.y * coords.y) * _WarpIntensity * (coords.x); 
			realCoordOffs.y = (1-coords.x * coords.x) * _WarpIntensity * (coords.y);
		
			half4 color = tex2D (_MainTex, UnityStereoScreenSpaceUVAdjust(i.uv - realCoordOffs, _MainTex_ST));



			// reduce color depth
			float4 pixel = pow(color, 0.8);
			pixel.r = round(pixel.r * (_RedShades - 1)) / _RedShades;
			pixel.g = round(pixel.g * (_GreenShades - 1)) / _GreenShades;
			pixel.b = round(pixel.b * (_BlueShades - 1)) / _BlueShades;

			pixel = pow(pixel, _GammaCorrection);
			pixel.a = 0.2f;
			return pixel;
		}

		ENDCG
		}
	}
	FallBack "Diffuse"
}