﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnoughHookLite.Offsets
{
    public sealed class Netvars
    {
        [JsonProperty("cs_gamerules_data")] public Int32 cs_gamerules_data = 0x0;
        [JsonProperty("m_ArmorValue")] public Int32 m_ArmorValue = 0x117CC;
        [JsonProperty("m_Collision")] public Int32 m_Collision = 0x320;
        [JsonProperty("m_CollisionGroup")] public Int32 m_CollisionGroup = 0x474;
        [JsonProperty("m_Local")] public Int32 m_Local = 0x2FCC;
        [JsonProperty("m_MoveType")] public Int32 m_MoveType = 0x25C;
        [JsonProperty("m_OriginalOwnerXuidHigh")] public Int32 m_OriginalOwnerXuidHigh = 0x31D4;
        [JsonProperty("m_OriginalOwnerXuidLow")] public Int32 m_OriginalOwnerXuidLow = 0x31D0;
        [JsonProperty("m_SurvivalGameRuleDecisionTypes")] public Int32 m_SurvivalGameRuleDecisionTypes = 0x1328;
        [JsonProperty("m_SurvivalRules")] public Int32 m_SurvivalRules = 0xD00;
        [JsonProperty("m_aimPunchAngle")] public Int32 m_aimPunchAngle = 0x303C;
        [JsonProperty("m_aimPunchAngleVel")] public Int32 m_aimPunchAngleVel = 0x3048;
        [JsonProperty("m_angEyeAnglesX")] public Int32 m_angEyeAnglesX = 0x117D0;
        [JsonProperty("m_angEyeAnglesY")] public Int32 m_angEyeAnglesY = 0x117D4;
        [JsonProperty("m_bBombDefused")] public Int32 m_bBombDefused = 0x29C0;
        [JsonProperty("m_bBombPlanted")] public Int32 m_bBombPlanted = 0x9A5;
        [JsonProperty("m_bBombTicking")] public Int32 m_bBombTicking = 0x2990;
        [JsonProperty("m_bFreezePeriod")] public Int32 m_bFreezePeriod = 0x20;
        [JsonProperty("m_bGunGameImmunity")] public Int32 m_bGunGameImmunity = 0x9990;
        [JsonProperty("m_bHasDefuser")] public Int32 m_bHasDefuser = 0x117DC;
        [JsonProperty("m_bHasHelmet")] public Int32 m_bHasHelmet = 0x117C0;
        [JsonProperty("m_bInReload")] public Int32 m_bInReload = 0x32B5;
        [JsonProperty("m_bIsDefusing")] public Int32 m_bIsDefusing = 0x997C;
        [JsonProperty("m_bIsQueuedMatchmaking")] public Int32 m_bIsQueuedMatchmaking = 0x74;
        [JsonProperty("m_bIsScoped")] public Int32 m_bIsScoped = 0x9974;
        [JsonProperty("m_bIsValveDS")] public Int32 m_bIsValveDS = 0x7C;
        [JsonProperty("m_bSpotted")] public Int32 m_bSpotted = 0x93D;
        [JsonProperty("m_bSpottedByMask")] public Int32 m_bSpottedByMask = 0x980;
        [JsonProperty("m_bStartedArming")] public Int32 m_bStartedArming = 0x3400;
        [JsonProperty("m_bUseCustomAutoExposureMax")] public Int32 m_bUseCustomAutoExposureMax = 0x9D9;
        [JsonProperty("m_bUseCustomAutoExposureMin")] public Int32 m_bUseCustomAutoExposureMin = 0x9D8;
        [JsonProperty("m_bUseCustomBloomScale")] public Int32 m_bUseCustomBloomScale = 0x9DA;
        [JsonProperty("m_clrRender")] public Int32 m_clrRender = 0x70;
        [JsonProperty("m_dwBoneMatrix")] public Int32 m_dwBoneMatrix = 0x26A8;
        [JsonProperty("m_fAccuracyPenalty")] public Int32 m_fAccuracyPenalty = 0x3340;
        [JsonProperty("m_fFlags")] public Int32 m_fFlags = 0x104;
        [JsonProperty("m_flC4Blow")] public Int32 m_flC4Blow = 0x29A0;
        [JsonProperty("m_flCustomAutoExposureMax")] public Int32 m_flCustomAutoExposureMax = 0x9E0;
        [JsonProperty("m_flCustomAutoExposureMin")] public Int32 m_flCustomAutoExposureMin = 0x9DC;
        [JsonProperty("m_flCustomBloomScale")] public Int32 m_flCustomBloomScale = 0x9E4;
        [JsonProperty("m_flDefuseCountDown")] public Int32 m_flDefuseCountDown = 0x29BC;
        [JsonProperty("m_flDefuseLength")] public Int32 m_flDefuseLength = 0x29B8;
        [JsonProperty("m_flFallbackWear")] public Int32 m_flFallbackWear = 0x31E0;
        [JsonProperty("m_flFlashDuration")] public Int32 m_flFlashDuration = 0x10470;
        [JsonProperty("m_flFlashMaxAlpha")] public Int32 m_flFlashMaxAlpha = 0x1046C;
        [JsonProperty("m_flLastBoneSetupTime")] public Int32 m_flLastBoneSetupTime = 0x2928;
        [JsonProperty("m_flLowerBodyYawTarget")] public Int32 m_flLowerBodyYawTarget = 0x9ADC;
        [JsonProperty("m_flNextAttack")] public Int32 m_flNextAttack = 0x2D80;
        [JsonProperty("m_flNextPrimaryAttack")] public Int32 m_flNextPrimaryAttack = 0x3248;
        [JsonProperty("m_flSimulationTime")] public Int32 m_flSimulationTime = 0x268;
        [JsonProperty("m_flTimerLength")] public Int32 m_flTimerLength = 0x29A4;
        [JsonProperty("m_hActiveWeapon")] public Int32 m_hActiveWeapon = 0x2F08;
        [JsonProperty("m_hBombDefuser")] public Int32 m_hBombDefuser = 0x29C4;
        [JsonProperty("m_hMyWeapons")] public Int32 m_hMyWeapons = 0x2E08;
        [JsonProperty("m_hObserverTarget")] public Int32 m_hObserverTarget = 0x339C;
        [JsonProperty("m_hOwner")] public Int32 m_hOwner = 0x29DC;
        [JsonProperty("m_hOwnerEntity")] public Int32 m_hOwnerEntity = 0x14C;
        [JsonProperty("m_hViewModel")] public Int32 m_hViewModel = 0x3308;
        [JsonProperty("m_iAccountID")] public Int32 m_iAccountID = 0x2FD8;
        [JsonProperty("m_iClip1")] public Int32 m_iClip1 = 0x3274;
        [JsonProperty("m_iCompetitiveRanking")] public Int32 m_iCompetitiveRanking = 0x1A84;
        [JsonProperty("m_iCompetitiveWins")] public Int32 m_iCompetitiveWins = 0x1B88;
        [JsonProperty("m_iCrosshairId")] public Int32 m_iCrosshairId = 0x11838;
        [JsonProperty("m_iDefaultFOV")] public Int32 m_iDefaultFOV = 0x333C;
        [JsonProperty("m_iEntityQuality")] public Int32 m_iEntityQuality = 0x2FBC;
        [JsonProperty("m_iFOV")] public Int32 m_iFOV = 0x31F4;
        [JsonProperty("m_iFOVStart")] public Int32 m_iFOVStart = 0x31F8;
        [JsonProperty("m_iGlowIndex")] public Int32 m_iGlowIndex = 0x10488;
        [JsonProperty("m_iHealth")] public Int32 m_iHealth = 0x100;
        [JsonProperty("m_iItemDefinitionIndex")] public Int32 m_iItemDefinitionIndex = 0x2FBA;
        [JsonProperty("m_iItemIDHigh")] public Int32 m_iItemIDHigh = 0x2FD0;
        [JsonProperty("m_iMostRecentModelBoneCounter")] public Int32 m_iMostRecentModelBoneCounter = 0x2690;
        [JsonProperty("m_iObserverMode")] public Int32 m_iObserverMode = 0x3388;
        [JsonProperty("m_iShotsFired")] public Int32 m_iShotsFired = 0x103E0;
        [JsonProperty("m_iState")] public Int32 m_iState = 0x3268;
        [JsonProperty("m_iTeamNum")] public Int32 m_iTeamNum = 0xF4;
        [JsonProperty("m_lifeState")] public Int32 m_lifeState = 0x25F;
        [JsonProperty("m_nBombSite")] public Int32 m_nBombSite = 0x2994;
        [JsonProperty("m_nFallbackPaintKit")] public Int32 m_nFallbackPaintKit = 0x31D8;
        [JsonProperty("m_nFallbackSeed")] public Int32 m_nFallbackSeed = 0x31DC;
        [JsonProperty("m_nFallbackStatTrak")] public Int32 m_nFallbackStatTrak = 0x31E4;
        [JsonProperty("m_nForceBone")] public Int32 m_nForceBone = 0x268C;
        [JsonProperty("m_nTickBase")] public Int32 m_nTickBase = 0x3440;
        [JsonProperty("m_nViewModelIndex")] public Int32 m_nViewModelIndex = 0x29D0;
        [JsonProperty("m_rgflCoordinateFrame")] public Int32 m_rgflCoordinateFrame = 0x444;
        [JsonProperty("m_szCustomName")] public Int32 m_szCustomName = 0x304C;
        [JsonProperty("m_szLastPlaceName")] public Int32 m_szLastPlaceName = 0x35C4;
        [JsonProperty("m_thirdPersonViewAngles")] public Int32 m_thirdPersonViewAngles = 0x31E8;
        [JsonProperty("m_vecOrigin")] public Int32 m_vecOrigin = 0x138;
        [JsonProperty("m_vecVelocity")] public Int32 m_vecVelocity = 0x114;
        [JsonProperty("m_vecViewOffset")] public Int32 m_vecViewOffset = 0x108;
        [JsonProperty("m_viewPunchAngle")] public Int32 m_viewPunchAngle = 0x3030;
        [JsonProperty("m_zoomLevel")] public Int32 m_zoomLevel = 0x33E0;
    }
}
