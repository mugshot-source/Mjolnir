using BepInEx;
using BepInEx.Logging;
using System.Collections.Generic;
using UnityEngine;
using ValheimLib;
using ValheimLib.ODB;

namespace Mjolnir
{
    [BepInPlugin("Mjolnir", "Mjolnir", "1.0.1")]
    [BepInProcess("valheim.exe")]
    public static class ItemData
    {
        public const string CraftingStationPrefabName = "forge";
        internal static void Init()
        {
            AddCustomItems();
        }

        public static void AddCustomItems()
        {
            //create clone of existing game asset
            var mock = Mock<ItemDrop>.Create("MaceSilver");
            var cloned = Prefab.GetRealPrefabFromMock<ItemDrop>(mock).gameObject.InstantiateClone($"{LanguageData.TokenValue}", true);
            cloned.name = LanguageData.TokenValue;

            //////setting data for itemdrop
            var newItemPrefab = cloned;
            var mace = new CustomItem(cloned, fixReference: true);
            var item = mace.ItemDrop;
            item.m_itemData.m_dropPrefab = newItemPrefab;
            item.m_itemData.m_shared.m_name = LanguageData.TokenName;
            item.m_itemData.m_shared.m_description = LanguageData.TokenDescriptionName;
            item.m_itemData.m_shared.m_setName = string.Empty;


            item.m_itemData.m_shared.m_weight = 30;
            item.m_itemData.m_shared.m_setStatusEffect = null;
            item.m_itemData.m_shared.m_attackStatusEffect = Prefab.Cache.GetPrefab<SE_Shocked>(LanguageData.EffectValue);
            item.m_itemData.m_shared.m_itemType = ItemDrop.ItemData.ItemType.OneHandedWeapon;
            item.m_itemData.m_shared.m_equipStatusEffect = Prefab.Cache.GetPrefab<SE_Electric>(LanguageData.EffectValue);

            ////////secondary
            item.m_itemData.m_shared.m_secondaryAttack.m_attackChainLevels = 0;
            item.m_itemData.m_shared.m_secondaryAttack.m_attackStamina = 20f;
            item.m_itemData.m_shared.m_secondaryAttack.m_damageMultiplier = 0f;
            item.m_itemData.m_shared.m_secondaryAttack.m_forceMultiplier = 100f;
            item.m_itemData.m_shared.m_secondaryAttack.m_staggerMultiplier = 6f;
            ///
            item.m_itemData.m_shared.m_equipDuration = 0.3f;
            item.m_itemData.m_shared.m_maxQuality = 1;
            item.m_itemData.m_shared.m_backstabBonus = 2f;
            item.m_itemData.m_shared.m_aiAttackInterval = 1f;
            item.m_itemData.m_shared.m_attackForce = 60f;
            item.m_itemData.m_shared.m_attack.m_attackStamina = 5f;
            item.m_itemData.m_shared.m_damages.m_frost = 0f;
            item.m_itemData.m_shared.m_damages.m_spirit = 0f;
            item.m_itemData.m_shared.m_damages.m_blunt = 20f;
            item.m_itemData.m_shared.m_skillType = Skills.SkillType.None;
            item.m_itemData.m_shared.m_damages.m_lightning = 300f;

            //item.m_itemData.m_shared.m_secondaryAttack.m_attackType = Attack.AttackType.Projectile;

            item.m_itemData.m_shared.m_damages.DamageRange(item.m_itemData.m_shared.m_damages.m_lightning, -300f, 300f);

            item.m_itemData.m_shared.m_secondaryAttack.m_attackAnimation = "swing_sledge";


            //material tweak

            var meshRenderer = newItemPrefab.transform.GetComponentInChildren<MeshRenderer>();
            var colorTarget = new Color(0f, 0.5f, 194f / 255f, 255f / 255f);
            meshRenderer.material.color = colorTarget;

            //Add our recipe to the object db so our item can be crafted
            var recipe = ScriptableObject.CreateInstance<Recipe>();
            recipe.name = "Recipe_Mjolnir";
            recipe.m_item = mace.ItemDrop;
            recipe.m_enabled = true;
            recipe.m_minStationLevel = 5;
            recipe.m_craftingStation = Mock<CraftingStation>.Create("forge");
            var neededResources = new List<Piece.Requirement>
            {
                MockRequirement.Create("Crystal", Mjolnir.CrystalRequired.Value),
                MockRequirement.Create("Iron", Mjolnir.IronRequired.Value),
                MockRequirement.Create("HardAntler", Mjolnir.HardAntler.Value),
                MockRequirement.Create("SerpentScale", Mjolnir.SerpentScale.Value)
            };
            recipe.m_resources = neededResources.ToArray();
            var maceRecipe = new CustomRecipe(recipe, true, true);

            ObjectDBHelper.Add(mace);
            ObjectDBHelper.Add(maceRecipe);
        }
    }
}
