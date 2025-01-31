﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;

namespace GLOBAL_PARA
{
    
    public enum CubeType
    {
        REDUP = 0,
        BLUEUP = 1,
        REDDOWN = 2,
        BLUEDOWN = 3,
        REDRIGHT = 4,
        BLUERIGHT = 5,
        REDLEFT = 6,
        BLUELEFT  =7,
        REDANY = 8,
        BLUEANY = 9,
    }
    public class LogoutInfo
    {
        public int PlayerID;
        public LogoutInfo()
        {
            this.PlayerID = GLOBAL_PARA.Game.PlayerID;
        }
    }

    public class GameRecord
    {
        public int PlayerID { get; set; }
        public int MusicID { get; set; }
        public float Score { get; set; }
        public float Progress { get; set; }
        public string RecordTime { get; set; }
        public GameRecord(int playerID,int musicID,float score,float progress,DateTime dateTime)
        {
            this.PlayerID = playerID;
            this.MusicID = musicID;
            this.Score = score;
            this.Progress = progress;
            this.RecordTime = DateTime.Now.GetDateTimeFormats('s')[0].ToString().Replace("T", " ");
        }
    }
    public class Global
    {

    }
    public class Game
    {
        public static int PlayerID;
        /// <summary>
        /// CubeHeatRecord记录被正确击中的物体的数量
        /// </summary>
        public static int CubeHeatRecord = 0;

        /// <summary>
        /// CubeSendRecord记录目前产生了多少个实体
        /// </summary>
        public static int CubeSendRecord = 0;

        /// <summary>
        /// CubeSumRecord记录当前歌曲要产生的实体总数
        /// </summary>
        public static int CubeSumRecord = 0;

        /// <summary>
        /// CurrentComboRecord记录当前的连击数
        /// </summary>
        public static int CurrentComboRecord = 0;

        /// <summary>
        /// MaxComboRecord记录游玩的最大连击数
        /// </summary>
        public static int MaxComboRecord = 0;

        /// <summary>
        /// 清空记录板
        /// </summary>
        public static void ClearRecord()
        {
            CubeHeatRecord = 0;
            CubeSendRecord = 0;
            CubeSumRecord = 0;
            CurrentComboRecord = 0;
            MaxComboRecord = 0;
        }

        /// <summary>
        /// 返回击中方块的百分比
        /// </summary>
        /// <returns>返回击中的方块数占总的方块数的比例</returns>
        public static float GetHeatPercent()
        {
            return ((float)CubeHeatRecord / (float)CubeSendRecord)*100;
        }

        /// <summary>
        /// 切割成功时
        /// </summary>
        public static void CutCorrect()
        {
            CubeHeatRecord++;
            CurrentComboRecord++;
        }

        /// <summary>
        /// 切割失败时刷新连击数
        /// </summary>
        public static void RefreshCombo()
        {
            if (MaxComboRecord < CurrentComboRecord)
                MaxComboRecord = CurrentComboRecord;
            CurrentComboRecord = 0;
        }
        /// <summary>
        /// 计算得分
        /// </summary>
        public static float CountScore()
        {
            return CubeHeatRecord*5+MaxComboRecord;
        }
            
    }

    /// <summary>
    /// 存储音乐的存放路径，BPM等信息
    /// </summary>
    public class SongInfo
    {
        public enum SONG
        {
            SingMeToSleep = 88,//228
            AllFallsDown = 98,//257
            WhateverItTakes = 121,//296
            TikTok = 120,//246
            TroubleMaker = 115,//263
            Believer = 125,
        }

        public string songName { set; get; }
        public string filaPath { set; get; }
        public int numOfCube { set; get; }
        public int bPM { set; get; }
        public SongInfo(GLOBAL_PARA.SongInfo.SONG song)
        {
            songName = song.ToString();
            bPM = (int)song;
            filaPath = Application.dataPath + @"/Data/" + songName + ".json";
        }
    }

    /// <summary>
    /// 要求击中物体的方向，上下左右以及任意点
    /// </summary>
    public enum HitPoint
    {
        UP = 0,
        DOWN = 1,
        LEFT = 2,
        RIGHT = 3,
        ANY = 4,
    };

    /// <summary>
    /// 枚举颜色类型，一共有RED和BLUE两种
    /// </summary>
    public enum TypeOfColor
    {
        RED = 0,
        BLUE = 1,
    };

    public class CubePoint
    {
        public int cubeType { get; set; }//对应预制体前面的编号
        public float time { get; set; }//在游戏出现的时间点
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }
        public CubePoint()
        {

        }
        public CubePoint(int cubeType, float time,float x,float y,float z)
        {
            this.cubeType = cubeType;
            this.time = time;
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }

}

