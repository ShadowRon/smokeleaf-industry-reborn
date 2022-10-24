using System;
using RimWorld;
using UnityEngine;
using Verse;

namespace SmokeleafIndustry
{
	[StaticConstructorOnStartup]
	public class CompPowerPlantHempSolar : CompPowerPlant
	{
		public override float DesiredPowerOutput
		{
			get
			{
				return Mathf.Lerp(NightPower, FullSunPower, this.parent.Map.skyManager.CurSkyGlow) * this.RoofedPowerOutputFactor;
			}
		}

		private float RoofedPowerOutputFactor
		{
			get
			{
				int num = 0;
				int num2 = 0;
				foreach (IntVec3 c in this.parent.OccupiedRect())
				{
					num++;
					bool flag = this.parent.Map.roofGrid.Roofed(c);
					if (flag)
					{
						num2++;
					}
				}
				return (float)(num - num2) / (float)num;
			}
		}

		public override void PostDraw()
		{
			base.PostDraw();
			GenDraw.FillableBarRequest r = default;
			r.center = this.parent.DrawPos + Vector3.up * 0.1f;
			r.size = CompPowerPlantHempSolar.BarSize;
			r.fillPercent = base.PowerOutput / FullSunPower;
			r.filledMat = CompPowerPlantHempSolar.PowerPlantHempSolarBarFilledMat;
			r.unfilledMat = CompPowerPlantHempSolar.PowerPlantHempSolarBarUnfilledMat;
			r.margin = 0.15f;
			Rot4 rotation = this.parent.Rotation;
			rotation.Rotate(RotationDirection.Clockwise);
			r.rotation = rotation;
			GenDraw.DrawFillableBar(r);
		}

		private const float FullSunPower = 3000f;
		private const float NightPower = 0f;
		private static readonly Vector2 BarSize = new Vector2(4f, 0.14f);
		private static readonly Material PowerPlantHempSolarBarFilledMat = SolidColorMaterials.SimpleSolidColorMaterial(new Color(0.5f, 0.475f, 0.1f), false);
		private static readonly Material PowerPlantHempSolarBarUnfilledMat = SolidColorMaterials.SimpleSolidColorMaterial(new Color(0.15f, 0.15f, 0.15f), false);
	}
}
