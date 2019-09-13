# StringCalculator

### Requirements

- [x] Support a maximum of 2 numbers using a comma delimiter
- [x] Support an unlimited number of numbers
- [x] Support a newline character as an alternative delimiter
- [x] Deny negative numbers
- [x] Ignore any number greater than 1000
- [x] Support 1 custom single character length delimiter
- [x] Support 1 custom delimiter of any length
- [ ] Support multiple delimiters of any length

### Stretch Goals

- [ ] Display the formula used to calculate the result
- [ ] Allow the application to process entered entries until Ctrl+C is used
- [ ] Allow the acceptance of arguments
- [ ] Use DI
- [ ] Support subtraction, multiplication, and division operations

### Assumptions

- For the requirement of supporting a maximum of 2 numbers, if there were more than 2 numbers, I chose to add the first 2 values and ignore the rest. Alternative solutions would have been to ignore all input and return 0 or throw an exception.

### Considerations

- I decided to validate numbers in a private method that would be called in the Add method. I thought about calling it in the ParseInput method but realized it may be hidden logic and wouldn't exactly describe what ParseInput does. I call ValidateNumbers in the Add method so when other devs view the Add method they can easily understand what logic takes place. Additionally, all validation logic takes place in one method instead of having it spread around in different spots.
- I decided to hardcode the upperbound limit for the ignore any number greater than 1000 requirement. I will consider making it dynamic if I do the stretch goal.
- One performance consideration I had was everytime we call ParseInput which calls GetDelimiter it makes a new hashset instead of using an existing one. Garbage collection should handle it but another option is I could make a class member hashset which is probably the better solution, but for readability, I think this solution is good.
