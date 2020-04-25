using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using static TaleWorlds.CampaignSystem.MobileParty;
using TaleWorlds.ObjectSystem;
using System.Diagnostics;

namespace OnlineControl
{
	class EntityController
	{
		MobileParty party;
		public EntityController()
		{

		}

		public void ProcessRequest(List<EntityActionRequest> requests)
		{
			requests.ForEach((request) =>
			{
				request.actions.ForEach((action) =>
				{
					dynamic target; //Can be Settlement or MobileParty
					Debug.Print("Applied " + action + " to " + request.target);
					switch (action) //TODO: More repeated code....
					{
						case "Engage":
							target = (MobileParty)FindEntity(request.target);
							request.subjects.ForEach((subjectId) =>
							{
								FindEntity(subjectId).SetMoveEngageParty(target);
							});
							break;
						case "Escort":
							target = (MobileParty)FindEntity(request.target);
							request.subjects.ForEach((subjectId) =>
							{
								FindEntity(subjectId).SetMoveEscortParty(target);
							});
							break;
						case "AiOn":
							request.subjects.ForEach((subjectId) =>
							{
								FindEntity(subjectId).Ai.SetDoNotMakeNewDecisions(false);
							});

							break;
						case "AiOff":
							request.subjects.ForEach((subjectId) =>
							{
								FindEntity(subjectId).Ai.SetDoNotMakeNewDecisions(true);
							});
							break;

					}
				});
			});
		}

		private dynamic FindEntity(string stringId)
		{
			MobileParty party = MBObjectManager.Instance.GetObject<MobileParty>(stringId);
			Settlement settlement = MBObjectManager.Instance.GetObject<Settlement>(stringId);

			if (party != null)
			{
				return party;
			}
			if (settlement != null)
			{
				return settlement;
			}
			return null;
		}

		private void FindLord()
		{
			//lord_1_28
			/*
			string lordId = "lord_1_28";
			lord = MBObjectManager.Instance.GetObject<Hero>(lordId);


			party = TaleWorlds.CampaignSystem.CharacterObject.Find("TaleWorlds.CampaignSystem.CharacterObject985").HeroObject.PartyBelongedTo; //lord.PartyBelongedTo;
			*/
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



		private void DisableDecisions()
		{

			Utility.ShowMsg("Disable AI");

			party.Ai.SetDoNotMakeNewDecisions(true);

			//MobileParty.MainParty.Army.AddPartyToMergedParties(MobileParty.MainParty);

			/*MobileParty.All.Where(p => p.IsBandit).ToList().ForEach(p => {
				p.Ai.SetDoNotMakeNewDecisions(true);
				p.SetMoveEscortParty(MobileParty.MainParty);
			});*/
		}

		private void EnableDecisions()
		{
			Utility.ShowMsg("Ensble AI");
			FindLord();
			party.Ai.SetDoNotMakeNewDecisions(false);
		}

		private void partyFuncTest()
		{
			party.SetMoveEngageParty(MainParty);
			party.AddElementToMemberRoster(CharacterObject.PlayerCharacter, 10);
		}

		private void EscortMe()
		{
			Utility.ShowMsg("escort me");
			FindLord();
			party.SetMoveEscortParty(MobileParty.MainParty);
		}



	}
}
