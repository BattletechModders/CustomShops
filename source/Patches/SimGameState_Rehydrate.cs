﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Harmony;
using BattleTech;
using BattleTech.Save.Test;
using BattleTech.Save;

namespace CustomShops.Patches
{
    [HarmonyPatch(typeof(SimGameState))]
    [HarmonyPatch("Rehydrate")]
    public class SimGameState_Rehydrate
    {
        [HarmonyPostfix]
        public static void LoadShops(GameInstanceSave gameInstanceSave, SimGameState __instance)
        {
            Control.State.CurrentSystem = __instance.CurSystem;
            Control.State.Sim = __instance;

            SerializableReferenceContainer globalReferences = gameInstanceSave.GlobalReferences;
            Control.LogDebug("Loading Shops");
            foreach (var shop in Control.Shops)
            {
                if (shop is ISaveShop save)
                {
                    var name = "Shop" + shop.Name;
                    Control.LogDebug("- " + shop.Name);
                    try
                    {
                        var Shop = globalReferences.GetItem<Shop>(name);
                        save.SetLoadedShop(Shop);

                        Control.LogDebug("-- " + shop.Name + " Loaded");
                        Control.LogDebug($"-- total {Shop.ActiveInventory.Count} items");
                    }
                    catch (Exception e)
                    {
                        Control.LogError($"Error finding {name} Create new");
                        shop.RefreshShop();
                        Control.LogDebug("-- total " + save.GetShopToSave().ActiveInventory.Count + " items");
                    }
                }
                else
                    shop.RefreshShop();
            }
        }
    }
}
