with (owner)
{
    scr_damage_chance_reset()
    Blunt_Damage = 0
    Rending_Damage = (8 + 0.4 * Arms_DEF + 0.25 * STR) * scr_mod_num_of_empty_hands()
    Knockback_Chance = (45 + 1.5 * STR) + 50 * scr_mod_num_of_empty_hands()
    Hit_Chance += 5
}
