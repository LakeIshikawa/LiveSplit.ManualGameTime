using LiveSplit.Model;
using LiveSplit.UI.Components;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace LiveSplit.ManualGameTime.UI.Components
{
    public partial class ShitSplitter : Form
    {
        public ITimerModel Model { get; set; }
        protected RTAmTBTimeSettings Settings { get; set; }

        public bool PauseInProgress { get; set; }
        public TimeSpan PauseDuration { get; set; }

        public ShitSplitter(LiveSplitState state, RTAmTBTimeSettings settings)
        {
            InitializeComponent();
            Model = new TimerModel()
            {
                CurrentState = state
            };
            Settings = settings;
        }

        private void txtGameTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            var entryerror = 0;
            if (e.KeyChar == '\r' && txtGameTime.Text != "")
            {
                bool hasContinue = false;
                bool timeBonus = true;

                try
                {
                    // Parse IGT
                    var timeSpans = txtGameTime.Text.Replace(" ", "").Split('+');
                    var totalTime = TimeSpan.Zero;
                    var lastTime = TimeSpan.Zero;
                    foreach (var time in timeSpans)
                    {
                        var inputTime = time;
                        if (inputTime.EndsWith("c"))
                        {
                            inputTime = time.Substring(0, time.Length - 1);
                            hasContinue = true;
                        }

                        if(inputTime.EndsWith("-"))
                        {
                            inputTime = time.Substring(0, time.Length - 1);
                            timeBonus = false;
                        }

                        var t = TimeSpanParser.Parse(inputTime);
                        totalTime += t;
                        lastTime += t;
                    }

                    // Set IGT
                    Model.CurrentState.SetGameTime(totalTime + Model.CurrentState.CurrentTime.GameTime);
                    txtGameTime.Text = "";

                    if(Model.CurrentState.CurrentPhase == TimerPhase.Paused)
                    {
                        Model.Pause();
                    }
                    Model.Split();

                    // Wait for time bonus
                    if (timeBonus)
                    {

                        // Pause RTA timer for the countdown time
                        int points = -1;
                        // look up the time in our lookup table
                        foreach (int compms in Settings.IGTLookup.Keys)
                        {
                            if (lastTime.TotalMilliseconds < compms)
                            {
                                points = Settings.IGTLookup[compms];
                                break;
                            }
                        }

                        int frames = points / Settings.PointsPerFrame;   // yes, assigning to an int without a round; it's deliberate! We want it to round down.
                        if (hasContinue) frames += Settings.ContinueFrames;
                        double delaysecs = frames / Settings.FramesPerSecond;
                        int delayfor = (int)Math.Round(delaysecs * 1000);

                        Model.Pause();
                        PauseDuration += new TimeSpan(0, 0, 0, 0, delayfor);
                        PauseInProgress = true;
                    }
                }
                catch {
                    entryerror = 1;
                }

                if (entryerror == 1)
                {
                    // signal a probable error in the input
                    txtGameTime.BackColor = Color.MistyRose;
                    txtGameTime.ForeColor = Color.Black;
                }
                else
                {
                    txtGameTime.BackColor = SystemColors.Window;
                    txtGameTime.ForeColor = SystemColors.WindowText;
                    txtGameTime.Text = "";
                }
            }
        }
    }
}
