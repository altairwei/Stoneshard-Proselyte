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
        // Handle Sprites

        Msl.GetSprite("s_vampirism_branch").OriginX = 0;
        Msl.GetSprite("s_vampirism_branch").OriginY = 0;

        AdjustSkillIcon("s_skills_mdpr_swipe");
        AdjustSkillIcon("s_skills_mdpr_rend_flesh");
        AdjustSkillIcon("s_skills_mdpr_baleful_scream");

        // Add Functions

        Msl.AddFunction(ModFiles.GetCode("scr_mod_num_of_empty_hands.gml"), "scr_mod_num_of_empty_hands");

        // Add Skills

        PatchSkill_Wild_Swipe();
        PatchSkill_Rend_Flesh();
        PatchSkill_Baleful_Scream();

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
                ds_list_add(attribute, ds_map_find_value(global.attribute, ""STR""))
                xx = 33
                button = ""5""
                scr_skill_atr()
                can_learn = true
                if (global.language == 3)
                {
                    info = ""- 至少需要一个兵器槽位空置""
                }
                else
                    info = ""- At least one weapon slot needs to be empty""
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

    private void PatchSkill_Baleful_Scream()
    {
        UndertaleGameObject o_skill_mdpr_baleful_scream = Msl.AddObject(
            name: "o_skill_mdpr_baleful_scream", 
            spriteName: "s_skills_mdpr_baleful_scream", 
            parentName: "o_skill", 
            isVisible: true,
            isPersistent: false,
            isAwake: true,
            collisionShapeFlags: CollisionShapeFlags.Circle
        );

        Msl.AddNewEvent(
            objectName: o_skill_mdpr_baleful_scream,
            eventType: EventType.Create, subtype: 0,
            eventCode: @"
                event_inherited()
                skill = ""MDPR_Baleful_Scream""
                ds_list_add(attribute, ds_map_find_value(global.attribute, ""WIL""))
                scr_skill_atr()
                can_learn = true
                ignore_interact = true
            "
        );

        Msl.AddNewEvent(
            objectName: o_skill_mdpr_baleful_scream,
            eventType: EventType.Other, subtype: 17,
            eventCode: @"
                if instance_exists(owner)
                    ds_map_replace(data, ""Psionic_Damage"", 6 + 0.5 * owner.WIL)
                event_inherited()
            "
        );

        UndertaleGameObject o_skill_mdpr_baleful_scream_ico = Msl.AddObject(
            name: "o_skill_mdpr_baleful_scream_ico", 
            spriteName: "s_skills_mdpr_baleful_scream",
            parentName: "o_skill_ico", 
            isVisible: true,
            isPersistent: false,
            isAwake: true,
            collisionShapeFlags: CollisionShapeFlags.Circle
        );

        Msl.AddNewEvent(
            objectName: o_skill_mdpr_baleful_scream_ico,
            eventType: EventType.Create, subtype: 0,
            eventCode: @"
                event_inherited()
                child_skill = o_skill_mdpr_baleful_scream
                event_perform_object(child_skill, ev_create, 0)
                xshift = 275
                yy += 30
            "
        );

        Msl.InjectTableSkillsStat(
            metaGroup: Msl.SkillsStatMetaGroup.PROSELYTES,
            id: "MDPR_Baleful_Scream",
            Object: "o_mdpr_baleful_scream_birth",
            Target: Msl.SkillsStatTarget.TargetObject,
            Range: "6",
            KD: 9,
            MP: 6,
            Bonus_Range: true,
            Maneuver: true
        );

        UndertaleGameObject o_mdpr_baleful_scream = Msl.AddObject(
            name: "o_mdpr_baleful_scream",
            spriteName: "s_screamofdoom_mid",
            parentName: "o_shell_damage",
            isVisible: true,
            isPersistent: false,
            isAwake: true,
            collisionShapeFlags: CollisionShapeFlags.Circle
        );

        o_mdpr_baleful_scream.ApplyEvent(
            new MslEvent(eventType: EventType.Create, subtype: 0, code: @"
                event_inherited()
                scr_damage_init()
                image_alpha = 0
                image_yscale = 0
                image_speed = 1
                image_index = random(image_number)
                speed = random_range(0, 1)
                scr_set_lt()
                play_impact = true
                alarm[0] = 10 + random(6)
                alarm[1] = 1
            "),
            new MslEvent(eventType: EventType.Destroy, subtype: 0, code: @"
                event_inherited()
                if is_player(owner)
                    scr_allturn()
            "),
            new MslEvent(eventType: EventType.Alarm, subtype: 0, code: "event_user(2)"),
            new MslEvent(eventType: EventType.Alarm, subtype: 1, code: ""),
            new MslEvent(eventType: EventType.Alarm, subtype: 2, code: "instance_destroy()"),
            new MslEvent(eventType: EventType.Step, subtype: 0, code: @"
                event_inherited()
                var _trg_speed = 5
                speed = 6
                image_speed = speed / _trg_speed
                image_alpha = lerp(image_alpha, 1, 0.3)
                image_yscale = image_alpha
                depth = (-y) - 26
                image_angle = direction
            "),
            new MslEvent(eventType: EventType.Other, subtype: 10, code: @"
                event_user(1)
                Psionic_Damage = 6 + 0.5 * owner.WIL
                with (target)
                    Body_Part_target = scr_choose_body_part(""head"")
                event_inherited()
                if target
                {
                    if instance_exists(target)
                    {
                        if scr_chance_value(75)
                            scr_effect_create(o_db_daze, o_coals_and_embers_birth, target, owner)
                        else
                            scr_effect_create(o_db_stagger, o_loot_cabbage_half, target, owner)
                    }
                }
            "),
            new MslEvent(eventType: EventType.Other, subtype: 11, code: @"
                if instance_exists(target)
                {
                    var _dir = direction
                    with (instance_create((target.x + (random_range(2, -2))), (target.y + (random_range(2, -2))), o_spellimpact))
                    {
                        scr_light_off()
                        owner = other.id
                        sprite_index = s_screamofdoom_explode
                        scr_set_lt()
                        lumalpha = 0
                        alpha = 0
                    }
                }
                else
                    instance_destroy()
            "),
            new MslEvent(eventType: EventType.Other, subtype: 12, code: ""),
            new MslEvent(eventType: EventType.Other, subtype: 21, code: @"
                if (instance_exists(target) && instance_exists(owner))
                {
                    var _distance = (point_distance(owner.x, owner.y, x, y)) / 26
                    var _direction = point_direction(owner.x, owner.y, target.x, target.y)
                    var _lightSurfaceOffset = o_shader_start.lightSurfaceOffset
                    var _end = 3
                    for (var i = 1; i < min(_end, _distance); i++)
                        draw_sprite_ext(s_screamofdoom_mid, (image_index - i), (x - (lengthdir_x((i * 26), _direction)) - global.cameraX + _lightSurfaceOffset), (y - (lengthdir_y((i * 26), _direction)) - global.cameraY + _lightSurfaceOffset), 1, 1, _direction, c_white, 1)
                }
            "),
            new MslEvent(eventType: EventType.Draw, subtype: 0, code: @"
                if (instance_exists(target) && instance_exists(owner))
                {
                    var _distance = (point_distance(owner.x, owner.y, x, y)) / 26
                    var _direction = point_direction(owner.x, owner.y, target.x, target.y)
                    var _end = 3
                    image_index = 13 + 6 * (sin(current_time / 300))
                    draw_sprite_ext(s_screamofdoom_mid, image_index, x, y, 1, 1, _direction, c_white, 1)
                    for (var i = 1; i < min(_end, _distance); i++)
                        draw_sprite_ext(s_screamofdoom_mid, (image_index - i), (x - (lengthdir_x((i * 26), _direction))), (y - (lengthdir_y((i * 26), _direction)) - 3), 1, 1, _direction, c_white, 1)
                }
            "),
            new MslEvent(eventType: EventType.PreCreate, subtype: 0, code: "event_inherited()\nblend = 4279667175")
        );

        UndertaleGameObject o_mdpr_baleful_scream_birth = Msl.AddObject(
            name: "o_mdpr_baleful_scream_birth", 
            spriteName: "s_screamofdoom_start",
            parentName: "o_spelllbirth", 
            isVisible: false,
            isPersistent: false,
            isAwake: true,
            collisionShapeFlags: CollisionShapeFlags.Circle
        );

        Msl.AddNewEvent(
            objectName: o_mdpr_baleful_scream_birth,
            eventType: EventType.Create, subtype: 0,
            eventCode: @"
                event_inherited()
                scr_set_lt()
                y -= 5
                cast_frame = 3
                spell = o_mdpr_baleful_scream
                is_crit = false
            "
        );

        Msl.AddNewEvent(
            objectName: o_mdpr_baleful_scream_birth,
            eventType: EventType.Alarm, subtype: 0,
            eventCode: @"
                event_inherited()
                if instance_exists(owner)
                    scr_audio_play_at(snd_skill_screem_of_doom)
            "
        );

        Msl.AddNewEvent(
            objectName: o_mdpr_baleful_scream_birth,
            eventType: EventType.PreCreate, subtype: 0,
            eventCode: @"
                event_inherited()
                alpha = 0.5
                lumalpha = 0.6
                blend = 4279667175
            "
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
