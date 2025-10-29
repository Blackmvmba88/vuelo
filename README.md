# 🚀 Vuelo - Multiverso Mamba

**Simulador cuántico modular inspirado en el vuelo sci-fi. Cada jugador crea su propio universo.**

## 📋 Descripción

Vuelo es un proyecto base Unity WebGL que proporciona un entorno 3D sci-fi minimalista con controles de cámara libre, iluminación neón, materiales metálicos reflectantes y un HUD transparente. El proyecto está diseñado como plataforma modular para desarrollar y conectar micro-juegos en el concepto "Multiverso Mamba".

## ✨ Características

### 🎮 Cámara Libre
- Control fluido de movimiento con interpolación suave
- WASD/Flechas: Movimiento horizontal
- Mouse: Control de vista con pitch clamping
- Shift: Sprint mode (velocidad multiplicada)
- ESC: Liberar cursor

### 🌅 Sistema Día/Noche
- Cambio dinámico entre día y noche
- Transiciones suaves de iluminación
- Ajuste automático de luces neón
- Control con tecla 'T'
- Cambios en iluminación ambiental, direccional y niebla

### 🌌 Entorno Sci-Fi
- Materiales metálicos con alto reflejo (metallic/smoothness)
- Iluminación neón procedural (cyan y magenta)
- Niebla atmosférica para profundidad
- Generación automática de entorno al iniciar

### 📊 HUD Transparente
- FPS counter en tiempo real
- Posición de cámara (X, Y, Z)
- Guía de controles integrada
- Información de estado del sistema
- Toggle con tecla 'H'

### 🔧 Sistema Modular
- Framework para cargar micro-juegos dinámicamente
- GameModuleManager para gestión de módulos
- Arquitectura preparada para expansión
- Soporte para múltiples universos/experiencias

## 🛠️ Estructura del Proyecto

```
vuelo/
├── Assets/
│   ├── Scenes/
│   │   └── MainScene.unity          # Escena principal
│   ├── Scripts/
│   │   ├── FreeCam.cs               # Control de cámara libre
│   │   ├── DayNightCycle.cs         # Sistema día/noche
│   │   ├── TransparentHUD.cs        # Sistema de HUD
│   │   ├── GameModuleManager.cs     # Gestor de módulos
│   │   └── SciFiEnvironment.cs      # Generador de entorno
│   ├── Materials/                   # Materiales del proyecto
│   └── Prefabs/                     # Prefabs reutilizables
├── ProjectSettings/                 # Configuración Unity
├── Packages/                        # Paquetes y dependencias
├── .gitignore                       # Ignorar archivos Unity
└── README.md                        # Este archivo
```

## 🚀 Inicio Rápido

### Requisitos
- Unity 2022.3.0f1 o superior
- Soporte para WebGL
- Navegador web moderno con WebGL 2.0

### Instalación

1. **Clonar el repositorio:**
```bash
git clone https://github.com/Blackmvmba88/vuelo.git
cd vuelo
```

2. **Abrir en Unity:**
   - Abre Unity Hub
   - Click en "Add" → "Add project from disk"
   - Selecciona la carpeta del proyecto
   - Abre el proyecto con Unity 2022.3 o superior

3. **Abrir la escena principal:**
   - Navega a `Assets/Scenes/MainScene.unity`
   - Haz doble click para abrir

4. **Ejecutar:**
   - Presiona Play en el Editor de Unity
   - O construye para WebGL (File → Build Settings → WebGL → Build)

## 🎮 Controles

| Acción | Control |
|--------|---------|
| Mover adelante/atrás | W/S o ↑/↓ |
| Mover izquierda/derecha | A/D o ←/→ |
| Mirar alrededor | Mouse |
| Sprint (velocidad rápida) | Shift (mantener) |
| Liberar cursor | ESC |
| Cambiar DÍA/NOCHE | T |
| Alternar HUD | H |
| Cambiar módulo | 1-9 (cuando haya módulos) |

## 📦 Build para WebGL

1. Abrir Build Settings: `File → Build Settings`
2. Seleccionar plataforma WebGL
3. Click en "Switch Platform" (si es necesario)
4. Click en "Build" o "Build and Run"
5. Selecciona carpeta de destino
6. Una vez completado, sube los archivos a tu servidor web

### Configuración Recomendada WebGL
- Compression Format: Gzip o Brotli
- Code Optimization: Release
- Exception Support: None (para mejor rendimiento)
- Memory Size: 256 MB (ajustable según necesidad)

## 🔌 Añadir Nuevos Módulos

Para crear un nuevo micro-juego o módulo:

1. **Crear prefab del módulo:**
```csharp
// Tu nuevo módulo debe ser un GameObject con scripts adjuntos
GameObject myModule = new GameObject("MyModule");
// Añade tus componentes y lógica aquí
```

2. **Registrar en GameModuleManager:**
```csharp
GameModuleManager manager = FindObjectOfType<GameModuleManager>();
manager.RegisterModule(
    "Mi Módulo", 
    "module_001", 
    myModulePrefab, 
    "Descripción del módulo"
);
```

3. **Cargar módulo:**
```csharp
manager.LoadModuleById("module_001");
```

## 🎨 Personalización

### Modificar Sistema Día/Noche
Edita `DayNightCycle.cs` o ajusta en el Inspector:
```csharp
// Colores de día
dayAmbientColor = new Color(0.4f, 0.4f, 0.5f);
dayLightIntensity = 1.0f;

// Colores de noche
nightAmbientColor = new Color(0.05f, 0.05f, 0.1f);
nightLightIntensity = 0.2f;

// Velocidad de transición
transitionSpeed = 1.0f;
```

### Modificar Colores Neón
Edita `SciFiEnvironment.cs`:
```csharp
neonColor1 = new Color(0f, 1f, 1f, 1f); // Cyan
neonColor2 = new Color(1f, 0f, 1f, 1f); // Magenta
```

### Ajustar Velocidad de Cámara
Edita `FreeCam.cs` o ajusta en el Inspector:
```csharp
speed = 10f;
lookSpeed = 2f;
sprintMultiplier = 3f;
smoothness = 0.1f;
```

### Cambiar Apariencia del HUD
Edita `TransparentHUD.cs`:
```csharp
hudColor = new Color(0f, 1f, 1f, 0.7f);
fontSize = 14;
```

## 🌐 Roadmap

- [ ] Sistema de portales entre universos
- [ ] Más módulos de micro-juegos
- [ ] Multijugador básico
- [ ] Sistema de personalización de avatar
- [ ] Física espacial mejorada
- [ ] Audio procedural
- [ ] Efectos de partículas avanzados

## 🤝 Contribución

Las contribuciones son bienvenidas! Por favor:

1. Fork el proyecto
2. Crea una rama para tu feature (`git checkout -b feature/AmazingFeature`)
3. Commit tus cambios (`git commit -m 'Add some AmazingFeature'`)
4. Push a la rama (`git push origin feature/AmazingFeature`)
5. Abre un Pull Request

## 📝 Licencia

Este proyecto es parte del Multiverso Mamba - Proyecto experimental de código abierto.

## 👤 Autor

**Mamba Studios**
- GitHub: [@Blackmvmba88](https://github.com/Blackmvmba88)

## 🙏 Agradecimientos

- Comunidad Unity
- Inspiración sci-fi de universos como Tron, Cyberpunk y Mass Effect
- Todos los desarrolladores del ecosistema open-source

---

**"Cada jugador crea su propio universo en el Multiverso Mamba"** 🐍✨
