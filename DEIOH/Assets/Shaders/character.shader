// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.36 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.36;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:33021,y:32726,varname:node_3138,prsc:2|emission-6020-OUT;n:type:ShaderForge.SFN_Color,id:7241,x:32314,y:32612,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_7241,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.07843138,c2:0.3921569,c3:0.7843137,c4:1;n:type:ShaderForge.SFN_Color,id:1896,x:31986,y:32554,ptovrint:False,ptlb:TopLight,ptin:_TopLight,varname:node_1896,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Color,id:6097,x:31769,y:33127,ptovrint:False,ptlb:ForwardLight,ptin:_ForwardLight,varname:node_6097,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Color,id:3851,x:32197,y:33127,ptovrint:False,ptlb:RimLight,ptin:_RimLight,varname:node_3851,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Fresnel,id:9019,x:31705,y:32901,varname:node_9019,prsc:2;n:type:ShaderForge.SFN_Multiply,id:1320,x:32182,y:32955,varname:node_1320,prsc:2|A-3902-OUT,B-6097-RGB;n:type:ShaderForge.SFN_Slider,id:816,x:31715,y:33405,ptovrint:False,ptlb:RimWidth,ptin:_RimWidth,varname:node_816,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:5.470086,max:20;n:type:ShaderForge.SFN_Fresnel,id:5580,x:32326,y:33286,varname:node_5580,prsc:2|EXP-816-OUT;n:type:ShaderForge.SFN_OneMinus,id:3902,x:31883,y:32925,varname:node_3902,prsc:2|IN-9019-OUT;n:type:ShaderForge.SFN_Add,id:6020,x:32820,y:32915,varname:node_6020,prsc:2|A-9991-OUT,B-1557-OUT;n:type:ShaderForge.SFN_Multiply,id:1557,x:32686,y:33197,varname:node_1557,prsc:2|A-3851-RGB,B-5580-OUT;n:type:ShaderForge.SFN_NormalVector,id:183,x:31811,y:32662,prsc:2,pt:False;n:type:ShaderForge.SFN_Vector3,id:6676,x:31811,y:32821,varname:node_6676,prsc:2,v1:0,v2:1,v3:0;n:type:ShaderForge.SFN_Dot,id:3023,x:31986,y:32775,varname:node_3023,prsc:2,dt:1|A-183-OUT,B-6676-OUT;n:type:ShaderForge.SFN_Multiply,id:1129,x:32202,y:32741,varname:node_1129,prsc:2|A-1896-RGB,B-3023-OUT;n:type:ShaderForge.SFN_Add,id:1382,x:32420,y:32840,varname:node_1382,prsc:2|A-1129-OUT,B-1320-OUT;n:type:ShaderForge.SFN_Multiply,id:9991,x:32608,y:32864,varname:node_9991,prsc:2|A-7241-RGB,B-1382-OUT;n:type:ShaderForge.SFN_NormalVector,id:9653,x:31947,y:33563,prsc:2,pt:False;n:type:ShaderForge.SFN_Dot,id:5906,x:32183,y:33686,varname:node_5906,prsc:2,dt:1|A-9653-OUT,B-4744-OUT;n:type:ShaderForge.SFN_Vector3,id:4744,x:31947,y:33740,varname:node_4744,prsc:2,v1:0,v2:1,v3:-1;n:type:ShaderForge.SFN_OneMinus,id:6472,x:32374,y:33725,varname:node_6472,prsc:2|IN-5906-OUT;proporder:7241-1896-6097-3851-816;pass:END;sub:END;*/

Shader "Shader Forge/character" {
    Properties {
        _Color ("Color", Color) = (0.07843138,0.3921569,0.7843137,1)
        _TopLight ("TopLight", Color) = (0.5,0.5,0.5,1)
        _ForwardLight ("ForwardLight", Color) = (1,1,1,1)
        _RimLight ("RimLight", Color) = (1,1,1,1)
        _RimWidth ("RimWidth", Range(0, 20)) = 5.470086
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
            #pragma multi_compile_fwdbase_fullshadows
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _Color;
            uniform float4 _TopLight;
            uniform float4 _ForwardLight;
            uniform float4 _RimLight;
            uniform float _RimWidth;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos(v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
////// Lighting:
////// Emissive:
                float3 emissive = ((_Color.rgb*((_TopLight.rgb*max(0,dot(i.normalDir,float3(0,1,0))))+((1.0 - (1.0-max(0,dot(normalDirection, viewDirection))))*_ForwardLight.rgb)))+(_RimLight.rgb*pow(1.0-max(0,dot(normalDirection, viewDirection)),_RimWidth)));
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
