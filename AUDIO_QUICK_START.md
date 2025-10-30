# 🎵 Audio System - Quick Start Guide

## ⚡ Setup Nhanh (5 Phút)

### Bước 1: Tạo AudioManager
1. Mở Scene chính của game
2. Tạo Empty GameObject: `GameObject > Create Empty`
3. Đặt tên: **AudioManager**
4. Add Component: **AudioManager** script
5. ✅ Xong! AudioManager sẽ tự động setup

### Bước 2: Import Audio Files
1. Chuẩn bị file âm thanh (xem `AUDIO_FILES_NEEDED.txt`)
2. Kéo thả vào folder `Assets/Audio/`
3. Chọn AudioManager GameObject
4. Assign audio clips vào các slot trong Inspector

### Bước 3: Test
1. Add **AudioTester** script vào bất kỳ GameObject nào
2. Play game
3. Nhấn phím để test:
   - `1-6`: Player sounds
   - `Q,W,E`: Enemy sounds
   - `A,S,D,F`: Boss sounds
   - `M,B,N`: Music controls
   - `H`: Show help

---

## 📁 Files Đã Tạo

### Core System:
- ✅ `AudioManager.cs` - Hệ thống âm thanh chính
- ✅ `AudioSettingsUI.cs` - UI controls cho volume
- ✅ `AudioTester.cs` - Script test âm thanh

### Đã Tích Hợp:
- ✅ `Controller.cs` - Player jump, attack, land sounds
- ✅ `CharacterStat.cs` - Player hurt, death sounds
- ✅ `EnemyStats.cs` - Enemy hurt, death sounds
- ✅ `BeserkStats.cs` - Boss sounds + music switching
- ✅ `MainMenu.cs` - Menu button sounds
- ✅ `PauseMenu.cs` - Pause menu sounds

### Documentation:
- ✅ `AUDIO_SYSTEM_GUIDE.md` - Hướng dẫn chi tiết
- ✅ `AUDIO_FILES_NEEDED.txt` - Danh sách file cần có
- ✅ `AUDIO_QUICK_START.md` - Guide này

---

## 🎮 Sử Dụng Cơ Bản

### Trong Code:
```csharp
// Phát sound effect
AudioManager.Instance.PlayPlayerJump();
AudioManager.Instance.PlayEnemyHurt();
AudioManager.Instance.PlayBossRoar();

// Điều khiển music
AudioManager.Instance.PlayBackgroundMusic();
AudioManager.Instance.PlayBossMusic();

// Điều chỉnh volume
AudioManager.Instance.SetMusicVolume(0.7f);
AudioManager.Instance.SetSFXVolume(1.0f);
```

---

## ✨ Tính Năng

- ✅ **Singleton Pattern** - Dễ dàng truy cập từ mọi nơi
- ✅ **Music Fade** - Chuyển nhạc mượt mà
- ✅ **Volume Control** - Riêng biệt cho Music/SFX/Ambient
- ✅ **Boss Music** - Tự động chuyển khi gặp boss
- ✅ **Save Settings** - Lưu volume với PlayerPrefs
- ✅ **Null Safe** - Hoạt động ngay cả khi chưa có audio files

---

## 🔊 Nguồn Audio Miễn Phí

- [freesound.org](https://freesound.org)
- [zapsplat.com](https://www.zapsplat.com)
- [mixkit.co](https://mixkit.co)
- [opengameart.org](https://opengameart.org)

---

## 📖 Đọc Thêm

Xem `AUDIO_SYSTEM_GUIDE.md` để biết:
- Hướng dẫn chi tiết từng bước
- Cách tạo UI Settings menu
- Troubleshooting
- Advanced features
- Code examples

---

**Chúc bạn thành công! 🎮🎵**
