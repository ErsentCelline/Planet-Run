using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Message : MonoBehaviour
{
    [SerializeField] 
    private UnityEngine.UI.Text _messageField;

    private static UnityEngine.UI.Text MessageField;

    public static void ShowMessage()
    {
        if (!MessageField.gameObject.activeInHierarchy)
            MessageField.gameObject.SetActive(true);
    }

    public static void HideMessage()
    {
        if (MessageField != null)
            MessageField.gameObject.SetActive(false);
    }

    private void Awake()
    {
        MessageField = _messageField;
    }
}
