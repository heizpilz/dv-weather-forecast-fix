using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using DV.WeatherSystem;
using HarmonyLib;
using UnityEngine;
using UnityModManagerNet;

namespace WeatherForecastFix;

#if DEBUG
[EnableReloading]
#endif
public static class Main
{
	// Unity Mod Manage Wiki: https://wiki.nexusmods.com/index.php/Category:Unity_Mod_Manager
	private static bool Load(UnityModManager.ModEntry modEntry)
	{
		Harmony? harmony = null;

		try
		{
			harmony = new Harmony(modEntry.Info.Id);
			harmony.PatchAll(Assembly.GetExecutingAssembly());

			// Other plugin startup logic
			#if DEBUG
			modEntry.OnUnload = OnUnload;
			#endif
		}
		catch (Exception ex)
		{
			modEntry.Logger.LogException($"Failed to load {modEntry.Info.DisplayName}:", ex);
			harmony?.UnpatchAll(modEntry.Info.Id);
			return false;
		}

		return true;
	}

	#if DEBUG
	static bool OnUnload(UnityModManager.ModEntry modEntry)
	{
		var harmony = new Harmony(modEntry.Info.Id);
		harmony.UnpatchAll(modEntry.Info.Id);
		return true;
	}
	#endif

	[HarmonyPatch(typeof(WeatherForecaster), "OnHourChanged")]
	class DoForecastInHoursAfter6
	{
		public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
		{
			bool modifyNext = false;
			foreach (CodeInstruction instruction in instructions)
			{
				if (modifyNext)
				{
					modifyNext = false;
					if (instruction.opcode != OpCodes.Bne_Un_S)
					{
						Debug.Log("[WeatherForecastFix] Wrong OpCode found: " + instruction.opcode.ToString());
					}
					else
					{
						// change 'dateTime.Hour != this.doForecastAt' to 'dateTime.Hour < this.doForecastAt'
						CodeInstruction replaceWith = new CodeInstruction(OpCodes.Blt_Un, instruction.operand);
						yield return replaceWith;
						continue;
					}
				}
				if (instruction.LoadsField(AccessTools.Field(typeof(WeatherForecaster), nameof(WeatherForecaster.doForecastAt))))
				{
					modifyNext = true;
				}
				yield return instruction;
			}
		}
	}

	[HarmonyPatch(typeof(WeatherForecaster), "Start")]
	class SkipFirstForecast
	{
		public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
		{
			foreach (CodeInstruction instruction in instructions)
			{
				if (instruction.Calls(AccessTools.Method(typeof(WeatherForecaster), "OnHourChanged")))
				{
					// replace call to forecast when the time is still wrong with instruction that keeps the stack intact
					yield return new CodeInstruction(OpCodes.Pop);
					continue;
				}
				yield return instruction;
			}
		}
	}

	[HarmonyPatch(typeof(WeatherDriver), nameof(WeatherDriver.LoadSaveData))]
	class CallOnHourChangedOnSaveLoad
	{
		public static void Postfix(WeatherDriver __instance)
		{
			// trigger time dependent updates after the time is loaded from save
			__instance.manager.AdvanceTime(0);
		}
	}
}
