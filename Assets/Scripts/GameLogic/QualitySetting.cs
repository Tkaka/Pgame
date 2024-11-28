using UnityEngine;

public enum QualityLevel
{
    Low = 1,
    Medium = 2,
    High = 3,
}

public class QualitySetting : SingletonTemplate<QualitySetting>
{
    public void Init()
    {
        int level = PlayerPrefs.GetInt("QualitySetting_PGame", (int)QualityLevel.Medium);
        applySetting((QualityLevel)level);
    }

    public void ChangeLevel(QualityLevel level)
    {
        PlayerPrefs.SetInt("QualitySetting_PGame", (int)level);
        PlayerPrefs.Save();

        applySetting(level);
    }

    private void applySetting(QualityLevel level)
    {
        QualitySettings.SetQualityLevel((int)level, true);
        int scrHeight = -1;
        int targetFrame = 30;
        switch (level)
        {
            case QualityLevel.Low: //低
                targetFrame = 30;
                if (Screen.height > 720)
                    scrHeight = 720;
                break;

            case QualityLevel.High: //高
                targetFrame = 60;
                if (Screen.height > 1125)
                    scrHeight = 1125;
                break;

            case QualityLevel.Medium: //中
            default:
                if (Screen.height > 900)
                    scrHeight = 900;
                targetFrame = 45;
                break;
        }

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFrame;
        if(scrHeight > 0)
        {
            float scale = Screen.height / (float)scrHeight;
            int scrWidth = (int)(Screen.width / scale);
            Screen.SetResolution(scrWidth, scrHeight, true, targetFrame);
        }
    }
}