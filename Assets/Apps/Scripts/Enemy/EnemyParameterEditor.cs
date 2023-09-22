using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public sealed class EnemyParameterEditor : ScriptableObject
{
    [SerializeField] private List<EnemyParameter.EnemyInfo> _enemyInfos;
    private Dictionary<int, EnemyParameter.EnemyInfo> _enemyInfoDic = new();
    private bool _isInitialized;

    private const string SheetID = "10goHwHFWuH8dmLh9-Yk4KcQifMriHLtRQqpj_rZ4Qps";
    private const string EnemySheetName = "パラメータ";

    public void Initialize()
    {
#if UNITY_EDITOR
        LoadSpreadSheet(EnemySheetName);
#else
        SetEnemyInfoDic();
#endif
    }

    private void LoadSpreadSheet(string _sheetName)
    {
        UnityWebRequest request = UnityWebRequest.Get("https://docs.google.com/spreadsheets/d/" + SheetID + "/gviz/tq?tqx=out:csv&sheet=" + _sheetName);
        request.SendWebRequest().completed += (operation) =>
        {
            if (request.result is UnityWebRequest.Result.ConnectionError or UnityWebRequest.Result.ProtocolError
                or UnityWebRequest.Result.DataProcessingError)
            {
                UnityEngine.Debug.Log(request.error);
            }
            else
            {
                SetParameter(request.downloadHandler.text);
            }
        };
    }

    private void SetParameter(string text)
    {
        _enemyInfos.Clear();
        StringReader reader = new StringReader(text);
        var loopCount = 0;

        while (reader.Peek() != -1)
        {
            var line = reader.ReadLine();
            //表の1～5行目までは無視
            if (loopCount < 5)
            {
                loopCount++;
                continue;
            }

            var info = new EnemyParameter.EnemyInfo();

            string[] elements = line.Split(',');    // 行のセルは,で区切られる
            for (int i = 0; i < elements.Length; i++)
            {
                elements[i] = elements[i].TrimStart('"').TrimEnd('"');
            }

            // 表が埋まっているとこまで
            if (elements[1] == "")
            {
                break;
            }

            info.ID = int.Parse(elements[1]);
            info.Name = elements[2];
            info.Hp = int.Parse(elements[3]);
            info.ResourceID = int.Parse(elements[4]);
            info.Projectile.ID = int.Parse(elements[5]);
            info.Projectile.Interval = float.Parse(elements[6]);
            info.Projectile.Speed = float.Parse(elements[7]);
            info.Projectile.Amount = int.Parse(elements[8]);
            info.EventID = int.Parse(elements[9]);
            _enemyInfos.Add(info);
        }

#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(this);
        UnityEditor.AssetDatabase.SaveAssets();
#endif

        SetEnemyInfoDic();
    }

    private void SetEnemyInfoDic()
    {
        _enemyInfoDic.Clear();

        for (var i = 0; i < _enemyInfos.Count; i++)
        {
            _enemyInfoDic.Add(_enemyInfos[i].ID, _enemyInfos[i]);
        }

        // 初期化完了
        _isInitialized = true;
    }

    public Dictionary<int, EnemyParameter.EnemyInfo> GetEnemyInfoDic()
    {
        return _enemyInfoDic;
    }

    public bool GetIsInitialized()
    {
        return _isInitialized;
    }

    public void ResetIsInitialized()
    {
        _isInitialized = false;
    }
}
