using Avalonia.Automation.Peers;

namespace ColorTextBlock.Avalonia
{
    /// <summary>
    ///     The automation peer for CTextBlock.
    /// </summary>
    public class CTextBlockAutomationPeer : ControlAutomationPeer
    {
        public CTextBlockAutomationPeer(CTextBlock owner) : base(owner)
        {
        }

        public new CTextBlock Owner
            => (CTextBlock)base.Owner;

        protected override AutomationControlType GetAutomationControlTypeCore()
        {
            return AutomationControlType.Text;
        }

        protected override string? GetNameCore()
        {
            return Owner.Text;
        }

        protected override bool IsControlElementCore()
        {
            return Owner.TemplatedParent is null && base.IsControlElementCore();
        }
    }
}