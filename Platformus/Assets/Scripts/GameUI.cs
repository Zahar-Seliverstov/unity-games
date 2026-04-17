using UnityEngine;

public class GameUI : MonoBehaviour
{
    private static GUIStyle boxStyle;
    private static GUIStyle labelStyle;

    void OnGUI()
    {
        var gm = GameManager.Instance;
        if (gm == null) return;

        if (labelStyle == null)
        {
            labelStyle = new GUIStyle(GUI.skin.label);
            labelStyle.fontSize = 20;
            labelStyle.normal.textColor = Color.white;
            labelStyle.fontStyle = FontStyle.Bold;
        }

        string level = "Level: " + gm.currentLevel + " / " + GameManager.TOTAL_LEVELS;
        string lvlTime = "Level:  " + FormatTime(Mathf.Max(0f, gm.levelTimeRemaining));
        string totTime = "Total:  " + FormatTime(Mathf.Max(0f, gm.totalTimeRemaining));

        float w = 230f, h = 80f, x = 10f, y = 10f, pad = 4f;
        GUI.color = new Color(0f, 0f, 0f, 0.55f);
        GUI.DrawTexture(new Rect(x, y, w, h), Texture2D.whiteTexture);
        GUI.color = Color.white;

        GUI.Label(new Rect(x + pad, y + pad, w - pad * 2f, 25f), level, labelStyle);
        GUI.Label(new Rect(x + pad, y + pad + 27f, w - pad * 2f, 25f), lvlTime, labelStyle);
        GUI.Label(new Rect(x + pad, y + pad + 54f, w - pad * 2f, 25f), totTime, labelStyle);
    }

    static string FormatTime(float t)
    {
        int m = (int)(t / 60f);
        int s = (int)(t % 60f);
        return string.Format("{0:00}:{1:00}", m, s);
    }
}
