# TomTokenGenerator

A C# CLI application for generating, serializing, and writing a sequence of TomToken tokens in JSON format.

## Features

- Streaming generation and serialization of tokens without buffering the entire volume
- JSON string length limitation (no more than 250 characters)
- Support for various token types: text, newline, repeat, reference
- Output to file or stdout
- Translation table for numeric references

## Token Types

- **TextToken** - "text" type token
- **NewLineToken** - "newline" type token (LF, CR, CRLF)
- **RepeatToken** - "repeat another token N times" type token
- **ReferenceToken** - "numeric reference to another entity" type token

## Usage

```bash
TomTokenGenerator [options]
```

### Options

- `--count, -c <number>` - Number of tokens to generate (default: 1000)
- `--output, -o <path>` - Path to output file
- `--stdout` - Output to standard output (default)
- `--help, -h` - Display help

### Examples

```bash
# Generate 1000 tokens and output to stdout
TomTokenGenerator

# Generate 10000 tokens and write to file
TomTokenGenerator --count 10000 --output tokens.json

# Generate 5000 tokens and output to stdout
TomTokenGenerator -c 5000 --stdout
```

## JSON Format

```json
{
  "metadata": {
    "name": "TomToken Sequence - 2025-05-28",
    "created_at": "2025-05-28T08:02:12Z",
    "generator_version": "1.0.0",
    "token_count": 1000,
    "TranslationTable": [
      {
        "id": 1,
        "token": {
          "type": "text",
          "value": " "
        }
      },
      // ... other translation table entries
    ]
  },
  "content": [
    {
      "type": "text",
      "value": "the"
    },
    {
      "type": "reference",
      "reference_id": 1
    },
    // ... other tokens
  ]
}
```

## Architecture

The project is divided into several components:

- **Models** - data models (TomToken and its descendants)
- **Generators** - token generators
- **Serialization** - token serializers
- **Translation** - translation table for numeric references
- **Metadata** - JSON metadata
- **CommandLine** - command line argument parsing

## Building and Running

```bash
# Build the project
dotnet build

# Run
dotnet run -- [options]
```
