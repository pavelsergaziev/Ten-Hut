using UnityEngine;

public class OptionsSavingModel
{
    public void IfFirstLaunchThenSetInitialValues(int languageInitialId, float fXInitialVolume, float sergeantInitialVolume)
    {
        int notFirstLaunch = PlayerPrefs.GetInt("NotFirstLaunch");

        if (notFirstLaunch == 1)
            return;

        PlayerPrefs.SetInt("NotFirstLaunch", 1);
        PlayerPrefs.SetInt("LanguageId", languageInitialId);
        PlayerPrefs.SetFloat("FXVolume", fXInitialVolume);
        PlayerPrefs.SetFloat("SergeantSpeechVolume", sergeantInitialVolume);
        PlayerPrefs.Save();
    }

    public int GetInitialLanguageId()
    {
        return PlayerPrefs.GetInt("LanguageId");        
    }

    public void SetInitialLanguage(int languageId)
    {
        PlayerPrefs.SetInt("LanguageId", languageId);
        PlayerPrefs.Save();
    }

    public float GetInitialFXVolume()
    {
        return PlayerPrefs.GetFloat("FXVolume");
    }

    public void SetInitialFXVolume(float volume)
    {
        PlayerPrefs.SetFloat("FXVolume", volume);
        PlayerPrefs.Save();
    }

    public float GetInitialSergeantSpeechVolume()
    {
        return PlayerPrefs.GetFloat("SergeantSpeechVolume");
    }

    public void SetInitialSergeantSpeechVolume(float volume)
    {
        PlayerPrefs.SetFloat("SergeantSpeechVolume", volume);
        PlayerPrefs.Save();
    }
}
