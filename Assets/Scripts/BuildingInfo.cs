[System.Serializable]
public class BuildingInfo
{
    public BuildingInfo(
        int _zip,
        string _name,
        int _bId,
        int _grade,
        int _oldLev,
        string _uId,
        bool _nInfo
    ){
        zip = _zip;
        name = _name;
        buildingId = _bId;
        grade = _grade;
        oldLevel = _oldLev;
        userId = _uId;
        newInfo = _nInfo;
    }
    public int zip;
    public string name;
    public int buildingId;
    public int grade;
    public int oldLevel;
    public string userId;
    public bool newInfo;
}
