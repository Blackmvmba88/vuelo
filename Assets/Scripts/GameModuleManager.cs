using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Modular game system manager for the Multiverso Mamba
/// Allows loading and switching between different micro-game modules
/// </summary>
public class GameModuleManager : MonoBehaviour
{
    [System.Serializable]
    public class GameModule
    {
        public string moduleName;
        public string moduleId;
        public GameObject modulePrefab;
        public bool isActive;
        [TextArea(3, 10)]
        public string description;
    }
    
    [Header("Module Management")]
    [SerializeField] private List<GameModule> availableModules = new List<GameModule>();
    [SerializeField] private Transform moduleContainer;
    
    private GameModule currentModule;
    private GameObject currentModuleInstance;
    
    void Start()
    {
        if (moduleContainer == null)
        {
            GameObject container = new GameObject("ModuleContainer");
            moduleContainer = container.transform;
            moduleContainer.SetParent(transform);
        }
        
        // Initialize with first module if available
        if (availableModules.Count > 0)
        {
            LoadModule(availableModules[0]);
        }
    }
    
    void Update()
    {
        // Quick module switching with number keys (1-9)
        for (int i = 0; i < Mathf.Min(9, availableModules.Count); i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                LoadModule(availableModules[i]);
            }
        }
    }
    
    /// <summary>
    /// Load a specific game module
    /// </summary>
    public void LoadModule(GameModule module)
    {
        if (module == null)
        {
            Debug.LogWarning("Cannot load null module");
            return;
        }
        
        // Unload current module
        UnloadCurrentModule();
        
        // Load new module
        if (module.modulePrefab != null)
        {
            currentModuleInstance = Instantiate(module.modulePrefab, moduleContainer);
            currentModule = module;
            currentModule.isActive = true;
            
            Debug.Log($"Loaded module: {module.moduleName}");
        }
        else
        {
            Debug.LogWarning($"Module {module.moduleName} has no prefab assigned");
        }
    }
    
    /// <summary>
    /// Load module by name
    /// </summary>
    public void LoadModuleByName(string moduleName)
    {
        GameModule module = availableModules.Find(m => m.moduleName == moduleName);
        if (module != null)
        {
            LoadModule(module);
        }
        else
        {
            Debug.LogWarning($"Module not found: {moduleName}");
        }
    }
    
    /// <summary>
    /// Load module by ID
    /// </summary>
    public void LoadModuleById(string moduleId)
    {
        GameModule module = availableModules.Find(m => m.moduleId == moduleId);
        if (module != null)
        {
            LoadModule(module);
        }
        else
        {
            Debug.LogWarning($"Module not found: {moduleId}");
        }
    }
    
    /// <summary>
    /// Unload the current module
    /// </summary>
    public void UnloadCurrentModule()
    {
        if (currentModuleInstance != null)
        {
            Destroy(currentModuleInstance);
            currentModuleInstance = null;
        }
        
        if (currentModule != null)
        {
            currentModule.isActive = false;
            currentModule = null;
        }
    }
    
    /// <summary>
    /// Add a new module at runtime
    /// </summary>
    public void RegisterModule(string name, string id, GameObject prefab, string description = "")
    {
        GameModule newModule = new GameModule
        {
            moduleName = name,
            moduleId = id,
            modulePrefab = prefab,
            description = description,
            isActive = false
        };
        
        availableModules.Add(newModule);
        Debug.Log($"Registered new module: {name}");
    }
    
    /// <summary>
    /// Get list of all available modules
    /// </summary>
    public List<GameModule> GetAvailableModules()
    {
        return new List<GameModule>(availableModules);
    }
    
    /// <summary>
    /// Get currently active module
    /// </summary>
    public GameModule GetCurrentModule()
    {
        return currentModule;
    }
}
