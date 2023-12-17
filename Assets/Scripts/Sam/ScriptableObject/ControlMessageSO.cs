using UnityEngine;

[CreateAssetMenu(fileName = "Control Message", menuName = "New Control Message")]
public class ControlMessageSO : ScriptableObject
{
    public bool useControllerSprite;
    [SerializeField] public Sprite controlSpriteKeyboardMouse;
    [SerializeField] public Sprite controlSpriteController;
    [SerializeField] public string controlMessage;
}
