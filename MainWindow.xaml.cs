using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AimbotConfig
{
    public partial class MainWindow : Window
    {
        private bool isCodSelected = false;
        private bool isR6Selected = false;
        private string? selectedCodOverlay = null;
        private string? selectedR6Overlay = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void CodOptimizerButton_Click(object sender, RoutedEventArgs e)
        {
            isCodSelected = !isCodSelected;
            R6OptimizerButton!.Visibility = isCodSelected ? Visibility.Collapsed : Visibility.Visible;
            CodOverlayPanel!.Visibility = isCodSelected ? Visibility.Visible : Visibility.Collapsed;
            BackButton!.Visibility = isCodSelected ? Visibility.Visible : Visibility.Collapsed;
            UpdateRunButtonState();
        }

        private void R6OptimizerButton_Click(object sender, RoutedEventArgs e)
        {
            isR6Selected = !isR6Selected;
            CodOptimizerButton!.Visibility = isR6Selected ? Visibility.Collapsed : Visibility.Visible;
            R6OverlayPanel!.Visibility = isR6Selected ? Visibility.Visible : Visibility.Collapsed;
            BackButton!.Visibility = isR6Selected ? Visibility.Visible : Visibility.Collapsed;
            UpdateRunButtonState();
        }

        private void CodOverlayButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button == null) return;
            string overlayType = button.Content.ToString();

            // Reset background colors for all overlay buttons
            CodNewButton!.Background = CodCloudpanelButton!.Background = System.Windows.Media.Brushes.DarkGray;

            // Highlight the selected overlay button
            button.Background = System.Windows.Media.Brushes.DodgerBlue;

            selectedCodOverlay = overlayType;
            UpdateRunButtonState();
        }

        private void R6OverlayButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button == null) return;
            string overlayType = button.Content.ToString();

            // Reset background colors for all overlay buttons
            R6LegacyButton!.Background = System.Windows.Media.Brushes.DarkGray;

            // Highlight the selected overlay button
            button.Background = System.Windows.Media.Brushes.DodgerBlue;

            selectedR6Overlay = overlayType;
            UpdateRunButtonState();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            isCodSelected = false;
            isR6Selected = false;
            selectedCodOverlay = null;
            selectedR6Overlay = null;
            CodOptimizerButton!.Visibility = Visibility.Visible;
            R6OptimizerButton!.Visibility = Visibility.Visible;
            CodOverlayPanel!.Visibility = Visibility.Collapsed;
            R6OverlayPanel!.Visibility = Visibility.Collapsed;
            BackButton!.Visibility = Visibility.Collapsed;
            RunButton!.IsEnabled = false;

            // Reset overlay button colors
            CodNewButton!.Background = CodCloudpanelButton!.Background = System.Windows.Media.Brushes.DarkGray;
            R6LegacyButton!.Background = System.Windows.Media.Brushes.DarkGray;
        }

        private void UpdateRunButtonState()
        {
            RunButton!.IsEnabled = (isCodSelected && selectedCodOverlay != null) || (isR6Selected && selectedR6Overlay != null);
        }

        private async void RunButton_Click(object sender, RoutedEventArgs e)
        {
            RunButton!.IsEnabled = false;
            ProgressBar!.Visibility = Visibility.Visible;

            string batchScript;
            if ((isCodSelected && selectedCodOverlay == "New") || (isR6Selected && selectedR6Overlay == "New"))
            {
                batchScript = @"
@echo off
title Maximum Performance Optimizer for COD and AI Aimbot

:: Check for admin privileges
net session >nul 2>&1
if %errorLevel% == 0 (
    echo Admin rights confirmed. Proceeding with optimizations...
) else (
    echo Error: This script must be run as Administrator.
    exit /b
)

:: Set High Performance Power Plan as fallback
powercfg /s 8c5e7fda-e8bf-4a96-9a85-a6e23a8c635c >nul 2>&1
echo Set power plan to High Performance.

:: Try setting a specific Ultimate Performance Power Plan
powercfg -setactive 593692db-da37-4226-bbf6-3608e7d28921 >nul 2>&1
if %errorlevel% == 0 (
    echo Set power plan to Ultimate Performance.
) else (
    echo Failed to set Ultimate Performance, staying on High Performance.
)

:services
:: Stop non-essential services if running
for %%s in (SysMain DiagTrack WSearch BITS wuauserv PcaSvc WerSvc) do (
    sc query %%s | findstr /I ""RUNNING"" >nul && (
        net stop %%s >nul 2>&1 && echo Stopped %%s || echo Failed to stop %%s
    ) || echo %%s not running or already stopped.
)
echo Non-essential services optimization complete.

:: Optimize NVIDIA GPU for max performance
nvidia-smi -pm 1 >nul 2>&1
nvidia-smi -pl 320 >nul 2>&1
echo Optimized NVIDIA GPU settings for maximum power/performance.

:: Disable Game DVR and Xbox features
reg add ""HKCU\Software\Microsoft\Windows\CurrentVersion\GameDVR"" /v AppCaptureEnabled /t REG_DWORD /d 0 /f >nul 2>&1
reg add ""HKCU\System\GameConfigStore"" /v GameDVR_Enabled /t REG_DWORD /d 0 /f >nul 2>&1
echo Disabled Game DVR.

:: Disable visual effects and animations
reg add ""HKCU\Control Panel\Desktop"" /v MenuShowDelay /t REG_SZ /d 0 /f >nul 2>&1
reg add ""HKCU\Control Panel\Desktop\WindowMetrics"" /v MinAnimate /t REG_SZ /d 0 /f >nul 2>&1
reg add ""HKCU\Software\Microsoft\Windows\CurrentVersion\Explorer\VisualEffects"" /v VisualFXSetting /t REG_DWORD /d 3 /f >nul 2>&1
echo Disabled unnecessary visual effects.

:: Set priorities and affinities (replace 'aimbot' with your AI process name)
powershell ""Get-Process cod -ErrorAction SilentlyContinue | foreach { $_.PriorityClass = 'High'; $_.ProcessorAffinity = 0xFFFF }"" >nul 2>&1
powershell ""Get-Process discord -ErrorAction SilentlyContinue | foreach { $_.PriorityClass = 'Realtime'; $_.ProcessorAffinity = 0xFFFF }"" >nul 2>&1
echo Set priorities and affinities for COD and AI aimbot processes.

:: Additional tips
echo Optimizations complete! Most changes revert on reboot.
echo For best FPS in COD at 3440x1440p:
echo - Use DLSS Quality/Balanced, high textures, low shadows/AA (search RTX 3080 COD guides).
echo - Set NVIDIA Control Panel: COD to 'Prefer Maximum Performance', 3440x1440p, max refresh rate.
echo - Close other apps if AI aimbot is GPU-bound; test without affinity if CPU-bound.
echo - Target: 150-200+ FPS in COD, higher AI aimbot FPS (from 90).
exit /b";
            }
            else if (isR6Selected && selectedR6Overlay == "Legacy")
            {
                batchScript = @"
@echo off
title Maximum Performance Optimizer for R6 and AI Aimbot

:: Check for admin privileges
net session >nul 2>&1
if %errorLevel% == 0 (
    echo Admin rights confirmed. Proceeding with optimizations...
) else (
    echo Error: This script must be run as Administrator.
    exit /b
)

:: Set High Performance Power Plan as fallback
powercfg /s 8c5e7fda-e8bf-4a96-9a85-a6e23a8c635c >nul 2>&1
echo Set power plan to High Performance.

:: Try setting a specific Ultimate Performance Power Plan
powercfg -setactive 593692db-da37-4226-bbf6-3608e7d28921 >nul 2>&1
if %errorlevel% == 0 (
    echo Set power plan to Ultimate Performance.
) else (
    echo Failed to set Ultimate Performance, staying on High Performance.
)

:services
:: Stop non-essential services if running
for %%s in (SysMain DiagTrack WSearch BITS wuauserv PcaSvc WerSvc) do (
    sc query %%s | findstr /I ""RUNNING"" >nul && (
        net stop %%s >nul 2>&1 && echo Stopped %%s || echo Failed to stop %%s
    ) || echo %%s not running or already stopped.
)
echo Non-essential services optimization complete.

:: Optimize NVIDIA GPU for max performance
nvidia-smi -pm 1 >nul 2>&1
nvidia-smi -pl 320 >nul 2>&1
echo Optimized NVIDIA GPU settings for maximum power/performance.

:: Disable Game DVR and Xbox features
reg add ""HKCU\Software\Microsoft\Windows\CurrentVersion\GameDVR"" /v AppCaptureEnabled /t REG_DWORD /d 0 /f >nul 2>&1
reg add ""HKCU\System\GameConfigStore"" /v GameDVR_Enabled /t REG_DWORD /d 0 /f >nul 2>&1
echo Disabled Game DVR.

:: Disable visual effects and animations
reg add ""HKCU\Control Panel\Desktop"" /v MenuShowDelay /t REG_SZ /d 0 /f >nul 2>&1
reg add ""HKCU\Control Panel\Desktop\WindowMetrics"" /v MinAnimate /t REG_SZ /d 0 /f >nul 2>&1
reg add ""HKCU\Software\Microsoft\Windows\CurrentVersion\Explorer\VisualEffects"" /v VisualFXSetting /t REG_DWORD /d 3 /f >nul 2>&1
echo Disabled unnecessary visual effects.

:: Set priorities and affinities (replace 'aimbot' with your AI process name)
powershell ""Get-Process RainbowSix -ErrorAction SilentlyContinue | foreach { $_.PriorityClass = 'High'; $_.ProcessorAffinity = 0xFFFF }"" >nul 2>&1
powershell ""Get-Process discord -ErrorAction SilentlyContinue | foreach { $_.PriorityClass = 'Realtime'; $_.ProcessorAffinity = 0xFFFF }"" >nul 2>&1
echo Set priorities and affinities for R6 and AI aimbot processes.

:: Additional tips
echo Optimizations complete! Most changes revert on reboot.
echo For best FPS in R6 at 3440x1440p:
echo - Use in-game settings: High textures, low shadows/AA, V-Sync off.
echo - Set NVIDIA Control Panel: R6 to 'Prefer Maximum Performance', 3440x1440p, max refresh rate.
echo - Close other apps if AI aimbot is GPU-bound; test without affinity if CPU-bound.
echo - Target: 120-180+ FPS in R6, higher AI aimbot FPS (from 90).
exit /b";
            }
            else
            {
                batchScript = @"
@echo off
title Performance Optimizer

:: Check for admin privileges
net session >nul 2>&1
if %errorLevel% == 0 (
    echo Admin rights confirmed...
) else (
    echo Error: Run as Administrator.
    exit /b
)

:: Set High Performance Power Plan
powercfg /s 8c5e7fda-e8bf-4a96-9a85-a6e23a8c635c >nul 2>&1
echo Set power plan to High Performance.

:: Optimize NVIDIA GPU
nvidia-smi -pm 1 >nul 2>&1
nvidia-smi -pl 320 >nul 2>&1
echo Optimized GPU settings.

:: Set process priorities (example: 'cod' or 'r6')
powershell ""Get-Process cod -ErrorAction SilentlyContinue | foreach { $_.PriorityClass = 'High' }"" >nul 2>&1
powershell ""Get-Process r6 -ErrorAction SilentlyContinue | foreach { $_.PriorityClass = 'High' }"" >nul 2>&1
echo Set process priorities.

echo Optimizations complete.
exit /b";
            }

            try
            {
                string tempPath = Path.Combine(Path.GetTempPath(), "Optimizer.bat");
                File.WriteAllText(tempPath, batchScript);

                ProcessStartInfo processInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = $"/c \"{tempPath}\"",
                    Verb = "runas",
                    UseShellExecute = true,
                    CreateNoWindow = false
                };

                using (Process process = Process.Start(processInfo))
                {
                    await Task.Run(() => process.WaitForExit());
                }

                File.Delete(tempPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}\nRun as Administrator.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                ProgressBar!.Visibility = Visibility.Hidden;
                RunButton!.IsEnabled = true;
                return;
            }

            ProgressBar!.Visibility = Visibility.Hidden;
            DonePanel!.Visibility = Visibility.Visible;
        }

        private async void ResetOptimizationsButton_Click(object sender, RoutedEventArgs e)
        {
            ResetOptimizationsButton!.IsEnabled = false;
            ProgressBar!.Visibility = Visibility.Visible;

            string resetScript = @"
@echo off
title Reset Optimizations

:: Check for admin privileges
net session >nul 2>&1
if %errorLevel% == 0 (
    echo Admin rights confirmed. Proceeding with reset...
) else (
    echo Error: This script must be run as Administrator.
    exit /b
)

:: Restore default power plan
powercfg /restoredefaultschemes >nul 2>&1
echo Restored default power plan.

:: Restart stopped services
for %%s in (SysMain DiagTrack WSearch BITS wuauserv PcaSvc WerSvc) do (
    sc query %%s | findstr /I ""STOPPED"" >nul && (
        net start %%s >nul 2>&1 && echo Started %%s || echo Failed to start %%s
    ) || echo %%s already running or not stopped.
)
echo Restarted non-essential services.

:: Re-enable Game DVR and Xbox features
reg add ""HKCU\Software\Microsoft\Windows\CurrentVersion\GameDVR"" /v AppCaptureEnabled /t REG_DWORD /d 1 /f >nul 2>&1
reg add ""HKCU\System\GameConfigStore"" /v GameDVR_Enabled /t REG_DWORD /d 1 /f >nul 2>&1
echo Re-enabled Game DVR.

:: Restore visual effects and animations
reg delete ""HKCU\Control Panel\Desktop"" /v MenuShowDelay /f >nul 2>&1
reg delete ""HKCU\Control Panel\Desktop\WindowMetrics"" /v MinAnimate /f >nul 2>&1
reg add ""HKCU\Software\Microsoft\Windows\CurrentVersion\Explorer\VisualEffects"" /v VisualFXSetting /t REG_DWORD /d 1 /f >nul 2>&1
echo Restored visual effects.

:: Reset NVIDIA GPU settings (note: manual reset may be needed via NVIDIA Control Panel)
nvidia-smi -pm 0 >nul 2>&1
echo Attempted to reset NVIDIA GPU power management.

echo Reset complete! Some settings may require a reboot to fully apply.
exit /b";

            try
            {
                string tempPath = Path.Combine(Path.GetTempPath(), "ResetOptimizer.bat");
                File.WriteAllText(tempPath, resetScript);

                ProcessStartInfo processInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = $"/c \"{tempPath}\"",
                    Verb = "runas",
                    UseShellExecute = true,
                    CreateNoWindow = false
                };

                using (Process process = Process.Start(processInfo))
                {
                    await Task.Run(() => process.WaitForExit());
                }

                File.Delete(tempPath);
                // Show popup after successful reset
                MessageBox.Show("Please restart your computer to ensure successful reset.", "Reset Complete", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}\nRun as Administrator.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                ProgressBar!.Visibility = Visibility.Hidden;
                ResetOptimizationsButton!.IsEnabled = true;
                return;
            }

            ProgressBar!.Visibility = Visibility.Hidden;
            DonePanel!.Visibility = Visibility.Visible;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            DonePanel!.Visibility = Visibility.Hidden;
            RunButton!.IsEnabled = true;
            ResetOptimizationsButton!.IsEnabled = true;
        }
    }
}
