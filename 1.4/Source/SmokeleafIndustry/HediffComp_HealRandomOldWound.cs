using System;
using System.Linq;
using RimWorld;
using Verse;

namespace SmokeleafIndustry
{
	public class HediffComp_HealRandomOldWound : HediffComp
	{
		public HediffCompProperties_HealRandomOldWound Props
		{
			get
			{
				return (HediffCompProperties_HealRandomOldWound)this.props;
			}
		}

		public override void CompPostMake()
		{
			base.CompPostMake();
			this.ResetTicksToHeal();
		}

		private void ResetTicksToHeal()
		{
			this.ticksToHeal = Rand.Range(3, 8) * 60000;
		}

		public override void CompPostTick(ref float severityAdjustment)
		{
			this.ticksToHeal--;
			if (this.ticksToHeal <= 0)
			{
				this.TryHealRandomOldWound();
				this.ResetTicksToHeal();
			}
		}

		private void TryHealRandomOldWound()
		{
            if (base.Pawn.health.hediffSet.hediffs.Where(new Func<Hediff, bool>(HediffUtility.IsPermanent)).TryRandomElement(out Hediff hediff))
			{
				hediff.Severity = 0f;
				if (PawnUtility.ShouldSendNotificationAbout(base.Pawn))
				{
					Messages.Message(TranslatorFormattedStringExtensions.Translate("MessagePermanentWoundHealed", this.parent.LabelCap, base.Pawn.LabelShort, hediff.Label, base.Pawn.Named("PAWN")), base.Pawn, MessageTypeDefOf.PositiveEvent, true);
				}
			}
		}

		public override void CompExposeData()
		{
			Scribe_Values.Look<int>(ref this.ticksToHeal, "ticksToHeal", 0, false);
		}

		public override string CompDebugString()
		{
			return "ticksToHeal: " + this.ticksToHeal;
		}

		private int ticksToHeal;
	}
}
