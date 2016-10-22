using UnityEngine;

namespace WorkshopVR
{
    // Interact with object using the controller in a close and a far mode     

    [RequireComponent(typeof(SteamVR_TrackedObject))]
    public class ViveInput : MonoBehaviour
    {
        // Attributes
        private int m_ControllerIndex = 0;
        private SteamVR_Controller.Device m_SteamVRControllerDevice = null;

        void Start()
        {
            //Get the index this controller
            m_ControllerIndex = (int)GetComponent<SteamVR_TrackedObject>().index;
            m_SteamVRControllerDevice = SteamVR_Controller.Input(m_ControllerIndex);
        }

        #region Transform
        public Vector3 GetPosition()
        {
            return m_SteamVRControllerDevice.transform.pos;
        }

        public Quaternion GetRotation()
        {
            return m_SteamVRControllerDevice.transform.rot;
        }
        #endregion

        #region Velocity
        public Vector3 GetVelocity()
        {            
            return m_SteamVRControllerDevice.velocity;
        }

        public Vector3 GetAngularVelocity()
        {
            return m_SteamVRControllerDevice.angularVelocity;
        }
        #endregion

        #region Trigger Input
        public bool IsTriggerDown()
        {
            return IsButtonPressed(m_ControllerIndex, ButtonPressTypes.PressDown, SteamVR_Controller.ButtonMask.Trigger);
        }

        public bool IsTriggerPressed()
        {
            return IsButtonPressed(m_ControllerIndex, ButtonPressTypes.Press, SteamVR_Controller.ButtonMask.Trigger);
        }

        public bool IsTriggerUp()
        {
            return IsButtonPressed(m_ControllerIndex, ButtonPressTypes.PressUp, SteamVR_Controller.ButtonMask.Trigger);
        }
        #endregion

        #region Grip Input
        public bool IsGripDown()
        {
            return IsButtonPressed(m_ControllerIndex, ButtonPressTypes.PressDown, SteamVR_Controller.ButtonMask.Grip);
        }

        public bool IsGripPressed()
        {
            return IsButtonPressed(m_ControllerIndex, ButtonPressTypes.Press, SteamVR_Controller.ButtonMask.Grip);
        }

        public bool IsGripUp()
        {
            return IsButtonPressed(m_ControllerIndex, ButtonPressTypes.PressUp, SteamVR_Controller.ButtonMask.Grip);
        }
        #endregion

        #region Touchpad Input
        public bool IsTouchpadDown()
        {
            return IsButtonPressed(m_ControllerIndex, ButtonPressTypes.PressDown, SteamVR_Controller.ButtonMask.Touchpad);
        }

        public bool IsTouchpadPressed()
        {
            return IsButtonPressed(m_ControllerIndex, ButtonPressTypes.Press, SteamVR_Controller.ButtonMask.Touchpad);
        }

        public bool IsTouchpadUp()
        {
            return IsButtonPressed(m_ControllerIndex, ButtonPressTypes.PressUp, SteamVR_Controller.ButtonMask.Touchpad);
        }

        public bool IsTouchpadTouchDown()
        {
            return IsButtonPressed(m_ControllerIndex, ButtonPressTypes.TouchDown, SteamVR_Controller.ButtonMask.Touchpad);
        }

        public bool IsTouchpadTouched()
        {
            return IsButtonPressed(m_ControllerIndex, ButtonPressTypes.Touch, SteamVR_Controller.ButtonMask.Touchpad);
        }

        public bool IsTouchpadTouchUp()
        {
            return IsButtonPressed(m_ControllerIndex, ButtonPressTypes.TouchUp, SteamVR_Controller.ButtonMask.Touchpad);
        }

        public Vector2 GetTouchpadAxis()
        {
            return m_SteamVRControllerDevice.GetAxis();
        }
        #endregion

        #region ApplicationMenu Input
        public bool IsApplicationMenuDown()
        {
            return IsButtonPressed(m_ControllerIndex, ButtonPressTypes.PressDown, SteamVR_Controller.ButtonMask.ApplicationMenu);
        }

        public bool IsApplicationMenuPressed()
        {
            return IsButtonPressed(m_ControllerIndex, ButtonPressTypes.Press, SteamVR_Controller.ButtonMask.ApplicationMenu);
        }

        public bool IsApplicationMenuUp()
        {
            return IsButtonPressed(m_ControllerIndex, ButtonPressTypes.PressUp, SteamVR_Controller.ButtonMask.ApplicationMenu);
        }
        #endregion

        #region Static SteamVR Controller Input 
        public enum ButtonPressTypes
        {
            Press,
            PressDown,
            PressUp,
            Touch,
            TouchDown,
            TouchUp
        }

        public static bool IsButtonPressed(int index, ButtonPressTypes type, ulong button)
        {
            if (index >= int.MaxValue)
            {
                return false;
            }

            SteamVR_Controller.Device device = SteamVR_Controller.Input(index);

            switch (type)
            {
                case ButtonPressTypes.Press:
                    return device.GetPress(button);
                case ButtonPressTypes.PressDown:
                    return device.GetPressDown(button);
                case ButtonPressTypes.PressUp:
                    return device.GetPressUp(button);
                case ButtonPressTypes.Touch:
                    return device.GetTouch(button);
                case ButtonPressTypes.TouchDown:
                    return device.GetTouchDown(button);
                case ButtonPressTypes.TouchUp:
                    return device.GetTouchUp(button);
            }

            return false;
        }
        #endregion
    }
}
