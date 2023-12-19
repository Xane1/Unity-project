using TMPro;
using UnityEngine;
using DG.Tweening;
using Image = UnityEngine.UI.Image;
using UnityEditor;

public class IntroMessage : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] TMP_Text tmpText;
    [SerializeField] ControlMessageSO[] controlMessages;
    [SerializeField] private float originalYPosition = 1830;
    [SerializeField] private float intoGameSpeed = 0.5f;
    [SerializeField] private float outOfGameSpeed = 3f;
    [SerializeField] private float newYPosition;
    private RectTransform _rectTransform;

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        originalYPosition = transform.position.y;
    }

    public void ShowControlMessage(int controlMessageInt)
    {
        if (controlMessages[controlMessageInt].useControllerSprite) image.sprite = controlMessages[controlMessageInt].controlSpriteController;
            
        else image.sprite = controlMessages[controlMessageInt].controlSpriteKeyboardMouse;

        tmpText.text = controlMessages[controlMessageInt].controlMessage;

        Vector3 rectTransformPos = _rectTransform.position;

        Vector3 oldPosition = new Vector3(rectTransformPos.x, originalYPosition, rectTransformPos.z);
        Vector3 newPosition = new Vector3(rectTransformPos.x, newYPosition, rectTransformPos.z);

        Sequence positionSequence = DOTween.Sequence();
        positionSequence.Append(transform.DOMoveY(newYPosition, intoGameSpeed, false).SetEase(Ease.OutBack));
        positionSequence.Append(transform.DOMoveY(originalYPosition, outOfGameSpeed, false).SetDelay(5).SetEase(Ease.OutBack));
        positionSequence.Play();
    }

    public string IntroMessageText(int controlMessageInt)
    {
        return controlMessages[controlMessageInt].controlMessage;
    }
}

// References.
// https://stackoverflow.com/questions/64595434/how-do-i-change-the-source-image-of-a-unity-image-component
// https://docs.unity3d.com/Manual/class-ScriptableObject.html#:~:text=A%20ScriptableObject%20is%20a%20data%20container%20that%20you,Project%E2%80%99s%20memory%20usage%20by%20avoiding%20copies%20of%20values.
// Acknowledgements.
// Thanks to theChief on Discord for helping out with line 38 and 39.