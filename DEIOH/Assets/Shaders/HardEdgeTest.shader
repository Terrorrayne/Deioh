// Shader created with Shader Forge v1.37 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.37;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:4013,x:34094,y:32377,varname:node_4013,prsc:2|diff-7374-OUT,emission-4043-OUT,custl-8694-OUT;n:type:ShaderForge.SFN_Color,id:1304,x:32356,y:32676,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_1304,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Vector1,id:3786,x:32661,y:32853,varname:node_3786,prsc:2,v1:0;n:type:ShaderForge.SFN_Fresnel,id:1136,x:32240,y:32966,varname:node_1136,prsc:2;n:type:ShaderForge.SFN_Add,id:3325,x:32619,y:32961,varname:node_3325,prsc:2|A-1136-OUT,B-2952-OUT;n:type:ShaderForge.SFN_Floor,id:2149,x:32789,y:32961,varname:node_2149,prsc:2|IN-3325-OUT;n:type:ShaderForge.SFN_Slider,id:2952,x:32257,y:33177,ptovrint:False,ptlb:Rim,ptin:_Rim,varname:node_2952,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.5384616,max:1;n:type:ShaderForge.SFN_OneMinus,id:2241,x:32986,y:32961,varname:node_2241,prsc:2|IN-2149-OUT;n:type:ShaderForge.SFN_Multiply,id:7374,x:33525,y:32764,varname:node_7374,prsc:2|A-677-OUT,B-2241-OUT,C-9065-OUT;n:type:ShaderForge.SFN_Tex2d,id:1084,x:32356,y:32504,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_1084,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:677,x:33138,y:32669,varname:node_677,prsc:2|A-1084-RGB,B-1304-RGB;n:type:ShaderForge.SFN_LightAttenuation,id:9065,x:33272,y:32915,varname:node_9065,prsc:2;n:type:ShaderForge.SFN_NormalVector,id:7656,x:32490,y:32209,prsc:2,pt:True;n:type:ShaderForge.SFN_LightVector,id:9979,x:32499,y:31961,varname:node_9979,prsc:2;n:type:ShaderForge.SFN_Dot,id:5067,x:32868,y:32100,varname:node_5067,prsc:2,dt:1|A-9979-OUT,B-7656-OUT;n:type:ShaderForge.SFN_Multiply,id:8694,x:33613,y:32410,varname:node_8694,prsc:2|A-1131-RGB,B-5067-OUT,C-7374-OUT;n:type:ShaderForge.SFN_LightColor,id:1131,x:32947,y:31958,varname:node_1131,prsc:2;n:type:ShaderForge.SFN_LightPosition,id:5575,x:32351,y:31961,varname:node_5575,prsc:2;n:type:ShaderForge.SFN_AmbientLight,id:2248,x:33485,y:32996,varname:node_2248,prsc:2;n:type:ShaderForge.SFN_Multiply,id:4043,x:33763,y:32776,varname:node_4043,prsc:2|A-7374-OUT,B-2248-RGB;proporder:1304-2952-1084;pass:END;sub:END;*/

Shader "Shader Forge/HardEdgeTest" {
    Properties {
        _Color ("Color", Color) = (1,1,1,1)
        _Rim ("Rim", Range(0, 1)) = 0.5384616
        _MainTex ("MainTex", 2D) = "white" {}
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _Color;
            uniform float _Rim;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
////// Emissive:
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float3 node_7374 = ((_MainTex_var.rgb*_Color.rgb)*(1.0 - floor(((1.0-max(0,dot(normalDirection, viewDirection)))+_Rim)))*attenuation);
                float3 emissive = (node_7374*UNITY_LIGHTMODEL_AMBIENT.rgb);
                float3 finalColor = emissive + (_LightColor0.rgb*max(0,dot(lightDirection,normalDirection))*node_7374);
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _Color;
            uniform float _Rim;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float3 node_7374 = ((_MainTex_var.rgb*_Color.rgb)*(1.0 - floor(((1.0-max(0,dot(normalDirection, viewDirection)))+_Rim)))*attenuation);
                float3 finalColor = (_LightColor0.rgb*max(0,dot(lightDirection,normalDirection))*node_7374);
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
