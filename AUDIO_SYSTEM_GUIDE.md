# 🎵 Hướng Dẫn Sử Dụng Hệ Thống Âm Thanh - Game 2D

## 📋 Tổng Quan

Hệ thống âm thanh đã được triển khai hoàn chỉnh cho game 2D với các tính năng:
- ✅ Background Music với fade in/out
- ✅ Sound Effects cho Player, Enemy, Boss
- ✅ UI Sound Effects
- ✅ Volume Controls (Music, SFX, Ambient)
- ✅ Singleton Pattern để dễ dàng truy cập
- ✅ Lưu settings với PlayerPrefs

---

## 🎮 Cấu Trúc Files

### Files Chính:
1. **AudioManager.cs** - Core audio system (Assets/)
2. **AudioSettingsUI.cs** - UI controls cho volume (Assets/Script/Menu/)
3. **Controller.cs** - Player sounds (Assets/Script/Control/)
4. **CharacterStat.cs** - Player hurt/death sounds (Assets/Script/CharacterStatManager/)
5. **EnemyStats.cs** - Enemy sounds (Assets/Script/Enemy/)
6. **BeserkStats.cs** - Boss sounds (Assets/Script/Enemy/)
7. **MainMenu.cs** - Menu button sounds (Assets/Script/Menu/)
8. **PauseMenu.cs** - Pause menu sounds (Assets/Script/Menu/)

---

## 🔧 Setup Trong Unity Editor

### Bước 1: Tạo AudioManager GameObject

1. Trong Scene, tạo Empty GameObject mới: `GameObject > Create Empty`
2. Đặt tên: **AudioManager**
3. Add Component: **AudioManager** script
4. AudioManager sẽ tự động tạo 3 AudioSource components:
   - Music Source (background music)
   - SFX Source (sound effects)
   - Ambient Source (ambient sounds)

### Bước 2: Import Audio Files

Chuẩn bị các file âm thanh sau và import vào folder `Assets/Audio/`:

#### Background Music:
- `background_music.mp3` - Nhạc nền chính
- `boss_music.mp3` - Nhạc khi đánh boss

#### Player SFX:
- `player_jump.wav`
- `player_attack.wav`
- `player_hurt.wav`
- `player_death.wav`
- `player_footstep.wav`
- `player_land.wav`

#### Enemy/Boss SFX:
- `enemy_attack.wav`
- `enemy_hurt.wav`
- `enemy_death.wav`
- `boss_attack.wav`
- `boss_hurt.wav`
- `boss_death.wav`
- `boss_roar.wav`

#### Environment SFX:
- `checkpoint.wav`
- `wall_touch.wav`
- `portal_in.wav`
- `portal_out.wav`
- `item_pickup.wav`
- `trap_activate.wav`

#### UI SFX:
- `button_click.wav`
- `menu_open.wav`
- `menu_close.wav`

### Bước 3: Assign Audio Clips

1. Chọn **AudioManager** GameObject
2. Trong Inspector, kéo thả các audio clips vào các slot tương ứng:
   - Background Music section
   - Player SFX section
   - Enemy/Boss SFX section
   - Environment SFX section
   - UI SFX section

### Bước 4: Cấu Hình Settings

Trong AudioManager Inspector:

**Volume Settings:**
- Music Volume: 0.7 (70%)
- SFX Volume: 1.0 (100%)
- Ambient Volume: 0.5 (50%)

**Audio Settings:**
- Music Fade Duration: 1.5 seconds
- Play Music On Start: ✓ (checked)

---

## 🎨 Tạo UI Settings Menu (Optional)

### Bước 1: Tạo Settings Panel

1. Trong Canvas, tạo Panel mới: `UI > Panel`
2. Đặt tên: **AudioSettingsPanel**

### Bước 2: Thêm Volume Sliders

Tạo 3 Sliders cho Music, SFX, và Ambient:

```
AudioSettingsPanel/
├── MusicVolumeSlider (UI > Slider)
│   └── Label (Text): "Music Volume"
│   └── ValueText (Text): "70%"
├── SFXVolumeSlider (UI > Slider)
│   └── Label (Text): "SFX Volume"
│   └── ValueText (Text): "100%"
└── AmbientVolumeSlider (UI > Slider)
    └── Label (Text): "Ambient Volume"
    └── ValueText (Text): "50%"
```

### Bước 3: Setup AudioSettingsUI Script

1. Add Component **AudioSettingsUI** vào AudioSettingsPanel
2. Assign các Sliders và Text components vào script
3. (Optional) Thêm Toggle buttons cho Mute

---

## 💻 Sử Dụng Trong Code

### Cách Gọi Sound Effects:

```csharp
// Player sounds
AudioManager.Instance.PlayPlayerJump();
AudioManager.Instance.PlayPlayerAttack();
AudioManager.Instance.PlayPlayerHurt();
AudioManager.Instance.PlayPlayerDeath();

// Enemy sounds
AudioManager.Instance.PlayEnemyAttack();
AudioManager.Instance.PlayEnemyHurt();
AudioManager.Instance.PlayEnemyDeath();

// Boss sounds
AudioManager.Instance.PlayBossAttack();
AudioManager.Instance.PlayBossHurt();
AudioManager.Instance.PlayBossDeath();
AudioManager.Instance.PlayBossRoar();

// Environment sounds
AudioManager.Instance.PlayCheckpoint();
AudioManager.Instance.PlayPortalIn();
AudioManager.Instance.PlayTrapActivate();

// UI sounds
AudioManager.Instance.PlayButtonClick();
AudioManager.Instance.PlayMenuOpen();
AudioManager.Instance.PlayMenuClose();

// Custom sound with volume control
AudioManager.Instance.PlaySFX(customClip, 0.5f); // 50% volume
```

### Điều Khiển Music:

```csharp
// Play background music với fade in
AudioManager.Instance.PlayBackgroundMusic();

// Play boss music
AudioManager.Instance.PlayBossMusic();

// Stop music với fade out
AudioManager.Instance.StopMusic(true);

// Play custom music
AudioManager.Instance.PlayMusic(customMusicClip, true);
```

### Điều Chỉnh Volume:

```csharp
// Set volumes (0.0 to 1.0)
AudioManager.Instance.SetMusicVolume(0.7f);
AudioManager.Instance.SetSFXVolume(1.0f);
AudioManager.Instance.SetAmbientVolume(0.5f);

// Get current volumes
float musicVol = AudioManager.Instance.GetMusicVolume();
float sfxVol = AudioManager.Instance.GetSFXVolume();
```

---

## 🎯 Các Tính Năng Đã Tích Hợp

### ✅ Player Actions:
- **Jump**: Phát sound khi nhảy
- **Attack**: Phát sound khi tấn công (Fight, Special Skill)
- **Land**: Phát sound khi tiếp đất (nếu rơi nhanh)
- **Hurt**: Phát sound khi bị damage
- **Death**: Phát sound khi chết
- **Trap**: Phát sound khi chạm bẫy

### ✅ Enemy/Boss:
- **Hurt**: Phát sound khi bị đánh
- **Death**: Phát sound khi chết
- **Boss Roar**: Phát sound khi boss xuất hiện
- **Boss Music**: Tự động chuyển nhạc khi gặp boss
- **Return Music**: Tự động trở về nhạc nền sau khi boss chết

### ✅ Menu/UI:
- **Button Click**: Phát sound khi click button
- **Menu Open**: Phát sound khi mở pause menu
- **Menu Close**: Phát sound khi đóng pause menu

---

## 🔊 Khuyến Nghị Audio Format

### Music Files:
- **Format**: MP3 hoặc OGG
- **Sample Rate**: 44100 Hz
- **Bitrate**: 128-192 kbps
- **Stereo**: Yes

### Sound Effects:
- **Format**: WAV (uncompressed) hoặc OGG (compressed)
- **Sample Rate**: 44100 Hz
- **Bit Depth**: 16-bit
- **Mono/Stereo**: Mono (tiết kiệm dung lượng)

### Unity Import Settings:

**Music:**
- Load Type: Streaming
- Compression Format: Vorbis
- Quality: 70-100

**SFX:**
- Load Type: Decompress On Load (ngắn) hoặc Compressed In Memory (dài)
- Compression Format: PCM (chất lượng cao) hoặc ADPCM (tiết kiệm)

---

## 🐛 Troubleshooting

### Không Nghe Thấy Âm Thanh?

1. **Kiểm tra AudioManager:**
   - Đảm bảo AudioManager GameObject tồn tại trong Scene
   - Kiểm tra các AudioSource components đã được tạo

2. **Kiểm tra Audio Clips:**
   - Đảm bảo đã assign audio clips vào AudioManager
   - Kiểm tra import settings của audio files

3. **Kiểm tra Volume:**
   - Volume sliders > 0
   - Audio Listener có trong Scene (thường ở Main Camera)

4. **Kiểm tra Code:**
   - `AudioManager.Instance != null` trước khi gọi
   - Không có lỗi trong Console

### Sound Bị Delay?

- Sử dụng WAV format cho SFX thay vì MP3
- Set Load Type = "Decompress On Load" cho SFX ngắn
- Giảm buffer size trong Audio Settings

### Music Không Loop?

- Kiểm tra Music Source có Loop = true
- Đảm bảo gọi `PlayMusic()` thay vì `PlaySFX()`

---

## 📝 Notes Quan Trọng

1. **AudioManager là Singleton**: Chỉ có 1 instance, persist qua scenes
2. **DontDestroyOnLoad**: AudioManager không bị destroy khi chuyển scene
3. **PlayerPrefs**: Volume settings được lưu tự động
4. **Null Checks**: Luôn check `AudioManager.Instance != null`
5. **Boss Detection**: Set `isBoss = true` trong BeserkStats Inspector

---

## 🎓 Ví Dụ Sử Dụng

### Thêm Sound Cho Skill Mới:

```csharp
// Trong script của skill
public void UseSpecialSkill()
{
    // Logic của skill
    animator.SetTrigger("SpecialSkill");
    
    // Phát sound effect
    if (AudioManager.Instance != null)
        AudioManager.Instance.PlayPlayerAttack();
}
```

### Tạo Checkpoint Với Sound:

```csharp
private void OnTriggerEnter2D(Collider2D collision)
{
    if (collision.CompareTag("Player"))
    {
        // Save checkpoint
        SaveCheckpoint();
        
        // Play sound
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayCheckpoint();
    }
}
```

### Thêm Portal Sound:

```csharp
public void EnterPortal()
{
    if (AudioManager.Instance != null)
        AudioManager.Instance.PlayPortalIn();
        
    // Teleport logic
    StartCoroutine(TeleportPlayer());
}

IEnumerator TeleportPlayer()
{
    yield return new WaitForSeconds(0.5f);
    
    // Teleport
    transform.position = targetPosition;
    
    if (AudioManager.Instance != null)
        AudioManager.Instance.PlayPortalOut();
}
```

---

## ✨ Tính Năng Nâng Cao (Có Thể Mở Rộng)

### 1. Audio Mixing Groups:
- Tạo Audio Mixer trong Unity
- Thêm effects (Reverb, Echo, Low Pass Filter)
- Control volume groups

### 2. 3D Spatial Audio:
- Chuyển AudioSource sang 3D mode
- Set Spatial Blend = 1
- Configure Min/Max Distance

### 3. Dynamic Music:
- Crossfade giữa các music layers
- Adaptive music theo game state
- Music intensity theo action

### 4. Sound Pools:
- Tạo object pool cho SFX
- Tránh tạo/destroy AudioSource liên tục
- Tối ưu performance

---

## 📞 Hỗ Trợ

Nếu gặp vấn đề hoặc cần thêm tính năng:
1. Kiểm tra Console log để xem lỗi
2. Verify tất cả audio clips đã được assign
3. Test với audio clips mẫu trước
4. Đọc kỹ phần Troubleshooting

---

**Chúc bạn làm việc hiệu quả! 🎮🎵**
