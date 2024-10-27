
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
                id: "MDPR_Baleful_Scream",
                name: new Dictionary<ModLanguage, string>{
                    {ModLanguage.English, "Baleful Scream"},
                    {ModLanguage.Chinese, "哀号"}
                },
                description: new Dictionary<ModLanguage, string>{
                    {ModLanguage.English, @"Lets out a shriek, dealing ~p~/*Psionic_Damage*/ Psionic Damage~/~. With ~w~75%~/~ chance ~w~Dazes~/~ the target or causes it to ~w~Stagger~/~ for ~w~2~/~ turns."},
                    {ModLanguage.Chinese, @"发出一声尖啸，对目标造成~p~/*Psionic_Damage*/点灵术伤害~/~。有~w~75%~/~的几率令目标~w~眩晕~/~或~w~失衡~/~~w~2~/~回合。"}
                }
            ),
            new LocalizationSkill(
                id: "MDPR_Wild_Swipe",
                name: new Dictionary<ModLanguage, string>{
                    {ModLanguage.English, "Wild Swipe"},
                    {ModLanguage.Chinese, "重扫"}
                },
                description: new Dictionary<ModLanguage, string>{
                    {ModLanguage.English, @"Deals ~w~/*Damage*/ Rending Damage~/~ to three adjacent tiles with ~w~/*Knockback_Chance*/%~/~ Knockback Chance (the damage is affected by the equipped ~w~gloves'~/~ Protection).##If both weapon slots are empty, then Damage ~w~is doubled~/~ and Knockback Chance is increased by an additional ~w~+50%~/~."},
                    {ModLanguage.Chinese, @"向三个邻近方格击打一次，造成~w~/*Damage*/点撕裂伤害~/~，本次击打击退几率为~w~/*Knockback_Chance*/%~/~（伤害数值与所穿~w~手部护具~/~的防护有关）。##若两个兵器槽位都空置，那么伤害~w~翻倍~/~，且击退几率额外~w~+50%~/~。"}
                }
            ),
            new LocalizationSkill(
                id: "MDPR_Rend_Flesh",
                name: new Dictionary<ModLanguage, string>{
                    {ModLanguage.English, "Rend Flesh"},
                    {ModLanguage.Chinese, "皮开肉绽"}
                },
                description: new Dictionary<ModLanguage, string>{
                    {ModLanguage.English, @"Deals ~w~/*Damage*/ Rending Damage~/~ with ~w~100%~/~ Bodypart Damage and ~w~/*Armor_Piercing*/%~/~ Armor Penetration (the damage is affected by the equipped ~w~gloves'~/~ Protection).##If both weapon slots are empty, then Damage ~w~is doubled~/~ and Armor Penetration is increased by ~w~+50%~/~."},
                    {ModLanguage.Chinese, @"造成~w~/*Damage*/点撕裂伤害~/~，本次击打肢体伤害为~w~100%~/~、护甲穿透为~w~/*Armor_Piercing*/%~/~（伤害数值与所穿~w~手部护具~/~的防护有关）。##若两个兵器槽位都空置，那么伤害~w~翻倍~/~，且护甲穿透额外~w~+50%~/~。"}
                }
            )
        );
    }
}