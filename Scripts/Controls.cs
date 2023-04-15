//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.5.1
//     from Assets/Scripts/Controls.inputactions
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

public partial class @Controls: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""UI Interact"",
            ""id"": ""848ad2e3-0515-45ce-bf87-32657e564cbe"",
            ""actions"": [
                {
                    ""name"": ""MousePos"",
                    ""type"": ""Value"",
                    ""id"": ""29e036b6-60fe-44ef-b30a-1a2c86de56ed"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""LeftClick"",
                    ""type"": ""Value"",
                    ""id"": ""a26eed5d-b322-46bd-8425-b8e16afec74a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""RightClick"",
                    ""type"": ""Button"",
                    ""id"": ""7da02ffb-b494-4d56-8da2-ca1a08f7e85d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""e7cf2a52-8e8d-43d4-8f4c-1117f4a0c64b"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePos"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""85181ca2-37cb-4a75-ad58-13418e24d875"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9af22285-59fb-452c-8baa-f8549bb63080"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // UI Interact
        m_UIInteract = asset.FindActionMap("UI Interact", throwIfNotFound: true);
        m_UIInteract_MousePos = m_UIInteract.FindAction("MousePos", throwIfNotFound: true);
        m_UIInteract_LeftClick = m_UIInteract.FindAction("LeftClick", throwIfNotFound: true);
        m_UIInteract_RightClick = m_UIInteract.FindAction("RightClick", throwIfNotFound: true);
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

    // UI Interact
    private readonly InputActionMap m_UIInteract;
    private List<IUIInteractActions> m_UIInteractActionsCallbackInterfaces = new List<IUIInteractActions>();
    private readonly InputAction m_UIInteract_MousePos;
    private readonly InputAction m_UIInteract_LeftClick;
    private readonly InputAction m_UIInteract_RightClick;
    public struct UIInteractActions
    {
        private @Controls m_Wrapper;
        public UIInteractActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @MousePos => m_Wrapper.m_UIInteract_MousePos;
        public InputAction @LeftClick => m_Wrapper.m_UIInteract_LeftClick;
        public InputAction @RightClick => m_Wrapper.m_UIInteract_RightClick;
        public InputActionMap Get() { return m_Wrapper.m_UIInteract; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIInteractActions set) { return set.Get(); }
        public void AddCallbacks(IUIInteractActions instance)
        {
            if (instance == null || m_Wrapper.m_UIInteractActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_UIInteractActionsCallbackInterfaces.Add(instance);
            @MousePos.started += instance.OnMousePos;
            @MousePos.performed += instance.OnMousePos;
            @MousePos.canceled += instance.OnMousePos;
            @LeftClick.started += instance.OnLeftClick;
            @LeftClick.performed += instance.OnLeftClick;
            @LeftClick.canceled += instance.OnLeftClick;
            @RightClick.started += instance.OnRightClick;
            @RightClick.performed += instance.OnRightClick;
            @RightClick.canceled += instance.OnRightClick;
        }

        private void UnregisterCallbacks(IUIInteractActions instance)
        {
            @MousePos.started -= instance.OnMousePos;
            @MousePos.performed -= instance.OnMousePos;
            @MousePos.canceled -= instance.OnMousePos;
            @LeftClick.started -= instance.OnLeftClick;
            @LeftClick.performed -= instance.OnLeftClick;
            @LeftClick.canceled -= instance.OnLeftClick;
            @RightClick.started -= instance.OnRightClick;
            @RightClick.performed -= instance.OnRightClick;
            @RightClick.canceled -= instance.OnRightClick;
        }

        public void RemoveCallbacks(IUIInteractActions instance)
        {
            if (m_Wrapper.m_UIInteractActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IUIInteractActions instance)
        {
            foreach (var item in m_Wrapper.m_UIInteractActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_UIInteractActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public UIInteractActions @UIInteract => new UIInteractActions(this);
    public interface IUIInteractActions
    {
        void OnMousePos(InputAction.CallbackContext context);
        void OnLeftClick(InputAction.CallbackContext context);
        void OnRightClick(InputAction.CallbackContext context);
    }
}