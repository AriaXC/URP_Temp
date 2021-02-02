using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;


//一个对象实例
public class CustomRenderPipeline : RenderPipeline
{
    CameraRenderer cameraRender = new CameraRenderer();

    //每一帧都调用
    //按照相机的顺序进行渲染
    protected override void Render(ScriptableRenderContext context, Camera[] cameras)
    {
        foreach (var item in cameras)
        {
            cameraRender.Render(context, item);
        }
    }
}
