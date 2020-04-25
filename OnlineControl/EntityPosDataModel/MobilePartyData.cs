using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;

namespace OnlineControl.EntityPosDataModel
{
	class MobilePartyData
	{
		public float x;
		public float y;
		public float xTarget;
		public float yTarget;
		public string stringId;
		public string factionId;
		public string stringIdTargetParty;
		public string stringIdTargetSettlement;
		public string leaderId;
		public string behavior;
		public int prisoners;
		public int troops;
		public float strength;
		public string settlementId;
		public string name;
		public string armyLeaderId;
		public bool isBandit;
		public uint color;
		public uint color2;
		public uint labelColor;
		public bool isEngaging;
		public bool isHolding;

		public MobilePartyData(MobileParty mobileParty)
		{
			if (!mobileParty.IsLeaderless && mobileParty.Leader != null)
			{
				leaderId = mobileParty.Leader.StringId;
			}
			name = mobileParty.Name.ToString();
			behavior = mobileParty.ShortTermBehavior.ToString();
			prisoners = mobileParty.PrisonRoster.Count;
			troops = mobileParty.MemberRoster.Count;
			strength = mobileParty.Party.TotalStrength;
			stringId = mobileParty.StringId;
			x = mobileParty.Position2D.x;
			y = mobileParty.Position2D.y;
			xTarget = mobileParty.TargetPosition.x;
			yTarget = mobileParty.TargetPosition.y;
			isBandit = mobileParty.IsBandit;
			isEngaging = mobileParty.IsEngaging;
			isHolding = mobileParty.IsHolding;

			if (mobileParty.CurrentSettlement != null)
			{
				settlementId = mobileParty.CurrentSettlement.StringId;
			}
			if (mobileParty.TargetParty != null)
			{
				MobileParty target = mobileParty.TargetParty;
				stringIdTargetParty = target.StringId;

			}
			if (mobileParty.TargetSettlement != null)
			{
				Settlement target = mobileParty.TargetSettlement;
				stringIdTargetSettlement = target.StringId;
			}

			factionId = mobileParty.MapFaction.StringId;

			if (mobileParty.Army != null)
			{
				armyLeaderId = mobileParty.Army.LeaderParty.StringId;
			}

		}
	}
}
