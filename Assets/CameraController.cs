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
    void Touch()
    {
        if(Input.touchCount == 2){
            Touch touchPos0 = Input.GetTouch(0);
            Touch touchPos1 = Input.GetTouch(1);

            // Vector2 center = (touchPos0.position + touchPos1.position) / 2;

            if(TouchPhase.Moved == touchPos0.phase || TouchPhase.Moved == touchPos1.phase){
                float delta = touchPos0.deltaPosition.magnitude + touchPos1.deltaPosition.magnitude;

                Debug.Log(delta);
            }
        }
        else if(Input.touchCount == 1){
            Touch touchPos = Input.GetTouch(0);

            // 移動していたら移動量を取得してオブジェクトを移動
            if (TouchPhase.Moved == touchPos.phase)
            {
                Vector2 delta = touchPos.deltaPosition;

                float cameraDeltaX = -delta.x - delta.y;
                float cameraDeltaZ =  delta.x - delta.y;
                // this.transform.Translate(cameraDeltaX, 0f, cameraDeltaZ);

                // これでもよい
                // this.gameObject.transform.position += new Vector3(cameraDeltaX, 0f, cameraDeltaZ).normalized * sensitivity;
                rb.velocity = new Vector3(cameraDeltaX, 0f, cameraDeltaZ).normalized * sensitivity;
            }
        }
        else{
            rb.velocity = rb.velocity * magnitude;
            if(rb.velocity.magnitude < 1e-2){
                rb.velocity = Vector3.zero;
            }
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
