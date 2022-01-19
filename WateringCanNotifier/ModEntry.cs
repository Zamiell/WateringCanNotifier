using System;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;

namespace YourProjectName
{
    public class ModEntry : Mod
    {
        private int WateringCanWaterOnLastFrame = 70; // Max energy
        public override void Entry(IModHelper helper)
        {
            helper.Events.GameLoop.UpdateTicked += this.OnUpdateTicked;
        }


        private void OnUpdateTicked(object sender, UpdateTickedEventArgs e)
        {
            if (!Context.IsWorldReady)
            {
                return;
            }

            CheckWateringCan();
        }

        private void CheckWateringCan()
        {
            if (!(Game1.player.CurrentTool is StardewValley.Tools.WateringCan))
            {
                return;
            }

            var wateringCan = Game1.player.CurrentTool as StardewValley.Tools.WateringCan;
            if (wateringCan.WaterLeft != WateringCanWaterOnLastFrame)
            {
                WateringCanWaterChanged(wateringCan.WaterLeft);
            }
            WateringCanWaterOnLastFrame = wateringCan.WaterLeft;
        }

        private void WateringCanWaterChanged(int waterLeft)
        {
            if (waterLeft == 0)
            {
                Notify("Watering can out of water!");
            }
        }

        private void Notify(string msg)
        {
            Game1.chatBox.addMessage(msg, Color.Red);
        }

        private void Log(string msg)
        {
            this.Monitor.Log(msg, LogLevel.Debug);
        }
    }
}
