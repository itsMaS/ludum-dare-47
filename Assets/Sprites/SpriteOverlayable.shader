Shader "Hidden/SpriteOvlerlayable"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_OverlayColor("Color", Color) = (1,1,1,1)
		_OverlayLerp("Tween", Range(0,1)) = 0
	}
    SubShader
    {
		Tags
		{
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
		}
        Pass
        {
			Lighting Off
			Blend SrcAlpha OneMinusSrcAlpha
			ZWrite Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
				return o;
            }

            sampler2D _MainTex;
			float4 _OverlayColor;
			float _OverlayLerp;

            fixed4 frag (v2f i) : SV_Target
            {
				float4 color1 = tex2D(_MainTex, i.uv);
				float4 color2 = _OverlayColor;
				color2.a = tex2D(_MainTex, i.uv).a;
				float4 color = lerp(color1, color2, _OverlayLerp);
				return color;
            }
            ENDCG
        }
    }
}
