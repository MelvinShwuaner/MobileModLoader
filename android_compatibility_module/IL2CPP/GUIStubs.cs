using System.Reflection;
using HarmonyLib;
using NeoModLoader.constants;
using NeoModLoader.services;
using UnityEngine;
using Object = UnityEngine.Object;
using OpCodes = System.Reflection.Emit.OpCodes;

namespace NeoModLoader.AndroidCompatibilityModule;

internal static class GUIStubs //fixes GUI stub methods
{
    private static Harmony harmony;

    static void Transpile(MethodBase method, Delegate @delegate)
    {
        harmony.Patch(method, null, null, new HarmonyMethod(@delegate));
    }
    public static void Init()
    {
        harmony = new Harmony(Others.harmony_id);
        Transpile(AccessTools.Method(typeof(GUILayout), nameof(GUILayout.Space)), SpaceFix);
        Transpile(AccessTools.Method(typeof(GUI), nameof(GUI.DrawTexture), [typeof(Rect), typeof(Texture), typeof(ScaleMode), typeof(bool), typeof(float), typeof(Color), typeof(Color), typeof(Color), typeof(Color), typeof(Vector4), typeof(Vector4), typeof(bool)]), DrawTextureFix);
        Transpile(AccessTools.Method(typeof(GUILayout), nameof(GUILayout.FlexibleSpace)), FlexibleSpaceFix);
    }
    static void Space(float pixels)
    {
        GUIUtility.CheckOnGUI();
        if (GUILayoutUtility.current.topLevel.isVertical)
            GUILayoutUtility.GetRect(0.0f, pixels, GUIHelper.spaceStyle, GUILayout.Height(pixels));
        else
            GUILayoutUtility.GetRect(pixels, 0.0f, GUIHelper.spaceStyle, GUILayout.Width(pixels));
        if (Event.current.type != EventType.Layout)
            return;
        GUILayoutUtility.current.topLevel.entries[GUILayoutUtility.current.topLevel.entries.Count - 1].consideredForMargin = false;
    }
    static void DrawTexture(
        Rect position,
        Texture image,
        ScaleMode scaleMode,
        bool alphaBlend,
        float imageAspect,
        Color leftColor,
        Color topColor,
        Color rightColor,
        Color bottomColor,
        Vector4 borderWidths,
        Vector4 borderRadiuses,
        bool drawSmoothCorners)
    {
        GUIUtility.CheckOnGUI();
        if (Event.current.type != EventType.Repaint)
            return;
        if (image == null)
        {
            Debug.LogWarning("null texture passed to GUI.DrawTexture");
        }
        else
        {
            /*if (imageAspect == 0.0)
                imageAspect = image.width / (float) image.height;
            Material material = !(borderWidths != Vector4.zero) ? (!(borderRadiuses != Vector4.zero) ? (alphaBlend ? GUI.blendMaterial : GUI.blitMaterial) : GUI.roundedRectMaterial) : (!(leftColor != topColor) && !(leftColor != rightColor) && !(leftColor != bottomColor) ? GUI.roundedRectMaterial : GUI.roundedRectWithColorPerBorderMaterial);
            Internal_DrawTextureArguments args = new Internal_DrawTextureArguments()
            {
                leftBorder = 0,
                rightBorder = 0,
                topBorder = 0,
                bottomBorder = 0,
                color = leftColor,
                leftBorderColor = leftColor,
                topBorderColor = topColor,
                rightBorderColor = rightColor,
                bottomBorderColor = bottomColor,
                borderWidths = borderWidths,
                cornerRadiuses = borderRadiuses,
                texture = image,
                smoothCorners = drawSmoothCorners,
                mat = material
            };
            var argsScreenRect = args.screenRect;
            var argsSourceRect = args.sourceRect;
            GUI.CalculateScaledTextureRects(position, scaleMode, imageAspect, ref argsScreenRect, ref argsSourceRect);
            args.screenRect = argsScreenRect;
            args.sourceRect = argsSourceRect;
            Graphics.Internal_DrawTexture(ref args); */ //doesnt fucking work
            Graphics.DrawTexture(position, image);
        }
    }
    static IEnumerable<CodeInstruction> SpaceFix(IEnumerable<CodeInstruction> instructions)
    {
        yield return new CodeInstruction(OpCodes.Ldarg_0);
        yield return new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(GUIStubs), nameof(Space)));
        yield return new CodeInstruction(OpCodes.Ret);
    }
    static IEnumerable<CodeInstruction> DrawTextureFix(IEnumerable<CodeInstruction> instructions)
    {
        for (int i = 0; i < 12; i++) //kill me
        {
            yield return new CodeInstruction(OpCodes.Ldarg, i);
        }
        yield return new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(GUIStubs), nameof(DrawTexture)));
        yield return new CodeInstruction(OpCodes.Ret);
    }
    public static void FlexibleSpace()
    {
        GUIUtility.CheckOnGUI();
        GUILayoutUtility.GetRect(0.0f, 0.0f, GUIHelper.spaceStyle, new GUILayoutOption((!GUILayoutUtility.current.topLevel.isVertical ? GUIHelper.Layout.ExpandWidth(true) : GUIHelper.Layout.ExpandHeight(true)).type, 10000));
        if (Event.current.type != EventType.Layout)
            return;
        GUILayoutUtility.current.topLevel.entries[GUILayoutUtility.current.topLevel.entries.Count - 1].consideredForMargin = false;
    }
    static IEnumerable<CodeInstruction> FlexibleSpaceFix(IEnumerable<CodeInstruction> instructions)
    {
        yield return new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(GUIStubs), nameof(FlexibleSpace)));
        yield return new CodeInstruction(OpCodes.Ret);
    }
}