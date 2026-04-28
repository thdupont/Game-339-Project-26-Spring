namespace Game339.Tests;

public class TestStuff
{
    public abstract class Animal
    {
        public abstract void Speak();

        public virtual void SpeakX()
        {
            Console.WriteLine("I must scream but I have no implementation");
        }

        public bool IsAlive()
        {
            return true;
        }
    }

    public class Dog : Animal
    {
        public override void Speak()
        {
            Console.WriteLine("Woof!");
        }
    }

    public class ShibaInu : Dog
    {
        public override void Speak()
        {
            Console.WriteLine("Meow");
            base.Speak();
        }
    }
}
