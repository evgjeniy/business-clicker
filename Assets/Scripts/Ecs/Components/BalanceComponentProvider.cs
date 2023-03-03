using Voody.UniLeo;

namespace Ecs.Components
{
    public class BalanceComponentProvider : MonoProvider<BalanceComponent>
    {
        private void Awake()
        {
            if (!TryLoadSavedBalance(out var loadedMoney)) return;
            value.MoneyAmount = loadedMoney;
        }

        private bool TryLoadSavedBalance(out float balance)
        {
            balance = 0.0f;
            // TODO - Try load balance from saves
            return balance != 0.0f;
        }
    }

    public struct BalanceComponent
    {
        public float MoneyAmount;
    }
}