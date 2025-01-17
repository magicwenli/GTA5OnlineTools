﻿using GTA5OnlineTools.Common.Utils;
using GTA5OnlineTools.Features.SDK;
using GTA5OnlineTools.Features.Data;
using static GTA5OnlineTools.Features.SDK.Hacks;

namespace GTA5OnlineTools.Modules.Windows.ExternalMenu
{
    /// <summary>
    /// EM03OnlineOptionView.xaml 的交互逻辑
    /// </summary>
    public partial class EM03OnlineOptionView : UserControl
    {
        private struct PVInfo
        {
            public int Index;
            public string Name;
            public long hash;
            public string plate;
        }

        private List<PVInfo> pVInfos = new List<PVInfo>();

        public EM03OnlineOptionView()
        {
            InitializeComponent();

            ExternalMenuView.ClosingDisposeEvent += ExternalMenuView_ClosingDisposeEvent;
        }

        private void ExternalMenuView_ClosingDisposeEvent()
        {
            
        }

        private void CheckBox_RemovePassiveModeCooldown_Click(object sender, RoutedEventArgs e)
        {
            Online.RemovePassiveModeCooldown(CheckBox_RemovePassiveModeCooldown.IsChecked == true);
        }

        private void CheckBox_RemoveSuicideCooldown_Click(object sender, RoutedEventArgs e)
        {
            Online.RemoveSuicideCooldown(CheckBox_RemoveSuicideCooldown.IsChecked == true);
        }

        private void CheckBox_DisableOrbitalCooldown_Click(object sender, RoutedEventArgs e)
        {
            Online.DisableOrbitalCooldown(CheckBox_DisableOrbitalCooldown.IsChecked == true);
        }

        private void CheckBox_OffRadar_Click(object sender, RoutedEventArgs e)
        {
            Online.OffRadar(CheckBox_OffRadar.IsChecked == true);
        }

        private void CheckBox_GhostOrganization_Click(object sender, RoutedEventArgs e)
        {
            Online.GhostOrganization(CheckBox_GhostOrganization.IsChecked == true);
        }

        private void CheckBox_BribeOrBlindCops_Click(object sender, RoutedEventArgs e)
        {
            Online.BribeOrBlindCops(CheckBox_BribeOrBlindCops.IsChecked == true);
        }

        private void CheckBox_BribeAuthorities_Click(object sender, RoutedEventArgs e)
        {
            Online.BribeAuthorities(CheckBox_BribeAuthorities.IsChecked == true);
        }

        private void CheckBox_RevealPlayers_Click(object sender, RoutedEventArgs e)
        {
            Online.RevealPlayers(CheckBox_RevealPlayers.IsChecked == true);
        }

        private void CheckBox_AllowSellOnNonPublic_Click(object sender, RoutedEventArgs e)
        {
            Online.AllowSellOnNonPublic(CheckBox_AllowSellOnNonPublic.IsChecked == true);
        }

        private void CheckBox_OnlineSnow_Click(object sender, RoutedEventArgs e)
        {
            Online.SessionSnow(CheckBox_OnlineSnow.IsChecked == true);
        }

        private void Button_Blips_Click(object sender, RoutedEventArgs e)
        {
            AudioUtil.ClickSound();

            var str = (e.OriginalSource as Button).Content.ToString();

            int index = MiscData.Blips.FindIndex(t => t.Name == str);
            if (index != -1)
            {
                Teleport.ToBlips(MiscData.Blips[index].ID);
            }
        }

        private void Button_MerryweatherServices_Click(object sender, RoutedEventArgs e)
        {
            AudioUtil.ClickSound();

            var str = (e.OriginalSource as Button).Content.ToString();

            int index = MiscData.MerryweatherServices.FindIndex(t => t.Name == str);
            if (index != -1)
            {
                Online.MerryweatherServices(MiscData.MerryweatherServices[index].ID);
            }
        }

        private void CheckBox_InstantBullShark_Click(object sender, RoutedEventArgs e)
        {
            Online.InstantBullShark(CheckBox_InstantBullShark.IsChecked == true);
        }

        private void CheckBox_BackupHeli_Click(object sender, RoutedEventArgs e)
        {
            Online.BackupHeli(CheckBox_BackupHeli.IsChecked == true);
        }

        private void CheckBox_Airstrike_Click(object sender, RoutedEventArgs e)
        {
            Online.Airstrike(CheckBox_Airstrike.IsChecked == true);
        }

        private void Button_RefushPersonalVehicleList_Click(object sender, RoutedEventArgs e)
        {
            AudioUtil.ClickSound();

            ListBox_PersonalVehicle.Items.Clear();
            pVInfos.Clear();

            Task.Run(() =>
            {
                int max_slots = ReadGA<int>(1585844);
                for (int i = 0; i < max_slots; i++)
                {
                    long hash = ReadGA<long>(1585844 + 1 + (i * 142) + 66);
                    if (hash == 0)
                        continue;

                    string plate = ReadGAString(1585844 + 1 + (i * 142) + 1);

                    pVInfos.Add(new PVInfo()
                    {
                        Index = i,
                        Name = FindVehicleDisplayName(hash, true),
                        hash = hash,
                        plate = plate
                    });
                }

                foreach (var item in pVInfos)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        ListBox_PersonalVehicle.Items.Add($"{item.Name} [{item.plate}]");
                    });
                }
            });
        }

        private void Button_SpawnPersonalVehicle_Click(object sender, RoutedEventArgs e)
        {
            AudioUtil.ClickSound();

            int index = ListBox_PersonalVehicle.SelectedIndex;

            if (index != -1)
            {
                Task.Run(() =>
                {
                    WriteGA<int>(2810287 + 965, pVInfos[index].Index);
                    WriteGA<int>(2810287 + 962, 1);
                    Task.Delay(500).Wait();
                    WriteGA<int>(2671444 + 8, 1);
                });
            }
        }

        private string FindVehicleDisplayName(long hash, bool isDisplay)
        {
            foreach (var item in VehicleData.VehicleClassData)
            {
                foreach (var item0 in item.VehicleInfo)
                {
                    if (item0.Hash == hash)
                    {
                        if (isDisplay)
                            return item0.DisplayName;
                        else
                            return item0.Name;
                    }
                }
            }

            return "";
        }
    }
}
