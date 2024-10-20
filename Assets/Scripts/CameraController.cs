// using System.Numerics;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private Camera m_camera;
    [SerializeField]
    private GameObject m_fukidashi;
    private Fukidashi m_fukidashiClass;
    [SerializeField]
    private float sensitivity;
    [SerializeField]
    private float magnitude;
    private TouchPhase prevTouchPhase = TouchPhase.Ended;

    private float tapInterval = 0.3f;
    private float tapTimer = 0;

    private float twoTapStartDistance = -1f;
    private Vector2 tapStartPos;

    private float minCameraSize = 50f;
    private float maxCameraSize = 200f;

    private float nowCameraSize = 0f;

    private Building selectingBuilding = null;

    GameObject ShotRay(Touch touch){
        Ray ray = Camera.main.ScreenPointToRay(touch.position);
        RaycastHit hit = new RaycastHit();
        if(Physics.Raycast(ray, out hit)){
            if(hit.collider.tag == "Building"){
                return hit.collider.gameObject;
            }
        }
        return null;
    }
    void Touch()
    {
        if(Input.touchCount == 2){
            Touch touchPos0 = Input.GetTouch(0);
            Touch touchPos1 = Input.GetTouch(1);

            // Vector2 center = (touchPos0.position + touchPos1.position) / 2;

            if(touchPos0.phase == TouchPhase.Began || touchPos1.phase == TouchPhase.Began){
                twoTapStartDistance = Vector2.Distance(touchPos0.position, touchPos1.position);
            }
            else if(touchPos0.phase == TouchPhase.Moved || touchPos1.phase == TouchPhase.Moved){
                float distance = Vector2.Distance(touchPos0.position, touchPos1.position);
                Vector2 delta0 = new Vector2(touchPos0.deltaPosition.x / Screen.width, touchPos0.deltaPosition.y * (Screen.height / Screen.width) / Screen.height);
                Vector2 delta1 = new Vector2(touchPos1.deltaPosition.x / Screen.width, touchPos1.deltaPosition.y * (Screen.height / Screen.width) / Screen.height);
                float delta = delta0.magnitude + delta1.magnitude;
                delta *= 100f;

                Debug.Log(delta);

                if(distance < twoTapStartDistance){
                    m_camera.orthographicSize = Mathf.Clamp(m_camera.orthographicSize + delta, minCameraSize, maxCameraSize);
                }
                else{
                    m_camera.orthographicSize = Mathf.Clamp(m_camera.orthographicSize - delta, minCameraSize, maxCameraSize);
                }
            }
            else if(touchPos0.phase == TouchPhase.Stationary || touchPos1.phase == TouchPhase.Stationary){
                twoTapStartDistance = Vector2.Distance(touchPos0.position, touchPos1.position);
            }
            rb.velocity *= 0;
        }
        else if(Input.touchCount == 1){
            Touch touchPos = Input.GetTouch(0);

            // 移動していたら移動量を取得してオブジェクトを移動
            if (touchPos.phase == TouchPhase.Moved)
            {
                Vector2 delta = new Vector2(touchPos.deltaPosition.x / Screen.width, touchPos.deltaPosition.y * (Screen.height / Screen.width) / Screen.height);

                float cameraDeltaX = -delta.x - delta.y;
                float cameraDeltaZ =  delta.x - delta.y;

                rb.velocity = new Vector3(cameraDeltaX, 0f, cameraDeltaZ) * sensitivity * 1000 * (nowCameraSize / 100f);
            }
            else if(touchPos.phase == TouchPhase.Began){
                tapStartPos = touchPos.position;
                tapTimer += Time.deltaTime;
            }
            else if(touchPos.phase == TouchPhase.Ended){
                if(tapTimer < tapInterval && Vector2.Distance(touchPos.position, tapStartPos) < 10f){
                    // Debug.Log("Tapped!!");
                    var result = ShotRay(touchPos);
                    if(result == null){
                        // Debug.Log("null");
                        if(selectingBuilding != null){
                            selectingBuilding.OnReleased();
                        }
                        selectingBuilding = null;
                        m_fukidashi.SetActive(false);
                    }
                    else{
                        // Debug.Log("touch building");
                        // Destroy(result);
                        var building = result.GetComponent<Building>();
                        if(selectingBuilding == null){
                            building.OnTapped();
                            
                            if(selectingBuilding != null){
                                selectingBuilding.OnReleased();
                            }
                            selectingBuilding = building;
                            m_fukidashiClass.BuildingTopPos = building.TopPosition;
                            m_fukidashi.SetActive(true);
                        }
                        else if(selectingBuilding.Zip != building.Zip){
                            
                            building.OnTapped();
                            
                            if(selectingBuilding != null){
                                selectingBuilding.OnReleased();
                            }
                            selectingBuilding = building;
                            m_fukidashiClass.BuildingTopPos = building.TopPosition;
                            m_fukidashi.SetActive(true);
                        }
                        else{
                            building.OnReleased();
                            selectingBuilding = null;
                            m_fukidashi.SetActive(false);
                        }
                    }
                }
                tapTimer = 0;
            }
            prevTouchPhase = touchPos.phase;
        }
        else{
            rb.velocity = rb.velocity * magnitude;
            if(rb.velocity.magnitude < 0.1f){
                rb.velocity = Vector3.zero;
            }
            prevTouchPhase = TouchPhase.Ended;
        }
        if(tapTimer != 0f){
            tapTimer += Time.deltaTime;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        nowCameraSize = m_camera.orthographicSize;
        m_fukidashiClass = m_fukidashi.GetComponent<Fukidashi>();
        m_fukidashiClass.CameraSize = nowCameraSize;
        m_fukidashi.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Touch();
        nowCameraSize = m_camera.orthographicSize;
    }
}
