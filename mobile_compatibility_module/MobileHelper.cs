extern alias Loader;
using HarmonyLib;
using NeoModLoader.constants;

namespace NeoModLoader.MobileCompatibilityModule;
#if IL2CPP
using Loader::ModLoader;
#endif
public static class MobileHelper
{
    #if IL2CPP
    public static string GetPath()
    {
        return Enviornment.MainPath;
    }
    /// <summary>
    /// throws an exception if on il2cpp
    /// </summary>
    public static void Throw(string Feature)
    {
        throw new PlatformNotSupportedException($"{Feature} is not supported on IL2CPP!");
    }
    internal static void Init()
    {
        Log("Initializing android support module");
        //TranspilerSupport.TranspilerSupport.Initialize();
        PCInputSystem.PCInputSystem.Init();
        WrapperHelper.Init();
        GUIStubs.Init();
    }
    /// <summary>
     /// Reads a file in the apk assets directory
     /// </summary>
     /// <param name="assetPath">the path to the file from the assets folder in the apk</param>
     /// <returns>the file bytes, or null if not found</returns>
    public static byte[] ReadAPKAsset(string assetPath)
    {
        return Loader::AssetManager.ReadAssetBytes(assetPath);
    }
    public static void Log(string msg)
    {
        Core.Logger.Msg(msg);
    }
    public static void LogError(string msg)
    {
        Core.Logger.Error(msg);
    }
    public static void LogWarning(string msg)
    {
        Core.Logger.Warning(msg);
    }
    #else
       public static string GetPath()
    {
       return "";
    }
     public static void Log(string msg)
    {
      UnityEngine.Debug.Log(msg);
    }
    public static void LogError(string msg)
    {
       UnityEngine.Debug.LogError(msg);
    }
    public static void LogWarning(string msg)
    {
        UnityEngine.Debug.LogWarning(msg);
    }
    public static byte[] ReadAPKAsset(string assetPath){
        throw new PlatformNotSupportedException("How did we get here?");
    }
    public static void Throw(string Feature){}
    public static void Init(){}
    #endif
}