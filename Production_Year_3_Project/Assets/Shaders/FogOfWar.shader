Shader "Unlit/FogOfWar"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _PlayerPosition("Player Position", Vector) = (0, 0, 0, 0)
        _FalloffDistance("Falloff Distance", Range(0, 10)) = 5
    }

        SubShader
        {
            Tags { "RenderType" = "Opaque" }
            LOD 100

            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag

                uniform sampler2D _MainTex;
                uniform float4 _PlayerPosition;
                uniform float _FalloffDistance;

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

                v2f vert(appdata v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                   fixed4 color = tex2D(_MainTex, i.uv);
                   float distance = length(i.uv - _PlayerPosition.xy);

                     // Calculate alpha based on distance and falloff distance
                   float alpha = smoothstep(_FalloffDistance, 0, distance);
                   color.a = alpha;

                    return color;
                }
                ENDCG
            }
        }
}
