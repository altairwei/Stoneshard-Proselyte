﻿// Copyright (C)
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
        // Handle Sprites

        Msl.GetSprite("s_vampirism_branch").OriginX = 0;
        Msl.GetSprite("s_vampirism_branch").OriginY = 0;

        AdjustSkillIcon("s_skills_mdpr_swipe");
        AdjustSkillIcon("s_skills_mdpr_rend_flesh");

        // Add Functions

        Msl.AddFunction(ModFiles.GetCode("scr_mod_num_of_empty_hands.gml"), "scr_mod_num_of_empty_hands");

        // Add Skills

        PatchSkill_Wild_Swipe();
        PatchSkill_Rend_Flesh();

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

        // Delete me!
        ExportTable("gml_GlobalScript_table_skills_stat");
        ExportTable("gml_GlobalScript_table_skills");
        ExportTable("gml_GlobalScript_table_all_attribute");
    }

    private void PatchSkill_Wild_Swipe()
    {
        // Skill - Wild Swipe

        UndertaleGameObject o_mdpr_wild_swipe = Msl.AddObject(
            name: "o_mdpr_wild_swipe",
            spriteName: "s_Cleave",
            parentName: "o_aoe_spell",
            isVisible: false,
            isPersistent: false,
            isAwake: true,
            collisionShapeFlags: CollisionShapeFlags.Circle
        );

        o_mdpr_wild_swipe.ApplyEvent(ModFiles,
            new MslEvent("gml_Object_o_mdpr_wild_swipe_Create_0.gml", EventType.Create, 0),
            new MslEvent("gml_Object_o_mdpr_wild_swipe_Alarm_10.gml", EventType.Alarm, 10),
            new MslEvent("gml_Object_o_mdpr_wild_swipe_Step_0.gml", EventType.Step, 0),
            new MslEvent("gml_Object_o_mdpr_wild_swipe_Other_10.gml", EventType.Other, 10),
            new MslEvent("gml_Object_o_mdpr_wild_swipe_Other_25.gml", EventType.Other, 25)
        );

        o_mdpr_wild_swipe.ApplyEvent(
            new MslEvent("", EventType.Alarm, 0),
            new MslEvent("if is_player(owner)\n    scr_allturn()", EventType.Destroy, 0),
            new MslEvent("instance_destroy()", EventType.Other, 7),
            new MslEvent("draw_self()", EventType.Draw, 0)
        );

        UndertaleGameObject o_mdpr_wild_swipe_birth = Msl.AddObject(
            name: "o_mdpr_wild_swipe_birth",
            parentName: "o_spelllbirth",
            isVisible: true,
            isPersistent: false,
            isAwake: true,
            collisionShapeFlags: CollisionShapeFlags.Circle
        );

        o_mdpr_wild_swipe_birth.ApplyEvent(ModFiles,
            new MslEvent("gml_Object_o_mdpr_wild_swipe_birth_Create_0.gml", EventType.Create, 0)
        );

        UndertaleGameObject o_skill_mdpr_wild_swipe = Msl.AddObject(
            name: "o_skill_mdpr_wild_swipe", 
            spriteName: "s_skills_mdpr_swipe", 
            parentName: "o_skill", 
            isVisible: true, 
            isPersistent: false, 
            isAwake: true,
            collisionShapeFlags: CollisionShapeFlags.Circle
        );

        UndertaleGameObject o_skill_mdpr_wild_swipe_ico = Msl.AddObject(
            name: "o_skill_mdpr_wild_swipe_ico", 
            spriteName: "s_skills_mdpr_swipe", 
            parentName: "o_skill_ico", 
            isVisible: true, 
            isPersistent: false, 
            isAwake: true,
            collisionShapeFlags: CollisionShapeFlags.Circle
        );

        o_skill_mdpr_wild_swipe.ApplyEvent(ModFiles, 
            new MslEvent("gml_Object_o_skill_mdpr_wild_swipe_Create_0.gml", EventType.Create, 0),
            new MslEvent("gml_Object_o_skill_mdpr_wild_swipe_Other_17.gml", EventType.Other, 17),
            new MslEvent("gml_Object_o_skill_mdpr_wild_swipe_Other_14.gml", EventType.Other, 14)
        );

        o_skill_mdpr_wild_swipe_ico.ApplyEvent(ModFiles, 
            new MslEvent("gml_Object_o_skill_mdpr_wild_swipe_ico_Create_0.gml", EventType.Create, 0)
        );

        // Write Skills Stat

        Msl.InjectTableSkillsStat(
            metaGroup: Msl.SkillsStatMetaGroup.PROSELYTES,
            id: "MDPR_Wild_Swipe",
            Object: "o_mdpr_wild_swipe_birth",
            Target: Msl.SkillsStatTarget.TargetArea,
            Range: "1",
            KD: 8,
            MP: 12,
            AOE_Lenght: 1,
            AOE_Width: 3,
            Attack: true
        );
    }

    private void PatchSkill_Rend_Flesh()
    {
        UndertaleGameObject o_skill_mdpr_rend_flesh = Msl.AddObject(
            name: "o_skill_mdpr_rend_flesh", 
            spriteName: "s_skills_mdpr_rend_flesh", 
            parentName: "o_skill", 
            isVisible: true, 
            isPersistent: false, 
            isAwake: true,
            collisionShapeFlags: CollisionShapeFlags.Circle
        );

        Msl.AddNewEvent(
            objectName: o_skill_mdpr_rend_flesh,
            eventType: EventType.Create, subtype: 0,
            eventCode: @"
                event_inherited()
                skill = ""MDPR_Rend_Flesh""
                xx = 33
                button = ""5""
                scr_skill_atr()
                can_learn = true
            "
        );

        Msl.AddNewEvent(
            objectName: o_skill_mdpr_rend_flesh,
            eventType: EventType.Other, subtype: 14,
            eventCode: @"
                if scr_mod_num_of_empty_hands() > 0
                    event_inherited()
            "
        );

        Msl.AddNewEvent(
            objectName: o_skill_mdpr_rend_flesh,
            eventType: EventType.Other, subtype: 17,
            eventCode: @"
                if instance_exists(owner)
                {
                    var _num_empty = scr_mod_num_of_empty_hands()
                    ds_map_replace(data, ""Damage"", (16 + 0.4 * owner.Arms_DEF + 0.25 * owner.STR) * _num_empty)
                    ds_map_replace(data, ""Armor_Piercing"", (45 + 1.5 * owner.STR) + 50 * _num_empty)
                }
                event_inherited()
            "
        );

        UndertaleGameObject o_skill_mdpr_rend_flesh_ico = Msl.AddObject(
            name: "o_skill_mdpr_rend_flesh_ico", 
            spriteName: "s_skills_mdpr_rend_flesh",
            parentName: "o_skill_ico", 
            isVisible: true,
            isPersistent: false,
            isAwake: true,
            collisionShapeFlags: CollisionShapeFlags.Circle
        );

        Msl.AddNewEvent(
            objectName: o_skill_mdpr_rend_flesh_ico,
            eventType: EventType.Create, subtype: 0,
            eventCode: @"
                event_inherited()
                child_skill = o_skill_mdpr_rend_flesh
                event_perform_object(child_skill, ev_create, 0)
                xshift = 226
                yy += 60
            "
        );

        UndertaleGameObject o_mdpr_rend_flesh = Msl.AddObject(
            name: "o_mdpr_rend_flesh",
            parentName: "o_target_spell",
            isVisible: true,
            isPersistent: false,
            isAwake: true,
            collisionShapeFlags: CollisionShapeFlags.Circle
        );

        Msl.AddNewEvent(
            objectName: o_mdpr_rend_flesh,
            eventType: EventType.Create, subtype: 0,
            eventCode: @"
                event_inherited()
                alarm[0] = 1
                type = ""noWeapon""
            "
        );

        Msl.AddNewEvent(
            objectName: o_mdpr_rend_flesh,
            eventType: EventType.Alarm, subtype: 0,
            eventCode: @"
                if instance_exists(target)
                {
                    with (owner)
                    {
                        scr_hit_deformation(other.target, o_hit_rapture)
                        scr_skill_attack(""noWeapon"")
                    }
                }
                event_inherited()
            "
        );

        Msl.AddNewEvent(
            objectName: o_mdpr_rend_flesh,
            eventType: EventType.Other, subtype: 25,
            eventCode: @"
                with (owner)
                {
                    scr_damage_chance_reset()
                    Bodypart_Damage = 99
                    Armor_Piercing = (45 + 1.5 * STR) + 50 * scr_mod_num_of_empty_hands()
                    Blunt_Damage = 0
                    Rending_Damage = (16 + 0.4 * Arms_DEF + 0.25 * STR) * scr_mod_num_of_empty_hands()
                    Hit_Chance += 5
                }
            "
        );

        Msl.InjectTableSkillsStat(
            metaGroup: Msl.SkillsStatMetaGroup.PROSELYTES,
            id: "MDPR_Rend_Flesh",
            Object: "o_mdpr_rend_flesh",
            Target: Msl.SkillsStatTarget.TargetObject,
            Range: "1",
            KD: 10,
            MP: 25,
            Attack: true
        );
    }

    private static void AdjustSkillIcon(string name)
    {
        UndertaleSprite ico = Msl.GetSprite(name);
        ico.CollisionMasks.RemoveAt(0);
        ico.IsSpecialType = true;
        ico.SVersion = 3;
        ico.OriginX = 12;
        ico.OriginY = 12;
        ico.GMS2PlaybackSpeed = 1;
        ico.GMS2PlaybackSpeedType = AnimSpeedType.FramesPerGameFrame;
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
}
