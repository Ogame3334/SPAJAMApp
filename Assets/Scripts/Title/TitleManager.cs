using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    [SerializeField]
    private Camera m_camera;
    enum State{
        Phase0,
        Phase1,
        Phase2,
        Phase3,
    }

    private float m_speed = 10f;

    State m_state;

    float m_timer = 0f;


    void Start()
    {
        
    }

    void Update()
    {
        switch (m_state)
        {
            case State.Phase0:
                m_camera.transform.position += new Vector3(1f, 0, 0.1f) * m_speed * Time.deltaTime;
                if(m_timer > 5f){
                    m_state = State.Phase1;
                    m_timer = 0;
                }
                break;
            case State.Phase1:
                m_camera.transform.position += new Vector3(-0.1f, 0, 1f) * m_speed * Time.deltaTime;
                if(m_timer > 5f){
                    m_state = State.Phase2;
                    m_timer = 0;
                }
                break;
            case State.Phase2:
                m_camera.transform.position += new Vector3(-1f, 0, -0.1f) * m_speed * Time.deltaTime;
                if(m_timer > 5f){
                    m_state = State.Phase3;
                    m_timer = 0;
                }
                break;
            case State.Phase3:
                m_camera.transform.position += new Vector3(0.1f, 0, -1f) * m_speed * Time.deltaTime;
                if(m_timer > 5f){
                    m_state = State.Phase0;
                    m_timer = 0;
                }
                break;
            default:
                break;
        }
        m_timer += Time.deltaTime;
    }
}
