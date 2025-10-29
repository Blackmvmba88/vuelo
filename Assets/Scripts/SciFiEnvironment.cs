using UnityEngine;

/// <summary>
/// Environment setup with metallic reflections and neon lighting
/// Creates a sci-fi atmosphere with procedural elements
/// </summary>
public class SciFiEnvironment : MonoBehaviour
{
    [Header("Environment Settings")]
    [SerializeField] private bool autoGenerateEnvironment = true;
    [SerializeField] private int gridSize = 20;
    [SerializeField] private float tileSize = 10f;
    
    [Header("Neon Lighting")]
    [SerializeField] private Color neonColor1 = new Color(0f, 1f, 1f, 1f); // Cyan
    [SerializeField] private Color neonColor2 = new Color(1f, 0f, 1f, 1f); // Magenta
    [SerializeField] private float neonIntensity = 2f;
    [SerializeField] private int neonLightCount = 10;
    
    [Header("Metallic Materials")]
    [SerializeField] private float metallic = 0.9f;
    [SerializeField] private float smoothness = 0.95f;
    
    private Material floorMaterial;
    private Material wallMaterial;
    
    void Start()
    {
        if (autoGenerateEnvironment)
        {
            GenerateEnvironment();
        }
    }
    
    void GenerateEnvironment()
    {
        CreateMaterials();
        CreateFloor();
        CreateWalls();
        CreateNeonLights();
        SetupAmbientLighting();
    }
    
    void CreateMaterials()
    {
        // Create metallic floor material
        floorMaterial = new Material(Shader.Find("Standard"));
        floorMaterial.SetFloat("_Metallic", metallic);
        floorMaterial.SetFloat("_Glossiness", smoothness);
        floorMaterial.SetColor("_Color", new Color(0.1f, 0.1f, 0.15f));
        
        // Create metallic wall material
        wallMaterial = new Material(Shader.Find("Standard"));
        wallMaterial.SetFloat("_Metallic", metallic * 0.8f);
        wallMaterial.SetFloat("_Glossiness", smoothness * 0.9f);
        wallMaterial.SetColor("_Color", new Color(0.15f, 0.15f, 0.2f));
    }
    
    void CreateFloor()
    {
        GameObject floor = GameObject.CreatePrimitive(PrimitiveType.Plane);
        floor.name = "Floor";
        floor.transform.SetParent(transform);
        floor.transform.localPosition = Vector3.zero;
        floor.transform.localScale = new Vector3(gridSize, 1, gridSize);
        
        Renderer renderer = floor.GetComponent<Renderer>();
        renderer.material = floorMaterial;
        renderer.material.mainTextureScale = new Vector2(gridSize * 2, gridSize * 2);
    }
    
    void CreateWalls()
    {
        float wallHeight = 5f;
        float halfSize = gridSize * tileSize / 2f;
        
        // Create four walls
        CreateWall("WallNorth", new Vector3(0, wallHeight/2, halfSize), new Vector3(gridSize * tileSize, wallHeight, 0.5f));
        CreateWall("WallSouth", new Vector3(0, wallHeight/2, -halfSize), new Vector3(gridSize * tileSize, wallHeight, 0.5f));
        CreateWall("WallEast", new Vector3(halfSize, wallHeight/2, 0), new Vector3(0.5f, wallHeight, gridSize * tileSize));
        CreateWall("WallWest", new Vector3(-halfSize, wallHeight/2, 0), new Vector3(0.5f, wallHeight, gridSize * tileSize));
    }
    
    void CreateWall(string name, Vector3 position, Vector3 size)
    {
        GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        wall.name = name;
        wall.transform.SetParent(transform);
        wall.transform.localPosition = position;
        wall.transform.localScale = size;
        
        Renderer renderer = wall.GetComponent<Renderer>();
        renderer.material = wallMaterial;
    }
    
    void CreateNeonLights()
    {
        GameObject lightsContainer = new GameObject("NeonLights");
        lightsContainer.transform.SetParent(transform);
        
        for (int i = 0; i < neonLightCount; i++)
        {
            // Alternate between two neon colors
            Color neonColor = (i % 2 == 0) ? neonColor1 : neonColor2;
            
            // Random position within the environment
            float x = Random.Range(-gridSize * tileSize / 3f, gridSize * tileSize / 3f);
            float y = Random.Range(2f, 4f);
            float z = Random.Range(-gridSize * tileSize / 3f, gridSize * tileSize / 3f);
            
            GameObject lightObj = new GameObject($"NeonLight_{i}");
            lightObj.transform.SetParent(lightsContainer.transform);
            lightObj.transform.position = new Vector3(x, y, z);
            
            Light light = lightObj.AddComponent<Light>();
            light.type = LightType.Point;
            light.color = neonColor;
            light.intensity = neonIntensity;
            light.range = 15f;
            light.renderMode = LightRenderMode.ForcePixel;
            
            // Add a glowing sphere for visual effect
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.SetParent(lightObj.transform);
            sphere.transform.localPosition = Vector3.zero;
            sphere.transform.localScale = Vector3.one * 0.3f;
            
            Material emissiveMat = new Material(Shader.Find("Standard"));
            emissiveMat.SetColor("_Color", neonColor);
            emissiveMat.SetColor("_EmissionColor", neonColor * 2f);
            emissiveMat.EnableKeyword("_EMISSION");
            
            Renderer sphereRenderer = sphere.GetComponent<Renderer>();
            sphereRenderer.material = emissiveMat;
            
            // Remove collider from decorative sphere
            Destroy(sphere.GetComponent<Collider>());
        }
    }
    
    void SetupAmbientLighting()
    {
        // Set ambient lighting for sci-fi atmosphere
        RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Flat;
        RenderSettings.ambientLight = new Color(0.05f, 0.05f, 0.1f);
        RenderSettings.fogColor = new Color(0.05f, 0.1f, 0.15f);
        RenderSettings.fog = true;
        RenderSettings.fogDensity = 0.02f;
    }
}
