﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using BattleTech;
using HBS;
using BattleTech.UI;

namespace CustomShops.Shops
{
    public class BlackMarketShop : TaggedShop, IDiscountFromFaction, IFillWidgetFromFaction, ISaveShop, ITextIcon
    {
        public override string Name => "BlackMarket";
        public override string TabText => "Black Market";
        public override string HeaderText => "Black Market";
        public override string ShopPanelImage => SG_Stores_StoreImagePanel.BLACK_MARKET_ILLUSTRATION;
        public string SpriteID => SG_Shop_Screen.BLACKMARKET_ICON;
        public override Color IconColor => Color.magenta;
        public override Color ShopColor => LazySingletonBehavior<UIManager>.Instance.UILookAndColorConstants.BlackMarketStoreColor.color;
        public FactionValue RelatedFaction => FactionEnumeration.GetAuriganPiratesFactionValue();

        public override bool Exists =>  Control.State.CurrentSystem == null ? false : Control.State.CurrentSystem.Def.BlackMarketShopItems != null;
        public override bool CanUse => true; // Control.State.CurrentSystem == null ? false : Control.State.Sim.CompanyTags.Contains(Control.State.Sim.Constants.Story.BlackMarketTag);
        public override bool RefreshOnSystemChange => true;
        public override bool RefreshOnMonthChange => false;
        public override bool RefreshOnOwnerChange => false;


        public override void Initilize()
        {
            Tags = Control.State.CurrentSystem.Def.BlackMarketShopItems;
            base.Initilize();
        }

        public override void SetLoadedShop(Shop shop)
        {
            Tags = Control.State.CurrentSystem.Def.BlackMarketShopItems;
            base.SetLoadedShop(shop);
        }

    }
}
