Shader "Custom/BlurEffect"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" { }
        _BlurAmount ("Blur Amount", Range(0, 10)) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            float _BlurAmount;

            v2f vert(appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
                half4 col = tex2D(_MainTex, i.uv);

                // Apply a simple blur using surrounding pixels
                col += tex2D(_MainTex, i.uv + float2(0.01, 0)) * _BlurAmount;
                col += tex2D(_MainTex, i.uv + float2(0, 0.01)) * _BlurAmount;
                col += tex2D(_MainTex, i.uv + float2(-0.01, 0)) * _BlurAmount;
                col += tex2D(_MainTex, i.uv + float2(0, -0.01)) * _BlurAmount;

                return col / (1 + 4 * _BlurAmount);  // Normalize the result
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
