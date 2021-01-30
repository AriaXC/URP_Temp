using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;


[CreateAssetMenu(menuName = "Rendering/Custom Render Pipeline")]
public class CustomRenderPipelineAssets : RenderPipelineAsset
{

    //负责获取渲染管线的对象实例
    protected override RenderPipeline CreatePipeline()
    {
        return new CustomRenderPipeline();
    }


}
