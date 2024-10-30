using UnityEngine;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

[Category("Custom/Conditions")]
[Name("Can Hear Player Steps")]
public class CanHearPlayerSteps : ConditionTask {
    public float hearingRadius = 10f;
    public BBParameter<AudioSource> playerAudioSource;

    protected override bool OnCheck() {
        if (playerAudioSource.value.isPlaying) {
            float distance = Vector3.Distance(agent.transform.position, playerAudioSource.value.transform.position);
            return distance <= hearingRadius;
        }
        return false;
    }
}




