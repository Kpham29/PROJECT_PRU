using UnityEngine;

/// <summary>
/// Script để test audio system trong game
/// Attach vào GameObject bất kỳ để test sounds bằng keyboard
/// </summary>
public class AudioTester : MonoBehaviour
{
    [Header("Test Controls")]
    [SerializeField] private bool enableTesting = true;
    
    [Header("Info")]
    [TextArea(5, 10)]
    [SerializeField] private string instructions = 
        "KEYBOARD SHORTCUTS:\n\n" +
        "PLAYER SOUNDS:\n" +
        "1 - Jump\n" +
        "2 - Attack\n" +
        "3 - Hurt\n" +
        "4 - Death\n" +
        "5 - Footstep\n" +
        "6 - Land\n\n" +
        "ENEMY SOUNDS:\n" +
        "Q - Enemy Attack\n" +
        "W - Enemy Hurt\n" +
        "E - Enemy Death\n\n" +
        "BOSS SOUNDS:\n" +
        "A - Boss Attack\n" +
        "S - Boss Hurt\n" +
        "D - Boss Death\n" +
        "F - Boss Roar\n\n" +
        "MUSIC:\n" +
        "M - Background Music\n" +
        "B - Boss Music\n" +
        "N - Stop Music\n\n" +
        "ENVIRONMENT:\n" +
        "C - Checkpoint\n" +
        "P - Portal In\n" +
        "O - Portal Out\n" +
        "T - Trap\n\n" +
        "UI:\n" +
        "U - Button Click\n" +
        "I - Menu Open\n" +
        "K - Menu Close";

    private void Update()
    {
        if (!enableTesting || AudioManager.Instance == null)
            return;

        // Player Sounds
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            AudioManager.Instance.PlayPlayerJump();
            Debug.Log("🎵 Playing: Player Jump");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            AudioManager.Instance.PlayPlayerAttack();
            Debug.Log("🎵 Playing: Player Attack");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            AudioManager.Instance.PlayPlayerHurt();
            Debug.Log("🎵 Playing: Player Hurt");
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            AudioManager.Instance.PlayPlayerDeath();
            Debug.Log("🎵 Playing: Player Death");
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            AudioManager.Instance.PlayPlayerFootstep();
            Debug.Log("🎵 Playing: Player Footstep");
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            AudioManager.Instance.PlayPlayerLand();
            Debug.Log("🎵 Playing: Player Land");
        }

        // Enemy Sounds
        if (Input.GetKeyDown(KeyCode.Q))
        {
            AudioManager.Instance.PlayEnemyAttack();
            Debug.Log("🎵 Playing: Enemy Attack");
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            AudioManager.Instance.PlayEnemyHurt();
            Debug.Log("🎵 Playing: Enemy Hurt");
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            AudioManager.Instance.PlayEnemyDeath();
            Debug.Log("🎵 Playing: Enemy Death");
        }

        // Boss Sounds
        if (Input.GetKeyDown(KeyCode.A))
        {
            AudioManager.Instance.PlayBossAttack();
            Debug.Log("🎵 Playing: Boss Attack");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            AudioManager.Instance.PlayBossHurt();
            Debug.Log("🎵 Playing: Boss Hurt");
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            AudioManager.Instance.PlayBossDeath();
            Debug.Log("🎵 Playing: Boss Death");
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            AudioManager.Instance.PlayBossRoar();
            Debug.Log("🎵 Playing: Boss Roar");
        }

        // Music Controls
        if (Input.GetKeyDown(KeyCode.M))
        {
            AudioManager.Instance.PlayBackgroundMusic();
            Debug.Log("🎵 Playing: Background Music");
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            AudioManager.Instance.PlayBossMusic();
            Debug.Log("🎵 Playing: Boss Music");
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            AudioManager.Instance.StopMusic(true);
            Debug.Log("🎵 Stopping Music");
        }

        // Environment Sounds
        if (Input.GetKeyDown(KeyCode.C))
        {
            AudioManager.Instance.PlayCheckpoint();
            Debug.Log("🎵 Playing: Checkpoint");
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            AudioManager.Instance.PlayPortalIn();
            Debug.Log("🎵 Playing: Portal In");
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            AudioManager.Instance.PlayPortalOut();
            Debug.Log("🎵 Playing: Portal Out");
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            AudioManager.Instance.PlayTrapActivate();
            Debug.Log("🎵 Playing: Trap Activate");
        }

        // UI Sounds
        if (Input.GetKeyDown(KeyCode.U))
        {
            AudioManager.Instance.PlayButtonClick();
            Debug.Log("🎵 Playing: Button Click");
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            AudioManager.Instance.PlayMenuOpen();
            Debug.Log("🎵 Playing: Menu Open");
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            AudioManager.Instance.PlayMenuClose();
            Debug.Log("🎵 Playing: Menu Close");
        }

        // Volume Controls (Arrow Keys)
        if (Input.GetKey(KeyCode.UpArrow))
        {
            float newVol = Mathf.Min(1f, AudioManager.Instance.GetMusicVolume() + Time.deltaTime);
            AudioManager.Instance.SetMusicVolume(newVol);
            Debug.Log($"🔊 Music Volume: {Mathf.RoundToInt(newVol * 100)}%");
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            float newVol = Mathf.Max(0f, AudioManager.Instance.GetMusicVolume() - Time.deltaTime);
            AudioManager.Instance.SetMusicVolume(newVol);
            Debug.Log($"🔉 Music Volume: {Mathf.RoundToInt(newVol * 100)}%");
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            float newVol = Mathf.Min(1f, AudioManager.Instance.GetSFXVolume() + Time.deltaTime);
            AudioManager.Instance.SetSFXVolume(newVol);
            Debug.Log($"🔊 SFX Volume: {Mathf.RoundToInt(newVol * 100)}%");
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            float newVol = Mathf.Max(0f, AudioManager.Instance.GetSFXVolume() - Time.deltaTime);
            AudioManager.Instance.SetSFXVolume(newVol);
            Debug.Log($"🔉 SFX Volume: {Mathf.RoundToInt(newVol * 100)}%");
        }
    }

    private void OnGUI()
    {
        if (!enableTesting || AudioManager.Instance == null)
            return;

        // Display current volumes
        GUIStyle style = new GUIStyle(GUI.skin.label);
        style.fontSize = 14;
        style.normal.textColor = Color.white;
        style.alignment = TextAnchor.UpperLeft;

        string volumeInfo = $"Music: {Mathf.RoundToInt(AudioManager.Instance.GetMusicVolume() * 100)}% | " +
                           $"SFX: {Mathf.RoundToInt(AudioManager.Instance.GetSFXVolume() * 100)}%\n" +
                           $"Press H to show/hide help";

        GUI.Label(new Rect(10, 10, 400, 50), volumeInfo, style);

        // Show help on H key
        if (Input.GetKey(KeyCode.H))
        {
            GUIStyle helpStyle = new GUIStyle(GUI.skin.box);
            helpStyle.fontSize = 12;
            helpStyle.normal.textColor = Color.white;
            helpStyle.alignment = TextAnchor.UpperLeft;
            helpStyle.wordWrap = true;

            GUI.Box(new Rect(10, 70, 300, 500), instructions, helpStyle);
        }
    }
}
