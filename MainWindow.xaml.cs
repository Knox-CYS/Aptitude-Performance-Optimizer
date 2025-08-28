using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Threading;

namespace GameBatchExecutor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Dictionary to store batch script contents directly
        private readonly Dictionary<string, string> batchScripts = new Dictionary<string, string>
        {
            { "R6PerformanceOptimizerV5-Legacy.bat", @"
@echo off
title Maximum Performance Optimizer for R6 and AI Aimbot

:: Check for admin privileges
net session >nul 2>&1
if %errorLevel% == 0 (
    echo Admin rights confirmed. Proceeding with optimizations...
) else (
    echo Error: This script must be run as Administrator.
    pause
    exit
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

:: Instructions for user
echo.
echo IMPORTANT: Run this script AFTER starting Call of Duty and your AI aimbot.
echo Edit process names below (e.g., ModernWarfare.exe for COD, youraimbot.exe for AI).
echo Current names: 'cod' for COD, 'opera' for AI aimbot.
echo Check Task Manager for exact names (without .exe) and edit script if needed.
echo Press any key to set process priorities and affinities...
pause >nul

:: Set priorities and affinities (replace 'aimbot' with your AI process name)
powershell ""Get-Process rainbowsix -ErrorAction SilentlyContinue | foreach { $_.PriorityClass = 'High'; $_.ProcessorAffinity = 0xFFFF }"" >nul 2>&1
powershell ""Get-Process discord -ErrorAction SilentlyContinue | foreach { $_.PriorityClass = 'Realtime'; $_.ProcessorAffinity = 0xFFFF }"" >nul 2>&1
echo Set priorities and affinities for COD and AI aimbot processes.

:: Additional tips
echo.
echo Optimizations complete! Most changes revert on reboot.
echo For best FPS in COD at 3440x1440p:
echo - Use DLSS Quality/Balanced, high textures, low shadows/AA (search RTX 3080 COD guides).
echo - Set NVIDIA Control Panel: COD to 'Prefer Maximum Performance', 3440x1440p, max refresh rate.
echo - Close other apps if AI aimbot is GPU-bound; test without affinity if CPU-bound.
echo - Target: 150-200+ FPS in COD, higher AI aimbot FPS (from 90).
echo Press any key to exit...
pause >nul
" },
            { "Cloudpanel-Chrome.bat", @"
@echo off
title Maximum Performance Optimizer for COD and AI Aimbot

:: Check for admin privileges
net session >nul 2>&1
if %errorLevel% == 0 (
    echo Admin rights confirmed. Proceeding with optimizations...
) else (
    echo Error: This script must be run as Administrator.
    pause
    exit
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

:: Instructions for user
echo.
echo IMPORTANT: Run this script AFTER starting Call of Duty and your AI aimbot.
echo Edit process names below (e.g., ModernWarfare.exe for COD, youraimbot.exe for AI).
echo Current names: 'cod' for COD, 'opera' for AI aimbot.
echo Check Task Manager for exact names (without .exe) and edit script if needed.
echo Press any key to set process priorities and affinities...

:: Set priorities and affinities (replace 'aimbot' with your AI process name)
powershell ""Get-Process cod -ErrorAction SilentlyContinue | foreach { $_.PriorityClass = 'High'; $_.ProcessorAffinity = 0xFFFF }"" >nul 2>&1
powershell ""Get-Process chrome -ErrorAction SilentlyContinue | foreach { $_.PriorityClass = 'Realtime'; $_.ProcessorAffinity = 0xFFFF }"" >nul 2>&1
echo Set priorities and affinities for COD and AI aimbot processes.

:: Additional tips
echo.
echo Optimizations complete! Most changes revert on reboot.
echo For best FPS in COD at 3440x1440p:
echo - Use DLSS Quality/Balanced, high textures, low shadows/AA (search RTX 3080 COD guides).
echo - Set NVIDIA Control Panel: COD to 'Prefer Maximum Performance', 3440x1440p, max refresh rate.
echo - Close other apps if AI aimbot is GPU-bound; test without affinity if CPU-bound.
echo - Target: 150-200+ FPS in COD, higher AI aimbot FPS (from 90).
echo Press any key to exit...
pause >nul
" },
            { "CODPerformanceOptimizerV5-Cloudpanel.bat", @"
@echo off
title Maximum Performance Optimizer for COD and AI Aimbot

:: Check for admin privileges
net session >nul 2>&1
if %errorLevel% == 0 (
    echo Admin rights confirmed. Proceeding with optimizations...
) else (
    echo Error: This script must be run as Administrator.
    pause
    exit
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

:: Instructions for user
echo.
echo IMPORTANT: Run this script AFTER starting Call of Duty and your AI aimbot.
echo Edit process names below (e.g., ModernWarfare.exe for COD, youraimbot.exe for AI).
echo Current names: 'cod' for COD, 'opera' for AI aimbot.
echo Check Task Manager for exact names (without .exe) and edit script if needed.
echo Press any key to set process priorities and affinities...

:: Set priorities and affinities (replace 'aimbot' with your AI process name)
powershell ""Get-Process cod -ErrorAction SilentlyContinue | foreach { $_.PriorityClass = 'High'; $_.ProcessorAffinity = 0xFFFF }"" >nul 2>&1
powershell ""Get-Process opera -ErrorAction SilentlyContinue | foreach { $_.PriorityClass = 'Realtime'; $_.ProcessorAffinity = 0xFFFF }"" >nul 2>&1
echo Set priorities and affinities for COD and AI aimbot processes.

:: Additional tips
echo.
echo Optimizations complete! Most changes revert on reboot.
echo For best FPS in COD at 3440x1440p:
echo - Use DLSS Quality/Balanced, high textures, low shadows/AA (search RTX 3080 COD guides).
echo - Set NVIDIA Control Panel: COD to 'Prefer Maximum Performance', 3440x1440p, max refresh rate.
echo - Close other apps if AI aimbot is GPU-bound; test without affinity if CPU-bound.
echo - Target: 150-200+ FPS in COD, higher AI aimbot FPS (from 90).
echo Press any key to exit...
pause >nul
" },
            { "CODPerformanceOptimizerV5-NewOverlay.bat", @"
@echo off
title Maximum Performance Optimizer for COD and AI Aimbot

:: Check for admin privileges
net session >nul 2>&1
if %errorLevel% == 0 (
    echo Admin rights confirmed. Proceeding with optimizations...
) else (
    echo Error: This script must be run as Administrator.
    pause
    exit
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

:: Instructions for user
echo.
echo IMPORTANT: Run this script AFTER starting Call of Duty and your AI aimbot.
echo Edit process names below (e.g., ModernWarfare.exe for COD, youraimbot.exe for AI).
echo Current names: 'cod' for COD, 'opera' for AI aimbot.
echo Check Task Manager for exact names (without .exe) and edit script if needed.
echo Press any key to set process priorities and affinities...

:: Set priorities and affinities (replace 'aimbot' with your AI process name)
powershell ""Get-Process cod -ErrorAction SilentlyContinue | foreach { $_.PriorityClass = 'High'; $_.ProcessorAffinity = 0xFFFF }"" >nul 2>&1
powershell ""Get-Process discord -ErrorAction SilentlyContinue | foreach { $_.PriorityClass = 'Realtime'; $_.ProcessorAffinity = 0xFFFF }"" >nul 2>&1
echo Set priorities and affinities for COD and AI aimbot processes.

:: Additional tips
echo.
echo Optimizations complete! Most changes revert on reboot.
echo For best FPS in COD at 3440x1440p:
echo - Use DLSS Quality/Balanced, high textures, low shadows/AA.
echo - Set NVIDIA Control Panel: COD to 'Prefer Maximum Performance', max refresh rate.
echo - Close other apps if AI aimbot is GPU-bound; test without affinity if CPU-bound.
echo - Target: 150-200+ FPS in COD, higher AI aimbot FPS (from 90).
echo Press any key to exit...
pause >nul
" },
            { "Reset.bat", @"
REM Paste your Reset.bat content here
@echo off
echo Running Reset Script...
:: Add your batch commands here
pause
" }
        };

        private string? selectedBatchResource = null;
        private string? selectedBrowser = null;

        private Random random = new Random();
        private DispatcherTimer particleTimer = null!;
        private List<System.Windows.Shapes.Line> particleLines = new List<System.Windows.Shapes.Line>();

        public MainWindow()
        {
            // Check if running as admin, if not, restart with admin privileges
            if (!IsRunningAsAdmin())
            {
                RestartAsAdmin();
                return;
            }

            InitializeComponent();
            // Initial setup
            R6SOptions.Visibility = Visibility.Collapsed;
            CODOptions.Visibility = Visibility.Collapsed;
            BrowserOptions.Visibility = Visibility.Collapsed;

            // Setup particles
            SetupParticles();
        }

        private bool IsRunningAsAdmin()
        {
            return new WindowsPrincipal(WindowsIdentity.GetCurrent())
                .IsInRole(WindowsBuiltInRole.Administrator);
        }

        private void RestartAsAdmin()
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = Process.GetCurrentProcess().MainModule.FileName,
                    UseShellExecute = true,
                    Verb = "runas" // Requests admin privileges
                };
                Process.Start(startInfo);
                Application.Current.Shutdown(); // Close current instance
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to restart as admin: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SetupParticles()
        {
            particleTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(50) };
            particleTimer.Tick += (s, e) => UpdateParticles();
            particleTimer.Start();

            // Create initial particles
            for (int i = 0; i < 35; i++)
            {
                CreateParticle();
            }
        }

        private void CreateParticle()
        {
            // Diamond shape using Polygon
            var particle = new System.Windows.Shapes.Polygon
            {
                Points = new PointCollection
                {
                    new Point(0, 5),    // Top
                    new Point(5, 0),    // Right
                    new Point(10, 5),   // Bottom
                    new Point(5, 10)    // Left
                },
                Fill = new SolidColorBrush(Color.FromRgb((byte)random.Next(128, 255), (byte)random.Next(0, 50), (byte)random.Next(128, 255))),
                Opacity = 1.0, // Always visible
                Stretch = Stretch.Fill,
                Width = random.Next(6, 13),
                Height = random.Next(6, 13)
            };

            // Add futuristic glow effect
            particle.Effect = new DropShadowEffect
            {
                Color = Colors.Purple,
                Direction = 0,
                ShadowDepth = 0,
                BlurRadius = 10,
                Opacity = 0.8
            };

            Canvas.SetLeft(particle, random.NextDouble() * ParticleCanvas.ActualWidth);
            Canvas.SetTop(particle, random.NextDouble() * ParticleCanvas.ActualHeight);

            // Random velocity
            particle.Tag = new Point(random.NextDouble() * 2 - 1, random.NextDouble() * 2 - 1);

            // Scale animation for futuristic feel (no fade)
            var scaleAnim = new DoubleAnimation(0.8, 1.2, TimeSpan.FromSeconds(random.Next(1, 3)));
            scaleAnim.AutoReverse = true;
            scaleAnim.RepeatBehavior = RepeatBehavior.Forever;
            var transform = new ScaleTransform();
            particle.RenderTransform = transform;
            transform.BeginAnimation(ScaleTransform.ScaleXProperty, scaleAnim);
            transform.BeginAnimation(ScaleTransform.ScaleYProperty, scaleAnim);

            ParticleCanvas.Children.Add(particle);
        }

        private void UpdateParticles()
        {
            if (ParticleCanvas.Children.OfType<System.Windows.Shapes.Polygon>().Count() < 20)
            {
                CreateParticle();
            }

            // Clear existing lines
            foreach (var line in particleLines)
            {
                ParticleCanvas.Children.Remove(line);
            }
            particleLines.Clear();

            // Get mouse position
            var mousePos = Mouse.GetPosition(ParticleCanvas);

            // Collect particles first to avoid collection modified exception
            var particles = ParticleCanvas.Children.OfType<System.Windows.Shapes.Polygon>().ToList();

            foreach (var particle in particles)
            {
                var posX = Canvas.GetLeft(particle);
                var posY = Canvas.GetTop(particle);
                var velocity = (Point)(particle.Tag ?? new Point(0, 0));

                posX += velocity.X;
                posY += velocity.Y;

                if (posX < 0 || posX > ParticleCanvas.ActualWidth) velocity.X = -velocity.X;
                if (posY < 0 || posY > ParticleCanvas.ActualHeight) velocity.Y = -velocity.Y;

                particle.Tag = velocity;
                Canvas.SetLeft(particle, posX);
                Canvas.SetTop(particle, posY);

                // Draw line if mouse is near (increased distance)
                var particleCenter = new Point(posX + particle.Width / 2, posY + particle.Height / 2);
                if ((particleCenter - mousePos).Length < 100)
                {
                    var line = new System.Windows.Shapes.Line
                    {
                        X1 = particleCenter.X,
                        Y1 = particleCenter.Y,
                        X2 = mousePos.X,
                        Y2 = mousePos.Y,
                        Stroke = new SolidColorBrush(Color.FromArgb(200, (byte)random.Next(256), (byte)random.Next(256), (byte)random.Next(256))),
                        StrokeThickness = 1,
                        Opacity = 0.8
                    };
                    // Add some "3D" effect with a shadow line
                    var shadowLine = new System.Windows.Shapes.Line
                    {
                        X1 = particleCenter.X + 2,
                        Y1 = particleCenter.Y + 2,
                        X2 = mousePos.X + 2,
                        Y2 = mousePos.Y + 2,
                        Stroke = Brushes.Black,
                        StrokeThickness = 1,
                        Opacity = 0.3
                    };
                    ParticleCanvas.Children.Add(shadowLine);
                    ParticleCanvas.Children.Add(line);
                    particleLines.Add(shadowLine);
                    particleLines.Add(line);
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Animate rainbow border
            AnimateRainbowBorder();
        }

        private void AnimateRainbowBorder()
        {
            var rainbowGradient = new LinearGradientBrush
            {
                StartPoint = new Point(0, 0),
                EndPoint = new Point(1, 1),
                GradientStops = new GradientStopCollection
                {
                    new GradientStop(Colors.Red, 0),
                    new GradientStop(Colors.Orange, 0.17),
                    new GradientStop(Colors.Yellow, 0.33),
                    new GradientStop(Colors.Green, 0.5),
                    new GradientStop(Colors.Blue, 0.67),
                    new GradientStop(Colors.Indigo, 0.83),
                    new GradientStop(Colors.Violet, 1)
                }
            };

            MainBorder.BorderBrush = rainbowGradient;

            // For simplicity, animate the entire brush transform
            var rotateTransform = new RotateTransform();
            rainbowGradient.Transform = rotateTransform;
            rotateTransform.BeginAnimation(RotateTransform.AngleProperty, new DoubleAnimation(0, 360, TimeSpan.FromSeconds(20)) { RepeatBehavior = RepeatBehavior.Forever });
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void GameSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GameSelector.SelectedItem is ComboBoxItem selected)
            {
                string game = selected.Content.ToString();
                selectedBatchResource = null;
                selectedBrowser = null;
                BrowserOptions.Visibility = Visibility.Collapsed;
                if (game == "Rainbow Six Siege")
                {
                    R6SOptions.Visibility = Visibility.Visible;
                    CODOptions.Visibility = Visibility.Collapsed;
                    LegacyRadio.IsChecked = false;
                }
                else if (game == "Call of Duty")
                {
                    R6SOptions.Visibility = Visibility.Collapsed;
                    CODOptions.Visibility = Visibility.Visible;
                    CloudPanelRadio.IsChecked = false;
                    NewOverlayRadio.IsChecked = false;
                }
                else
                {
                    R6SOptions.Visibility = Visibility.Collapsed;
                    CODOptions.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void LegacyRadio_Checked(object sender, RoutedEventArgs e)
        {
            selectedBatchResource = "R6PerformanceOptimizerV5-Legacy.bat";
            BrowserOptions.Visibility = Visibility.Collapsed;
        }

        private void CloudPanelRadio_Checked(object sender, RoutedEventArgs e)
        {
            selectedBatchResource = null; // Will set based on browser
            BrowserOptions.Visibility = Visibility.Visible;
        }

        private void NewOverlayRadio_Checked(object sender, RoutedEventArgs e)
        {
            selectedBatchResource = "CODPerformanceOptimizerV5-NewOverlay.bat";
            BrowserOptions.Visibility = Visibility.Collapsed;
        }

        private void BrowserSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BrowserSelector.SelectedItem is ComboBoxItem selected)
            {
                selectedBrowser = selected.Content.ToString();
                if (selectedBrowser == "Chrome")
                {
                    selectedBatchResource = "Cloudpanel-Chrome.bat";
                }
                else if (selectedBrowser == "Opera GX")
                {
                    selectedBatchResource = "CODPerformanceOptimizerV5-Cloudpanel.bat";
                }
            }
        }

        private void RunButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(selectedBatchResource))
            {
                ExecuteBatchScript(selectedBatchResource);
            }
            else
            {
                StatusText.Text = "Please select an option.";
                StatusText.Foreground = Brushes.Yellow;
            }
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            ExecuteBatchScript("Reset.bat");
        }

        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("KNX Optimizer v2.0\n\nThis application allows you to execute optimization scripts for Rainbow Six Siege and Call of Duty.\n\nCreated with WPF in C#.\n\nFor support, contact @cor0724");
        }

        private void ExecuteBatchScript(string scriptName)
        {
            if (string.IsNullOrEmpty(scriptName) || !batchScripts.ContainsKey(scriptName))
            {
                StatusText.Text = "Error: Batch script not found.";
                StatusText.Foreground = Brushes.Red;
                return;
            }

            try
            {
                string tempPath = Path.Combine(Path.GetTempPath(), scriptName);
                File.WriteAllText(tempPath, batchScripts[scriptName]);

                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = tempPath,
                    UseShellExecute = true,
                    Verb = "runas" // Run batch file as admin
                };

                Process.Start(startInfo);

                StatusText.Text = "Optimizations Successfull!";
                StatusText.Foreground = Brushes.Green;
            }
            catch (Exception ex)
            {
                StatusText.Text = $"Error: {ex.Message}";
                StatusText.Foreground = Brushes.Red;
            }
        }
    }
}
