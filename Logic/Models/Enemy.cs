namespace Logic.Models
{
    public class Enemy : DynamicObject
    {
        public Enemy(int xPosition, int yPosition) : base(xPosition, yPosition, "E", 1, false, 0)
        {
        }

        public override int Priority => 1;

        public override void Tick()
        {
            YPosition--;
            base.Tick();
           
        }
    }
}
