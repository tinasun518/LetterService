Please note that right now the default setting is incorrect, only command with all parameters set up will work.
LetterService.exe -i <input path> -o <output path> -d <date> -r <root folder path> -a <archive path>



# LetterService

LetterService is a console application designed to process admission letters and scholarship letters for students. The application combines letters for the same student and saves them in the output folder. It also provides options to customize input and output paths, process date, root folder, and archive folder.

## Prerequisites

- .NET runtime environment

## Installation

1. Clone the repository or download the source code.
2. Build the project using the appropriate build tools or IDE.

## Usage
LetterService.exe -i <input path> -o <output path> -d <date> -r <root folder path> -a <archive path>



- `-i <input path>`: Specifies the path to the input folder. It overwrites the default input path defined in the root folder.
- `-o <output path>`: Specifies the path to the output folder. It overwrites the default output path defined in the root folder.
- `-d <date>`: Specifies the date in the `yyyyMMdd` format. If not provided, it defaults to today's date.
- `-r <root folder path>`: Specifies the path to the root folder. It overwrites the default root folder path.
- `-a <archive path>`: Specifies the path to the archive folder. It overwrites the default archive path defined in the root folder.

## Execution

1. Open a command prompt or terminal.
2. Navigate to the location of the compiled LetterService executable.
3. Execute the command with the desired options.

## Examples

- Process letters with default settings:



- `-i <input path>`: Specifies the path to the input folder. It overwrites the default input path defined in the root folder.
- `-o <output path>`: Specifies the path to the output folder. It overwrites the default output path defined in the root folder.
- `-d <date>`: Specifies the date in the `yyyyMMdd` format. If not provided, it defaults to today's date.
- `-r <root folder path>`: Specifies the path to the root folder. It overwrites the default root folder path.
- `-a <archive path>`: Specifies the path to the archive folder. It overwrites the default archive path defined in the root folder.

## Execution

1. Open a command prompt or terminal.
2. Navigate to the location of the compiled LetterService executable.
3. Execute the command with the desired options.

## Examples

- Process letters with default settings:
LetterService.exe


- Process letters with custom input and output paths:
LetterService.exe -i C:\input-folder -o C:\output-folder


- Process letters for a specific date:
LetterService.exe -d 20220125


- Process letters with a custom root folder and archive folder:
LetterService.exe -r C:\custom-root-folder -a C:\custom-archive-folder

