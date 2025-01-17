//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/_Scripts/Input/PlayerInputAsset.inputactions
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

public partial class @PlayerInputAsset: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputAsset()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputAsset"",
    ""maps"": [
        {
            ""name"": ""PCMap"",
            ""id"": ""f5f5ce86-952a-46e1-a9a4-c28a3be6eebe"",
            ""actions"": [
                {
                    ""name"": ""Fire"",
                    ""type"": ""Button"",
                    ""id"": ""1341f9f3-4ddf-49a1-b325-74c2fe45fa83"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold(duration=0.01)"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Aim"",
                    ""type"": ""Button"",
                    ""id"": ""92160088-6aaf-4569-9533-99e7caf675a0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""DirectionInput"",
                    ""type"": ""Value"",
                    ""id"": ""fd421da5-9815-4978-9997-c5f9fee9f929"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""RotationInput"",
                    ""type"": ""Value"",
                    ""id"": ""956c0ff0-bc1f-4b55-8c0d-7a1fbd3ec6a7"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Reload"",
                    ""type"": ""Button"",
                    ""id"": ""ab0e50d3-714c-4ac5-9be3-de72848dc9cc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ChangeWeapon"",
                    ""type"": ""Value"",
                    ""id"": ""e5c4888f-8d7b-4f8b-914a-ddbdc096155a"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""d26de448-1dfa-4381-8ce6-4cbd8ae0d14d"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseKeyboard"",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""966909c0-8036-4dd6-8255-aff7d5284686"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseKeyboard"",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""5481efdf-bb4e-4f3f-aff1-3a7e64a59f02"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DirectionInput"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""e35b86d4-e1b6-4822-9616-fd6afbc7f877"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseKeyboard"",
                    ""action"": ""DirectionInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""21d2ba08-7a76-40c9-9fe5-dea26ae82b1b"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseKeyboard"",
                    ""action"": ""DirectionInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""abcdb6ca-b149-407a-a881-cf63a42d474c"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseKeyboard"",
                    ""action"": ""DirectionInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""92343935-cfb7-4ae7-8786-a851d43c518c"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseKeyboard"",
                    ""action"": ""DirectionInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""ee9d0c09-2fd7-424c-b000-08d54b6bcf18"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseKeyboard"",
                    ""action"": ""RotationInput"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9348d166-dd64-4493-b1cb-184209e3faa6"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Reload"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bb447a26-40d6-4956-9646-5b91254bb73a"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseKeyboard"",
                    ""action"": ""ChangeWeapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""MouseKeyboard"",
            ""bindingGroup"": ""MouseKeyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // PCMap
        m_PCMap = asset.FindActionMap("PCMap", throwIfNotFound: true);
        m_PCMap_Fire = m_PCMap.FindAction("Fire", throwIfNotFound: true);
        m_PCMap_Aim = m_PCMap.FindAction("Aim", throwIfNotFound: true);
        m_PCMap_DirectionInput = m_PCMap.FindAction("DirectionInput", throwIfNotFound: true);
        m_PCMap_RotationInput = m_PCMap.FindAction("RotationInput", throwIfNotFound: true);
        m_PCMap_Reload = m_PCMap.FindAction("Reload", throwIfNotFound: true);
        m_PCMap_ChangeWeapon = m_PCMap.FindAction("ChangeWeapon", throwIfNotFound: true);
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

    // PCMap
    private readonly InputActionMap m_PCMap;
    private List<IPCMapActions> m_PCMapActionsCallbackInterfaces = new List<IPCMapActions>();
    private readonly InputAction m_PCMap_Fire;
    private readonly InputAction m_PCMap_Aim;
    private readonly InputAction m_PCMap_DirectionInput;
    private readonly InputAction m_PCMap_RotationInput;
    private readonly InputAction m_PCMap_Reload;
    private readonly InputAction m_PCMap_ChangeWeapon;
    public struct PCMapActions
    {
        private @PlayerInputAsset m_Wrapper;
        public PCMapActions(@PlayerInputAsset wrapper) { m_Wrapper = wrapper; }
        public InputAction @Fire => m_Wrapper.m_PCMap_Fire;
        public InputAction @Aim => m_Wrapper.m_PCMap_Aim;
        public InputAction @DirectionInput => m_Wrapper.m_PCMap_DirectionInput;
        public InputAction @RotationInput => m_Wrapper.m_PCMap_RotationInput;
        public InputAction @Reload => m_Wrapper.m_PCMap_Reload;
        public InputAction @ChangeWeapon => m_Wrapper.m_PCMap_ChangeWeapon;
        public InputActionMap Get() { return m_Wrapper.m_PCMap; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PCMapActions set) { return set.Get(); }
        public void AddCallbacks(IPCMapActions instance)
        {
            if (instance == null || m_Wrapper.m_PCMapActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PCMapActionsCallbackInterfaces.Add(instance);
            @Fire.started += instance.OnFire;
            @Fire.performed += instance.OnFire;
            @Fire.canceled += instance.OnFire;
            @Aim.started += instance.OnAim;
            @Aim.performed += instance.OnAim;
            @Aim.canceled += instance.OnAim;
            @DirectionInput.started += instance.OnDirectionInput;
            @DirectionInput.performed += instance.OnDirectionInput;
            @DirectionInput.canceled += instance.OnDirectionInput;
            @RotationInput.started += instance.OnRotationInput;
            @RotationInput.performed += instance.OnRotationInput;
            @RotationInput.canceled += instance.OnRotationInput;
            @Reload.started += instance.OnReload;
            @Reload.performed += instance.OnReload;
            @Reload.canceled += instance.OnReload;
            @ChangeWeapon.started += instance.OnChangeWeapon;
            @ChangeWeapon.performed += instance.OnChangeWeapon;
            @ChangeWeapon.canceled += instance.OnChangeWeapon;
        }

        private void UnregisterCallbacks(IPCMapActions instance)
        {
            @Fire.started -= instance.OnFire;
            @Fire.performed -= instance.OnFire;
            @Fire.canceled -= instance.OnFire;
            @Aim.started -= instance.OnAim;
            @Aim.performed -= instance.OnAim;
            @Aim.canceled -= instance.OnAim;
            @DirectionInput.started -= instance.OnDirectionInput;
            @DirectionInput.performed -= instance.OnDirectionInput;
            @DirectionInput.canceled -= instance.OnDirectionInput;
            @RotationInput.started -= instance.OnRotationInput;
            @RotationInput.performed -= instance.OnRotationInput;
            @RotationInput.canceled -= instance.OnRotationInput;
            @Reload.started -= instance.OnReload;
            @Reload.performed -= instance.OnReload;
            @Reload.canceled -= instance.OnReload;
            @ChangeWeapon.started -= instance.OnChangeWeapon;
            @ChangeWeapon.performed -= instance.OnChangeWeapon;
            @ChangeWeapon.canceled -= instance.OnChangeWeapon;
        }

        public void RemoveCallbacks(IPCMapActions instance)
        {
            if (m_Wrapper.m_PCMapActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPCMapActions instance)
        {
            foreach (var item in m_Wrapper.m_PCMapActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PCMapActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PCMapActions @PCMap => new PCMapActions(this);
    private int m_MouseKeyboardSchemeIndex = -1;
    public InputControlScheme MouseKeyboardScheme
    {
        get
        {
            if (m_MouseKeyboardSchemeIndex == -1) m_MouseKeyboardSchemeIndex = asset.FindControlSchemeIndex("MouseKeyboard");
            return asset.controlSchemes[m_MouseKeyboardSchemeIndex];
        }
    }
    public interface IPCMapActions
    {
        void OnFire(InputAction.CallbackContext context);
        void OnAim(InputAction.CallbackContext context);
        void OnDirectionInput(InputAction.CallbackContext context);
        void OnRotationInput(InputAction.CallbackContext context);
        void OnReload(InputAction.CallbackContext context);
        void OnChangeWeapon(InputAction.CallbackContext context);
    }
}
