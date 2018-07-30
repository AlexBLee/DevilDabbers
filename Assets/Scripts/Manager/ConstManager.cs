using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ConstManager
{
    // TAGS
    public const string TAG_PLAYER = "Player";
    public const string TAG_ENEMY = "Enemy";
    public const string TAG_BULLET = "Bullet";
    public const string TAG_KILLZONE = "Killzone";
    public const string TAG_PLAYER_BULLET = "PlayerBullet";

    // EVENTS
    public const string EVENT_PLAYER_FIRE = "PlayerFire";
    public const string EVENT_PLAYER_SGFIRE = "PlayerSGFire";
    public const string EVENT_PLAYER_DEATH = "PlayerDeath";

    public const string EVENT_PLAY_BGM = "PlayBGM";
    public const string EVENT_PLAYER_HIT = "PlayerHit";
    public const string EVENT_ENEMY_HIT = "EnemyHit";

    // AUDIO CLIP NAMES
    public const string SFX_PLAYER_FIRE = "SFX_Fire";
    public const string SFX_PLAYER_SGFIRE = "SFX_SGFire";
    public const string SFX_PLAYER_DEATH = "SFX_Death";
    public const string SFX_ENEMY_HIT = "SFX_EnemyHit";
    public const string BGM_MAIN = "BGM_Main";

}
