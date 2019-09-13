# StringCalculator

### Overview

A C# application that calculates math operations given a string of integers that are properly delimited. The main components of this app are TDD, parsing and validating input, and returning correct calculations.

**Ex Input:**
`1,2`
`//;\n2;5`
`/[***]\n1***2***3`
`/['][/]\n1'2/3`

### Requirements to Run

- make
- .NET Core 2.2
- Docker (optional)

### Commands to Run

- `make run` or
  `make run alt-delimiter=<string> deny-negatives=<boolean> upperbound=<integer>`
- `make test`

### Docker to Run (optional)

- `make docker-build`
- `make docker-run` or
  `make docker-run alt-delimiter=<string> deny-negatives=<boolean> upperbound=<integer>`
- `make docker-test`

### Requirements

- [x] Support a maximum of 2 numbers using a comma delimiter
- [x] Support an unlimited number of numbers
- [x] Support a newline character as an alternative delimiter
- [x] Deny negative numbers
- [x] Ignore any number greater than 1000
- [x] Support 1 custom single character length delimiter
- [x] Support 1 custom delimiter of any length
- [x] Support multiple delimiters of any length

### Stretch Goals

- [x] Display the formula used to calculate the result
- [x] Allow the application to process entered entries until Ctrl+C is used
- [x] Allow the acceptance of arguments
- [ ] Use DI
- [x] Support subtraction, multiplication, and division operations

### Assumptions

- For the requirement of supporting a maximum of 2 numbers, if there were more than 2 numbers, I chose to add the first 2 values and ignore the rest. Alternative solutions would have been to ignore all input and return 0 or throw an exception.
- For displaying the formula stretch goal, I wasn't exactly sure the expected implementation on whether it should be it's own public method or just print when a calculation was made. I chose to print to the console when a calculation is made.

### Considerations

- I decided to validate numbers in a private method that would be called in the Calculate method. I thought about calling it in the ParseInput method but realized it may be hidden logic and wouldn't exactly describe what ParseInput does. I call ValidateNumbers in the Calculate method so when other devs view the Calculate method they can easily understand what logic takes place. Additionally, all validation logic takes place in one method instead of having it spread around in different spots.
- ~~I decided to hardcode the upperbound limit for the ignore any number greater than 1000 requirement. I will consider making it dynamic if I do the stretch goal.~~
- One performance consideration I had was everytime we call ParseInput which calls GetDelimiter it makes a new hashset instead of using an existing one. Garbage collection should handle it but another option is I could make a class member hashset which is probably the better solution, but for readability, I think this solution is good.
- One consideration I had implementing the different math operations was for PrintFormula and passing the mathOperation directly to it. I considered making the math operation a private class field so it didn't need to be passed around but I didn't feel it was necessary to make it a field and clutter the class if it didn't really need it.
