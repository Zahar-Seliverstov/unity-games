using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public float totalTimeRemaining = 600f;
    public float levelTimeRemaining = 120f;
    public int currentLevel = 1;
    public const int TOTAL_LEVELS = 5;

    private GUIStyle hudStyle;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        totalTimeRemaining -= Time.deltaTime;
        levelTimeRemaining -= Time.deltaTime;

        if (totalTimeRemaining <= 0f || levelTimeRemaining <= 0f)
            GoToLevel1();
    }

    void OnGUI()
    {
        if (hudStyle == null)
        {
            hudStyle = new GUIStyle(GUI.skin.label);
            hudStyle.fontSize = 20;
            hudStyle.normal.textColor = Color.white;
            hudStyle.fontStyle = FontStyle.Bold;
        }

        float x = 10f, y = 10f, w = 240f, h = 82f, pad = 5f;
        GUI.color = new Color(0f, 0f, 0f, 0.55f);
        GUI.DrawTexture(new Rect(x, y, w, h), Texture2D.whiteTexture);
        GUI.color = Color.white;

        GUI.Label(new Rect(x + pad, y + pad, w, 26f), "Level: " + currentLevel + " / " + TOTAL_LEVELS, hudStyle);
        GUI.Label(new Rect(x + pad, y + pad + 27f, w, 26f), "Level:  " + Fmt(Mathf.Max(0f, levelTimeRemaining)), hudStyle);
        GUI.Label(new Rect(x + pad, y + pad + 54f, w, 26f), "Total:  " + Fmt(Mathf.Max(0f, totalTimeRemaining)), hudStyle);
    }

    public void NextLevel()
    {
        if (currentLevel >= TOTAL_LEVELS)
        {
            GoToLevel1();
            return;
        }
        currentLevel++;
        levelTimeRemaining = 120f;
        SceneManager.LoadScene("Level" + currentLevel);
    }

    public void GoToLevel1()
    {
        currentLevel = 1;
        totalTimeRemaining = 600f;
        levelTimeRemaining = 120f;
        SceneManager.LoadScene("Level1");
    }

    static string Fmt(float t)
    {
        int m = (int)(t / 60f);
        int s = (int)(t % 60f);
        return string.Format("{0:00}:{1:00}", m, s);
    }
}
