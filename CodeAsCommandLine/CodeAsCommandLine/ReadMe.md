# Design goals
- Easily turn exsisting code into a command line program
  - run a program.exe with arguments
  - automatically create a program 
- Extensible 
- 


## Components

### Fluent api's

### setup
- => generate commands and parameters from exsisting types/assemblies

### running code
- Command parser => parses a single command into arguments (argumentlist).
- argumentlist parser => parses the argument list and turns the values into input to be used when invoking code.
- Code runner => runs the code with the given arguments.



