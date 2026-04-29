/*Script was written by CJ Robinson. handle UI hover effects*/
using UnityEngine;
using UnityEngine.UIElements;

public class UIHoverEffect : MonoBehaviour
{
    private float targetAngle = -5f;
    public void OnHoverEnter(GameObject go)
    {
        go.transform.localScale = new Vector3(1.2f, 1.2f, 1.0f);
        go.transform.eulerAngles = new Vector3(0, 0, targetAngle);
    }
    public void OnHoverExit(GameObject go)
    {
        go.transform.localScale = Vector3.one;
        go.transform.eulerAngles = new Vector3(0, 0, 0);
    }
}
