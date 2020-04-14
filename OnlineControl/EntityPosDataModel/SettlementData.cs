using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;

namespace OnlineControl.EntityPosDataModel
{
    class SettlementData
    {
		public float x;
		public float y;
		public int settlementType;
		public string stringId;
		public string factionId;
		public string name;

		public SettlementData(Settlement settlement)
		{
			this.x = settlement.GetPosition2D.x;
			this.y = settlement.GetPosition2D.y;
			this.stringId = settlement.StringId;
			this.factionId = settlement.MapFaction.StringId;
			name = settlement.Name.ToString();

			if (settlement.IsTown)
			{
				settlementType = 0;
			}
			if (settlement.IsCastle)
			{
				settlementType = 1;
			}
			if (settlement.IsVillage)
			{
				settlementType = 2;
			}
			if (settlement.IsHideout())
			{
				settlementType = 3;
			}

		}
	}
}
