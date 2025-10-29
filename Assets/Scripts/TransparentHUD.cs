using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Transparent HUD overlay for the sci-fi simulator
/// Displays FPS, camera position, and system status
/// </summary>
public class TransparentHUD : MonoBehaviour
{
    [Header("HUD Settings")]
    [SerializeField] private bool showHUD = true;
    [SerializeField] private Color hudColor = new Color(0f, 1f, 1f, 0.7f); // Cyan neon
    [SerializeField] private int fontSize = 14;
    
    [Header("Display Options")]
    [SerializeField] private bool showFPS = true;
    [SerializeField] private bool showPosition = true;
    [SerializeField] private bool showControls = true;
    
    private float deltaTime = 0.0f;
    private Camera mainCamera;
    private GUIStyle hudStyle;
    private DayNightCycle dayNightCycle;
    
    void Start()
    {
        mainCamera = Camera.main;
        dayNightCycle = FindObjectOfType<DayNightCycle>();
        InitializeStyle();
    }
    
    void Update()
    {
        // Toggle HUD with H key
        if (Input.GetKeyDown(KeyCode.H))
        {
            showHUD = !showHUD;
        }
        
        // Calculate FPS
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
    }
    
    void OnGUI()
    {
        if (!showHUD) return;
        
        if (hudStyle == null)
            InitializeStyle();
        
        float padding = 10f;
        float lineHeight = fontSize + 5f;
        float currentY = padding;
        
        // Top-left corner - Title and status
        GUI.Label(new Rect(padding, currentY, 400, lineHeight), 
            "VUELO - MULTIVERSO MAMBA", hudStyle);
        currentY += lineHeight;
        
        GUI.Label(new Rect(padding, currentY, 400, lineHeight), 
            "━━━━━━━━━━━━━━━━━━━━━━━━", hudStyle);
        currentY += lineHeight;
        
        // FPS Counter
        if (showFPS)
        {
            float fps = 1.0f / deltaTime;
            string fpsText = string.Format("FPS: {0:0.}", fps);
            GUI.Label(new Rect(padding, currentY, 400, lineHeight), fpsText, hudStyle);
            currentY += lineHeight;
        }
        
        // Camera position
        if (showPosition && mainCamera != null)
        {
            Vector3 pos = mainCamera.transform.position;
            string posText = string.Format("POS: X:{0:F1} Y:{1:F1} Z:{2:F1}", pos.x, pos.y, pos.z);
            GUI.Label(new Rect(padding, currentY, 400, lineHeight), posText, hudStyle);
            currentY += lineHeight;
        }
        
        // Controls help
        if (showControls)
        {
            currentY = Screen.height - (lineHeight * 9) - padding;
            
            GUI.Label(new Rect(padding, currentY, 400, lineHeight), 
                "━━━━━━ CONTROLES ━━━━━━", hudStyle);
            currentY += lineHeight;
            
            GUI.Label(new Rect(padding, currentY, 400, lineHeight), 
                "WASD/Flechas: Mover", hudStyle);
            currentY += lineHeight;
            
            GUI.Label(new Rect(padding, currentY, 400, lineHeight), 
                "Shift: Sprint", hudStyle);
            currentY += lineHeight;
            
            GUI.Label(new Rect(padding, currentY, 400, lineHeight), 
                "Mouse: Mirar | ESC: Cursor", hudStyle);
            currentY += lineHeight;
            
            GUI.Label(new Rect(padding, currentY, 400, lineHeight), 
                "H: Toggle HUD", hudStyle);
            currentY += lineHeight;
            
            GUI.Label(new Rect(padding, currentY, 400, lineHeight), 
                "T: Cambiar DÍA/NOCHE", hudStyle);
        }
        
        // Top-right corner - System status
        float rightX = Screen.width - 250;
        currentY = padding;
        
        GUI.Label(new Rect(rightX, currentY, 240, lineHeight), 
            "ESTADO: ACTIVO", hudStyle);
        currentY += lineHeight;
        
        GUI.Label(new Rect(rightX, currentY, 240, lineHeight), 
            string.Format("TIEMPO: {0:F1}s", Time.time), hudStyle);
        currentY += lineHeight;
        
        // Mostrar hora del día
        if (dayNightCycle != null)
        {
            string timeOfDay = dayNightCycle.IsDay() ? "DÍA" : "NOCHE";
            GUI.Label(new Rect(rightX, currentY, 240, lineHeight), 
                string.Format("HORA: {0}", timeOfDay), hudStyle);
        }
    }
    
    void InitializeStyle()
    {
        hudStyle = new GUIStyle();
        hudStyle.fontSize = fontSize;
        hudStyle.normal.textColor = hudColor;
        hudStyle.font = Font.CreateDynamicFontFromOSFont("Consolas", fontSize);
        if (hudStyle.font == null)
            hudStyle.font = Font.CreateDynamicFontFromOSFont("Courier New", fontSize);
    }
}
