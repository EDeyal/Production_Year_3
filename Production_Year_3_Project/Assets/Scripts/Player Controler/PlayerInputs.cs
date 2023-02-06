//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/Scripts/Player Controler/PlayerInputs.inputactions
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

public partial class @PlayerInputs : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputs()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputs"",
    ""maps"": [
        {
            ""name"": ""BasicActions"",
            ""id"": ""586535c3-af1b-42d7-9f2a-ec222f0b89fa"",
            ""actions"": [
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""b0d426ba-bafc-4e80-bb78-b12f58e8e85a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold(duration=0.08)"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""62929b8f-73ca-4973-8f7d-74e4dba5c31a"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""BasicAttack"",
                    ""type"": ""Button"",
                    ""id"": ""39d03e8c-25e2-4a44-8794-6b86f2d8e6b1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SpellAttack"",
                    ""type"": ""Button"",
                    ""id"": ""b7dc13fd-3675-40a1-b8f0-179522c8d5e5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Dash"",
                    ""type"": ""Button"",
                    ""id"": ""14572b06-0b28-4703-8301-7ed08c33fb03"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""3fd18762-d6a4-484a-9806-eea2a8841f31"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e87c4b20-4982-432e-9e32-54727dc79e18"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d97b425e-fcef-45d1-bdd4-60dd5966c965"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SpellAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1bde7d55-4ccc-43a3-842d-77d69765cbb4"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SpellAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""7a49a404-172a-4c83-9b02-2e1c98b48f64"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""down"",
                    ""id"": ""27b92977-226a-494a-b91d-f07bad1a4679"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""0ac87d90-3f60-4a5f-9318-6722400ebb2c"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""58a4e8f3-f689-4b66-a9c7-472fb0295d39"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D vector"",
                    ""id"": ""1866a399-fca7-4ded-b572-e47a114e3ba8"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""down"",
                    ""id"": ""d59fa72c-9a24-4ce8-8919-6fab18ef6c5a"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""a72d23d4-953e-4d94-968b-ebbd20930040"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""c62bcd7c-c863-4f19-9d91-9f5b3803866c"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""f7ad19c2-b9d0-43ee-b872-e94af42ea9ff"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""34ec8852-4375-4536-b360-5d948284616f"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""BasicAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Camera"",
            ""id"": ""0d62bb39-2999-4948-8f42-5dc45e14e809"",
            ""actions"": [
                {
                    ""name"": ""MoveDown"",
                    ""type"": ""Button"",
                    ""id"": ""e741b71e-097c-40d5-b675-6c2f16ad7309"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""097b41dd-5277-4331-9dfb-e87446873c4c"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1043fde1-e94a-4bbc-9a2d-4265b7d5d2bf"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // BasicActions
        m_BasicActions = asset.FindActionMap("BasicActions", throwIfNotFound: true);
        m_BasicActions_Jump = m_BasicActions.FindAction("Jump", throwIfNotFound: true);
        m_BasicActions_Move = m_BasicActions.FindAction("Move", throwIfNotFound: true);
        m_BasicActions_BasicAttack = m_BasicActions.FindAction("BasicAttack", throwIfNotFound: true);
        m_BasicActions_SpellAttack = m_BasicActions.FindAction("SpellAttack", throwIfNotFound: true);
        m_BasicActions_Dash = m_BasicActions.FindAction("Dash", throwIfNotFound: true);
        // Camera
        m_Camera = asset.FindActionMap("Camera", throwIfNotFound: true);
        m_Camera_MoveDown = m_Camera.FindAction("MoveDown", throwIfNotFound: true);
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

    // BasicActions
    private readonly InputActionMap m_BasicActions;
    private IBasicActionsActions m_BasicActionsActionsCallbackInterface;
    private readonly InputAction m_BasicActions_Jump;
    private readonly InputAction m_BasicActions_Move;
    private readonly InputAction m_BasicActions_BasicAttack;
    private readonly InputAction m_BasicActions_SpellAttack;
    private readonly InputAction m_BasicActions_Dash;
    public struct BasicActionsActions
    {
        private @PlayerInputs m_Wrapper;
        public BasicActionsActions(@PlayerInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @Jump => m_Wrapper.m_BasicActions_Jump;
        public InputAction @Move => m_Wrapper.m_BasicActions_Move;
        public InputAction @BasicAttack => m_Wrapper.m_BasicActions_BasicAttack;
        public InputAction @SpellAttack => m_Wrapper.m_BasicActions_SpellAttack;
        public InputAction @Dash => m_Wrapper.m_BasicActions_Dash;
        public InputActionMap Get() { return m_Wrapper.m_BasicActions; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(BasicActionsActions set) { return set.Get(); }
        public void SetCallbacks(IBasicActionsActions instance)
        {
            if (m_Wrapper.m_BasicActionsActionsCallbackInterface != null)
            {
                @Jump.started -= m_Wrapper.m_BasicActionsActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_BasicActionsActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_BasicActionsActionsCallbackInterface.OnJump;
                @Move.started -= m_Wrapper.m_BasicActionsActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_BasicActionsActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_BasicActionsActionsCallbackInterface.OnMove;
                @BasicAttack.started -= m_Wrapper.m_BasicActionsActionsCallbackInterface.OnBasicAttack;
                @BasicAttack.performed -= m_Wrapper.m_BasicActionsActionsCallbackInterface.OnBasicAttack;
                @BasicAttack.canceled -= m_Wrapper.m_BasicActionsActionsCallbackInterface.OnBasicAttack;
                @SpellAttack.started -= m_Wrapper.m_BasicActionsActionsCallbackInterface.OnSpellAttack;
                @SpellAttack.performed -= m_Wrapper.m_BasicActionsActionsCallbackInterface.OnSpellAttack;
                @SpellAttack.canceled -= m_Wrapper.m_BasicActionsActionsCallbackInterface.OnSpellAttack;
                @Dash.started -= m_Wrapper.m_BasicActionsActionsCallbackInterface.OnDash;
                @Dash.performed -= m_Wrapper.m_BasicActionsActionsCallbackInterface.OnDash;
                @Dash.canceled -= m_Wrapper.m_BasicActionsActionsCallbackInterface.OnDash;
            }
            m_Wrapper.m_BasicActionsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @BasicAttack.started += instance.OnBasicAttack;
                @BasicAttack.performed += instance.OnBasicAttack;
                @BasicAttack.canceled += instance.OnBasicAttack;
                @SpellAttack.started += instance.OnSpellAttack;
                @SpellAttack.performed += instance.OnSpellAttack;
                @SpellAttack.canceled += instance.OnSpellAttack;
                @Dash.started += instance.OnDash;
                @Dash.performed += instance.OnDash;
                @Dash.canceled += instance.OnDash;
            }
        }
    }
    public BasicActionsActions @BasicActions => new BasicActionsActions(this);

    // Camera
    private readonly InputActionMap m_Camera;
    private ICameraActions m_CameraActionsCallbackInterface;
    private readonly InputAction m_Camera_MoveDown;
    public struct CameraActions
    {
        private @PlayerInputs m_Wrapper;
        public CameraActions(@PlayerInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @MoveDown => m_Wrapper.m_Camera_MoveDown;
        public InputActionMap Get() { return m_Wrapper.m_Camera; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CameraActions set) { return set.Get(); }
        public void SetCallbacks(ICameraActions instance)
        {
            if (m_Wrapper.m_CameraActionsCallbackInterface != null)
            {
                @MoveDown.started -= m_Wrapper.m_CameraActionsCallbackInterface.OnMoveDown;
                @MoveDown.performed -= m_Wrapper.m_CameraActionsCallbackInterface.OnMoveDown;
                @MoveDown.canceled -= m_Wrapper.m_CameraActionsCallbackInterface.OnMoveDown;
            }
            m_Wrapper.m_CameraActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MoveDown.started += instance.OnMoveDown;
                @MoveDown.performed += instance.OnMoveDown;
                @MoveDown.canceled += instance.OnMoveDown;
            }
        }
    }
    public CameraActions @Camera => new CameraActions(this);
    public interface IBasicActionsActions
    {
        void OnJump(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
        void OnBasicAttack(InputAction.CallbackContext context);
        void OnSpellAttack(InputAction.CallbackContext context);
        void OnDash(InputAction.CallbackContext context);
    }
    public interface ICameraActions
    {
        void OnMoveDown(InputAction.CallbackContext context);
    }
}
