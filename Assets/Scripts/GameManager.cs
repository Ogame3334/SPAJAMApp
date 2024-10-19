using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    private Vector2Int m_fieldSize = new Vector2Int(10, 10);
    private BuildingInfo[] m_buildingField;

    [SerializeField]
    private GameObject m_grassTile;
    [SerializeField]
    private GameObject m_roadStraightTile;
    [SerializeField]
    private GameObject m_roadCornerTile;
    [SerializeField]
    private GameObject m_roadTTile;

    [SerializeField]
    private GameObject m_building;
    [SerializeField]
    private GameObject[] m_buildings;

    public BuildingInfo[] BuildingField
    {
        get { return this.m_buildingField; }
    }

    public void Awake()
    {
        if (this != Instance)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        m_buildingField = new BuildingInfo[100];

        for (int y = 0; y < m_fieldSize.y; ++y)
        {
            for (int x = 0; x < m_fieldSize.x; ++x)
            {
                m_buildingField[x + y * m_fieldSize.x] = new BuildingInfo( //fieldの初期化
                    x + y * m_fieldSize.x,
                    "",
                    Random.Range(0, 12),
                    0,
                    0,
                    "",
                    false
                );
            }
        }
        for (int y = 0; y < m_fieldSize.y + 6; ++y)
        {
            for (int x = 0; x < m_fieldSize.x + 2; ++x)
            {
                { // 道路の生成
                    if (x == 0 || x == m_fieldSize.x + 1)
                    {
                        if (y == 0 || y == m_fieldSize.y + 5)
                        {
                            float rot = 0;
                            if (x == 0 && y == 0) rot = 90;
                            else if (x == 0 && y == m_fieldSize.y + 5) rot = 180;
                            else if (y == 0 && x == m_fieldSize.x + 1) rot = 0;
                            else rot = 270;
                            Instantiate(m_roadCornerTile, new Vector3(x * 20f, 0, y * 20f), Quaternion.Euler(0, rot, 0));
                        }
                        else if (y % 3 == 0)
                        {
                            Instantiate(m_roadTTile, new Vector3(x * 20f, 0, y * 20f), Quaternion.Euler(0, 90f * (x == 0 ? 1 : -1), 0));
                        }
                        else
                        {
                            Instantiate(m_roadStraightTile, new Vector3(x * 20f, 0, y * 20f), Quaternion.identity);
                        }
                    }
                    else if (y % 3 == 0)
                    {
                        Instantiate(m_roadStraightTile, new Vector3(x * 20f, 0, y * 20f), Quaternion.Euler(0, 90f, 0));
                    }
                    // else
                    // {
                    //     // Instantiate(m_grassTile, new Vector3(x * 20f, 0, y * 20f), Quaternion.identity);
                    // }
                }
                { // 草タイル
                    if(!(x == 0 || x == m_fieldSize.x + 1) && y % 3 != 0){
                        Instantiate(m_grassTile, new Vector3(x * 20f, 0, y * 20f), Quaternion.identity);
                    }
                }
                { // 建物
                    int buildingX = x - 1;
                    int buildingY = y - y / 3 - 1;

                    int zip = buildingX + buildingY * m_fieldSize.x;
                    if(!(x == 0 || x == m_fieldSize.x + 1) && y % 3 != 0){
                        BuildingInfo shouldBuilding = null;
                        foreach (var buildingInfo in m_buildingField)
                        {
                            if(buildingInfo.zip == zip){
                                shouldBuilding = buildingInfo;
                                break;
                            }
                        }
                        if(shouldBuilding == null) continue;


                        var building = Instantiate(m_building, new Vector3(x * 20f, 0, y * 20f), Quaternion.Euler(0, y % 3 == 1 ? 180f : 0, 0));
                        Instantiate(m_buildings[shouldBuilding.buildingId], building.transform);
                        building.GetComponent<Building>().ThenChildInit(m_buildingField[zip]);
                        // Instantiate(m_buildings[m_buildingField[x - 1, y - (int)(y / 3) - 1].zip % 3], new Vector3(x * 20f, 0, y * 20f), Quaternion.Euler(0, (y % 3 == 1 ? 180f : 0), 0));
                    }
                }
            }

        }
    }

    void Update()
    {

    }

}
