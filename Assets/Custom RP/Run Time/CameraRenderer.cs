using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEditor;


public partial class CameraRenderer 
{
    ScriptableRenderContext context;

    Camera camera;

    const string bufferName = "my Command Render Camera";

    CommandBuffer Cbuffer = new CommandBuffer(){ name = bufferName };

    CullingResults cullingResults;

    //指出使用哪种阴影pass
    static ShaderTagId unlitShader = new ShaderTagId("SRPDefaultUnlit");

    public void Render(ScriptableRenderContext context, Camera camera)
    {
        this.context = context;
        this.camera = camera;
#if UNITY_EDITOR
        CameraBuffer();
        ShowSceneWindow();
#endif
        if (!Cull())
        {
            return;
        }

        Setup();
        DrawVisibleGeometry();
#if UNITY_EDITOR
        DrawUnsupportedShader();
        DrawGizmos();
#endif
        Submit();

    }

    //绘制一个简单的东西
    void DrawVisibleGeometry( )
    {
     
        SortingSettings sortSetting = new SortingSettings(camera) {
            criteria = SortingCriteria.CommonOpaque
        };
        DrawingSettings drawSetting = new DrawingSettings(unlitShader, sortSetting);
        FilteringSettings filterSetting = new FilteringSettings(RenderQueueRange.opaque);

        context.DrawRenderers(cullingResults, ref drawSetting, ref filterSetting);

        context.DrawSkybox(camera);

        sortSetting.criteria = SortingCriteria.CommonTransparent;
        drawSetting.sortingSettings = sortSetting;
        filterSetting.renderQueueRange = RenderQueueRange.transparent;

        context.DrawRenderers(cullingResults,ref drawSetting ,ref filterSetting);

    }
    //提交这个队列  不然是没有效果 只是缓冲
    void Submit()
    {
        Cbuffer.EndSample(bufferName);
        ExecuteCommandBuffer();
        context.Submit();
    }
    //设置视图的投影矩阵什么的
    //说是 之后的天空盒正确呈现 不懂
    void Setup()
    {
        context.SetupCameraProperties(camera);
        //要清除上一帧的画面啊 什么的
        Cbuffer.ClearRenderTarget(true, true, Color.clear);
        Cbuffer.BeginSample(bufferName);
        ExecuteCommandBuffer();
    }
    //清除
    void ExecuteCommandBuffer()
    {
        context.ExecuteCommandBuffer(Cbuffer);
        Cbuffer.Clear();
    }
    //剔除
    bool Cull()
    {
        ScriptableCullingParameters p;
        if (camera.TryGetCullingParameters(out p))
        {
            cullingResults = context.Cull(ref p);
            return true;
        }
        return false;
    }
}
