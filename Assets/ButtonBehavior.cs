using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class ButtonBehavior : MonoBehaviour
{
    [SerializeField] private bool _useCustomHoverState;

    private Button selfRef;

    private void Start()
    {
        selfRef = GetComponent<Button>();
        if (_useCustomHoverState)
        {
        }
    }

}
