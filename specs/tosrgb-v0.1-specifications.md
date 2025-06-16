# toSrgb v0.1 - Technical Specifications

## 1. Product Overview

- **Product Name:** toSrgb
- **Version:** v0.1
- **Type:** Console Application
- **Platform:** .NET 9.0
- **License:** GPL-3.0
- **Author:** nao7sep
- **Company:** Purrfect Code

### 1.1 Purpose
toSrgb is a command-line utility designed to convert JPEG images from Adobe RGB color space to sRGB color space while providing intelligent JPEG quality selection based on practical use cases.

### 1.2 Key Features
- Color space conversion from Adobe RGB to sRGB
- Automatic ICC profile handling and removal
- Intelligent JPEG quality presets with clear use-case descriptions
- Batch processing of multiple images
- Automatic filename cleaning and organization
- Metadata preservation options
- Desktop output directory with timestamped folders

## 2. Technical Architecture

### 2.1 Framework and Dependencies
- **Target Framework:** .NET 9.0
- **Primary Dependency:** Magick.NET-Q16-AnyCPU v14.6.0 (ImageMagick wrapper)
- **Language Features:** C# with nullable reference types enabled, implicit usings

### 2.2 Project Structure
```
src/toSrgbApp/
├── Program.cs              # Main application entry point
├── CommandLineOptions.cs   # Command-line argument parser
├── ImageConverter.cs       # Core image processing logic
├── JpegQualitySelector.cs  # Interactive quality selection
└── toSrgbApp.csproj        # Project configuration
```

### 2.3 Core Classes

#### 2.3.1 Program Class
- **Namespace:** `toSrgbApp`
- **Purpose:** Main application orchestration and error handling
- **Key Responsibilities:**
  - Command-line argument processing
  - Quality selection workflow
  - Batch file processing
  - Output directory management
  - Error handling and user feedback

#### 2.3.2 CommandLineOptions Class
- **Namespace:** `toSrgbApp`
- **Purpose:** Parse and validate command-line arguments
- **Properties:**
  - `JpegQuality` (int?): Optional JPEG quality value (1-100)
  - `ImagePaths` (List<string>): Collection of input image file paths
- **Methods:**
  - `Parse(string[] args)`: Static method to parse command-line arguments

#### 2.3.3 ImageConverter Class
- **Namespace:** `toSrgbApp`
- **Purpose:** Core image processing operations
- **Methods:**
  - `ConvertToSrgb(MagickImage, bool)`: Converts image to sRGB color space
  - `SaveAsJpeg(MagickImage, string, int)`: Saves image as JPEG with specified quality

#### 2.3.4 JpegQualitySelector Class
- **Namespace:** `toSrgbApp`
- **Purpose:** Interactive console interface for quality selection
- **Methods:**
  - `PromptForQuality()`: Displays quality options and captures user selection

## 3. Functional Specifications

### 3.1 Command-Line Interface
```
Usage: toSrgbApp --quality NN <image1> [image2 ...]
```

#### 3.1.1 Parameters
- `--quality NN`: Optional JPEG quality parameter (1-100)
- `<image1> [image2 ...]`: One or more input image file paths

#### 3.1.2 Interactive Mode
When `--quality` is not specified, the application prompts for quality selection:
- **c - Compact (Quality 75)**: Smaller file size, metadata stripped
- **b - Balanced (Quality 85)**: Balanced size/quality, metadata stripped
- **d - Detailed (Quality 95)**: High quality, metadata preserved
- Interactive quality selection uses color-coded console output for clarity.

### 3.2 Image Processing Workflow

#### 3.2.1 Input Validation
- Verify file existence for each input path
- Display error messages for missing files
- Continue processing remaining files if some are missing

#### 3.2.2 Color Space Conversion Process
1. **Auto-orientation**: Apply EXIF orientation corrections
2. **ICC Profile Handling**:
   - If ICC profile exists: Transform using embedded profile, then remove ICC profile
   - If no ICC profile and not sRGB: Assume Adobe RGB and convert to sRGB
3. **Thumbnail Removal**: Remove embedded EXIF thumbnails to reduce file size
4. **Metadata Handling**: Strip metadata for quality 75/85, preserve for quality 95

#### 3.2.3 Filename Processing
- **Pattern Recognition**: Detect timestamp-prefixed filenames matching `YYYYMMDD-HHMMSS (basename).ext`
- **Filename Cleaning**: Extract clean basename from timestamped filenames
- **Fallback**: Use original filename if pattern doesn't match
- **Overwrite Risk**: If multiple input files resolve to the same cleaned filename, the last processed file will overwrite previous ones in the output directory. Users should ensure unique input filenames to avoid accidental overwrites.

### 3.3 Output Management

#### 3.3.1 Directory Structure
- **Base Path**: User's Desktop directory
- **Folder Naming**: `toSrgb-YYYYMMDD-HHMMSS-quality-NN`
- **Creation**: Directory created only when first file is processed

#### 3.3.2 File Naming
- Use cleaned filenames (timestamp prefixes removed)
- Preserve original file extensions
- Maintain original filename if no timestamp pattern detected

## 4. Quality Presets Analysis

### 4.1 Research-Based Quality Selection
The application's quality presets are based on analysis of JPEG quality from various sources:
- **Sony Alpha DSLR**: Original JPEGs ~96-97 quality
- **Xiaomi Phone**: Original JPEGs ~95-96 quality
- **ScanSnap Scanner**: Original JPEGs ~75-80 quality

### 4.2 Preset Definitions
- **Quality 75 (Compact)**: Optimized for sharing and storage efficiency
- **Quality 85 (Balanced)**: General-purpose compromise between size and quality
- **Quality 95 (Detailed)**: Near-lossless quality for professional use
- **Quality 100**: Intentionally excluded due to excessive file size with minimal visual benefit

### 4.3 Metadata Handling Strategy
- **Qualities 75/85**: Strip metadata to minimize file size
- **Quality 95**: Preserve metadata for professional workflows

## 5. Error Handling

### 5.1 File-Level Errors
- Individual file processing errors don't terminate the application
- Error messages displayed in red console text
- Processing continues with remaining files
- File-level errors are caught and reported per file, with processing continuing for remaining files.

### 5.2 Application-Level Errors
- Global exception handling with user-friendly error messages
- Application waits for user input before termination
- Console color coding for error visibility

### 5.3 Input Validation
- JPEG quality range validation (1-100)
- Command-line argument validation
- File existence verification

## 6. User Experience Features

### 6.1 Console Feedback
- Progress indication for each processed file
- Color-coded messages (red for errors, blue for information)
- Clear quality selection interface with use-case descriptions

### 6.2 Batch Processing
- Support for multiple input files
- Robust error handling that doesn't interrupt batch operations
- Consolidated output directory for all processed files

### 6.3 Quality Selection Guidance
- Clear descriptions of each quality preset
- Use-case recommendations for each option
- Visual indicators for metadata handling differences

## 7. Technical Constraints

### 7.1 Supported Formats
- **Input**: Any format supported by ImageMagick (focus on JPEG)
- **Output**: JPEG format only

### 7.2 Color Space Support
- **Source**: Adobe RGB, sRGB, or any color space with ICC profile
- **Target**: sRGB only

### 7.3 Platform Requirements
- .NET 9.0 runtime
- Windows, macOS, or Linux (cross-platform via .NET)
- ImageMagick native libraries (provided by Magick.NET package)

## 8. Performance Considerations

### 8.1 Memory Management
- Uses `using` statements for proper MagickImage disposal
- All image resources are disposed of immediately after processing using `using` statements, preventing memory leaks.
- Processes images individually to minimize memory footprint
- No concurrent processing (sequential batch processing)

### 8.2 File I/O
- Output directory created lazily (only when needed)
- Direct file-to-file processing without intermediate storage
- Efficient regex-based filename pattern matching

## 9. Future Enhancement Opportunities

### 9.1 Potential Features
- Support for additional output formats (PNG, TIFF)
- Concurrent/parallel processing for large batches
- Configuration file support for custom quality presets
- GUI version of the application
- Progress bars for large file processing

### 9.2 Quality Improvements
- More sophisticated color space detection
- Custom ICC profile support
- Batch operation logging
- Undo/rollback functionality

## 10. Version History

### v0.1 (Current)
- Initial release
- Basic Adobe RGB to sRGB conversion
- Three-tier quality preset system
- Batch processing capability
- Interactive quality selection
- Filename cleaning functionality
- Desktop output organization