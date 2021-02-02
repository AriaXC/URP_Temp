using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEditor;

//分部类 更好的分离代码
public partial class CameraRenderer
{

    //暂时不支持的
    static ShaderTagId[] otherId = {
        new ShaderTagId("Always"),
        new ShaderTagId("ForwardBase"),
        new ShaderTagId("PrepassBase"),
        new ShaderTagId("Vertex"),
        new ShaderTagId("VertexLMRGBM"),
        new ShaderTagId("VertexLM"),
    };

    //显示的
    static Material errorMaterial;

    //绘制不支持的shader
    void DrawUnsupportedShader()
    {
        if (errorMaterial == null)
        {
            errorMaterial = new Material(Shader.Find("Hidden/InternalErrorShader"));
        }

        DrawingSettings drawingSettings = new DrawingSettings(otherId[0], new SortingSettings(camera))
        {
            overrideMaterial = errorMaterial
        };
        for (int i = 1; i < otherId.Length; i++)
        {
            drawingSettings.SetShaderPassName(i, otherId[i]);
        }
        FilteringSettings filtering = FilteringSettings.defaultValue;
        context.DrawRenderers(cullingResults, ref drawingSettings, ref filtering);

    }

    //绘制 
    public void DrawGizmos()
    {
        if (Handles.ShouldRenderGizmos())
        {
            context.DrawGizmos(camera, GizmoSubset.PostImageEffects);
            context.DrawGizmos(camera, GizmoSubset.PreImageEffects);
        }
    }


    public void ShowSceneWindow()
    {
        if (camera.cameraType == CameraType.SceneView)
        {
            ScriptableRenderContext.EmitWorldGeometryForSceneView(camera);
        }
    }

    void CameraBuffer()
    {
        //Cbuffer.BeginSample("UICamera M ");
        Cbuffer.name = camera.name;
        //Cbuffer.EndSample("UICamera M ");
        
    }
}
