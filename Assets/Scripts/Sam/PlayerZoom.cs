using UnityEngine;
using Cinemachine;
using DG.Tweening;
using UnityEngine.Serialization;

public class PlayerZoom : MonoBehaviour
{
    private CinemachineVirtualCamera _cinemachineVirtualCamera;
    public float originalLensSize;
    [SerializeField] private float lensZoom = 10;
    [SerializeField] private bool revertZoom;
    bool canZoom = true;
    [FormerlySerializedAs("zoomTime")]
    [Range(0.5f, 20f)]
    [SerializeField] private float zoomSpeed = 10f;
    float xPosition;

    GameObject _playerObj;
    private Transform playerTransform;

    private void Start()
    {
        DOTween.Init();
        _cinemachineVirtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        _playerObj = GameObject.FindGameObjectWithTag("Player");
        playerTransform = _playerObj.GetComponent<Transform>();
        originalLensSize = _cinemachineVirtualCamera.m_Lens.OrthographicSize;
        xPosition = transform.position.x;
    }
    private void FixedUpdate()
    {
        if (playerTransform != null)
        {
            if (playerTransform.position.x >= xPosition)
            {
                if (revertZoom)
                {
                    SetLensZoom(originalLensSize);
                }
                else
                {
                    SetLensZoom(lensZoom);
                }
            }
        }
    }
    void SetLensZoom(float newZoom)
    {
        if (canZoom)
        {
            DOVirtual.Float(_cinemachineVirtualCamera.m_Lens.OrthographicSize, newZoom, zoomSpeed, newValue => _cinemachineVirtualCamera.m_Lens.OrthographicSize = newValue).SetEase(Ease.InOutQuint);
            canZoom = false;
        }
    }
}
