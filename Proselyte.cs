// Copyright (C)
// See LICENSE file for extended copyright information.
// This file is part of the repository from .

using System.IO;

using ModShardLauncher;
using ModShardLauncher.Mods;
using UndertaleModLib.Models;

namespace Proselyte;
public class Proselyte : Mod
{
    public override string Author => "Altair";
    public override string Name => "Proselyte";
    public override string Description => "mod_description";
    public override string Version => "1.0.0";
    public override string TargetVersion => "0.8.2.10";

    public override void PatchMod()
    {
        Msl.GetSprite("s_vampirism_branch").OriginX = 0;
        Msl.GetSprite("s_vampirism_branch").OriginY = 0;

        // Skill - Wild Swipe

        UndertaleGameObject o_skill_mdpr_wild_swipe = Msl.AddObject(
            name: "o_skill_mdpr_wild_swipe", 
            spriteName: "s_skills_swipe02", 
            parentName: "o_skill", 
            isVisible: true, 
            isPersistent: false, 
            isAwake: true,
            collisionShapeFlags: CollisionShapeFlags.Circle
        );

        UndertaleGameObject o_skill_mdpr_wild_swipe_ico = Msl.AddObject(
            name: "o_skill_mdpr_wild_swipe_ico", 
            spriteName: "s_skills_swipe02", 
            parentName: "o_skill_ico", 
            isVisible: true, 
            isPersistent: false, 
            isAwake: true,
            collisionShapeFlags: CollisionShapeFlags.Circle
        );

        o_skill_mdpr_wild_swipe.ApplyEvent(ModFiles, 
            new MslEvent("gml_Object_o_skill_mdpr_wild_swipe_Create_0.gml", EventType.Create, 0)
        );

        o_skill_mdpr_wild_swipe_ico.ApplyEvent(ModFiles, 
            new MslEvent("gml_Object_o_skill_mdpr_wild_swipe_ico_Create_0.gml", EventType.Create, 0)
        );

        // Write Skills Stat

        SkillsStatPatching();

        // Add Skill Branch

        UndertaleGameObject o_skill_category_vampirism = Msl.AddObject(
            name: "o_skill_category_vampirism", 
            spriteName: "", 
            parentName: "o_skill_category_weapon", 
            isVisible: true, 
            isPersistent: false, 
            isAwake: true,
            collisionShapeFlags: CollisionShapeFlags.Circle
        );

        o_skill_category_vampirism.ApplyEvent(ModFiles,
            new MslEvent("gml_Object_o_skill_category_vampirism_Create_0.gml", EventType.Create, 0),
            new MslEvent("gml_Object_o_skill_category_vampirism_Other_24.gml", EventType.Other, 24)
        );

        Msl.LoadGML("gml_Object_o_skillmenu_Create_0")
            .MatchFrom("var _metaCategoriesArray = ")
            .InsertBelow(@"array_push(_metaCategoriesArray[1], o_skill_category_vampirism)")
            .Save();

        // Localization

        Localization.TextTreesPatching();
        Localization.SkillTextsPatching();
        ExportTable("gml_GlobalScript_table_skills_stat");
    }

    private static void ExportTable(string table)
    {
        DirectoryInfo dir = new("ModSources/Proselyte/tmp");
        if (!dir.Exists) dir.Create();
        List<string>? lines = ModLoader.GetTable(table);
        if (lines != null)
        {
            File.WriteAllLines(
                Path.Join(dir.FullName, Path.DirectorySeparatorChar.ToString(), table + ".tsv"),
                lines.Select(x => string.Join('\t', x.Split(';')))
            );
        }
    }

    public static void SkillsStatPatching()
    {
        Msl.InjectTableSkillsStat(
            metaGroup: Msl.SkillsStatMetaGroup.PROSELYTES,
            id: "MDPR_Wild_Swipe",
            Object: "o_swipe_birth",
            Target: Msl.SkillsStatTarget.TargetArea,
            Range: "1",
            KD: 8,
            MP: 12,
            AOE_Lenght: 1,
            AOE_Width: 3,
            Attack: true
        );
    }
}
