Shader "Sprites/test2"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
    }

    SubShader
    {
        Tags { "RenderType" = "Opaque" }
        CGPROGRAM
        #pragma surface surf SimpleLambert

        fixed4 _Color;

        half4 LightingSimpleLambert (SurfaceOutput s, half3 lightDir, half atten)
        {
            half NdotL = dot (s.Normal, lightDir);
            half4 c;

            c.rgb = min(s.Albedo * _LightColor0.rgb * (NdotL * atten), _Color.rgb);
            c.a = s.Alpha;
            return c;
        }

        struct Input
        {
            float2 uv_MainTex;
        };
    
        sampler2D _MainTex;
    
        void surf (Input IN, inout SurfaceOutput o)
        {
            o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb;
        }
        
        ENDCG
    }
    
    Fallback "Diffuse"
}