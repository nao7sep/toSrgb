namespace toSrgbApp
{
    /// <summary>
    /// Parses command line arguments for the toSrgbApp application.
    /// </summary>
    public class CommandLineOptions
    {
        public int? JpegQuality { get; set; }
        public ImageResizeSelector.ResizeMode? ResizeMode { get; set; } // Add resize mode
        public List<string> ImagePaths { get; } = [];

        public static CommandLineOptions Parse(string[] args)
        {
            var options = new CommandLineOptions();
            for (int i = 0; i < args.Length; i++)
            {
                string arg = args[i];
                if (string.Equals(arg, "--quality", StringComparison.OrdinalIgnoreCase))
                {
                    if (i + 1 >= args.Length)
                    {
                        throw new ArgumentException("Missing value for --quality parameter.");
                    }
                    string value = args[++i];
                    if (int.TryParse(value, out int q) && q >= 1 && q <= 100)
                    {
                        options.JpegQuality = q;
                    }
                    else
                    {
                        throw new ArgumentException("Invalid JPEG quality. Must be 1-100.");
                    }
                }
                else if (string.Equals(arg, "--resize", StringComparison.OrdinalIgnoreCase))
                {
                    if (i + 1 >= args.Length)
                    {
                        throw new ArgumentException("Missing value for --resize parameter.");
                    }
                    string value = args[++i].ToLower();
                    options.ResizeMode = value switch
                    {
                        "o" or "original" => ImageResizeSelector.ResizeMode.Original,
                        "8" or "800" => ImageResizeSelector.ResizeMode.Fit800,
                        "16" or "1600" => ImageResizeSelector.ResizeMode.Fit1600,
                        "32" or "3200" => ImageResizeSelector.ResizeMode.Fit3200,
                        _ => throw new ArgumentException("Invalid resize mode. Use o, 8, 16, or 32.")
                    };
                }
                else
                {
                    options.ImagePaths.Add(arg);
                }
            }
            return options;
        }
    }
}
