//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Input/PauseMenuControls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PauseMenuControls: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PauseMenuControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PauseMenuControls"",
    ""maps"": [
        {
            ""name"": ""PauseMenu"",
            ""id"": ""a58856db-67d4-4ea0-993d-9acc2b0099f3"",
            ""actions"": [
                {
                    ""name"": ""Escape"",
                    ""type"": ""Button"",
                    ""id"": ""0c8b8584-3140-4f25-891d-c997250e8c67"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""9125543d-9fce-439e-95c9-cfe79e8d16e8"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Escape"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PauseMenu
        m_PauseMenu = asset.FindActionMap("PauseMenu", throwIfNotFound: true);
        m_PauseMenu_Escape = m_PauseMenu.FindAction("Escape", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // PauseMenu
    private readonly InputActionMap m_PauseMenu;
    private List<IPauseMenuActions> m_PauseMenuActionsCallbackInterfaces = new List<IPauseMenuActions>();
    private readonly InputAction m_PauseMenu_Escape;
    public struct PauseMenuActions
    {
        private @PauseMenuControls m_Wrapper;
        public PauseMenuActions(@PauseMenuControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Escape => m_Wrapper.m_PauseMenu_Escape;
        public InputActionMap Get() { return m_Wrapper.m_PauseMenu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PauseMenuActions set) { return set.Get(); }
        public void AddCallbacks(IPauseMenuActions instance)
        {
            if (instance == null || m_Wrapper.m_PauseMenuActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PauseMenuActionsCallbackInterfaces.Add(instance);
            @Escape.started += instance.OnEscape;
            @Escape.performed += instance.OnEscape;
            @Escape.canceled += instance.OnEscape;
        }

        private void UnregisterCallbacks(IPauseMenuActions instance)
        {
            @Escape.started -= instance.OnEscape;
            @Escape.performed -= instance.OnEscape;
            @Escape.canceled -= instance.OnEscape;
        }

        public void RemoveCallbacks(IPauseMenuActions instance)
        {
            if (m_Wrapper.m_PauseMenuActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPauseMenuActions instance)
        {
            foreach (var item in m_Wrapper.m_PauseMenuActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PauseMenuActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PauseMenuActions @PauseMenu => new PauseMenuActions(this);
    public interface IPauseMenuActions
    {
        void OnEscape(InputAction.CallbackContext context);
    }
}
