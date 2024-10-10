using Unity.VisualScripting;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public Texture2D map;
    public ColorToPrefab[] colorMappings;
    public Texture2D[] levels;
    int currentLevel = 0;
    // Start is called before the first frame update
    void Start()
    {
        map = levels[currentLevel];
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        GenerateLevel();
    }
     void Update()
    {
        if (GameManager.Instance.won && currentLevel< levels.Length-1) {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(0, 0, 0);
            currentLevel++;
            map = levels[currentLevel];
            GenerateLevel();
            GameManager.Instance.won = false;
        }
    }
    void GenerateLevel() {
 
        for(int x = 0 ; x < map.width; x++)
        {
            for (int y = 0; y < map.height; y++)
            {
                GenerateTile(x, y);
            }
        }
    }
    void GenerateTile(int x, int y) { 
        Color pixColor = map.GetPixel(x, y);
 
        if (pixColor.a == 0) {
            return;
            //blank pixel
        }
        
        foreach (ColorToPrefab colorMapping in colorMappings) {
            if (colorMapping.color.Equals(pixColor)) {
                Vector3 position = new Vector3(x-16,0, y-16);
                var inst = Instantiate(colorMapping.prefab, position, Quaternion.identity, transform);
            }
        }


    }
}
