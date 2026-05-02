using System.Reflection;
using Il2CppInterop.Runtime.Attributes;
using Il2CppInterop.Runtime.Injection;
using MelonLoader;
using NeoModLoader.services;
using NeoModLoader.utils.Collections;
using UnityEngine;
using Object = Il2CppSystem.Object;

namespace NeoModLoader.AndroidCompatibilityModule;
[RegisterTypeInIl2Cpp]
public sealed class Il2CPPBehaviour : MonoBehaviour
{
    private static SlotList<Il2CPPBehaviour> Pool = new();
    public Il2CPPBehaviour(IntPtr ptr) : base(ptr)
    {
    }

    public Il2CPPBehaviour() : base(ClassInjector.DerivedConstructorPointer<Il2CPPBehaviour>())
    {
        ClassInjector.DerivedConstructorBody(this);
    }

    public void OnEnable()
    {
        onEnable?.Invoke(WrappedBehaviour);
    }

    public void Start()
    {
       start?.Invoke(WrappedBehaviour);
    }

    public void OnDisable()
    {
        onDisable?.Invoke(WrappedBehaviour);
    }

    private bool canawake;
    /// <summary>
    /// do not touch this. this is public for Unity to see
    /// </summary>
    public int SlotIndex = -1;
    [HideFromIl2Cpp]
    public void CopyFrom(Il2CPPBehaviour other)
    {
        WrapperResolver.ResolveInstantiate(gameObject, other.gameObject);
    }

    public bool IsSlotIndexValid()
    {
        if (SlotIndex == -1)
        {
            return false;
        }
        return !Pool.Slots.IsEmpty(SlotIndex);
    }
    void CheckIndex()
    {
        if (IsSlotIndexValid())
        {
            if (WrappedBehaviour == null)
            {
                CopyFrom(Pool.Slots[SlotIndex].Item);
                SlotIndex = Pool.Slots.Add(this);
            }
        }
        else
        {
            SlotIndex = Pool.Slots.Add(this);
        }
    }
    public void Awake()
    {
        CheckIndex();
        if (!canawake) return;
        awake?.Invoke(WrappedBehaviour);
        canawake = false;
    }

    public void OnDestroy()
    {
        if (IsSlotIndexValid())
        {
            Pool.Slots.RemoveAt(SlotIndex);
        }
        onDestroy?.Invoke(WrappedBehaviour);
        WrappedBehaviour = null;
    }

    public void Update()
    {
        update?.Invoke(WrappedBehaviour);
    }

    public void LateUpdate()
    {
        lateupdate?.Invoke(WrappedBehaviour);
    }

    public void OnGUI()
    {
        ongui?.Invoke(WrappedBehaviour);
    }
    [HideFromIl2Cpp]
    public WrappedAction GetWrappedMethod(string Method)
    {
        Type type = WrappedType;
        while (type != null)
        {
            var method = type.GetMethod(
                Method,
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.DeclaredOnly, Type.EmptyTypes);
            if (method != null)
                return WrapperHelper.CreateWrappedAction(method);
            type = type.BaseType;
        }
        return null;
    }
    [HideFromIl2Cpp]
    internal B SetWrappedBehaviour<B>(B Behaviour) where B : WrappedBehaviour
    {
        WrappedBehaviour = Behaviour;
        WrappedType = Behaviour.GetType();
        Behaviour.Wrapper = this;
        update = GetWrappedMethod("Update");
        start = GetWrappedMethod("Start");
        awake = GetWrappedMethod("Awake");
        canawake = true;
        if (gameObject.activeInHierarchy)
        {
            Awake();
        }
        ongui = GetWrappedMethod("OnGUI");
        onEnable = GetWrappedMethod("OnEnable");
        onDisable = GetWrappedMethod("OnDisable");
        lateupdate = GetWrappedMethod("LateUpdate");
        onDestroy = GetWrappedMethod("OnDestroy");
        return Behaviour;
    }
    [HideFromIl2Cpp]
    internal WrappedBehaviour CreateWrapperIfNull(Type WrappedType)
    {
        return WrappedBehaviour ?? SetWrappedBehaviour((WrappedBehaviour)Activator.CreateInstance(WrappedType));
    }
    [HideFromIl2Cpp]
    internal W CreateWrapper<W>() where W : WrappedBehaviour
    {
        return SetWrappedBehaviour(Activator.CreateInstance<W>());
    }
    private WrappedAction update;
    private WrappedAction start;
    private WrappedAction awake;
    private WrappedAction ongui;
    private WrappedAction onEnable;
    private WrappedAction onDisable;
    private WrappedAction lateupdate;
    private WrappedAction onDestroy;
    public Type WrappedType { [HideFromIl2Cpp] get; [HideFromIl2Cpp] private set; }
    public WrappedBehaviour WrappedBehaviour {  [HideFromIl2Cpp]get;  [HideFromIl2Cpp]private set; }
}
public delegate void WrappedAction(WrappedBehaviour beh);