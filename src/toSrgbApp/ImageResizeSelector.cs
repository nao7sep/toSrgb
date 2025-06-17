namespace toSrgbApp
{
    /// <summary>
    /// Provides options for resizing images to fit within a bounding box.
    /// </summary>
    public class ImageResizeSelector
    {
        public enum ResizeMode
        {
            Original,
            Fit800,
            Fit1600,
            Fit3200
        }

        public static ResizeMode PromptForResizeMode()
        {
            Console.WriteLine("Select image resize option:");
            Console.WriteLine("o - Original size (no resize)");
            Console.WriteLine("8 - Fit within 800x800");
            Console.WriteLine("16 - Fit within 1600x1600");
            Console.WriteLine("32 - Fit within 3200x3200");
            Console.Write("Enter choice (o/8/16/32): ");
            var input = Console.ReadLine()?.Trim().ToLower();
            switch (input)
            {
                case "o":
                    return ResizeMode.Original;
                case "8":
                    return ResizeMode.Fit800;
                case "16":
                    return ResizeMode.Fit1600;
                case "32":
                    return ResizeMode.Fit3200;
                default:
                    Console.WriteLine("Invalid input. Please try again.");
                    return PromptForResizeMode();
            }
        }

        public static (int width, int height)? GetBoxSize(ResizeMode mode)
        {
            return mode switch
            {
                ResizeMode.Fit800 => (800, 800),
                ResizeMode.Fit1600 => (1600, 1600),
                ResizeMode.Fit3200 => (3200, 3200),
                _ => null
            };
        }
    }
}
