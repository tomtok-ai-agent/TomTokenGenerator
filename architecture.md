# TomTokenGenerator - Project Architecture

## General Description
TomTokenGenerator is a C# CLI application that generates, serializes, and writes a sequence of TomToken tokens in JSON format. The application provides streaming data processing without buffering the entire volume of tokens in memory.

## Architecture Components

### 1. Data Model (Models)
- **TomToken** - base abstract class for all token types
- **TextToken** - "text" type token
- **NewLineToken** - "newline" type token
- **RepeatToken** - "repeat another token N times" type token
- **ReferenceToken** - "numeric reference to another entity" type token
- **TokenType** - enumeration of token types

### 2. Token Generator (Generators)
- **ITokenGenerator** - interface for token generators
- **RandomTokenGenerator** - implementation of random token generator
- **TokenGeneratorOptions** - settings for token generator

### 3. Serialization (Serialization)
- **ITokenSerializer** - interface for token serializers
- **JsonTokenSerializer** - implementation of token serializer to JSON
- **SerializationOptions** - settings for serialization

### 4. Translation Table (Translation)
- **TranslationTable** - class for storing and managing the translation table of numeric references
- **TranslationEntry** - entry in the translation table

### 5. Metadata (Metadata)
- **Metadata** - class for storing JSON metadata

### 6. CLI (CommandLine)
- **CommandLineOptions** - class for parsing command line arguments
- **Program** - main application class

## Data Flows

1. User launches the application with command line arguments
2. CommandLineOptions parses the arguments
3. Program creates instances of TokenGenerator and JsonTokenSerializer
4. TokenGenerator generates tokens one by one, without buffering the entire volume
5. JsonTokenSerializer serializes tokens to JSON in streaming mode
6. The result is written to a file or stdout depending on the arguments

## Project File Structure

```
TomTokenGenerator/
├── src/
│   ├── Models/
│   │   ├── TomToken.cs
│   │   ├── TextToken.cs
│   │   ├── NewLineToken.cs
│   │   ├── RepeatToken.cs
│   │   ├── ReferenceToken.cs
│   │   └── TokenType.cs
│   ├── Generators/
│   │   ├── ITokenGenerator.cs
│   │   ├── RandomTokenGenerator.cs
│   │   └── TokenGeneratorOptions.cs
│   ├── Serialization/
│   │   ├── ITokenSerializer.cs
│   │   ├── JsonTokenSerializer.cs
│   │   └── SerializationOptions.cs
│   ├── Translation/
│   │   ├── TranslationTable.cs
│   │   └── TranslationEntry.cs
│   ├── Metadata/
│   │   └── Metadata.cs
│   ├── CommandLine/
│   │   └── CommandLineOptions.cs
│   └── Program.cs
├── TomTokenGenerator.csproj
└── README.md
```

## Implementation Features

1. **Streaming Processing**: Using yield return for token generation and JsonTextWriter for streaming JSON writing.
2. **String Length Limitation**: Control of string length in JsonTextWriter.
3. **Efficient Memory Usage**: No buffering of the entire volume of tokens.
4. **Modularity**: Clear separation of responsibilities between components.
5. **Extensibility**: Ability to add new token types and serializers.
