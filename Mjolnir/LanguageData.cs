namespace Mjolnir
{
    public class LanguageData
    {
        public const string TokenName = "$custom_item_mjolnir";
        public const string TokenValue = "Mjolnir";

        public const string TokenDescriptionName = "$custom_item_mjolnir_description";
        public const string TokenDescriptionValue = "A magical hammer fit for the gods.";

        public const string EffectName = "$se_shocked_name";
        public const string EffectValue = "Mjölnir";

        public const string EffectName2 = "$se_electric_name";
        public const string EffectValue2 = "Mjölnir";

        public const string MjolnirTooltipName = "$se_shocked_tooltip";
        public const string MjolnirTooltipValue = "You feel lightning in your veins.";

        public static void Init()
        {
            ValheimLib.Language.AddToken(TokenName, TokenValue, true);
            ValheimLib.Language.AddToken(TokenDescriptionName, TokenDescriptionValue, true);
            ValheimLib.Language.AddToken(EffectName, EffectValue, true);
            ValheimLib.Language.AddToken(EffectName2, EffectValue2, true);
            ValheimLib.Language.AddToken(MjolnirTooltipName, MjolnirTooltipValue, true);
        }
    }
}
