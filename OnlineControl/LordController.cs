using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using static TaleWorlds.CampaignSystem.MobileParty;

namespace OnlineControl
{
	class LordController
	{
		Hero lord;
		MobileParty party;
		public LordController()
		{
			
		}

		private void FindLord()
		{
			//lord_1_28
			string lordId = "lord_1_28";
			lord = MBObjectManager.Instance.GetObject<Hero>(lordId);


			party = TaleWorlds.CampaignSystem.CharacterObject.Find("TaleWorlds.CampaignSystem.CharacterObject985").HeroObject.PartyBelongedTo; //lord.PartyBelongedTo;

		}
	
		/*
		 * 
		 * 
				
				EnterSettlementAction.ApplyForParty(val, _hideout);
				float totalStrength = val.get_Party().get_TotalStrength();
				int num = (int)(1f * MBRandom.get_RandomFloat() * 20f * totalStrength + 50f);
				val.InitializePartyTrade(num);
				val.SetMoveGoToSettlement(_hideout);
				EnterSettlementAction.ApplyForParty(val, _hideout);
				*/

		

		internal void DisableDecisions()
		{
			
			Utility.ShowMsg("Disable AI");

			party.Ai.SetDoNotMakeNewDecisions(true);

			//MobileParty.MainParty.Army.AddPartyToMergedParties(MobileParty.MainParty);

			/*MobileParty.All.Where(p => p.IsBandit).ToList().ForEach(p => {
				p.Ai.SetDoNotMakeNewDecisions(true);
				p.SetMoveEscortParty(MobileParty.MainParty);
			});*/
		}

		internal void EnableDecisions()
		{
			Utility.ShowMsg("Ensble AI");
			FindLord();
			party.Ai.SetDoNotMakeNewDecisions(false);
		}

		internal void partyFuncTest()
		{
			party.SetMoveEngageParty(MainParty);
			party.AddElementToMemberRoster(CharacterObject.PlayerCharacter, 10);
		}

		internal void EscortMe()
		{
			Utility.ShowMsg("escort me");
			FindLord();
			party.SetMoveEscortParty(MobileParty.MainParty);
		}


	
	}
}
