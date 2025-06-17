# toSrgb

**toSrgb** is a simple, user-friendly desktop tool for converting JPEG images from Adobe RGB (or other color spaces) to sRGB, with flexible resizing and quality options. It is designed for photographers, designers, and anyone who needs to ensure their images display correctly on the web or in standard software.

## What does it do?
- Converts JPEG images to the sRGB color space for maximum compatibility.
- Offers three easy quality presets: Compact, Balanced, and Detailed, with clear guidance for each use case.
- **Lets you resize images to original, 800x800, 1600x1600, or 3200x3200 pixels (maintaining aspect ratio).**
- Cleans up filenames and organizes your converted images into a dedicated folder on your Desktop, with timestamp and settings in the folder name.
- Handles image metadata automatically: preserves it for high-quality output, strips it for smaller files.
- Processes multiple images at once with robust error handling and clear feedback.

## Who is it for?
- Anyone who needs to convert images to sRGB for web, printing, or sharing.
- Users who want a no-fuss, reliable batch converter with sensible defaults.

## How does it work?
- Run the app from the command line:
  ```
  toSrgbApp [--resize o|8|16|32] [--quality NN] <image1> [image2 ...]
  ```
- If you omit `--resize` or `--quality`, the app will prompt you interactively.
- The app takes care of color conversion, resizing, file naming, and output organization.
- Your converted images are ready to use, with no technical knowledge required.

---

**toSrgb** is free and open source. For more details, see the included specifications document.