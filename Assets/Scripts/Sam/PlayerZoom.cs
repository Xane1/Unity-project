using UnityEngine;
using Cinemachine;

public class PlayerZoom : MonoBehaviour
{
    private CinemachineVirtualCamera _cinemachineVirtualCamera;
    private float _originalLensSize;
    [SerializeField] private float lensZoom = 10;
    [SerializeField] private bool revertZoom;
    [Range(0.1f, 1f)]
    [SerializeField] private float zoomTime = 0.5f;

    private void Start()
    {
        _cinemachineVirtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        _originalLensSize = _cinemachineVirtualCamera.m_Lens.OrthographicSize;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) SetLensZoom();
    }

    void SetLensZoom()
    {
        if (revertZoom) _cinemachineVirtualCamera.m_Lens.OrthographicSize = _originalLensSize;
        else _cinemachineVirtualCamera.m_Lens.OrthographicSize = lensZoom;
    }
}
