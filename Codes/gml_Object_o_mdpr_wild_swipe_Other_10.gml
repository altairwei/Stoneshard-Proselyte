if (!instance_exists(owner))
    return;
var hit = 0
if (target.object_index != o_skill_aoe_zone)
{
    with (owner)
    {
        var _kc = Knockback_Chance

        Knockback_Chance = (45 + 1.5 * STR) + 50 * scr_mod_num_of_empty_hands()
        hit = scr_skill_attack("noWeapon")

        Knockback_Chance = _kc

        if hit
        {
            with (other.target)
            {
                with (scr_guiAnimation(s_swipehit, 1, 1, 0))
                    scr_set_lt(sprite_index)
            }
        }
    }
    if hit
    {
        with (instance_create(target.x, target.y, o_spellimpact))
            col = 0xFFFFFF
    }
}
