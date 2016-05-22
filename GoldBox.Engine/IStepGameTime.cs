namespace GoldBox.Engine
{
    public interface IStepGameTime
    {
        void step_game_time(int time_slot, int amount);
    }

    internal class StepGameTime : IStepGameTime
    {
        public void step_game_time(int time_slot, int amount)
        {
            ovr021.step_game_time(time_slot, amount);
        }
    }
}
