﻿using System.Media;

namespace GTA5OnlineTools.Common.Utils
{
    public class AudioUtil
    {
        public static SoundPlayer SP_Click_01 = new SoundPlayer(Properties.Resources.Click_01);
        public static SoundPlayer SP_Click_02 = new SoundPlayer(Properties.Resources.Click_02);
        public static SoundPlayer SP_Click_03 = new SoundPlayer(Properties.Resources.Click_03);
        public static SoundPlayer SP_Click_04 = new SoundPlayer(Properties.Resources.Click_04);
        public static SoundPlayer SP_Click_05 = new SoundPlayer(Properties.Resources.Click_05);

        public static SoundPlayer SP_GTA5_Email = new SoundPlayer(Properties.Resources.GTA5_Email);
        public static SoundPlayer SP_GTA5_Job = new SoundPlayer(Properties.Resources.GTA5_Job);
        public static SoundPlayer SP_DownloadOK = new SoundPlayer(Properties.Resources.DownloadOK);

        // 按钮提示音
        public static int Button_ClickSound = 4;

        /// <summary>
        /// 按钮点击音效
        /// </summary>
        public static void ClickSound()
        {
            switch (Button_ClickSound)
            {
                case 0:
                    break;
                case 1:
                    SP_Click_01.Play();
                    break;
                case 2:
                    SP_Click_02.Play();
                    break;
                case 3:
                    SP_Click_03.Play();
                    break;
                case 4:
                    SP_Click_04.Play();
                    break;
                case 5:
                    SP_Click_05.Play();
                    break;
            }
        }
    }
}
