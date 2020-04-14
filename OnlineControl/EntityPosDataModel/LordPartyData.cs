using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;

namespace OnlineControl.EntityPosDataModel
{
    //Stores data about lords i.e. what they're doing, status, etc
    class LordPartyData
    {
        public bool isFactionLeader = true;
        public string factionId;
        public string stringId;
        public string partyStringId;
        //public string currentSettlementId;
        public string name; //Some lords are auto-generated so this is necessary

        public LordPartyData(Hero lord)
        {
            isFactionLeader = lord.IsFactionLeader;
            factionId = lord.MapFaction.StringId;
            stringId = lord.StringId;
            name = lord.Name.ToString();

            if (lord.PartyBelongedTo != null)
            {
                partyStringId = lord.PartyBelongedTo.StringId;
            }
            if (lord.CurrentSettlement != null)
            {
                //currentSettlementId = lord.CurrentSettlement.StringId;
            }
            

        }
    }
}
