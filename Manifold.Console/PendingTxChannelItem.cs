using Nethereum.RPC.Eth.DTOs;

namespace Manifold.Console
{
    public class PendingTxChannelItem
    {


        public PendingTxChannelItem(Transaction tx)
        {
            PendingTransaction = tx;
            FirstSeen = DateTime.UtcNow;
        }

        public Transaction PendingTransaction { get; set; }
        public DateTime FirstSeen { get; set; }
    }
}