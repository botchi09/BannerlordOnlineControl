using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;

namespace OnlineControl.EntityPosDataModel
{
	class KingdomData
	{
		public string stringId;
		public uint color;
		public uint color2;
		public uint labelColor;
		public uint primaryBannerColor;
		public uint secondaryBannerColor;
		public string name;

		public KingdomData(Kingdom kingdom)
		{
			stringId = kingdom.StringId;
			color = kingdom.Color;
			color2 = kingdom.Color2;
			labelColor = kingdom.LabelColor;
			primaryBannerColor = kingdom.PrimaryBannerColor;
			secondaryBannerColor = kingdom.SecondaryBannerColor;
			name = kingdom.Name.ToString();
		}
	}
}
