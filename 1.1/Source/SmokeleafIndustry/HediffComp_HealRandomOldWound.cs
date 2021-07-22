using System;
using System.Linq;
using RimWorld;
using Verse;

namespace SmokeleafIndustry
{
	// Token: 0x02000004 RID: 4
	public class HediffComp_HealRandomOldWound : HediffComp
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000007 RID: 7 RVA: 0x0000224C File Offset: 0x0000044C
		public HediffCompProperties_HealRandomOldWound Props
		{
			get
			{
				return (HediffCompProperties_HealRandomOldWound)this.props;
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002269 File Offset: 0x00000469
		public override void CompPostMake()
		{
			base.CompPostMake();
			this.ResetTicksToHeal();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000227A File Offset: 0x0000047A
		private void ResetTicksToHeal()
		{
			this.ticksToHeal = Rand.Range(3, 8) * 60000;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002290 File Offset: 0x00000490
		public override void CompPostTick(ref float severityAdjustment)
		{
			this.ticksToHeal--;
			bool flag = this.ticksToHeal <= 0;
			if (flag)
			{
				this.TryHealRandomOldWound();
				this.ResetTicksToHeal();
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000022CC File Offset: 0x000004CC
		private void TryHealRandomOldWound()
		{
			Hediff hediff;
			bool flag = !base.Pawn.health.hediffSet.hediffs.Where(new Func<Hediff, bool>(HediffUtility.IsPermanent)).TryRandomElement(out hediff);
			if (!flag)
			{
				hediff.Severity = 0f;
				bool flag2 = PawnUtility.ShouldSendNotificationAbout(base.Pawn);
				if (flag2)
				{
					Messages.Message(TranslatorFormattedStringExtensions.Translate("MessagePermanentWoundHealed", this.parent.LabelCap, base.Pawn.LabelShort, hediff.Label, base.Pawn.Named("PAWN")), base.Pawn, MessageTypeDefOf.PositiveEvent, true);
				}
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000238A File Offset: 0x0000058A
		public override void CompExposeData()
		{
			Scribe_Values.Look<int>(ref this.ticksToHeal, "ticksToHeal", 0, false);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000023A0 File Offset: 0x000005A0
		public override string CompDebugString()
		{
			return "ticksToHeal: " + this.ticksToHeal;
		}

		// Token: 0x04000006 RID: 6
		private int ticksToHeal;
	}
}
