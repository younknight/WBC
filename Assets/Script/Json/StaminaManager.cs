using System.Collections;
using System;
using UnityEngine;
using System.IO;
public class StaminaData
{
    public DateTime lastLogInTime;
    public int maxStamina = 10;
    public int currentStamina = 10;

    public void SetLastLogInTime(DateTime today)
    {
        lastLogInTime = today.Date;
    }
    public bool OverDay()
    {
        return (DateTime.Now - lastLogInTime).Days > 0;
    }
    public void ChargeStamina()
    {
        currentStamina = maxStamina;
    }
}
public class StaminaManager : MonoBehaviour
{
    #region ½Ì±ÛÅæ
    static StaminaManager instance;
    public static StaminaManager Instance { get => instance; set => instance = value; }
    public StaminaData StaminaData { get => staminaData; set => staminaData = value; }

    string path;
    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else Destroy(this.gameObject);
        path = Application.persistentDataPath + "/" + "Stamina.json";
        Load();
    }
    #endregion
    StaminaData staminaData = new StaminaData();
    JsonParser jsonParser = new JsonParser();

    [ContextMenu("Reset Stamina Json Data")]
    public void ResetProgress()
    {
        path =  Application.persistentDataPath + "/"+ "Stamina.json" ;
        jsonParser.SaveJson<StaminaData>(staminaData, path);
    }
    public void Load()
    {
        if (!File.Exists(path))
        {
            ResetProgress();

        }
            staminaData = jsonParser.LoadJson<StaminaData>(path);
    }
    public void Save()
    {
        staminaData.SetLastLogInTime(DateTime.Now);
        jsonParser.SaveJson<StaminaData>(staminaData, path);
    }
    public bool OverDay()
    {
        return staminaData.OverDay();
    }
    public void CheckDay()
    {
        staminaData.ChargeStamina();
    }
    public void AddStamina()
    {
        staminaData.currentStamina++;
        StaminaGauge.instance.Setup(staminaData.currentStamina);
        Save();
    }
    public void UseStamina()
    {
        staminaData.currentStamina--;
        Save();
    }
}

