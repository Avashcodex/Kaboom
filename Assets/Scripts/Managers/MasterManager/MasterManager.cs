using UnityEngine;

[CreateAssetMenu(menuName = "Singletons/MasterManager", order = 0)]
public class MasterManager : SingletonScriptableObject<MasterManager>
{
    [SerializeField] private GameSettings gameSettings;

    public static GameSettings GameSettings { get { return Instance.gameSettings; } }
}
