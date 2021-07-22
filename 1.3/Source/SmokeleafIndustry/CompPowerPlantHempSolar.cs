using System;
using RimWorld;
using UnityEngine;
using Verse;

namespace SmokeleafIndustry
{
	// Token: 0x02000002 RID: 2
	[StaticConstructorOnStartup]
	public class CompPowerPlantHempSolar : CompPowerPlant
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public override float DesiredPowerOutput
		{
			get
			{
				return Mathf.Lerp(0f, 3000f, this.parent.Map.skyManager.CurSkyGlow) * this.RoofedPowerOutputFactor;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002090 File Offset: 0x00000290
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

		// Token: 0x06000003 RID: 3 RVA: 0x0000211C File Offset: 0x0000031C
		public override void PostDraw()
		{
			base.PostDraw();
			GenDraw.FillableBarRequest r = default(GenDraw.FillableBarRequest);
			r.center = this.parent.DrawPos + Vector3.up * 0.1f;
			r.size = CompPowerPlantHempSolar.BarSize;
			r.fillPercent = base.PowerOutput / 3000f;
			r.filledMat = CompPowerPlantHempSolar.PowerPlantHempSolarBarFilledMat;
			r.unfilledMat = CompPowerPlantHempSolar.PowerPlantHempSolarBarUnfilledMat;
			r.margin = 0.15f;
			Rot4 rotation = this.parent.Rotation;
			rotation.Rotate(RotationDirection.Clockwise);
			r.rotation = rotation;
			GenDraw.DrawFillableBar(r);
		}

		// Token: 0x04000001 RID: 1
		private const float FullSunPower = 3000f;

		// Token: 0x04000002 RID: 2
		private const float NightPower = 0f;

		// Token: 0x04000003 RID: 3
		private static readonly Vector2 BarSize = new Vector2(4f, 0.14f);

		// Token: 0x04000004 RID: 4
		private static readonly Material PowerPlantHempSolarBarFilledMat = SolidColorMaterials.SimpleSolidColorMaterial(new Color(0.5f, 0.475f, 0.1f), false);

		// Token: 0x04000005 RID: 5
		private static readonly Material PowerPlantHempSolarBarUnfilledMat = SolidColorMaterials.SimpleSolidColorMaterial(new Color(0.15f, 0.15f, 0.15f), false);
	}
}
