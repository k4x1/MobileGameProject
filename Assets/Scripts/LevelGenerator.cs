using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public Texture2D map;
    public ColorToPrefab[] colorMappings;
    public Texture2D[] levels;
    int currentLevel = 0;
    [SerializeField] float scale = 1.0f;
    Vector2 offset = new Vector2(16,16);
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
            GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(0, 1, 0);
            GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>().velocity = Vector3.zero;
            currentLevel++;
            map = levels[currentLevel];
            GetComponent<LevelMovement>().ResetOrientation();
            GenerateLevel();
            GameManager.Instance.won = false;
            UiManager.Instance.SetWinMenu(false);
        }
   
    }

    void GenerateLevel() {
        offset = new Vector2(map.width/2, map.height/2);
        offset*=scale;
        for (int x = 0 ; x < map.width; x++)
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
                Vector3 position = new Vector3((x*scale) - offset.x, 0, (y*scale) - offset.y);
                var inst = Instantiate(colorMapping.prefab, position, Quaternion.identity, transform);
                inst.transform.localScale*=scale;
            }
        }


    }
}
