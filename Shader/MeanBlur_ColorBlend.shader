// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "MiakCore/MeanBlur_ColorBlend"
{

    Properties
    {
        _MainTex("MainTex", 2D) = "white" {}
        _BlurSize("BlurSize", float) = 1
        _Color("ColorBlend", Color) = (1, 1, 1, 1)
        _BlendWeight("BlendWeight", float) = 0.5
    }

    SubShader
    {
        ZTest Always
        Cull Off
        ZWrite Off
        Fog{ Mode Off }

        Pass
        {

            CGPROGRAM
            #pragma vertex vert  
            #pragma fragment frag  
            #include "UnityCG.cginc"  

            struct VertexOutput
            {
                float4 pos : SV_POSITION;
                float2 uv  : TEXCOORD0;
            };


            sampler2D _MainTex;
            float4 _MainTex_TexelSize;
            float _BlurSize; 
            float4 _Color;
            float _BlendWeight;

            VertexOutput vert(appdata_img v)
            {
                VertexOutput o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.texcoord.xy;
                return o;
            }

            fixed4 frag(VertexOutput i) : SV_Target
            {
                float4 o = 0;
                float sum = 0;
                float2 uvOffset;
                float weight = 1 / (_BlurSize*2 + 1);

                for (int x = -_BlurSize; x <= _BlurSize; ++x) {
                    for (int y = -_BlurSize; y <= _BlurSize; ++y)
                    {
                        uvOffset = i.uv;
                        uvOffset.x += x * _MainTex_TexelSize.x;
                        uvOffset.y += y * _MainTex_TexelSize.y;

                        o += tex2D(_MainTex, uvOffset) * weight;
                        sum += weight;
                    }
                }

                o *= (1.0f / sum);
                o = o * (1 - _BlendWeight) + _Color * _BlendWeight;
                return o;
            }

            ENDCG
        }

    }
}
