﻿using UnityEngine;
using System;
using UnityEngine.Rendering.PostProcessing;


[Serializable]
[PostProcess(renderer: typeof(PixelateRenderer),
    PostProcessEvent.AfterStack,
    "Custom/Pixelate")]


public sealed class Pixelate : PostProcessEffectSettings
{
    [Range(0, 1), Tooltip("Intensidad")]
    public FloatParameter _Strength = new FloatParameter { };
    [/*Range(1, 50), */Tooltip("Pasos de simplificacion del color")]
    public IntParameter _Steps = new IntParameter { };
}

public class PixelateRenderer : PostProcessEffectRenderer<Pixelate>
{
    public override void Render(PostProcessRenderContext context)
    {
        var sheet = context.propertySheets.Get(Shader.Find("Hidden/Custom/Pixelate"));
        sheet.properties.SetFloat("_Steps", settings._Steps);
        sheet.properties.SetFloat("_Strength", settings._Strength);
        //Vector4 texelSize = Vector4.zero;
        //texelSize.z = context.camera.pixelWidth;
        //texelSize.w = context.camera.pixelHeight;
        //texelSize.x = 1f / texelSize.z;
        //texelSize.y = 1f / texelSize.w;
        //sheet.properties.SetVector("_MainTex_TexelSize", texelSize);
        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
    }
}

