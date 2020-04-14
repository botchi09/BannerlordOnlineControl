using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;

namespace OnlineControl.EntityPosDataModel
{
    class EntityDataScraper
    {

        public Dictionary<string, SettlementData> GenerateSettlementData()
        {
            Dictionary<string, SettlementData> settlementList = new Dictionary<string, SettlementData>();

            Settlement.All.ToList().ForEach(s =>
            {
                SettlementData settlementData = new SettlementData(s);
                //settlementList[s.Id.ToString()] = settlementData;
                settlementList[s.StringId] = settlementData;
            });
            return settlementList;
        }

        public Dictionary<string, LordPartyData> GenerateLordData()
        {
            Dictionary<string, LordPartyData> lordPartyDataList = new Dictionary<string, LordPartyData>();
            Hero.All.Where(h => h.IsAlive).ToList().ForEach(h =>
            {
                LordPartyData lordPartyData = new LordPartyData(h);
                lordPartyDataList[h.StringId] = lordPartyData;
            });
            return lordPartyDataList;
        }

        public Dictionary<string, MobilePartyData> GenerateMobilePartyData()
        {
            Dictionary<string, MobilePartyData> mobilePartyDataList = new Dictionary<string, MobilePartyData>();
            MobileParty.All.ToList().ForEach(p =>
            {
                MobilePartyData mobilePartyData = new MobilePartyData(p);
                mobilePartyDataList[mobilePartyData.stringId] = mobilePartyData;
            });
            return mobilePartyDataList;
        }
    }
}
