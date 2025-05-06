using System;
using UnityEngine;

public class Consts
{
    public struct SceneNames{
        public const string GAME_SCENE = "GameScene";
        public const string MENU_SCENE = "MenuScene";
    }
    public struct Layers{
        public const string GROUND_LAYER = "Ground";
        public const string FLOOR_LAYER = "Floor";
    }
    public struct CatAnimations{
        public const string IS_IDLING = "IsIdling";
        public const string IS_WALKING = "IsWalking";
        public const string IS_RUNNING = "IsRunning";
        public const string IS_ATTAKING = "IsAttacking";
    }
    public struct PlayerAnimations{
        public const string IS_MOVING ="IsMoving";
        public const string IS_JUMPİNG ="IsJumping";
        public const string IS_SLIDING ="IsSliding";
        public const string IS_SLIDING_ACTIVE ="IsSlidingActive";
    }
    public struct WheatTypes{
        public const string GOLD_WHEAT = "GoldWheat";
        public const string HOLY_WHEAT = "HolyWheat";
        public const string Rotten_WHEAT = "RottenWheat";
    }

    public struct OtherAnimations{
        public const string IS_SPATULAJUMPİNG = "IsSpatulaJumping";
    }
}
