# StringCalculator

### Assumptions

- For the requirement of supporting a maximum of 2 numbers, if there were more than 2 numbers, I chose to add the first 2 values and ignore the rest. Alternative solutions would have been to ignore all input and return 0 or throw an exception.

### Considerations

- I decided to validate numbers in a private method that would be called in the Add method. I thought about calling it in the ParseInput method but realized it may be hidden logic and wouldn't exactly describe what ParseInput does. I call ValidateNumbers in the Add method so when other devs view the Add method they can easily understand what logic takes place. Additionally, all validation logic takes place in one method instead of having it spread around in different spots.
