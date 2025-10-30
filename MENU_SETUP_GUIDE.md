# 🎮 Hướng Dẫn Setup Main Menu & Pause Menu

## 📋 Phân Biệt 2 Menus

### 🏠 Main Menu (Scene "Main Menu")
**Khi:** Lúc bắt đầu game
**Buttons:**
- **PLAY** → Bắt đầu game (LoadScene 1)
- **OPTIONS** → Xem hướng dẫn chơi
- **SETTINGS** → Chỉnh âm thanh
- **EXIT** → Thoát game

### ⏸️ Pause Menu (Trong gameplay)
**Khi:** Đang chơi, nhấn ESC
**Buttons:**
- **CONTINUE** (Resume) → Tiếp tục chơi
- **RESTART** → Chơi lại level
- **OPTIONS** → Xem hướng dẫn
- **MAIN MENU** → Về menu chính
- **EXIT** → Thoát game

---

## 🏠 SETUP MAIN MENU

### Cấu Trúc UI:

```
Main Menu Scene/
└── Canvas
    ├── MainMenuPanel (Panel - Active)
    │   ├── Title (Text: "YOUR GAME NAME")
    │   ├── PlayButton (Text: "PLAY")
    │   ├── OptionsButton (Text: "OPTIONS")
    │   ├── SettingsButton (Text: "SETTINGS")
    │   └── ExitButton (Text: "EXIT")
    │
    ├── OptionsPanel (Panel - Inactive)
    │   ├── Title (Text: "HOW TO PLAY")
    │   ├── ScrollView
    │   │   └── Content (Controls text)
    │   └── BackButton (Text: "BACK")
    │
    └── SettingsPanel (Panel - Inactive)
        ├── Title (Text: "SETTINGS")
        ├── AudioSettingsUI component
        ├── Music Slider
        ├── SFX Slider
        └── BackButton (Text: "BACK")
```

### Bước Setup:

#### 1. Tạo Main Menu Panel
```
1. Trong Canvas, tạo Panel: MainMenuPanel
2. Thêm Title text (tên game)
3. Thêm 4 buttons:
   - Play Button
   - Options Button
   - Settings Button
   - Exit Button
4. Giữ panel ACTIVE (checked)
```

#### 2. Tạo Options Panel
```
1. Tạo Panel: OptionsPanel
2. Thêm Title: "HOW TO PLAY"
3. Thêm ScrollView với controls text
4. Thêm Back Button
5. TẮT panel (uncheck Active)
```

#### 3. Tạo Settings Panel
```
1. Tạo Panel: SettingsPanel
2. Thêm Title: "SETTINGS"
3. Thêm Music Volume Slider
4. Thêm SFX Volume Slider
5. Add AudioSettingsUI component
6. Thêm Back Button
7. TẮT panel (uncheck Active)
```

#### 4. Setup MainMenu Script
```
1. Chọn Canvas (hoặc tạo Empty GameObject: MainMenuManager)
2. Add Component: MainMenu
3. Assign:
   - Main Menu Panel → MainMenuPanel
   - Options Panel → OptionsPanel
   - Settings Panel → SettingsPanel
```

#### 5. Connect Buttons

**Play Button:**
```
OnClick() → MainMenu.playGame()
```

**Options Button:**
```
OnClick() → MainMenu.ShowOptions()
```

**Settings Button:**
```
OnClick() → MainMenu.ShowSettings()
```

**Exit Button:**
```
OnClick() → MainMenu.quitGame()
```

**Back Buttons (trong Options & Settings):**
```
OnClick() → MainMenu.ShowMainMenu()
```

---

## ⏸️ SETUP PAUSE MENU

### Cấu Trúc UI:

```
Gameplay Scene (Level 1, Level 2...)/
└── Canvas
    ├── PauseMenuPanel (Panel - Inactive)
    │   ├── Title (Text: "PAUSED")
    │   ├── ContinueButton (Text: "CONTINUE")
    │   ├── RestartButton (Text: "RESTART")
    │   ├── OptionsButton (Text: "OPTIONS")
    │   ├── MainMenuButton (Text: "MAIN MENU")
    │   └── ExitButton (Text: "EXIT")
    │
    └── OptionsMenuPanel (Panel - Inactive)
        ├── Title (Text: "CONTROLS")
        ├── ScrollView
        │   └── Content (Controls text)
        └── BackButton (Text: "BACK")
```

### Bước Setup:

#### 1. Tạo Pause Menu Panel
```
1. Trong Canvas, tạo Panel: PauseMenuPanel
2. Thêm Title: "PAUSED"
3. Thêm 5 buttons:
   - Continue Button (Resume)
   - Restart Button
   - Options Button
   - Main Menu Button
   - Exit Button
4. TẮT panel (uncheck Active)
```

#### 2. Tạo Options Menu Panel
```
1. Tạo Panel: OptionsMenuPanel
2. Thêm Title: "CONTROLS"
3. Thêm ScrollView với controls text
4. Thêm Back Button
5. TẮT panel (uncheck Active)
```

#### 3. Setup PauseMenu Script
```
1. Tạo Empty GameObject: PauseMenuManager
2. Add Component: PauseMenu
3. Assign:
   - Pause Menu UI → PauseMenuPanel
   - Options Menu UI → OptionsMenuPanel
```

#### 4. Connect Buttons

**Continue Button:**
```
OnClick() → PauseMenu.Resume()
```

**Restart Button:**
```
OnClick() → PauseMenu.RestartLevel()
```

**Options Button:**
```
OnClick() → PauseMenu.OpenOptions()
```

**Main Menu Button:**
```
OnClick() → PauseMenu.LoadMainMenu()
```

**Exit Button:**
```
OnClick() → PauseMenu.QuitGame()
```

**Back Button (trong Options):**
```
OnClick() → PauseMenu.CloseOptions()
```

---

## 🎨 UI Design Tips

### Colors Scheme:
```
Background: #1a1a2e (Dark Blue)
Panel: rgba(22, 33, 62, 0.95) (Semi-transparent)
Title: #FFD700 (Gold)
Button Normal: #3282b8 (Blue)
Button Hover: #bbe1fa (Light Blue)
Button Pressed: #0f4c75 (Dark Blue)
Text: #ffffff (White)
```

### Font Sizes:
```
Game Title: 48-64px
Panel Title: 32-40px
Button Text: 20-24px
Body Text: 16-18px
```

### Button Style:
```
Width: 200-250px
Height: 50-60px
Border Radius: 5-10px
Shadow: 0 4px 8px rgba(0,0,0,0.3)
```

---

## 🔧 Thêm Restart Function

Cần thêm method Restart vào PauseMenu.cs:

```csharp
public void RestartLevel()
{
    // Play button click sound
    if (AudioManager.Instance != null)
        AudioManager.Instance.PlayButtonClick();
        
    Time.timeScale = 1f; // reset time scale
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
}
```

---

## 📝 Controls Text Template

Copy vào ScrollView Content:

```
🎮 GAME CONTROLS

🏃 MOVEMENT
A / ← : Move Left
D / → : Move Right
Space : Jump
Shift : Run

⚔️ COMBAT
Mouse Click : Attack
Q : Special Skill
E : Shield

⚙️ SYSTEM
ESC : Pause Menu

🎯 OBJECTIVE
💀 Defeat all enemies
⭐ Collect coins
🚪 Reach the portal
```

---

## ✅ Testing Checklist

### Main Menu:
```
☐ Click PLAY → Load gameplay scene
☐ Click OPTIONS → Show controls
☐ Click SETTINGS → Show audio settings
☐ Click EXIT → Quit game (trong build)
☐ Click BACK → Return to main menu
☐ All buttons có sound effects
```

### Pause Menu:
```
☐ Nhấn ESC → Pause menu hiện
☐ Game pause (Time.timeScale = 0)
☐ Click CONTINUE → Resume game
☐ Click RESTART → Reload level
☐ Click OPTIONS → Show controls
☐ Click MAIN MENU → Load main menu
☐ Click EXIT → Quit game
☐ Nhấn ESC trong Options → Back to pause menu
☐ Nhấn ESC trong Pause → Resume game
```

---

## 🐛 Common Issues

### Issue 1: Buttons Không Hoạt Động
**Fix:**
```
☐ Check EventSystem có trong Scene
☐ Check Button có Graphic Raycaster
☐ Check OnClick() đã setup đúng
```

### Issue 2: ESC Không Pause
**Fix:**
```
☐ Check PauseMenu script enabled
☐ Check pauseMenuUI assigned
☐ Check GameObject active
```

### Issue 3: Panel Không Ẩn/Hiện
**Fix:**
```
☐ Check panels assigned trong Inspector
☐ Check panels có Canvas Group (nếu dùng)
☐ Check không có script khác control visibility
```

### Issue 4: Time.timeScale Không Reset
**Fix:**
```
☐ Luôn set Time.timeScale = 1f trước khi LoadScene
☐ Check không có script khác set timeScale
```

---

## 📱 Build Settings

### Scene Order:
```
Build Settings → Scenes In Build:
0. Main Menu
1. Character Selection (nếu có)
2. Level 1
3. Level 2
4. Level 3
...
```

### Player Settings:
```
Company Name: Your Company
Product Name: Your Game
Default Icon: Your Icon
Resolution: 1920x1080 (hoặc tùy chọn)
Fullscreen Mode: Fullscreen Window
```

---

## 🎯 Quick Setup Summary

### Main Menu Scene:
```
1. Tạo 3 panels: Main, Options, Settings
2. Add MainMenu script
3. Assign panels
4. Connect buttons
5. Test
```

### Gameplay Scenes:
```
1. Tạo 2 panels: Pause, Options
2. Add PauseMenu script
3. Assign panels
4. Connect buttons
5. Test ESC key
```

---

**Done! Bây giờ bạn có:**
- ✅ Main Menu với PLAY, OPTIONS, SETTINGS, EXIT
- ✅ Pause Menu với CONTINUE, RESTART, OPTIONS, MAIN MENU, EXIT
- ✅ Options hiển thị controls
- ✅ Settings điều chỉnh audio
- ✅ ESC để pause/unpause
- ✅ Sound effects cho tất cả buttons

🎮 **Chúc bạn thành công!**
