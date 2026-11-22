
Shader "Custom/Pixelation"
{
    Properties
    {
        _PixelSize ("Pixel Size", Range(1, 16)) = 4
    }

    SubShader
    {
        Tags { "RenderPipeline" = "UniversalPipeline" }

        Pass
        {
            ZWrite Off
            ZTest Always
            Cull Off

            HLSLPROGRAM
            #pragma vertex Vert
            #pragma fragment Frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.core/Runtime/Utilities/Blit.hlsl"

            float _PixelSize;

            half4 Frag(Varyings input) : SV_Target
            {
                float2 screenSize = _ScreenParams.xy;

                float2 pixelUV = floor(
                    input.texcoord * screenSize / _PixelSize
                ) * _PixelSize / screenSize;

                return SAMPLE_TEXTURE2D_X(
                    _BlitTexture,
                    sampler_LinearClamp,
                    pixelUV
                );
            }
            ENDHLSL
        }
    }
}
