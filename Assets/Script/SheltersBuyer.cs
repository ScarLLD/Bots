using UnityEngine;

public class SheltersBuyer : MonoBehaviour
{
    [SerializeField] private int _shelterPrice;
    [SerializeField] private Wallet _wallet;

    public bool TryConfrimBuyPossability()
    {
        if (_wallet.ResourceCount >= _shelterPrice)
        {
            _wallet.DecreaseResources(_shelterPrice);
            return true;
        }

        return false;
    }
}
