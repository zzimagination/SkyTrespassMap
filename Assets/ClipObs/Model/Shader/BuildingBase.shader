// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "SkyTrespass/Map/BuildingBase"
{
	Properties
	{
		_MainColor("MainColor", Color) = (1,1,1,1)
		_Hight("Hight", Float) = -10
		_DispearColor("DispearColor", Color) = (0,0,0,1)
		_Bottom("Bottom", Float) = -15
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Unlit keepalpha 
		struct Input
		{
			float3 worldPos;
		};

		uniform half4 _DispearColor;
		uniform half4 _MainColor;
		uniform half _Bottom;
		uniform half _Hight;

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float3 ase_vertex3Pos = mul( unity_WorldToObject, float4( i.worldPos , 1 ) );
			half smoothstepResult51 = smoothstep( _Bottom , _Hight , mul( unity_ObjectToWorld, half4( ase_vertex3Pos , 0.0 ) ).xyz.y);
			half4 lerpResult61 = lerp( _DispearColor , _MainColor , smoothstepResult51);
			o.Emission = lerpResult61.rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17101
219;510;1723;1004;882.2978;442.8022;1;True;True
Node;AmplifyShaderEditor.ObjectToWorldMatrixNode;56;-1442.296,-133.032;Inherit;False;0;1;FLOAT4x4;0
Node;AmplifyShaderEditor.PosVertexDataNode;55;-1450.296,-25.03195;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;57;-1182.296,-41.03195;Inherit;False;2;2;0;FLOAT4x4;0,0,0,0,0,1,0,0,0,0,1,0,0,0,0,1;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.BreakToComponentsNode;59;-986.296,3.968048;Inherit;False;FLOAT3;1;0;FLOAT3;0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.RangedFloatNode;41;-973.6133,139.8165;Inherit;False;Property;_Bottom;Bottom;3;0;Create;True;0;0;False;0;-15;-15;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;24;-969.4023,263.6701;Inherit;False;Property;_Hight;Hight;1;0;Create;True;0;0;False;0;-10;-5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SmoothstepOpNode;51;-608.6133,90.81653;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;36;-814.2635,-495.1446;Inherit;False;Property;_DispearColor;DispearColor;2;0;Create;True;0;0;False;0;0,0,0,1;0.5,0.5,0.5,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;1;-848.7761,-279.0189;Inherit;False;Property;_MainColor;MainColor;0;0;Create;True;0;0;False;0;1,1,1,1;0.8490566,0.8490566,0.8490566,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;61;-442.296,-208.032;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;-160,-269;Half;False;True;2;ASEMaterialInspector;0;0;Unlit;SkyTrespass/Map/BuildingBase;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;False;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;1;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;57;0;56;0
WireConnection;57;1;55;0
WireConnection;59;0;57;0
WireConnection;51;0;59;1
WireConnection;51;1;41;0
WireConnection;51;2;24;0
WireConnection;61;0;36;0
WireConnection;61;1;1;0
WireConnection;61;2;51;0
WireConnection;0;2;61;0
ASEEND*/
//CHKSM=20C238E31998E2B32EABA24D008158756F5B5DC9