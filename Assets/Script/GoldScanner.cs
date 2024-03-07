using UnityEngine;

public class GoldScanner : MonoBehaviour
{
    [SerializeField] private GoldPool _goldPool;

    public void Scan()
    {
        int goldCount = _goldPool.GetGoldCount;

        if (goldCount > 0)
            Debug.Log($"Найдено золото: {goldCount}.");
        else
            Debug.Log($"Золота нет.");
    }
}
