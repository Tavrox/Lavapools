Shader "Hidden/Pixelation Effect"
{
  Properties
  {
    _MainTex ("Main Texture", 2D) = "white" {}
    _DownsampledTexture ("Downsampled Texture", 2D) = "white" {}
  }
 
     
Subshader {
      ZTest Always Cull Off ZWrite Off
      Fog { Mode off }
 
 Pass {
 
      CGPROGRAM
      #pragma vertex vert_img
      #pragma fragment frag
      #pragma fragmentoption ARB_precision_hint_fastest
     
      #include "UnityCG.cginc"
     
      uniform float4 _RectangleArea;
      sampler2D _MainTex;
      sampler2D _DownsampledTexture;
      float4 _MainTex_TexelSize;
 
      float4 frag(v2f_img i) : COLOR
      {
        // make sure uv is the right way up
        #if UNITY_UV_STARTS_AT_TOP
        if (_MainTex_TexelSize.y < 0)
             i.uv.y = 1.0 - i.uv.y;
        #endif
       
        // start with original render texture
        float4 col = tex2D(_MainTex,i.uv);
       
        // if within rectangle, replace with downsampled version.
        if(i.uv.x > _RectangleArea.x  i.uv.x < _RectangleArea.z)
        {
            if(i.uv.y >_RectangleArea.y  i.uv.y < _RectangleArea.w)
            {
                col = tex2D(_DownsampledTexture,i.uv);
            }
        }
 
        return col;
      }
     
      ENDCG
     
   }
}
  Fallback off
}