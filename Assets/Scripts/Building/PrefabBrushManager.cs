using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PrefabBrushManager : MonoBehaviour
{
    
    public Dictionary<string, int> prefabMap = new Dictionary<string, int>();
    private string[] prefabs = new string[]{"Chube", "Chubator", "Trash Collector", "Walkable", "Energy Generator", "Windmill", "Coal"};

    void Start()
    {
        foreach (string prefab in prefabs) {
            prefabMap.Add(prefab, 0);
        }
    }
    public void paint(Tilemap tilemap, GameObject prefab, Vector3Int pos) {
        //prefabBrush.Paint(tilemap, prefab, pos);
        Vector3 worldPos = tilemap.GetCellCenterWorld(pos);
        Instantiate(prefab, worldPos, Quaternion.identity);
    }

    public void addCount(GameObject prefab) {
        prefabMap[prefab.name] += 1;
    }

    public void subtractCount(string prefabname)
    {
        if (prefabname.Length > 7 && prefabname.Substring(prefabname.Length - 7) == "(Clone)")
        {
            prefabname = prefabname.Substring(0, prefabname.Length - 7);
        }

        print(prefabname);
        
        prefabMap[prefabname] -= 1;
        //Debug.Log("Walkable count: " + prefabMap["Walkable"]);
    }
}
