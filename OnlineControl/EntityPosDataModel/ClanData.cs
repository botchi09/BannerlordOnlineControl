using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;

namespace OnlineControl.EntityPosDataModel
{
	class ClanData
	{
		public string stringId;
		public uint color;
		public uint color2;
		public uint labelColor;
		public uint alternativeColor;
		public uint alternativeColor2;
		public string factionId;
		public string name;
		public bool isKingdomFaction;
		public bool isMapFaction;
		public bool isMinorFaction;
		public bool hasKingdom;

		public ClanData(Clan clan)
		{
			stringId = clan.StringId;
			color = clan.Color;
			color2 = clan.Color2;
			labelColor = clan.LabelColor;
			alternativeColor = clan.AlternativeColor;
			alternativeColor2 = clan.AlternativeColor2;
			factionId = clan.MapFaction.StringId;
			name = clan.Name.ToString();
			isKingdomFaction = clan.IsKingdomFaction; //This is ALWAYS false. Does literally nothing.
			isMapFaction = clan.IsMapFaction;
			isMinorFaction = clan.IsMinorFaction;
			hasKingdom = (clan.Kingdom != null);

		}
	}
}
