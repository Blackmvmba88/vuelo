using UnityEngine;

/// <summary>
/// Sistema de ciclo día/noche controlable por el jugador
/// Permite cambiar entre día y noche con transiciones suaves
/// </summary>
public class DayNightCycle : MonoBehaviour
{
    [Header("Configuración de Iluminación")]
    [SerializeField] private Light directionalLight;
    [SerializeField] private Light[] neonLights;
    
    [Header("Configuración Día")]
    [SerializeField] private Color dayAmbientColor = new Color(0.4f, 0.4f, 0.5f);
    [SerializeField] private Color dayLightColor = new Color(1f, 0.95f, 0.8f);
    [SerializeField] private float dayLightIntensity = 1.0f;
    [SerializeField] private Color dayFogColor = new Color(0.5f, 0.6f, 0.7f);
    
    [Header("Configuración Noche")]
    [SerializeField] private Color nightAmbientColor = new Color(0.05f, 0.05f, 0.1f);
    [SerializeField] private Color nightLightColor = new Color(0.3f, 0.4f, 0.6f);
    [SerializeField] private float nightLightIntensity = 0.2f;
    [SerializeField] private Color nightFogColor = new Color(0.05f, 0.1f, 0.15f);
    
    [Header("Intensidad Neón")]
    [SerializeField] private float neonIntensityDay = 0.5f;
    [SerializeField] private float neonIntensityNight = 2.5f;
    
    [Header("Configuración de Transición")]
    [SerializeField] private float transitionSpeed = 1.0f;
    [SerializeField] private KeyCode toggleKey = KeyCode.T;
    
    private bool isDay = true;
    private float currentTransition = 1.0f; // 1 = día, 0 = noche
    
    void Start()
    {
        // Buscar luz direccional si no está asignada
        if (directionalLight == null)
        {
            directionalLight = FindObjectOfType<Light>();
        }
        
        // Buscar todas las luces neón si no están asignadas
        if (neonLights == null || neonLights.Length == 0)
        {
            Light[] allLights = FindObjectsOfType<Light>();
            System.Collections.Generic.List<Light> neons = new System.Collections.Generic.List<Light>();
            foreach (Light light in allLights)
            {
                if (light.type == LightType.Point && light != directionalLight)
                {
                    neons.Add(light);
                }
            }
            neonLights = neons.ToArray();
        }
        
        // Aplicar estado inicial (día)
        ApplyLighting();
    }
    
    void Update()
    {
        // Cambiar entre día y noche con la tecla T
        if (Input.GetKeyDown(toggleKey))
        {
            ToggleDayNight();
        }
        
        // Transición suave
        float targetTransition = isDay ? 1.0f : 0.0f;
        currentTransition = Mathf.Lerp(currentTransition, targetTransition, Time.deltaTime * transitionSpeed);
        
        ApplyLighting();
    }
    
    /// <summary>
    /// Alternar entre día y noche
    /// </summary>
    public void ToggleDayNight()
    {
        isDay = !isDay;
        Debug.Log(isDay ? "Cambiando a DÍA" : "Cambiando a NOCHE");
    }
    
    /// <summary>
    /// Establecer directamente día o noche
    /// </summary>
    public void SetTimeOfDay(bool day)
    {
        isDay = day;
    }
    
    /// <summary>
    /// Aplicar la iluminación según el tiempo actual
    /// </summary>
    void ApplyLighting()
    {
        // Interpolar colores y valores
        Color ambientColor = Color.Lerp(nightAmbientColor, dayAmbientColor, currentTransition);
        Color lightColor = Color.Lerp(nightLightColor, dayLightColor, currentTransition);
        float lightIntensity = Mathf.Lerp(nightLightIntensity, dayLightIntensity, currentTransition);
        Color fogColor = Color.Lerp(nightFogColor, dayFogColor, currentTransition);
        float neonIntensity = Mathf.Lerp(neonIntensityNight, neonIntensityDay, currentTransition);
        
        // Aplicar iluminación ambiental
        RenderSettings.ambientLight = ambientColor;
        RenderSettings.fogColor = fogColor;
        
        // Aplicar luz direccional
        if (directionalLight != null)
        {
            directionalLight.color = lightColor;
            directionalLight.intensity = lightIntensity;
            
            // Rotar la luz para simular posición del sol/luna
            float angle = Mathf.Lerp(-80f, 50f, currentTransition);
            directionalLight.transform.rotation = Quaternion.Euler(angle, -30f, 0f);
        }
        
        // Aplicar intensidad a luces neón
        if (neonLights != null)
        {
            foreach (Light neonLight in neonLights)
            {
                if (neonLight != null)
                {
                    neonLight.intensity = neonIntensity;
                }
            }
        }
    }
    
    /// <summary>
    /// Obtener si es de día
    /// </summary>
    public bool IsDay()
    {
        return isDay;
    }
    
    /// <summary>
    /// Obtener valor de transición actual (0-1)
    /// </summary>
    public float GetTransition()
    {
        return currentTransition;
    }
}
