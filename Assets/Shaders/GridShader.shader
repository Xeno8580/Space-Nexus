
Shader "Custom/GridShader"
{
    Properties
    {
        _BackgroundColor ("Background Color", Color) = (0.015, 0.02, 0.035, 1)
        _GridColor ("Grid Color", Color) = (0.0, 0.5, 0.8, 1)
        _GridSize ("Grid Size", Float) = 1
        _LineThickness ("Line Thickness", Range(0.001, 0.1)) = 0.015
    }

    SubShader
    {
        Tags
        {
            "RenderPipeline" = "UniversalPipeline"
            "RenderType" = "Opaque"
            "Queue" = "Geometry"
        }

        Pass
        {
            HLSLPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            CBUFFER_START(UnityPerMaterial)
                float4 _BackgroundColor;
                float4 _GridColor;
                float _GridSize;
                float _LineThickness;
            CBUFFER_END

            struct Attributes
            {
                float4 positionOS : POSITION;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float3 positionWS : TEXCOORD0;
            };

            Varyings vert(Attributes input)
            {
                Varyings output;

                output.positionHCS =
                    TransformObjectToHClip(input.positionOS.xyz);

                output.positionWS =
                    TransformObjectToWorld(input.positionOS.xyz);

                return output;
            }

            
half4 frag(Varyings input) : SV_Target
{
    float2 grid = input.positionWS.xz / max(_GridSize, 0.001);

    float2 distanceToLine =
        abs(frac(grid - 0.5) - 0.5);

    float2 derivative = fwidth(grid);

    float2 lines = distanceToLine / max(derivative, 0.0001);

    float gridMask = 1.0 - smoothstep(
        _LineThickness,
        _LineThickness + 1.0,
        min(lines.x, lines.y)
    );

    float4 background = _BackgroundColor;
    float4 gridColour = _GridColor;

    return lerp(background, gridColour, gridMask);
}


            ENDHLSL
        }
    }
}
