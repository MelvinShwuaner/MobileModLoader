#if IL2CPP
extern alias Loader;
global using RegisterTypeInIl2Cpp = Loader::ModLoader.RegisterTypeInIl2Cpp;
global using ObjectArray = Il2CppInterop.Runtime.InteropTypes.Arrays.Il2CppReferenceArray<UnityEngine.Object>;
global using SysType = Il2CppSystem.Type;
global using BaseBehaviour = UnityEngine.MonoBehaviour;
#else
global using ObjectArray = UnityEngine.Object[];
global using SysType = System.Type;
global using BaseBehaviour = NeoModLoader.MobileCompatibilityModule.BehaviourStub;
global using Il2CppSystem = System;
#endif