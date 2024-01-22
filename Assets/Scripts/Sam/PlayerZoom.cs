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
    float newZoom;
    bool canZoom;
    [FormerlySerializedAs("zoomTime")]
    [Range(1f, 20f)]
    [SerializeField] private float zoomSpeed = 10f;
    float xPosition;

    GameObject _playerObj;
    private Transform playerTransform;

    private void Start()
    {
        _cinemachineVirtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        _playerObj = GameObject.FindGameObjectWithTag("Player");
        playerTransform = _playerObj.GetComponent<Transform>();
        originalLensSize = _cinemachineVirtualCamera.m_Lens.OrthographicSize;
        xPosition = transform.position.x;
    }

    private void Update()
    {
        if (canZoom) SetLensZoom();
    }
    private void FixedUpdate()
    {
        if (playerTransform != null)
        {
            if (playerTransform.position.x >= xPosition)
            {
                if (revertZoom)
                {
                    newZoom = originalLensSize;
                }
                else 
                {
                    newZoom = lensZoom;
                }
                canZoom = true;
            }
        }
    }
    void SetLensZoom()
    {
        _cinemachineVirtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(_cinemachineVirtualCamera.m_Lens.OrthographicSize, newZoom, Time.deltaTime * zoomSpeed);
        if (_cinemachineVirtualCamera.m_Lens.OrthographicSize > newZoom)
        {
            canZoom = false;
        }
    }
}
