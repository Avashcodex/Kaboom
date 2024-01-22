using UnityEngine;

[CreateAssetMenu(menuName ="Manager/GameSettings")]
public class GameSettings : ScriptableObject
{
    [SerializeField] private string gameVersion = "0.0.0";
    [SerializeField] private string nickName = "Salvo";

    public string GameVersion { get { return gameVersion; } }
    public string NickName { 
        get {

            int value = Random.Range(0, 9999);
            return nickName + value.ToString();        
        }     
    }

}
