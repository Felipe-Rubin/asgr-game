using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillState
{
    use,
    cast,
    activated,
    completed,
    cooldown,
    ready
}

// Use: Someone Requested it's use, created Instance
// Cast: Wait Cast Time if exists
// Activated: Is currently active on the screen
// Completed: Execution Complete
// Cooldown: Wait Cooldown Timeto use again
// Ready: Ready to use (again)