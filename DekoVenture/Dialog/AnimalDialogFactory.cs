namespace DekoVenture
{
    public class AnimalDialogFactory
    {
        public static string GetRandomAnimalNoise()
        {
            string[] noises =  //chatGpt generated list, and I editied it a bit to be more fitting for the game, and less repetitive.
            {
                "a low growl through the darkness.",
                "chittering sounds.",
                "an echoing wet snarl.",
                "a claw scrape.",
                "a rising howl, then cuts off suddenly.",
                "a sharp screech tears through the silence.",
                "a broken, rasping cry.",
                "a deep hiss seeps from the shadows.",
                "a loud gnawing.",
                "a guttural bark.",
                "a long, mournful wail.",
                "a shrill cry, almost like laughter.",
                "a rattling wheeze.",
                "a sudden snort.",
                "a deep croak.",
                "a snarl, then goes completely quiet.",
                "strange animal cries that echoe like a warning."
                };

            int index = Random.Shared.Next(noises.Length);
            return noises[index];
        }
    }
}
