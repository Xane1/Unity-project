using UnityEngine;

public class MessageDisplay : MonoBehaviour
{
    private IntroMessage _introMessage;
    [SerializeField] private int controlMessage;
    private bool _hasDisplayedMessage;
    // Start is called before the first frame update
    void Start()
    {
        _introMessage = FindObjectOfType<IntroMessage>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !_hasDisplayedMessage) ShowPlayerMessage();
    }

    void ShowPlayerMessage()
    {
        Debug.Log("A message should be displayed on top of the screen, consisting of the following");
        Debug.Log(_introMessage.IntroMessageText(controlMessage));
         _introMessage.ShowControlMessage(controlMessage);
         _hasDisplayedMessage = true;
    }
}
