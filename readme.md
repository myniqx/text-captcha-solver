# text-captcha-solver

This project is a TensorFlow-based artificial intelligence project that solves a 4-character captcha. 

## TensorFlowModel Class

The "TensorFlowModel" class contains the following data:
- Input layer: 50x50x3 (RGB image input)
- Convolutional layers: 3 convolutional layers with ReLU activation
- Recurrent layers: LSTM layer for sequence modeling
- Output layer:  Softmax layer for character classification

## Sample Generation and Training

With the program's interface, you can:
- Automatically generate samples
- Train the model
- Save the weights

## Cloning and Installation

To clone and install the C# project from the GitHub repository, follow these steps:

1. Clone the repository by running the following command:
```bash 
git clone <github_repository_link>
```

2. Install the necessary dependencies by running:
```bash
dotnet restore
```

3. Build the project using the following command:
```bash
dotnet build
```

## License

This project is licensed under the MIT License. You can find the full license text "here".>