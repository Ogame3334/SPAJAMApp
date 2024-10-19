using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    enum State
    {
        None,
        TappedBigger,
        TappedSmaller,
        Selected,
        Released
    }
    [SerializeField]
    private BuildingInfo m_buildingInfo;
    private GameObject m_buildingModelPrefab;
    private MeshRenderer m_modelMeshRenderer;

    private State m_state;

    private float m_timer = 0f;

    public int Zip
    {
        get {return this.m_buildingInfo.zip;}
    }

    private void Start()
    {
        m_state = State.None;
    }

    private void Update()
    {
        switch (m_state)
        {
            case State.TappedBigger:
                this.transform.localScale += Vector3.one * Time.deltaTime;
                if(this.transform.localScale.x > 1.1f)
                    m_state = State.TappedSmaller;
                break;
            case State.TappedSmaller:
                this.transform.localScale -= Vector3.one * Time.deltaTime;
                if(this.transform.localScale.x < 1.0f){
                    this.transform.localScale.Scale(Vector3.one);
                    m_state = State.Selected;
                }
                break;
            case State.Selected:
                m_timer += Time.deltaTime;
                m_modelMeshRenderer.material.color = Color.white * ((Mathf.Sin(m_timer * 4) + 1) / 4 + 0.5f);
                break;
            case State.Released:
                m_modelMeshRenderer.material.color = Color.white;
                this.transform.localScale = Vector3.one;
                m_timer = 0f;
                break;
            default:
                break;
        }
        if(m_timer != 0f){
            m_timer += Time.deltaTime;
        }
    }

    public void ThenChildInit(BuildingInfo buildingInfo){
        m_buildingInfo = buildingInfo;
        m_buildingModelPrefab = transform.GetChild(0).gameObject;
        m_modelMeshRenderer = m_buildingModelPrefab.GetComponent<MeshRenderer>();
    }

    public void OnTapped(){
        // m_modelMeshRenderer.material.color = Color.black;
        m_state = State.TappedBigger;
    }
    public void OnReleased(){
        m_state = State.Released;
    }
}
