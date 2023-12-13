using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;

    private Vector3 _newCameraPosition;

    private void Update()
    {
        SetCameraPosition();
    }

    void SetCameraPosition()
    {
        if (playerTransform != null)
        {
            var transform1 = transform;
            Vector3 cameraPosition = transform1.position;
            _newCameraPosition = new Vector3(playerTransform.position.x, cameraPosition.y, cameraPosition.z);
            transform1.position = _newCameraPosition;
        }
    }
}
