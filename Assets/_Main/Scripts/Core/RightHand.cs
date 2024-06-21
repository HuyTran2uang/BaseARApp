using Oculus.Interaction;
using UnityEngine;

public class RightHand : MonoBehaviour
{
    private bool _isHandPressing;
    public GameObject rightHandController;

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
                if (GameManager.Instance.Turns > 0)
                {
                    GameManager.Instance.Turns--;
                    _isHandPressing = true;
                    Ball.Instance.transform.SetParent(transform);
                    Ball.Instance.Select(transform);
                    Ball.Instance.transform.position = transform.position;
                    rightHandController.SetActive(false);
                }
            }
        }

        if (primaryHandTrigger < .1f)
        {
            _isHandPressing = false;
            Ball.Instance.transform.SetParent(null);
            Ball.Instance.Unselect();
            rightHandController.SetActive(true);
        }
    }
}
