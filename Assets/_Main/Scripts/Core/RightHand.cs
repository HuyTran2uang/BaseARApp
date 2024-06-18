using UnityEngine;

public class RightHand : MonoBehaviour
{
    private bool _isHandPressing;

    private void Update()
    {
        Input();
    }

    private void Input()
    {
        float primaryHandTrigger = OVRInput.Get(OVRInput.RawAxis1D.RHandTrigger);

        if (primaryHandTrigger > .9f)
        {
            if (!_isHandPressing)
            {
                _isHandPressing = true;
                Ball.Instance.transform.SetParent(transform);
                Ball.Instance.Select(transform);
            }
        }

        if (primaryHandTrigger < .1f)
        {
            _isHandPressing = false;
            Ball.Instance.transform.SetParent(null);
            Ball.Instance.Unselect();
        }
    }
}
