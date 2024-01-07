using UnityEngine;
using Cinemachine;
using UnityEngine.Serialization;

public class PlayerZoom : MonoBehaviour
{
    private CinemachineVirtualCamera _cinemachineVirtualCamera;
    [FormerlySerializedAs("_originalLensSize")] public float originalLensSize;
    [SerializeField] private float lensZoom = 10;
    [SerializeField] private bool revertZoom;
    [Range(0.1f, 1f)]
    [SerializeField] private float zoomTime = 0.5f;

    private void Start()
    {
        _cinemachineVirtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        originalLensSize = _cinemachineVirtualCamera.m_Lens.OrthographicSize;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) SetLensZoom();
    }

    void SetLensZoom()
    {
        if (revertZoom) _cinemachineVirtualCamera.m_Lens.OrthographicSize = originalLensSize;
        else _cinemachineVirtualCamera.m_Lens.OrthographicSize = lensZoom;
    }
}
