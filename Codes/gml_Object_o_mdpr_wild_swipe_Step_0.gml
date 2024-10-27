image_angle = direction
image_xscale = lerp(image_xscale, 1, 0.2)
if (!is_execute)
{
    if (image_index >= (cast_frame + 0.5))
    {
        is_execute = true
        with (o_skill_aoe_zone)
        {
            if (main_owner == other.owner)
                alarm[1] = 1
        }
    }
}
