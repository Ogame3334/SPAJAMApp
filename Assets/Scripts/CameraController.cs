// using System.Numerics;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private float sensitivity;
    [SerializeField]
    private float magnitude;
    private TouchPhase prevTouchPhase = TouchPhase.Ended;

    private float tapInterval = 0.3f;
    private float tapTimer = 0;

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
        /*if(Input.touchCount == 2){
            Touch touchPos0 = Input.GetTouch(0);
            Touch touchPos1 = Input.GetTouch(1);

            // Vector2 center = (touchPos0.position + touchPos1.position) / 2;

            if(TouchPhase.Moved == touchPos0.phase || TouchPhase.Moved == touchPos1.phase){
                float delta = touchPos0.deltaPosition.magnitude + touchPos1.deltaPosition.magnitude;

                // Debug.Log(delta);
            }
        }
        else */if(Input.touchCount == 1){
            Touch touchPos = Input.GetTouch(0);

            // 移動していたら移動量を取得してオブジェクトを移動
            if (touchPos.phase == TouchPhase.Moved)
            {
                // Vector2 delta = touchPos.deltaPosition;
                Vector2 delta = new Vector2(touchPos.deltaPosition.x / Screen.width, touchPos.deltaPosition.y * 2 / Screen.height);

                float cameraDeltaX = -delta.x - delta.y;
                float cameraDeltaZ =  delta.x - delta.y;
                // this.transform.Translate(cameraDeltaX, 0f, cameraDeltaZ);

                // これでもよい
                // this.gameObject.transform.position += new Vector3(cameraDeltaX, 0f, cameraDeltaZ).normalized * sensitivity;
                rb.velocity = new Vector3(cameraDeltaX, 0f, cameraDeltaZ) * sensitivity * 1000;
            }
            else if(touchPos.phase == TouchPhase.Began){
                tapTimer += Time.deltaTime;
            }
            else if(touchPos.phase == TouchPhase.Ended){
                if(tapTimer < tapInterval){
                    Debug.Log("Tapped!!");
                    var result = ShotRay(touchPos);
                    if(result == null){
                        Debug.Log("null");
                    }
                    else{
                        Debug.Log("touch building");
                        Destroy(result);
                    }
                }
                tapTimer = 0;
            }
            prevTouchPhase = touchPos.phase;
        }
        else{
            rb.velocity = rb.velocity * magnitude;
            if(rb.velocity.magnitude < 1e-2){
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
        
    }

    // Update is called once per frame
    void Update()
    {
        Touch();
    }
}
