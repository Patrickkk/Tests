# Design goals
- Easily turn exsisting code into a command line program
  - run a program.exe with arguments
  - automatically create a console program 
- Extensible 
- Support different types of grammar.


## Components

### Fluent api's

### setup
- => generate commands and parameters from exsisting types/assemblies

### running code
- Command parser => parses a single command into arguments (argumentlist).
- argumentlist parser => parses the argument list and turns the values into input to be used when invoking code.
- Code runner => runs the code with the given arguments.

# Roadmap
- Support for generics
- construct custom types from stings

# Updates
Untill version 1 is reached all development and (automatic) deployments come directly from master. This will mean that there might be some broken packages and breaking changes without notice.
once version 1 is reached prerelease will come from the develop branch and relases from the master branch.

