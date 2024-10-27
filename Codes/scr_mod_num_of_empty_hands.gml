function scr_mod_num_of_empty_hands()
{
    var _count = 0
    with (o_weapon_slot_parent)
    {
        if instance_exists(children)
        {
            if children.equipped
            {
                _count++
                with (children)
                {
                    if (type == "2hsword" || type == "2haxe" || type == "2hmace" || type == "spear" || type == "2hStaff")
                        _count++
                }
            }
        }
    }

    return 2 - _count;
}