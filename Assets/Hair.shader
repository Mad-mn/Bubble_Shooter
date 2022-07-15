Shader "CustomColorPattern" {
     Properties{
         _Color1("$$anonymous$$ain Color 1", Color) = (1,1,1,1)
         _Color2("$$anonymous$$ain Color 2", Color) = (1,1,1,1)
         _$$anonymous$$ainTex("$$anonymous$$ask Texture", 2D) = "white" {}
     }
 
         SubShader{
         Tags{ "RenderType" = "Opaque" }
         LOD 100
 
         Pass{
         CGPROGRA$$anonymous$$
 #pragma vertex vert
 #pragma fragment frag
 #pragma multi_compile_fog
 
 #include "UnityCG.cginc"
 
     struct appdata_t {
         float4 vertex : POSITION;
         float4 color    : COLOR;
         float2 texcoord : TEXCOORD0;
     };
 
     struct v2f {
         float4 vertex : SV_POSITION;
         fixed4 color : COLOR;
         half2 texcoord  : TEXCOORD0;
         UNITY_FOG_COORDS(0)
     };
 
     fixed4 _Color1;
     fixed4 _Color2;
 
     v2f vert(appdata_t v)
     {
         v2f o;
         o.vertex = mul(UNITY_$$anonymous$$ATRIX_$$anonymous$$VP, v.vertex);
         o.texcoord = v.texcoord;
         o.color = v.color;
         UNITY_TRANSFER_FOG(o,o.vertex);
         return o;
     }
 
     sampler _$$anonymous$$ainTex;
 
     fixed4 frag(v2f i) : COLOR
     {
         fixed4 c = tex2D(_$$anonymous$$ainTex, i.texcoord);
     if (c.r == 0 && c.g == 0 && c.b == 0)
     {
         c = _Color1;
     }
     else
     {
         c = _Color2;
     }
     UNITY_APPLY_FOG(i.fogCoord, c);
     UNITY_OPAQUE_ALPHA(c.a);
     return c;
     }
         ENDCG
     }
     }
 
 }