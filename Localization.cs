
using ModShardLauncher;
using ModShardLauncher.Mods;

namespace Proselyte;
public class Localization
{
    public static void TextTreesPatching()
    {
        Msl.InjectTableTextTreesLocalization(
            new LocalizationTextTree(
                id: "Vampirism",
                tier: new Dictionary<ModLanguage, string>{
                    {ModLanguage.English, "Vampirism"},
                    {ModLanguage.Chinese, "吸血秘仪"}
                },
                hover: new Dictionary<ModLanguage, string>{
                    {ModLanguage.English, "Gained the ability to leech blood through an occult ritual, but also became extremely tyrannical and bloodthirsty.##~y~Main focus:~/~#~w~High Damage~/~, ~w~Bleeding~/~, ~w~Survivability~/~"},
                    {ModLanguage.Chinese, "通过秘仪获得了吸血能力，但也因此变得异常暴虐嗜血。##~y~能力要义：~/~#~w~高伤害~/~、~w~造成出血~/~、~w~生存能力~/~"}
                }
            ),
            new LocalizationTextTree(
                id: "Hemomancy",
                tier: new Dictionary<ModLanguage, string>{
                    {ModLanguage.English, "Sanguimancy"},
                    {ModLanguage.Chinese, "血咒"}
                },
                hover: new Dictionary<ModLanguage, string>{
                    {ModLanguage.English, "Master the mysteries of blood and use it as a medium to cast powerful spells.##~y~Main focus:~/~#~w~Support~/~, ~w~Minion Management~/~, ~w~Survivability~/~, ~w~Weakening Effects~/~"},
                    {ModLanguage.Chinese, "掌握血液的奥秘，并以此为媒介施展强大的法术。##~y~能力要义：~/~#~w~支援~/~、~w~仆从管理~/~、~w~生存能力~/~、~w~造成减益~/~"}
                }
            )
        );
    }

    public static void SkillTextsPatching()
    {
        Msl.InjectTableSkillsLocalization(
            new LocalizationSkill(
                id: "MDPR_Wild_Swipe",
                name: new Dictionary<ModLanguage, string>{
                    {ModLanguage.English, "Wild Swipe"},
                    {ModLanguage.Chinese, "重扫"}
                },
                description: new Dictionary<ModLanguage, string>{
                    {ModLanguage.English, @"Delivers a strike to three adjacent tiles with ~lg~+100%~/~ Knockback Chance."},
                    {ModLanguage.Chinese, @"向三个邻近方格击打一次，本次击打击退几率~lg~+100%~/~。"}
                }
            )
        );
    }
}