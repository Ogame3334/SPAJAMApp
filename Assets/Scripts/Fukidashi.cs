using UnityEngine;

public class Fukidashi : MonoBehaviour
{
    [SerializeField]
    private Camera m_camera;
    private RectTransform m_rt;
    private Vector3 m_buildingTopPos;
    public Vector3 BuildingTopPos
    {
        set{
            m_buildingTopPos = value;
        }
    }
    private float m_cameraSize;
    public float CameraSize
    {
        set{
            m_cameraSize = value;
        }
    }

    void Start()
    {
        m_rt = GetComponent<RectTransform>();
        m_buildingTopPos = Vector2.zero;
        m_cameraSize = 0;
    }

    void Update()
    {
        var center = 0.5f * new Vector3(Screen.width, Screen.height);
        m_rt.anchoredPosition = m_camera.WorldToScreenPoint(m_buildingTopPos) - center;
        // m_rt.sizeDelta = new Vector2(800 / Screen.width, 400 / Screen.height);
        // Debug.Log(m_rt.sizeDelta);
    }
}
