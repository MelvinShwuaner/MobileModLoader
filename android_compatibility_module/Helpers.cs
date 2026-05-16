using UnityEngine;

namespace NeoModLoader.AndroidCompatibilityModule;

public class SmoothLoaderHelper
{
    #if IL2CPP
    public static void add(
        Action pAction,
        string pId,
        bool pSkipFrame = false,
        float pNewWaitTimerValue = 0.001f,
        bool pToEnd = false)
    {
        SmoothLoader.add(pAction, pId, pSkipFrame, pNewWaitTimerValue, pToEnd);
    }
    #endif
    public static void add(
        MapLoaderAction pAction,
        string pId,
        bool pSkipFrame = false,
        float pNewWaitTimerValue = 0.001f,
        bool pToEnd = false)
    {
        SmoothLoader.add(pAction, pId, pSkipFrame, pNewWaitTimerValue, pToEnd);
    }
}
public static class GUIHelper
{
    #if IL2CPP
    public static GUIStyle spaceStyle
    {
        get
        {
            field ??= new GUIStyle();
            field.stretchWidth = false;
            return field;
        }
    }
    #else
    public static GUIStyle spaceStyle => GUILayoutUtility.spaceStyle;
    #endif
    public static class Layout{
#if IL2CPP
        public static Rect Window(int id, Rect clientRect, Action<int>  func, string text)
        {
            return GUILayout.Window(id, clientRect, func, text);
        }
        public static Rect Window(int id, Rect clientRect, Action<int>  func, GUIContent text)
        {
            return GUILayout.Window(id, clientRect, func, text);
        }
#endif
        public static Rect Window(int id, Rect clientRect, GUI.WindowFunction  func, string text)
        {
            return GUILayout.Window(id, clientRect, func, text);
        }
        public static Rect Window(int id, Rect clientRect, GUI.WindowFunction func, GUIContent text)
        {
            return GUILayout.Window(id, clientRect, func, text);
        }
        public static GUILayoutOption MinWidth(float minWidth) //cuz the original functions dont fucking work for the most BS reason
        {
            return new GUILayoutOption(GUILayoutOption.Type.minWidth, minWidth);
        }

        public static GUILayoutOption MaxWidth(float maxWidth)
        {
            return new GUILayoutOption(GUILayoutOption.Type.maxWidth, maxWidth);
        }

        public static GUILayoutOption MinHeight(float minHeight)
        {
            return new GUILayoutOption(GUILayoutOption.Type.minHeight, minHeight);
        }

        public static GUILayoutOption MaxHeight(float maxHeight)
        {
            return new GUILayoutOption(GUILayoutOption.Type.maxHeight, maxHeight);
        }

        public static GUILayoutOption ExpandWidth(bool expand)
        {
            return new GUILayoutOption(GUILayoutOption.Type.stretchWidth, (expand ? 1 : 0));
        }

        public static GUILayoutOption ExpandHeight(bool expand)
        {
            return new GUILayoutOption(GUILayoutOption.Type.stretchHeight, (expand ? 1 : 0));
        }
    }
    #if IL2CPP
    public static Rect Window(int id, Rect clientRect, Action<int>  func, string text)
    {
        return GUI.Window(id, clientRect, func, text);
    }
    public static Rect Window(int id, Rect clientRect, Action<int>  func, GUIContent text)
    {
        return GUI.Window(id, clientRect, func, text);
    }
    #endif
    public static Rect Window(int id, Rect clientRect, GUI.WindowFunction  func, string text)
    {
        return GUI.Window(id, clientRect, func, text);
    }
    public static Rect Window(int id, Rect clientRect, GUI.WindowFunction func, GUIContent text)
    {
        return GUI.Window(id, clientRect, func, text);
    }
}

public static class ActionHelper
{
    public static readonly WorldAction Default = IL2CPPHelper.C<WorldAction>((BaseSimObject _, WorldTile _) => true);
}