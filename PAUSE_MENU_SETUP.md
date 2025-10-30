# 🎮 Hướng Dẫn Setup Pause Menu & Options

## 🐛 Fix: Pause Menu Không Hoạt Động Khi Build

### Nguyên Nhân Thường Gặp:

1. **Thiếu Event System** trong Scene
2. **pauseMenuUI chưa được assign**
3. **PauseMenu script không active**
4. **Input Settings chưa đúng**

---

## ✅ Checklist Fix Pause Menu

### 1. Kiểm Tra Event System

**Trong mỗi Scene gameplay:**
```
Hierarchy → Tìm "EventSystem"
```

**Nếu KHÔNG có:**
```
1. Right Click Hierarchy
2. UI → Event System
3. ✅ Xong!
```

### 2. Kiểm Tra PauseMenu Setup

**Trong Scene có gameplay (Level 1, Level 2...):**

```
Hierarchy:
├── Canvas
│   ├── PauseMenuPanel (GameObject với Image)
│   │   ├── Title (Text: "PAUSED")
│   │   ├── ResumeButton
│   │   ├── OptionsButton
│   │   ├── MainMenuButton
│   │   └── QuitButton
│   └── OptionsMenuPanel (GameObject với Image)
│       └── (Controls info - xem bên dưới)
└── PauseMenuManager (Empty GameObject)
    └── PauseMenu Script
```

**Setup PauseMenu Script:**
```
1. Chọn PauseMenuManager GameObject
2. Inspector → PauseMenu component
3. Assign:
   - Pause Menu UI: Kéo PauseMenuPanel vào đây
   - Options Menu UI: Kéo OptionsMenuPanel vào đây
4. Save Scene
```

### 3. Setup Buttons

**Resume Button:**
```
- OnClick() → PauseMenuManager → PauseMenu.Resume()
```

**Options Button:**
```
- OnClick() → PauseMenuManager → PauseMenu.OpenOptions()
```

**Main Menu Button:**
```
- OnClick() → PauseMenuManager → PauseMenu.LoadMainMenu()
```

**Quit Button:**
```
- OnClick() → PauseMenuManager → PauseMenu.QuitGame()
```

---

## 🎨 Tạo Options Menu (Hướng Dẫn Controls)

### Cấu Trúc UI:

```
OptionsMenuPanel (Panel)
├── Background (Image - màu tối, alpha 0.95)
├── Title (Text: "CONTROLS & GUIDE")
├── CloseButton (Button: "Back" hoặc "X")
│   └── OnClick() → PauseMenu.CloseOptions()
├── ScrollView (Scroll Rect)
│   └── Content
│       ├── MovementSection
│       │   ├── SectionTitle (Text: "MOVEMENT")
│       │   ├── ControlItem1 (Text: "A/D or ← → : Move Left/Right")
│       │   ├── ControlItem2 (Text: "Space : Jump")
│       │   └── ControlItem3 (Text: "Shift : Run")
│       ├── CombatSection
│       │   ├── SectionTitle (Text: "COMBAT")
│       │   ├── ControlItem1 (Text: "Mouse Click : Attack")
│       │   ├── ControlItem2 (Text: "Q : Special Skill")
│       │   └── ControlItem3 (Text: "E : Shield")
│       ├── SystemSection
│       │   ├── SectionTitle (Text: "SYSTEM")
│       │   ├── ControlItem1 (Text: "ESC : Pause Menu")
│       │   └── ControlItem2 (Text: "Tab : Map (if available)")
│       └── ObjectiveSection
│           ├── SectionTitle (Text: "OBJECTIVE")
│           └── ObjectiveText (Text: "Defeat all enemies and reach the portal!")
```

---

## 📝 Nội Dung Hướng Dẫn Mẫu

### Movement (Di Chuyển):
```
A / ← : Move Left
D / → : Move Right
Space : Jump
Shift : Run/Sprint
```

### Combat (Chiến Đấu):
```
Mouse Click / Z : Basic Attack
Q / X : Special Skill
E / C : Shield/Block
```

### System (Hệ Thống):
```
ESC : Pause Menu
Tab : Open Map
I : Inventory (if available)
```

### Objective (Nhiệm Vụ):
```
🎯 Main Quest:
- Defeat all enemies in the level
- Find and activate checkpoints
- Reach the portal to next level

💀 Boss Fight:
- Watch for attack patterns
- Dodge red indicators
- Use special skills wisely

⭐ Bonus:
- Collect all coins
- Find hidden items
- Complete without dying
```

---

## 🎨 Thiết Kế UI Đẹp (Khuyến Nghị)

### Colors:
```css
Background: #1a1a2e (Dark Blue)
Panel: #16213e (Darker Blue, Alpha 0.95)
Title: #ffffff (White)
Text: #e0e0e0 (Light Gray)
Accent: #0f4c75 (Blue)
Button: #3282b8 (Light Blue)
Button Hover: #bbe1fa (Very Light Blue)
```

### Fonts:
```
Title: Bold, Size 36-48
Section Headers: Bold, Size 24-28
Controls Text: Regular, Size 18-20
```

### Layout:
```
Padding: 20-30px
Spacing: 10-15px between items
Button Size: 200x50px
Icon Size: 32x32px (if using icons)
```

---

## 🔧 Script Setup

### 1. PauseMenu.cs (Đã Update)

Đã thêm:
- ✅ `optionsMenuUI` reference
- ✅ `OpenOptions()` method
- ✅ `CloseOptions()` method
- ✅ ESC để đóng Options
- ✅ Initialize check

### 2. OptionsMenu.cs (Mới)

Tính năng:
- Tab switching (Controls/Audio/About)
- Sound effects khi click
- Panel management

---

## 🎯 Quick Setup (5 Phút)

### Bước 1: Tạo UI Structure

```
1. Mở Scene có gameplay
2. Tạo Canvas (nếu chưa có)
3. Tạo PauseMenuPanel:
   - Right Click Canvas → UI → Panel
   - Đặt tên: PauseMenuPanel
   - Thêm Title, Buttons (Resume, Options, Main Menu, Quit)
4. Tạo OptionsMenuPanel:
   - Right Click Canvas → UI → Panel
   - Đặt tên: OptionsMenuPanel
   - Thêm ScrollView với controls info
5. Tắt cả 2 panels (uncheck ở Inspector)
```

### Bước 2: Setup PauseMenu Script

```
1. Tạo Empty GameObject: PauseMenuManager
2. Add Component: PauseMenu
3. Assign:
   - Pause Menu UI → PauseMenuPanel
   - Options Menu UI → OptionsMenuPanel
```

### Bước 3: Connect Buttons

```
Resume Button → OnClick() → PauseMenu.Resume()
Options Button → OnClick() → PauseMenu.OpenOptions()
Main Menu Button → OnClick() → PauseMenu.LoadMainMenu()
Quit Button → OnClick() → PauseMenu.QuitGame()
Back Button (in Options) → OnClick() → PauseMenu.CloseOptions()
```

### Bước 4: Test

```
1. Play Scene
2. Nhấn ESC → Pause menu hiện
3. Click Options → Controls hiện
4. Nhấn ESC → Về Pause menu
5. Nhấn ESC → Resume game
```

---

## 🐛 Troubleshooting

### Pause Menu Không Hiện?

**Check:**
```
☐ pauseMenuUI đã assign?
☐ pauseMenuUI.SetActive(false) ở Start?
☐ Canvas có trong Scene?
☐ EventSystem có trong Scene?
```

### ESC Không Hoạt Động?

**Check:**
```
☐ PauseMenu script enabled?
☐ GameObject active?
☐ Không có script khác catch ESC?
☐ Build Settings → Player Settings → Input System đúng?
```

### Buttons Không Click Được?

**Check:**
```
☐ EventSystem có trong Scene?
☐ Canvas → Render Mode = Screen Space - Overlay?
☐ Button có Graphic Raycaster?
☐ Button không bị che bởi panel khác?
```

### Build Game Không Hoạt Động?

**Check:**
```
☐ Tất cả Scenes đã add vào Build Settings?
☐ Scene Index đúng trong LoadScene()?
☐ Input Manager có ESC key?
```

---

## 📱 Build Settings

### Thêm Scenes Vào Build:

```
1. File → Build Settings
2. Add Open Scenes hoặc kéo thả Scenes vào
3. Đảm bảo thứ tự:
   - Scene 0: Main Menu
   - Scene 1: Level 1
   - Scene 2: Level 2
   - ...
```

### Input Settings:

```
Edit → Project Settings → Input Manager
→ Kiểm tra "Cancel" axis có map với ESC
```

---

## 🎨 Template HTML Cho Controls (Copy & Paste)

Nếu muốn dùng TextMeshPro với Rich Text:

```html
<size=28><b>MOVEMENT</b></size>

<b>A / ←</b> : Move Left
<b>D / →</b> : Move Right
<b>Space</b> : Jump
<b>Shift</b> : Run

<size=28><b>COMBAT</b></size>

<b>Mouse Click</b> : Attack
<b>Q</b> : Special Skill
<b>E</b> : Shield

<size=28><b>SYSTEM</b></size>

<b>ESC</b> : Pause Menu
<b>Tab</b> : Map

<size=28><b>OBJECTIVE</b></size>

🎯 Defeat all enemies
⭐ Collect coins
🚪 Reach the portal
```

---

## ✨ Bonus: Audio Settings Panel

Nếu muốn thêm Audio Settings vào Options:

```
1. Tạo AudioPanel trong OptionsMenuPanel
2. Add AudioSettingsUI script
3. Thêm Sliders cho Music/SFX volume
4. Connect với AudioManager
```

Xem `AudioSettingsUI.cs` đã tạo trước đó!

---

**Chúc bạn setup thành công! 🎮**
