if instance_exists(owner)
{
    var _num_empty = scr_mod_num_of_empty_hands()
    ds_map_replace(data, "Damage", (8 + 0.4 * owner.Arms_DEF + 0.25 * owner.STR) * _num_empty)
    ds_map_replace(data, "Knockback_Chance", (45 + 1.5 * owner.STR) + 50 * _num_empty)
}
event_inherited()