event_inherited()
skill = "MDPR_Wild_Swipe"
ds_list_add(attribute, ds_map_find_value(global.attribute, "STR"))
xx = 33
button = "5"
scr_skill_atr()
can_learn = true
// Important!
main_spell = o_mdpr_wild_swipe
target_group = o_enemy
if (global.language == 3)
{
    info = "- 至少需要一个兵器槽位空置"
}
else
    info = "- At least one weapon slot needs to be empty"