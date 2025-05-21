using UnityEngine;

namespace GriftTogether {

    public static class MapMessage {

        public const string START_TURN = "Start turn!";
        public const string WAIT_TURN = "Wait turn";
        public const string PROCESS_TURN = "Processing...";
        public const string END_TURN = "Ent turn";

        public const string SPAWN_DUST = "throw";

        public const string COMPLETE_ROUND = "finished the game-board round!";

        public const string BUY_BUTTON = "Buy";
        public const string RENT_BUTTON = "Rent";
        public const string GAP_PRICE = "Price for breaking the contract";

        public const string PURCHASED_BUILD = "purchased";
        public const string PAID_RENT = "paid for";

        public const string SUBSCRIBE_FOR = "purchased a subscription to";
        public const string UN_SUBSCRIBE_FOR = "unsubscribed from";
    }
}
