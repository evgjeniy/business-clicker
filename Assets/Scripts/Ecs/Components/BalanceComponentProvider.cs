using Voody.UniLeo;

namespace Ecs.Components
{
    public class BalanceComponentProvider : MonoProvider<BalanceComponent> {}

    public struct BalanceComponent
    {
        public float MoneyAmount;
    }
}